using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex04.Menus.Interfaces;
using Ex04.Menus.Test;

namespace Program
{
    internal class Program : IMenuItemSelectedObserver
    {
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