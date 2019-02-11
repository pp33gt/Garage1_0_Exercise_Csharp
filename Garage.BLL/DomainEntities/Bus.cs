namespace Garage.BLL.DomainEntities
{
    public class Bus : Vehicle
    {
        public int BusMaxNumberOfStandingPassengers { get; }

        public Bus(int licenseNo, int numberOfWheels, Color color, int busMaxNumberOfStandingPassengers) : base(licenseNo, numberOfWheels, color)
        {
            BusMaxNumberOfStandingPassengers = busMaxNumberOfStandingPassengers;
        }

        public override string ToString()
        {
            return base.ToString() + $" BusMaxNumberOfStandingPassengers: {BusMaxNumberOfStandingPassengers}";
        }
    }

}
