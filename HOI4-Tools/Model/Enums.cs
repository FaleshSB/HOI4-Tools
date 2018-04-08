using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOI4_Tools.Model
{
    public enum PageName { DivisionDesigner, Construction }
    public enum UnitName { AntiAir, AntiTank, Artillery, BicycleBattalion, Cavalry, HeavySPAntiAir, HeavySPArtillery,
                           HeavyTank, HeavyTankDestroyer, Infantry, LightSPAntiAir, LightSPArtillery, LightTank,
                           LightTankDestroyer, Marines, Mechanized, MediumSPArtillery, MediumTank, MediumTankDestroyer,
                           ModernSPAntiAir, ModernSPArtillery, ModernTank, ModernTankDestroyer, Motorized, MotorizedRocketArtillery,
                           Mountaineers, Paratroopers, RocketArtillery, SuperHeavySPAntiAir, SuperHeavySPArtillery, SuperHeavyTank,
                           SuperHeavyTankDestroyer, MediumSPAntiAir }
    public enum ButtonName { AntiAir, AntiTank, Artillery, BicycleBattalion, Cavalry, HeavySPAntiAir, HeavySPArtillery,
                             HeavyTank, HeavyTankDestroyer, Infantry, LightSPAntiAir, LightSPArtillery, LightTank,
                             LightTankDestroyer, Marines, Mechanized, MediumSPArtillery, MediumTank, MediumTankDestroyer,
                             ModernSPAntiAir, ModernSPArtillery, ModernTank, ModernTankDestroyer, Motorized, MotorizedRocketArtillery,
                             Mountaineers, Paratroopers, RocketArtillery, SuperHeavySPAntiAir, SuperHeavySPArtillery, SuperHeavyTank,
                             SuperHeavyTankDestroyer, AddUnit, MediumSPAntiAir }
    public enum EquipmentName { Infantry1, Infantry0, Infantry, Infantry2, Infantry3, AntiAir, AntiAir1, AntiAir2, AntiAir3,
                                AntiTank, AntiTank1, AntiTank2, AntiTank3, Artillery, Artillery1, Artillery2, Artillery3,
                                RocketArtillery, RocketArtillery1, RocketArtillery2, MotorizedRocketArtillery, MotorizedRocketArtillery1,
                                Mechanized, Mechanized1, Mechanized2, Mechanized3, Motorized, Motorized1, HeavyTank, HeavyTank1, HeavyTank2,
                                HeavyTank3, HeavyTankArtillery, HeavyTankArtillery1, HeavyTankArtillery2, HeavyTankArtillery3, HeavyTankDestroyer,
                                HeavyTankDestroyer1, HeavyTankDestroyer2, HeavyTankDestroyer3, HeavyTankAntiAir, HeavyTankAntiAir1, HeavyTankAntiAir2,
                                HeavyTankAntiAir3, LightTank, LightTank1, LightTank2, LightTank3, LightTankArtillery, LightTankArtillery1,
                                LightTankArtillery2, LightTankArtillery3, LightTankDestroyer, LightTankDestroyer1, LightTankDestroyer2,
                                LightTankDestroyer3, LightTankAntiAir, LightTankAntiAir1, LightTankAntiAir2, LightTankAntiAir3, MediumTank,
                                MediumTank1, MediumTank2, MediumTank3, MediumTankArtillery, MediumTankArtillery1, MediumTankArtillery2,
                                MediumTankArtillery3, MediumTankDestroyer, MediumTankDestroyer1, MediumTankDestroyer2, MediumTankDestroyer3,
                                MediumTankAntiAir, MediumTankAntiAir1, MediumTankAntiAir2, MediumTankAntiAir3, SuperHeavyTank, SuperHeavyTank1,
                                SuperHeavyTankArtillery, SuperHeavyTankArtillery1, SuperHeavyTankDestroyer, SuperHeavyTankDestroyer1,
                                SuperHeavyTankAntiAir, SuperHeavyTankAntiAir1, ModernTank, ModernTank1, ModernTankArtillery, ModernTankArtillery1,
                                ModernTankDestroyer, ModernTankDestroyer1, ModernTankAntiAir, ModernTankAntiAir1 }
    public enum EquipmentType { Infantry }
    public enum EquipmentInFile { Infantry, AntiAir, AntiTank, Artillery, Mechanized, Motorized, TankHeavy, TankLight, TankMedium,
                                  TankModern, TankSuperHeavy}
    public enum UnitsInFile { Infantry, Cavalry, LightArmour, MediumArmour, HeavyArmour, SuperHeavyArmour, ModernArmour,
                              ArtilleryBrigade, AntiTankBrigade, AntiAirBrigade, SPAntiAirBrigade, SPArtilleryBrigade, TankDestroyerBrigade }
    public enum TerrainType { Forest, Hills, Mountain, Marsh, Plains, Urban, Desert, River, Amphibious }
    public enum TransportType { Motorized, Mechanized }
}