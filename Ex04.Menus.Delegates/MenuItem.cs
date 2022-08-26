using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex04.Menus.Delegates
{
    //internal class MenuItem<TEnum> where TEnum : Enum
        //private TEnum m_EnunVal;
    internal class MenuItem
    {
        private const string k_Back = "Back";
        private const string k_LinedisplayFormt = "{0} ==> {1}";
        private byte m_Value;
        private readonly Dictionary<Enum, MenuItem> r_MenuItems;
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
                return !(r_MenuItems.Count > 0);
            }
        }
        
        private readonly bool r_IsMainMenu;

        public MenuItem(string i_Title, byte i_Value, bool i_IsMainMenu, Type i_EnumType,)
        {
            Title = i_Title;
            r_IsMainMenu = i_IsMainMenu;
            m_Value = i_Value; 
            string[] names = Enum.GetNames(i_EnumType);
            int[] values = (int[])Enum.GetValues(i_EnumType);

            foreach (string name in names)
            {
                
            }

        }


        public override string ToString()
        {
            //Enum.GetName(typeof(Days), value);

            return string.Format(k_LinedisplayFormt, m_Value, m_Title);
        }
    }
}
