using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOI4_Tools.Model
{
    public class Units
    {
        public Units()
        {
            string[] infantry = FileHandler.LoadFile("infantry.txt");
            int infantryStart;
            int infantryEnd;
            int bicycleBattalionStart;
            int bicycleBattalionEnd;

            bool isCheckingInfantry = false;
            bool isCheckingbicycleBattalion = false;

            for (int i = 0;i < infantry.Length;i++)
            {
                if(infantry[i].Contains("infantry = {"))
                {
                    infantryStart = i;
                    isCheckingInfantry = true;
                }
                if (isCheckingInfantry && infantry[i].Equals("\t}"))
                {
                    infantryEnd = i;
                    isCheckingInfantry = false;
                }
                if (infantry[i].Contains("bicycle_battalion = {"))
                {
                    bicycleBattalionStart = i;
                    isCheckingbicycleBattalion = true;
                }
                if (isCheckingbicycleBattalion && infantry[i].Equals("\t}"))
                {
                    bicycleBattalionEnd = i;
                    isCheckingbicycleBattalion = false;
                }
            }

            int x = 0;
        }
    }
}
