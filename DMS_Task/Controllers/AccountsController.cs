﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DAL.Models;
using DMS_Task.ViewModels;
using Microsoft.AspNetCore.Identity;
using DMS_Task.JwtFeatures;
using System.IdentityModel.Tokens.Jwt;
using DAL;
using Microsoft.AspNetCore.Authorization;
using DMS_Task.Constants;
using DMS_Task.services.email;
using Microsoft.AspNetCore.WebUtilities;

namespace DMS_Task.Controllers
{
    [Route("api/accounts")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly JwtHandler _jwtHandler;
        private readonly IEmailSender _emailSender;
        private readonly IUnitOfWork _unitOfWork;


        public AccountsController(UserManager<ApplicationUser> userManager, IMapper mapper, JwtHandler jwtHandler,
            IEmailSender emailSender, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _mapper = mapper;
            _jwtHandler = jwtHandler;
            _emailSender = emailSender;
            _unitOfWork = unitOfWork;
        }

        [HttpPost("Registration")]
        public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userForRegistration)
        {
            if (userForRegistration == null || !ModelState.IsValid)
                return BadRequest();

            var user = _mapper.Map<ApplicationUser>(userForRegistration);

            var result = await _userManager.CreateAsync(user, userForRegistration.Password);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);

                return BadRequest(new RegistrationResponseDto { Errors = errors });
            }

            await _userManager.AddToRoleAsync(user, Authorization.Roles.Employee.ToString());
            user.EmailConfirmed = true;

            var customer = new Customer() { Name = user.FirstName + " " + user.LastName, IsVisible = true, UserId = user.Id };
            _unitOfWork.Customer.Add(customer);
            
            _unitOfWork.SaveChanges();

            //var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            //var param = new Dictionary<string, string>
            //{
            //    {"token", token },
            //    {"email", user.Email }
            //};
            //var callback = QueryHelpers.AddQueryString(userForRegistration.ClientURI, param);
            //var message = new Message(new string[] { userForRegistration.Email }, "Email Confirmation token", callback, null, false);
            //await _emailSender.SendEmailAsync(message);

            return StatusCode(201);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserForAuthenticationDto userForAuthentication)
        {
            var user = await _userManager.FindByEmailAsync(userForAuthentication.Email);

            if (user == null)
                return BadRequest("Authentication failed. Wrong Username or Password");

            //you can check here if the account is locked out in case the user enters valid credentials after locking the account.
            if (await _userManager.IsLockedOutAsync(user))
            {
                // var content = $"Your account is locked out. To reset the password click this link: {userForAuthentication.clientURI}";
                // var message = new Message(new string[] { userForAuthentication.Email }, "Locked out account information", content, null);
                // await _emailSender.SendEmailAsync(message);

                return Unauthorized(new AuthResponseDto { ErrorMessage = "The account is locked out" });
            }

            if (!await _userManager.CheckPasswordAsync(user, userForAuthentication.Password))
            {
                await _userManager.AccessFailedAsync(user);

                return Unauthorized(new AuthResponseDto { ErrorMessage = "Invalid Authentication" });
            }


            var signingCredentials = _jwtHandler.GetSigningCredentials();
            //var claims = _jwtHandler.GetClaims(user);

            var claims = await _jwtHandler.GetClaims(user);
            var tokenOptions = _jwtHandler.GenerateTokenOptions(signingCredentials, claims);
            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            await _userManager.ResetAccessFailedCountAsync(user);

            return Ok(new AuthResponseDto { IsAuthSuccessful = true, Token = token });
        }

        [HttpPost("ExternalLogin")]
        public async Task<IActionResult> ExternalLogin([FromBody] ExternalAuthDto externalAuth)
        {
            var payload = await _jwtHandler.VerifyGoogleToken(externalAuth);
            if (payload == null)
                return BadRequest("Invalid External Authentication.");

            var info = new UserLoginInfo(externalAuth.Provider, payload.Subject, externalAuth.Provider);

            var user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);
            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(payload.Email);

                if (user == null)
                {
                    user = new ApplicationUser { Email = payload.Email, UserName = payload.Email, FirstName = payload.GivenName, LastName = payload.FamilyName };
                    await _userManager.CreateAsync(user);

                    //TODO: prepare and send an email for the email confirmation

                    await _userManager.AddToRoleAsync(user, Authorization.Roles.Employee.ToString());
                    await _userManager.AddLoginAsync(user, info);

                    //var employee = new Employee() { UserId = user.Id, Photo = "Resources/images/user-m.png" };
                    //_unitOfWork.Employees.Add(employee);
                    //_unitOfWork.SaveChanges();
                }
                else
                {
                    if (await _userManager.IsInRoleAsync(user, Authorization.Roles.Company.ToString()) &&
                        !user.EmailConfirmed)
                    {
                        return BadRequest("Invalid External Authentication., your account is under review");
                    }

                    await _userManager.AddLoginAsync(user, info);
                }
            }
            //check for the Locked out account


            var token = await _jwtHandler.GenerateToken(user);
            return Ok(new AuthResponseDto { Token = token, IsAuthSuccessful = true });
        }

        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto forgotPasswordDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var user = await _userManager.FindByEmailAsync(forgotPasswordDto.Email);
            if (user == null)
                return BadRequest("Email Not Found");

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var param = new Dictionary<string, string>
            {
                {"token", token },
                {"email", forgotPasswordDto.Email }
            };

            var callback = QueryHelpers.AddQueryString(forgotPasswordDto.ClientURI, param);

            var message = new Message(new string[] { user.Email }, "Reset password token", callback, null, false);
            await _emailSender.SendEmailAsync(message);

            return Ok();
        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto resetPasswordDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var user = await _userManager.FindByEmailAsync(resetPasswordDto.Email);
            if (user == null)
                return BadRequest("Invalid Request");

            var resetPassResult = await _userManager.ResetPasswordAsync(user, resetPasswordDto.Token, resetPasswordDto.Password);
            if (!resetPassResult.Succeeded)
            {
                var errors = resetPassResult.Errors.Select(e => e.Description);

                return BadRequest(new { Errors = errors });
            }

            await _userManager.SetLockoutEndDateAsync(user, new DateTime(2000, 1, 1));
            return Ok();
        }

        [HttpGet("EmailConfirmation")]
        public async Task<IActionResult> EmailConfirmation([FromQuery] string email, [FromQuery] string token)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return BadRequest("Invalid Email Confirmation Request");

            var confirmResult = await _userManager.ConfirmEmailAsync(user, token);
            if (!confirmResult.Succeeded)
                return BadRequest("Invalid Email Confirmation Request");

            return Ok();
        }

        [Authorize]
        [HttpGet("Privacy")]
        public IActionResult Privacy()
        {
            var claims = User.Claims
                .Select(c => new { c.Type, c.Value })
                .ToList();

            return Ok(claims);
        }

        //[HttpPost("CompanyRegistration")]
        //public async Task<IActionResult> RegisterCompany([FromBody] CompanyForRegistrationDto companyForRegistration)
        //{
        //    if (companyForRegistration == null || !ModelState.IsValid)
        //        return BadRequest();

        //    var user = _mapper.Map<ApplicationUser>(companyForRegistration);

        //    var result = await _userManager.CreateAsync(user, companyForRegistration.Password);
        //    if (!result.Succeeded)
        //    {
        //        var errors = result.Errors.Select(e => e.Description);

        //        return BadRequest(new RegistrationResponseDto { Errors = errors });
        //    }

        //    await _userManager.AddToRoleAsync(user, Authorization.Roles.Company.ToString());

        //    var company = _mapper.Map<Company>(companyForRegistration);
        //    company.RequestStatusId = _unitOfWork.CompanyRequestStatus.GetSingleOrDefault(cr =>
        //        cr.Name == Enums.CompanyRequestStatus.UnderReview.ToString()).Id;

        //    company.Logo = "/Resources/company-logos/company-placeholder.png";

        //    var companyManager = new CompanyManager() { UserId = user.Id, Title = companyForRegistration.Title, Company = company };
        //    _unitOfWork.CompaniesManagers.Add(companyManager);

        //    _unitOfWork.SaveChanges();

        //    var message = new Message(new string[] { companyForRegistration.Email }, "Company account under review",
        //        "we received your request and will get back to you  as soon as it is reviewed", null, false);
        //    await _emailSender.SendEmailAsync(message);

        //    return StatusCode(201);
        //}
    }
}
