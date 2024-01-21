// Date: #DATETIME#
// Author: #DEVELOPER#
// Write a short description.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticleSystem : MonoBehaviour
{
    [SerializeField] private ParticleSystem notes_particle_sys;
    [SerializeField] private ParticleSystem notes2_particle_sys;

    [Header("Elements")] 
    [SerializeField] private ParticleSystem land_particle_sys;
    [SerializeField] private ParticleSystem water_particle_sys;
    [SerializeField] private ParticleSystem fire_particle_sys;
    [SerializeField] private ParticleSystem rock_particle_sys;
    [SerializeField] private ParticleSystem wind_particle_sys;

    public void DoParticleEffectForNote(SongController.Note note) {

        ParticleSystem particleSys = null;

        switch(note.noteId) {
            case '1': 
                particleSys = land_particle_sys;
                break;
            case '2': 
                particleSys = water_particle_sys;
                break;
            case '3': 
                particleSys = fire_particle_sys;
                break;
            case '4': 
                particleSys = rock_particle_sys;
                break;
            case '5': 
                particleSys = wind_particle_sys;
                break;
        }

        notes_particle_sys.Play(); //Emit(10);
        notes2_particle_sys.Play();
        particleSys.Play(); //Emit(10);

    }
}
