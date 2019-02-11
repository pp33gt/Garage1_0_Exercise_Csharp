namespace Garage.BLL.DomainEntities
{
    public class AirPlane : Vehicle
    {
        public int NumberOfAirPlaneWings { get; }

        public AirPlane(int licenseNo, int numberOfWheels, Color color, int numberOfAirPlaneWings) : base(licenseNo, numberOfWheels, color)
        {
            NumberOfAirPlaneWings = numberOfAirPlaneWings;
        }

        public override string ToString()
        {
            return base.ToString() + $" NumberOfAirPlaneWings: {NumberOfAirPlaneWings}";
        }
    }

}
