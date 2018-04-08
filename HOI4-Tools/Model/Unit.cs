using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOI4_Tools.Model
{
    class Unit
    {
        public int manpower = 0;
        public int trainingTime = 0;
        public int maxOrganisation = 0;
        public int combatWidth = 0;
        public int suppression = 0;
        public int infantryEquipment = 0;
        public int supportEquipment = 0;
        public int motorizedEquipment = 0;
        public int mechanizedEquipment = 0;

        public float weight = 0;
        public float maxStrength = 0;
        public float supplyConsumption = 0;
        public float maxSpeed = 0;
        public float breakthrough = 0;
        public float hardness = 0;

        public bool canBeParachuted = false;

        public Dictionary<TerrainType, TerrainStats> terrainStats = new Dictionary<TerrainType, TerrainStats>();
    }
}