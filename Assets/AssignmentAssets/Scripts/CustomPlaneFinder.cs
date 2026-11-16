using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class CustomPlaneFinder: PlaneFinderBehaviour
{
    public void onTrigger()
    {
        Vector2 pos = new Vector2(0, 0);

        this.PerformHitTest(pos);
    }
}
