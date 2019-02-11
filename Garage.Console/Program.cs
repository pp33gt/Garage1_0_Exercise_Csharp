using Garage.BLL;
using Garage.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Garage.BLL.DomainEntities;
using Garage.BLL.ConsoleUI;

namespace Garage.Console
{
    class Program
    {
        private static GarageHandler GarageHandler { get; set; }
        private static ConsoleInteraction Console { get; set; }
        private static UIConsoleHandler UIHandler { get; set;}

        static void Main(string[] args)
        {
            Console = new ConsoleInteraction();
            GarageHandler = new GarageHandler(/*Console*/);

            UIHandler = new UIConsoleHandler(Console, GarageHandler);
            UIHandler.Start();
        }
    }
}
