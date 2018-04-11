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

        public UnitOrEquipment GetCombinedClone(UnitOrEquipment b)
        {
            UnitOrEquipment combined = GetClone();

            combined.year = (combined.year > b.year) ? combined.year : b.year;

            // TODO some of these need to be averaged or otherwise changed
            combined.maxSpeed = (combined.maxSpeed == 0 || b.maxSpeed > combined.maxSpeed) ? b.maxSpeed : combined.maxSpeed;
            combined.defense += b.defense;
            combined.breakthrough += b.breakthrough;
            combined.armorValue += b.armorValue;
            combined.reliability += b.reliability;
            combined.hardness += b.hardness;
            combined.softAttack += b.softAttack;
            combined.hardAttack += b.hardAttack;
            combined.apAttack += b.apAttack;
            combined.airAttack += b.airAttack;
            combined.buildCostIc += b.buildCostIc;
            combined.manpower += b.manpower;
            combined.trainingTime += b.trainingTime;
            combined.maxOrganisation += b.maxOrganisation;
            combined.combatWidth += b.combatWidth;
            combined.suppression += b.suppression;
            combined.infantryEquipment += b.infantryEquipment;
            combined.supportEquipment += b.supportEquipment;
            combined.motorizedEquipment += b.motorizedEquipment;
            combined.mechanizedEquipment += b.mechanizedEquipment;

            combined.weight += b.weight;
            combined.maxStrength += b.maxStrength;
            combined.supplyConsumption += b.supplyConsumption;
            combined.maxSpeed += b.maxSpeed;

            combined.canBeParachuted = (combined.canBeParachuted && b.canBeParachuted) ? true : false;

            foreach (KeyValuePair<TerrainType, TerrainStats> terrainStst in b.terrainStats)
            {
                combined.terrainStats[terrainStst.Key].attack += terrainStst.Value.attack;
                combined.terrainStats[terrainStst.Key].defence += terrainStst.Value.defence;
                combined.terrainStats[terrainStst.Key].movement += terrainStst.Value.movement;
            }

            return combined;
        }
    }
}
