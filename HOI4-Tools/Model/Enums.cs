using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOI4_Tools.Model
{
    public enum PageName { DivisionDesigner, Construction }
    public enum UnitName {
        Mechanized, Motorized, Paratrooper, Mountaineers, Marine, BicycleBattalion, AirTransport, AntiAir, AntiTank, Armor, Artillery, CapitalShop, Carrier, Cas, Fighter, Infantry, Interceptor,
                           Missile, NavalBomber, ScreenShip, StrategicBomber, Submarine, Suicide, Support, TacticalBomber }
    public enum UnitType { Armor, Infantry, Mobile, Support }
    public enum UnitIcon { Armored, Infantry, Other, Ship, Transport, Uboat }
    public enum TerrainType { Forest, Hills, Mountain, Marsh, Plains, Urban, Desert, River, Amphibious }
}