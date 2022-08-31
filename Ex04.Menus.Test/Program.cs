using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex04.Menus.Test;
using DelegatesMainMenu = Ex04.Menus.Delegates.MainMenu;


// using InterfacesMenu = Ex04.Menus.Interfaces;
namespace Program
{
    internal class Program : IMenuItemSelectedObserver
    {
 // master
        private MainMenu m_MainMenu1;
        private MainMenu m_MainMenu2;

        public Program()
        {
            m_MainMenu1 = createIterfaceMenu();

            // m_MainMenu2 = createDelegateMenu();
        }

        public static void Main()
        {
            Program testProgram = new Program();

            testProgram.m_MainMenu1.Show();
            // testProgram.m_MainMenu2.Show();
        }

        private MainMenu createIterfaceMenu()
        {
            MainMenu mainMenu2 = new MainMenu("Interface Main Menu");
            MenuItem firstLevelMenu1 = new MenuItem(mainMenu2, "Version and Spaces");
            MenuItem firstLevelMenu2 = new MenuItem(mainMenu2, "Show Date/Time");
            _ = new MenuItem(firstLevelMenu1, "Count Spaces", eActions.CountSpaces);
            _ = new MenuItem(firstLevelMenu1, "Show Version", eActions.ShowVersion);
            _ = new MenuItem(firstLevelMenu2, "Show Time", eActions.ShowTime);
            _ = new MenuItem(firstLevelMenu2, "Show Date", eActions.ShowDate);

            ((IMenuItemSelectedNotifier)mainMenu2).AttachObserver(this as IMenuItemSelectedObserver);
            return mainMenu2;
        }

        public static void CountSpaces()
        {
            Console.WriteLine("Please enter your sentence:");
            string userSentence = Console.ReadLine();
            int numOfSpaces = 0;

            foreach(char c in userSentence)
            {
               if(c == ' ')
                {
                    numOfSpaces++;
                }
            }

            Console.WriteLine("There are {0} spaces in your sentence", numOfSpaces);
        }

        public static void ShowVersion()
        {
            Console.WriteLine("Version: 22.3.4.8650");
        }

        public static void ShowTime()
        {
            Console.WriteLine("{0}:{1}", DateTime.Now.Hour, DateTime.Now.Minute);
          // 
        private const char k_SpacesChar = ' ';
        private const string k_TitleMainMenu = "Deleates Main Menu";
        private const string k_TitleSubMenueVersionAndSpaces = "Version And Spaces";
        private const string k_TitleSubMenueDateTime = "Show Date/Time";
        private const string k_WaitForAnyInpu = "Enter any input to continue";
        private const string k_Version = "Version: 22.3.4.8650";
        private const string k_AskForLineToCountSpaces = @"Please enter the desired line where you want to count the Spaces";
        private const string k_StrFormtOfCountSpace = @"There is a {0} Space in the line :
{1}";

        public static void Main()
        {
            bool isInitSecsucceeded = initiationDelegatesMainMenu(out DelegatesMainMenu io_MainMenuDelegates);
            if(isInitSecsucceeded)
            {
                io_MainMenuDelegates.Show();
            }
        }

        private static bool initiationDelegatesMainMenu(out DelegatesMainMenu io_MainMenuDelegates)
        {
            bool initiationDelegates = false;
            io_MainMenuDelegates = new DelegatesMainMenu(k_TitleMainMenu);
            try
            {
                // add Version And Spaces
                DelegatesMainMenu subMenueVersionAndSpaces = new DelegatesMainMenu(k_TitleSubMenueVersionAndSpaces);
                subMenueVersionAndSpaces.CreatMenuItemFromEnum(typeof(eVersionAndSpaces));
                subMenueVersionAndSpaces[eVersionAndSpaces.ShowVersion].SelectItemOccured += MenuItem_Select_Version;
                subMenueVersionAndSpaces[eVersionAndSpaces.CountSpaces].SelectItemOccured += MenuItem_Select_CountSpaces;
                io_MainMenuDelegates.AddSubMenue(subMenueVersionAndSpaces);

                // add Version And Spaces
                DelegatesMainMenu subMenueDateTime = new DelegatesMainMenu(k_TitleSubMenueDateTime);
                subMenueDateTime.CreatMenuItemFromEnum(typeof(eDateTime));
                subMenueDateTime[eDateTime.ShowTime].SelectItemOccured += MenuItem_Select_ShowTime;
                subMenueDateTime[eDateTime.ShowDate].SelectItemOccured += MenuItem_Select_ShowDate;
                io_MainMenuDelegates.AddSubMenue(subMenueDateTime);

                initiationDelegates = true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return initiationDelegates;
        }

        public static void MenuItem_Select_Version(object sender)
        {
            Console.WriteLine(k_Version);
            waitForAnyInput();
        }

        public static void MenuItem_Select_CountSpaces(object sender)
        {
            string userInputToCount = getLineForCountSpaces();
            int numOfSpaces = countSpacaes(userInputToCount, k_SpacesChar);
            Console.WriteLine(string.Format(k_StrFormtOfCountSpace, numOfSpaces, userInputToCount));
            waitForAnyInput();
        }

        private static void waitForAnyInput()
        {
            Console.WriteLine(k_WaitForAnyInpu);
            Console.Read();
        }

        private static int countSpacaes(string i_StrToCount, char i_CharToCount)
        {
            int cunt = 0;
            char[] charsOfStr = i_StrToCount.ToCharArray();
            foreach(char c in charsOfStr)
            {
                if(c == i_CharToCount)
                {
                    ++cunt;
                }
            }

            return cunt;
        }

        private static string getLineForCountSpaces()
        {
            Console.WriteLine(k_AskForLineToCountSpaces);
            return Console.ReadLine();
        }

        public static void MenuItem_Select_ShowDate(object sender)
        {
            showDateTime(eDateTime.ShowDate);
            waitForAnyInput();
        }

        public static void MenuItem_Select_ShowTime(object sender)
        {
            showDateTime(eDateTime.ShowTime);
            waitForAnyInput();
        }

        private static void showDateTime(eDateTime i_DateTime)
        {
            string strToPrint;
            switch (i_DateTime)
            {
                case eDateTime.ShowTime:
                    strToPrint = DateTime.UtcNow.ToString();
                    break;
                case eDateTime.ShowDate:
                    strToPrint = DateTime.Today.ToString();
                    break;
                default:
                    strToPrint = DateTime.Now.ToString();
                    break;
            }

            Console.WriteLine(strToPrint);
     /// eviatar-dev
        }

        public static void ShowDate()
        {
            Console.WriteLine("{0}/{1}/{2}", DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year);
        }

        void IMenuItemSelectedObserver.MenuItem_Selected(MenuItem i_MenuItem)
        {
            switch (i_MenuItem.Action)
            {
                case eActions.CountSpaces:
                    CountSpaces();
                    break;
                case eActions.ShowVersion:
                    ShowVersion();
                    break;
                case eActions.ShowTime:
                    ShowTime();
                    break;
                case eActions.ShowDate:
                    ShowDate();
                    break;
            }
        }

    }
}