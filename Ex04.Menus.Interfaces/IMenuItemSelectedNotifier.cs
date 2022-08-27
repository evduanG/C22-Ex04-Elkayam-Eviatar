namespace Ex04.Menus.Interfaces
{
    public interface IMenuItemSelectedNotifier
    {
        void AttachObserver(IMenuItemSelectedObserver i_MenuItemSelectedObserver);

        void DetachObserver(IMenuItemSelectedObserver i_MenuItemSelectedObserver);

        /// <summary>
        /// Notify observer (previous menu item) which menu item was selected
        /// </summary>
        void NotifiyObserver(MenuItem i_MenuItem);
    }
}
