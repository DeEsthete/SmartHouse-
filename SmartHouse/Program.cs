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

            List<Device> obj=new List<Device>();
            User user = new User();
            bool isRegister=false;//это типа файл
            Secutiry security = new Secutiry();
            string login, password;
            #region Temp
            //ElectricityPowerSensor electricityPowerSensorTemp;//доделать кейсыс датчиками
            MoovingSensor moovingSensorTemp;
            OpenSensor openSensorTemp;
            TemperatureSensor temperatureSensorTemp;
            #endregion
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
            string[] stringsMainMenu = {  "Добавить устройство", "Добавить сценарий", "Просмотреть сценарии", "Удалить сценарий", "Функции", "Просмотреть устройства", "Выход" };
            
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
                                                moovingSensorTemp = new MoovingSensor();
                                                obj.Add(electricityPowerSensorTemp);
                                                Console.WriteLine("Датчик движения добавлен");
                                                Console.ReadLine();
                                                break;

                                            case 2:
                                                Console.WriteLine("Датчик температуры добавлен");
                                                Console.ReadLine();
                                                break;

                                            case 3:
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
                                    Console.WriteLine("Введите имя устройства");
                                    Console.ReadLine();//name
                                    Console.WriteLine("Устройство добавлено");
                                    Console.ReadLine();
                                    break;
                            }


                            //Console.ReadKey();
                        } while (deviceMenuResult != stringsDeviceMenu.Length);
                        break;



                    case 2:
                        string[] stringsScenarioMenu = { "Выбрать устройство", "Назначить время", "Что сделать с уcтройством", "Назад" };
                        ConsoleMenu scenarioMenu = new ConsoleMenu(stringsScenarioMenu);
                        int scenarioMenuResult;
                        do
                        {
                            scenarioMenuResult = scenarioMenu.PrintMenu();
                            scenarioMenuResult++;



                            switch (scenarioMenuResult)
                            {
                                case 1:
                                    //создается массив строк на размер массива устройств из него делаем менюшку так же проверка на наличие устройств
                                    //
                                    //
                                    //
                                    break;

                                case 2:
                                    //DateTime a=DateTime.Now;
                                    //КАК ДОБАВИТ ВРЕМЯ
                                    //
                                    //
                                    //
                                    break;


                                case 3:
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
                                                Console.WriteLine("Устройство будет включено");
                                                Console.ReadLine();
                                                break;

                                            case 2:
                                                //занести в сценарий что устройство выключается
                                                Console.WriteLine("Устройство будет выключено");
                                                Console.ReadLine();
                                                break;

                                        }



                                    } while (onOffMenuResult != stringsOnOffMenu.Length);
                                    break;
                            }



                        } while (scenarioMenuResult != stringsScenarioMenu.Length);
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

                        string[] stringsFunctionsMenu = { "Описание Функций", "Изменить максимальную температуру", "Изменить максимальное потребление тока(Вт)", "Назад" };
                        ConsoleMenu functionsMenu = new ConsoleMenu(stringsFunctionsMenu);
                        int functionsMenuResult;
                        do
                        {
                            functionsMenuResult = functionsMenu.PrintMenu();
                            functionsMenuResult++;



                            switch (functionsMenuResult)
                            {
                                case 1:
                                    Console.WriteLine("1 - При достижении максимальной температуры в 30*С будет включаться кондиционер(если он присутсвует)");
                                    Console.WriteLine("2 - Когда произойдет перенапряжение система оповестит вас о том что вы используете слишком много электро энергии");
                                    Console.ReadLine();//как то определить наличие кондиционера
                                    break;

                                case 2:
                                    //занести в сценарий что устройство выключается
                                    Console.WriteLine("Введите максимальную температуру");//temperature - change
                                    Console.ReadLine();
                                    Console.WriteLine("Температура изменена");
                                    Console.ReadLine();
                                    break;

                                case 3:
                                    //нельзя точно определить напряжение так что хз
                                    //
                                    //
                                    break;

                            }



                        } while (functionsMenuResult != stringsFunctionsMenu.Length);

                        break;


                    case 6:
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
