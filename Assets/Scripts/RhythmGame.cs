using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmGame : MonoBehaviour
{
    public delegate void OnNoteHit(GameObject note);
    public OnNoteHit OnNoteHitEvent;
    public delegate void OnNoteMiss(GameObject note);
    public OnNoteMiss OnNoteMissEvent;
    public delegate void OnRhythemFinished();
    public OnRhythemFinished OnRhythemFinishedEvent;

    //
    public static RhythmGame Instance;
    GameObject InBar;
    [SerializeField] GameObject NotesGO;
    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        NoteBehaviour.OnNoteTriggeredEvent += OnNoteTriggered;
    }
    public void StartNewRhythm(Rhythm rhythm)
    {
        StartCoroutine("CO_StartNewRhythm",rhythm);
    }
    IEnumerator CO_StartNewRhythm(Rhythm rhythm)
    {
        GameObject Bar = Resources.LoadAsync("Prefabs/Bar").asset as GameObject;
        InBar = Instantiate(Bar, new Vector3(Screen.width - (Screen.width * 0.05f), 0, 0), Quaternion.identity);
        InBar.transform.SetParent(Instance.gameObject.transform);

        // Fixed.
            // FIXME: gross code
            // adding the spaces is cheating but it works for now
            // it solves the bar disappearing too soon issue
            //  KEEP IN MIND it wont work for every screen so again
            //  bad way of doing this
        string pattern = rhythm.pattern; // + " ";
        int _numberOfNotes = 0;
        for(int i = 0; i < pattern.Length; i++){
            if(pattern[i] == 'o')
            {
                _numberOfNotes++;
                GameObject Note = Resources.LoadAsync("Prefabs/Note").asset as GameObject;
                GameObject InNote = Instantiate(Note, new Vector3(0, 100, 0), Quaternion.identity);
                InNote.name = i.ToString();
                // Change note behaviour 

                // FIXME: This is super slow, but it will work for now
                NoteBehaviour noteBehaviour = InNote.GetComponent<NoteBehaviour>();
                
                noteBehaviour.tempo = rhythm.note.tempo;

                InNote.transform.SetParent(NotesGO.transform);
                yield return new WaitForSeconds(1);
            }
            else if(pattern[i] == ' ')
            {
                yield return new WaitForSeconds(0.5f);
            }
        }
        // while there are still notes in the bar
        while(NotesGO.transform.childCount > 0)
        {
            yield return new WaitForEndOfFrame();
        }
        Destroy(InBar);

    }

    void OnNoteTriggered(GameObject note)
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            OnNoteHitEvent?.Invoke(note);
            Debug.Log("Hit!");
        }
        else
        {
            OnNoteMissEvent?.Invoke(note);
            Debug.Log("Miss!");
        }
    }
}
