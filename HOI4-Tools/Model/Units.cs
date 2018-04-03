using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOI4_Tools.Model
{
    class Units
    {
        private List<Unit> units = new List<Unit>();

        public void AddUnit(Unit unit)
        {
            units.Add(unit);
        }
    }
}
