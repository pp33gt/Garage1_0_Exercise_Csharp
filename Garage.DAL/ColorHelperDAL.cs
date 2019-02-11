using System;

namespace Garage.DAL
{
    public static class ColorHelperDAL
    {
        public static Color ParseColor(string colorString)
        {
            if (Enum.TryParse(colorString, out Color tmpColor))
            {
                return tmpColor;
            }
            throw new ArgumentException("Error: Could not parse colorString to Enum");
        }
    }
}
