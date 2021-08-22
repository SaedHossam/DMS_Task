using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Text;
using DAL;
using DAL.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using DMS_Task.JwtFeatures;
using DMS_Task.services.email;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace DMS_Task
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //To avoid the MultiPartBodyLength
            services.Configure<FormOptions>(o => {
                o.ValueLengthLimit = int.MaxValue;
                o.MultipartBodyLengthLimit = int.MaxValue;
                o.MemoryBufferThreshold = int.MaxValue;
            });

            services.AddControllersWithViews()
            .AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            //User Manager Service
            services.AddIdentity<ApplicationUser, IdentityRole>(opt =>
                {
                    opt.Password.RequireNonAlphanumeric = false;

                    opt.Lockout.AllowedForNewUsers = true;
                    opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(2);
                    opt.Lockout.MaxFailedAccessAttempts = 3;
                }).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

            services.Configure<DataProtectionTokenProviderOptions>(opt =>
                opt.TokenLifespan = TimeSpan.FromHours(2));

            // Add Google Authentication
            services.AddAuthentication().AddGoogle("google", opt =>
            {
                var googleAuth = Configuration.GetSection("Authentication:Google");
                opt.ClientId = googleAuth["ClientId"];
                opt.ClientSecret = googleAuth["ClientSecret"];
                opt.SignInScheme = IdentityConstants.ExternalScheme;
            });

            //Adding DB Context with MSSQL
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection"),
                    b =>
                    {
                        b.UseNetTopologySuite();
                        b.MigrationsAssembly("DAL");
                    }
                    ));

            var jwtSettings = Configuration.GetSection("JwtSettings");
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = jwtSettings.GetSection("validIssuer").Value,
                    ValidAudience = jwtSettings.GetSection("validAudience").Value,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.GetSection("securityKey").Value))
                };
            });

            services.AddAutoMapper(typeof(Startup));
            services.AddControllers();

            var emailConfig = Configuration
                .GetSection("EmailConfiguration")
                .Get<EmailConfiguration>();
            services.AddSingleton(emailConfig);

            services.AddScoped<IUnitOfWork, HttpUnitOfWork>();
            services.AddScoped<IEmailSender, EmailSender>();

            services.AddScoped<JwtHandler>();

            services.Configure<FormOptions>(o =>
            {
                o.ValueLengthLimit = int.MaxValue;
                o.MultipartBodyLengthLimit = int.MaxValue;
                o.MemoryBufferThreshold = int.MaxValue;
            });

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DMS_Task", Version = "v1" });
                c.OperationFilter<AuthorizeCheckOperationFilter>();
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Scheme = "bearer",
                    Description = "Please insert JWT token into field",
                });

                //c.AddSecurityRequirement(new OpenApiSecurityRequirement
                //{
                //    {
                //        new OpenApiSecurityScheme
                //        {
                //            Reference = new OpenApiReference
                //            {
                //                Type = ReferenceType.SecurityScheme,
                //                Id = "Bearer"
                //            }
                //        },
                //        new string[] { }
                //    }
                //});
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // configuration for upload api
            //app.UseStaticFiles();
            Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), @"Resources/images"));
            Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), @"Resources/cv"));
            Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), @"Resources/company-logos"));

            string cachePeriod = env.IsDevelopment() ? "600" : "31536000";
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Resources")),
                RequestPath = new PathString("/Resources"),

                OnPrepareResponse = staticFileResponceContext =>
                {

                    var directoryName = new FileInfo(staticFileResponceContext.File.PhysicalPath).Directory?.Name;

                    if (directoryName != null && (directoryName.Equals("images", StringComparison.OrdinalIgnoreCase) ||
                                                  directoryName.Equals("company-logos", StringComparison.OrdinalIgnoreCase) ||
                                                  directoryName.Equals("cv", StringComparison.OrdinalIgnoreCase)
                        ))
                    {
                        staticFileResponceContext.Context.Response.Headers.Append("Cache-Control", $"public, no-store");
                    }
                    else
                    {
                        staticFileResponceContext.Context.Response.Headers.Append("Cache-Control", $"public, max-age={cachePeriod}");
                    }
                }
            });

            //var jsonTextCities = System.IO.File.ReadAllText(@"cities.json");
            //Seeding.Seed<Country>(jsonTextCities, app.ApplicationServices);


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DMS_Task v1"));
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    // Live reload not working for .net5 and ng11 for now,
                    // see https://github.com/dotnet/aspnetcore/issues/29478
                    // spa.UseAngularCliServer(npmScript: "start");
                    // spa.Options.StartupTimeout = TimeSpan.FromSeconds(180); // Increase the timeout if angular app is taking longer to startup
                    spa.UseProxyToSpaDevelopmentServer("http://localhost:4200"); // Use this instead to use the angular cli server
                }
            });
        }
    }
}
