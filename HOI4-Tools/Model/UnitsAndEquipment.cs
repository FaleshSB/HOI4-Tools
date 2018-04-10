using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOI4_Tools.Model
{
    static class UnitsAndEquipment
    {
        public static Dictionary<UnitName, UnitOrEquipment> units = new Dictionary<UnitName, UnitOrEquipment>();
        public static Dictionary<EquipmentName, Dictionary<float, UnitOrEquipment>> equipment = new Dictionary<EquipmentName, Dictionary<float, UnitOrEquipment>>();

        public static void test()
        {

        }
    
        public static Dictionary<float, UnitOrEquipment> GetEquipment(UnitName unitName)
        {
            switch (unitName)
            {
                case UnitName.Infantry:
                case UnitName.BicycleBattalion:
                case UnitName.Cavalry:
                case UnitName.Marines:
                case UnitName.Mountaineers:
                case UnitName.Paratroopers:
                    return equipment[EquipmentName.Infantry];
                case UnitName.AntiAir:
                    return equipment[EquipmentName.AntiAir];
                case UnitName.AntiTank:
                    return equipment[EquipmentName.AntiTank];
                case UnitName.Artillery:
                    return equipment[EquipmentName.Artillery];
                case UnitName.HeavySPAntiAir:
                    return equipment[EquipmentName.HeavyTankAntiAir];
                case UnitName.HeavySPArtillery:
                    return equipment[EquipmentName.HeavyTankArtillery];
                case UnitName.HeavyTank:
                    return equipment[EquipmentName.HeavyTank];
                case UnitName.HeavyTankDestroyer:
                    return equipment[EquipmentName.HeavyTankDestroyer];
                case UnitName.LightSPAntiAir:
                    return equipment[EquipmentName.LightTankAntiAir];
                case UnitName.LightSPArtillery:
                    return equipment[EquipmentName.LightTankArtillery];
                case UnitName.LightTank:
                    return equipment[EquipmentName.LightTank];
                case UnitName.LightTankDestroyer:
                    return equipment[EquipmentName.LightTankDestroyer];
                case UnitName.Mechanized:
                    return equipment[EquipmentName.Mechanized];
                case UnitName.MediumSPArtillery:
                    return equipment[EquipmentName.MediumTankArtillery];
                case UnitName.MediumTank:
                    return equipment[EquipmentName.MediumTank];
                case UnitName.MediumTankDestroyer:
                    return equipment[EquipmentName.MediumTankDestroyer];
                case UnitName.ModernSPAntiAir:
                    return equipment[EquipmentName.ModernTankAntiAir];
                case UnitName.ModernSPArtillery:
                    return equipment[EquipmentName.ModernTankArtillery];
                case UnitName.ModernTank:
                    return equipment[EquipmentName.ModernTank];
                case UnitName.ModernTankDestroyer:
                    return equipment[EquipmentName.ModernTankDestroyer];
                case UnitName.Motorized:
                    return equipment[EquipmentName.Motorized];
                case UnitName.MotorizedRocketArtillery:
                    return equipment[EquipmentName.MotorizedRocketArtillery];
                case UnitName.RocketArtillery:
                    return equipment[EquipmentName.RocketArtillery];
                case UnitName.SuperHeavySPAntiAir:
                    return equipment[EquipmentName.SuperHeavyTankAntiAir];
                case UnitName.SuperHeavySPArtillery:
                    return equipment[EquipmentName.SuperHeavyTankArtillery];
                case UnitName.SuperHeavyTank:
                    return equipment[EquipmentName.SuperHeavyTank];
                case UnitName.SuperHeavyTankDestroyer:
                    return equipment[EquipmentName.SuperHeavyTankDestroyer];
                case UnitName.MediumSPAntiAir:
                    return equipment[EquipmentName.MediumTankAntiAir];
            }
            return null;
        }
    }
}
