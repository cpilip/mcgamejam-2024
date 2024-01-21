using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class SongController : MonoBehaviour
{
    public static SongController Instance { get; private set; }


    [Serializable]
    public class Note
    {
        public char noteId;
        public KeyCode key;

        public string PlayAudioEvent => $"P{(P1Note ? "1" : "2")}_Note{noteId}_Harp";
        public string ChordedAudioEvent => $"P{(P1Note ? "1" : "2")}_Note{noteId}_Vocals";

        public bool P1Note => SongController.Instance.p1Notes.Contains(this);

        public bool Held => Input.GetKey(key);
        public bool Pressing => Input.GetKeyDown(key);
        public bool Releasing => Input.GetKeyUp(key);

        public override string ToString()
        {
            if (SongController.Instance.p1Notes.Contains(this))
                return $"P1:{noteId}";
            else if (SongController.Instance.p2Notes.Contains(this))
                return $"P2:{noteId}";
            return $"???:{noteId}";
        }
    }

    public Note[] p1Notes;
    public Note[] p2Notes;
    
    public struct Chord
    {
        public Note p1Note;
        public Note p2Note;

        public Chord(Note p1Note, Note p2Note)
        {
            this.p1Note = p1Note;
            this.p2Note = p2Note;
        }

        public override string ToString() =>
            (p1Note.noteId < p2Note.noteId)
                ? $"{p1Note.noteId}{p2Note.noteId}"
                : $"{p2Note.noteId}{p1Note.noteId}";

        public override bool Equals(object o)
        {
            if (o is Chord other)
            {
                return this.GetHashCode() == other.GetHashCode();
            }
            return false;
        }

        public override int GetHashCode()
        {
            return (p1Note.noteId < p2Note.noteId)
                ? HashCode.Combine(p1Note, p2Note)
                : HashCode.Combine(p2Note, p1Note);
        }
    }

    [Serializable]
    public class Song
    {
        public string internalName;
        public string songString;
        public GameObject sceneObject;
        public UnityEvent onComplete;
        public List<GameObject> prerequisites;

        public string AudioTrack => $"Track{internalName}";

        public List<Chord> Chords =>
            _chords ??= songString.Split(' ')
                .Select(chordStr => new Chord(
                    SongController.Instance.p1Notes.Single(n => n.noteId == chordStr[0]),
                    SongController.Instance.p2Notes.Single(n => n.noteId == chordStr[1])
                ))
                .ToList();

        private List<Chord> _chords;

        public HashSet<Chord> ChordsSet => _chordsSet ??= Chords.ToHashSet();
        private HashSet<Chord> _chordsSet;

        public bool Matches(List<Chord> o) => ChordsSet.SetEquals(o.ToHashSet());
    }

    public List<Song> songs;

    public float holdTime = 1f;
    public float fudgeTime = 0.25f;

    [Header("UI stuff")]
    [SerializeField] private ContraptionScript contraptionScript;
    [SerializeField] private ConlangSymbolManager conlangSymbolManager;

    private Note p1CurrentNote;
    private Note p2CurrentNote;

    private Coroutine songCoroutine = null;

    private string ToString(List<Chord> chords) => 
        string.Join(' ', chords.Select(chord => $"{chord.p1Note.noteId}{chord.p2Note.noteId}"));

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        foreach (var song in songs)
        {
            if (song.sceneObject != null)
            {
                song.sceneObject.SetActive(false);
            }
        }
    }

    void Update()
    {
        // player1
        foreach (var note in p1Notes)
        {
            if (note.Pressing)
            {
                p1CurrentNote = note;
                AudioController.Instance.PlayNote(note);
                
                if (songCoroutine == null)
                {
                    songCoroutine = StartCoroutine(SongCoroutine());
                }
            }
        }
        
        // player2
        foreach (var note in p2Notes)
        {
            if (note.Pressing)
            {
                p2CurrentNote = note;
                AudioController.Instance.PlayNote(note);
                
                if (songCoroutine == null)
                {
                    songCoroutine = StartCoroutine(SongCoroutine());
                }
            }
        }
    }

    private void LateUpdate()
    {
        p1CurrentNote = null;
        p2CurrentNote = null;
    }

    private IEnumerator SongCoroutine()
    {
        // Debug.Log("Song starting");

        var chords = new List<Chord>();

        Note p1Note = null;
        Note p2Note = null;
        float timeElapsed = 0;
        float fudgeTimeElapsed = 0;

        while (true)
        {
            if (p1CurrentNote != null) // if p1 is playing a note
            {
                if (p1Note == null) // if we don't have a note for p1 in the current chord yet
                {
                    p1Note = p1CurrentNote;
                }
                else if (p1Note != p1CurrentNote) // or, if p1 already played a different note for the current chord
                {
                    Debug.LogWarning("Song failed -- player 1 note too early");
                    // AudioController.Instance.ChordNotes(p1CurrentNote, null);
                    SongFailed();
                    break;
                }
            }

            if (p2CurrentNote != null) // if p2 is playing a note
            {
                if (p2Note == null) // if we don't have a note for p2 in the current chord yet
                {
                    p2Note = p2CurrentNote;
                }
                else if (p2Note != p2CurrentNote) // or, if p2 already played a different note for the current chord
                {
                    Debug.LogWarning("Song failed -- player 2 note too early");
                    // AudioController.Instance.ChordNotes(null, p2CurrentNote);
                    SongFailed();
                    break;
                }
            }
            
            if (p1Note != null && p2Note != null) // if both players have played a note
            {
                // completed chord
                AudioController.Instance.ChordNotes(p1Note, p2Note);
            
                // Debug.Log($"Song -- played chord ({p1Note}, {p2Note})");

                var newChord = new Chord(p1Note, p2Note);

                chords.Add(newChord);
                conlangSymbolManager.InstantiateSymbolP1(newChord);

                p1Note = null;
                p2Note = null;
                timeElapsed = 0;
                fudgeTimeElapsed = 0;

                yield return null;
                continue; // go to next chord
            }

            // if holdTime has passed, check if we've successfully finished the song
            if (timeElapsed > holdTime && p1Note == null && p2Note == null)
            {
                SongCompleted(chords);
                break;
            }

            // if either timer has passed, by this point, we've failed
            if (fudgeTimeElapsed > fudgeTime || timeElapsed > holdTime)
            {
                if (p1Note == null)
                {
                    Debug.LogWarning("Song failed -- player 1 missed note");
                    // AudioController.Instance.ChordNotes(null, p2Note);
                    SongFailed();
                    break;
                }

                if (p2Note == null)
                {
                    Debug.LogWarning("Song failed -- player 2 missed note");
                    // AudioController.Instance.ChordNotes(p1Note, null);
                    SongFailed();
                    break;
                }
            }

            timeElapsed += Time.deltaTime;

            if (p1Note != null || p2Note != null)
                fudgeTimeElapsed += Time.deltaTime;

            yield return null;
        }

        songCoroutine = null;
    }

    private void SongFailed()
    {
        contraptionScript.turnOnRedLight();

        //TODO UI
    }

    private void SongCompleted(List<Chord> chords)
    {
        Song song = null;
        foreach (var s in songs)
        {
            if (s.Matches(chords))
            {
                song = s; 
                break;
            }
        }

        if (song == null)
        {
            Debug.LogWarning($"Song completed -- song not recognized: {ToString(chords)}");
            contraptionScript.turnOnRedLight();
            return;
        }

        if (!song.prerequisites.All(go => go.activeInHierarchy))
        {
            Debug.LogWarning("Song completed -- prereqs not met");
            //TODO

            contraptionScript.turnOnYellowLight();
            return;
        }
        contraptionScript.turnOnGreenLight();
        Debug.Log($"Song completed: {song.internalName} ({ToString(song.Chords)})");
        if (song.sceneObject != null)
        {
            song.sceneObject.SetActive(true);
        }

        song.onComplete?.Invoke();

        AudioController.Instance.PostEvent(song.AudioTrack);
    }
}
