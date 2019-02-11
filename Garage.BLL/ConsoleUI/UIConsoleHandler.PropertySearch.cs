using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Garage.BLL.DomainEntities;
using Garage.Common;

namespace Garage.BLL.ConsoleUI
{
    public partial class UIConsoleHandler
    {
        private List<PropName> propertySearchUserPrompts = new List<PropName>() {
                            PropName.LicenseNumber,
                            PropName.NumberOfWheels,
                            PropName.Color,
                            PropName.NumberOfAirPlaneWings ,
                            PropName.MotorCycleTopSpeed ,
                            PropName.NumberOfCarSeats ,
                            PropName.BusMaxNumberOfStandingPassengers ,
                            PropName.NumberOfBoatSails
                        };

        private Dictionary<PropName, string> GetUserInputForAllProperties()
        {
            var propertySearchUserInputValues = new Dictionary<PropName, string>();

            foreach (var item in propertySearchUserPrompts)
            {
                Console.WriteLine(item.ToString());

                var userInput = Console.ReadLine().Trim();
                var userInputEvaluated = userInput == string.Empty ? null : userInput;
                propertySearchUserInputValues.Add(item, userInputEvaluated);
            }

            return propertySearchUserInputValues;
        }

        private void UserFindVehicleByProperties()
        {
            Console.WriteLine("\nFind Vehicle By Property:");
            
            Dictionary<PropName, string> propertySearchUserInputValues = GetUserInputForAllProperties();

            Garage<Vehicle>.ListVehicleByPropertyArgs listVehicleByPropertyArguments = MapPropertySearchValues(propertySearchUserInputValues, out List<string> errors);
            if (errors.Count > 0)
            {
                errors.ForEach(a => Console.WriteLine(a));
            }

            var vehicles = GarageHandler.FindVehiclesByProperty(listVehicleByPropertyArguments);

            UserListVehicles(vehicles);

            if (vehicles.Count == 1)
            {
                UserUnparkVehicle(vehicles[0]);
            }
        }

        private static Garage<Vehicle>.ListVehicleByPropertyArgs MapPropertySearchValues(Dictionary<PropName, string> propertySearchUserInputValues, out List<string> errors)
        {
            errors = new List<string>();

            Color? userInputColorEvaluated = null;
            var colorValue = propertySearchUserInputValues[PropName.Color];
            if (colorValue != null)
            {
                if (TryParseColor(colorValue, out Color tmpColor))
                {
                    userInputColorEvaluated = tmpColor;
                }
                else
                {
                    var validColors = ColorHelper.GetColorNames();
                    errors.Add("Error invalid Color, Valid Colors are:");
                    foreach (var item in validColors)
                    {
                        errors.Add(item.ToString());
                    }
                }
            }

            var ListVehicleByPropertyArguments = new Garage<Vehicle>.ListVehicleByPropertyArgs()
            {
                LicenseNumber = propertySearchUserInputValues[PropName.LicenseNumber],
                NumberOfWheels = Util.StringToNullableInt(propertySearchUserInputValues[PropName.NumberOfWheels]),
                Color = userInputColorEvaluated,

                NumberOfAirPlaneWings = Util.StringToNullableInt(propertySearchUserInputValues[PropName.NumberOfAirPlaneWings]),
                MotorCycleTopSpeed = Util.StringToNullableInt(propertySearchUserInputValues[PropName.MotorCycleTopSpeed]),
                NumberOfCarSeats = Util.StringToNullableInt(propertySearchUserInputValues[PropName.NumberOfCarSeats]),
                BusMaxNumberOfStandingPassengers = Util.StringToNullableInt(propertySearchUserInputValues[PropName.BusMaxNumberOfStandingPassengers]),
                NumberOfBoatSails = Util.StringToNullableInt(propertySearchUserInputValues[PropName.NumberOfBoatSails])
            };
            return ListVehicleByPropertyArguments;
        }

        private static bool TryParseColor(string colorValue, out Color color)
        {
            color = Color.Black;
            if (ColorHelper.TryParseColor(colorValue, out Color? tmpColor))
            {
                color = tmpColor.Value;
                return true;
            }
            return false;
        }

        private enum PropName
        {
            LicenseNumber,
            NumberOfWheels,
            Color,

            NumberOfAirPlaneWings,
            MotorCycleTopSpeed,
            NumberOfCarSeats,
            BusMaxNumberOfStandingPassengers,
            NumberOfBoatSails
        }

        private class PropSearch
        {
            public PropSearch(string property)
            {
                Property = property;
            }

            public string Property { get; set; }
            public string UserInput { get; set; } = null;
        }

    }
}
