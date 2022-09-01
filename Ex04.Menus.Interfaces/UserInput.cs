using System;

namespace Ex04.Menus.Interfaces
{
    internal class UserInput
    {
        /// <summary>
        /// Get string from the console
        /// </summary>
        /// <returns>User input after trimming</returns>
        public static string Read()
        {
            return Console.ReadLine().Trim();
        }

        /// <summary>
        /// Get menu index selected from the user
        /// </summary>
        /// <returns>Menu item index selected</returns>
        /// <exception cref="ArgumentException"></exception>
        public static int ReadSelection()
        {
            if (int.TryParse(Read(), out int o_Selection))
            {
                return o_Selection;
            }
            else
            {
                throw new ArgumentException("Argument exception");
            }
        }

        /// <summary>
        /// Wait for user to accept and continue
        /// </summary>
        public static void AwaitProgression()
        {
            Screen.Print("Press any key to continue...");
            Read();
        }
    }
}