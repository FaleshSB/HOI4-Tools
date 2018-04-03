using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOI4_Tools.Model
{
    public class Division
    {
        public int displayOrder;
        public int uniqueDivisionId;

        public float maxSpeed;
        public int maxStrength;
        public int maxOrganisation;
        // recovery rate
        // reconnaissance
        public int suppression;
        public float weight;
        public float supplyConsumption;
        public float reliability;
        // Trickleback
        // exp loss
        public float softAttack;
        public float hardAttack;
        public float airAttack;
        public int defense;
        public float breakthrough;
        public int armorValue;
        // piercing
        // initiative
        // entrenchment
        // eq capture ratio
        public int combatWidth;
        public int manpower;
        public int trainingTime;
        // artillery eq
        public int infantryEquipment;
        public int mechanizedEquipment;
        public int motorizedEquipment;
        public int supportEquipment;


        public float hardness;
        public float apAttack;
        public float buildCostIc;

        private Dictionary<UnitName, int> units = new Dictionary<UnitName, int>();
        public int year = 1918;

        public void AddUnit(UnitName unitName)
        {
            int value;
            if (units.TryGetValue(unitName, out value))
            {
                units[unitName] = value + 1;
            }
            else
            {
                units[unitName] = 1;
            }

            CalculateStats();
        }

        private void CalculateStats()
        {
            int totalNumberOfUnits = 0;

            foreach (KeyValuePair<UnitName, int> unitNameAndNumber in units)
            {
                Unit unit = UnitsAndEquipment.units[unitNameAndNumber.Key];
                Equipment equipmentUsed = new Equipment();
                int numberOfUnits = unitNameAndNumber.Value;
                totalNumberOfUnits += numberOfUnits;
                int highest = 0;
                foreach (KeyValuePair<int, Equipment> yearAndEquipment in UnitsAndEquipment.equipment[unitNameAndNumber.Key])
                {
                    if(yearAndEquipment.Key <= year && yearAndEquipment.Key > highest)
                    {
                        equipmentUsed = yearAndEquipment.Value;
                    }
                }

                maxSpeed = (unit.maxSpeed > equipmentUsed.maximumSpeed) ? unit.maxSpeed : equipmentUsed.maximumSpeed;
                maxStrength = unit.maxStrength * numberOfUnits;
                maxOrganisation = unit.maxOrganisation * numberOfUnits;
                suppression = unit.suppression * numberOfUnits;
                weight = unit.weight * numberOfUnits;
                supplyConsumption = unit.supplyConsumption * numberOfUnits;
                // Relyability
                softAttack = equipmentUsed.softAttack * numberOfUnits;
                hardAttack = equipmentUsed.hardAttack * numberOfUnits;
                airAttack = equipmentUsed.airAttack * numberOfUnits;
                defense = equipmentUsed.defense * numberOfUnits;
                breakthrough = (unit.breakthrough + equipmentUsed.breakthrough) * numberOfUnits;
                armorValue = equipmentUsed.armorValue * numberOfUnits;
                // piercing
                // initiative
                // entrenchment
                // eq capture ratio
                combatWidth = unit.combatWidth * numberOfUnits;
                manpower = unit.manpower * numberOfUnits;
                trainingTime = unit.trainingTime * numberOfUnits;
                // artillery eq
                infantryEquipment = unit.infantryEquipment * numberOfUnits;
                mechanizedEquipment = unit.mechanizedEquipment * numberOfUnits;
                motorizedEquipment = unit.motorizedEquipment * numberOfUnits;
                supportEquipment = unit.supportEquipment * numberOfUnits;


                /*
                public float hardness;
                public float apAttack;
                public float buildCostIc;*/
            }

            maxOrganisation = maxOrganisation / totalNumberOfUnits;
        }
    }
}
