using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex04.Menus.Interfaces
{
    public class MenuItem : IMenuItemSelectedObserver, IMenuItemSelectedNotifier
    {
        private const string k_ErrorInsertNull = "Error: are trying to insert a NULL menu item?";
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

        /// <summary>
        /// Check if this instance of MenuItem has sub-menus
        /// </summary>
        /// <returns>True if has sub-menus</returns>
        internal bool HasSubMenus
        {
            get { return SubMenuItems.Count > 0; }
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
        void IMenuItemSelectedObserver.MenuItem_Selected(MenuItem i_ItemSelected)
        {
            OnSelected(i_ItemSelected);
        }

        /// <summary>
        /// show sub-menus if available, otherwise activate the i_ItemSelected
        /// </summary>
        /// <param name="i_SelectedItem"></param>
        internal static void OnSelected(MenuItem i_SelectedItem)
        {
            if (i_SelectedItem.HasSubMenus)
            {
                // show sub-menu
                if(i_SelectedItem is MainMenu)
                {
                    Screen.ShowTitle(i_SelectedItem.Title);
                }
                else
                {
                    Screen.ShowTitleAsItem(i_SelectedItem.Title, i_SelectedItem.getIndxInParentMenu());
                }

                Screen.ShowSubMenus(i_SelectedItem);
            }
            else
            {
                // activate action - send i_ItemSelected up to observer
                ((IMenuItemSelectedNotifier)i_SelectedItem.ParentMenu).NotifiyObserver(i_SelectedItem);
                UserInput.AwaitProgression();
            }
        }

        // Methods as MenuItem:

        /// <summary>
        /// Add sub-menu i_ItemSelected to the menu
        /// </summary>
        /// <param name="i_MenuItemToAdd"></param>
        /// <exception cref="ArgumentException"></exception>
        public void AddMenuItem(MenuItem i_MenuItemToAdd)
        {
            if (i_MenuItemToAdd != null)
            {
                r_SubMenuItems.Add(i_MenuItemToAdd);
                ((IMenuItemSelectedNotifier)i_MenuItemToAdd).AttachObserver(this as IMenuItemSelectedObserver);
            }
            else
            {
                throw new ArgumentException(k_ErrorInsertNull);
            }
        }

        private int getIndxInParentMenu()
        {
            return this.r_ParentMenu.SubMenuItems.IndexOf(this) + 1;
        }
    }
}
