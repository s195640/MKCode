using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cna.poo {

    [Serializable]
    public class MapHexVO {
        /*
             0 1
            2 3 4
             5 6
        */
        private MapHexId_Enum hexId;
        private Image_Enum[] terrainList;
        private Image_Enum[] structureList;

        public MapHexId_Enum HexId { get => hexId; set => hexId = value; }
        public Image_Enum[] TerrainList { get => terrainList; set => terrainList = value; }
        public Image_Enum[] StructureList { get => structureList; set => structureList = value; }

        public MapHexVO(MapHexId_Enum hexId, Image_Enum[] terrainList, Image_Enum[] structureList) {
            this.HexId = hexId;
            this.TerrainList = terrainList;
            this.StructureList = structureList;
        }
    }
}
