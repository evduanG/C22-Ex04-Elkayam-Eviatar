using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex04.Menus.Interfaces;

namespace Program
{
    internal class Program
    {
        public static void Main()
        {
            TestSystem sys = new TestSystem();
            sys.MyMenu.Show();
            Console.ReadLine();
        }
    }
}
