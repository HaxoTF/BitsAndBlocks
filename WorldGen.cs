using Microsoft.Xna.Framework;

namespace BitsAndBlocks;

public partial class Game1
{
    void GenerateWorld()
    {
        world = new int[WRLD_HEIGHT, WRLD_WIDTH];

        for (int y=0; y<WRLD_HEIGHT; y++) {
            for (int x=0; x<WRLD_WIDTH; x++) {

                int block_id = 0;
                if (y<20) { 
                    block_id = 1;
                    if (rand.Next(0, 5)==1) { block_id=4; }
                }
                else if(y<24) { block_id=2; }
                else if(y<25) { block_id=3; }
                world[y,x] = block_id;

            }
        }
    }

    public bool PosInBorder(int x, int y) {
        bool hor = x>=0 && x<WRLD_WIDTH;
        bool ver = y>=0 && y<WRLD_HEIGHT;
        return hor && ver;
    }
}