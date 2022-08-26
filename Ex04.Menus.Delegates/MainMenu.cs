using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex04.Menus.Delegates
{
    internal class MainMenu
    {
        public const string k_Version = "Version: 22.3.4.8650";
        public MainMenu()
        {

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

    }
}
