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
                           SuperHeavyTankDestroyer }
    public enum ButtonName { AddUnit, Mechanized, Motorized, Paratrooper, Mountaineers, Marine, BicycleBattalion, Infantry }
    public enum UnitType { Armor, Infantry, Mobile, Support }
    public enum TerrainType { Forest, Hills, Mountain, Marsh, Plains, Urban, Desert, River, Amphibious }
    public enum TransportType { Motorized, Mechanized }
}