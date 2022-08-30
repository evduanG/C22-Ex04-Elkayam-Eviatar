using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex04.Menus.Delegates
{
    internal class Screen
    {
        private const string k_ParsingErrorMesage = "Invalid type of input, please try again!";
        private const string k_ValueOutOfBoundsMesage = "No such option available, please try again!";
        private const string k_LineSeparator = "================================";

        private const string k_MenuOptionStructure = "{0}. {1}";
        private const string k_PromptStructure1 = "Please enter one of the options above ({0}-{1} or 0 to exit):";
        private const string k_PromptStructure2 = "Please enter one of the options above ({0}-{1} or 0 to go back):";

        /// <summary>
        /// Write a string on console
        /// </summary>
        /// <param name="i_ItemToPrint"></param>
        public static void Print(string i_ItemToPrint)
        {
            Console.WriteLine(i_ItemToPrint);
        }

        /// <summary>
        /// Write a string with arguments inside using string.Format()
        /// </summary>
        /// <param name="i_Structure"></param>
        /// <param name="i_Args"></param>
        public static void Print(string i_Structure, params string[] i_Args)
        {
            Print(string.Format(i_Structure, i_Args));
        }

        /// <summary>
        /// Print the menu option with index at the beginning
        /// </summary>
        /// <param name="i_Title"></param>
        /// <param name="i_Index"></param>
        public static void ShowTitle(string i_Title, int i_Index)
        {
            Print(k_MenuOptionStructure, i_Index.ToString(), i_Title);
            Print(k_LineSeparator);
        }

        /// <summary>
        /// Print the main menu title
        /// </summary>
        /// <param name="i_Title"></param>
        public static void ShowTitle(string i_Title)
        {
            Print(i_Title);
            Print(k_LineSeparator);
        }

        /// <summary>
        /// Clears the console
        /// </summary>
        private static void ClearScreen()
        {
            Console.Clear();
        }

        /// <summary>
        /// Go over the sub-menus list and print in format
        /// </summary>
        /// <param name="i_SubMenusToShow"></param>

        /// <summary>
        /// Print menu option prompt using the range of options available
        /// </summary>
        /// <param name="i_FirstMenuItemIndex"></param>
        /// <param name="i_SecondMEnuItemIndex"></param>
        public static void ShowMenuPrompt(MainMenu i_MainMenuToPrint)
        {
            ClearScreen();
           // Print(i_MainMenuToPrint.CreateMenuStr());
        }

        internal static void ShowEror(Exception e)
        {
            Print(e.Message);
        }

        /// <summary>
        /// Print message according to the exception type given
        /// </summary>
        /// <param name="i_ErrorType"></param>
        //public static void ShowErrorMessage(eExceptionType i_ErrorType)
        //{
        //    switch (i_ErrorType)
        //    {
        //        case eExceptionType.Parsing:
        //            Print(k_ParsingErrorMesage);
        //            break;
        //        case eExceptionType.ValueOutOfBounds:
        //            Print(k_ValueOutOfBoundsMesage);
        //            break;
        //    }
        //}
    }
}
