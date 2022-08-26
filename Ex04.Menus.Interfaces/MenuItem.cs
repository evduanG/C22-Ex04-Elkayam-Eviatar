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

        void IMenuItemSelectedNotifier.NotifiyObserver(MenuItem i_MenuItem)
        {
            foreach(IMenuItemSelectedObserver observer in r_MenuItemSelectedObservers)
            {
                observer.OnMenuItemSelected(this);
            }
        }

        // Methods as OBSERVER:
        void IMenuItemSelectedObserver.OnMenuItemSelected(MenuItem item)
        {
            Screen.ClearScreen();
            item.DoWhenSelected();
        }

        // Methods as MenuItem:
        internal void DoWhenSelected()
        {
            if(r_SubMenuItems.Count > 0)
            {
                // show sub-menu
                Screen.ShowTitle(r_Title);
                Screen.ShowSubMenus(this);
            }
            else
            {
                // activate action - send item up to observer
                Console.WriteLine("action activated");
                ((IMenuItemSelectedNotifier)this).NotifiyObserver(this);
                
                // TODO: think about what to after selected

                //if (r_MenuItemSelectedObservers[0] is MenuItem)
                //{
                //    Screen.ShowSubMenus(r_MenuItemSelectedObservers[0] as MenuItem);
                //    ((MenuItem)r_MenuItemSelectedObservers[0]).Show();
                //}
            }
        }

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

        // TOOD: move to main menu?
        public void Show()
        {
            bool choseQuit = false;
            MenuItem currentMenu = this;
            do
            {
                try
                {
                    currentMenu.DoWhenSelected();
                    Screen.ShowMenuPrompt(1, currentMenu.SubMenuItems.Count);
                    int itemSelectedIndex = UserInput.ReadSelection();
                    authenticate(itemSelectedIndex, 0, currentMenu.SubMenuItems.Count);

                    if (itemSelectedIndex == 0)
                    {
                        choseQuit = true;
                    }
                    else
                    {
                        MenuItem chosenItem = SubMenuItems[itemSelectedIndex - 1];
                        currentMenu = chosenItem;
                    }
                }
                catch (ArgumentOutOfRangeException)
                {
                    Screen.ShowErrorMessage(eExceptionType.ValueOutOfBounds);
                }
                catch (ArgumentException)
                {
                    Screen.ShowErrorMessage(eExceptionType.Parsing);
                }
                catch (Exception e)
                {
                    Screen.Print("unknown exception: " + e.Message);
                }

                //UserInput.AwaitProgression();
                //Screen.ClearScreen();
            }
            while (!choseQuit);

            Screen.Print("Goodbye!");
        }

        /// <summary>
        /// Validate that the index given is within lower and upper bounds (inclusive)
        /// </summary>
        /// <param name="i_ItemSelectedIndex"></param>
        /// <param name="i_LowerBound"></param>
        /// <param name="i_UpperBound"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        private void authenticate(int i_ItemSelectedIndex, int i_LowerBound, int i_UpperBound)
        {
            if (i_ItemSelectedIndex < i_LowerBound || i_ItemSelectedIndex > i_UpperBound)
            {
                throw new ArgumentOutOfRangeException();
            }
        }
    }
}
