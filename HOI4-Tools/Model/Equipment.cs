using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOI4_Tools.Model
{
    class Equipment
    {
        public int year;
        public int maximumSpeed;
        public int defense;
        public int breakthrough;
        public int armorValue;

        public float reliability;
        public float hardness;
        public float softAttack;
        public float hardAttack;
        public float apAttack;
        public float airAttack;
        public float buildCostIc;

        public Equipment GetClone()
        {
            return (Equipment)this.MemberwiseClone();
        }
    }
}
