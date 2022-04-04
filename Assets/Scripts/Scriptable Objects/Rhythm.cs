using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "_Rhythm", menuName = "Rhythm", order = 1)]
public class Rhythm : ScriptableObject
{
    public string pattern;
    public GameObject physicalObject;
    public Note note;
}
