using System;

namespace Ex04.Menus.Interfaces
{
    internal class Screen
    {
        private const string k_ParsingErrorMesage = "Invalid type of input, please try again!";
        private const string k_ValueOutOfBoundsMesage = "No such option available, please try again!";
        private const string k_LineSeparator = "================================";
        private const string k_ReturnVal = "0";
        private const string k_Exit = "Exit";
        private const string k_Back = "Back";

        private const string k_MenuOptionStructure = "{0}. {1}";
        private const string k_PromptStructure = "Please enter one of the options above ({0}-{1} or 0 to {2}):";

        /// <summary>
        /// Write a string on console
        /// </summary>
        /// <param name="i_StrToPrint"></param>
        internal static void Print(string i_StrToPrint)
        {
            Console.WriteLine(i_StrToPrint);
        }

        /// <summary>
        /// Write a string with arguments inside using string.Format()
        /// </summary>
        /// <param name="i_Structure"></param>
        /// <param name="i_Args"></param>
        private static void print(string i_Structure, params string[] i_Args)
        {
            Print(string.Format(i_Structure, i_Args));
        }

        /// <summary>
        /// print the menu option with index at the beginning
        /// </summary>
        /// <param name="i_Title"></param>
        /// <param name="i_Index"></param>
        internal static void ShowTitleAsItem(string i_Title, int i_Index)
        {
            print(k_MenuOptionStructure, i_Index.ToString(), i_Title);
            Print(k_LineSeparator);
        }

        /// <summary>
        /// print the main menu title
        /// </summary>
        /// <param name="i_Title"></param>
        internal static void ShowTitle(string i_Title)
        {
            Print(i_Title);
            Print(k_LineSeparator);
        }

        /// <summary>
        /// Clears the console
        /// </summary>
        internal static void ClearScreen()
        {
            Console.Clear();
        }

        /// <summary>
        /// Go over the sub-menus list and print in format
        /// </summary>
        /// <param name="i_SubMenusToShow"></param>
        internal static void ShowSubMenus(MenuItem i_MenuItem)
        {
            int index = 1;

            foreach(MenuItem menuItem in i_MenuItem.SubMenuItems)
            {
                print(k_MenuOptionStructure, index.ToString(), menuItem.Title);
                index++;
            }

            print(k_MenuOptionStructure, k_ReturnVal, getExitOrBack(i_MenuItem));
            Print(k_LineSeparator);
        }

        /// <summary>
        /// print menu option prompt using the range of options available
        /// </summary>
        /// <param name="i_FirstMenuItemIndex"></param>
        /// <param name="i_SecondMenuItemIndex"></param>
        internal static void ShowMenuPrompt(MenuItem i_MenuItem, int i_FirstMenuItemIndex, int i_SecondMenuItemIndex)
        {
            print(k_PromptStructure, i_FirstMenuItemIndex.ToString(), i_SecondMenuItemIndex.ToString(), getExitOrBack(i_MenuItem));
        }

        private static string getExitOrBack(MenuItem i_MenuItemToCheckIsMain)
        {
            return i_MenuItemToCheckIsMain is MainMenu ? k_Exit : k_Back;
        }

        /// <summary>
        /// print message according to the exception type given
        /// </summary>
        /// <param name="i_ErrorType"></param>
        internal static void ShowErrorMessage(eExceptionType i_ErrorType)
        {
            switch (i_ErrorType)
            {
                case eExceptionType.Parsing:
                    Print(k_ParsingErrorMesage);
                    break;
                case eExceptionType.ValueOutOfBounds:
                    Print(k_ValueOutOfBoundsMesage);
                    break;
            }
        }
    }
}
