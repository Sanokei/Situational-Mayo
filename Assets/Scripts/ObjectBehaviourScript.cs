using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBehaviourScript : MonoBehaviour
{
    [SerializeField] GameObject State;
    // note hit
    public Animation hitAnimation;

    // note miss
    // public Animation missAnimation;

    // fail and win states
    public GameObject failAnimation;
    public GameObject winAnimation;

}
