using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : Cell
{

    private int numGoldenShields;

    public Castle()
    {
        numGoldenShields = 0;
    }
    public int getNumGoldenShield() { return numGoldenShields; }

    public void initGoldenShields(int numOfPlayers)
    {
        if (numOfPlayers == 4) { numGoldenShields = 1; }
        else if (numOfPlayers == 3) { numGoldenShields = 2; }
        else if (numOfPlayers == 2) { numGoldenShields = 3; }
    }
    public int decrementGoldenShields()
    {
        if (numGoldenShields > 0) { numGoldenShields--; return 1; } else { return -1; }   // game over
    }

    public void addGoldenShield() { numGoldenShields++; }

}
