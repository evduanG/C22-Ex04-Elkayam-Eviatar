using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex04.Menus.Interfaces
{
    public class MainMenu : MenuItem, IMenuItemSelectedObserver, IMenuItemSelectedNotifier
    {
        // TODO: make it a member of MainMenu (because it is unique to every instance of MainMenu) AND accessible from MenuItem Class (?)
        internal static Stack<MenuItem> s_MenuHistory = new Stack<MenuItem>();

        public static Stack<MenuItem> MenuHistory
        {
            get { return s_MenuHistory; }
            set { s_MenuHistory = value; }
        }

        // Constructor
        public MainMenu(string i_Title)
            : base(i_Title)
        {
            MenuHistory.Push(this);
            MenuHistory.Push(this);
        }

        // Methods as OBSERVER:
        void IMenuItemSelectedObserver.OnMenuItemSelected(MenuItem item)
        {
            ((IMenuItemSelectedNotifier)this).NotifiyObserver(item);
        }

        /**
        internal override void DoWhenSelected(MenuItem i_MenuItem)
        {
            if (i_MenuItem.HasSubMenus())
            {
                // show sub-menus
                MenuHistory.Push(i_MenuItem);
                Screen.ShowTitle(i_MenuItem.Title);
                Screen.ShowSubMenus(i_MenuItem);
            }
            else
            {
                // activate action - send item up to observer
                Console.WriteLine("action activated");
                ((IMenuItemSelectedNotifier)this).NotifiyObserver(i_MenuItem);

                UserInput.AwaitProgression();
            }
        }
        **/

        // Methods as MainMenu

        /// <summary>
        /// Main loop for menu interface
        /// </summary>
        public void Show()
        {
            bool choseQuit = false;
            do
            {
                try
                {
                    MenuItem currentMenuItem = MenuHistory.Pop();
                    MenuHistory.Peek().DoWhenSelected(currentMenuItem);

                    if (currentMenuItem.HasSubMenus())
                    {
                        Screen.ShowMenuPrompt(currentMenuItem, 1, currentMenuItem.SubMenuItems.Count);
                        int itemSelectedIndex = UserInput.ReadSelection();
                        Authenticate(itemSelectedIndex, 0, currentMenuItem.SubMenuItems.Count);

                        if (itemSelectedIndex == 0)
                        {
                            if (currentMenuItem is MainMenu)
                            {
                                choseQuit = true;
                            }

                            MenuHistory.Pop();
                        }
                        else
                        {
                            MenuItem chosenItem = currentMenuItem.SubMenuItems[itemSelectedIndex - 1];
                            MenuHistory.Push(chosenItem);
                        }
                    }
                }
                catch (ArgumentOutOfRangeException)
                {
                    Screen.ShowErrorMessage(eExceptionType.ValueOutOfBounds);
                    UserInput.AwaitProgression();
                }
                catch (ArgumentException)
                {
                    Screen.ShowErrorMessage(eExceptionType.Parsing);
                    UserInput.AwaitProgression();
                }
                catch (Exception e)
                {
                    Screen.Print("unknown exception: " + e.Message);
                    UserInput.AwaitProgression();
                }

                Screen.ClearScreen();
            }
            while (!choseQuit);

            Screen.Print("Goodbye!");
            UserInput.AwaitProgression();
        }

        /// <summary>
        /// Validate that the index given is within lower and upper bounds (inclusive)
        /// </summary>
        /// <param name="i_ItemSelectedIndex"></param>
        /// <param name="i_LowerBound"></param>
        /// <param name="i_UpperBound"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        internal static void Authenticate(int i_ItemSelectedIndex, int i_LowerBound, int i_UpperBound)
        {
            if (i_ItemSelectedIndex < i_LowerBound || i_ItemSelectedIndex > i_UpperBound)
            {
                throw new ArgumentOutOfRangeException();
            }
        }
    }
}
