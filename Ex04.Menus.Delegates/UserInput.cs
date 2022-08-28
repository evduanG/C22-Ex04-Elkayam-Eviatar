using System;
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

    }
}
