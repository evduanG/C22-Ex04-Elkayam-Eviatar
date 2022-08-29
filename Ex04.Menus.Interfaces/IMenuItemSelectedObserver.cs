namespace Ex04.Menus.Interfaces
{
    public interface IMenuItemSelectedObserver
    {
        /// <summary>
        /// do when one of the item listened to are selected. either open another sub-menu or notify observer (parent-menu).
        /// </summary>
        /// <param name="i_MenuItem"></param>
        void MenuItem_Selected(MenuItem i_MenuItem);
    }
}
