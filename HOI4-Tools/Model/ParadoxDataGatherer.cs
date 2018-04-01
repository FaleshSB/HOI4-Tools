﻿using System;
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
        string[] infantryFileData;

        int infantryStart;
        int bicycleBattalionStart;
        int marineStart;
        int mountaineersStart;
        int paratrooperStart;
        int motorizedStart;
        int mechanizedStart;

        int infantryEnd;
        int bicycleBattalionEnd;
        int marineEnd;
        int mountaineersEnd;
        int paratrooperEnd;
        int motorizedEnd;
        int mechanizedEnd;

        bool isCheckingInfantry = false;
        bool isCheckingBicycleBattalion = false;
        bool isCheckingMarine = false;
        bool isCheckingMountaineers = false;
        bool isCheckingParatrooper = false;
        bool isCheckingMotorized = false;
        bool isCheckingMechanized = false;


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
            GetUnitData();
            GetEquipmentData();
        }

        private void GetEquipmentData()
        {
            infantryEquipmentFileData = FileHandler.LoadFile("infantry.txt", "equipment");
            GetInfantryEquipmentLocations();

            Equipment infantryEquipment = new Equipment();
            GetEquipmentStats(infantryEquipment, infantryEquipmentStart, infantryEquipmentEnd);
            Equipment infantryEquipment0 = new Equipment();
            GetEquipmentStats(infantryEquipment0, infantryEquipment0Start, infantryEquipment0End);
            Equipment infantryEquipment1 = new Equipment();
            GetEquipmentStats(infantryEquipment1, infantryEquipment1Start, infantryEquipment1End);
            Equipment infantryEquipment2 = new Equipment();
            GetEquipmentStats(infantryEquipment2, infantryEquipment2Start, infantryEquipment2End);
            Equipment infantry3Equipment = new Equipment();
            GetEquipmentStats(infantry3Equipment, infantryEquipment3Start, infantryEquipment3End);


        }

        private void GetUnitData()
        {
            infantryFileData = FileHandler.LoadFile("infantry.txt");
            GetInfantryLocations();

            Unit infantry = new Unit();
            infantry.unitName = UnitName.Infantry;
            infantry.unitType = UnitType.Infantry;
            infantry.unitIcon = UnitIcon.Infantry;
            GetUnitStats(infantry, infantryStart, infantryEnd);
            Unit bicycleBattalion = new Unit();
            infantry.unitName = UnitName.BicycleBattalion;
            infantry.unitType = UnitType.Infantry;
            infantry.unitIcon = UnitIcon.Infantry;
            GetUnitStats(bicycleBattalion, bicycleBattalionStart, bicycleBattalionEnd);
            Unit marine = new Unit();
            infantry.unitName = UnitName.Marine;
            infantry.unitType = UnitType.Infantry;
            infantry.unitIcon = UnitIcon.Infantry;
            GetUnitStats(marine, marineStart, marineEnd);
            Unit mountaineers = new Unit();
            infantry.unitName = UnitName.Mountaineers;
            infantry.unitType = UnitType.Infantry;
            infantry.unitIcon = UnitIcon.Infantry;
            GetUnitStats(mountaineers, mountaineersStart, mountaineersEnd);
            Unit paratrooper = new Unit();
            infantry.unitName = UnitName.Paratrooper;
            infantry.unitType = UnitType.Infantry;
            infantry.unitIcon = UnitIcon.Infantry;
            GetUnitStats(paratrooper, paratrooperStart, paratrooperEnd);
            Unit motorized = new Unit();
            infantry.unitName = UnitName.Motorized;
            infantry.unitType = UnitType.Infantry;
            infantry.unitIcon = UnitIcon.Infantry;
            GetUnitStats(motorized, motorizedStart, motorizedEnd);
            Unit mechanized = new Unit();
            infantry.unitName = UnitName.Mechanized;
            infantry.unitType = UnitType.Infantry;
            infantry.unitIcon = UnitIcon.Infantry;
            GetUnitStats(mechanized, mechanizedStart, mechanizedEnd);
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

        private void GetUnitStats(Unit unit, int start, int end)
        {
            Match match;
            for (int i = start; i < end; i++)
            {
                match = Regex.Match(infantryFileData[i], @"combat_width[^0-9]+?([\-0-9]+)");
                if (match.Success)
                {
                    unit.combatWidth = Int32.Parse(match.Groups[1].Value);
                }
                match = Regex.Match(infantryFileData[i], @"max_organisation[^0-9]+?([\-0-9]+)");
                if (match.Success)
                {
                    unit.maxOrganisation = Int32.Parse(match.Groups[1].Value);
                }
                match = Regex.Match(infantryFileData[i], @"manpower[^0-9]+?([\-0-9]+)");
                if (match.Success)
                {
                    unit.manpower = Int32.Parse(match.Groups[1].Value);
                }
                match = Regex.Match(infantryFileData[i], @"training_time[^0-9]+?([\-0-9]+)");
                if (match.Success)
                {
                    unit.trainingTime = Int32.Parse(match.Groups[1].Value);
                }
                match = Regex.Match(infantryFileData[i], @"suppression[^0-9]+?([\-0-9]+)");
                if (match.Success)
                {
                    unit.suppression = Int32.Parse(match.Groups[1].Value);
                }
                match = Regex.Match(infantryFileData[i], @"infantry_equipment[^0-9]+?([\-0-9]+)");
                if (match.Success)
                {
                    unit.infantryEquipment = Int32.Parse(match.Groups[1].Value);
                }
                match = Regex.Match(infantryFileData[i], @"support_equipment[^0-9]+?([\-0-9]+)");
                if (match.Success)
                {
                    unit.supportEquipment = Int32.Parse(match.Groups[1].Value);
                }
                match = Regex.Match(infantryFileData[i], @"motorized_equipment[^0-9]+?([\-0-9]+)");
                if (match.Success)
                {
                    unit.motorizedEquipment = Int32.Parse(match.Groups[1].Value);
                }
                match = Regex.Match(infantryFileData[i], @"mechanized_equipment[^0-9]+?([\-0-9]+)");
                if (match.Success)
                {
                    unit.mechanizedEquipment = Int32.Parse(match.Groups[1].Value);
                }


                match = Regex.Match(infantryFileData[i], @"weight[^0-9]+?([\-0-9\.]+)");
                if (match.Success)
                {
                    unit.weight = float.Parse(match.Groups[1].Value, CultureInfo.InvariantCulture.NumberFormat);
                }
                match = Regex.Match(infantryFileData[i], @"supply_consumption[^0-9]+?([\-0-9\.]+)");
                if (match.Success)
                {
                    unit.supplyConsumption = float.Parse(match.Groups[1].Value, CultureInfo.InvariantCulture.NumberFormat);
                }
                match = Regex.Match(infantryFileData[i], @"maximum_speed[^0-9]+?([\-0-9\.]+)");
                if (match.Success)
                {
                    unit.maxSpeed = float.Parse(match.Groups[1].Value, CultureInfo.InvariantCulture.NumberFormat);
                }
                match = Regex.Match(infantryFileData[i], @"breakthrough[^0-9]+?([\-0-9\.]+)");
                if (match.Success)
                {
                    unit.breakthrough = float.Parse(match.Groups[1].Value, CultureInfo.InvariantCulture.NumberFormat);
                }
                match = Regex.Match(infantryFileData[i], @"hardness[^0-9]+?([\-0-9\.]+)");
                if (match.Success)
                {
                    unit.hardness = float.Parse(match.Groups[1].Value, CultureInfo.InvariantCulture.NumberFormat);
                }


                if (Regex.Match(infantryFileData[i], @"can_be_parachuted.*?yes").Success)
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


                if (Regex.Match(infantryFileData[i], @"forest.*?\{").Success)
                {
                    GetTerrainStats(unit, TerrainType.Forest, i + 1, i + 3);
                }
                if (Regex.Match(infantryFileData[i], @"hills.*?\{").Success)
                {
                    GetTerrainStats(unit, TerrainType.Hills, i + 1, i + 3);
                }
                if (Regex.Match(infantryFileData[i], @"mountain.*?\{").Success)
                {
                    GetTerrainStats(unit, TerrainType.Mountain, i + 1, i + 3);
                }
                if (Regex.Match(infantryFileData[i], @"marsh.*?\{").Success)
                {
                    GetTerrainStats(unit, TerrainType.Marsh, i + 1, i + 3);
                }
                if (Regex.Match(infantryFileData[i], @"plains.*?\{").Success)
                {
                    GetTerrainStats(unit, TerrainType.Plains, i + 1, i + 3);
                }
                if (Regex.Match(infantryFileData[i], @"urban.*?\{").Success)
                {
                    GetTerrainStats(unit, TerrainType.Urban, i + 1, i + 3);
                }
                if (Regex.Match(infantryFileData[i], @"desert.*?\{").Success)
                {
                    GetTerrainStats(unit, TerrainType.Desert, i + 1, i + 3);
                }
                if (Regex.Match(infantryFileData[i], @"river.*?\{").Success)
                {
                    GetTerrainStats(unit, TerrainType.River, i + 1, i + 3);
                }
                if (Regex.Match(infantryFileData[i], @"amphibious.*?\{").Success)
                {
                    GetTerrainStats(unit, TerrainType.Amphibious, i + 1, i + 3);
                }
            }
        }

        private void GetTerrainStats(Unit unit, TerrainType terrainType, int start, int end)
        {
            Regex regex;
            Match match;
            TerrainStats terrainStats = new TerrainStats();
            for (int i = start; i < end; i++)
            {
                regex = new Regex(@"attack[^0-9]+?([\-0-9\.]+)");
                match = regex.Match(infantryFileData[i]);
                if (match.Success)
                {
                    terrainStats.attack = float.Parse(match.Groups[1].Value, CultureInfo.InvariantCulture.NumberFormat);
                }
                regex = new Regex(@"defence[^0-9]+?([\-0-9\.]+)");
                match = regex.Match(infantryFileData[i]);
                if (match.Success)
                {
                    terrainStats.defence = float.Parse(match.Groups[1].Value, CultureInfo.InvariantCulture.NumberFormat);
                }
                regex = new Regex(@"movement[^0-9]+?([\-0-9\.]+)");
                match = regex.Match(infantryFileData[i]);
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


        private void GetInfantryLocations()
        {
            int i;
            for (i = 0; i < infantryFileData.Length; i++)
            {
                if (Regex.Match(infantryFileData[i], @"infantry.*?\{").Success)
                {
                    infantryStart = i;
                    GetInfantryLocationEnds(i);
                    isCheckingInfantry = true;
                }
                else if (Regex.Match(infantryFileData[i], @"bicycle_battalion.*?\{").Success)
                {
                    bicycleBattalionStart = i;
                    GetInfantryLocationEnds(i);
                    isCheckingBicycleBattalion = true;
                }
                else if (Regex.Match(infantryFileData[i], @"marine.*?\{").Success)
                {
                    marineStart = i;
                    GetInfantryLocationEnds(i);
                    isCheckingMarine = true;
                }
                else if (Regex.Match(infantryFileData[i], @"mountaineers.*?\{").Success)
                {
                    mountaineersStart = i;
                    GetInfantryLocationEnds(i);
                    isCheckingMountaineers = true;
                }
                else if (Regex.Match(infantryFileData[i], @"paratrooper.*?\{").Success)
                {
                    paratrooperStart = i;
                    GetInfantryLocationEnds(i);
                    isCheckingParatrooper = true;
                }
                else if (Regex.Match(infantryFileData[i], @"motorized.*?\{").Success)
                {
                    motorizedStart = i;
                    GetInfantryLocationEnds(i);
                    isCheckingMotorized = true;
                }
                else if (Regex.Match(infantryFileData[i], @"mechanized.*?\{").Success)
                {
                    mechanizedStart = i;
                    GetInfantryLocationEnds(i);
                    isCheckingMechanized = true;
                }
            }
            GetInfantryLocationEnds(i);
        }
        private void GetInfantryLocationEnds(int location)
        {
            if(isCheckingInfantry)
            {
                infantryEnd = location - 1;
                isCheckingInfantry = false;
            }
            else if (isCheckingBicycleBattalion)
            {
                bicycleBattalionEnd = location - 1;
                isCheckingBicycleBattalion = false;
            }
            else if (isCheckingMarine)
            {
                marineEnd = location - 1;
                isCheckingMarine = false;
            }
            else if (isCheckingMountaineers)
            {
                mountaineersEnd = location - 1;
                isCheckingMountaineers = false;
            }
            else if (isCheckingParatrooper)
            {
                paratrooperEnd = location - 1;
                isCheckingParatrooper = false;
            }
            else if (isCheckingMotorized)
            {
                motorizedEnd = location - 1;
                isCheckingMotorized = false;
            }
            else if (isCheckingMechanized)
            {
                mechanizedEnd = location - 1;
                isCheckingMechanized = false;
            }
        }
    }
}