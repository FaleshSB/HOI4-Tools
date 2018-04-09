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
        public float maxStrength;
        public float maxOrganisation;
        // recovery rate
        // reconnaissance
        public float suppression;
        public float weight;
        public float supplyConsumption;
        public float reliability;
        // Trickleback
        // exp loss
        public float softAttack;
        public float hardAttack;
        public float airAttack;
        public float defense;
        public float breakthrough;
        public float armorValue;
        // piercing
        // initiative
        // entrenchment
        // eq capture ratio
        public float combatWidth;
        public float manpower;
        public float trainingTime;
        // artillery eq
        public float infantryEquipment;
        public float mechanizedEquipment;
        public float motorizedEquipment;
        public float supportEquipment;


        public float hardness;
        public float apAttack;
        public float buildCostIc;

        public string maxSpeedDescription = "How quickly this unit can traverse terrain under optimal circumstances.";
        public string maxStrengthDescription = "HP represents how much damage this unit can suffer before it is destroyed.";
        public string maxOrganisationDescription = "Organization indicates combat readiness and how organized a unit is. A unit with no organization can't fight or move effectively.";
        // recovery rate
        // reconnaissance
        public string suppressionDescription = "Ability to suppress local resistance.";
        public string weightDescription = "How much the unit will weigh. Heavier units will require more transports to ship and perform invasions effectively.";
        public string supplyConsumptionDescription = "How much supply a unit consumes per day.";
        public string reliabilityDescription = "The lower the value the more likely the equipment is of suffering random failure, accidents or exploding in a fiery ball of death when lightly bumped.";
        // Trickleback
        // exp loss
        public string softAttackDescription = "How many attacks the unit can make versus enemies with low hardness.";
        public string hardAttackDescription = "How many attacks the unit can make versus enemies with high hardness.";
        public string airAttackDescription = "How much damage we can do against airplanes. High Air Attack also helps to counter enemy Air Superiority effects.";
        public string defenseDescription = "How many enemy attacks a unit can attempt to avoid while on the defensive, effectively allowing it to hold the line longer.";
        public string breakthroughDescription = "How many enemy attacks a unit can attempt to avoid while on the offensive, effectively allowing it to stay on the offense longer.";
        public string armorValueDescription = "Having Armor that is higher than the opponents Piercing value makes you take less damage and also makes it possible to perform more attacks in combat as the unit has more freedom of movement.";
        // piercing
        // initiative
        // entrenchment
        // eq capture ratio
        public string combatWidthDescription = "The size of the fighting unit. A unit needs to be able to fit into the total §HCombat Width§! of a battle to contribute to it.";
        public string manpowerDescription = "";
        public string trainingTimeDescription = "";
        // artillery eq
        public string infantryEquipmentDescription = "";
        public string mechanizedEquipmentDescription = "";
        public string motorizedEquipmentDescription = "";
        public string supportEquipmentDescription = "";


        public string hardnessDescription = "Hardness represents how much of your Division is made up of armored or at least protected vehicles. When attacked, a Division adds together all Soft Attacks and Hard Attacks. A Division with high Hardness will suffer fewer Soft Attacks and more Hard Attacks - and vice versa.";
        public string apAttackDescription = "Having equal or greater Piercing to the targets Armor value allow you to do more damage and more effectively pin down their armored forces";
        public string buildCostIcDescription = "How much Factory Output a piece of equipment needs.";

        public int year = 1936;

        public bool IsColumnFull(int column)
        {
            if (unitsInDivision.ContainsKey(column) == false) { return false; }

            int columnCount = 0;
            foreach(KeyValuePair<UnitName, int> unitNameAndNumber in unitsInDivision[column])
            {
                columnCount += unitNameAndNumber.Value;
            }
            if(columnCount < 5) { return false; }
            else { return true; }
        }

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
                    foreach (KeyValuePair<int, Equipment> yearAndEquipment in UnitsAndEquipment.GetEquipment(unitNameAndNumber.Key))
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
