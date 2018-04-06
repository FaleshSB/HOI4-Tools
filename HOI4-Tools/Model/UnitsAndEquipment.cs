using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOI4_Tools.Model
{
    static class UnitsAndEquipment
    {
        public static Dictionary<UnitName, Unit> units = new Dictionary<UnitName, Unit>();
        public static Dictionary<EquipmentType, Dictionary<int, Equipment>> equipment = new Dictionary<EquipmentType, Dictionary<int, Equipment>>();

        public static Dictionary<int, Equipment> GetEquipment(UnitName unitName)
        {
            switch (unitName)
            {


                case UnitName.Infantry:
                case UnitName.BicycleBattalion:
                case UnitName.Cavalry:
                case UnitName.Marines:
                case UnitName.Mountaineers:
                case UnitName.Paratroopers:
                    return equipment[EquipmentType.Infantry];


                case UnitName.AntiAir:
                    break;
                case UnitName.AntiTank:
                    break;
                case UnitName.Artillery:
                    break;
                case UnitName.HeavySPAntiAir:
                    break;
                case UnitName.HeavySPArtillery:
                    break;
                case UnitName.HeavyTank:
                    break;
                case UnitName.HeavyTankDestroyer:
                    break;
                case UnitName.LightSPAntiAir:
                    break;
                case UnitName.LightSPArtillery:
                    break;
                case UnitName.LightTank:
                    break;
                case UnitName.LightTankDestroyer:
                    break;
                case UnitName.Mechanized:
                    break;
                case UnitName.MediumSPArtillery:
                    break;
                case UnitName.MediumTank:
                    break;
                case UnitName.MediumTankDestroyer:
                    break;
                case UnitName.ModernSPAntiAir:
                    break;
                case UnitName.ModernSPArtillery:
                    break;
                case UnitName.ModernTank:
                    break;
                case UnitName.ModernTankDestroyer:
                    break;
                case UnitName.Motorized:
                    break;
                case UnitName.MotorizedRocketArtillery:
                    break;
                case UnitName.RocketArtillery:
                    break;
                case UnitName.SuperHeavySPAntiAir:
                    break;
                case UnitName.SuperHeavySPArtillery:
                    break;
                case UnitName.SuperHeavyTank:
                    break;
                case UnitName.SuperHeavyTankDestroyer:
                    break;
                case UnitName.MediumSPAntiAir:
                    break;
            }
            return null;
        }
    }
}
