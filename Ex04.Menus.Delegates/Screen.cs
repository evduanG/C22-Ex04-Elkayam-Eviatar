using System;

namespace Ex04.Menus.Delegates
{
    internal class Screen
    {
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

        /// <summary>
        /// Clears the console
        /// </summary>
        public static void ClearScreen()
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
            Print(i_MainMenuToPrint.ToString());
        }

        internal static void ShowEror(Exception e)
        {
            Print(e.Message);
        }
    }
}
