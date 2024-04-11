using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameActionTrigger : MonoBehaviour
{
    [SerializeField]
    private bool bToggle;
    [SerializeField]
    private List<GameAction> actions;
    private bool bActive, bDeActions;

    private void OnTriggerEnter(Collider other)
    {
        if (bActive) return;
        bActive = true;
        StartCoroutine(nameof(ExecuteActions));
    }
    private void OnTriggerExit(Collider other)
    {
        if (bActive && bDeActions) return;

        if (bToggle)
        {
            bDeActions = true;
            StartCoroutine(nameof(ExecuteActions));
        }
    }
    IEnumerator ExecuteActions()
    {
        foreach (GameAction action in actions)
        {
            yield return new WaitForSeconds(action.delay);

            if (bDeActions)
                action.DeAction();
            else
                action.Action();
        }
        bDeActions = false;
        bActive = false;
    }
}
