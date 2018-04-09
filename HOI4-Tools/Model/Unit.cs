using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOI4_Tools.Model
{
    class Unit
    {
        public float manpower = 0;
        public float trainingTime = 0;
        public float maxOrganisation = 0;
        public float combatWidth = 0;
        public float suppression = 0;
        public float infantryEquipment = 0;
        public float supportEquipment = 0;
        public float motorizedEquipment = 0;
        public float mechanizedEquipment = 0;

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