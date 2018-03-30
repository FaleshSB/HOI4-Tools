using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HOI4_Tools.View;

namespace HOI4_Tools.Model
{
    static class Pages
    {
        public static Dictionary<PageName, DefaultPage> pages = new Dictionary<PageName, DefaultPage>();

        static Pages()
        {
            pages[PageName.DivisionDesigner] = new DivisionDesignerPage();
        }
    }
}