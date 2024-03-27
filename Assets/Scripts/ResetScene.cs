using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetScene : MonoBehaviour
{
    public static Action ResetWorld = delegate { };

    public bool bResetWorld;

    private void Update()
    {
        if (bResetWorld)
        {
            bResetWorld = false;
            ResetWorld();
        }
    }
}
