using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHouseLibrary;
using SmartHousLibrary;
using Sensors;
using System.IO.Ports;
using System.IO;
namespace SmartHouse
{
    class Program
    {
        static void Main(string[] args)
        {
            //Main per

            #region Margins(поля)
            // List<Device> obj = new List<Device>();
            List<Room> rooms = new List<Room>();
            
            List<Scenario> scenario = new List<Scenario>();
        
            User user = new User();
            bool isRegister;//это типа файл
            Secutiry security = new Secutiry();
            string login="", password="";
            //int lostPassword = 0;
            //считываем с файла юзер
            //и бул переменная
            #endregion

            #region Registration
            string way = (Directory.GetCurrentDirectory()+@"\User.bin");
            if (!File.Exists(way))
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
                isRegister = true;
                user.CreationDate = DateTime.Now;
                Console.WriteLine("Вы успешно зарегистрировались");
                Console.ReadLine();
                using (BinaryWriter writer = new BinaryWriter(new FileStream(way, FileMode.OpenOrCreate)))
                {
                    writer.Write(login);
                    writer.Write(password);
                    writer.Write(isRegister);
                    writer.Write(user.CreationDate.ToString());
                }
            }
            else
            {
                using (BinaryReader reader = new BinaryReader(new FileStream(way, FileMode.OpenOrCreate)))
                {
                    login = reader.ReadString();
                    password = reader.ReadString();
                    isRegister = reader.ReadBoolean();
                    string time = reader.ReadString();
                    DateTime date = user.CreationDate;
                    DateTime.TryParse(time,  out date);
                    user.CreationDate = date;
                }
                bool isRegisterSucces = false;
                
                while (!isRegisterSucces)
                {
                    Console.WriteLine("Введите логин");
                    security.Login = Console.ReadLine();
                    Console.WriteLine("Введите пароль");
                    security.Password = Console.ReadLine();
                    if (security.CheckLogin(login) && security.CheckPassword(password))
                    {
                        string wayF = (Directory.GetCurrentDirectory() + @"\Scenario.bin");
                        using (BinaryReader reader = new BinaryReader(new FileStream(wayF, FileMode.OpenOrCreate)))
                        {
                            int countS = reader.ReadInt32();
                            if (countS == 0)
                            {

                            }
                            else
                            {
                                for (int i = 0; i < countS; i++)
                                {
                                    Scenario scenarioTemp = new Scenario();
                                    scenarioTemp.device = new Device();
                                    scenarioTemp.Id = System.Guid.Parse(reader.ReadString());
                                    scenarioTemp.Name = reader.ReadString();
                                    scenarioTemp.device.Id = System.Guid.Parse(reader.ReadString());
                                    scenarioTemp.device.Name = reader.ReadString();
                                    scenarioTemp.device.IsOn = reader.ReadBoolean();
                                    scenarioTemp.time = DateTime.Parse(reader.ReadString());
                                    scenario.Add(scenarioTemp);
                                  
                                }
                            }
                        }

                        string wayRoom = (Directory.GetCurrentDirectory() + @"\Rooms.bin");

                        using (BinaryReader reader = new BinaryReader(new FileStream(wayRoom, FileMode.OpenOrCreate)))
                        {
                            int countS = reader.ReadInt32();
                            if (countS == 0)
                            {

                            }
                            else
                            {
                                for (int i = 0; i < countS; i++)
                                {
                                    Room roomTemp = new Room();
                                    roomTemp.Device = new List<Device>();
                                    roomTemp.Id = System.Guid.Parse(reader.ReadString());
                                    roomTemp.Name = reader.ReadString();
                                    int countD = reader.ReadInt32();

                                    for(int j = 0; j < countD; j++)
                                    {
                                        Device deviceTemp = new Device();
                                        deviceTemp.Id = System.Guid.Parse(reader.ReadString());
                                        deviceTemp.Name= reader.ReadString();
                                        deviceTemp.IsOn = reader.ReadBoolean();
                                        roomTemp.Device.Add(deviceTemp);
                                    }

                                    rooms.Add(roomTemp);

                                }
                            }
                        }



                        isRegisterSucces = true;
                    }
                    else
                    {
                        Console.WriteLine("Вы ввели неправильный логин или пароль. Повторите попытку.");
                        Console.ReadLine();
                    }
                }
            }
            #endregion

            //
            //как то считать с файла инфу

