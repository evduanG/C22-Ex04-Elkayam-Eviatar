using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex04.Menus.Interfaces
{
    public class MenuItem : IMenuItemSelectedObserver, IMenuItemSelectedNotifier
    {
        private readonly List<MenuItem> r_SubMenuItems;
        private readonly string r_Title;

        protected readonly List<IMenuItemSelectedObserver> r_MenuItemSelectedObservers = new List<IMenuItemSelectedObserver>();

        // Properties:
        public string Title
        {
            get { return r_Title; }
        }

        public List<MenuItem> SubMenuItems
        {
            get { return r_SubMenuItems; }
        }

        // Constructor:
        public MenuItem(string i_Title)
        {
            r_Title = i_Title;
            r_SubMenuItems = new List<MenuItem>();
        }

        // Methods as NOTIFIER
        void IMenuItemSelectedNotifier.AttachObserver(IMenuItemSelectedObserver i_MenuItemSelectedObserver)
        {
            r_MenuItemSelectedObservers.Add(i_MenuItemSelectedObserver);
        }

        void IMenuItemSelectedNotifier.DetachObserver(IMenuItemSelectedObserver i_MenuItemSelectedObserver)
        {
            r_MenuItemSelectedObservers.Remove(i_MenuItemSelectedObserver);
        }

        /// <summary>
        /// Notify observer (previous menu item) which menu item was selected
        /// </summary>
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
            DoWhenSelected(item);
        }

        /// <summary>
        /// show sub-menus if available, otherwise activate the item
        /// </summary>
        /// <param name="i_MenuItem"></param>
        internal void DoWhenSelected(MenuItem i_MenuItem)
        {
            if (i_MenuItem.HasSubMenus())
            {
                // show sub-menu
                MainMenu.MenuHistory.Push(i_MenuItem);
                if (SubMenuItems.IndexOf(i_MenuItem) != -1)
                {
                    Screen.ShowTitle(i_MenuItem.Title, SubMenuItems.IndexOf(i_MenuItem) + 1);
                }
                else
                {
                    Screen.ShowTitle(i_MenuItem.Title);
                }

                Screen.ShowSubMenus(i_MenuItem);
            }
            else
            {
                // activate action - send item up to observer
                ((IMenuItemSelectedNotifier)this).NotifiyObserver(i_MenuItem);
                UserInput.AwaitProgression();
            }
        }

        // Methods as MenuItem:

        /// <summary>
        /// Add sub-menu item to the menu
        /// </summary>
        /// <param name="i_MenuItem"></param>
        /// <exception cref="ArgumentException"></exception>
        public void AddMenuItem(MenuItem i_MenuItem)
        {
            if (i_MenuItem != null)
            {
                r_SubMenuItems.Add(i_MenuItem);
                ((IMenuItemSelectedNotifier)i_MenuItem).AttachObserver(this as IMenuItemSelectedObserver);
            }
            else
            {
                throw new ArgumentException("Error: are trying to insert a NULL menu item?");
            }
        }

        /// <summary>
        /// Check if this instance of MenuItem has sub-menus
        /// </summary>
        /// <returns>True if has sub-menus</returns>
        internal bool HasSubMenus()
        {
            return SubMenuItems.Count > 0;
        }
    }
}
