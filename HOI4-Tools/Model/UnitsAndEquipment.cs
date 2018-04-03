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
        public static Dictionary<UnitName, Dictionary<int, Equipment>> equipment = new Dictionary<UnitName, Dictionary<int, Equipment>>();
    }
}
