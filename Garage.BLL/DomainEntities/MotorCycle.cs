namespace Garage.BLL.DomainEntities
{
    public class MotorCycle : Vehicle
    {
        public int MotorCycleTopSpeed { get; }

        public MotorCycle(int licenseNo, int numberOfWheels, Color color, int motorCycleTopSpeed) : base(licenseNo, numberOfWheels, color)
        {
            MotorCycleTopSpeed = motorCycleTopSpeed;
        }

        public override string ToString()
        {
            return base.ToString() + $" MotorCycleTopSpeed: {MotorCycleTopSpeed}";
        }
    }

}
