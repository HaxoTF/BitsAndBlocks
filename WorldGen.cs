using Microsoft.Xna.Framework;

namespace BitsAndBlocks;

public partial class Game1
{
    void GenerateWorld()
    {
        world = new int[WRLD_HEIGHT, WRLD_WIDTH];

        for (int y=0; y<WRLD_HEIGHT; y++) {
            for (int x=0; x<WRLD_WIDTH; x++) {

                if ( 
                    !(x==0 || x==WRLD_WIDTH-1) &&
                    !(y==0 || y==WRLD_HEIGHT-1)
                )
                { continue; }
                
                world[y,x] = rand.Next(1, 3);

            }
        }
    }

    bool PosInBorder(int x, int y) {
        bool hor = x>=0 && x<WRLD_WIDTH;
        bool ver = y>=0 && y<WRLD_HEIGHT;
        return hor && ver;
    }
}