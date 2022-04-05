using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteBehaviour : MonoBehaviour
{
    [SerializeField] RectTransform _rt;

    //CLEANUP: getter and setter blah blah blah
    [HideInInspector] public float tempo;
    public Image image;

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
