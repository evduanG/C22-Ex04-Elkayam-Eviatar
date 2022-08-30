using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex04.Menus.Interfaces;

namespace Ex04.Menus.Interfaces
{
    internal class TestSystem
    {
        private readonly TestSystemInterfaces r_SystemInterfaces = new TestSystemInterfaces();
        private readonly TestSystemDelegates r_SystemDelegates = new TestSystemDelegates();

        public void ShowInterfacesMenu()
        {
            r_SystemInterfaces.ShowMenu();
        }

        public void ShowDelegatessMenu()
        {
            r_SystemDelegates.ShowMenu();
        }

        internal class TestSystemDelegates
        {

            public TestSystemDelegates()
            {

            }

            internal void ShowMenu()
            {
                throw new NotImplementedException();
            }
        }

        internal class TestSystemInterfaces : IMenuItemSelectedObserver
        {
            private readonly MainMenu r_MyMenu;

            public MainMenu MyMenu
            {
                get { return r_MyMenu; }
            }

            public TestSystemInterfaces()
            {
                r_MyMenu = new MainMenu("~Obi-Wan Kenobi Main Menu~");
                addSubMenu();
            }

            private void addSubMenu()
            {
                MenuItem subMenu1 = new MenuItem(r_MyMenu, "sub-menu-1");
                MenuItem subMenu2 = new MenuItem(r_MyMenu, "sub-menu-2");

                // added these lines to the ctor :D

                // r_MyMenu.AddMenuItem(subMenu1);
                // r_MyMenu.AddMenuItem(subMenu2);

                MenuItem test1 = new MenuItem(subMenu1, "Test1");
                MenuItem test2 = new MenuItem(subMenu1, "Test2");
                MenuItem test3 = new MenuItem(subMenu2, "Test3");

                // subMenu1.AddMenuItem(new MenuItem(subMenu1, "Test1"));
                // subMenu1.AddMenuItem(new MenuItem(subMenu1, "Test2"));
                // subMenu2.AddMenuItem(new MenuItem(subMenu2, "Test3"));

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

            void IMenuItemSelectedObserver.MenuItem_Selected(MenuItem i_MenuItem)
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

            internal void ShowMenu()
            {
                MyMenu.Show();
            }
        }
    }
}