            #region Menu
            string[] stringsMainMenu = { "Добавить комнату", "Добавить устройство", "Добавить сценарий", "Просмотреть сценарии", "Удалить сценарий", "Просмотреть устройства", "Удалить устройство", "Удалить комнату", "Включить/Выключить устройство", "Выход" };

            ConsoleMenu mainMenu = new ConsoleMenu(stringsMainMenu);
            int mainMenuResult;
            do
            {
                if (scenario.Count != 0)
                {
                    for(int i = 0; i < scenario.Count; i++)
                    {
                        if (scenario[i].time.Hour == DateTime.Now.Hour && scenario[i].time.Minute == DateTime.Now.Minute)
                        {
                            scenario[i].device.IsOn = scenario[i].IsOn;
                            Logic logic = new Logic();
                            logic.Ardu(scenario[i].device.IsOn);
                        }
                        
                    }
                }
                mainMenuResult = mainMenu.PrintMenu();
                mainMenuResult++;


                switch (mainMenuResult)
                {


                    case 1:
                        Room roomTemp = new Room();
                        Console.WriteLine("Введите имя комнаты");
                        roomTemp.Name = Console.ReadLine();
                        if (roomTemp.Name == string.Empty || roomTemp.Name.Contains(" "))
                        {
                            Console.WriteLine("Вы ввели некорекктное имя");
                            Console.ReadLine();
                            break;
                        }
                        rooms.Add(roomTemp);
                        Console.WriteLine("Комната {0} добавлена", roomTemp.Name);
                        break;

                    case 2:
                        if (rooms.Count == 0)
                        {
                            Console.WriteLine("Нету комнат");
                            Console.ReadLine();
                            break;
                        }
                        //------------------------------------------------------------------------------------------------------------------------------------
                        string[] stringsRoomMenu = new string[rooms.Count + 1];
                        for (int i = 0; i < rooms.Count; i++)
                        {
                            stringsRoomMenu[i] = rooms[i].Name;
                        }
                        stringsRoomMenu[rooms.Count] = "Назад";
                        ConsoleMenu roomMenu = new ConsoleMenu(stringsRoomMenu);
                        int roomMenuResult;
                        do
                        {
                            roomMenuResult = roomMenu.PrintMenu();
                            roomMenuResult++;
                            if (roomMenuResult != rooms.Count + 1)
                            {


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
                                                        moovingSensorTemp.Name = string.Format("Датчик движения - {0}", moovingSensorTemp.Id);
                                                        rooms[roomMenuResult - 1].Device.Add(moovingSensorTemp);
                                                        Console.WriteLine("{0} добавлен", moovingSensorTemp.Name);
                                                        Console.ReadLine();
                                                        break;

                                                    case 2:
                                                        TemperatureSensor temperatureSensorTemp = new TemperatureSensor();
                                                        temperatureSensorTemp.Name = string.Format("Датчик температуры - {0}", temperatureSensorTemp.Id);
                                                        rooms[roomMenuResult - 1].Device.Add(temperatureSensorTemp);
                                                        Console.WriteLine("{0} добавлен", temperatureSensorTemp.Name);
                                                        Console.ReadLine();
                                                        break;

                                                    case 3:
                                                        OpenSensor openSensorTemp = new OpenSensor();
                                                        openSensorTemp.Name = string.Format("Датчик открытия - {0}", openSensorTemp.Id);
                                                        rooms[roomMenuResult - 1].Device.Add(openSensorTemp);
                                                        Console.WriteLine("{0} добавлен", openSensorTemp.Name);
                                                        Console.ReadLine();
                                                        break;

                                                    case 4:
                                                        ElectricityPowerSensor electricityPowerSensorTemp;
                                                        electricityPowerSensorTemp = new ElectricityPowerSensor();
                                                        electricityPowerSensorTemp.Name = string.Format("Датчик электроэнергии - {0}", electricityPowerSensorTemp.Id);
                                                        rooms[roomMenuResult - 1].Device.Add(electricityPowerSensorTemp);
                                                        Console.WriteLine("{0} добавлен", electricityPowerSensorTemp.Name);
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
                                            if (deviceTemp.Name == string.Empty || deviceTemp.Name.Contains(" "))
                                            {
                                                Console.WriteLine("Вы ввели некорекктное имя");
                                                Console.ReadLine();
                                                break;
                                            }
                                            deviceTemp.IsOn = false;
                                            rooms[roomMenuResult - 1].Device.Add(deviceTemp);
                                            Console.WriteLine("Устройство добавлено");
                                            Console.ReadLine();
                                            break;
                                    }


                                    //Console.ReadKey();
                                } while (deviceMenuResult != stringsDeviceMenu.Length);
                                //------------------------------------------------------------------------------------------------------------------------
                            }
                            break;
                        } while (roomMenuResult != stringsRoomMenu.Length);

