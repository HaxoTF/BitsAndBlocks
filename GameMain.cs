using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BitsAndBlocks;

public partial class Game1 : Game
{
    protected override void Update(GameTime gameTime)
    { float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

        // States
        MouseState mouse_state = Mouse.GetState();
        KeyboardState keystate = Keyboard.GetState();
        int scroll_delta = mouse_state.ScrollWheelValue - prev_scroll_value;

        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();


        float cam_speed = 1000f;
        if (keystate.IsKeyDown(Keys.W)) { CamPosition.Y -= cam_speed * deltaTime; }
        if (keystate.IsKeyDown(Keys.S)) { CamPosition.Y += cam_speed * deltaTime; }
        if (keystate.IsKeyDown(Keys.A)) { CamPosition.X -= cam_speed * deltaTime; }
        if (keystate.IsKeyDown(Keys.D)) { CamPosition.X += cam_speed * deltaTime; }

        // Building
        Vector2 block_index = GetBlockByScreenPos(mouse_state.X, mouse_state.Y, 4f);
        int bix = (int)block_index.X; int biy = (int)block_index.Y;
        if (PosInBorder(bix, biy)) {

            if (mouse_state.LeftButton == ButtonState.Pressed) {
                world[biy, bix] = 0;
            }
            else if (mouse_state.RightButton == ButtonState.Pressed) {
                world[biy, bix] = curr_block;
            }

        }

        // Scroll
        curr_block += scroll_delta;
        curr_block = Math.Clamp(curr_block, 1, 2);

        // Dead End
        prev_scroll_value = mouse_state.ScrollWheelValue;
        base.Update(gameTime);
    }
}
