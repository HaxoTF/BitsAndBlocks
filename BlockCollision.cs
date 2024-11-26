using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace BitsAndBlocks;

public partial class Game1
{
    public struct CollisionBox {

        public float X {get; set;}
        public float Y {get; set;}
        public float W {get; set;}
        public float H {get; set;}

        public CollisionBox(float _x, float _y, float _w, float _h) { 
            X = _x; Y = _y; W = _w; H = _h; 
        }

        public bool CollidesWith(CollisionBox _other) {
            bool a = X <= _other.X + _other.W;
            bool b = X + W >= _other.X;
            bool c = Y <= _other.Y + _other.H;
            bool d = Y + H >= _other.Y;
            return a && b && c && d;
        }
    }

    public bool PlrCollidesWithBlock(Vector2 _plr_pos, int bx, int by, bool _ignore_air=false) {
        if (!PosInBorder(bx, by)) { return true; }
        if (world[by, bx]==0 && !_ignore_air) { return false; }
        CollisionBox plr_box = GetPlayerCollisionBox(_plr_pos);
        CollisionBox block_box = new CollisionBox(bx, by, 1, 1);
        return plr_box.CollidesWith(block_box);
    }

    public void PlrBlockOffset(Vector2 _plr_pos, int offset_x, int offset_y, out int bx, out int by) {
        PlayerToBlockPos(_plr_pos, out bx, out by);
        bx += offset_x; by += offset_y;
    }

    public bool PlrCollidesWithBlockOffset(Vector2 _plr_pos, int offset_x, int offset_y) {
        int bx, by; PlrBlockOffset(_plr_pos, offset_x, offset_y, out bx, out by);
        return PlrCollidesWithBlock(_plr_pos, bx, by);
    }

    public bool PlrCollidesDown(Vector2 _plr_pos) {
        for (int i=-1; i<=1; i++) {
            if (PlrCollidesWithBlockOffset(_plr_pos, i, 0)) { return true; }
        }
        return false;
    }

    public bool PlrCollidesHorizontaly(Vector2 _plr_pos, int dir_x) {
        _plr_pos.Y += 0.1f;
        for (int i=0; i<=1; i++) {
            if (PlrCollidesWithBlockOffset(_plr_pos, dir_x, i)) { return true; }
        }
        return false;
    }

    public bool PlrCollidesUp(Vector2 _plr_pos) {
        for (int i=-1; i<=1; i++) {
            if (PlrCollidesWithBlockOffset(_plr_pos, i, 2)) { return true; }
        }
        return false;
    }
}