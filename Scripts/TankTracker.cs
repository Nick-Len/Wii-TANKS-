using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankTracker : MonoBehaviour
{
    [SerializeField]
    private AudioSource aSource;
    public int btank = 0;
    public int greytank = 0;
    public int ytank = 0;
    public int ptank = 0;
    public int greentank = 0;

    private void OnEnable()
    {
        aSource.Play();
    }
}
