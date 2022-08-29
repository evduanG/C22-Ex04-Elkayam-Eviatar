using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex04.Menus.Delegates
{
    internal delegate void ChoiceInvoker(MenuItem i_MenuItem);

    internal class MenuItem
    {
        private const bool v_IsFinalItem = true;
        private const string k_LineDisplayFormt = "{0} ==> {1}";

        // Member
        private byte m_Value;
        private string m_Title;

        public event ChoiceInvoker m_ChoiceInvoker;

        public string Title
        {
            get { return m_Title; }
            set { m_Title = value; }
        }
        public virtual bool IsFinalItem
        {
            get
            {
                return v_IsFinalItem;
            }
        }
        public byte Value
        {
            get { return m_Value; }
            set { m_Value = value; }
        }


        public MenuItem(string i_Title, byte i_Value, MenuItem i_Parent)
        {
            Title = i_Title;
            m_Value = i_Value; 
        }

        public string Show()
        {
            return string.Format(k_LineDisplayFormt, m_Value, m_Title);
        }

        protected virtual void OnClicked()
        {
            // lets tell the form that I was clicked:
            if (m_ChoiceInvoker != null)
            {
                m_ChoiceInvoker.Invoke(this);
            }
        }

        internal static bool GetvaluesAndNamesFormEnum(Type i_EnumType, out string[] o_Name, out int[] o_values)
        {
            bool isSucceed = false;

            if (i_EnumType.IsEnum)
            {
                o_Name = Enum.GetNames(i_EnumType);
                o_values = (int[])Enum.GetValues(i_EnumType);
            }
            else
            {
                o_Name = null;
                o_values = null;
            }

            return isSucceed;
        }
    }
}
