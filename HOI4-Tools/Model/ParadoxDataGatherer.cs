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
        private Dictionary<UnitName, int> unitStart = new Dictionary<UnitName, int>();
        private Dictionary<UnitName, int> unitEnd = new Dictionary<UnitName, int>();
        private Dictionary<UnitName, bool> unitChecking = new Dictionary<UnitName, bool>();
        private Dictionary<UnitsInFile, string[]> unitFileData = new Dictionary<UnitsInFile, string[]>();

        private Dictionary<EquipmentName, int> equipmentStart = new Dictionary<EquipmentName, int>();
        private Dictionary<EquipmentName, int> equipmentEnd = new Dictionary<EquipmentName, int>();
        private Dictionary<EquipmentInFile, string[]> equipmentFileData = new Dictionary<EquipmentInFile, string[]>();
        private Dictionary<EquipmentName, Equipment> equipmentArchetype = new Dictionary<EquipmentName, Equipment>();

        public ParadoxDataGatherer()
        {
            foreach (UnitName unitName in Enum.GetValues(typeof(UnitName)))
            {
                unitChecking[unitName] = false;
            }

            GetUnitData();
            GetEquipmentData();
            UnitsAndEquipment.test();
        }

        private void GetEquipmentData()
        {
            equipmentFileData[EquipmentInFile.AntiAir] = FileHandler.LoadFile("anti_air.txt", "equipment");
            equipmentFileData[EquipmentInFile.AntiTank] = FileHandler.LoadFile("anti_tank.txt", "equipment");
            equipmentFileData[EquipmentInFile.Artillery] = FileHandler.LoadFile("artillery.txt", "equipment");
            equipmentFileData[EquipmentInFile.Infantry] = FileHandler.LoadFile("infantry.txt", "equipment");
            equipmentFileData[EquipmentInFile.Mechanized] = FileHandler.LoadFile("mechanized.txt", "equipment");
            equipmentFileData[EquipmentInFile.Motorized] = FileHandler.LoadFile("motorized.txt", "equipment");
            equipmentFileData[EquipmentInFile.TankHeavy] = FileHandler.LoadFile("tank_heavy.txt", "equipment");
            equipmentFileData[EquipmentInFile.TankLight] = FileHandler.LoadFile("tank_light.txt", "equipment");
            equipmentFileData[EquipmentInFile.TankMedium] = FileHandler.LoadFile("tank_medium.txt", "equipment");
            equipmentFileData[EquipmentInFile.TankModern] = FileHandler.LoadFile("tank_modern.txt", "equipment");
            equipmentFileData[EquipmentInFile.TankSuperHeavy] = FileHandler.LoadFile("tank_super_heavy.txt", "equipment");
            GetEquipmentLocations();

            foreach (EquipmentName equipmentName in Enum.GetValues(typeof(EquipmentName)))
            {
                switch (equipmentName)
                {
                    case EquipmentName.Infantry:
                        GetEquipmentStats(equipmentName, equipmentFileData[EquipmentInFile.Infantry], new Equipment(), equipmentName, true);
                        break;
                    case EquipmentName.AntiAir:
                        GetEquipmentStats(equipmentName, equipmentFileData[EquipmentInFile.AntiAir], new Equipment(), equipmentName, true);
                        break;
                    case EquipmentName.AntiTank:
                        GetEquipmentStats(equipmentName, equipmentFileData[EquipmentInFile.AntiTank], new Equipment(), equipmentName, true);
                        break;
                    case EquipmentName.Artillery:
                        GetEquipmentStats(equipmentName, equipmentFileData[EquipmentInFile.Artillery], new Equipment(), equipmentName, true);
                        break;
                    case EquipmentName.RocketArtillery:
                        GetEquipmentStats(equipmentName, equipmentFileData[EquipmentInFile.Artillery], new Equipment(), equipmentName, true);
                        break;
                    case EquipmentName.MotorizedRocketArtillery:
                        GetEquipmentStats(equipmentName, equipmentFileData[EquipmentInFile.Artillery], new Equipment(), equipmentName, true);
                        break;
                    case EquipmentName.Mechanized:
                        GetEquipmentStats(equipmentName, equipmentFileData[EquipmentInFile.Mechanized], new Equipment(), equipmentName, true);
                        break;
                    case EquipmentName.Motorized:
                        GetEquipmentStats(equipmentName, equipmentFileData[EquipmentInFile.Motorized], new Equipment(), equipmentName, true);
                        break;
                    case EquipmentName.HeavyTank:
                        GetEquipmentStats(equipmentName, equipmentFileData[EquipmentInFile.TankHeavy], new Equipment(), equipmentName, true);
                        break;
                    case EquipmentName.HeavyTankArtillery:
                        GetEquipmentStats(equipmentName, equipmentFileData[EquipmentInFile.TankHeavy], new Equipment(), equipmentName, true);
                        break;
                    case EquipmentName.HeavyTankDestroyer:
                        GetEquipmentStats(equipmentName, equipmentFileData[EquipmentInFile.TankHeavy], new Equipment(), equipmentName, true);
                        break;
                    case EquipmentName.HeavyTankAntiAir:
                        GetEquipmentStats(equipmentName, equipmentFileData[EquipmentInFile.TankHeavy], new Equipment(), equipmentName, true);
                        break;
                    case EquipmentName.LightTank:
                        GetEquipmentStats(equipmentName, equipmentFileData[EquipmentInFile.TankLight], new Equipment(), equipmentName, true);
                        break;
                    case EquipmentName.LightTankArtillery:
                        GetEquipmentStats(equipmentName, equipmentFileData[EquipmentInFile.TankLight], new Equipment(), equipmentName, true);
                        break;
                    case EquipmentName.LightTankDestroyer:
                        GetEquipmentStats(equipmentName, equipmentFileData[EquipmentInFile.TankLight], new Equipment(), equipmentName, true);
                        break;
                    case EquipmentName.LightTankAntiAir:
                        GetEquipmentStats(equipmentName, equipmentFileData[EquipmentInFile.TankLight], new Equipment(), equipmentName, true);
                        break;
                    case EquipmentName.MediumTank:
                        GetEquipmentStats(equipmentName, equipmentFileData[EquipmentInFile.TankMedium], new Equipment(), equipmentName, true);
                        break;
                    case EquipmentName.MediumTankArtillery:
                        GetEquipmentStats(equipmentName, equipmentFileData[EquipmentInFile.TankMedium], new Equipment(), equipmentName, true);
                        break;
                    case EquipmentName.MediumTankDestroyer:
                        GetEquipmentStats(equipmentName, equipmentFileData[EquipmentInFile.TankMedium], new Equipment(), equipmentName, true);
                        break;
                    case EquipmentName.MediumTankAntiAir:
                        GetEquipmentStats(equipmentName, equipmentFileData[EquipmentInFile.TankMedium], new Equipment(), equipmentName, true);
                        break;
                    case EquipmentName.SuperHeavyTank:
                        GetEquipmentStats(equipmentName, equipmentFileData[EquipmentInFile.TankSuperHeavy], new Equipment(), equipmentName, true);
                        break;
                    case EquipmentName.SuperHeavyTankArtillery:
                        GetEquipmentStats(equipmentName, equipmentFileData[EquipmentInFile.TankSuperHeavy], new Equipment(), equipmentName, true);
                        break;
                    case EquipmentName.SuperHeavyTankDestroyer:
                        GetEquipmentStats(equipmentName, equipmentFileData[EquipmentInFile.TankSuperHeavy], new Equipment(), equipmentName, true);
                        break;
                    case EquipmentName.SuperHeavyTankAntiAir:
                        GetEquipmentStats(equipmentName, equipmentFileData[EquipmentInFile.TankSuperHeavy], new Equipment(), equipmentName, true);
                        break;
                    case EquipmentName.ModernTank:
                        GetEquipmentStats(equipmentName, equipmentFileData[EquipmentInFile.TankModern], new Equipment(), equipmentName, true);
                        break;
                    case EquipmentName.ModernTankArtillery:
                        GetEquipmentStats(equipmentName, equipmentFileData[EquipmentInFile.TankModern], new Equipment(), equipmentName, true);
                        break;
                    case EquipmentName.ModernTankDestroyer:
                        GetEquipmentStats(equipmentName, equipmentFileData[EquipmentInFile.TankModern], new Equipment(), equipmentName, true);
                        break;
                    case EquipmentName.ModernTankAntiAir:
                        GetEquipmentStats(equipmentName, equipmentFileData[EquipmentInFile.TankModern], new Equipment(), equipmentName, true);
                        break;
                }
            }

            foreach (EquipmentName equipmentName in Enum.GetValues(typeof(EquipmentName)))
            {
                switch (equipmentName)
                {
                    case EquipmentName.Infantry0:
                    case EquipmentName.Infantry1:
                    case EquipmentName.Infantry2:
                    case EquipmentName.Infantry3:
                        GetEquipmentStats(equipmentName, equipmentFileData[EquipmentInFile.Infantry], equipmentArchetype[EquipmentName.Infantry].GetClone(), EquipmentName.Infantry);
                        break;
                    case EquipmentName.AntiAir1:
                    case EquipmentName.AntiAir2:
                    case EquipmentName.AntiAir3:
                        GetEquipmentStats(equipmentName, equipmentFileData[EquipmentInFile.AntiAir], equipmentArchetype[EquipmentName.AntiAir].GetClone(), EquipmentName.AntiAir);
                        break;
                    case EquipmentName.AntiTank1:
                    case EquipmentName.AntiTank2:
                    case EquipmentName.AntiTank3:
                        GetEquipmentStats(equipmentName, equipmentFileData[EquipmentInFile.AntiTank], equipmentArchetype[EquipmentName.AntiTank].GetClone(), EquipmentName.AntiTank);
                        break;
                    case EquipmentName.Artillery1:
                    case EquipmentName.Artillery2:
                    case EquipmentName.Artillery3:
                        GetEquipmentStats(equipmentName, equipmentFileData[EquipmentInFile.Artillery], equipmentArchetype[EquipmentName.Artillery].GetClone(), EquipmentName.Artillery);
                        break;
                    case EquipmentName.RocketArtillery1:
                    case EquipmentName.RocketArtillery2:
                        GetEquipmentStats(equipmentName, equipmentFileData[EquipmentInFile.Artillery], equipmentArchetype[EquipmentName.RocketArtillery].GetClone(), EquipmentName.RocketArtillery);
                        break;
                    case EquipmentName.MotorizedRocketArtillery1:
                        GetEquipmentStats(equipmentName, equipmentFileData[EquipmentInFile.Artillery], equipmentArchetype[EquipmentName.MotorizedRocketArtillery].GetClone(), EquipmentName.MotorizedRocketArtillery);
                        break;
                    case EquipmentName.Mechanized1:
                    case EquipmentName.Mechanized2:
                    case EquipmentName.Mechanized3:
                        GetEquipmentStats(equipmentName, equipmentFileData[EquipmentInFile.Mechanized], equipmentArchetype[EquipmentName.Mechanized].GetClone(), EquipmentName.Mechanized);
                        break;
                    case EquipmentName.Motorized1:
                        GetEquipmentStats(equipmentName, equipmentFileData[EquipmentInFile.Motorized], equipmentArchetype[EquipmentName.Motorized].GetClone(), EquipmentName.Motorized);
                        break;
                    case EquipmentName.HeavyTank1:
                    case EquipmentName.HeavyTank2:
                    case EquipmentName.HeavyTank3:
                        GetEquipmentStats(equipmentName, equipmentFileData[EquipmentInFile.TankHeavy], equipmentArchetype[EquipmentName.HeavyTank].GetClone(), EquipmentName.HeavyTank);
                        break;
                    case EquipmentName.HeavyTankArtillery1:
                    case EquipmentName.HeavyTankArtillery2:
                    case EquipmentName.HeavyTankArtillery3:
                        GetEquipmentStats(equipmentName, equipmentFileData[EquipmentInFile.TankHeavy], equipmentArchetype[EquipmentName.HeavyTankArtillery].GetClone(), EquipmentName.HeavyTankArtillery);
                        break;
                    case EquipmentName.HeavyTankDestroyer1:
                    case EquipmentName.HeavyTankDestroyer2:
                    case EquipmentName.HeavyTankDestroyer3:
                        GetEquipmentStats(equipmentName, equipmentFileData[EquipmentInFile.TankHeavy], equipmentArchetype[EquipmentName.HeavyTankDestroyer].GetClone(), EquipmentName.HeavyTankDestroyer);
                        break;
                    case EquipmentName.HeavyTankAntiAir1:
                    case EquipmentName.HeavyTankAntiAir2:
                    case EquipmentName.HeavyTankAntiAir3:
                        GetEquipmentStats(equipmentName, equipmentFileData[EquipmentInFile.TankHeavy], equipmentArchetype[EquipmentName.HeavyTankAntiAir].GetClone(), EquipmentName.HeavyTankAntiAir);
                        break;
                    case EquipmentName.LightTank1:
                    case EquipmentName.LightTank2:
                    case EquipmentName.LightTank3:
                        GetEquipmentStats(equipmentName, equipmentFileData[EquipmentInFile.TankLight], equipmentArchetype[EquipmentName.LightTank].GetClone(), EquipmentName.LightTank);
                        break;
                    case EquipmentName.LightTankArtillery1:
                    case EquipmentName.LightTankArtillery2:
                    case EquipmentName.LightTankArtillery3:
                        GetEquipmentStats(equipmentName, equipmentFileData[EquipmentInFile.TankLight], equipmentArchetype[EquipmentName.LightTankArtillery].GetClone(), EquipmentName.LightTankArtillery);
                        break;
                    case EquipmentName.LightTankDestroyer1:
                    case EquipmentName.LightTankDestroyer2:
                    case EquipmentName.LightTankDestroyer3:
                        GetEquipmentStats(equipmentName, equipmentFileData[EquipmentInFile.TankLight], equipmentArchetype[EquipmentName.LightTankDestroyer].GetClone(), EquipmentName.LightTankDestroyer);
                        break;
                    case EquipmentName.LightTankAntiAir1:
                    case EquipmentName.LightTankAntiAir2:
                    case EquipmentName.LightTankAntiAir3:
                        GetEquipmentStats(equipmentName, equipmentFileData[EquipmentInFile.TankLight], equipmentArchetype[EquipmentName.LightTankAntiAir].GetClone(), EquipmentName.LightTankAntiAir);
                        break;
                    case EquipmentName.MediumTank1:
                    case EquipmentName.MediumTank2:
                    case EquipmentName.MediumTank3:
                        GetEquipmentStats(equipmentName, equipmentFileData[EquipmentInFile.TankMedium], equipmentArchetype[EquipmentName.MediumTank].GetClone(), EquipmentName.MediumTank);
                        break;
                    case EquipmentName.MediumTankArtillery1:
                    case EquipmentName.MediumTankArtillery2:
                    case EquipmentName.MediumTankArtillery3:
                        GetEquipmentStats(equipmentName, equipmentFileData[EquipmentInFile.TankMedium], equipmentArchetype[EquipmentName.MediumTankArtillery].GetClone(), EquipmentName.MediumTankArtillery);
                        break;
                    case EquipmentName.MediumTankDestroyer1:
                    case EquipmentName.MediumTankDestroyer2:
                    case EquipmentName.MediumTankDestroyer3:
                        GetEquipmentStats(equipmentName, equipmentFileData[EquipmentInFile.TankMedium], equipmentArchetype[EquipmentName.MediumTankDestroyer].GetClone(), EquipmentName.MediumTankDestroyer);
                        break;
                    case EquipmentName.MediumTankAntiAir1:
                    case EquipmentName.MediumTankAntiAir2:
                    case EquipmentName.MediumTankAntiAir3:
                        GetEquipmentStats(equipmentName, equipmentFileData[EquipmentInFile.TankMedium], equipmentArchetype[EquipmentName.MediumTankAntiAir].GetClone(), EquipmentName.MediumTankAntiAir);
                        break;
                    case EquipmentName.SuperHeavyTank1:
                        GetEquipmentStats(equipmentName, equipmentFileData[EquipmentInFile.TankSuperHeavy], equipmentArchetype[EquipmentName.SuperHeavyTank].GetClone(), EquipmentName.SuperHeavyTank);
                        break;
                    case EquipmentName.SuperHeavyTankArtillery1:
                        GetEquipmentStats(equipmentName, equipmentFileData[EquipmentInFile.TankSuperHeavy], equipmentArchetype[EquipmentName.SuperHeavyTankArtillery].GetClone(), EquipmentName.SuperHeavyTankArtillery);
                        break;
                    case EquipmentName.SuperHeavyTankDestroyer1:
                        GetEquipmentStats(equipmentName, equipmentFileData[EquipmentInFile.TankSuperHeavy], equipmentArchetype[EquipmentName.SuperHeavyTankDestroyer].GetClone(), EquipmentName.SuperHeavyTankDestroyer);
                        break;
                    case EquipmentName.SuperHeavyTankAntiAir1:
                        GetEquipmentStats(equipmentName, equipmentFileData[EquipmentInFile.TankSuperHeavy], equipmentArchetype[EquipmentName.SuperHeavyTankAntiAir].GetClone(), EquipmentName.SuperHeavyTankAntiAir);
                        break;
                    case EquipmentName.ModernTank1:
                        GetEquipmentStats(equipmentName, equipmentFileData[EquipmentInFile.TankModern], equipmentArchetype[EquipmentName.ModernTank].GetClone(), EquipmentName.ModernTank);
                        break;
                    case EquipmentName.ModernTankArtillery1:
                        GetEquipmentStats(equipmentName, equipmentFileData[EquipmentInFile.TankModern], equipmentArchetype[EquipmentName.ModernTankArtillery].GetClone(), EquipmentName.ModernTankArtillery);
                        break;
                    case EquipmentName.ModernTankDestroyer1:
                        GetEquipmentStats(equipmentName, equipmentFileData[EquipmentInFile.TankModern], equipmentArchetype[EquipmentName.ModernTankDestroyer].GetClone(), EquipmentName.ModernTankDestroyer);
                        break;
                    case EquipmentName.ModernTankAntiAir1:
                        GetEquipmentStats(equipmentName, equipmentFileData[EquipmentInFile.TankModern], equipmentArchetype[EquipmentName.ModernTankAntiAir].GetClone(), EquipmentName.ModernTankAntiAir);
                        break;
                }
            }
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

        private void GetEquipmentStats(EquipmentName equipmentName, string[] paradoxData, Equipment equipment, EquipmentName equipmentArchetypeName, bool isEquipmentArchetype = false)
        {
            Match match;
            for (int i = equipmentStart[equipmentName]; i < equipmentEnd[equipmentName]; i++)
            {
                match = Regex.Match(paradoxData[i], @"year[^0-9]+?([\-0-9]+)");
                if (match.Success)
                {
                    equipment.year = Int32.Parse(match.Groups[1].Value);
                }
                match = Regex.Match(paradoxData[i], @"maximum_speed[^0-9]+?([\-0-9\.]+)");
                if (match.Success)
                {
                    equipment.maximumSpeed = float.Parse(match.Groups[1].Value);
                }
                match = Regex.Match(paradoxData[i], @"defense[^0-9]+?([\-0-9\.]+)");
                if (match.Success)
                {
                    equipment.defense = float.Parse(match.Groups[1].Value);
                }
                match = Regex.Match(paradoxData[i], @"breakthrough[^0-9]+?([\-0-9\.]+)");
                if (match.Success)
                {
                    equipment.breakthrough = float.Parse(match.Groups[1].Value);
                }
                match = Regex.Match(paradoxData[i], @"armor_value[^0-9]+?([\-0-9\.]+)");
                if (match.Success)
                {                    
                    equipment.armorValue = float.Parse(match.Groups[1].Value);
                }


                match = Regex.Match(paradoxData[i], @"reliability[^0-9]+?([\-0-9\.]+)");
                if (match.Success)
                {
                    equipment.reliability = float.Parse(match.Groups[1].Value, CultureInfo.InvariantCulture.NumberFormat);
                }
                match = Regex.Match(paradoxData[i], @"hardness[^0-9]+?([\-0-9\.]+)");
                if (match.Success)
                {
                    equipment.hardness = float.Parse(match.Groups[1].Value, CultureInfo.InvariantCulture.NumberFormat);
                }
                match = Regex.Match(paradoxData[i], @"soft_attack[^0-9]+?([\-0-9\.]+)");
                if (match.Success)
                {
                    equipment.softAttack = float.Parse(match.Groups[1].Value, CultureInfo.InvariantCulture.NumberFormat);
                }
                match = Regex.Match(paradoxData[i], @"hard_attack[^0-9]+?([\-0-9\.]+)");
                if (match.Success)
                {
                    equipment.hardAttack = float.Parse(match.Groups[1].Value, CultureInfo.InvariantCulture.NumberFormat);
                }
                match = Regex.Match(paradoxData[i], @"ap_attack[^0-9]+?([\-0-9\.]+)");
                if (match.Success)
                {
                    equipment.apAttack = float.Parse(match.Groups[1].Value, CultureInfo.InvariantCulture.NumberFormat);
                }
                match = Regex.Match(paradoxData[i], @"air_attack[^0-9]+?([\-0-9\.]+)");
                if (match.Success)
                {
                    equipment.airAttack = float.Parse(match.Groups[1].Value, CultureInfo.InvariantCulture.NumberFormat);
                }
                match = Regex.Match(paradoxData[i], @"build_cost_ic[^0-9]+?([\-0-9\.]+)");
                if (match.Success)
                {
                    equipment.buildCostIc = float.Parse(match.Groups[1].Value, CultureInfo.InvariantCulture.NumberFormat);
                }
            }
            if(isEquipmentArchetype)
            {
                equipmentArchetype[equipmentName] = equipment;
            }
            else
            {
                if(UnitsAndEquipment.equipment.ContainsKey(equipmentArchetypeName) == false) { UnitsAndEquipment.equipment[equipmentArchetypeName] = new Dictionary<int, Equipment>(); }
                UnitsAndEquipment.equipment[equipmentArchetypeName][equipment.year] = equipment;
            }
        }

        private void GetUnitStats(UnitName unitName, string[] paradoxData)
        {
            Unit unit = new Unit();
            Match match;

            for (int i = unitStart[unitName]; i < unitEnd[unitName]; i++)
            {
                match = Regex.Match(paradoxData[i], @"combat_width[^0-9]+?([\-0-9\.]+)");
                if (match.Success)
                {
                    unit.combatWidth = float.Parse(match.Groups[1].Value);
                }
                match = Regex.Match(paradoxData[i], @"max_strength[^0-9]+?([\-0-9\.]+)");
                if (match.Success)
                {
                    unit.maxStrength = float.Parse(match.Groups[1].Value);
                }
                match = Regex.Match(paradoxData[i], @"max_organisation[^0-9]+?([\-0-9\.]+)");
                if (match.Success)
                {
                    unit.maxOrganisation = float.Parse(match.Groups[1].Value);
                }
                match = Regex.Match(paradoxData[i], @"manpower[^0-9]+?([\-0-9\.]+)");
                if (match.Success)
                {
                    unit.manpower = float.Parse(match.Groups[1].Value);
                }
                match = Regex.Match(paradoxData[i], @"training_time[^0-9]+?([\-0-9\.]+)");
                if (match.Success)
                {
                    unit.trainingTime = float.Parse(match.Groups[1].Value);
                }
                match = Regex.Match(paradoxData[i], @"suppression[^0-9]+?([\-0-9\.]+)");
                if (match.Success)
                {
                    unit.suppression = float.Parse(match.Groups[1].Value);
                }
                match = Regex.Match(paradoxData[i], @"infantry_equipment[^0-9]+?([\-0-9\.]+)");
                if (match.Success)
                {
                    unit.infantryEquipment = float.Parse(match.Groups[1].Value);
                }
                match = Regex.Match(paradoxData[i], @"support_equipment[^0-9]+?([\-0-9\.]+)");
                if (match.Success)
                {
                    unit.supportEquipment = float.Parse(match.Groups[1].Value);
                }
                match = Regex.Match(paradoxData[i], @"motorized_equipment[^0-9]+?([\-0-9\.]+)");
                if (match.Success)
                {
                    unit.motorizedEquipment = float.Parse(match.Groups[1].Value);
                }
                match = Regex.Match(paradoxData[i], @"mechanized_equipment[^0-9]+?([\-0-9\.]+)");
                if (match.Success)
                {
                    unit.mechanizedEquipment = float.Parse(match.Groups[1].Value);
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

        private void GetEquipmentLocationsHelper(EquipmentName equipmentName, int startLocation, EquipmentInFile equipmentInFile)
        {
            equipmentStart[equipmentName] = startLocation;
            int bracketCount = 1;
            for (int i = startLocation + 1; i < equipmentFileData[equipmentInFile].Length; i++)
            {
                for (int y = 0; y < equipmentFileData[equipmentInFile][i].Length; y++)
                {
                    if(equipmentFileData[equipmentInFile][i][y] == '{') { bracketCount++; }
                    if(equipmentFileData[equipmentInFile][i][y] == '}') { bracketCount--; }
                }
                if(bracketCount == 0)
                {
                    equipmentEnd[equipmentName] = i;
                    break;
                }
            }
        }
        private void GetEquipmentLocations()
        {
            int i;
            for (i = 0; i < equipmentFileData[EquipmentInFile.Infantry].Length; i++)
            {
                if (Regex.Match(equipmentFileData[EquipmentInFile.Infantry][i], @"infantry_equipment[^_]*?\{").Success)
                {
                    GetEquipmentLocationsHelper(EquipmentName.Infantry, i, EquipmentInFile.Infantry);
                }
                else if (Regex.Match(equipmentFileData[EquipmentInFile.Infantry][i], @"infantry_equipment_0.*?\{").Success)
                {
                    GetEquipmentLocationsHelper(EquipmentName.Infantry0, i, EquipmentInFile.Infantry);
                }
                else if (Regex.Match(equipmentFileData[EquipmentInFile.Infantry][i], @"infantry_equipment_1.*?\{").Success)
                {
                    GetEquipmentLocationsHelper(EquipmentName.Infantry1, i, EquipmentInFile.Infantry);
                }
                else if (Regex.Match(equipmentFileData[EquipmentInFile.Infantry][i], @"infantry_equipment_2.*?\{").Success)
                {
                    GetEquipmentLocationsHelper(EquipmentName.Infantry2, i, EquipmentInFile.Infantry);
                }
                else if (Regex.Match(equipmentFileData[EquipmentInFile.Infantry][i], @"infantry_equipment_3.*?\{").Success)
                {
                    GetEquipmentLocationsHelper(EquipmentName.Infantry3, i, EquipmentInFile.Infantry);
                }
            }

            for (i = 0; i < equipmentFileData[EquipmentInFile.AntiAir].Length; i++)
            {
                if (Regex.Match(equipmentFileData[EquipmentInFile.AntiAir][i], @"anti_air_equipment[^_]*?\{").Success)
                {
                    GetEquipmentLocationsHelper(EquipmentName.AntiAir, i, EquipmentInFile.AntiAir );
                }
                else if (Regex.Match(equipmentFileData[EquipmentInFile.AntiAir][i], @"anti_air_equipment_1.*?\{").Success)
                {
                    GetEquipmentLocationsHelper(EquipmentName.AntiAir1, i, EquipmentInFile.AntiAir);
                }
                else if (Regex.Match(equipmentFileData[EquipmentInFile.AntiAir][i], @"anti_air_equipment_2.*?\{").Success)
                {
                    GetEquipmentLocationsHelper(EquipmentName.AntiAir2, i, EquipmentInFile.AntiAir);
                }
                else if (Regex.Match(equipmentFileData[EquipmentInFile.AntiAir][i], @"anti_air_equipment_3.*?\{").Success)
                {
                    GetEquipmentLocationsHelper(EquipmentName.AntiAir3, i, EquipmentInFile.AntiAir);
                }
            }

            for (i = 0; i < equipmentFileData[EquipmentInFile.AntiTank].Length; i++)
            {
                if (Regex.Match(equipmentFileData[EquipmentInFile.AntiTank][i], @"anti_tank_equipment[^_]*?\{").Success)
                {
                    GetEquipmentLocationsHelper(EquipmentName.AntiTank, i, EquipmentInFile.AntiTank);
                }
                else if (Regex.Match(equipmentFileData[EquipmentInFile.AntiTank][i], @"anti_tank_equipment_1.*?\{").Success)
                {
                    GetEquipmentLocationsHelper(EquipmentName.AntiTank1, i, EquipmentInFile.AntiTank);
                }
                else if (Regex.Match(equipmentFileData[EquipmentInFile.AntiTank][i], @"anti_tank_equipment_2.*?\{").Success)
                {
                    GetEquipmentLocationsHelper(EquipmentName.AntiTank2, i, EquipmentInFile.AntiTank);
                }
                else if (Regex.Match(equipmentFileData[EquipmentInFile.AntiTank][i], @"anti_tank_equipment_3.*?\{").Success)
                {
                    GetEquipmentLocationsHelper(EquipmentName.AntiTank3, i, EquipmentInFile.AntiTank);
                }
            }

            for (i = 0; i < equipmentFileData[EquipmentInFile.Artillery].Length; i++)
            {
                if (Regex.Match(equipmentFileData[EquipmentInFile.Artillery][i], @"artillery_equipment[^_]*?\{").Success)
                {
                    GetEquipmentLocationsHelper(EquipmentName.Artillery, i, EquipmentInFile.Artillery);
                }
                else if (Regex.Match(equipmentFileData[EquipmentInFile.Artillery][i], @"artillery_equipment_1.*?\{").Success)
                {
                    GetEquipmentLocationsHelper(EquipmentName.Artillery1, i, EquipmentInFile.Artillery);
                }
                else if (Regex.Match(equipmentFileData[EquipmentInFile.Artillery][i], @"artillery_equipment_2.*?\{").Success)
                {
                    GetEquipmentLocationsHelper(EquipmentName.Artillery2, i, EquipmentInFile.Artillery);
                }
                else if (Regex.Match(equipmentFileData[EquipmentInFile.Artillery][i], @"artillery_equipment_3.*?\{").Success)
                {
                    GetEquipmentLocationsHelper(EquipmentName.Artillery3, i, EquipmentInFile.Artillery);
                }
            }

            for (i = 0; i < equipmentFileData[EquipmentInFile.Artillery].Length; i++)
            {
                if (Regex.Match(equipmentFileData[EquipmentInFile.Artillery][i], @"rocket_artillery_equipment[^_]*?\{").Success)
                {
                    GetEquipmentLocationsHelper(EquipmentName.RocketArtillery, i, EquipmentInFile.Artillery);
                }
                else if (Regex.Match(equipmentFileData[EquipmentInFile.Artillery][i], @"rocket_artillery_equipment_1.*?\{").Success)
                {
                    GetEquipmentLocationsHelper(EquipmentName.RocketArtillery1, i, EquipmentInFile.Artillery);
                }
                else if (Regex.Match(equipmentFileData[EquipmentInFile.Artillery][i], @"rocket_artillery_equipment_2.*?\{").Success)
                {
                    GetEquipmentLocationsHelper(EquipmentName.RocketArtillery2, i, EquipmentInFile.Artillery);
                }
            }

            for (i = 0; i < equipmentFileData[EquipmentInFile.Artillery].Length; i++)
            {
                if (Regex.Match(equipmentFileData[EquipmentInFile.Artillery][i], @"motorized_rocket_equipment[^_]*?\{").Success)
                {
                    GetEquipmentLocationsHelper(EquipmentName.MotorizedRocketArtillery, i, EquipmentInFile.Artillery);
                }
                else if (Regex.Match(equipmentFileData[EquipmentInFile.Artillery][i], @"motorized_rocket_equipment_1.*?\{").Success)
                {
                    GetEquipmentLocationsHelper(EquipmentName.MotorizedRocketArtillery1, i, EquipmentInFile.Artillery);
                }
            }

            for (i = 0; i < equipmentFileData[EquipmentInFile.Mechanized].Length; i++)
            {
                if (Regex.Match(equipmentFileData[EquipmentInFile.Mechanized][i], @"mechanized_equipment[^_]*?\{").Success)
                {
                    GetEquipmentLocationsHelper(EquipmentName.Mechanized, i, EquipmentInFile.Mechanized);
                }
                else if (Regex.Match(equipmentFileData[EquipmentInFile.Mechanized][i], @"mechanized_equipment_1.*?\{").Success)
                {
                    GetEquipmentLocationsHelper(EquipmentName.Mechanized1, i, EquipmentInFile.Mechanized);
                }
                else if (Regex.Match(equipmentFileData[EquipmentInFile.Mechanized][i], @"mechanized_equipment_2.*?\{").Success)
                {
                    GetEquipmentLocationsHelper(EquipmentName.Mechanized2, i, EquipmentInFile.Mechanized);
                }
                else if (Regex.Match(equipmentFileData[EquipmentInFile.Mechanized][i], @"mechanized_equipment_3.*?\{").Success)
                {
                    GetEquipmentLocationsHelper(EquipmentName.Mechanized3, i, EquipmentInFile.Mechanized);
                }
            }

            for (i = 0; i < equipmentFileData[EquipmentInFile.Motorized].Length; i++)
            {
                if (Regex.Match(equipmentFileData[EquipmentInFile.Motorized][i], @"motorized_equipment[^_]*?\{").Success)
                {
                    GetEquipmentLocationsHelper(EquipmentName.Motorized, i, EquipmentInFile.Motorized);
                }
                else if (Regex.Match(equipmentFileData[EquipmentInFile.Motorized][i], @"motorized_equipment_1.*?\{").Success)
                {
                    GetEquipmentLocationsHelper(EquipmentName.Motorized1, i, EquipmentInFile.Motorized);
                }
            }

            for (i = 0; i < equipmentFileData[EquipmentInFile.TankHeavy].Length; i++)
            {
                if (Regex.Match(equipmentFileData[EquipmentInFile.TankHeavy][i], @"heavy_tank_equipment[^_]*?\{").Success)
                {
                    GetEquipmentLocationsHelper(EquipmentName.HeavyTank, i, EquipmentInFile.TankHeavy);
                }
                else if (Regex.Match(equipmentFileData[EquipmentInFile.TankHeavy][i], @"heavy_tank_equipment_1.*?\{").Success)
                {
                    GetEquipmentLocationsHelper(EquipmentName.HeavyTank1, i, EquipmentInFile.TankHeavy);
                }
                else if (Regex.Match(equipmentFileData[EquipmentInFile.TankHeavy][i], @"heavy_tank_equipment_2.*?\{").Success)
                {
                    GetEquipmentLocationsHelper(EquipmentName.HeavyTank2, i, EquipmentInFile.TankHeavy);
                }
                else if (Regex.Match(equipmentFileData[EquipmentInFile.TankHeavy][i], @"heavy_tank_equipment_3.*?\{").Success)
                {
                    GetEquipmentLocationsHelper(EquipmentName.HeavyTank3, i, EquipmentInFile.TankHeavy);
                }
            }

            for (i = 0; i < equipmentFileData[EquipmentInFile.TankHeavy].Length; i++)
            {
                if (Regex.Match(equipmentFileData[EquipmentInFile.TankHeavy][i], @"heavy_tank_artillery_equipment[^_]*?\{").Success)
                {
                    GetEquipmentLocationsHelper(EquipmentName.HeavyTankArtillery, i, EquipmentInFile.TankHeavy);
                }
                else if (Regex.Match(equipmentFileData[EquipmentInFile.TankHeavy][i], @"heavy_tank_artillery_equipment_1.*?\{").Success)
                {
                    GetEquipmentLocationsHelper(EquipmentName.HeavyTankArtillery1, i, EquipmentInFile.TankHeavy);
                }
                else if (Regex.Match(equipmentFileData[EquipmentInFile.TankHeavy][i], @"heavy_tank_artillery_equipment_2.*?\{").Success)
                {
                    GetEquipmentLocationsHelper(EquipmentName.HeavyTankArtillery2, i, EquipmentInFile.TankHeavy);
                }
                else if (Regex.Match(equipmentFileData[EquipmentInFile.TankHeavy][i], @"heavy_tank_artillery_equipment_3.*?\{").Success)
                {
                    GetEquipmentLocationsHelper(EquipmentName.HeavyTankArtillery3, i, EquipmentInFile.TankHeavy);
                }
            }

            for (i = 0; i < equipmentFileData[EquipmentInFile.TankHeavy].Length; i++)
            {
                if (Regex.Match(equipmentFileData[EquipmentInFile.TankHeavy][i], @"heavy_tank_destroyer_equipment[^_]*?\{").Success)
                {
                    GetEquipmentLocationsHelper(EquipmentName.HeavyTankDestroyer, i, EquipmentInFile.TankHeavy);
                }
                else if (Regex.Match(equipmentFileData[EquipmentInFile.TankHeavy][i], @"heavy_tank_destroyer_equipment_1.*?\{").Success)
                {
                    GetEquipmentLocationsHelper(EquipmentName.HeavyTankDestroyer1, i, EquipmentInFile.TankHeavy);
                }
                else if (Regex.Match(equipmentFileData[EquipmentInFile.TankHeavy][i], @"heavy_tank_destroyer_equipment_2.*?\{").Success)
                {
                    GetEquipmentLocationsHelper(EquipmentName.HeavyTankDestroyer2, i, EquipmentInFile.TankHeavy);
                }
                else if (Regex.Match(equipmentFileData[EquipmentInFile.TankHeavy][i], @"heavy_tank_destroyer_equipment_3.*?\{").Success)
                {
                    GetEquipmentLocationsHelper(EquipmentName.HeavyTankDestroyer3, i, EquipmentInFile.TankHeavy);
                }
            }

            for (i = 0; i < equipmentFileData[EquipmentInFile.TankHeavy].Length; i++)
            {
                if (Regex.Match(equipmentFileData[EquipmentInFile.TankHeavy][i], @"heavy_tank_aa_equipment[^_]*?\{").Success)
                {
                    GetEquipmentLocationsHelper(EquipmentName.HeavyTankAntiAir, i, EquipmentInFile.TankHeavy);
                }
                else if (Regex.Match(equipmentFileData[EquipmentInFile.TankHeavy][i], @"heavy_tank_aa_equipment_1.*?\{").Success)
                {
                    GetEquipmentLocationsHelper(EquipmentName.HeavyTankAntiAir1, i, EquipmentInFile.TankHeavy);
                }
                else if (Regex.Match(equipmentFileData[EquipmentInFile.TankHeavy][i], @"heavy_tank_aa_equipment_2.*?\{").Success)
                {
                    GetEquipmentLocationsHelper(EquipmentName.HeavyTankAntiAir2, i, EquipmentInFile.TankHeavy);
                }
                else if (Regex.Match(equipmentFileData[EquipmentInFile.TankHeavy][i], @"heavy_tank_aa_equipment_3.*?\{").Success)
                {
                    GetEquipmentLocationsHelper(EquipmentName.HeavyTankAntiAir3, i, EquipmentInFile.TankHeavy);
                }
            }

            for (i = 0; i < equipmentFileData[EquipmentInFile.TankLight].Length; i++)
            {
                if (Regex.Match(equipmentFileData[EquipmentInFile.TankLight][i], @"light_tank_equipment[^_]*?\{").Success)
                {
                    GetEquipmentLocationsHelper(EquipmentName.LightTank, i, EquipmentInFile.TankLight);
                }
                else if (Regex.Match(equipmentFileData[EquipmentInFile.TankLight][i], @"light_tank_equipment_1.*?\{").Success)
                {
                    GetEquipmentLocationsHelper(EquipmentName.LightTank1, i, EquipmentInFile.TankLight);
                }
                else if (Regex.Match(equipmentFileData[EquipmentInFile.TankLight][i], @"light_tank_equipment_2.*?\{").Success)
                {
                    GetEquipmentLocationsHelper(EquipmentName.LightTank2, i, EquipmentInFile.TankLight);
                }
                else if (Regex.Match(equipmentFileData[EquipmentInFile.TankLight][i], @"light_tank_equipment_3.*?\{").Success)
                {
                    GetEquipmentLocationsHelper(EquipmentName.LightTank3, i, EquipmentInFile.TankLight);
                }
            }

            for (i = 0; i < equipmentFileData[EquipmentInFile.TankLight].Length; i++)
            {
                if (Regex.Match(equipmentFileData[EquipmentInFile.TankLight][i], @"light_tank_artillery_equipment[^_]*?\{").Success)
                {
                    GetEquipmentLocationsHelper(EquipmentName.LightTankArtillery, i, EquipmentInFile.TankLight);
                }
                else if (Regex.Match(equipmentFileData[EquipmentInFile.TankLight][i], @"light_tank_artillery_equipment_1.*?\{").Success)
                {
                    GetEquipmentLocationsHelper(EquipmentName.LightTankArtillery1, i, EquipmentInFile.TankLight);
                }
                else if (Regex.Match(equipmentFileData[EquipmentInFile.TankLight][i], @"light_tank_artillery_equipment_2.*?\{").Success)
                {
                    GetEquipmentLocationsHelper(EquipmentName.LightTankArtillery2, i, EquipmentInFile.TankLight);
                }
                else if (Regex.Match(equipmentFileData[EquipmentInFile.TankLight][i], @"light_tank_artillery_equipment_3.*?\{").Success)
                {
                    GetEquipmentLocationsHelper(EquipmentName.LightTankArtillery3, i, EquipmentInFile.TankLight);
                }
            }

            for (i = 0; i < equipmentFileData[EquipmentInFile.TankLight].Length; i++)
            {
                if (Regex.Match(equipmentFileData[EquipmentInFile.TankLight][i], @"light_tank_destroyer_equipment[^_]*?\{").Success)
                {
                    GetEquipmentLocationsHelper(EquipmentName.LightTankDestroyer, i, EquipmentInFile.TankLight);
                }
                else if (Regex.Match(equipmentFileData[EquipmentInFile.TankLight][i], @"light_tank_destroyer_equipment_1.*?\{").Success)
                {
                    GetEquipmentLocationsHelper(EquipmentName.LightTankDestroyer1, i, EquipmentInFile.TankLight);
                }
                else if (Regex.Match(equipmentFileData[EquipmentInFile.TankLight][i], @"light_tank_destroyer_equipment_2.*?\{").Success)
                {
                    GetEquipmentLocationsHelper(EquipmentName.LightTankDestroyer2, i, EquipmentInFile.TankLight);
                }
                else if (Regex.Match(equipmentFileData[EquipmentInFile.TankLight][i], @"light_tank_destroyer_equipment_3.*?\{").Success)
                {
                    GetEquipmentLocationsHelper(EquipmentName.LightTankDestroyer3, i, EquipmentInFile.TankLight);
                }
            }

            for (i = 0; i < equipmentFileData[EquipmentInFile.TankLight].Length; i++)
            {
                if (Regex.Match(equipmentFileData[EquipmentInFile.TankLight][i], @"light_tank_aa_equipment[^_]*?\{").Success)
                {
                    GetEquipmentLocationsHelper(EquipmentName.LightTankAntiAir, i, EquipmentInFile.TankLight);
                }
                else if (Regex.Match(equipmentFileData[EquipmentInFile.TankLight][i], @"light_tank_aa_equipment_1.*?\{").Success)
                {
                    GetEquipmentLocationsHelper(EquipmentName.LightTankAntiAir1, i, EquipmentInFile.TankLight);
                }
                else if (Regex.Match(equipmentFileData[EquipmentInFile.TankLight][i], @"light_tank_aa_equipment_2.*?\{").Success)
                {
                    GetEquipmentLocationsHelper(EquipmentName.LightTankAntiAir2, i, EquipmentInFile.TankLight);
                }
                else if (Regex.Match(equipmentFileData[EquipmentInFile.TankLight][i], @"light_tank_aa_equipment_3.*?\{").Success)
                {
                    GetEquipmentLocationsHelper(EquipmentName.LightTankAntiAir3, i, EquipmentInFile.TankLight);
                }
            }

            for (i = 0; i < equipmentFileData[EquipmentInFile.TankMedium].Length; i++)
            {
                if (Regex.Match(equipmentFileData[EquipmentInFile.TankMedium][i], @"medium_tank_equipment[^_]*?\{").Success)
                {
                    GetEquipmentLocationsHelper(EquipmentName.MediumTank, i, EquipmentInFile.TankMedium);
                }
                else if (Regex.Match(equipmentFileData[EquipmentInFile.TankMedium][i], @"medium_tank_equipment_1.*?\{").Success)
                {
                    GetEquipmentLocationsHelper(EquipmentName.MediumTank1, i, EquipmentInFile.TankMedium);
                }
                else if (Regex.Match(equipmentFileData[EquipmentInFile.TankMedium][i], @"medium_tank_equipment_2.*?\{").Success)
                {
                    GetEquipmentLocationsHelper(EquipmentName.MediumTank2, i, EquipmentInFile.TankMedium);
                }
                else if (Regex.Match(equipmentFileData[EquipmentInFile.TankMedium][i], @"medium_tank_equipment_3.*?\{").Success)
                {
                    GetEquipmentLocationsHelper(EquipmentName.MediumTank3, i, EquipmentInFile.TankMedium);
                }
            }

            for (i = 0; i < equipmentFileData[EquipmentInFile.TankMedium].Length; i++)
            {
                if (Regex.Match(equipmentFileData[EquipmentInFile.TankMedium][i], @"medium_tank_artillery_equipment[^_]*?\{").Success)
                {
                    GetEquipmentLocationsHelper(EquipmentName.MediumTankArtillery, i, EquipmentInFile.TankMedium);
                }
                else if (Regex.Match(equipmentFileData[EquipmentInFile.TankMedium][i], @"medium_tank_artillery_equipment_1.*?\{").Success)
                {
                    GetEquipmentLocationsHelper(EquipmentName.MediumTankArtillery1, i, EquipmentInFile.TankMedium);
                }
                else if (Regex.Match(equipmentFileData[EquipmentInFile.TankMedium][i], @"medium_tank_artillery_equipment_2.*?\{").Success)
                {
                    GetEquipmentLocationsHelper(EquipmentName.MediumTankArtillery2, i, EquipmentInFile.TankMedium);
                }
                else if (Regex.Match(equipmentFileData[EquipmentInFile.TankMedium][i], @"medium_tank_artillery_equipment_3.*?\{").Success)
                {
                    GetEquipmentLocationsHelper(EquipmentName.MediumTankArtillery3, i, EquipmentInFile.TankMedium);
                }
            }

            for (i = 0; i < equipmentFileData[EquipmentInFile.TankMedium].Length; i++)
            {
                if (Regex.Match(equipmentFileData[EquipmentInFile.TankMedium][i], @"medium_tank_destroyer_equipment[^_]*?\{").Success)
                {
                    GetEquipmentLocationsHelper(EquipmentName.MediumTankDestroyer, i, EquipmentInFile.TankMedium);
                }
                else if (Regex.Match(equipmentFileData[EquipmentInFile.TankMedium][i], @"medium_tank_destroyer_equipment_1.*?\{").Success)
                {
                    GetEquipmentLocationsHelper(EquipmentName.MediumTankDestroyer1, i, EquipmentInFile.TankMedium);
                }
                else if (Regex.Match(equipmentFileData[EquipmentInFile.TankMedium][i], @"medium_tank_destroyer_equipment_2.*?\{").Success)
                {
                    GetEquipmentLocationsHelper(EquipmentName.MediumTankDestroyer2, i, EquipmentInFile.TankMedium);
                }
                else if (Regex.Match(equipmentFileData[EquipmentInFile.TankMedium][i], @"medium_tank_destroyer_equipment_3.*?\{").Success)
                {
                    GetEquipmentLocationsHelper(EquipmentName.MediumTankDestroyer3, i, EquipmentInFile.TankMedium);
                }
            }

            for (i = 0; i < equipmentFileData[EquipmentInFile.TankMedium].Length; i++)
            {
                if (Regex.Match(equipmentFileData[EquipmentInFile.TankMedium][i], @"medium_tank_aa_equipment[^_]*?\{").Success)
                {
                    GetEquipmentLocationsHelper(EquipmentName.MediumTankAntiAir, i, EquipmentInFile.TankMedium);
                }
                else if (Regex.Match(equipmentFileData[EquipmentInFile.TankMedium][i], @"medium_tank_aa_equipment_1.*?\{").Success)
                {
                    GetEquipmentLocationsHelper(EquipmentName.MediumTankAntiAir1, i, EquipmentInFile.TankMedium);
                }
                else if (Regex.Match(equipmentFileData[EquipmentInFile.TankMedium][i], @"medium_tank_aa_equipment_2.*?\{").Success)
                {
                    GetEquipmentLocationsHelper(EquipmentName.MediumTankAntiAir2, i, EquipmentInFile.TankMedium);
                }
                else if (Regex.Match(equipmentFileData[EquipmentInFile.TankMedium][i], @"medium_tank_aa_equipment_3.*?\{").Success)
                {
                    GetEquipmentLocationsHelper(EquipmentName.MediumTankAntiAir3, i, EquipmentInFile.TankMedium);
                }
            }

            for (i = 0; i < equipmentFileData[EquipmentInFile.TankSuperHeavy].Length; i++)
            {
                if (Regex.Match(equipmentFileData[EquipmentInFile.TankSuperHeavy][i], @"super_heavy_tank_equipment[^_]*?\{").Success)
                {
                    GetEquipmentLocationsHelper(EquipmentName.SuperHeavyTank, i, EquipmentInFile.TankSuperHeavy);
                }
                else if (Regex.Match(equipmentFileData[EquipmentInFile.TankSuperHeavy][i], @"super_heavy_tank_equipment_1.*?\{").Success)
                {
                    GetEquipmentLocationsHelper(EquipmentName.SuperHeavyTank1, i, EquipmentInFile.TankSuperHeavy);
                }
            }

            for (i = 0; i < equipmentFileData[EquipmentInFile.TankSuperHeavy].Length; i++)
            {
                if (Regex.Match(equipmentFileData[EquipmentInFile.TankSuperHeavy][i], @"super_heavy_tank_artillery_equipment[^_]*?\{").Success)
                {
                    GetEquipmentLocationsHelper(EquipmentName.SuperHeavyTankArtillery, i, EquipmentInFile.TankSuperHeavy);
                }
                else if (Regex.Match(equipmentFileData[EquipmentInFile.TankSuperHeavy][i], @"super_heavy_tank_artillery_equipment_1.*?\{").Success)
                {
                    GetEquipmentLocationsHelper(EquipmentName.SuperHeavyTankArtillery1, i, EquipmentInFile.TankSuperHeavy);
                }
            }

            for (i = 0; i < equipmentFileData[EquipmentInFile.TankSuperHeavy].Length; i++)
            {
                if (Regex.Match(equipmentFileData[EquipmentInFile.TankSuperHeavy][i], @"super_heavy_tank_destroyer_equipment[^_]*?\{").Success)
                {
                    GetEquipmentLocationsHelper(EquipmentName.SuperHeavyTankDestroyer, i, EquipmentInFile.TankSuperHeavy);
                }
                else if (Regex.Match(equipmentFileData[EquipmentInFile.TankSuperHeavy][i], @"super_heavy_tank_destroyer_equipment_1.*?\{").Success)
                {
                    GetEquipmentLocationsHelper(EquipmentName.SuperHeavyTankDestroyer1, i, EquipmentInFile.TankSuperHeavy);
                }
            }

            for (i = 0; i < equipmentFileData[EquipmentInFile.TankSuperHeavy].Length; i++)
            {
                if (Regex.Match(equipmentFileData[EquipmentInFile.TankSuperHeavy][i], @"super_heavy_tank_aa_equipment[^_]*?\{").Success)
                {
                    GetEquipmentLocationsHelper(EquipmentName.SuperHeavyTankAntiAir, i, EquipmentInFile.TankSuperHeavy);
                }
                else if (Regex.Match(equipmentFileData[EquipmentInFile.TankSuperHeavy][i], @"super_heavy_tank_aa_equipment_1.*?\{").Success)
                {
                    GetEquipmentLocationsHelper(EquipmentName.SuperHeavyTankAntiAir1, i, EquipmentInFile.TankSuperHeavy);
                }
            }

            for (i = 0; i < equipmentFileData[EquipmentInFile.TankModern].Length; i++)
            {
                if (Regex.Match(equipmentFileData[EquipmentInFile.TankModern][i], @"modern_tank_equipment[^_]*?\{").Success)
                {
                    GetEquipmentLocationsHelper(EquipmentName.ModernTank, i, EquipmentInFile.TankModern);
                }
                else if (Regex.Match(equipmentFileData[EquipmentInFile.TankModern][i], @"modern_tank_equipment_1.*?\{").Success)
                {
                    GetEquipmentLocationsHelper(EquipmentName.ModernTank1, i, EquipmentInFile.TankModern);
                }
            }

            for (i = 0; i < equipmentFileData[EquipmentInFile.TankModern].Length; i++)
            {
                if (Regex.Match(equipmentFileData[EquipmentInFile.TankModern][i], @"modern_tank_artillery_equipment[^_]*?\{").Success)
                {
                    GetEquipmentLocationsHelper(EquipmentName.ModernTankArtillery, i, EquipmentInFile.TankModern);
                }
                else if (Regex.Match(equipmentFileData[EquipmentInFile.TankModern][i], @"modern_tank_artillery_equipment_1.*?\{").Success)
                {
                    GetEquipmentLocationsHelper(EquipmentName.ModernTankArtillery1, i, EquipmentInFile.TankModern);
                }
            }

            for (i = 0; i < equipmentFileData[EquipmentInFile.TankModern].Length; i++)
            {
                if (Regex.Match(equipmentFileData[EquipmentInFile.TankModern][i], @"modern_tank_destroyer_equipment[^_]*?\{").Success)
                {
                    GetEquipmentLocationsHelper(EquipmentName.ModernTankDestroyer, i, EquipmentInFile.TankModern);
                }
                else if (Regex.Match(equipmentFileData[EquipmentInFile.TankModern][i], @"modern_tank_destroyer_equipment_1.*?\{").Success)
                {
                    GetEquipmentLocationsHelper(EquipmentName.ModernTankDestroyer1, i, EquipmentInFile.TankModern);
                }
            }

            for (i = 0; i < equipmentFileData[EquipmentInFile.TankModern].Length; i++)
            {
                if (Regex.Match(equipmentFileData[EquipmentInFile.TankModern][i], @"modern_tank_aa_equipment[^_]*?\{").Success)
                {
                    GetEquipmentLocationsHelper(EquipmentName.ModernTankAntiAir, i, EquipmentInFile.TankModern);
                }
                else if (Regex.Match(equipmentFileData[EquipmentInFile.TankModern][i], @"modern_tank_aa_equipment_1.*?\{").Success)
                {
                    GetEquipmentLocationsHelper(EquipmentName.ModernTankAntiAir1, i, EquipmentInFile.TankModern);
                }
            }
        }

        private void GetUnitLocationsHelper(UnitName unitName, int startLocation, UnitsInFile unitsInFile)
        {
            unitStart[unitName] = startLocation;
            int bracketCount = 1;
            for (int i = startLocation + 1; i < unitFileData[unitsInFile].Length; i++)
            {
                for (int y = 0; y < unitFileData[unitsInFile][i].Length; y++)
                {
                    if (unitFileData[unitsInFile][i][y] == '{') { bracketCount++; }
                    if (unitFileData[unitsInFile][i][y] == '}') { bracketCount--; }
                }
                if (bracketCount == 0)
                {
                    unitEnd[unitName] = i;
                    break;
                }
            }
        }
        private void GetUnitLocations()
        {
            int i;
            for (i = 0; i < unitFileData[UnitsInFile.Infantry].Length; i++)
            {
                if (Regex.Match(unitFileData[UnitsInFile.Infantry][i], @"infantry.*?\{").Success)
                {
                    GetUnitLocationsHelper(UnitName.Infantry, i, UnitsInFile.Infantry);
                }
                else if (Regex.Match(unitFileData[UnitsInFile.Infantry][i], @"bicycle_battalion.*?\{").Success)
                {
                    GetUnitLocationsHelper(UnitName.BicycleBattalion, i, UnitsInFile.Infantry);
                }
                else if (Regex.Match(unitFileData[UnitsInFile.Infantry][i], @"marine.*?\{").Success)
                {
                    GetUnitLocationsHelper(UnitName.Marines, i, UnitsInFile.Infantry);
                }
                else if (Regex.Match(unitFileData[UnitsInFile.Infantry][i], @"mountaineers.*?\{").Success)
                {
                    GetUnitLocationsHelper(UnitName.Mountaineers, i, UnitsInFile.Infantry);
                }
                else if (Regex.Match(unitFileData[UnitsInFile.Infantry][i], @"paratrooper.*?\{").Success)
                {
                    GetUnitLocationsHelper(UnitName.Paratroopers, i, UnitsInFile.Infantry);
                }
                else if (Regex.Match(unitFileData[UnitsInFile.Infantry][i], @"motorized.*?\{").Success)
                {
                    GetUnitLocationsHelper(UnitName.Motorized, i, UnitsInFile.Infantry);
                }
                else if (Regex.Match(unitFileData[UnitsInFile.Infantry][i], @"mechanized.*?\{").Success)
                {
                    GetUnitLocationsHelper(UnitName.Mechanized, i, UnitsInFile.Infantry);
                }
            }

            for (i = 0; i < unitFileData[UnitsInFile.Cavalry].Length; i++)
            {
                if (Regex.Match(unitFileData[UnitsInFile.Cavalry][i], @"cavalry.*?\{").Success)
                {
                    GetUnitLocationsHelper(UnitName.Cavalry, i, UnitsInFile.Cavalry);
                }
            }

            for (i = 0; i < unitFileData[UnitsInFile.LightArmour].Length; i++)
            {
                if (Regex.Match(unitFileData[UnitsInFile.LightArmour][i], @"light_armor.*?\{").Success)
                {
                    GetUnitLocationsHelper(UnitName.LightTank, i, UnitsInFile.LightArmour);
                }
            }

            for (i = 0; i < unitFileData[UnitsInFile.MediumArmour].Length; i++)
            {
                if (Regex.Match(unitFileData[UnitsInFile.MediumArmour][i], @"medium_armor.*?\{").Success)
                {
                    GetUnitLocationsHelper(UnitName.MediumTank, i, UnitsInFile.MediumArmour);
                }
            }

            for (i = 0; i < unitFileData[UnitsInFile.HeavyArmour].Length; i++)
            {
                if (Regex.Match(unitFileData[UnitsInFile.HeavyArmour][i], @"^[^_]*?heavy_armor.*?\{").Success)
                {
                    GetUnitLocationsHelper(UnitName.HeavyTank, i, UnitsInFile.HeavyArmour);
                }
            }

            for (i = 0; i < unitFileData[UnitsInFile.SuperHeavyArmour].Length; i++)
            {
                if (Regex.Match(unitFileData[UnitsInFile.SuperHeavyArmour][i], @"super_heavy_armor.*?\{").Success)
                {
                    GetUnitLocationsHelper(UnitName.SuperHeavyTank, i, UnitsInFile.SuperHeavyArmour);
                }
            }

            for (i = 0; i < unitFileData[UnitsInFile.ModernArmour].Length; i++)
            {
                if (Regex.Match(unitFileData[UnitsInFile.ModernArmour][i], @"modern_armor.*?\{").Success)
                {
                    GetUnitLocationsHelper(UnitName.ModernTank, i, UnitsInFile.ModernArmour);
                }
            }

            for (i = 0; i < unitFileData[UnitsInFile.AntiTankBrigade].Length; i++)
            {
                if (Regex.Match(unitFileData[UnitsInFile.AntiTankBrigade][i], @"anti_tank_brigade.*?\{").Success)
                {
                    GetUnitLocationsHelper(UnitName.AntiTank, i, UnitsInFile.AntiTankBrigade);
                }
            }

            for (i = 0; i < unitFileData[UnitsInFile.AntiAirBrigade].Length; i++)
            {
                if (Regex.Match(unitFileData[UnitsInFile.AntiAirBrigade][i], @"anti_air_brigade.*?\{").Success)
                {
                    GetUnitLocationsHelper(UnitName.AntiAir, i, UnitsInFile.AntiAirBrigade);
                }
            }

            for (i = 0; i < unitFileData[UnitsInFile.ArtilleryBrigade].Length; i++)
            {
                if (Regex.Match(unitFileData[UnitsInFile.ArtilleryBrigade][i], @"^[^_]*?artillery_brigade.*?\{").Success)
                {
                    GetUnitLocationsHelper(UnitName.Artillery, i, UnitsInFile.ArtilleryBrigade);
                }
                else if (Regex.Match(unitFileData[UnitsInFile.ArtilleryBrigade][i], @"rocket_artillery_brigade.*?\{").Success)
                {
                    GetUnitLocationsHelper(UnitName.RocketArtillery, i, UnitsInFile.ArtilleryBrigade);
                }
                else if (Regex.Match(unitFileData[UnitsInFile.ArtilleryBrigade][i], @"motorized_rocket_brigade.*?\{").Success)
                {
                    GetUnitLocationsHelper(UnitName.MotorizedRocketArtillery, i, UnitsInFile.ArtilleryBrigade);
                }
            }

            for (i = 0; i < unitFileData[UnitsInFile.SPAntiAirBrigade].Length; i++)
            {
                if (Regex.Match(unitFileData[UnitsInFile.SPAntiAirBrigade][i], @"light_sp_anti_air_brigade.*?\{").Success)
                {
                    GetUnitLocationsHelper(UnitName.LightSPAntiAir, i, UnitsInFile.SPAntiAirBrigade);
                }
                else if (Regex.Match(unitFileData[UnitsInFile.SPAntiAirBrigade][i], @"medium_sp_anti_air_brigade.*?\{").Success)
                {
                    GetUnitLocationsHelper(UnitName.MediumSPAntiAir, i, UnitsInFile.SPAntiAirBrigade);
                }
                else if (Regex.Match(unitFileData[UnitsInFile.SPAntiAirBrigade][i], @"^[^_]*?heavy_sp_anti_air_brigade.*?\{").Success)
                {
                    GetUnitLocationsHelper(UnitName.HeavySPAntiAir, i, UnitsInFile.SPAntiAirBrigade);
                }
                else if (Regex.Match(unitFileData[UnitsInFile.SPAntiAirBrigade][i], @"super_heavy_sp_anti_air_brigade.*?\{").Success)
                {
                    GetUnitLocationsHelper(UnitName.SuperHeavySPAntiAir, i, UnitsInFile.SPAntiAirBrigade);
                }
                else if (Regex.Match(unitFileData[UnitsInFile.SPAntiAirBrigade][i], @"modern_sp_anti_air_brigade.*?\{").Success)
                {
                    GetUnitLocationsHelper(UnitName.ModernSPAntiAir, i, UnitsInFile.SPAntiAirBrigade);
                }
            }

            for (i = 0; i < unitFileData[UnitsInFile.SPArtilleryBrigade].Length; i++)
            {
                if (Regex.Match(unitFileData[UnitsInFile.SPArtilleryBrigade][i], @"light_sp_artillery_brigade.*?\{").Success)
                {
                    GetUnitLocationsHelper(UnitName.LightSPArtillery, i, UnitsInFile.SPArtilleryBrigade);
                }
                else if (Regex.Match(unitFileData[UnitsInFile.SPArtilleryBrigade][i], @"medium_sp_artillery_brigade.*?\{").Success)
                {
                    GetUnitLocationsHelper(UnitName.MediumSPArtillery, i, UnitsInFile.SPArtilleryBrigade);
                }
                else if (Regex.Match(unitFileData[UnitsInFile.SPArtilleryBrigade][i], @"^[^_]*?heavy_sp_artillery_brigade.*?\{").Success)
                {
                    GetUnitLocationsHelper(UnitName.HeavySPArtillery, i, UnitsInFile.SPArtilleryBrigade);
                }
                else if (Regex.Match(unitFileData[UnitsInFile.SPArtilleryBrigade][i], @"super_heavy_sp_artillery_brigade.*?\{").Success)
                {
                    GetUnitLocationsHelper(UnitName.SuperHeavySPArtillery, i, UnitsInFile.SPArtilleryBrigade);
                }
                else if (Regex.Match(unitFileData[UnitsInFile.SPArtilleryBrigade][i], @"modern_sp_artillery_brigade.*?\{").Success)
                {
                    GetUnitLocationsHelper(UnitName.ModernSPArtillery, i, UnitsInFile.SPArtilleryBrigade);
                }
            }

            for (i = 0; i < unitFileData[UnitsInFile.TankDestroyerBrigade].Length; i++)
            {
                if (Regex.Match(unitFileData[UnitsInFile.TankDestroyerBrigade][i], @"light_tank_destroyer_brigade.*?\{").Success)
                {
                    GetUnitLocationsHelper(UnitName.LightTankDestroyer, i, UnitsInFile.TankDestroyerBrigade);
                }
                else if (Regex.Match(unitFileData[UnitsInFile.TankDestroyerBrigade][i], @"medium_tank_destroyer_brigade.*?\{").Success)
                {
                    GetUnitLocationsHelper(UnitName.MediumTankDestroyer, i, UnitsInFile.TankDestroyerBrigade);
                }
                else if (Regex.Match(unitFileData[UnitsInFile.TankDestroyerBrigade][i], @"^[^_]*?heavy_tank_destroyer_brigade.*?\{").Success)
                {
                    GetUnitLocationsHelper(UnitName.HeavyTankDestroyer, i, UnitsInFile.TankDestroyerBrigade);
                }
                else if (Regex.Match(unitFileData[UnitsInFile.TankDestroyerBrigade][i], @"super_heavy_tank_destroyer_brigade.*?\{").Success)
                {
                    GetUnitLocationsHelper(UnitName.SuperHeavyTankDestroyer, i, UnitsInFile.TankDestroyerBrigade);
                }
                else if (Regex.Match(unitFileData[UnitsInFile.TankDestroyerBrigade][i], @"modern_tank_destroyer_brigade.*?\{").Success)
                {
                    GetUnitLocationsHelper(UnitName.ModernTankDestroyer, i, UnitsInFile.TankDestroyerBrigade);
                }
            }
        }
    }
}