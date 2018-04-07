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
        private Dictionary<UnitsInFile, string[]> unitFileData = new Dictionary<UnitsInFile, string[]>();

        private Dictionary<EquipmentInFile, string[]> equipmentFileData = new Dictionary<EquipmentInFile, string[]>();


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

        private void GetEquipmentDataNew()
        {
            equipmentFileData[EquipmentInFile.Infantry] = FileHandler.LoadFile("anti_air.txt", "equipment");
            equipmentFileData[EquipmentInFile.AntiAir] = FileHandler.LoadFile("anti_tank.txt", "equipment");
            equipmentFileData[EquipmentInFile.AntiTank] = FileHandler.LoadFile("artillery.txt", "equipment");
            equipmentFileData[EquipmentInFile.Artillery] = FileHandler.LoadFile("infantry.txt", "equipment");
            equipmentFileData[EquipmentInFile.Mechanized] = FileHandler.LoadFile("mechanized.txt", "equipment");
            equipmentFileData[EquipmentInFile.Motorized] = FileHandler.LoadFile("motorized.txt", "equipment");
            equipmentFileData[EquipmentInFile.TankHeavy] = FileHandler.LoadFile("tank_heavy.txt", "equipment");
            equipmentFileData[EquipmentInFile.TankLight] = FileHandler.LoadFile("tank_light.txt", "equipment");
            equipmentFileData[EquipmentInFile.TankMedium] = FileHandler.LoadFile("tank_medium.txt", "equipment");
            equipmentFileData[EquipmentInFile.TankModern] = FileHandler.LoadFile("tank_modern.txt", "equipment");
            equipmentFileData[EquipmentInFile.TankSuperHeavy] = FileHandler.LoadFile("tank_super_heavy.txt", "equipment");
        }
        private void GetUnitData()
        {
            unitFileData[UnitsInFile.Infantry] = FileHandler.LoadFile("infantry.txt");
            unitFileData[UnitsInFile.Cavalry] = FileHandler.LoadFile("cavalry.txt");
            unitFileData[UnitsInFile.LightArmour] = FileHandler.LoadFile("light_armor.txt");
            unitFileData[UnitsInFile.MediumArmour] = FileHandler.LoadFile("medium_armor.txt");
            unitFileData[UnitsInFile.HeavyArmour] = FileHandler.LoadFile("heavy_armor.txt");
            unitFileData[UnitsInFile.SuperHeavyArmour] = FileHandler.LoadFile("super_heavy_armor.txt");
            unitFileData[UnitsInFile.ModernArmour] = FileHandler.LoadFile("modern_armor.txt");
            unitFileData[UnitsInFile.AntiTankBrigade] = FileHandler.LoadFile("anti_tank_brigade.txt");
            unitFileData[UnitsInFile.AntiAirBrigade] = FileHandler.LoadFile("anti-air_brigade.txt");
            unitFileData[UnitsInFile.ArtilleryBrigade] = FileHandler.LoadFile("artillery_brigade.txt");
            unitFileData[UnitsInFile.SPAntiAirBrigade] = FileHandler.LoadFile("sp_anti-air_brigade.txt");
            unitFileData[UnitsInFile.SPArtilleryBrigade] = FileHandler.LoadFile("sp_artillery_brigade.txt");
            unitFileData[UnitsInFile.TankDestroyerBrigade] = FileHandler.LoadFile("tank_destroyer_brigade.txt");
            GetUnitLocations();

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
                        GetUnitStats(unitName, unitFileData[UnitsInFile.Infantry]);
                        break;
                    case UnitName.Cavalry:
                        GetUnitStats(unitName, unitFileData[UnitsInFile.Cavalry]);
                        break;
                    case UnitName.AntiAir:
                        GetUnitStats(unitName, unitFileData[UnitsInFile.AntiAirBrigade]);
                        break;
                    case UnitName.AntiTank:
                        GetUnitStats(unitName, unitFileData[UnitsInFile.TankDestroyerBrigade]);
                        break;
                    case UnitName.Artillery:
                    case UnitName.RocketArtillery:
                    case UnitName.MotorizedRocketArtillery:
                        GetUnitStats(unitName, unitFileData[UnitsInFile.ArtilleryBrigade]);
                        break;
                    case UnitName.LightTank:
                        GetUnitStats(unitName, unitFileData[UnitsInFile.LightArmour]);
                        break;
                    case UnitName.MediumTank:
                        GetUnitStats(unitName, unitFileData[UnitsInFile.MediumArmour]);
                        break;
                    case UnitName.HeavyTank:
                        GetUnitStats(unitName, unitFileData[UnitsInFile.HeavyArmour]);
                        break;
                    case UnitName.SuperHeavyTank:
                        GetUnitStats(unitName, unitFileData[UnitsInFile.SuperHeavyArmour]);
                        break;
                    case UnitName.ModernTank:
                        GetUnitStats(unitName, unitFileData[UnitsInFile.ModernArmour]);
                        break;
                    case UnitName.LightSPAntiAir:
                    case UnitName.MediumSPAntiAir:
                    case UnitName.HeavySPAntiAir:
                    case UnitName.SuperHeavySPAntiAir:
                    case UnitName.ModernSPAntiAir:
                        GetUnitStats(unitName, unitFileData[UnitsInFile.SPAntiAirBrigade]);
                        break;
                    case UnitName.LightSPArtillery:
                    case UnitName.MediumSPArtillery:
                    case UnitName.HeavySPArtillery:
                    case UnitName.SuperHeavySPArtillery:
                    case UnitName.ModernSPArtillery:
                        GetUnitStats(unitName, unitFileData[UnitsInFile.SPArtilleryBrigade]);
                        break;
                    case UnitName.LightTankDestroyer:
                    case UnitName.MediumTankDestroyer:
                    case UnitName.HeavyTankDestroyer:
                    case UnitName.SuperHeavyTankDestroyer:
                    case UnitName.ModernTankDestroyer:
                        GetUnitStats(unitName, unitFileData[UnitsInFile.TankDestroyerBrigade]);
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

        private void GetUnitLocationsHelper(UnitName unitName, int location)
        {
            unitStart[unitName] = location;
            GetUnitLocationEnds(location);
            checking[unitName] = true;
        }
        private void GetUnitLocations()
        {
            int i;
            for (i = 0; i < unitFileData[UnitsInFile.Infantry].Length; i++)
            {
                if (Regex.Match(unitFileData[UnitsInFile.Infantry][i], @"infantry.*?\{").Success)
                {
                    GetUnitLocationsHelper(UnitName.Infantry, i);
                }
                else if (Regex.Match(unitFileData[UnitsInFile.Infantry][i], @"bicycle_battalion.*?\{").Success)
                {
                    GetUnitLocationsHelper(UnitName.BicycleBattalion, i);
                }
                else if (Regex.Match(unitFileData[UnitsInFile.Infantry][i], @"marine.*?\{").Success)
                {
                    GetUnitLocationsHelper(UnitName.Marines, i);
                }
                else if (Regex.Match(unitFileData[UnitsInFile.Infantry][i], @"mountaineers.*?\{").Success)
                {
                    GetUnitLocationsHelper(UnitName.Mountaineers, i);
                }
                else if (Regex.Match(unitFileData[UnitsInFile.Infantry][i], @"paratrooper.*?\{").Success)
                {
                    GetUnitLocationsHelper(UnitName.Paratroopers, i);
                }
                else if (Regex.Match(unitFileData[UnitsInFile.Infantry][i], @"motorized.*?\{").Success)
                {
                    GetUnitLocationsHelper(UnitName.Motorized, i);
                }
                else if (Regex.Match(unitFileData[UnitsInFile.Infantry][i], @"mechanized.*?\{").Success)
                {
                    GetUnitLocationsHelper(UnitName.Mechanized, i);
                }
            }
            GetUnitLocationEnds(i);

            for (i = 0; i < unitFileData[UnitsInFile.Cavalry].Length; i++)
            {
                if (Regex.Match(unitFileData[UnitsInFile.Cavalry][i], @"cavalry.*?\{").Success)
                {
                    GetUnitLocationsHelper(UnitName.Cavalry, i);
                }
            }
            GetUnitLocationEnds(i);

            for (i = 0; i < unitFileData[UnitsInFile.LightArmour].Length; i++)
            {
                if (Regex.Match(unitFileData[UnitsInFile.LightArmour][i], @"light_armor.*?\{").Success)
                {
                    GetUnitLocationsHelper(UnitName.LightTank, i);
                }
            }
            GetUnitLocationEnds(i);

            for (i = 0; i < unitFileData[UnitsInFile.MediumArmour].Length; i++)
            {
                if (Regex.Match(unitFileData[UnitsInFile.MediumArmour][i], @"medium_armor.*?\{").Success)
                {
                    GetUnitLocationsHelper(UnitName.MediumTank, i);
                }
            }
            GetUnitLocationEnds(i);

            for (i = 0; i < unitFileData[UnitsInFile.HeavyArmour].Length; i++)
            {
                if (Regex.Match(unitFileData[UnitsInFile.HeavyArmour][i], @"^[^_]*?heavy_armor.*?\{").Success)
                {
                    GetUnitLocationsHelper(UnitName.HeavyTank, i);
                }
            }
            GetUnitLocationEnds(i);

            for (i = 0; i < unitFileData[UnitsInFile.SuperHeavyArmour].Length; i++)
            {
                if (Regex.Match(unitFileData[UnitsInFile.SuperHeavyArmour][i], @"super_heavy_armor.*?\{").Success)
                {
                    GetUnitLocationsHelper(UnitName.SuperHeavyTank, i);
                }
            }
            GetUnitLocationEnds(i);

            for (i = 0; i < unitFileData[UnitsInFile.ModernArmour].Length; i++)
            {
                if (Regex.Match(unitFileData[UnitsInFile.ModernArmour][i], @"modern_armor.*?\{").Success)
                {
                    GetUnitLocationsHelper(UnitName.ModernTank, i);
                }
            }
            GetUnitLocationEnds(i);

            for (i = 0; i < unitFileData[UnitsInFile.AntiTankBrigade].Length; i++)
            {
                if (Regex.Match(unitFileData[UnitsInFile.AntiTankBrigade][i], @"anti_tank_brigade.*?\{").Success)
                {
                    GetUnitLocationsHelper(UnitName.AntiTank, i);
                }
            }
            GetUnitLocationEnds(i);

            for (i = 0; i < unitFileData[UnitsInFile.AntiAirBrigade].Length; i++)
            {
                if (Regex.Match(unitFileData[UnitsInFile.AntiAirBrigade][i], @"anti_air_brigade.*?\{").Success)
                {
                    GetUnitLocationsHelper(UnitName.AntiAir, i);
                }
            }
            GetUnitLocationEnds(i);

            for (i = 0; i < unitFileData[UnitsInFile.ArtilleryBrigade].Length; i++)
            {
                if (Regex.Match(unitFileData[UnitsInFile.ArtilleryBrigade][i], @"^[^_]*?artillery_brigade.*?\{").Success)
                {
                    GetUnitLocationsHelper(UnitName.Artillery, i);
                }
                else if (Regex.Match(unitFileData[UnitsInFile.ArtilleryBrigade][i], @"rocket_artillery_brigade.*?\{").Success)
                {
                    GetUnitLocationsHelper(UnitName.RocketArtillery, i);
                }
                else if (Regex.Match(unitFileData[UnitsInFile.ArtilleryBrigade][i], @"motorized_rocket_brigade.*?\{").Success)
                {
                    GetUnitLocationsHelper(UnitName.MotorizedRocketArtillery, i);
                }
            }
            GetUnitLocationEnds(i);

            for (i = 0; i < unitFileData[UnitsInFile.SPAntiAirBrigade].Length; i++)
            {
                if (Regex.Match(unitFileData[UnitsInFile.SPAntiAirBrigade][i], @"light_sp_anti_air_brigade.*?\{").Success)
                {
                    GetUnitLocationsHelper(UnitName.LightSPAntiAir, i);
                }
                else if (Regex.Match(unitFileData[UnitsInFile.SPAntiAirBrigade][i], @"medium_sp_anti_air_brigade.*?\{").Success)
                {
                    GetUnitLocationsHelper(UnitName.MediumSPAntiAir, i);
                }
                else if (Regex.Match(unitFileData[UnitsInFile.SPAntiAirBrigade][i], @"^[^_]*?heavy_sp_anti_air_brigade.*?\{").Success)
                {
                    GetUnitLocationsHelper(UnitName.HeavySPAntiAir, i);
                }
                else if (Regex.Match(unitFileData[UnitsInFile.SPAntiAirBrigade][i], @"super_heavy_sp_anti_air_brigade.*?\{").Success)
                {
                    GetUnitLocationsHelper(UnitName.SuperHeavySPAntiAir, i);
                }
                else if (Regex.Match(unitFileData[UnitsInFile.SPAntiAirBrigade][i], @"modern_sp_anti_air_brigade.*?\{").Success)
                {
                    GetUnitLocationsHelper(UnitName.ModernSPAntiAir, i);
                }
            }
            GetUnitLocationEnds(i);

            for (i = 0; i < unitFileData[UnitsInFile.SPArtilleryBrigade].Length; i++)
            {
                if (Regex.Match(unitFileData[UnitsInFile.SPArtilleryBrigade][i], @"light_sp_artillery_brigade.*?\{").Success)
                {
                    GetUnitLocationsHelper(UnitName.LightSPArtillery, i);
                }
                else if (Regex.Match(unitFileData[UnitsInFile.SPArtilleryBrigade][i], @"medium_sp_artillery_brigade.*?\{").Success)
                {
                    GetUnitLocationsHelper(UnitName.MediumSPArtillery, i);
                }
                else if (Regex.Match(unitFileData[UnitsInFile.SPArtilleryBrigade][i], @"^[^_]*?heavy_sp_artillery_brigade.*?\{").Success)
                {
                    GetUnitLocationsHelper(UnitName.HeavySPArtillery, i);
                }
                else if (Regex.Match(unitFileData[UnitsInFile.SPArtilleryBrigade][i], @"super_heavy_sp_artillery_brigade.*?\{").Success)
                {
                    GetUnitLocationsHelper(UnitName.SuperHeavySPArtillery, i);
                }
                else if (Regex.Match(unitFileData[UnitsInFile.SPArtilleryBrigade][i], @"modern_sp_artillery_brigade.*?\{").Success)
                {
                    GetUnitLocationsHelper(UnitName.ModernSPArtillery, i);
                }
            }
            GetUnitLocationEnds(i);

            for (i = 0; i < unitFileData[UnitsInFile.TankDestroyerBrigade].Length; i++)
            {
                if (Regex.Match(unitFileData[UnitsInFile.TankDestroyerBrigade][i], @"light_tank_destroyer_brigade.*?\{").Success)
                {
                    GetUnitLocationsHelper(UnitName.LightTankDestroyer, i);
                }
                else if (Regex.Match(unitFileData[UnitsInFile.TankDestroyerBrigade][i], @"medium_tank_destroyer_brigade.*?\{").Success)
                {
                    GetUnitLocationsHelper(UnitName.MediumTankDestroyer, i);
                }
                else if (Regex.Match(unitFileData[UnitsInFile.TankDestroyerBrigade][i], @"^[^_]*?heavy_tank_destroyer_brigade.*?\{").Success)
                {
                    GetUnitLocationsHelper(UnitName.HeavyTankDestroyer, i);
                }
                else if (Regex.Match(unitFileData[UnitsInFile.TankDestroyerBrigade][i], @"super_heavy_tank_destroyer_brigade.*?\{").Success)
                {
                    GetUnitLocationsHelper(UnitName.SuperHeavyTankDestroyer, i);
                }
                else if (Regex.Match(unitFileData[UnitsInFile.TankDestroyerBrigade][i], @"modern_tank_destroyer_brigade.*?\{").Success)
                {
                    GetUnitLocationsHelper(UnitName.ModernTankDestroyer, i);
                }
            }
            GetUnitLocationEnds(i);
        }
        private void GetUnitLocationEnds(int location)
        {
            foreach (UnitName unitName in Enum.GetValues(typeof(UnitName)))
            {
                if (checking[unitName])
                {
                    unitEnd[unitName] = location - 1;
                    checking[unitName] = false;
                }
            }
        }
    }
}