                        //---------------------------------------------------------------------------------------------------------------------------------------

                        break;



                    case 3:
                        //string[] stringsScenarioMenu = { "Выбрать устройство", "Назначить время", "Что сделать с уcтройством", "Назад" };
                        //ConsoleMenu scenarioMenu = new ConsoleMenu(stringsScenarioMenu);
                        //int scenarioMenuResult;
                        //do
                        //{
                        //    scenarioMenuResult = scenarioMenu.PrintMenu();
                        //    scenarioMenuResult++;
                        if (rooms.Count == 0)
                        {
                            Console.WriteLine("Комнат нет");
                            Console.ReadLine();
                            break;
                        }
                        else if (rooms[0].Device.Count == 0)
                        {
                            Console.WriteLine("Устройств нет");
                            Console.ReadLine();
                            break;
                        }
                        Scenario scenarioTemp = new Scenario();
                        Console.WriteLine("Введите имя сценария");
                        scenarioTemp.Name = Console.ReadLine();
                        if (scenarioTemp.Name == string.Empty || scenarioTemp.Name.Contains(" "))
                        {
                            Console.WriteLine("Вы ввели некорекктное имя");
                            Console.ReadLine();
                            break;
                        }
                        Console.WriteLine("Выберите комнату");
                        Console.ReadLine();


                        string[] stringsRoommMenu = new string[rooms.Count + 1];
                        for (int i = 0; i < rooms.Count; i++)
                        {
                            stringsRoommMenu[i] = rooms[i].Name;
                        }

                        stringsRoommMenu[rooms.Count] = " ";

