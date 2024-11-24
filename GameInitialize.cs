using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BitsAndBlocks;

public partial class Game1 {

    Random rand = new Random();

    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    // Screen Shorts idk
    int WIN_WIDTH  = 1280;
    int WIN_HEIGHT = 720;

    // World Variables
    int WRLD_WIDTH  = 100;
    int WRLD_HEIGHT = 100;
    int[,] world;

    // Game Controls
    int curr_block = 1;

    public Game1()
    {
        // Technical
        _graphics = new GraphicsDeviceManager(this);
        _graphics.PreferredBackBufferWidth  = WIN_WIDTH;
        _graphics.PreferredBackBufferHeight = WIN_HEIGHT;
        // _graphics.IsFullScreen = true;
        
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        base.Initialize();
        GenerateWorld();
    }
}