using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace BitsAndBlocks;

public partial class Game1
{
    Vector2 PlayerPosition;
    Vector2 PlayerVelocity;
    float player_speed = 7f;
    float player_speeding = 28f;

    float player_gravity = 30f;

    bool can_jump = true;
    float player_jump_power = 15f;

    Vector2 BlockToPlayerPos(int x, int y) {
        return new Vector2(x+0.5f, y);
    }

    void PlayerToBlockPos(Vector2 plr_pos, out int _rx, out int _ry) {
        _rx = (int)plr_pos.X;
        _ry = (int)plr_pos.Y;
    }

    CollisionBox GetPlayerCollisionBox(Vector2 _plr_pos) {
        return new CollisionBox( _plr_pos.X-0.4f, _plr_pos.Y, 0.8f, 1.8f);
    }

    Vector2 CalcNewPlrPos() { return PlayerPosition + PlayerVelocity * deltaTime; }

    void HandlePlayer()
    {
        // Gravity
        PlayerVelocity.Y -= player_gravity * deltaTime;

        // Input
        int mov_x = 0;
        if (_input.KeyDown(Keys.A))      { mov_x -= 1; }
        else if (_input.KeyDown(Keys.D)) { mov_x += 1; }

        PlayerVelocity.X = Stage.MoveTowards(PlayerVelocity.X, mov_x*player_speed, deltaTime*player_speeding);

        if (_input.KeyDown(Keys.Space) && can_jump) { 
            PlayerVelocity.Y += player_jump_power; can_jump = false;
        }

        // Logic
        Vector2 new_pos = CalcNewPlrPos();

        // Collision - Left
        if ( PlayerVelocity.X < 0 && PlrCollidesHorizontaly(new_pos, -1) ) {
            PlayerVelocity.X = 0;
            new_pos = CalcNewPlrPos();
        }
        else if ( PlayerVelocity.X > 0 && PlrCollidesHorizontaly(new_pos, 1) ) {
            PlayerVelocity.X = 0;
            new_pos = CalcNewPlrPos();
        }

        // Collision - Down
        if ( PlayerVelocity.Y < 0 && PlrCollidesDown(new_pos) ) {
            PlayerVelocity.Y = 0;
            can_jump = true;
        }

        // Collides - Up
        else if ( PlayerVelocity.Y > 0 && PlrCollidesUp(new_pos) ) {
            PlayerVelocity.Y = 0;
        }

        // Update position
        PlayerPosition = CalcNewPlrPos();
    }
}