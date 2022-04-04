using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSetActive: MonoBehaviour
{
    public delegate void SetActiveEvent(GameObject target);
    public static event SetActiveEvent OnSetActive;

    public delegate void SetInactiveEvent(GameObject target);
    public static event SetInactiveEvent OnSetInactive;

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
        if(active)
        {
            OnSetActive?.Invoke(gameObject);
        }
        else
        {
            OnSetInactive?.Invoke(gameObject);
        }
    }
}
