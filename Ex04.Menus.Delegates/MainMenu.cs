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
        private const string k_ChoiceQuestion = "Enter your request ({0} to {1} or `0` to {2} )";
        private const string k_LineDisplayFormt = "{0} ==> {1}";
        private const byte k_ReturnItemKey = 0;
        private const string k_BackTitle = "Back";
        private const string k_ExitTitle = "Exit";
        private static MainMenu s_CurrentMenuLevel = null;
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
                bool isExist = r_SubMenuItems.TryGetValue(i_Index, out MenuItem o_MenuItem);

                if(!isExist)
                {
                    throw new FormatException("Member does not exist");
                }

                return o_MenuItem;
            }

            set
            {
                r_SubMenuItems[i_Index] = value;
            }
        }

        public void Show()
        {
            s_CurrentMenuLevel = this;

            while (s_CurrentMenuLevel != null)
            {
                Screen.ShowMenuPrompt(s_CurrentMenuLevel);
                byte userChoice = getUserChoice();
                bool isExist = userChoice == k_ReturnItemKey;

                if (isExist)
                {
                    s_CurrentMenuLevel = s_CurrentMenuLevel.ParentMenu;
                }
                else
                {
                    s_CurrentMenuLevel[userChoice].SelectItem();
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

        public void AddMenuItem(MenuItem i_SubMenueVersionAndSpaces)
        {
            byte index = (byte)r_SubMenuItems.Count;
            index++;
            i_SubMenueVersionAndSpaces.ParentMenu = this;

            while(r_SubMenuItems.ContainsKey(index))
            {
                index++;
            }

            r_SubMenuItems.Add(index, i_SubMenueVersionAndSpaces);
            this[index].SelectItemOccured += OnSelectItem;
        }

        public override string ToString()
        {
            byte minOption = byte.MaxValue;
            byte maxOption = byte.MinValue;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(getTitleToString());
            sb.AppendLine(k_LineSeparatorFormt);

            foreach (KeyValuePair<byte, MenuItem> item in r_SubMenuItems)
            {
                minOption = Math.Min(minOption, item.Key);
                maxOption = Math.Max(maxOption, item.Key);
                sb.AppendLine(string.Format(k_LineDisplayFormt, item.Key, item.Value.Title));
            }

            sb.AppendLine(getReturnOptionToString());
            sb.AppendLine(k_LineSeparatorFormt);
            sb.AppendLine(getSelectionToSting(minOption, maxOption));

            return sb.ToString();
        }

        private string getTitleToString()
        {
            return string.Format(k_TitleDisplayFormt, Title);
        }

        private string getSelectionToSting(byte i_MinOption, byte i_MaxOption)
        {
            return string.Format(k_ChoiceQuestion, i_MinOption, i_MaxOption, getBackStr());
        }

        private string getReturnOptionToString()
        {
            return string.Format(k_LineDisplayFormt, k_ReturnItemKey, getBackStr());
        }

        private string getBackStr()
        {
            return IsRoot ? k_ExitTitle : k_BackTitle;
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

        protected override void OnSelectItem(MenuItem i_SelectItem)
        {
            if (i_SelectItem is MainMenu mainMenuToSwictTo)
            {
                s_CurrentMenuLevel = mainMenuToSwictTo;
            }
        }
    }
}
