using System;

namespace CSC340_ordering_sytem.Models.Helpers
{
    public class CreditCardHelper
    {
        public static string GenerateToken()
        {
            var g = Guid.NewGuid();
            var guidString = Convert.ToBase64String(g.ToByteArray());
            guidString = guidString.Replace("=", "");
            guidString = guidString.Replace("+", "");
            return guidString;
        }
    }
}