using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage.Common
{
    public class Util
    {
        public static int? StringToNullableInt(string text)
        {
            int? result = null;

            if (text == null) return null;

            text = text.Trim();

            if(int.TryParse(text, out int tmpResult))
            {
                result = tmpResult;
            }

            return result;
        }

        public static string GetLastPartOfType(Type t)
        {
            var typeFrags = t.ToString().Split('.');
            if (typeFrags.Length > 0)
            {
                return typeFrags[typeFrags.Count() - 1];
            }
            return string.Empty;
        }
        
    }
}
