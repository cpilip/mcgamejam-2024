using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController Instance { get; private set; }

    public AudioSource player1AudioSource;
    public AudioSource player2AudioSource;

    [Header("Player Particle Systems")]    
    [SerializeField] PlayerParticleSystem player1ParticleSystem;
    [SerializeField] PlayerParticleSystem player2ParticleSystem;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void StartNotes(SongController.Note p1Note, SongController.Note p2Note)
    {
        StopNotes();
        if (p1Note != null) {
            player1AudioSource.PlayOneShot(p1Note.audio);
            
            player1ParticleSystem.DoParticleEffectForNote(p1Note);
        }
        if (p2Note != null) {
            player2AudioSource.PlayOneShot(p2Note.audio);

            player2ParticleSystem.DoParticleEffectForNote(p2Note);
        }
    }

    public void StopNotes()
    {
        player1AudioSource.Stop();
        player2AudioSource.Stop();
    }
}