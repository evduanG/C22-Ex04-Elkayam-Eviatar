using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex04.Menus.Delegates
{
    internal class MainMenu : MenuItem
    {
        private const string k_Exit = "Exit";
        private const string k_Back = "Back";
        private const string k_TitleDisplayFormt = "**{0}**";
        private const string k_LineSeparatorFormt = "--------------------------";
        private const string k_ChoiceQuestion = "Enter your requst ({0} to {1} or `0` to {2}";

        private readonly Dictionary<byte, MenuItem> r_MenuItems;


        public const string k_Version = "Version: 22.3.4.8650";
        public MainMenu(Enum i_Enum, MenuItem i_MenuItem)
            :base(i_MenuItem.Title, i_MenuItem.Value, i_MenuItem.Parent)
        {
            string[] names = Enum.GetNames(i_Enum.GetType());
            byte[] index = (byte[])Enum.GetValues(i_Enum.GetType());
            r_MenuItems = new Dictionary<byte, MenuItem>();
            for(int i = 0; i < names.Length; i++)
            {
                r_MenuItems.Add(index[i], new MenuItem(names[i], index[i], this));
            }
        }

        public override bool IsFinalItem
        {
            get
            {
                return !(r_MenuItems.Count > 0);
            }
        }

        public string Show()
        {
            StringBuilder sb = new StringBuilder(string.Format(k_TitleDisplayFormt, Title));
            byte minCosi = 1;
            byte maxCosi = 1;
            string backStr = k_Back;
            sb.AppendLine(k_LineSeparatorFormt);

            foreach (MenuItem item in r_MenuItems.Values)
            {
                minCosi = Math.Min(minCosi, item.Value);
                maxCosi = Math.Max(maxCosi, item.Value);
                sb.AppendLine(item.Show());
            }

            sb.AppendLine(k_LineSeparatorFormt);

            if (IsRoot)
            {
                backStr = k_Exit;
            }
            sb.AppendLine(string.Format(k_ChoiceQuestion, minCosi, minCosi, backStr));

            return sb.ToString();
        }

        public int CountSpaceInText(string i_TextForInspection)
        {
            int spaceInText = 0;

            return spaceInText;
        }

        public string ShowVersion()
        {
            return k_Version;
        }

        public string ShowTime()
        {
            DateTime now = DateTime.Now;
            return string.Format("{ 1:g}", now.TimeOfDay);
        }

        public string ShowDate()
        {
            DateTime now = DateTime.Now;
            return string.Format("{ 1:g}", now.TimeOfDay);
        }
        
        protected virtual void Back_evant()
        {
            if(IsRoot)
            {
                Exit_evant();
            }
            else
            {
                Back_evant(Parent);
            }
        }

    }
}
