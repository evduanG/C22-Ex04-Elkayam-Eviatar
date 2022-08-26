namespace Ex04.Menus.Interfaces
{
    public interface IMenuItemSelectedNotifier
    {
        void AttachObserver(IMenuItemSelectedObserver i_MenuItemSelectedObserver);

        void DetachObserver(IMenuItemSelectedObserver i_MenuItemSelectedObserver);

        void NotifiyObserver(MenuItem i_MenuItem);
    }
}
