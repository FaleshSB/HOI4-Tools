using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOI4_Tools.Model
{
    public class Division
    {
        public Dictionary<int, Dictionary<UnitName, int>> unitsInDivision = new Dictionary<int, Dictionary<UnitName, int>>();

        public int displayOrder;

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

        public int year = 1918;

        public void AddUnit(UnitName unitName, int column)
        {
            if(unitsInDivision.ContainsKey(column) == false)
            {
                unitsInDivision[column] = new Dictionary<UnitName, int>();
            }
            int value;
            if (unitsInDivision[column].TryGetValue(unitName, out value))
            {
                unitsInDivision[column][unitName] = value + 1;
            }
            else
            {
                unitsInDivision[column][unitName] = 1;
            }

            CalculateStats();
        }

        private void CalculateStats()
        {
            maxSpeed = 0;
            maxStrength = 0;
            maxOrganisation = 0;
            suppression = 0;
            weight = 0;
            supplyConsumption = 0;
            // Relyability
            softAttack = 0;
            hardAttack = 0;
            airAttack = 0;
            defense = 0;
            breakthrough = 0;
            armorValue = 0;
            // piercing
            // initiative
            // entrenchment
            // eq capture ratio
            combatWidth = 0;
            manpower = 0;
            trainingTime = 0;
            // artillery eq
            infantryEquipment = 0;
            mechanizedEquipment = 0;
            motorizedEquipment = 0;
            supportEquipment = 0;

            int totalNumberOfUnits = 0;

            foreach (KeyValuePair<int, Dictionary<UnitName, int>> columnAndUnitData in unitsInDivision)
            {
                foreach (KeyValuePair<UnitName, int> unitNameAndNumber in columnAndUnitData.Value)
                {
                    Unit unit = UnitsAndEquipment.units[unitNameAndNumber.Key];
                    Equipment equipmentUsed = new Equipment();
                    int numberOfUnits = unitNameAndNumber.Value;
                    totalNumberOfUnits += numberOfUnits;
                    int highest = 0;
                    foreach (KeyValuePair<int, Equipment> yearAndEquipment in UnitsAndEquipment.equipment[unitNameAndNumber.Key])
                    {
                        if (yearAndEquipment.Key <= year && yearAndEquipment.Key > highest)
                        {
                            equipmentUsed = yearAndEquipment.Value;
                        }
                    }

                    float newMaxSpeed = (unit.maxSpeed > equipmentUsed.maximumSpeed) ? unit.maxSpeed : equipmentUsed.maximumSpeed;
                    maxSpeed = (maxSpeed == 0 || (maxSpeed > newMaxSpeed)) ? newMaxSpeed : maxSpeed;
                    maxStrength += unit.maxStrength * numberOfUnits;
                    maxOrganisation += unit.maxOrganisation * numberOfUnits;
                    suppression += unit.suppression * numberOfUnits;
                    weight += unit.weight * numberOfUnits;
                    supplyConsumption += unit.supplyConsumption * numberOfUnits;
                    // Relyability
                    softAttack += equipmentUsed.softAttack * numberOfUnits;
                    hardAttack += equipmentUsed.hardAttack * numberOfUnits;
                    airAttack += equipmentUsed.airAttack * numberOfUnits;
                    defense += equipmentUsed.defense * numberOfUnits;
                    breakthrough += (unit.breakthrough + equipmentUsed.breakthrough) * numberOfUnits;
                    armorValue += equipmentUsed.armorValue * numberOfUnits;
                    // piercing
                    // initiative
                    // entrenchment
                    // eq capture ratio
                    combatWidth += unit.combatWidth * numberOfUnits;
                    manpower += unit.manpower * numberOfUnits;
                    trainingTime += unit.trainingTime * numberOfUnits;
                    // artillery eq
                    infantryEquipment += unit.infantryEquipment * numberOfUnits;
                    mechanizedEquipment += unit.mechanizedEquipment * numberOfUnits;
                    motorizedEquipment += unit.motorizedEquipment * numberOfUnits;
                    supportEquipment += unit.supportEquipment * numberOfUnits;


                    /*
                    public float hardness;
                    public float apAttack;
                    public float buildCostIc;*/
                }
            }
            maxOrganisation = maxOrganisation / totalNumberOfUnits;
        }
    }
}
