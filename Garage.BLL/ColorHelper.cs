using System;
using System.Collections.Generic;
using System.Linq;

namespace Garage.BLL.DomainEntities
{
    public static class ColorHelper
    {
        internal static bool TryParseColor(string colorString, out Color? color)
        {
            color = null;
            if (Enum.TryParse(colorString, out Color tmpColor))
            {
                color = tmpColor;
                return true;
            }
            return false;
        }

        internal static Color ParseColor(string colorString)
        {
            if (Enum.TryParse(colorString, out Color tmpColor))
            {
                return tmpColor;
            }
            throw new ArgumentException("Error: Could not parse colorString to Enum");
        }

        internal static IEnumerable<Color> GetColorNames()
        {
            return Enum.GetValues(typeof(Color)).Cast<Color>();
        }
    }
}
