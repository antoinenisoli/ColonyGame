using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : ColonyRoom
{
    [Header(nameof(Factory))]
    public int resourceCollected;
    public float produceDelay = 2f;
    float timer;

    public override void Process()
    {
        foreach (var item in settlers)
        {
            if (item)
            {
                timer += Time.deltaTime;
            }
        }

        if (timer > produceDelay)
        {
            timer = 0;
            resourceCollected++;
        }
    }
}
