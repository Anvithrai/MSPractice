﻿namespace Mango.Web.Utilities
{
    public class SD
    {
        public static string CouponApiBase {get;set;}
        public static string ProductApiBase { get; set; }
        public static string AuthApIBase { get; set; }
        public const string RoleAdmin = "ADMIN";
        public const string RoleCustomer = "CUSTOMER";

        public enum ApiType
        {
            GET,
            POST,
            PUT,
            DELETE
        }
    }
}
