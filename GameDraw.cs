using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BitsAndBlocks;

public partial class Game1
{
    Vector2 CamPosition = new Vector2(0, 0);
    float txt_draw_scale = 4f;

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);



        // Setup for point filtering
        _spriteBatch.Begin(
            SpriteSortMode.Deferred,
            BlendState.AlphaBlend,
            SamplerState.PointClamp
        );

        // self-explanatory
        DrawWorld();

        // Left Top block indicator
        _spriteBatch.Draw(
            TXT_Blocks[Block.BlockIDs[curr_block].Texture],
            new Vector2(10, 10),
            null,
            Color.White,
            0f,
            Vector2.Zero,
            Vector2.One * txt_draw_scale * 2,
            SpriteEffects.None,
            0f
        );

        // Player
        _spriteBatch.Draw(
            TXT_Player,
            WorldToScreenPos(PlayerPosition),
            null,
            Color.White,
            0f,
            new Vector2(4, 8),
            Vector2.One * txt_draw_scale,
            SpriteEffects.None,
            0f
        );

        _spriteBatch.End();

        base.Draw(gameTime);
    }

    void DrawWorld()
    {
        MouseState mouse_state = Mouse.GetState();
        Vector2 the_one_hovered = IntVector2(ScreenToWorldPos(mouse_state.X, mouse_state.Y));

        for (int y=0; y<WRLD_HEIGHT; y++) {
            for (int x=0; x<WRLD_WIDTH; x++) {
                
                Vector2 true_pos = WorldToScreenPos(x, y);

                // Do not draw things outside FOV lol (PERFORMANCE!!!!!!!!)
                if ( 
                    true_pos.X + block_size * txt_draw_scale < 0 || true_pos.X > WIN_WIDTH ||
                    true_pos.Y + block_size * txt_draw_scale < 0 || true_pos.Y > WIN_HEIGHT
                ) { continue; }

                int block_id = world[y,x];
                if (block_id == 0) { continue; }

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
                    Vector2.One * txt_draw_scale,
                    SpriteEffects.None,
                    0f
                );
            }
        }
    }

    // Translate world pos to screen
    Vector2 WorldToScreenPos(float x, float y) {
        Vector2 actual_pos = (new Vector2(x, WRLD_HEIGHT-y-1) - new Vector2(CamPosition.X, WRLD_HEIGHT-CamPosition.Y)) * block_size * txt_draw_scale;
        actual_pos += new Vector2(WIN_WIDTH/2, WIN_HEIGHT/2);
        actual_pos = new Vector2((int)actual_pos.X, (int)actual_pos.Y);
        return actual_pos;
    }
    Vector2 WorldToScreenPos(Vector2 _pos) { return WorldToScreenPos(_pos.X, _pos.Y); }


    // Translate screen pos to world
    Vector2 ScreenToWorldPos(int x, int y) {
        Vector2 actual_pos = ((new Vector2(x, y) - new Vector2(WIN_WIDTH/2, WIN_HEIGHT/2)) / block_size / txt_draw_scale) + new Vector2(CamPosition.X, WRLD_HEIGHT-CamPosition.Y);
        actual_pos.Y = WRLD_HEIGHT - actual_pos.Y;
        return actual_pos;
    }
    Vector2 ScreenToWorldPos(Vector2 _pos) { return ScreenToWorldPos((int)_pos.X, (int)_pos.Y); }


    // Quick convert float values of a vector2 to ints
    Vector2 IntVector2(Vector2 _vec2) { return new Vector2((int)_vec2.X, (int)_vec2.Y); }
}