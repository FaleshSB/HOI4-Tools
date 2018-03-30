using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOI4_Tools.Model
{
    class Unit
    {
        public UnitName unitName;
        public UnitType unitType;
        public UnitIcon unitIcon;

        public int manpower;
        public int maxStrength;
        public int trainingTime;
        public float weight;
        public float defaultMorale;
        public int maxOrganisation;
        public int combatWidth;
        public float supplyConsumption;
    }
}
