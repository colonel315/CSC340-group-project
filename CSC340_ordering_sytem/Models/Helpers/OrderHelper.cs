using System;

namespace CSC340_ordering_sytem.Models.Helpers
{
    public class OrderHelper
    {
        public static string GenerateOrderNumber()
        {
            var g = Guid.NewGuid();
            var guidString = Convert.ToBase64String(g.ToByteArray());
            guidString = guidString.Replace("=", "");
            guidString = guidString.Replace("+", "");
            return guidString;
        }
    }
}