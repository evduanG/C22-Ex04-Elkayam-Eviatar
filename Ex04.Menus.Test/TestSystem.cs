using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex04.Menus.Interfaces;

namespace Ex04.Menus.Interfaces
{
    internal class TestSystem : IMenuItemSelectedObserver
    {
        public readonly MainMenu r_MyMenu;

        public TestSystem()
        {
            r_MyMenu = new MainMenu("Master Kenobi~~");
            r_MyMenu.AddMenuItem(new MenuItem("Test1"));
            r_MyMenu.AddMenuItem(new MenuItem("Test2"));

            ((IMenuItemSelectedNotifier)r_MyMenu).AttachObserver(this as IMenuItemSelectedObserver);
        }

        public void TestMethod1()
        {
            Console.WriteLine("Happy Testing!");
        }

        public void TestMethod2()
        {
            Console.WriteLine("Enter your name: ");
            string name = Console.ReadLine();
            Console.WriteLine("hello " + name + " :D");
        }

        void IMenuItemSelectedObserver.OnMenuItemSelected(MenuItem item)
        {
            switch (item.Title)
            {
                case "Test1":
                    TestMethod1();
                    break;
                case "Test2":
                    TestMethod2();
                    break;
            }
        }
    }
}
