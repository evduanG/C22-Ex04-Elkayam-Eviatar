﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex04.Menus.Delegates
{
    internal class UserInput
    {
        private static string readMessage()
        {
            return Console.ReadLine();
        }

        public static string GetUserInput()
        {
            return readMessage();
        }

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
                if (int.TryParse(Read(), out int selection))
                {
                    return selection;
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
