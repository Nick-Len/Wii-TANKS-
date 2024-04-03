using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowDelegate : MonoBehaviour
{
    public static Action<Transform> follow = delegate {};

    private void Update()
    {
        follow(transform);
    }
}
