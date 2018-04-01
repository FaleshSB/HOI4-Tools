using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOI4_Tools.Model
{
    class Equipment
    {
        public int manpower;
        public int trainingTime;
        public int maxOrganisation;
        public int combatWidth;
        public int suppression;
        public int infantryEquipment;
        public int supportEquipment;
        public int motorizedEquipment;
        public int mechanizedEquipment;

        public float weight;
        public float supplyConsumption;
        public float maxSpeed;
        public float breakthrough;
        public float hardness;

        public bool canBeParachuted = false;

        public TransportType transportType;

        public Dictionary<TerrainType, TerrainStats> terrainStats = new Dictionary<TerrainType, TerrainStats>();
    }
}
