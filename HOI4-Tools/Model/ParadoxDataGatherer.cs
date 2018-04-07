using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HOI4_Tools.Model
{
    public class ParadoxDataGatherer
    {
        private Dictionary<UnitName, int> unitStart = new Dictionary<UnitName, int>();
        private Dictionary<UnitName, int> unitEnd = new Dictionary<UnitName, int>();
        private Dictionary<UnitName, bool> checking = new Dictionary<UnitName, bool>();



        string[] infantryEquipmentFileData;

        int infantryEquipmentStart;
        int infantryEquipment0Start;
        int infantryEquipment1Start;
        int infantryEquipment2Start;
        int infantryEquipment3Start;

        int infantryEquipmentEnd;
        int infantryEquipment0End;
        int infantryEquipment1End;
        int infantryEquipment2End;
        int infantryEquipment3End;

        bool isCheckinginfantryEquipment = false;
        bool isCheckinginfantryEquipment0 = false;
        bool isCheckinginfantryEquipment1 = false;
        bool isCheckinginfantryEquipment2 = false;
        bool isCheckinginfantryEquipment3 = false;


        public ParadoxDataGatherer()
        {
            foreach (UnitName unitName in Enum.GetValues(typeof(UnitName)))
            {
                checking[unitName] = false;
            }

            GetUnitData();
            GetEquipmentData();
        }

        private void GetEquipmentData()
        {
            infantryEquipmentFileData = FileHandler.LoadFile("infantry.txt", "equipment");
            GetInfantryEquipmentLocations();

            Equipment infantryEquipmentTemplate = new Equipment();
            GetEquipmentStats(infantryEquipmentTemplate, infantryEquipmentStart, infantryEquipmentEnd);

            UnitsAndEquipment.equipment[EquipmentType.Infantry] = new Dictionary<int, Equipment>();

            Equipment infantryEquipment0 = (Equipment)infantryEquipmentTemplate.GetClone();
            GetEquipmentStats(infantryEquipment0, infantryEquipment0Start, infantryEquipment0End);
            UnitsAndEquipment.equipment[EquipmentType.Infantry][infantryEquipment0.year] = infantryEquipment0;

            Equipment infantryEquipment1 = (Equipment)infantryEquipmentTemplate.GetClone();
            GetEquipmentStats(infantryEquipment1, infantryEquipment1Start, infantryEquipment1End);
            UnitsAndEquipment.equipment[EquipmentType.Infantry][infantryEquipment1.year] = infantryEquipment1;

            Equipment infantryEquipment2 = (Equipment)infantryEquipmentTemplate.GetClone();
            GetEquipmentStats(infantryEquipment2, infantryEquipment2Start, infantryEquipment2End);
            UnitsAndEquipment.equipment[EquipmentType.Infantry][infantryEquipment2.year] = infantryEquipment2;

            Equipment infantryEquipment3 = (Equipment)infantryEquipmentTemplate.GetClone();
            GetEquipmentStats(infantryEquipment3, infantryEquipment3Start, infantryEquipment3End);
            UnitsAndEquipment.equipment[EquipmentType.Infantry][infantryEquipment3.year] = infantryEquipment3;
        }

        private void GetUnitData()
        {
            foreach (UnitName unitName in Enum.GetValues(typeof(UnitName)))
            {
                switch (unitName)
                {

                    case UnitName.BicycleBattalion:
                    case UnitName.Infantry:
                    case UnitName.Marines:
                    case UnitName.Mechanized:
                    case UnitName.Motorized:
                    case UnitName.Mountaineers:
                    case UnitName.Paratroopers:
                        GetInfantryLocations(FileHandler.LoadFile("infantry.txt"));
                        GetUnitStats(unitName, FileHandler.LoadFile("infantry.txt"));
                        break;

                    case UnitName.AntiAir:
                    case UnitName.AntiTank:
                    case UnitName.Artillery:
                    case UnitName.Cavalry:
                    case UnitName.HeavySPAntiAir:
                    case UnitName.HeavySPArtillery:
                    case UnitName.HeavyTank:
                    case UnitName.HeavyTankDestroyer:
                    case UnitName.LightSPAntiAir:
                    case UnitName.LightSPArtillery:
                    case UnitName.LightTank:
                    case UnitName.LightTankDestroyer:
                    case UnitName.MediumSPArtillery:
                    case UnitName.MediumTank:
                    case UnitName.MediumTankDestroyer:
                    case UnitName.ModernSPAntiAir:
                    case UnitName.ModernSPArtillery:
                    case UnitName.ModernTank:
                    case UnitName.ModernTankDestroyer:
                    case UnitName.MotorizedRocketArtillery:
                    case UnitName.RocketArtillery:
                    case UnitName.SuperHeavySPAntiAir:
                    case UnitName.SuperHeavySPArtillery:
                    case UnitName.SuperHeavyTank:
                    case UnitName.SuperHeavyTankDestroyer:
                    case UnitName.MediumSPAntiAir:
                    default:
                        break;
                }
            }
        }

        private void GetEquipmentStats(Equipment equipment, int start, int end)
        {
            Match match;
            for (int i = start; i < end; i++)
            {
                match = Regex.Match(infantryEquipmentFileData[i], @"year[^0-9]+?([\-0-9]+)");
                if (match.Success)
                {
                    equipment.year = Int32.Parse(match.Groups[1].Value);
                }
                match = Regex.Match(infantryEquipmentFileData[i], @"maximum_speed[^0-9]+?([\-0-9]+)");
                if (match.Success)
                {
                    equipment.maximumSpeed = Int32.Parse(match.Groups[1].Value);
                }
                match = Regex.Match(infantryEquipmentFileData[i], @"defense[^0-9]+?([\-0-9]+)");
                if (match.Success)
                {
                    equipment.defense = Int32.Parse(match.Groups[1].Value);
                }
                match = Regex.Match(infantryEquipmentFileData[i], @"breakthrough[^0-9]+?([\-0-9]+)");
                if (match.Success)
                {
                    equipment.breakthrough = Int32.Parse(match.Groups[1].Value);
                }
                match = Regex.Match(infantryEquipmentFileData[i], @"armor_value[^0-9]+?([\-0-9]+)");
                if (match.Success)
                {
                    equipment.armorValue = Int32.Parse(match.Groups[1].Value);
                }


                match = Regex.Match(infantryEquipmentFileData[i], @"reliability[^0-9]+?([\-0-9\.]+)");
                if (match.Success)
                {
                    equipment.reliability = float.Parse(match.Groups[1].Value, CultureInfo.InvariantCulture.NumberFormat);
                }
                match = Regex.Match(infantryEquipmentFileData[i], @"hardness[^0-9]+?([\-0-9\.]+)");
                if (match.Success)
                {
                    equipment.hardness = float.Parse(match.Groups[1].Value, CultureInfo.InvariantCulture.NumberFormat);
                }
                match = Regex.Match(infantryEquipmentFileData[i], @"soft_attack[^0-9]+?([\-0-9\.]+)");
                if (match.Success)
                {
                    equipment.softAttack = float.Parse(match.Groups[1].Value, CultureInfo.InvariantCulture.NumberFormat);
                }
                match = Regex.Match(infantryEquipmentFileData[i], @"hard_attack[^0-9]+?([\-0-9\.]+)");
                if (match.Success)
                {
                    equipment.hardAttack = float.Parse(match.Groups[1].Value, CultureInfo.InvariantCulture.NumberFormat);
                }
                match = Regex.Match(infantryEquipmentFileData[i], @"ap_attack[^0-9]+?([\-0-9\.]+)");
                if (match.Success)
                {
                    equipment.apAttack = float.Parse(match.Groups[1].Value, CultureInfo.InvariantCulture.NumberFormat);
                }
                match = Regex.Match(infantryEquipmentFileData[i], @"air_attack[^0-9]+?([\-0-9\.]+)");
                if (match.Success)
                {
                    equipment.airAttack = float.Parse(match.Groups[1].Value, CultureInfo.InvariantCulture.NumberFormat);
                }
                match = Regex.Match(infantryEquipmentFileData[i], @"build_cost_ic[^0-9]+?([\-0-9\.]+)");
                if (match.Success)
                {
                    equipment.buildCostIc = float.Parse(match.Groups[1].Value, CultureInfo.InvariantCulture.NumberFormat);
                }
            }
        }

        private void GetUnitStats(UnitName unitName, string[] paradoxData)
        {
            Unit unit = new Unit();
            Match match;

            for (int i = unitStart[unitName]; i < unitEnd[unitName]; i++)
            {
                match = Regex.Match(paradoxData[i], @"combat_width[^0-9]+?([\-0-9]+)");
                if (match.Success)
                {
                    unit.combatWidth = Int32.Parse(match.Groups[1].Value);
                }
                match = Regex.Match(paradoxData[i], @"max_strength[^0-9]+?([\-0-9]+)");
                if (match.Success)
                {
                    unit.maxStrength = Int32.Parse(match.Groups[1].Value);
                }
                match = Regex.Match(paradoxData[i], @"max_organisation[^0-9]+?([\-0-9]+)");
                if (match.Success)
                {
                    unit.maxOrganisation = Int32.Parse(match.Groups[1].Value);
                }
                match = Regex.Match(paradoxData[i], @"manpower[^0-9]+?([\-0-9]+)");
                if (match.Success)
                {
                    unit.manpower = Int32.Parse(match.Groups[1].Value);
                }
                match = Regex.Match(paradoxData[i], @"training_time[^0-9]+?([\-0-9]+)");
                if (match.Success)
                {
                    unit.trainingTime = Int32.Parse(match.Groups[1].Value);
                }
                match = Regex.Match(paradoxData[i], @"suppression[^0-9]+?([\-0-9]+)");
                if (match.Success)
                {
                    unit.suppression = Int32.Parse(match.Groups[1].Value);
                }
                match = Regex.Match(paradoxData[i], @"infantry_equipment[^0-9]+?([\-0-9]+)");
                if (match.Success)
                {
                    unit.infantryEquipment = Int32.Parse(match.Groups[1].Value);
                }
                match = Regex.Match(paradoxData[i], @"support_equipment[^0-9]+?([\-0-9]+)");
                if (match.Success)
                {
                    unit.supportEquipment = Int32.Parse(match.Groups[1].Value);
                }
                match = Regex.Match(paradoxData[i], @"motorized_equipment[^0-9]+?([\-0-9]+)");
                if (match.Success)
                {
                    unit.motorizedEquipment = Int32.Parse(match.Groups[1].Value);
                }
                match = Regex.Match(paradoxData[i], @"mechanized_equipment[^0-9]+?([\-0-9]+)");
                if (match.Success)
                {
                    unit.mechanizedEquipment = Int32.Parse(match.Groups[1].Value);
                }


                match = Regex.Match(paradoxData[i], @"weight[^0-9]+?([\-0-9\.]+)");
                if (match.Success)
                {
                    unit.weight = float.Parse(match.Groups[1].Value, CultureInfo.InvariantCulture.NumberFormat);
                }
                match = Regex.Match(paradoxData[i], @"supply_consumption[^0-9]+?([\-0-9\.]+)");
                if (match.Success)
                {
                    unit.supplyConsumption = float.Parse(match.Groups[1].Value, CultureInfo.InvariantCulture.NumberFormat);
                }
                match = Regex.Match(paradoxData[i], @"maximum_speed[^0-9]+?([\-0-9\.]+)");
                if (match.Success)
                {
                    unit.maxSpeed = float.Parse(match.Groups[1].Value, CultureInfo.InvariantCulture.NumberFormat);
                }
                match = Regex.Match(paradoxData[i], @"breakthrough[^0-9]+?([\-0-9\.]+)");
                if (match.Success)
                {
                    unit.breakthrough = float.Parse(match.Groups[1].Value, CultureInfo.InvariantCulture.NumberFormat);
                }
                match = Regex.Match(paradoxData[i], @"hardness[^0-9]+?([\-0-9\.]+)");
                if (match.Success)
                {
                    unit.hardness = float.Parse(match.Groups[1].Value, CultureInfo.InvariantCulture.NumberFormat);
                }


                if (Regex.Match(paradoxData[i], @"can_be_parachuted.*?yes").Success)
                {
                    unit.canBeParachuted = true;
                }


                /*
                if (Regex.Match(infantryFileData[i], @"transport.*?motorized_equipment").Success)
                {
                    unit.transportType = TransportType.Motorized;
                }
                if (Regex.Match(infantryFileData[i], @"transport.*?mechanized_equipment").Success)
                {
                    unit.transportType = TransportType.Mechanized;
                }
                */


                if (Regex.Match(paradoxData[i], @"forest.*?\{").Success)
                {
                    GetTerrainStats(unit, TerrainType.Forest, i + 1, i + 3, paradoxData);
                }
                if (Regex.Match(paradoxData[i], @"hills.*?\{").Success)
                {
                    GetTerrainStats(unit, TerrainType.Hills, i + 1, i + 3, paradoxData);
                }
                if (Regex.Match(paradoxData[i], @"mountain.*?\{").Success)
                {
                    GetTerrainStats(unit, TerrainType.Mountain, i + 1, i + 3, paradoxData);
                }
                if (Regex.Match(paradoxData[i], @"marsh.*?\{").Success)
                {
                    GetTerrainStats(unit, TerrainType.Marsh, i + 1, i + 3, paradoxData);
                }
                if (Regex.Match(paradoxData[i], @"plains.*?\{").Success)
                {
                    GetTerrainStats(unit, TerrainType.Plains, i + 1, i + 3, paradoxData);
                }
                if (Regex.Match(paradoxData[i], @"urban.*?\{").Success)
                {
                    GetTerrainStats(unit, TerrainType.Urban, i + 1, i + 3, paradoxData);
                }
                if (Regex.Match(paradoxData[i], @"desert.*?\{").Success)
                {
                    GetTerrainStats(unit, TerrainType.Desert, i + 1, i + 3, paradoxData);
                }
                if (Regex.Match(paradoxData[i], @"river.*?\{").Success)
                {
                    GetTerrainStats(unit, TerrainType.River, i + 1, i + 3, paradoxData);
                }
                if (Regex.Match(paradoxData[i], @"amphibious.*?\{").Success)
                {
                    GetTerrainStats(unit, TerrainType.Amphibious, i + 1, i + 3, paradoxData);
                }
            }
            UnitsAndEquipment.units[unitName] = unit;
        }

        private void GetTerrainStats(Unit unit, TerrainType terrainType, int start, int end, string[] paradoxData)
        {
            Regex regex;
            Match match;
            TerrainStats terrainStats = new TerrainStats();
            for (int i = start; i < end; i++)
            {
                regex = new Regex(@"attack[^0-9]+?([\-0-9\.]+)");
                match = regex.Match(paradoxData[i]);
                if (match.Success)
                {
                    terrainStats.attack = float.Parse(match.Groups[1].Value, CultureInfo.InvariantCulture.NumberFormat);
                }
                regex = new Regex(@"defence[^0-9]+?([\-0-9\.]+)");
                match = regex.Match(paradoxData[i]);
                if (match.Success)
                {
                    terrainStats.defence = float.Parse(match.Groups[1].Value, CultureInfo.InvariantCulture.NumberFormat);
                }
                regex = new Regex(@"movement[^0-9]+?([\-0-9\.]+)");
                match = regex.Match(paradoxData[i]);
                if (match.Success)
                {
                    terrainStats.movement = float.Parse(match.Groups[1].Value, CultureInfo.InvariantCulture.NumberFormat);
                }
            }
            unit.terrainStats[terrainType] = terrainStats;
        }

        private void GetInfantryEquipmentLocations()
        {
            int i;
            for (i = 0; i < infantryEquipmentFileData.Length; i++)
            {
                if (Regex.Match(infantryEquipmentFileData[i], @"infantry_equipment[^_]*?\{").Success)
                {
                    infantryEquipmentStart = i;
                    GetInfantryEquipmentLocationEnds(i);
                    isCheckinginfantryEquipment = true;
                }
                else if (Regex.Match(infantryEquipmentFileData[i], @"infantry_equipment_0.*?\{").Success)
                {
                    infantryEquipment0Start = i;
                    GetInfantryEquipmentLocationEnds(i);
                    isCheckinginfantryEquipment0 = true;
                }
                else if (Regex.Match(infantryEquipmentFileData[i], @"infantry_equipment_1.*?\{").Success)
                {
                    infantryEquipment1Start = i;
                    GetInfantryEquipmentLocationEnds(i);
                    isCheckinginfantryEquipment1 = true;
                }
                else if (Regex.Match(infantryEquipmentFileData[i], @"infantry_equipment_2.*?\{").Success)
                {
                    infantryEquipment2Start = i;
                    GetInfantryEquipmentLocationEnds(i);
                    isCheckinginfantryEquipment2 = true;
                }
                else if (Regex.Match(infantryEquipmentFileData[i], @"infantry_equipment_3.*?\{").Success)
                {
                    infantryEquipment3Start = i;
                    GetInfantryEquipmentLocationEnds(i);
                    isCheckinginfantryEquipment3 = true;
                }
            }
            GetInfantryEquipmentLocationEnds(i);
        }
        private void GetInfantryEquipmentLocationEnds(int location)
        {
            if (isCheckinginfantryEquipment)
            {
                infantryEquipmentEnd = location - 1;
                isCheckinginfantryEquipment = false;
            }
            else if (isCheckinginfantryEquipment0)
            {
                infantryEquipment0End = location - 1;
                isCheckinginfantryEquipment0 = false;
            }
            else if (isCheckinginfantryEquipment1)
            {
                infantryEquipment1End = location - 1;
                isCheckinginfantryEquipment1 = false;
            }
            else if (isCheckinginfantryEquipment2)
            {
                infantryEquipment2End = location - 1;
                isCheckinginfantryEquipment2 = false;
            }
            else if (isCheckinginfantryEquipment3)
            {
                infantryEquipment3End = location - 1;
                isCheckinginfantryEquipment3 = false;
            }
        }


        private void GetInfantryLocations(string[] infantryFileData)
        {
            int i;
            for (i = 0; i < infantryFileData.Length; i++)
            {
                if (Regex.Match(infantryFileData[i], @"infantry.*?\{").Success)
                {
                    unitStart[UnitName.Infantry] = i;
                    GetUnitLocationEnds(i);
                    checking[UnitName.Infantry] = true;
                }
                else if (Regex.Match(infantryFileData[i], @"bicycle_battalion.*?\{").Success)
                {
                    unitStart[UnitName.BicycleBattalion] = i;
                    GetUnitLocationEnds(i);
                    checking[UnitName.BicycleBattalion] = true;
                }
                else if (Regex.Match(infantryFileData[i], @"marine.*?\{").Success)
                {
                    unitStart[UnitName.Marines] = i;
                    GetUnitLocationEnds(i);
                    checking[UnitName.Marines] = true;
                }
                else if (Regex.Match(infantryFileData[i], @"mountaineers.*?\{").Success)
                {
                    unitStart[UnitName.Mountaineers] = i;
                    GetUnitLocationEnds(i);
                    checking[UnitName.Mountaineers] = true;
                }
                else if (Regex.Match(infantryFileData[i], @"paratrooper.*?\{").Success)
                {
                    unitStart[UnitName.Paratroopers] = i;
                    GetUnitLocationEnds(i);
                    checking[UnitName.Paratroopers] = true;
                }
                else if (Regex.Match(infantryFileData[i], @"motorized.*?\{").Success)
                {
                    unitStart[UnitName.Motorized] = i;
                    GetUnitLocationEnds(i);
                    checking[UnitName.Motorized] = true;
                }
                else if (Regex.Match(infantryFileData[i], @"mechanized.*?\{").Success)
                {
                    unitStart[UnitName.Mechanized] = i;
                    GetUnitLocationEnds(i);
                    checking[UnitName.Mechanized] = true;
                }
            }
            GetUnitLocationEnds(i);
        }
        private void GetUnitLocationEnds(int location)
        {
            if (checking[UnitName.Infantry])
            {
                unitEnd[UnitName.Infantry] = location - 1;
                checking[UnitName.Infantry] = false;
            }
            else if (checking[UnitName.BicycleBattalion])
            {
                unitEnd[UnitName.BicycleBattalion] = location - 1;
                checking[UnitName.BicycleBattalion] = false;
            }
            else if (checking[UnitName.Marines])
            {
                unitEnd[UnitName.Marines] = location - 1;
                checking[UnitName.Marines] = false;
            }
            else if (checking[UnitName.Mountaineers])
            {
                unitEnd[UnitName.Mountaineers] = location - 1;
                checking[UnitName.Mountaineers] = false;
            }
            else if (checking[UnitName.Paratroopers])
            {
                unitEnd[UnitName.Paratroopers] = location - 1;
                checking[UnitName.Paratroopers] = false;
            }
            else if (checking[UnitName.Motorized])
            {
                unitEnd[UnitName.Motorized] = location - 1;
                checking[UnitName.Motorized] = false;
            }
            else if (checking[UnitName.Mechanized])
            {
                unitEnd[UnitName.Mechanized] = location - 1;
                checking[UnitName.Mechanized] = false;
            }
        }
    }
}