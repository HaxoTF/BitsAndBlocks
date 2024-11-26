using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BitsAndBlocks;

public partial class Game1 : Game
{

    // Input handling
    float deltaTime = 0f;
    InputHandler _input = new InputHandler();

    protected override void Update(GameTime gameTime)
    { deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

        // States
        _input.UpdateState();

        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        CamPosition = Stage.MoveTowardsSmoothVector2(CamPosition, PlayerPosition, deltaTime*4);
        
        // Building
        Vector2 block_index = ScreenToWorldPos(_input.GetMousePos());
        int bix = (int)block_index.X; int biy = (int)block_index.Y;


        // Block placing
        if (PosInBorder(bix, biy) && Vector2.Distance(PlayerPosition, block_index) <= 10f) {

            if (_input.LeftDown()) {
                world[biy, bix] = 0;
            }
            else if (_input.RightDown() && !PlrCollidesWithBlock(PlayerPosition, bix, biy, true)) {
                world[biy, bix] = curr_block;
            }
        }

        // Scroll
        curr_block += _input.scroll_delta / 120;
        curr_block = Math.Clamp(curr_block, 1, 5);

        HandlePlayer();

        // Dead End
        base.Update(gameTime);
    }
}
