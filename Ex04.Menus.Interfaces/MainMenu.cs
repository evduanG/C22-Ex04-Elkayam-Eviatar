using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex04.Menus.Interfaces
{
    public class MainMenu : MenuItem, IMenuItemSelectedObserver
    {
        private const int k_ExitSymbol = 0;

        // Constructor
        public MainMenu(string i_Title)
            : base(null, i_Title)
        {
        }

        // Methods as OBSERVER:
        void IMenuItemSelectedObserver.MenuItem_Selected(MenuItem item)
        {
            ((IMenuItemSelectedNotifier)this).NotifiyObserver(item);
        }

        // Methods as MainMenu

        /// <summary>
        /// Main loop for menu interface
        /// </summary>
        public void Show()
        {
            bool choseQuit = false;
            MenuItem currentMenuToShow = this;

            do
            {
                try
                {
                    DoWhenSelected(currentMenuToShow);

                    if (currentMenuToShow.HasSubMenus())
                    {
                        Screen.ShowMenuPrompt(currentMenuToShow, 1, currentMenuToShow.SubMenuItems.Count);
                        int itemSelectedIndex = UserInput.ReadSelection();
                        Authenticate(itemSelectedIndex, 0, currentMenuToShow.SubMenuItems.Count);

                        if (itemSelectedIndex == k_ExitSymbol)
                        {
                            if (currentMenuToShow is MainMenu)
                            {
                                choseQuit = true;
                            }
                            else
                            {
                                // go back a menu
                                currentMenuToShow = currentMenuToShow.ParentMenu;
                            }
                        }
                        else
                        {
                            // go forward
                            MenuItem chosenItem = currentMenuToShow.SubMenuItems[itemSelectedIndex - 1];
                            currentMenuToShow = chosenItem;
                        }
                    }
                    else
                    {
                        // go back a menu
                        currentMenuToShow = currentMenuToShow.ParentMenu;
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
