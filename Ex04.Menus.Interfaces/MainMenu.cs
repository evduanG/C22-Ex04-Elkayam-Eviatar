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
        private const int k_FirstMenuItemIndex = 1;
        private const string k_UnknownExceptionFormat = "unknown exception: {0}";

        // Constructor
        public MainMenu(string i_Title)
            : base(null, i_Title)
        {
        }

        // Methods as OBSERVER:
        void IMenuItemSelectedObserver.MenuItem_Selected(MenuItem i_SelectedItem)
        {
            ((IMenuItemSelectedNotifier)this).NotifiyObserver(i_SelectedItem);
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
                    OnSelected(currentMenuToShow);

                    if (currentMenuToShow.HasSubMenus)
                    {
                        int numOfSubMenus = currentMenuToShow.SubMenuItems.Count;
                        Screen.ShowMenuPrompt(currentMenuToShow, k_FirstMenuItemIndex, numOfSubMenus);
                        int itemSelectedIndex = UserInput.ReadSelection();
                        Authenticate(itemSelectedIndex, k_ExitSymbol, numOfSubMenus);
                        bool isUserChooseToExit = itemSelectedIndex == k_ExitSymbol;

                        if (isUserChooseToExit)
                        {
                            choseQuit = currentMenuToShow is MainMenu;

                            if (!choseQuit)
                            {
                                // go back a menu
                                currentMenuToShow = currentMenuToShow.ParentMenu;
                            }
                        }
                        else
                        {
                            // go forward
                            currentMenuToShow = currentMenuToShow.SubMenuItems[itemSelectedIndex - 1];
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
                    Screen.Print(string.Format(k_UnknownExceptionFormat, e.Message));
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
