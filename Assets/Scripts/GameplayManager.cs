using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    public static GameplayManager Instance;

    // TODO: Flag variable bad practice but it will do for now
    public static bool isPlaying = false;

    // Holds all the Rhythm SOs to have them be played in random orders every start
    public Rhythm[] RhythmGameObjects; 
    [SerializeField] Camera _Camera;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        TargetSetActive.OnSetInactive += SetInactive;
    }
    void Start()
    {
        ShuffleGameObjectArray(RhythmGameObjects);
    }

    void Update()
    {
        if(isPlaying)
        {
            // This whole part here is pretty stupid but it works for now
            isPlaying = false;
            // Recursive call to start the next rhythm
            StartRhythm(RhythmGameObjects[0]);
        }
    }

    // Listens for when the start menu is closed
    void SetInactive(GameObject target)
    {
        // TODO: This is a pretty dog shit way of doing this but it will work for now
        if(target.tag == "Start Menu")
        {
            isPlaying = true;
        }
    }

    // Dont know why this is a method but whatever
    void StartRhythm(Rhythm rhythm)
    {
        RhythmGame.Instance.StartNewRhythm(rhythm);
    }

    // Shuffles a Rhythm array
    public void ShuffleGameObjectArray (Rhythm[] array)
    {
        /*
        You may ask why I didnt use a for loop.
        in C# when looping through a list of non primitives
        it is faster to use a while loop than a for loop.
        why? no idea but its very minimal.
        https://cc.davelozinski.com/c-sharp/for-vs-foreach-vs-while#:~:text=Looping%20over%20DataRow%5B%5D%3A
        */
        int n = array.Length;
        while (n > 1)
        {
            int k = Random.Range(0, n--);
            Rhythm temp = array[n];
            array[n] = array[k];
            array[k] = temp;
        }
    }
}
