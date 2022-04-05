using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    GameObject InItem;

    // i know there is a better way to do this, i cannot be bothered to do it rn
    [SerializeField] GameObject ExplanationPanel;
    [SerializeField] GameObject NotesGO;
    [SerializeField] GameObject ItemsGO;
    [SerializeField] GameObject BarGO;
    [SerializeField] GameObject ExplanationGO;

    // ran out of time
    [SerializeField] TextMeshProUGUI _scoreText;

    ObjectBehaviourScript _currentObject;
    int _numberOfHits;
    int _numberOfNotes;
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
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(BarBehaviour.Instance._isPlaying)
            {
                Debug.Log("hit");
                // _currentObject.hitAnimation.Play("Hit");
                _numberOfHits++;
                _scoreText.text = "Score: " + _numberOfHits + "/" + _numberOfNotes;

            }
            else
            {
                Debug.Log("miss");
            }
        }
    }

    public void StartNewRhythm(Rhythm rhythm)
    {
        _currentObject = rhythm.physicalObject.GetComponent<ObjectBehaviourScript>();
        // panel that explains the task
        GameObject EP = Instantiate(ExplanationPanel, new Vector3(Screen.width / 2, Screen.height / 2, 0), Quaternion.identity);
        EP.GetComponent<ExplanationPanelBehaviour>()._text.text = rhythm.explanation;
        EP.transform.SetParent(ExplanationGO.transform);
        StartCoroutine("CO_StartNewRhythm",rhythm);
    }
    IEnumerator CO_StartNewRhythm(Rhythm rhythm)
    {
        GameObject Bar = Resources.LoadAsync("Prefabs/Bar").asset as GameObject;
        InBar = Instantiate(Bar, new Vector3(Screen.width - (Screen.width * 0.05f), 0, 0), Quaternion.identity);
        InBar.transform.SetParent(BarGO.transform);

        InItem = Instantiate(rhythm.physicalObject, new Vector3(Screen.width / 2, Screen.height / 2, 0), Quaternion.identity);
        InItem.transform.SetParent(ItemsGO.transform);
        // Fixed.
            // FIXME: gross code
            // adding the spaces is cheating but it works for now
            // it solves the bar disappearing too soon issue
            //  KEEP IN MIND it wont work for every screen so again
            //  bad way of doing this

        // im adding leading spaces cuz fuck that shit
        // string pattern = "       " + rhythm.pattern; // + " ";
        
        // random pattern of o and spaces
        string pattern = "      ";
        // for(int i = 0; i < pattern.Length; i++){
        for(int i = 0; i < int.MaxValue; i++){
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
                noteBehaviour.image.sprite = rhythm.note.image;

                InNote.transform.SetParent(NotesGO.transform);
                yield return new WaitForSeconds(1);
            }
            else if(pattern[i] == ' ')
            {
                pattern += Random.Range(0,2) == 0 ? "o" : " ";
                yield return new WaitForSeconds(0.5f);
            }
        }
        // while there are still notes in the bar
        while(NotesGO.transform.childCount > 0)
        {
            yield return new WaitForEndOfFrame();
        }
        Destroy(InBar);
        Destroy(InItem);
    }
}
