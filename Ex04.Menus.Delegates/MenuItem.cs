using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex04.Menus.Delegates
{
    public delegate void SelectItemHandler(MenuItem i_MenuItem);

    public class MenuItem
    {
        private MainMenu m_ParentMenu;

        public event SelectItemHandler SelectItemOccured;

        private string m_Title;

        // Properties:
        public string Title
        {
            get { return m_Title; }
            set { m_Title = value; }
        }

        public MainMenu ParentMenu
        {
            get { return m_ParentMenu; }
            set { m_ParentMenu = value; }
        }

        public bool IsRoot
        {
            get { return m_ParentMenu == null; }
        }

        public MenuItem(string i_Title)
        {
            m_Title = i_Title;
            m_ParentMenu = null;
        }

        public MenuItem(string i_Title, MainMenu i_ParentMenu)
        {
            m_Title = i_Title;
            m_ParentMenu = i_ParentMenu;
        }

        protected virtual void OnSelectItem(MenuItem i_SelectItem)
        {
            if(SelectItemOccured != null)
            {
                Screen.ClearScreen();
                SelectItemOccured(i_SelectItem);
            }
        }

        internal void SelectItem()
        {
            OnSelectItem(this);
        }
    }
}
