using System;
using System.Collections.Generic;
using System.Linq;

namespace Garage.BLL.ConsoleUI
{
    internal class MenuItems
    {
        internal const ConsoleKey Quit = ConsoleKey.Q;
        internal const ConsoleKey ShowMenu = ConsoleKey.OemPlus;
        internal const ConsoleKey CreateGarage = ConsoleKey.D1;
        internal const ConsoleKey ListVehicles = ConsoleKey.D2;
        internal const ConsoleKey ListTypesOfVehicles = ConsoleKey.D3;
        internal const ConsoleKey GetRandomVehicle = ConsoleKey.D4;
        internal const ConsoleKey ParkVehicle = ConsoleKey.P;
        internal const ConsoleKey FindVehicleByLicense = ConsoleKey.D5;
        internal const ConsoleKey FindVehicleByProperties = ConsoleKey.D6;
        internal const ConsoleKey UnparkVehicle = ConsoleKey.U;
        internal const ConsoleKey SaveGarage = ConsoleKey.S;
        internal const ConsoleKey LoadGarage = ConsoleKey.L;

        internal MenuItems(Dictionary<ConsoleKey, Action> actionsDic)
        {
            InitActions(actionsDic);
        }

        internal string GetMenuText(ConsoleKey key, bool garageExist, bool garageIsFull)
        {
            var item = GetMenuItem(key, garageExist, garageIsFull);
            if (item != null) return item.Text;

            return string.Empty;
        }

        internal List<MenuItem> GetActiveMenuItems(bool garageExists, bool garageIsFull)
        {
            var res = BasicMenu;

            if (garageExists)
            {
                res = GetMainMenu(garageIsFull);
            }
            return res;
        }

        internal bool TryHandleMenuItem(ConsoleKeyInfo keyInfo, bool garageExist, bool garageIsFull)
        {
            var key = keyInfo.Key;

            var menuItem = GetMenuItem(key, garageExist, garageIsFull);

            if (menuItem != null)
            {
                menuItem.Action();
                return true;
            }
            return false;
        }

        private void InitActions(Dictionary<ConsoleKey, Action> actionsDic)
        {
            foreach(var item in AllUniqMenuItems)
            {
                if(actionsDic.ContainsKey(item.ConsoleKey))
                {
                    item.Action = actionsDic[item.ConsoleKey];
                }
            }
        }

        private List<MenuItem> allUniqMenuItems = null;
        private List<MenuItem> AllUniqMenuItems {
            get
            {
                if(allUniqMenuItems == null)
                {
                    allUniqMenuItems = new List<MenuItem>() {
                    new MenuItem("<Q>: Quit", null, Quit),
                    new MenuItem("<?>: Menu", null, ShowMenu),
                    new MenuItem("<1>: Create Garage", null, CreateGarage),
                    new MenuItem("<2>: Garage: List Vehicles", null, ListVehicles),
                    new MenuItem("<3>: Garage: List Types of Vehicles", null, ListTypesOfVehicles),
                    new MenuItem("<4>: Random: Get Vehicle", null, GetRandomVehicle),
                    new MenuItem("<P>: Garage: Park Vehicle", null, ParkVehicle),
                    new MenuItem("<5>: Garage: Find Vehicle by License", null, FindVehicleByLicense),
                    new MenuItem("<U>: Unpark Vehicle in Garage", null, UnparkVehicle),
                    new MenuItem("<6>: Garage: Find Vehicles in by Properties", null, FindVehicleByProperties),
                    new MenuItem("<S>: Save Vehicles in Garage To File", null, SaveGarage),
                    new MenuItem("<L>: Load Vehicles to Garage from File", null, LoadGarage)
                    };
                }
                return allUniqMenuItems;
            }
        }

        private MenuItem GetMenuItem(ConsoleKey key, bool garageExist, bool garageIsFull)
        {
            var activeMenuItems = GetActiveMenuItems(garageExist, garageIsFull);
            foreach (var item in activeMenuItems)
            {
                if (item.ConsoleKey == key) return item;
            }
            return null;
        }

        private Action GetMenuFunction(ConsoleKey key, bool garageExist, bool garageIsFull)
        {
            var item = GetMenuItem(key, garageExist, garageIsFull);
            if (item != null) return item.Action;

            return () => { };
        }

        private List<MenuItem> GetCustomList(List<ConsoleKey> filter)
        {
            var res = filter.Select(a => AllUniqMenuItems.Find(b => b.ConsoleKey == a)).ToList();
            return res;
        }

        private List<MenuItem> BasicMenu
        {
            get
            {
                return GetCustomList(new List<ConsoleKey>() {
                    Quit,
                    ShowMenu,
                    CreateGarage,
                    LoadGarage
                });
            }
        }

        private List<MenuItem> GetMainMenu(bool garageIsFull)
        {
            var res = GetCustomList(new List<ConsoleKey>() {
                    Quit,
                    ShowMenu,
                    CreateGarage,
                    ListVehicles,
                    ListTypesOfVehicles,
                    GetRandomVehicle,
                    ParkVehicle,
                    FindVehicleByLicense,
                    UnparkVehicle,
                    FindVehicleByProperties,
                    UnparkVehicle,
                    SaveGarage,
                    LoadGarage
                });

            if (garageIsFull)
            {
                res.RemoveAll(a => a.ConsoleKey == ParkVehicle);
            }

            return res;
        }
    }
}
