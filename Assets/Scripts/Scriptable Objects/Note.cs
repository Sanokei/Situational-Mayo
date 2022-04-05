using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "_Note", menuName = "Note", order = 2)]
public class Note : ScriptableObject
{
    public float tempo = 200f;
    public Sprite image;
}
