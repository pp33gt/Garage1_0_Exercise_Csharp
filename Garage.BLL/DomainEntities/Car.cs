namespace Garage.BLL.DomainEntities
{
    public class Car : Vehicle
    {
        public int NumberOfCarSeats { get; }

        public Car(int licenseNo, int numberOfWheels, Color color, int numberOfCarSeats) : base(licenseNo, numberOfWheels, color)
        {
            NumberOfCarSeats = numberOfCarSeats;
        }

        public override string ToString()
        {
            return base.ToString() + $" NumberOfCarSeats: {NumberOfCarSeats}";
        }
    }

}
