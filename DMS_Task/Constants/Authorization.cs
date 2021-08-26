using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DMS_Task.Constants
{
    public class Authorization
    {
        public enum Roles
        {
            Administrator,
            Employee,
            Company
        }

        public const string AdminUserName = "admin@shop.com";
        public const string AdminEmail = "admin@shop.com";
        public const string AdminPassword = "Aa12345";
        public const Roles AdminRole = Roles.Administrator;
        public const string Admin = "Administrator";

        //public const string CompanyUserName = "company@shop.com";
        //public const string CompanyEmail = "company@shop.com";
        //public const string CompanyPassword = "Aa12345";
        //public const Roles  CompanyRole = Roles.Company;
        //public const string Company = "Company";


        public const string EmployeeUserName = "customer@shop.com";
        public const string EmployeeEmail = "customer@shop.com";
        public const string EmployeePassword = "Aa12345";
        public const Roles  EmployeeRole = Roles.Employee;
        public const string Employee = "Employee";

    }
}
