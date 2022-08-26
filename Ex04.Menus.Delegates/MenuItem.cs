using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex04.Menus.Delegates
{
    internal class MenuItem<TEnum> where TEnum : Enum
    {
        private const string k_Exit = "Exit";
        private const string k_Back = "Back";
        private const string k_LinedisplayFormt = "{0} ==> {1}";
        private TEnum m_EnunVal;
        //private static readonly Dictionary<string, MenuItem> k_MenuItems = new Dictionary<string, MenuItem>();
        private string m_Title;

        public string Title
        {
            get { return m_Title; }
            set { m_Title = value; }
        }
        public bool IsFinalItem
        {
            get
            {
                //return !(m_SubmenusItems.Count > 0);
            }
        }
        
        //public List<MenuItem> m_SubmenusItems;
        private readonly bool r_IsMainMenu;

        public MenuItem(string i_Title, List<MenuItem> i_SubmenusItems)
        {
            Title = i_Title;
            //m_SubmenusItems = i_SubmenusItems;
        }


        public override string ToString()
        {
            //Enum.GetName(typeof(Days), value);

            //return string.Format(k_LinedisplayFormt, (int)m_EnunVal, m_EnunVal);
        }
    }
}
