using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex04.Menus.Delegates
{

    public class MainMenu : MenuItem
    {
        private const string k_TitleDisplayFormt = "**{0}**";
        private const string k_LineSeparatorFormt = "--------------------------";
        private const string k_ChoiceQuestion = "Enter your requst ({0} to {1} or `0` to {2} )";
        private const string k_LineDisplayFormt = "{0} ==> {1}";


        private static MainMenu s_CorntMenuLevel = null;
        private const byte k_ReturnItemKey = 0;
        private const string k_BackTitle = "Back";
        private const string k_ExitTitle = "Exit";
        private readonly Dictionary<byte, MenuItem> r_SubMenuItems;

        public MainMenu(string i_Title)
          : base(i_Title)
        {
            r_SubMenuItems = new Dictionary<byte, MenuItem>();
        }

        public MenuItem this[byte i_Index]
        {
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

        public void Show()
        {
            s_CorntMenuLevel = this;
            while (s_CorntMenuLevel != null)
            {
                Screen.ShowMenuPrompt(s_CorntMenuLevel);
                byte userChoice = getUserChoice();
                if (userChoice == k_ReturnItemKey)
                {
                    s_CorntMenuLevel = s_CorntMenuLevel.ParentMenu;
                }
                else
                {
                    s_CorntMenuLevel[userChoice].SelectItem();
                }
            }
        }

        private byte getUserChoice()
        {
            byte userChoice = 0;
            bool isChoiceValid = false;

            while (!isChoiceValid)
            {
                try
                {
                    userChoice = UserInput.ReadSelection();
                    isChoiceValid = true;
                }
                catch (Exception e)
                {
                    Screen.ShowEror(e);
                }
            }

            return userChoice;
        }

        public MenuItem this[Enum i_EnumInex]
        {
            get
            {
                string strInx = Enum.Format(i_EnumInex.GetType(), i_EnumInex, "x");
                byte.TryParse(strInx, out byte o_Indx);
                return this[o_Indx];
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
            Console.WriteLine(index);
            index++;
            Console.WriteLine(index);

            subMenueVersionAndSpaces.ParentMenu = this;
            while(r_SubMenuItems.ContainsKey(index))
            {
                index++;
            }

            this.r_SubMenuItems.Add(index, subMenueVersionAndSpaces);
            this[index].SelectItemOccured += OnSelectItem;
        }

        internal string CreateMenuStrToPrint()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(string.Format(k_TitleDisplayFormt, Title));
            byte minCosi = byte.MaxValue;
            byte maxCosi = byte.MinValue;
            string backStr = k_BackTitle;
            sb.AppendLine(k_LineSeparatorFormt);

            foreach (KeyValuePair<byte, MenuItem> item in r_SubMenuItems)
            {
                minCosi = Math.Min(minCosi, item.Key);
                maxCosi = Math.Max(maxCosi, item.Key);
                sb.AppendLine(string.Format(k_LineDisplayFormt, item.Key, item.Value.Title));
            }

            if (IsRoot)
            {
                backStr = k_ExitTitle;
            }
            sb.AppendLine(string.Format(k_LineDisplayFormt, k_ReturnItemKey, backStr));
            sb.AppendLine(k_LineSeparatorFormt);

            sb.AppendLine(string.Format(k_ChoiceQuestion, minCosi, minCosi, backStr));

            return sb.ToString();
        }

        public void CreatMenuItemFromEnum(Type i_EnumType)
        {
            if(!i_EnumType.IsEnum)
            {
                throw new ArgumentException("Wrong front the type must be enum");
            }

            string[] names = Enum.GetNames(i_EnumType);
            int[] indexKey = (int[])Enum.GetValues(i_EnumType);
            for (int i = 0; i < names.Length; i++)
            {
                if (indexKey[i] == k_ReturnItemKey)
                {
                    removeAllFromDictionaryByEnum(i_EnumType);
                    throw new FormatException(string.Format("the enum {0} is invalid, The value {1} cannot be part of the elements in {0}", nameof(i_EnumType), k_ReturnItemKey));
                }

                Console.WriteLine((byte)indexKey[i]);
                string nameModify = addSpacesBeforeCapitalLetter(names[i]);
                r_SubMenuItems.Add((byte)indexKey[i], new MenuItem(nameModify, this));
            }
        }

        private static string addSpacesBeforeCapitalLetter(string i_StrToModify)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(i_StrToModify[0]);
            for(int i = 1; i < i_StrToModify.Length; i++)
            {
                bool isLetterUpper = char.IsUpper(i_StrToModify[i]);
                if (isLetterUpper)
                {
                    sb.Append(' ');
                }

                sb.Append(i_StrToModify[i]);
            }

            return sb.ToString();
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
            MainMenu mainMenuToSwictTo = item as MainMenu;
            if (mainMenuToSwictTo != null)
            {
                s_CorntMenuLevel = mainMenuToSwictTo;
            }
        }
    }
}
