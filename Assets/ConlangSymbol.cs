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

    public bool canFade = false;
    public float fadeInSpead;
    public float fadeOutSpead;
    public float counterDown;
    private bool fadeInCompleted;

    public void Init() {

        




        canFade = false;
        fadeInSpead = 0.01f;
        fadeOutSpead = 0.01f;
        counterDown = 4f;
        fadeInCompleted = false;

        icon.GetComponent<CanvasRenderer>().SetAlpha(0f);

        canFade = true;
    }

    public void Update()
    {
        if (canFade)
        {
            StartCoroutine(FadeIn());
        }

        else if (fadeInCompleted)
        {
            counterDown -= Time.deltaTime;

            if (counterDown < 0)
            {
                StartCoroutine(FadeOut());
            }
        }
    }

    public IEnumerator FadeIn()
    {
        icon.CrossFadeAlpha(1f,0.2f,false);

        while (icon.color.a < 1)
        {   
            yield return null;
        }
        
        canFade = false;
        fadeInCompleted = true;
    }
    
    public IEnumerator FadeOut()
    {
        icon.CrossFadeAlpha(0f,0.8f,false);
        while (icon.color.a > 0)
        {
            yield return null;
        }

        icon.CrossFadeAlpha(0f,1f,false);

        canFade = false;
        fadeInCompleted = false;

        Destroy(gameObject);
    }
}
