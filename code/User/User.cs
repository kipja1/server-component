using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

using NLog;

namespace User
{
    /// <summary>
    /// USer client class
    /// </summary>
    class User
    {

        Logger log = LogManager.GetCurrentClassLogger();
        bool isBattery = false;
        private static int[] sizes = { 200, 500, 800, 1000 };
        List<int> possiblBatterySize = new List<int>(sizes);
        int BatterySize = 0;
        int EnergyInBattery = 0;

        /// <summary>
        /// Console log formate
        /// </summary>
        private void ConfigureLogging()
        {
            var config = new NLog.Config.LoggingConfiguration();

            var console =
                new NLog.Targets.ConsoleTarget("console")
                {
                    Layout = @"${date:format=HH\:mm\:ss}|${level}| ${message} ${exception}"
                };
            config.AddTarget(console);
            config.AddRuleForAllLevels(console);

            LogManager.Configuration = config;
        }

        /// <summary>
        /// Class main method
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            User self = new User();
            self.run();
        }

        /// <summary>
        /// Class execution method
        /// </summary>
        private void run()
        {
            ConfigureLogging();
            var service = new UserService(@"http://localhost:5000/service/");

            //variable for calculation
            Random rand = new Random();
            try
            {

                int userID = service.getUserID();
                log.Info($"User ID {userID}");

                generateBattery(rand);

                while (true)
                {
                    if (isBattery)
                    {
                        checkForShortage(service, userID);
                    }
                    generate(service, rand, userID);
                    
                }
            }
            catch (Exception e)
            {
                //log whatever exception to console
                log.Warn(e, "Unhandled exception caught. Will restart main loop.");

                //prevent console spamming
                Thread.Sleep(1000);
            }
        }

        /// <summary>
        /// Generating users usage of electricity
        /// </summary>
        /// <param name="service">Web service object</param>
        /// <param name="rand">random generator object</param>
        private void generate(UserService service, Random rand, int userID)
        {
            int randomNum = rand.Next(100, 300);
            if (isBattery)
            {
                if (EnergyInBattery < BatterySize)
                {
                    double coeficient = rand.NextDouble() * (0.99 - 0.8) + 0.8;
                    int generated = (int)(BatterySize * 0.1 * coeficient);
                    EnergyInBattery += generated;
                    if (EnergyInBattery > BatterySize)
                    {
                        EnergyInBattery = BatterySize;
                    }
                    log.Info($"User used {randomNum}. There is {EnergyInBattery} of energy stored in the battery.");
                }
                else
                {
                    log.Info($"User used {randomNum}. There is {EnergyInBattery} of energy stored in the battery.");
                }
            }
            else
            {
                log.Info($"User used {randomNum}");
            }
            service.changeElectricityUsage(randomNum,userID);
            //sleep for a while
            Thread.Sleep(1000);
        }

        /// <summary>
        /// Generating information about battery
        /// </summary>
        /// <param name="rand">random generator object</param>
        private void generateBattery(Random rand)
        {
            isBattery = rand.Next(100) <= 80 ? true : false;
            if (isBattery)
            {
                int index = rand.Next(possiblBatterySize.Count);
                BatterySize = possiblBatterySize[index];
                log.Info($"User has battery of the size {BatterySize}");
            }
            else
            {
                log.Info($"User does not have battery");
            }
            
        }

        /// <summary>
        /// Checks is there a shortage in electrical grid
        /// </summary>
        /// <param name="service">Web service object</param>
        /// <param name="ID">user ID</param>
        private void checkForShortage(UserService service, int ID)
        {
            bool isShortage = service.getShortage();
            if (isShortage)
            {
                service.sendElectricity(EnergyInBattery, ID);
                EnergyInBattery = 0;
            }
        }

    }
}
