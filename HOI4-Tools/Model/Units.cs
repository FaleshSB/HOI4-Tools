using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOI4_Tools.Model
{
    public class Units
    {
        string[] infantry;

        int infantryStart;
        int bicycleBattalionStart;
        int marineStart;
        int mountaineersStart;
        int paratrooperStart;
        int motorizedStart;
        int mechanizedStart;

        int infantryEnd;
        int bicycleBattalionEnd;
        int marineEnd;
        int mountaineersEnd;
        int paratrooperEnd;
        int motorizedEnd;
        int mechanizedEnd;

        bool isCheckingInfantry = false;
        bool isCheckingBicycleBattalion = false;
        bool isCheckingMarine = false;
        bool isCheckingMountaineers = false;
        bool isCheckingParatrooper = false;
        bool isCheckingMotorized = false;
        bool isCheckingMechanized = false;

        public Units()
        {
            GetData();
            int i = 0;
        }

        private void GetData()
        {
            infantry = FileHandler.LoadFile("infantry.txt");
            GetInfantryLocations();
            GetInfantryStats();
        }

        private void GetInfantryStats()
        {
            Unit basicInfantry = new Unit();
            for (int i = 0; i < infantry.Length; i++)
            {
                if (infantry[i].Contains("infantry = {"))
                {

                }
        }

        private void GetInfantryLocations()
        {
            int i;
            for (i = 0; i < infantry.Length; i++)
            {
                if (infantry[i].Contains("infantry = {"))
                {
                    infantryStart = i;
                    GetEnds(i);
                    isCheckingInfantry = true;
                }
                else if (infantry[i].Contains("bicycle_battalion = {"))
                {
                    bicycleBattalionStart = i;
                    GetEnds(i);
                    isCheckingBicycleBattalion = true;
                }
                else if (infantry[i].Contains("marine = {"))
                {
                    marineStart = i;
                    GetEnds(i);
                    isCheckingMarine = true;
                }
                else if (infantry[i].Contains("mountaineers = {"))
                {
                    mountaineersStart = i;
                    GetEnds(i);
                    isCheckingMountaineers = true;
                }
                else if (infantry[i].Contains("paratrooper = {"))
                {
                    paratrooperStart = i;
                    GetEnds(i);
                    isCheckingParatrooper = true;
                }
                else if (infantry[i].Contains("motorized = {"))
                {
                    motorizedStart = i;
                    GetEnds(i);
                    isCheckingMotorized = true;
                }
                else if (infantry[i].Contains("mechanized = {"))
                {
                    mechanizedStart = i;
                    GetEnds(i);
                    isCheckingMechanized = true;
                }
            }
            GetEnds(i);
        }

        private void GetEnds(int location)
        {
            if(isCheckingInfantry)
            {
                infantryEnd = location - 1;
                isCheckingInfantry = false;
            }
            else if (isCheckingBicycleBattalion)
            {
                bicycleBattalionEnd = location - 1;
                isCheckingBicycleBattalion = false;
            }
            else if (isCheckingMarine)
            {
                marineEnd = location - 1;
                isCheckingMarine = false;
            }
            else if (isCheckingMountaineers)
            {
                mountaineersEnd = location - 1;
                isCheckingMountaineers = false;
            }
            else if (isCheckingParatrooper)
            {
                paratrooperEnd = location - 1;
                isCheckingParatrooper = false;
            }
            else if (isCheckingMotorized)
            {
                motorizedEnd = location - 1;
                isCheckingMotorized = false;
            }
            else if (isCheckingMechanized)
            {
                mechanizedEnd = location - 1;
                isCheckingMechanized = false;
            }
        }
    }
}
