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
        private IConsoleInteraction Console { get; }

        private GarageHandler GarageHandler { get; }

        private bool GarageExists => GarageHandler.GarageExists;
        private bool GarageIsFull => GarageExists && GarageHandler.Garage.GarageIsFull;
        private int NumGarageCapacity => GarageExists ? GarageHandler.Garage.MaxNumberOfGarageVehicles : 0;

        private MenuItems MenuItems { get; set; }

        public bool Exit { get; private set; } = false;

        public UIConsoleHandler(IConsoleInteraction console, GarageHandler garageHandler)
        {
            Console = console;
            GarageHandler = garageHandler;
            InitMenuItems();
        }

        private void InitMenuItems()
        {
            Dictionary<ConsoleKey, Action> actionsDic = new Dictionary<ConsoleKey, Action>()
            {
                {MenuItems.Quit, UserQuit },
                {MenuItems.ShowMenu, UserShowMenu },
                {MenuItems.CreateGarage, UserCreateGarage },
                {MenuItems.ListVehicles, UserListVehicles },
                {MenuItems.ListTypesOfVehicles, UserListTypesOfVehicles },
                {MenuItems.GetRandomVehicle, UserGetRandomVehicle },
                {MenuItems.ParkVehicle, () => { } },
                {MenuItems.FindVehicleByLicense, UserFindVehicleByLicense },
                {MenuItems.UnparkVehicle, () => { } },
                {MenuItems.FindVehicleByProperties, UserFindVehicleByProperties },
                {MenuItems.SaveGarage, SaveGarage },
                {MenuItems.LoadGarage, LoadGarage }
            };

            MenuItems = new MenuItems(actionsDic);
        }

        public void Start()
        {
            UserShowMenu();
            UserHandleUserMenuInput();
        }

        public void UserHandleUserMenuInput()
        {
            while (!Exit)
            {
                var key = Console.ReadKey();
                if (!MenuItems.TryHandleMenuItem(key, GarageExists, GarageIsFull))
                {
                    UserShowMenu();
                }
            }
        }

        internal List<string> GetRows()
        {
            var result = new List<string>() { "Menu:" };

            var activeMenuItems = GetActiveMenuItems(GarageExists, GarageIsFull);
            
            bool doGarageHeader = GarageExists;

            string indent = string.Empty;
            foreach (MenuItem item in activeMenuItems)
            {
                if (item.ConsoleKey == MenuItems.UnparkVehicle || item.ConsoleKey == MenuItems.ParkVehicle) indent = "\t";

                if (doGarageHeader)
                {
                    var showFull = GarageIsFull ? " IS FULL" : string.Empty;

                    result.Add($"********************************************************************");
                    result.Add($"* GARAGE (Capacity: {NumGarageCapacity} )" + showFull);
                    result.Add($"********************************************************************");
                    doGarageHeader = false;
                }

                result.Add(indent + item.Text);
                indent = string.Empty;
            }

            return result;
        }

        private List<MenuItem> GetActiveMenuItems(bool garageExists, bool garageIsFull)
        {
            return MenuItems.GetActiveMenuItems(garageExists, garageIsFull);
        }

        private void UserQuit()
        {
            Exit = true;
        }

        private void UserShowMenu()
        {
            var rows = GetRows();

            foreach (var row in rows)
            {
                Console.WriteLine(row);
            }
        }

        private void UserCreateGarage()
        {
            Console.WriteLine("\nPlease enter max number of vehicles in Garage:");
            var userResponse = Console.ReadLine();
            if (int.TryParse(userResponse, out int numberOfVehicles))
            {
                if (numberOfVehicles > -1)
                {
                    GarageHandler.CreateGarage(numberOfVehicles);
                    UserShowMenu();
                    return;
                }
            }
            Console.WriteLine("Invalid input, please enter a non negative number");
        }

        internal void UserListVehicles()
        {
            UserListVehicles(GarageHandler.ListVehicles());
        }

        internal void UserListVehicles(List<Vehicle> vehicles)
        {
            Console.WriteLine("\nGarage Vehicles:");
            if (vehicles.Count > 0)
            {
                vehicles.ForEach(a => Console.WriteLine(a.ToString()));
            }
            else
            {
                Console.WriteLine("*** EMPTY ***");
            }
            Console.WriteLine(string.Empty);
        }

        internal void UserListTypesOfVehicles()
        {
            Console.WriteLine("\nGarage Vehicle Types:");
            var vehicles = GarageHandler.ListTypesOfVehicles();

            if (vehicles.Count < 1)
            {
                Console.WriteLine("*** EMPTY ***");
                return;
            }

            var keys = new List<string>(vehicles.Keys);

            keys.ForEach(a =>
            {
                var result = $"Typ:" + a.PadRight(15);
                result += "Count:" + vehicles[a];
                Console.WriteLine(result);
            });
        }

        private void UserGetRandomVehicle()
        {
            var randomVehicle = GarageHandler.GetRandomVehicle();
            Console.WriteLine("\nRANDOM VEHICLE: " + randomVehicle.ToString().Trim());
            UserParkVehicle(randomVehicle);
        }

        private void UserParkVehicle(Vehicle randomVehicle)
        {
            if (randomVehicle == null)
            {
                Console.WriteLine("Invalid Vehicle, was NOT Parked");
                return;
            }

            var parkMenuItemKey = MenuItems.ParkVehicle;
            var parkMenuItem = MenuItems.GetMenuText(parkMenuItemKey, GarageExists, GarageIsFull);
            if (parkMenuItem.Length < 1)
            {
                return;
            }

            Console.WriteLine("Press " + parkMenuItem);
            var userResponse = Console.ReadKey();
            if (userResponse.Key == parkMenuItemKey)
            {
                if (GarageHandler.TryParkVehicle(randomVehicle))
                {
                    Console.WriteLine("VEHICLE PARKED");
                    return;
                }
            }

            Console.WriteLine("VEHICLE NOT PARKED");
        }

        private void UserFindVehicleByLicense()
        {
            Console.WriteLine("\nFind Vehicle, Please type LicenseNumber:");
            var licenseNumber = Console.ReadLine();
            var vehicle = GarageHandler.FindVehicleByLicense(licenseNumber);
            if (vehicle != null)
            {
                Console.WriteLine($"FOUND VEHICLE IN GARAGE {vehicle.ToString()}");
                UserUnparkVehicle(vehicle);
                return;
            }
            Console.WriteLine($"Vehicle with License: {licenseNumber} Not in Garage");
        }

        private void UserUnparkVehicle(Vehicle vehicle)
        {
            Console.WriteLine("Press " + MenuItems.GetMenuText(MenuItems.UnparkVehicle, GarageExists, GarageIsFull));
            var userResponse = Console.ReadKey();
            if (userResponse.Key == ConsoleKey.U)
            {
                if (GarageHandler.TryUnparkVehicle(vehicle))
                {
                    Console.WriteLine("VEHICLE UNPARKED");
                    return;
                }
                else
                {
                    Console.WriteLine("ERROR: VEHICLE NOT UNPARKED");
                }
            }
        }

        internal void SaveGarage()
        {
            Console.WriteLine("\nSave Garage");
            try
            {
                GarageHandler.SaveGarage();
                Console.WriteLine("*** Saved ***");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        internal void LoadGarage()
        {
            Console.WriteLine("\nLoad Garage:");
            try
            {
                GarageHandler.LoadGarage();
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("*** Kunde inte hitta filen ***");
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }

            Console.WriteLine("*** Loaded ***");
            UserShowMenu();
        }
    }
}
