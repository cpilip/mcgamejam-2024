// Date: #DATETIME#
// Author: #DEVELOPER#
// Write a short description.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class ConlangSymbol : MonoBehaviour
{
    [SerializeField] private Image icon;

    [Header("ConLang Symbol Prefabs")]
    [SerializeField] private Sprite air;
    [SerializeField] private Sprite airFireGrow;
    [SerializeField] private Sprite airLandFauna;
    [SerializeField] private Sprite airSolo;
    [SerializeField] private Sprite fireFire;
    [SerializeField] private Sprite fireStoneGrow;
    [SerializeField] private Sprite fireWaterMagic;
    [SerializeField] private Sprite landLand;
    [SerializeField] private Sprite landSolo;
    [SerializeField] private Sprite stoneLandFlora;
    [SerializeField] private Sprite stoneSolo;
    [SerializeField] private Sprite stoneStone;
    [SerializeField] private Sprite stoneWaterDecay;
    [SerializeField] private Sprite waterLandDark;
    [SerializeField] private Sprite waterSolo;
    [SerializeField] private Sprite waterWater;
    [SerializeField] private Sprite dud;

    public bool canFade = false;
    public float fadeInSpead;
    public float fadeOutSpead;
    public float counterDown;
    private bool fadeInCompleted;

    public void Init(SongController.Chord chord) {

        Sprite sprite = null;

        switch(chord.p1Note.noteId) {
            case '1': // Land

                switch(chord.p2Note.noteId) {
                case '1': // LandLand
                    sprite = landLand;
                    break;
                case '2': // Water
                    sprite = waterLandDark; 
                    break;
                case '3': // LandFire --> dud
                    sprite = dud;
                    break;
                case '4': //Stone
                    sprite = stoneLandFlora;
                    break;
                case '5': // air
                    sprite = airLandFauna;
                    break;
                }

                break;
            case '2': // Water

                switch(chord.p2Note.noteId) {
                case '1': // waterLand
                    sprite = waterLandDark;
                    break;
                case '2': // WaterWater
                    sprite = waterWater; 
                    break;
                case '3': // Fire
                    sprite = fireWaterMagic;
                    break;
                case '4': //Stone
                    sprite = stoneWaterDecay;
                    break;
                case '5': // waterair --> dud
                    sprite = dud;
                    break;
                }

                break;
            
            case '3': // Fire

                switch(chord.p2Note.noteId) {
                case '1': // fireLand --> dud
                    sprite = dud;
                    break;
                case '2': // fireWater
                    sprite = fireWaterMagic; 
                    break;
                case '3': // Firefire
                    sprite = fireFire;
                    break;
                case '4': // fireStone
                    sprite = fireStoneGrow;
                    break;
                case '5': // fireair
                    sprite = airFireGrow;
                    break;
                }

                break;

            case '4': //Stone

                switch(chord.p2Note.noteId) {
                case '1': // stoneLand
                    sprite = stoneLandFlora;
                    break;
                case '2': // stoneWater
                    sprite = stoneWaterDecay; 
                    break;
                case '3': // stoneFire
                    sprite = fireStoneGrow;
                    break;
                case '4': // stoneStone
                    sprite = stoneStone;
                    break;
                case '5': // stoneair --> dud
                    sprite = dud;
                    break;
                }

                break;

            case '5': // air

                switch(chord.p2Note.noteId) {
                case '1': // airLand
                    sprite = airLandFauna;
                    break;
                case '2': // airWater --> dud
                    sprite = dud;
                    break;
                case '3': // airfire
                    sprite = airFireGrow;
                    break;
                case '4': // airStone -- dud
                    sprite = dud;
                    break;
                case '5': // airair
                    sprite = air;
                    break;
                }

                break;
        }

        if (sprite==null) Destroy(gameObject);

        icon.sprite = sprite;


        canFade = false;
        counterDown = 2f;
        fadeInCompleted = false;

        icon.GetComponent<CanvasRenderer>().SetAlpha(0f);

        StartCoroutine(FadeIn());
    }

    public IEnumerator FadeIn()
    {
        icon.CrossFadeAlpha(1f,0.5f,false);
        
        canFade = false;
        fadeInCompleted = true;
 
        yield return new WaitForSeconds(2f);

        Destroy(gameObject);
    }
}
