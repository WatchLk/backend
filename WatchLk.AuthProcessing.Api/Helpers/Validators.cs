using System.Text.RegularExpressions;

namespace WatchLk.AuthProcessing.Api.Helpers
{
    public class Validators
    {
        private static readonly string _emailRegex = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }
            
            try
            {
                return Regex.Match(email, _emailRegex).Success;
            }
            catch
            {
                return false;
            }
        }
    }
}
