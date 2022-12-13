using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleHandler : StepHandler
{

    private int placedCubes = 0;
    public int numCubes = 3;

    private bool done;

    public void CubesChanged(bool val)
    {
        if (val)
        {
            placedCubes++;

            if(placedCubes >= numCubes && !done)
            {
                done = true;
                AdvanceStory();
            }
        } else
        {
            placedCubes--;
        }
    }

}
