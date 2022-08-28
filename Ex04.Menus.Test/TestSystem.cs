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
        private readonly MainMenu r_MyMenu;

        public MainMenu MyMenu
        {
            get { return r_MyMenu; }
        }

        public TestSystem()
        {
            r_MyMenu = new MainMenu("~Obi-Wan Kenobi Main Menu~");

            MenuItem subMenu1 = new MenuItem("sub-menu-1");
            MenuItem subMenu2 = new MenuItem("sub-menu-2");

            subMenu1.AddMenuItem(new MenuItem("Test1"));
            subMenu1.AddMenuItem(new MenuItem("Test2"));

            subMenu2.AddMenuItem(new MenuItem("Test3"));

            r_MyMenu.AddMenuItem(subMenu1);
            r_MyMenu.AddMenuItem(subMenu2);

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
            Console.WriteLine("Hello There, " + name + " :D");
        }

        public void TestMethod3()
        {
            Console.WriteLine("Version: 22.3.4.8650");
        }

        void IMenuItemSelectedObserver.OnMenuItemSelected(MenuItem i_MenuItem)
        {
            switch (i_MenuItem.Title)
            {
                case "Test1":
                    TestMethod1();
                    break;
                case "Test2":
                    TestMethod2();
                    break;
                case "Test3":
                    TestMethod3();
                    break;
            }
        }
    }
}
