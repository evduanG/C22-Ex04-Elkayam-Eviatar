using System;

namespace Ex04.Menus.Delegates
{
    internal class Screen
    {
        /// <summary>
        /// Write a string on console
        /// </summary>
        /// <param name="i_ItemToPrint"></param>
        internal static void Print(string i_ItemToPrint)
        {
            Console.WriteLine(i_ItemToPrint);
        }

        /// <summary>
        /// Clears the console
        /// </summary>
        internal static void ClearScreen()
        {
            Console.Clear();
        }

        /// <summary>
        /// Print menu option prompt using the range of options available
        /// </summary>
        /// <param name="i_FirstMenuItemIndex"></param>
        /// <param name="i_SecondMEnuItemIndex"></param>
        internal static void ShowMenuPrompt(MainMenu i_MainMenuToPrint)
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