                        ConsoleMenu rommMenu = new ConsoleMenu(stringsRoommMenu);
                        int rommMenuResult;
                        do
                        {
                            rommMenuResult = rommMenu.PrintMenu();
                            rommMenuResult++;
                            if (rommMenuResult != rooms.Count + 1)
                            {

                                Console.WriteLine("Выберите устройство");
                                Console.ReadLine();
                                string[] stringsDeviceeMenu = new string[rooms[rommMenuResult - 1].Device.Count + 1];
                                for (int i = 0; i < rooms[rommMenuResult - 1].Device.Count; i++)
                                {
                                    stringsDeviceeMenu[i] = rooms[rommMenuResult - 1].Device[i].Name;
                                }

                                stringsDeviceeMenu[rooms[rommMenuResult - 1].Device.Count] = " ";

                                ConsoleMenu deviceeMenu = new ConsoleMenu(stringsDeviceeMenu);
                                int deviceeMenuResult;
                                do
                                {
                                    deviceeMenuResult = deviceeMenu.PrintMenu();
                                    deviceeMenuResult++;
                                    if (deviceeMenuResult != rooms[rommMenuResult - 1].Device.Count + 1)
                                    {
                                        scenarioTemp.device = rooms[rommMenuResult - 1].Device[deviceeMenuResult - 1];

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
                                int minute = 0;
                                int hour = 0;
                                // int dayOfWeek=0;
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


                            }
                            break;
                        } while (rommMenuResult != stringsRoommMenu.Length);
                        //-------------------------------------------------------------------------------------------------------------------------------

                        break;

                    case 4:
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
                        for (int i = 0; i < scenario.Count; i++)
                        {
                            Console.WriteLine("Имя сценария - {0}", scenario[i].Name);
                            Console.WriteLine("Имя устройства - {0}", scenario[i].device.Name);
                            if (scenario[i].IsOn)
                            {
                                Console.WriteLine("Устройство включается в {0}", scenario[i].time.ToShortTimeString());
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

                    case 5:
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

                        string[] stringsDeleteMenu = new string[scenario.Count + 1];
                        for (int i = 0; i < scenario.Count; i++)
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
                            if (deleteMenuResult != scenario.Count + 1)
                            {
                                scenario.RemoveAt(deleteMenuResult - 1);
                                Console.WriteLine("Сценарий Удален");
                                Console.ReadLine();

                            }
                            break;
                        } while (deleteMenuResult != stringsDeleteMenu.Length);
                        break;




                    case 6:
                        //Вывод всех устройств 
                        //
                        //
                        //
                        //
                        if (rooms.Count == 0)
                        {
                            Console.WriteLine("Комнат нет");
                            Console.ReadLine();
                            break;
                        }
                        else if (rooms[0].Device.Count == 0)
                        {
                            Console.WriteLine("Устройств нет");
                            Console.ReadLine();
                            break;
                        }
                        for (int j = 0; j < rooms.Count; j++)
                        {
                            Console.WriteLine("**************************************");
                            Console.WriteLine("Комната - {0}", rooms[j].Name);
                            Console.WriteLine("**************************************");
                            if (rooms[j].Device.Count != 0)
                            {
                                for (int i = 0; i < rooms[j].Device.Count; i++)
                                {
                                    Console.WriteLine("Имя устройства - {0}", rooms[j].Device[i].Name);
                                    if (rooms[j].Device[i].IsOn)
                                    {
                                        Console.WriteLine("Устройство включаено");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Устройство выключено");
                                    }

                                }
                            }
                            else
                            {
                                Console.WriteLine("В комнате нет устройств");
                            }
                        }
                        Console.ReadLine();
                        break;

                    case 7:
                        if (rooms.Count == 0)
                        {
                            Console.WriteLine("Комнат нет");
                            Console.ReadLine();
                            break;
                        }
                        else if (rooms[0].Device.Count == 0)
                        {
                            Console.WriteLine("Устройств нет");
                            Console.ReadLine();
                            break;
                        }

                        //*********************************************************************************


                        string[] stringsRomMenu = new string[rooms.Count + 1];
                        for (int i = 0; i < rooms.Count; i++)
                        {
                            stringsRomMenu[i] = rooms[i].Name;
                        }

                        stringsRomMenu[rooms.Count] = "Назад";

                        ConsoleMenu romMenu = new ConsoleMenu(stringsRomMenu);
                        int romMenuResult;
                        do
                        {
                            romMenuResult = romMenu.PrintMenu();
                            romMenuResult++;
                            if (romMenuResult != rooms.Count + 1)
                            {
                                if (rooms[0].Device.Count == 0)
                                {
                                    Console.WriteLine("Устройств нет");
                                    Console.ReadLine();
                                    break;
                                }
                                string[] stringsDeleteoMenu = new string[rooms[romMenuResult - 1].Device.Count + 1];
                                for (int i = 0; i < rooms[romMenuResult - 1].Device.Count; i++)
                                {
                                    stringsDeleteoMenu[i] = rooms[romMenuResult - 1].Device[i].Name;
                                }
                                stringsDeleteoMenu[rooms[romMenuResult - 1].Device.Count] = "Назад";
                                ConsoleMenu deleteoMenu = new ConsoleMenu(stringsDeleteoMenu);
                                int deleteoMenuResult;
                                do
                                {
                                    deleteoMenuResult = deleteoMenu.PrintMenu();
                                    deleteoMenuResult++;
                                    if (deleteoMenuResult != rooms[romMenuResult - 1].Device.Count + 1)
                                    {
                                        rooms[romMenuResult - 1].Device.RemoveAt(deleteoMenuResult - 1);
                                        Console.WriteLine("Устройство Удалено");
                                        Console.ReadLine();

                                    }
                                    break;
                                } while (deleteoMenuResult != stringsDeleteoMenu.Length);

                            }

                        } while (romMenuResult != stringsRomMenu.Length);



                        //************************************************************************************************


                        break;

                    case 8:
                        if (rooms.Count == 0)
                        {
                            Console.WriteLine("Комнат нет");
                            Console.ReadLine();
                            break;
                        }

                        string[] stringsDRomMenu = new string[rooms.Count + 1];
                        for (int i = 0; i < rooms.Count; i++)
                        {
                            stringsDRomMenu[i] = rooms[i].Name;
                        }

                        stringsDRomMenu[rooms.Count] = "Назад";

                        ConsoleMenu dromMenu = new ConsoleMenu(stringsDRomMenu);
                        int dromMenuResult;
                        do
                        {
                            dromMenuResult = dromMenu.PrintMenu();
                            dromMenuResult++;
                            if (dromMenuResult != rooms.Count + 1)
                            {
                                rooms.RemoveAt(dromMenuResult - 1);
                                Console.WriteLine("Комната удалена");
                                Console.ReadLine();
                            }
                            break;

                        } while (dromMenuResult != stringsDRomMenu.Length);

                        break;

                    case 9:
                        if (rooms.Count == 0)
                        {
                            Console.WriteLine("Комнат нет");
                            Console.ReadLine();
                            break;
                        }
                        else if (rooms[0].Device.Count == 0)
                        {
                            Console.WriteLine("Устройств нет");
                            Console.ReadLine();
                            break;
                        }

                        string[] stringsRromMenu = new string[rooms.Count + 1];
                        for (int i = 0; i < rooms.Count; i++)
                        {
                            stringsRromMenu[i] = rooms[i].Name;
                        }

                        stringsRromMenu[rooms.Count] = "Назад";

                        ConsoleMenu rromMenu = new ConsoleMenu(stringsRromMenu);
                        int rromMenuResult;
                        do
                        {
                            rromMenuResult = rromMenu.PrintMenu();
                            rromMenuResult++;
                            if (rromMenuResult != rooms.Count + 1)
                            {

                                string[] stringsOnMenu = new string[rooms[rromMenuResult - 1].Device.Count + 1];
                                for (int i = 0; i < rooms[rromMenuResult - 1].Device.Count; i++)
                                {
                                    stringsOnMenu[i] = rooms[rromMenuResult - 1].Device[i].Name;
                                }
                                stringsOnMenu[rooms[rromMenuResult - 1].Device.Count] = "Назад";
                                ConsoleMenu onMenu = new ConsoleMenu(stringsOnMenu);
                                int onMenuResult;
                                do
                                {
                                    onMenuResult = onMenu.PrintMenu();
                                    onMenuResult++;
                                    if (onMenuResult != rooms[rromMenuResult - 1].Device.Count + 1)
                                    {
                                        Logic logic = new Logic();
                                        if (rooms[rromMenuResult - 1].Device[onMenuResult - 1].IsOn == true)
                                        {
                                            rooms[rromMenuResult - 1].Device[onMenuResult - 1].TurnOff();
                                            logic.Ardu(rooms[rromMenuResult - 1].Device[onMenuResult - 1].IsOn);
                                        }
                                        else
                                        {
                                            rooms[rromMenuResult - 1].Device[onMenuResult - 1].TurnOn();
                                            logic.Ardu(rooms[rromMenuResult - 1].Device[onMenuResult - 1].IsOn);
                                        }

                                    }
                                    break;
                                } while (onMenuResult != stringsOnMenu.Length);

                            }

                        } while (rromMenuResult != stringsRromMenu.Length);

                        //**************************************************************************************************


                        break;
                }
            } while (mainMenuResult != stringsMainMenu.Length);
            #endregion
            #region FileSave
            string waySaveScenario = (Directory.GetCurrentDirectory() + @"\Scenario.bin");
            int countRoom = rooms.Count;
            int countDevice;
            int countScenario = scenario.Count;
            using (BinaryWriter writer = new BinaryWriter(new FileStream(waySaveScenario, FileMode.OpenOrCreate)))
            {
                writer.Write(countScenario);
                for (int i = 0; i < scenario.Count; i++)
                {
                    
                    writer.Write(scenario[i].Id.ToString());
                    writer.Write(scenario[i].Name);

                    writer.Write(scenario[i].device.Id.ToString());
                    writer.Write(scenario[i].device.Name);
                    writer.Write(scenario[i].device.IsOn);

                    writer.Write(scenario[i].time.ToString());
                }
            }
            string waySaveRoom = (Directory.GetCurrentDirectory() + @"\Rooms.bin");
            using (BinaryWriter writer = new BinaryWriter(new FileStream(waySaveRoom, FileMode.OpenOrCreate)))
            {
                writer.Write(countRoom);
                for (int i = 0; i < scenario.Count; i++)
                {
                    
                    writer.Write(rooms[i].Id.ToString());
                    writer.Write(rooms[i].Name);
                    countDevice = rooms[i].Device.Count;
                    writer.Write(countDevice);
                    for (int j = 0; j < rooms[i].Device.Count; j++)
                    {
                        
                        writer.Write(rooms[i].Device[j].Id.ToString());
                        writer.Write(rooms[i].Device[j].Name);
                        writer.Write(rooms[i].Device[j].IsOn);
                    }
                }
            }
            #endregion
        }
    }
}
