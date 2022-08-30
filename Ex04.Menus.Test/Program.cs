﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex04.Menus.Test;
using DelegatesMainMenu = Ex04.Menus.Delegates.MainMenu;
using InterfacesMenu = Ex04.Menus.Interfaces;

namespace Program
{
    internal class Program
    {
        private const string k_TitleMainMenu = "Deleates Main Menu";
        private const string k_TitleSubMenueVersionAndSpaces = "Version And Spaces";
        private const string k_TitleSubMenueDateTime = "Show Date/Time";

        private const string k_Version = "Version: 22.3.4.8650";
        private const string k_StrFormtOfCountSpace = @"There is a {0} Space in the line 
{1}";

        private const string k_AskForLineToCountSpaces = @"Please enter the desired line where you want to count the Spaces";
        private const char k_SpacesChar = ' ';

        public static void Main()
        {
            initiationDelegatesMainMenu(out DelegatesMainMenu io_MainMenuDelegates);
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
                subMenueVersionAndSpaces.AddReturnItem();
                io_MainMenuDelegates.AddSubMenue(subMenueVersionAndSpaces);

                // add Version And Spaces
                DelegatesMainMenu subMenueDateTime = new DelegatesMainMenu(k_TitleSubMenueDateTime);
                subMenueVersionAndSpaces.CreatMenuItemFromEnum(typeof(eDateTime));
                subMenueVersionAndSpaces[eDateTime.ShowTime].SelectItemOccured += MenuItem_Select_ShowTime;
                subMenueVersionAndSpaces[eDateTime.ShowDate].SelectItemOccured += MenuItem_Select_ShowDate;
                subMenueVersionAndSpaces.AddReturnItem();
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
        }

        public static void MenuItem_Select_CountSpaces(object sender)
        {
            string userInputToCount = getLineForCountSpaces();
            int numOfSpaces = countSpacaes(userInputToCount, k_SpacesChar);
            Console.WriteLine(string.Format(k_StrFormtOfCountSpace, numOfSpaces, userInputToCount));
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
        }

        public static void MenuItem_Select_ShowTime(object sender)
        {
            showDateTime(eDateTime.ShowTime);
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
        }
    }
}
