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
        private readonly MenuItem r_ParentMenu;
        private readonly string r_Title;
        private readonly Enum r_Action;

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

        public MenuItem ParentMenu
        {
            get { return r_ParentMenu; }
        }

        public Enum Action
        {
            get { return r_Action; }
        }

        // Constructor:
        public MenuItem(MenuItem i_ParentMenu, string i_Title)
        {
            r_Title = i_Title;
            r_ParentMenu = i_ParentMenu;
            if (i_ParentMenu != null)
            {
                i_ParentMenu.AddMenuItem(this);
            }

            r_Action = null;
            r_SubMenuItems = new List<MenuItem>();
        }

        public MenuItem(MenuItem i_ParentMenu, string i_Title, Enum i_Action)
            : this(i_ParentMenu, i_Title)
        {
            r_Action = i_Action;
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

        void IMenuItemSelectedNotifier.NotifiyObserver(MenuItem i_MenuItem)
        {
            foreach(IMenuItemSelectedObserver observer in r_MenuItemSelectedObservers)
            {
                observer.MenuItem_Selected(i_MenuItem);
            }
        }

        // Methods as OBSERVER:
        void IMenuItemSelectedObserver.MenuItem_Selected(MenuItem item)
        {
            DoWhenSelected(item);
        }

        /// <summary>
        /// show sub-menus if available, otherwise activate the item
        /// </summary>
        /// <param name="i_MenuItem"></param>
        internal static void DoWhenSelected(MenuItem i_MenuItem)
        {
            if (i_MenuItem.HasSubMenus())
            {
                // show sub-menu
                MenuItem parentMenuOfSelectedMenu = i_MenuItem.ParentMenu;
                if(i_MenuItem is MainMenu)
                {
                    Screen.ShowTitle(i_MenuItem.Title);
                }
                else
                {
                    Screen.ShowTitle(i_MenuItem.Title, parentMenuOfSelectedMenu.SubMenuItems.IndexOf(i_MenuItem) + 1);
                }

                Screen.ShowSubMenus(i_MenuItem);
            }
            else
            {
                // activate action - send item up to observer
                ((IMenuItemSelectedNotifier)i_MenuItem.ParentMenu).NotifiyObserver(i_MenuItem);
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
