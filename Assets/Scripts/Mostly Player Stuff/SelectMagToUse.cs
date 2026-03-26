using UnityEngine;

public class SelectMagToUse : MonoBehaviour
{
    public Movement movement;
    public InputHander inputHander;

    void Update()
    {
        if(inputHander.Mag1Pressed())
        {
            movement.WhatMagnetToUse = "pull";
            Debug.Log("Pull Magnet activated");
        }
        else if(inputHander.Mag2Pressed())
        {
            movement.WhatMagnetToUse = "push";
            Debug.Log("Push Magnet activated");
        }
        else if(inputHander.Mag3Pressed())
        {
            movement.WhatMagnetToUse = "lazer";
            Debug.Log("Lazer Magnet activated");
        }
    }
}
