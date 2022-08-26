using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex04.Menus.Interfaces
{
    public class MainMenu : MenuItem, IMenuItemSelectedObserver, IMenuItemSelectedNotifier
    {
        // TODO: current menu member?

        // Constructor
        public MainMenu(string i_Title)
            : base(i_Title)
        {
        }

        // Methods as NOTIFIER
        void IMenuItemSelectedNotifier.NotifiyObserver(MenuItem i_MenuItem)
        {
            foreach(IMenuItemSelectedObserver observer in r_MenuItemSelectedObservers)
            {
                observer.OnMenuItemSelected(i_MenuItem);
            }
        }

        // Methods as OBSERVER:
        void IMenuItemSelectedObserver.OnMenuItemSelected(MenuItem item)
        {
            Console.Clear();
            ((IMenuItemSelectedNotifier)this).NotifiyObserver(item);
        }

        // Methods as MainMenu
        internal void DoWhenSelected(MenuItem menuItem)
        {
            if (SubMenuItems.Count > 0)
            {
                // show sub-menus
                Screen.ShowTitle(Title);
                Screen.ShowSubMenus(menuItem);
            }
            else
            {
                // activate action - send item up to observer
                Console.WriteLine("action activated");
                ((IMenuItemSelectedNotifier)this).NotifiyObserver(menuItem);
            }
        }
    }
}
