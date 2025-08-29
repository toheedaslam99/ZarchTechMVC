using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace E_infoGlobalNew.Helper
{
    public static class ApplicationHelper
    {
        #region "Page Name Constant Value Helper"
        public const string PageLogin = "login";
        public const string PageForgotPassword = "forgot-password";
        public const string PageResetPassword = "reset-password";
        public const string PageRegister = "register";
        public const string PageDashboard = "dashboard";
        public const string PageProfile = "profile";
        public const string PageLogout = "logout";
        public const string TimeZone = "Pakistan Standard Time";
        #endregion
        #region Value Helper
        public const string LoginUserSession = "LoginUserSession";
        public const string LoginUserCookie = "LoginUserCookie";
        public const string EncryptKey = "FCSystem";
        #endregion
        #region WebsiteValueHelper
        public const string UserProfileFilePath = "uploads/UserProfileFilePath";
        public const string CompanyLogoPath = "uploads/CompanyLogo";
        public const string AccountProfileFilePath = "uploads/AccountProfileFile";
        public const string ProductImageFilePath = "uploads/ProductImage";
        #endregion
        #region EnumHelper
        public static class EnumStatus
        {
            public const string Enable = "Enable";
            public const string Disable = "Disable";
        }
        public static class EnumDrCr
        {
            public const string Debit = "Debit";
            public const string Credit = "Credit";
        }
        public static class EnumProfle
        {
            public const string Supplier = "Vendor";
            public const string Customer = "Customer";
            public const string Employee = "Employee";
            public const string CompanyProfile = "CompanyProfile";
        }
        public static class EnumBatchNo
        {
            public const string CashPayment = "CashPayment";
            public const string CashReceived = "CashReceived";
            public const string BankPayment = "BankPayment";
            public const string BankReceived = "BankReceived";
        }
        public static class EnumMenuType
        {
            public const string Parent = "P";
            public const string Children = "C";
        }
        public static class EnumRole
        {
            public const string SuperAdministrator = "Super Administrator";
            public const string Administrator = "Administrator";
            public const string Employee = "Employee";
            public const string Leader = "Leader";
            public const string Vendor = "Vendor";
        }
        public static class EnumPageType
        {
            public const string Index = "Index";
            public const string Add = "Add";
            public const string Edit = "Edit";
            public const string View = "View";
            public const string Sorting = "Sorting";
            public const string Import = "Import";
            public const string Print = "Print";
        }
        public static class EnumJQueryResponseType
        {
            public const string DataOnly = "D";
            public const string MessageOnly = "M";
            public const string RedirectOnly = "T";
            public const string RefreshOnly = "R";
            public const string ReloadOnly = "RL";
            public const string MessageAndRedirect = "M-T";
            public const string MessageAndRedirectWithDelay = "M-TD";
            public const string MessageAndRefresh = "M-R";
            public const string MessageRefreshRedirect = "M-R-T";
            public const string MessageRefreshRedirectWithDelay = "M-R-TD";
            public const string RefreshAndRedirect = "R-T";
            public const string RefreshAndRedirectWithDelay = "R-TD";
            public const string RedirectWithDelay = "TD";
            public const string MessageAndReloadWithDelay = "M-RLD";
        }
        #endregion
        #region AjaxResponse
        public class AjaxResponse
        {
            public bool Success { get; set; }
            public string Type { get; set; }
            public string FieldName { get; set; }
            public string Message { get; set; }
            public string TargetURL { get; set; }
            public string Data { get; set; }
        }
        #endregion
        #region Core Functions
        public static string GetViewName(string _viewName)
        {
            return "~/Views/" + _viewName + ".cshtml";
        }
        public static DateTime GetDateTime(string timeZoneId)
        {
            TimeZoneInfo time_zone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
            return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, time_zone);
        }
        public static DateTime GetUtcDateTime()
        {
            //TimeZoneInfo time_zone = TimeZoneInfo.FindSystemTimeZoneById("Pakistan Standard Time");
            //return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, time_zone);
            return DateTime.UtcNow;
        }
        public static string TextToSlug(string _value)
        {
            Regex rgx = new Regex(@"[^-a-zA-Z0-9\d\s]");
            // Replace Special Charater and space with emptystring 
            string finalOutput = rgx.Replace(_value, "");
            Regex rgx1 = new Regex("\\s+");
            // Replace space with underscore 
            finalOutput = rgx1.Replace(finalOutput, "-");
            if (finalOutput.StartsWith("-") || finalOutput.StartsWith(" "))
            {
                finalOutput = finalOutput.TrimStart(finalOutput[0]);
            }
            if (finalOutput.EndsWith("-") || finalOutput.EndsWith(" "))
            {
                finalOutput = finalOutput.TrimEnd(finalOutput[finalOutput.Length - 1]);
            }
            return finalOutput.ToLower();
        }
        public static string TrimCharacters(string _value, int _length = 1)
        {
            if (string.IsNullOrWhiteSpace(_value))
            {
                return _value;
            }
            else
            {
                return _value.TrimEnd(_value[_value.Length - _length]);
            }
        }
        public static string FirstCharToUpper(string input)
        {
            if (String.IsNullOrEmpty(input))
                throw new ArgumentException("ARGH!");
            return input.First().ToString().ToUpper() + String.Join("", input.Skip(1));
        }
        public static string StripHtmlTags(string _value)
        {
            return Regex.Replace(_value, "<.*?>|&.*?;", string.Empty);
        }
        public static string UpperCaseWords(string value)
        {
            char[] array = value.ToCharArray();
            // Handle the first letter in the string.
            if (array.Length >= 1)
            {
                if (char.IsLower(array[0]))
                {
                    array[0] = char.ToUpper(array[0]);
                }
            }
            // Scan through the letters, checking for spaces.
            // ... Uppercase the lowercase letters following spaces.
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i - 1] == ' ')
                {
                    if (char.IsLower(array[i]))
                    {
                        array[i] = char.ToUpper(array[i]);
                    }
                }
            }
            return new string(array);
        }
        public static string UpperCaseFirstLetter(string _value)
        {
            // Check for empty string.
            if (string.IsNullOrEmpty(_value))
            {
                return string.Empty;
            }
            // Return char and concat substring.
            return char.ToUpper(_value[0]) + _value.Substring(1);
        }
        public static List<string> GetTimeZoneList()
        {
            List<string> timeList = new List<string>();
            var tzone = TimeZoneInfo.GetSystemTimeZones();
            foreach (TimeZoneInfo tzi in tzone)
            {
                timeList.Add(tzi.Id);
            }
            return timeList;
        }
        public static string CreateFileName(string fileName)
        {
            string[] strGUID = Guid.NewGuid().ToString().Split(new char[] { '-' });
            return fileName + "-" + strGUID[0];
        }
        public static int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }
        public static string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }
        public static string RandomPassword(int size = 0)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(RandomString(4, true));
            builder.Append(RandomNumber(1000, 9999));
            builder.Append(RandomString(2, false));
            return builder.ToString();
        }
        #endregion
        #region "Redirect Page Helper"
        public static List<string> ListMenuPermissions()
        {
            List<string> permission = new List<string>();
            permission.Add("None");
            permission.Add("All");
            permission.Add("Add");
            permission.Add("Edit");
            permission.Add("Delete");
            permission.Add("View");
            permission.Add("Print");
            return permission;
        }
        public static List<string> WithOutLoginView()
        {
            List<string> PageList = new List<string>();
            PageList.Add(PageLogin);
            PageList.Add(PageForgotPassword);
            PageList.Add(PageResetPassword);
            PageList.Add(PageRegister);
            return PageList;
        }
        public static List<string> WithLoginView()
        {
            List<string> PageList = new List<string>();
            PageList.Add(PageProfile);
            PageList.Add(PageLogout);
            return PageList;
        }
        public static List<string> AllowedLink()
        {
            List<string> PageList = new List<string>();
            PageList.Add("home");
            PageList.Add("home/logout");
            PageList.Add("home/unauthorized");
            PageList.Add("home/profile");
            PageList.Add("home/valuation");
            return PageList;
        }
        #endregion
        #region "Default Values Helper"
        public static int ParseInt(object value)
        {
            int parseVal;
            return ((value == null) || (value == DBNull.Value)) ? 0 : int.TryParse(value.ToString(), out parseVal) ? parseVal : 0;
        }
        public static decimal ParseDecimal(object value)
        {
            decimal parseVal;
            return ((value == null) || (value == DBNull.Value)) ? 0 : decimal.TryParse(value.ToString(), out parseVal) ? parseVal : 0;
        }
        public static double ParseDouble(object value)
        {
            double parseVal;
            return ((value == null) || (value == DBNull.Value)) ? 0 : double.TryParse(value.ToString(), out parseVal) ? parseVal : 0;
        }
        public static DateTime ParseDateTime(object value)
        {
            DateTime parseVal;
            return ((value == null) || (value == DBNull.Value)) ? new DateTime(1900, 1, 1) : DateTime.TryParse(value.ToString(), out parseVal) ? parseVal : new DateTime(1900, 1, 1);
        }
        public static string ParseString(object value)
        {
            return ((value == null) || (value == DBNull.Value)) ? string.Empty : value.ToString();
        }
        public static bool ParseBoolean(object value)
        {
            bool parseVal;
            return ((value == null) || (value == DBNull.Value)) ? false : bool.TryParse(value.ToString(), out parseVal) ? parseVal : false;
        }
        public static bool IsEmailAddressValid(string EmailAddress)
        {
            string pattern = @"[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\.)+[a-zA-z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?";
            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
            return regex.IsMatch(EmailAddress);
        }
        #endregion
    }
}