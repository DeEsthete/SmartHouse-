using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHouseLibrary;
using SmartHousLibrary;
using Sensors;

namespace SmartHouse
{
    class Program
    {
        static void Main(string[] args)
        {
            //Main per

            List<Device> obj = new List<Device>();
            List<Scenario> scenario = new List<Scenario>();
            User user = new User();
            bool isRegister = false;//это типа файл
            Secutiry security = new Secutiry();
            string login, password;
            //считываем с файла юзер
            //и бул переменная

            //
            //как то считать с файла инфу
            if (isRegister)
            {
                bool isRegisterSucces = false;
                while (!isRegisterSucces)
                {
                    Console.WriteLine("Введите логин");
                    login = Console.ReadLine();
                    Console.WriteLine("Введите пароль");
                    password = Console.ReadLine();
                    if (security.CheckLogin(login) && security.CheckPassword(password))
                    {
                        isRegisterSucces = true;
                    }
                    else
                    {
                        Console.WriteLine("Вы ввели неправильный логин или пароль. Повторите попытку.");
                        Console.ReadLine();
                    }
                }

            }
            else
            {
                Console.WriteLine("Пожалуйста пройдите регистрацию");
                Console.ReadLine();
                Console.WriteLine("Введите логин");
                user.Login = Console.ReadLine();
                Console.WriteLine("Введите пароль");
                user.Password = Console.ReadLine();
                user.CreationDate = DateTime.Now;
                //Записываем юзера в файл
            }


            #region Menu
            string[] stringsMainMenu = { "Добавить устройство", "Добавить сценарий", "Просмотреть сценарии", "Удалить сценарий", "Просмотреть устройства", "Выход" };

            ConsoleMenu mainMenu = new ConsoleMenu(stringsMainMenu);
            int mainMenuResult;
            do
            {
                mainMenuResult = mainMenu.PrintMenu();
                mainMenuResult++;


                switch (mainMenuResult)
                {




                    case 1:
                        string[] stringsDeviceMenu = { "Датчик", "Устройство", "Назад" };
                        ConsoleMenu deviceMenu = new ConsoleMenu(stringsDeviceMenu);
                        int deviceMenuResult;
                        do
                        {
                            deviceMenuResult = deviceMenu.PrintMenu();
                            deviceMenuResult++;


                            switch (deviceMenuResult)
                            {
                                case 1:
                                    string[] stringsSensorsMenu = { "Добавить датчик движения", "Добавить датчик температуры", "Добавить датчик открытия", "Добавить датчик для контроля электроэнергией", "Назад" };
                                    ConsoleMenu sensorMenu = new ConsoleMenu(stringsSensorsMenu);
                                    int sensorMenuResult;
                                    do
                                    {
                                        sensorMenuResult = sensorMenu.PrintMenu();
                                        sensorMenuResult++;


                                        switch (sensorMenuResult)
                                        {
                                            case 1:
                                                MoovingSensor moovingSensorTemp = new MoovingSensor();
                                                obj.Add(moovingSensorTemp);
                                                Console.WriteLine("Датчик движения добавлен");
                                                Console.ReadLine();
                                                break;

                                            case 2:
                                                TemperatureSensor temperatureSensorTemp = new TemperatureSensor();
                                                obj.Add(temperatureSensorTemp);
                                                Console.WriteLine("Датчик температуры добавлен");
                                                Console.ReadLine();
                                                break;

                                            case 3:
                                                OpenSensor openSensorTemp = new OpenSensor();
                                                obj.Add(openSensorTemp);
                                                Console.WriteLine("Датчик открытия добавлен");
                                                Console.ReadLine();
                                                break;

                                            case 4:
                                                ElectricityPowerSensor electricityPowerSensorTemp;
                                                electricityPowerSensorTemp = new ElectricityPowerSensor();
                                                obj.Add(electricityPowerSensorTemp);
                                                Console.WriteLine("Датчик для контроля электроэнергией добавлен");
                                                Console.ReadLine();
                                                break;
                                        }


                                    }
                                    while (sensorMenuResult != stringsSensorsMenu.Length);
                                    break;


                                case 2:
                                    Device deviceTemp = new Device();
                                    Console.WriteLine("Введите имя устройства");
                                    deviceTemp.Name = Console.ReadLine();
                                    deviceTemp.IsOn = false;
                                    obj.Add(deviceTemp);
                                    Console.WriteLine("Устройство добавлено");
                                    Console.ReadLine();
                                    break;
                            }


                            //Console.ReadKey();
                        } while (deviceMenuResult != stringsDeviceMenu.Length);
                        break;



                    case 2:
                        //string[] stringsScenarioMenu = { "Выбрать устройство", "Назначить время", "Что сделать с уcтройством", "Назад" };
                        //ConsoleMenu scenarioMenu = new ConsoleMenu(stringsScenarioMenu);
                        //int scenarioMenuResult;
                        //do
                        //{
                        //    scenarioMenuResult = scenarioMenu.PrintMenu();
                        //    scenarioMenuResult++;
                        Scenario scenarioTemp = new Scenario();
                        Console.WriteLine("Введите имя сценария");
                        scenarioTemp.Name = Console.ReadLine();
                        string[] stringsDeviceeMenu = new string[obj.Count + 1];
                        for (int i = 0; i < obj.Count; i++)
                        {
                            stringsDeviceeMenu[i] = obj[i].Name;
                        }

                        stringsDeviceeMenu[obj.Count] = "Назад";

                        ConsoleMenu deviceeMenu = new ConsoleMenu(stringsDeviceeMenu);
                        int deviceeMenuResult;
                        do
                        {
                            deviceeMenuResult = deviceeMenu.PrintMenu();
                            deviceeMenuResult++;
                            if (deviceeMenuResult != obj.Count+1)
                            {
                                scenarioTemp.device = obj[deviceeMenuResult - 1];
                            }

                        } while (deviceeMenuResult != stringsDeviceeMenu.Length);

                        string[] stringsOnOffMenu = { "Включить", "Выключить", "Назад" };
                        ConsoleMenu onOffMenu = new ConsoleMenu(stringsOnOffMenu);
                        int onOffMenuResult;
                        do
                        {
                            onOffMenuResult = onOffMenu.PrintMenu();
                            onOffMenuResult++;



                            switch (onOffMenuResult)
                            {
                                case 1:
                                    //занести в сценарий что устройство включается
                                    scenarioTemp.IsOn = true;
                                    Console.WriteLine("Устройство будет включено");
                                    Console.ReadLine();
                                    break;

                                case 2:
                                    //занести в сценарий что устройство выключается
                                    scenarioTemp.IsOn = false;
                                    Console.WriteLine("Устройство будет выключено");
                                    Console.ReadLine();
                                    break;

                            }



                        } while (onOffMenuResult != stringsOnOffMenu.Length);
                        /*} while (scenarioMenuResult != stringsScenarioMenu.Length);*/
                        Console.WriteLine("Введите время; Minute, Hour, Day of week");
                        int minute;
                        int hour;
                        int dayOfWeek;
                        Console.Write("Minute - ");
                        int.TryParse(Console.ReadLine(), out minute);
                        Console.Write("Hour - ");
                        int.TryParse(Console.ReadLine(), out hour);
                            Console.Write("Day of week -  (0 - 6)");
                            int.TryParse(Console.ReadLine(), out dayOfWeek);
                            DateTime dateTime=new DateTime(2000,11,11,hour,minute,0);
                        scenarioTemp.DayOfWeek = (DayOfWeek)Enum.Parse(typeof(DayOfWeek),dayOfWeek.ToString());
                        scenarioTemp.time = dateTime;
                        scenario.Add(scenarioTemp);
                        break;

                    case 3:
                        //выводятся все сценарии 
                        //
                        //
                        //
                        break;
                        
                    case 4:
                        //создается массив строк на количество сценариев и один сценарий будет 1 строкой и делается менюшка нужна проверка на наличие сценариев
                        //
                        //
                        //
                        break;

                


                    case 5:
                        //Вывод всех устройств 
                        //
                        //
                        //
                        //
                        break;
                        //Console.ReadKey();
                }
            } while (mainMenuResult != stringsMainMenu.Length);
            #endregion

        }
    }
}
