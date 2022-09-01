using System;

namespace Ex04.Menus.Delegates
{
    internal class UserInput
    {
        /// <summary>
        /// Get string from the console
        /// </summary>
        /// <returns>User input after trimming</returns>
        internal static string Read()
        {
            return Console.ReadLine().Trim();
        }

        /// <summary>
        /// Get menu index selected from the user
        /// </summary>
        /// <returns>Menu item index selected</returns>
        /// <exception cref="ArgumentException"></exception>
        internal static byte ReadSelection()
        {
            if (byte.TryParse(Read(), out byte selection))
            {
                return selection;
            }
            else
            {
                throw new ArgumentException("Argument exception:Your selection was not in the correct format");
            }
        }
    }
}
