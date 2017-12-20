using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHouseLibrary;
using SmartHousLibrary;
using Sensors;
using System.IO.Ports;

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
            //int lostPassword = 0;
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
                        
                    }
                }

            }
            else
            {
                bool isRegisterSucces = false;
                while (!isRegisterSucces)
                {
                    Console.WriteLine("Пожалуйста пройдите регистрацию");
                    Console.WriteLine("Введите логин");
                    login = Console.ReadLine();
                    Console.WriteLine("Введите пароль");
                    password = Console.ReadLine();
                    if (login == string.Empty || login == " " || password == string.Empty || password == " ")
                    {
                        Console.WriteLine("Вы ввели неккоректный логин или пароль");
                        Console.ReadLine();
                    }
                    else
                    {
                        isRegisterSucces = true;
                    }
                }
               
                user.CreationDate = DateTime.Now;
                Console.WriteLine("Вы успешно зарегистрировались");
                Console.ReadLine();
                //Записываем юзера в файл
            }


            #region Menu
            string[] stringsMainMenu = { "Добавить устройство", "Добавить сценарий", "Просмотреть сценарии", "Удалить сценарий", "Просмотреть устройства", "Удалить устройство","Включить/Выключить устройство", "Выход" };

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
                                                moovingSensorTemp.Name = string.Format("Датчик движения - {0}",moovingSensorTemp.Id);
                                                obj.Add(moovingSensorTemp);
                                                Console.WriteLine("{0} добавлен",moovingSensorTemp.Name);
                                                Console.ReadLine();
                                                break;

                                            case 2:
                                                TemperatureSensor temperatureSensorTemp = new TemperatureSensor();
                                                temperatureSensorTemp.Name = string.Format("Датчик температуры - {0}",temperatureSensorTemp.Id);
                                                obj.Add(temperatureSensorTemp);
                                                Console.WriteLine("{0} добавлен",temperatureSensorTemp.Name);
                                                Console.ReadLine();
                                                break;

                                            case 3:
                                                OpenSensor openSensorTemp = new OpenSensor();
                                                openSensorTemp.Name = string.Format("Датчик открытия - {0}",openSensorTemp.Id);
                                                obj.Add(openSensorTemp);
                                                Console.WriteLine("{0} добавлен",openSensorTemp.Name);
                                                Console.ReadLine();
                                                break;

                                            case 4:
                                                ElectricityPowerSensor electricityPowerSensorTemp;
                                                electricityPowerSensorTemp = new ElectricityPowerSensor();
                                                electricityPowerSensorTemp.Name = string.Format("Датчик электроэнергии - {0}",electricityPowerSensorTemp.Id);
                                                obj.Add(electricityPowerSensorTemp);
                                                Console.WriteLine("{0} добавлен",electricityPowerSensorTemp.Name);
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
                        if (obj.Count == 0)
                        {
                            Console.WriteLine("Устройств нет");
                            Console.ReadLine();
                            break;
                        }
                        Scenario scenarioTemp = new Scenario();
                        Console.WriteLine("Введите имя сценария");
                        scenarioTemp.Name = Console.ReadLine();
                        Console.WriteLine("Выберите устройство");
                        Console.ReadLine();
                        string[] stringsDeviceeMenu = new string[obj.Count + 1];
                        for (int i = 0; i < obj.Count; i++)
                        {
                            stringsDeviceeMenu[i] = obj[i].Name;
                        }

                        stringsDeviceeMenu[obj.Count] = " ";

                        ConsoleMenu deviceeMenu = new ConsoleMenu(stringsDeviceeMenu);
                        int deviceeMenuResult;
                        do
                        {
                            deviceeMenuResult = deviceeMenu.PrintMenu();
                            deviceeMenuResult++;
                            if (deviceeMenuResult != obj.Count + 1)
                            {
                                scenarioTemp.device = obj[deviceeMenuResult - 1];

                            }
                            break;
                        } while (deviceeMenuResult != stringsDeviceeMenu.Length);

                        string[] stringsOnOffMenu = { "Включить", "Выключить", "  " };
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

                            break;

                        } while (onOffMenuResult != stringsOnOffMenu.Length);
                        /*} while (scenarioMenuResult != stringsScenarioMenu.Length);*/
                        Console.WriteLine("Введите время; Minute, Hour, Day of week");
                        int minute=0;
                        int hour=0;
                        int dayOfWeek=0;
                        bool succes = false;
                        while (!succes)
                        {
                            Console.Write("Hour - ");
                            succes = int.TryParse(Console.ReadLine(), out hour);
                            Console.Write("Minute - ");
                            succes = int.TryParse(Console.ReadLine(), out minute);
                            //Console.Write("Day of week -  (0 - 6)");
                            //succes = int.TryParse(Console.ReadLine(), out dayOfWeek);
                            if (!succes)
                            {
                                Console.WriteLine("Вы ввели неверное время ");
                                Console.WriteLine();
                            }
                        }
                        DateTime dateTime = new DateTime(2000, 11, 11, hour, minute, 0);
                        //scenarioTemp.DayOfWeek = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), dayOfWeek.ToString());
                        scenarioTemp.time = dateTime;
                        scenario.Add(scenarioTemp);
                        Console.WriteLine("Сценарий добавлен");
                        Console.ReadLine();
                        break;

                    case 3:
                        //выводятся все сценарии 
                        //
                        //
                        //
                        if (scenario.Count == 0)
                        {
                            Console.WriteLine("Сценариев нет");
                            Console.ReadLine();
                            break;
                        }
                        for(int i = 0; i < scenario.Count; i++)
                        {
                            Console.WriteLine("Имя сценария - {0}",scenario[i].Name);
                            Console.WriteLine("Имя устройства - {0}",scenario[i].device.Name);
                            if (scenario[i].IsOn)
                            {
                                Console.WriteLine("Устройство включается в {0}",scenario[i].time.ToShortTimeString());
                                //Console.WriteLine("Дни включения - {0}",scenario[i].DayOfWeek);
                            }
                            else
                            {
                                Console.WriteLine("Устройство выключается в {0}", scenario[i].time.ToShortTimeString());
                            }
                            Console.WriteLine("**************************************");
                        }
                        Console.ReadLine();
                        break;

                    case 4:
                        //создается массив строк на количество сценариев и один сценарий будет 1 строкой и делается менюшка нужна проверка на наличие сценариев
                        //
                        //
                        //
                        if (scenario.Count == 0)
                        {
                            Console.WriteLine("Сценариев нет");
                            Console.ReadLine();
                            break;
                        }

                        string[] stringsDeleteMenu = new string[scenario.Count+1];
                        for(int i = 0; i < scenario.Count; i++)
                        {
                            stringsDeleteMenu[i] = scenario[i].Name;
                        }
                        stringsDeleteMenu[scenario.Count] = "Назад";
                        ConsoleMenu deleteMenu = new ConsoleMenu(stringsDeleteMenu);
                        int deleteMenuResult;
                        do
                        {
                            deleteMenuResult = deleteMenu.PrintMenu();
                            deleteMenuResult++;
                            if (deleteMenuResult !=scenario.Count+1)
                            {
                                scenario.RemoveAt(deleteMenuResult - 1);
                                Console.WriteLine("Сценарий Удален");
                                Console.ReadLine();
                                
                            }
                            break;
                        } while (deleteMenuResult != stringsDeleteMenu.Length);
                            break;




                    case 5:
                        //Вывод всех устройств 
                        //
                        //
                        //
                        //
                        if (obj.Count == 0)
                        {
                            Console.WriteLine("Устройств нет");
                            Console.ReadLine();
                            break;
                        }
                        for (int i = 0; i < obj.Count; i++)
                        {
                            Console.WriteLine("Имя устройства - {0}", obj[i].Name);
                            if (obj[i].IsOn)
                            {
                                Console.WriteLine("Устройство включаено");
                            }
                            else
                            {
                                Console.WriteLine("Устройство выключено");
                            }
                            Console.WriteLine("**************************************");
                        }
                        Console.ReadLine();
                        break;

                    case 6:
                        if (obj.Count == 0)
                        {
                            Console.WriteLine("Устройств нет");
                            Console.ReadLine();
                            break;
                        }

                        string[] stringsDeleteoMenu = new string[obj.Count + 1];
                        for (int i = 0; i < obj.Count; i++)
                        {
                            stringsDeleteoMenu[i] = obj[i].Name;
                        }
                        stringsDeleteoMenu[obj.Count] = "Назад";
                        ConsoleMenu deleteoMenu = new ConsoleMenu(stringsDeleteoMenu);
                        int deleteoMenuResult;
                        do
                        {
                            deleteoMenuResult = deleteoMenu.PrintMenu();
                            deleteoMenuResult++;
                            if (deleteoMenuResult != obj.Count + 1)
                            {
                                obj.RemoveAt(deleteoMenuResult - 1);
                                Console.WriteLine("Устройство Удалено");
                                Console.ReadLine();

                            }
                            break;
                        } while (deleteoMenuResult != stringsDeleteoMenu.Length);
                        break;

                    case 7:
                        if (obj.Count == 0)
                        {
                            Console.WriteLine("Устройств нет");
                            Console.ReadLine();
                            break;
                        }

                        string[] stringsOnMenu = new string[obj.Count + 1];
                        for (int i = 0; i < obj.Count; i++)
                        {
                            stringsOnMenu[i] = obj[i].Name;
                        }
                        stringsOnMenu[obj.Count] = "Назад";
                        ConsoleMenu onMenu = new ConsoleMenu(stringsOnMenu);
                        int onMenuResult;
                        do
                        {
                           onMenuResult = onMenu.PrintMenu();
                            onMenuResult++;
                            if (onMenuResult != obj.Count + 1)
                            {
                                Logic logic = new Logic();
                                if (obj[onMenuResult - 1].IsOn == true)
                                {
                                    obj[onMenuResult - 1].TurnOff();
                                    logic.Ardu(obj[onMenuResult - 1].IsOn);
                                }
                                else
                                {
                                    obj[onMenuResult - 1].TurnOn();
                                    logic.Ardu(obj[onMenuResult - 1].IsOn);
                                }

                            }
                            break;
                        } while (onMenuResult != stringsOnMenu.Length);

                        break;
                }
            } while (mainMenuResult != stringsMainMenu.Length);
            #endregion

        }
    }
}
