using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOI4_Tools.Model
{
    class Equipment
    {
        // When adding equipment stats make sure they are also added to places like Division.CalculateStats()
        public int year = 0;

        public float maximumSpeed = 0;
        public float defense = 0;
        public float breakthrough = 0;
        public float armorValue = 0;
        public float reliability = 0;
        public float hardness = 0;
        public float softAttack = 0;
        public float hardAttack = 0;
        public float apAttack = 0;
        public float airAttack = 0;
        public float buildCostIc = 0;

        public Equipment GetClone()
        {
            return (Equipment)this.MemberwiseClone();
        }
    }
}
