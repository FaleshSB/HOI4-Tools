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
        public int trainingTime;
        public int maxOrganisation;
        public int combatWidth;
        public int suppression;
        public int infantryEquipment;
        public int supportEquipment;

        public float weight;
        public float supplyConsumption;
        public float maxSpeed;
        public float breakthrough;

        public bool canBeParachuted = false;

        public Dictionary<TerrainType, TerrainStats> terrainStats = new Dictionary<TerrainType, TerrainStats>();
    }
}
