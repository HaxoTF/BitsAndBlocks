using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BitsAndBlocks;

public class BlockListInfo {
    public string BlockName {get; set;}
    public string Texture   {get; set;}
}

public partial class Block
{
    public static Dictionary<int, BlockListInfo> BlockIDs = new Dictionary<int, BlockListInfo> {
        { 0, new BlockListInfo { BlockName = "air",    Texture = null     } },
        { 1, new BlockListInfo { BlockName = "stone",  Texture = "stone"  } },
        { 2, new BlockListInfo { BlockName = "dirt",   Texture = "dirt"   } },
        { 3, new BlockListInfo { BlockName = "grass",  Texture = "grass"  } },
        { 4, new BlockListInfo { BlockName = "sand",   Texture = "sand"   } },
        { 5, new BlockListInfo { BlockName = "planks", Texture = "planks" } }
    };


    // Gets index of BlockListInfo by name (BlockName)
    public static int GetIndexOfName(string _block_name) {
        foreach ( KeyValuePair<int, BlockListInfo> kvp in BlockIDs ) {
            if (kvp.Value.BlockName == _block_name) { return kvp.Key; }
        }
        return -1;
    }
}