using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex04.Menus.Delegates
{

    public class MainMenu : MenuItem
    {
        private const byte k_ReturnItemKey = 0;
        private const string k_BackTitle = "Back";
        private const string k_ExitTitle = "Exit";
        private readonly Dictionary<byte, MenuItem> r_SubMenuItems;

        public MainMenu(string i_Title)
          : base(i_Title)
        {
            r_SubMenuItems = new Dictionary<byte, MenuItem>();
        }

        public MenuItem this[byte i_Index] {
            get
            {
                bool isExist = r_SubMenuItems.TryGetValue(i_Index, out MenuItem o_menuItem);
                if(!isExist)
                {
                    throw new FormatException("Member does not exist");
                }

                return o_menuItem;
            }

            set
            {
                r_SubMenuItems[i_Index] = value;
            }
        }

        public MenuItem this[Enum i_EnumInex]
        {
            get
            {
                byte index = (byte)i_EnumInex.GetTypeCode();
                return this[index];
            }

            set
            {
                byte index = (byte)i_EnumInex.GetTypeCode();
                this[index] = value;
            }
        }

        public void AddSubMenue(MainMenu subMenueVersionAndSpaces)
        {
            byte index = (byte)this.r_SubMenuItems.Count;
            index++;
            subMenueVersionAndSpaces.ParentMenu = this;
            this.r_SubMenuItems.Add(index, subMenueVersionAndSpaces);
        }

        public void AddReturnItem()
        {
            if(!r_SubMenuItems.ContainsKey(k_ReturnItemKey))
            {
                string titelOfReturn = k_BackTitle;
                if (IsRoot)
                {
                    titelOfReturn = k_ExitTitle;
                }

                r_SubMenuItems.Add(k_ReturnItemKey, new MenuItem(titelOfReturn, this));
            }
        }

        public void CreatMenuItemFromEnum(Type i_EnumType)
        {
            if(!i_EnumType.IsEnum)
            {
                throw new ArgumentException("Wrong front the type must be enum");
            }

            string[] names = Enum.GetNames(i_EnumType);
            byte[] indexKey = (byte[])Enum.GetValues(i_EnumType);
            for (int i = 0; i < names.Length; i++)
            {
                if (indexKey[i] == k_ReturnItemKey)
                {
                    removeAllFromDictionaryByEnum(i_EnumType);
                    throw new FormatException(string.Format("the enum {0} is invalid, The value {1} cannot be part of the elements in {0}", nameof(i_EnumType), k_ReturnItemKey));
                }

                r_SubMenuItems.Add(indexKey[i], new MenuItem(names[i]));
            }
        }

        private void removeAllFromDictionaryByEnum(Type i_EnumType)
        {
            if (!i_EnumType.IsEnum)
            {
                throw new ArgumentException("Wrong front the type must be enum");
            }

            byte[] indexKey = (byte[])Enum.GetValues(i_EnumType);
            foreach (byte key in indexKey)
            {
                r_SubMenuItems.Remove(key);
            }
        }

        protected virtual void OnSelectItem(MenuItem item)
        {
            
        }
    }
}
