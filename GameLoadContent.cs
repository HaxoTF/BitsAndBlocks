using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BitsAndBlocks;

public partial class Game1
{
    // Variables
    const int block_size = 8;

    // Textures
    Texture2D TXT_Player;
    Dictionary<string, Texture2D> TXT_Blocks;

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // Singular
        TXT_Player = Content.Load<Texture2D>("player");

        // Blocks Load
        TXT_Blocks = new Dictionary<string, Texture2D> {
            { "stone",  Content.Load<Texture2D>("Blocks\\stone")  },
            { "dirt",   Content.Load<Texture2D>("Blocks\\dirt")   },
            { "grass",  Content.Load<Texture2D>("Blocks\\grass")  },
            { "sand",   Content.Load<Texture2D>("Blocks\\sand")   },
            { "planks", Content.Load<Texture2D>("Blocks\\planks") },
        };
    }
}