using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BitsAndBlocks;

public partial class Game1
{
    Vector2 CamPosition = new Vector2(0, 0);
    float txt_block_scale = 4f;

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin(
            SpriteSortMode.Deferred,
            BlendState.AlphaBlend,
            SamplerState.PointClamp
        );
        DrawWorld();

        _spriteBatch.Draw(
            TXT_Blocks[Block.BlockIDs[curr_block].Texture],
            new Vector2(10, 10),
            null,
            Color.White,
            0f,
            Vector2.Zero,
            Vector2.One * txt_block_scale * 2,
            SpriteEffects.None,
            0f
        );

        _spriteBatch.End();

        base.Draw(gameTime);
    }

    void DrawWorld()
    {
        MouseState mouse_state = Mouse.GetState();
        Vector2 the_one_hovered = GetBlockByScreenPos(mouse_state.X, mouse_state.Y, 4f);

        for (int y=0; y<WRLD_HEIGHT; y++) {

            // Do not render what's outside FOV
            int ysp = GetBlockDrawPosY(y);
            if (ysp+8*txt_block_scale < 0 || ysp > WIN_WIDTH) { continue; }

            for (int x=0; x<WRLD_WIDTH; x++) {

                int xsp = GetBlockDrawPosX(x);
                if (xsp+8*txt_block_scale < 0 || xsp > WIN_WIDTH) { continue; }
                Vector2 true_pos = GetBlockDrawPos(x, y);
                
                int block_id = world[y,x];
                BlockListInfo block_info;
                Block.BlockIDs.TryGetValue(block_id, out block_info);

                // Do not render null
                if (block_info.Texture == null) { continue; }
                Texture2D block_txt; TXT_Blocks.TryGetValue(block_info.Texture, out block_txt);

                // Process

                Color block_color = Color.White;
                if (the_one_hovered == new Vector2(x, y)) {block_color = Color.Red; }

                _spriteBatch.Draw(
                    block_txt,
                    true_pos,
                    null,
                    block_color,
                    0f,
                    new Vector2(0, 0),
                    Vector2.One * txt_block_scale,
                    SpriteEffects.None,
                    0f
                );
            }
        }
    }

    int GetBlockDrawPosX(int x) {
        float true_cam_pos_x     = CamPosition.X - WIN_WIDTH/2;
        float screen_block_pos_x = (x-WRLD_WIDTH/2)*8*txt_block_scale;
        return (int)(screen_block_pos_x - true_cam_pos_x);
    }

    int GetBlockDrawPosY(int y) {
        float true_cam_pos_y    = CamPosition.Y - WIN_HEIGHT/2;
        float screen_block_pos_y = (WRLD_HEIGHT-1-y-WRLD_HEIGHT/2)*8*txt_block_scale;
        return (int)(screen_block_pos_y - true_cam_pos_y);
    }

    // Defines where block should be drawn
    Vector2 GetBlockDrawPos(int x, int y) {
        return new Vector2(GetBlockDrawPosX(x), GetBlockDrawPosY(y));
    }
    Vector2 GetBlockScreenPos(Vector2 _pos, float txt_scale) {
        return GetBlockByScreenPos((int)_pos.X, (int)_pos.Y, txt_scale);
    }

    // Get specific Block index based on eg. mouse pos
    Vector2 GetBlockByScreenPos(int x, int y, float txt_scale) {
        
        // X
        x += (int)(CamPosition.X   - ( WIN_WIDTH/2 ));
        x  = (int)(x/(8*txt_scale) + ( WRLD_WIDTH/2 ));

        // Y
        y += (int)(CamPosition.Y - ( WIN_HEIGHT/2 ));
        y  = WRLD_HEIGHT-1 - (int)(y/(8*txt_scale) + ( WRLD_HEIGHT/2 ));

        return new Vector2(x, y);
    }
    Vector2 GetBlockByScreenPos(Vector2 _pos, float txt_scale) {
        return GetBlockByScreenPos((int)_pos.X, (int)_pos.Y, txt_scale);
    }
}