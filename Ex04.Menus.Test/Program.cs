using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex04.Menus.Interfaces;
using DelegatesMainMenu = Ex04.Menus.Delegates.MainMenu;

namespace Ex04.Menus.Test
{
    internal class Program
    {
        internal class InterfaceMenuSystem : IMenuItemSelectedObserver
        {
            private const string k_TitleCountSpaces = "Count Spaces";
            private const string k_TitleShowVersion = "Show Version";
            private const string k_TitleShowTime = "Show Time";
            private const string k_TitleShowDate = "Show Date";
            private readonly MainMenu r_MainMenu;

            public InterfaceMenuSystem()
            {
                r_MainMenu = createIterfaceMenu();
            }

            public void Show()
            {
                r_MainMenu.Show();
            }

            void IMenuItemSelectedObserver.MenuItem_Selected(MenuItem i_MenuItem)
            {
                switch (i_MenuItem.Action)
                {
                    case eVersionAndSpaces.CountSpaces:
                        CountSpaces();
                        break;
                    case eVersionAndSpaces.ShowVersion:
                        ShowVersion();
                        break;
                    case eDateTime.ShowTime:
                        ShowTime();
                        break;
                    case eDateTime.ShowDate:
                        ShowDate();
                        break;
                }
            }

            private MainMenu createIterfaceMenu()
            {
                MainMenu mainMenu = new MainMenu(k_TitleMainMenuInterface);
                MenuItem subMenu1 = addSubMenu(mainMenu, k_TitleSubMenuVersionAndSpaces);
                MenuItem subMenu2 = addSubMenu(mainMenu, k_TitleSubMenuDateTime);

                addMenuItem(subMenu1, k_TitleCountSpaces, eVersionAndSpaces.CountSpaces);
                addMenuItem(subMenu1, k_TitleShowVersion, eVersionAndSpaces.ShowVersion);
                addMenuItem(subMenu2, k_TitleShowTime, eDateTime.ShowTime);
                addMenuItem(subMenu2, k_TitleShowDate, eDateTime.ShowDate);

                ((IMenuItemSelectedNotifier)mainMenu).AttachObserver(this as IMenuItemSelectedObserver);
                return mainMenu;
            }

            private MenuItem addSubMenu(MainMenu i_MainMenu, string i_MenuTitle)
            {
                return new MenuItem(i_MainMenu, i_MenuTitle);
            }

            private void addMenuItem(MenuItem subMenu, string i_Title, Enum i_functionName)
            {
                new MenuItem(subMenu, i_Title, i_functionName);
            }
        }

        private const char k_SpacesChar = ' ';
        private const string k_TitleMainMenuDelegate = "Delegate Main Menu";
        private const string k_TitleMainMenuInterface = "Interface Main Menu";
        private const string k_TitleSubMenuVersionAndSpaces = "Version And Spaces";
        private const string k_TitleSubMenuDateTime = "Show Date/Time";
        private const string k_WaitForAnyInput = "Press enter to continue";
        private const string k_Version = "Version: 22.3.4.8650";
        private const string k_AskForLineToCountSpaces = @"Please enter the desired line in which you want to count the spaces";
        private const string k_StrFormtOfCountSpace = @"There are {0} spaces in the line:
{1}";

        public const string k_TimeFormat = "The time is: {0}:{1}";
        public const string k_DateFormat = "The date is: {0}/{1}/{2}";

        public static void Main()
        {
            InterfaceMenuSystem testSystem = new InterfaceMenuSystem();
            testSystem.Show();

            bool isInitSecsucceeded = createDelegatesMainMenu(out DelegatesMainMenu o_MainMenuDelegates);
            if (isInitSecsucceeded)
            {
                o_MainMenuDelegates.Show();
            }
        }

        private static bool createDelegatesMainMenu(out DelegatesMainMenu o_MainMenuDelegates)
        {
            bool initiationDelegates = false;
            o_MainMenuDelegates = new DelegatesMainMenu(k_TitleMainMenuDelegate);
            try
            {
                // add Version And Spaces
                DelegatesMainMenu subMenueVersionAndSpaces = new DelegatesMainMenu(k_TitleSubMenuVersionAndSpaces);
                subMenueVersionAndSpaces.CreatMenuItemFromEnum(typeof(eVersionAndSpaces));
                subMenueVersionAndSpaces[eVersionAndSpaces.ShowVersion].SelectItemOccured += MenuItem_Select_Version;
                subMenueVersionAndSpaces[eVersionAndSpaces.CountSpaces].SelectItemOccured += MenuItem_Select_CountSpaces;
                o_MainMenuDelegates.AddMenuItem(subMenueVersionAndSpaces);

                // add Version And Spaces
                DelegatesMainMenu subMenueDateTime = new DelegatesMainMenu(k_TitleSubMenuDateTime);
                subMenueDateTime.CreatMenuItemFromEnum(typeof(eDateTime));
                subMenueDateTime[eDateTime.ShowTime].SelectItemOccured += MenuItem_Select_ShowTime;
                subMenueDateTime[eDateTime.ShowDate].SelectItemOccured += MenuItem_Select_ShowDate;
                o_MainMenuDelegates.AddMenuItem(subMenueDateTime);

                initiationDelegates = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return initiationDelegates;
        }

        public static void CountSpaces()
        {
            Console.WriteLine(k_AskForLineToCountSpaces);
            string userSentence = Console.ReadLine();
            int numOfSpaces = 0;

            foreach(char c in userSentence)
            {
               if(c == k_SpacesChar)
                {
                    numOfSpaces++;
                }
            }

            Console.WriteLine(k_StrFormtOfCountSpace, numOfSpaces, userSentence);
        }

        public static void MenuItem_Select_CountSpaces(object sender)
        {
            CountSpaces();
            waitForAnyInput();
        }

        public static void ShowVersion()
        {
            Console.WriteLine(k_Version);
        }

        public static void MenuItem_Select_Version(object sender)
        {
            ShowVersion();
            waitForAnyInput();
        }

        public static void ShowTime()
        {
            Console.WriteLine(k_TimeFormat, DateTime.Now.Hour, DateTime.Now.Minute);
        }

        public static void MenuItem_Select_ShowTime(object sender)
        {
            ShowTime();
            waitForAnyInput();
        }

        public static void ShowDate()
        {
            Console.WriteLine(k_DateFormat, DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year);
        }

        public static void MenuItem_Select_ShowDate(object sender)
        {
            ShowDate();
            waitForAnyInput();
        }

        private static void waitForAnyInput()
        {
            Console.WriteLine(k_WaitForAnyInput);
            Console.ReadLine();
        }
    }
}