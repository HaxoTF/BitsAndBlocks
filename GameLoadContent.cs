using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BitsAndBlocks;

public partial class Game1
{
    Dictionary<string, Texture2D> TXT_Blocks;

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // Blocks Load
        TXT_Blocks = new Dictionary<string, Texture2D> {
            { "stone", Content.Load<Texture2D>("Blocks\\stone") },
            { "dirt",  Content.Load<Texture2D>("Blocks\\dirt")  }
        };
    }
}