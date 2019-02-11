namespace Garage.BLL.DomainEntities
{
    public class Vehicle
    {
        public int LicenseNo { get; }
        public string LicenseNumber { get { return $"{LicenseNo:0000}"; } }
        public int NumberOfWheels { get; }
        public Color Color { get; }

        public Vehicle(int licenseNo, int numberOfWheels, Color color)
        {
            LicenseNo = licenseNo;
            NumberOfWheels = numberOfWheels;
            Color = color;
        }

        public override string ToString()
        {
            var type = Common.Util.GetLastPartOfType(GetType());
            var result = type.PadLeft(12);
            result += " LicenseNumber: " + LicenseNumber;
            result += " Color: " + Color.ToString().PadRight(7);
            result += "NumberOfWheels: " + NumberOfWheels.ToString().PadRight(5);
            return result;
        }
    }

}
