using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex04.Menus.Delegates
{
    internal struct Screen
    {
        public static void ShowMessage(string i_Msg)
        {
            print(i_Msg);
        }
        public static void ShowErrorMessage(string i_ErrorMsg)
        {
            print(i_ErrorMsg);
        }

        private static void print(string i_Msg)
        {
            Console.WriteLine(i_Msg);
        }

    }
}
