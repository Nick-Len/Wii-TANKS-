using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Disable : MonoBehaviour
{
    private void OnEnable()
    {
        LevelGenerator.newRound += Reset;
    }
    private void OnDisable()
    {
        LevelGenerator.newRound -= Reset;
    }

    private void Reset()
    {
        Destroy(GetComponent<GraphicRaycaster>());
        Destroy(GetComponent<CanvasScaler>());
        Destroy(GetComponent<Canvas>());
    }
}
