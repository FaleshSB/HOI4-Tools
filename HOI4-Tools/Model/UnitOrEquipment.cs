using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOI4_Tools.Model
{
    class UnitOrEquipment
    {
        // When adding equipment stats make sure they are also added to places like Division.CalculateStats()
        public float year = 0;

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
        public float manpower = 0;
        public float trainingTime = 0;
        public float maxOrganisation = 0;
        public float combatWidth = 0;
        public float suppression = 0;
        public float infantryEquipment = 0;
        public float supportEquipment = 0;
        public float motorizedEquipment = 0;
        public float mechanizedEquipment = 0;

        public float weight = 0;
        public float maxStrength = 0;
        public float supplyConsumption = 0;
        public float maxSpeed = 0;

        public bool canBeParachuted = false;

        public Dictionary<TerrainType, TerrainStats> terrainStats = new Dictionary<TerrainType, TerrainStats>();

        public UnitOrEquipment GetClone()
        {
            UnitOrEquipment clone = (UnitOrEquipment)this.MemberwiseClone();
            foreach (KeyValuePair<TerrainType, TerrainStats> terrainStst in terrainStats)
            {
                clone.terrainStats[terrainStst.Key] = terrainStst.Value.GetClone();
            }
            return clone;
        }
    }
}
