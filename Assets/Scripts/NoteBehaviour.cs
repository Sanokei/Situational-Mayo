using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteBehaviour : MonoBehaviour
{
    private bool _hit = false;
    public delegate void NoteTriggeredEvent(GameObject note, bool hit);
    public static event NoteTriggeredEvent OnNoteTriggeredEvent;

    [SerializeField] RectTransform _rt;

    //CLEANUP: getter and setter blah blah blah
    [HideInInspector] public float tempo;

    void FixedUpdate()
    {
        if(_rt.anchoredPosition.x < Screen.width)
        {
            _rt.anchoredPosition += new Vector2(tempo * Time.deltaTime, 0);
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
