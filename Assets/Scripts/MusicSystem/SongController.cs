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

        // public override bool Equals(object o)
        // {
        //     if (o is Chord other)
        //     {
        //         return (this.p1Note.noteId == other.p1Note.noteId && this.p2Note.noteId == other.p2Note.noteId) ||
        //                (this.p1Note.noteId == other.p2Note.noteId && this.p2Note.noteId == other.p1Note.noteId);
        //     }
        //     return false;
        // }
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

        public bool Matches(List<Chord> o)
        {
            if (o.Count != Chords.Count) return false;

            for (int i = 0; i < Chords.Count; i++)
            {
                var thisNote1 = Chords[i].p1Note.noteId;
                var thisNote2 = Chords[i].p2Note.noteId;
                var otherNote1 = o[i].p1Note.noteId;
                var otherNote2 = o[i].p2Note.noteId;
                if (!((thisNote1 == otherNote1 && thisNote2 == otherNote2) ||
                      (thisNote1 == otherNote2 && thisNote2 == otherNote1)))
                {
                    return false;
                }
            }
            return true;
        }
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
                Debug.Log($"[p1CurrentNote={p1CurrentNote}], p2CurrentNote={p2CurrentNote}");
                AudioController.Instance.PlayNote(note);
                
                if (songCoroutine == null)
                {
                    songCoroutine = StartCoroutine(SongCoroutine());
                }
            }
        }

        if (p1CurrentNote != null && p1CurrentNote.Releasing)
        {
            p1CurrentNote = null;
            Debug.Log($"[p1CurrentNote=null], p2CurrentNote={p2CurrentNote}");
        }
        
        // player2
        foreach (var note in p2Notes)
        {
            if (note.Pressing)
            {
                p2CurrentNote = note;
                Debug.Log($"p1CurrentNote={p1CurrentNote}, [p2CurrentNote={p2CurrentNote}]");
                AudioController.Instance.PlayNote(note);
                
                if (songCoroutine == null)
                {
                    songCoroutine = StartCoroutine(SongCoroutine());
                }
            }
        }

        if (p2CurrentNote != null && p2CurrentNote.Releasing)
        {
            p2CurrentNote = null;
            Debug.Log($"p1CurrentNote={p1CurrentNote}, [p2CurrentNote=null]");
        }
    }

    private IEnumerator SongCoroutine()
    {
        Debug.Log("Song starting");

        var chords = new List<Chord>();

        while (p1CurrentNote != null || p2CurrentNote != null)
        {
            Note p1Note = null;
            Note p2Note = null;

            float timeElapsed = 0;
            while (timeElapsed < holdTime)
            {
                if (p1CurrentNote != null) // if p1 is playing a note
                {
                    if (p1Note == null) // if we don't have a note for p1 in the current chord yet
                    {
                        p1Note = p1CurrentNote;
                        if (p1Note != null && p2Note != null) // if both players played a note, play the audio
                        {
                            AudioController.Instance.ChordNotes(p1Note, p2Note);
                        }
                    }
                    else if (p1CurrentNote != p1Note) // or, if p1 already played a different note for the current chord
                    {
                        if (timeElapsed < holdTime - fudgeTime) // if it's not within fudgeTime before the next chord
                        {
                            Debug.LogWarning("Song failed -- p1 note too early");
                            // AudioController.Instance.ChordNotes(p1CurrentNote, null);
                            SongFailed();
                            yield break;
                        }
                        // otherwise, it'll be treated as queued up for the next chord
                    }
                }

                if (p2CurrentNote != null) // if p2 is playing a note
                {
                    if (p2Note == null) // if we don't have a note for p2 in the current chord yet
                    {
                        p2Note = p2CurrentNote;
                        if (p1Note != null && p2Note != null) // if both players played a note, play the audio
                        {
                            AudioController.Instance.ChordNotes(p1Note, p2Note);
                        }
                    }
                    else if (p2CurrentNote != p2Note) // or, if p2 already played a different note for the current chord
                    {
                        if (timeElapsed < holdTime - fudgeTime) // if it's not within fudgeTime before the next chord
                        {
                            Debug.LogWarning("Song failed -- p2 note too early");
                            // AudioController.Instance.ChordNotes(null, p2CurrentNote);
                            SongFailed();
                            yield break;
                        }
                        // otherwise, it'll be treated as queued up for the next chord
                    }
                }

                if (timeElapsed > fudgeTime) // if fudgeTime has passed, check that we have a note from both players
                {
                    if (p1Note == null)
                    {
                        Debug.LogWarning("Song failed -- p1 missed note");
                        // AudioController.Instance.ChordNotes(null, p2Note);
                        SongFailed();
                        yield break;
                    }
                    if (p2Note == null)
                    {
                        Debug.LogWarning("Song failed -- p2 missed note");
                        // AudioController.Instance.ChordNotes(p1Note, null);
                        SongFailed();
                        yield break;
                    }
                }

                timeElapsed += Time.deltaTime;
                yield return null;
            }
            
            // completed chord
            
            Debug.Log($"Song completed chord ({p1Note}, {p2Note})");

            var newChord = new Chord(p1Note, p2Note);

            chords.Add(newChord);
            conlangSymbolManager.InstantiateSymbolP1(newChord);
        }
        
        foreach (var song in songs)
        {
            if (song.Matches(chords))
            {
                SongCompleted(song);
                yield break;
            }
        }
        Debug.LogWarning($"Song completed -- song not recognized: {ToString(chords)}");
        SongCompleted(null);
    }

    private void SongFailed()
    {
        songCoroutine = null;
        
        contraptionScript.turnOnRedLight();

        //TODO UI
    }

    private void SongCompleted(Song song)
    {
        songCoroutine = null;

        if (song == null)
        {
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
