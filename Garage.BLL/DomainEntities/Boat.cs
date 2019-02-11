namespace Garage.BLL.DomainEntities
{
    public class Boat : Vehicle
    {
        public int NumberOfBoatSails { get; }

        public Boat(int licenseNo, int numberOfWheels, Color color, int numberOfBoatSails) : base(licenseNo, numberOfWheels, color)
        {
            NumberOfBoatSails = numberOfBoatSails;
        }

        public override string ToString()
        {
            return base.ToString() + $" NumberOfBoatSails: {NumberOfBoatSails}";
        }
    }

}
