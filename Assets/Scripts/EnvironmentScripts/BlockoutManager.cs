// Date: #DATETIME#
// Author: #DEVELOPER#
// Write a short description.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockoutManager : MonoBehaviour
{
    [SerializeField] private Material groundShaderMaterial;

    [SerializeField]
    private Texture2D blank_world;
    [SerializeField]
    private Texture2D ground;
    [SerializeField]
    private Texture2D grass;
    [SerializeField]
    private Texture2D final;
    [SerializeField]
    private Texture2D conlang;

    private float bLerp;
    private bool locked = false;


    private GameObject[] invisibleRiverbedBlocks;
    private GameObject[] waterBlocks;
    private GameObject[] mountainBlocks;
    private GameObject[] mountainRiverbedBlocks;
    private GameObject[] riverbedBlocks;
    private GameObject[] caveBlocks;
    private GameObject[] terraBlocks;
    private GameObject[] rampBlocks;
    private GameObject[] invisibleBlocks;


    [SerializeField]
    private int stage = 0;

    private bool l1 = false;
    private bool l2 = false;
    private bool l3 = false;
    private bool l4 = false;

    private void Awake()
    {
        terraBlocks = GameObject.FindGameObjectsWithTag("Terra");
        invisibleRiverbedBlocks = GameObject.FindGameObjectsWithTag("InvisibleRiverbed");
        waterBlocks = GameObject.FindGameObjectsWithTag("Water");
        mountainBlocks = GameObject.FindGameObjectsWithTag("Mountain");
        mountainRiverbedBlocks = GameObject.FindGameObjectsWithTag("MountainRiverbed");
        riverbedBlocks = GameObject.FindGameObjectsWithTag("Riverbed");
        caveBlocks = GameObject.FindGameObjectsWithTag("Caves");
        rampBlocks = GameObject.FindGameObjectsWithTag("Ramp");
        invisibleBlocks = GameObject.FindGameObjectsWithTag("Invisible");

        // Convert to ground from blankness
        foreach (GameObject block in terraBlocks)
        {
            MeshRenderer rnd = block.GetComponent<MeshRenderer>();
            BoxCollider col = block.GetComponent<BoxCollider>();

            rnd.enabled = true;
            col.enabled = false;
        }

        foreach (GameObject block in mountainRiverbedBlocks)
        {
            MeshRenderer rnd = block.GetComponent<MeshRenderer>();
            BoxCollider col = block.GetComponent<BoxCollider>();

            rnd.enabled = true;
            col.enabled = true;
        }

        foreach (GameObject block in invisibleRiverbedBlocks)
        {
            MeshRenderer rnd = block.GetComponent<MeshRenderer>();
            BoxCollider col = block.GetComponent<BoxCollider>();

            rnd.enabled = false;
            col.enabled = false;
        }

        foreach (GameObject block in invisibleBlocks)
        {
            MeshRenderer rnd = block.GetComponent<MeshRenderer>();
            BoxCollider col = block.GetComponent<BoxCollider>();

            rnd.enabled = false;
            col.enabled = true;
        }
    }

    public void buildTerra()
    {
        // Convert to ground from blankness
        bLerp = groundShaderMaterial.GetFloat("_bLerp");
        Debug.Log("bl " + bLerp);

        groundShaderMaterial.SetTexture("_og", blank_world);
        groundShaderMaterial.SetTexture("_ou", blank_world);

        groundShaderMaterial.SetTexture("_ng", ground);
        groundShaderMaterial.SetTexture("_nu", blank_world);
        
        if (!l1)
        {
            l1 = true;
            StartCoroutine("Fade");
        }
    }

    public void buildValleys()
    {
        foreach (GameObject block in riverbedBlocks)
        {
            MeshRenderer rnd = block.GetComponent<MeshRenderer>();
            BoxCollider col = block.GetComponent<BoxCollider>();

            rnd.enabled = false;
            col.enabled = false;
        }

        foreach (GameObject block in invisibleRiverbedBlocks)
        {
            MeshRenderer rnd = block.GetComponent<MeshRenderer>();
            BoxCollider col = block.GetComponent<BoxCollider>();

            rnd.enabled = false;
            col.enabled = true;
        }
    }

    public void buildMountains()
    {
        foreach (GameObject block in mountainBlocks)
        {

            MeshRenderer rnd = block.GetComponent<MeshRenderer>();
            BoxCollider col = block.GetComponent<BoxCollider>();
            
            rnd.enabled = true;

            if (col)
            {
                col.enabled = true;
            }
        }

        foreach (GameObject block in rampBlocks)
        {
            for (int i = 0; i < block.transform.childCount; i++)
            {
                block.transform.GetChild(i).transform.gameObject.SetActive(true);
            }
        }

        foreach (GameObject block in mountainRiverbedBlocks)
        {
            MeshRenderer rnd = block.GetComponent<MeshRenderer>();
            BoxCollider col = block.GetComponent<BoxCollider>();

            rnd.enabled = false;
            col.enabled = false;
        }

        foreach (GameObject block in riverbedBlocks)
        {
            MeshRenderer rnd = block.GetComponent<MeshRenderer>();
            BoxCollider col = block.GetComponent<BoxCollider>();

            rnd.enabled = true;
            col.enabled = true;
        }

        foreach (GameObject block in caveBlocks)
        {
            MeshRenderer rnd = block.GetComponent<MeshRenderer>();
            BoxCollider col = block.GetComponent<BoxCollider>();

            rnd.enabled = true;
            col.enabled = true;
        }

    }

    public void buildWater()
    {
        if (waterBlocks.Length == 1)
        {
            for (int i = 0; i <
            waterBlocks[0].transform.childCount; i++)
            {
                waterBlocks[0].transform.GetChild(i).transform.gameObject.SetActive(true);
            }
        }
    }
    public void buildGrass()
    {
        //bLerp = groundShaderMaterial.GetFloat("_bLerp");
        //Debug.Log("bl " + bLerp);

        //groundShaderMaterial.SetTexture("_ng", ground);
        //groundShaderMaterial.SetTexture("_nu", blank_world);

        groundShaderMaterial.SetTexture("_og", grass);
        groundShaderMaterial.SetTexture("_ou", ground);

        if (!l2)
        {
            l2 = true;
            StartCoroutine("FadeReverse");
        }
    }

    public void buildNiceGrass()
    {
        //bLerp = groundShaderMaterial.GetFloat("_bLerp");
        //Debug.Log("bl " + bLerp);

        groundShaderMaterial.SetTexture("_ng", final);
        groundShaderMaterial.SetTexture("_nu", ground);

        //groundShaderMaterial.SetTexture("_og", grass);
        //groundShaderMaterial.SetTexture("_ou", ground);

        if (!l3)
        {
            l3 = true;
            StartCoroutine("Fade");
        }
    }

    public void buildPrettyGrass()
    {
        //bLerp = groundShaderMaterial.GetFloat("_bLerp");
        //Debug.Log("bl " + bLerp);

        //groundShaderMaterial.SetTexture("_ng", final);
        //groundShaderMaterial.SetTexture("_nu", ground);

        groundShaderMaterial.SetTexture("_og", conlang);
        groundShaderMaterial.SetTexture("_ou", ground);

        if (!l4)
        {
            l4 = true;
            StartCoroutine("FadeReverse");
        }
    }


    public void buildCaves()
    {
        foreach (GameObject block in caveBlocks)
        {
            MeshRenderer rnd = block.GetComponent<MeshRenderer>();
            BoxCollider col = block.GetComponent<BoxCollider>();

            rnd.enabled = false;
            col.enabled = false;
        }

    }


    //StartCoroutine(Fade());
    IEnumerator Fade()
    {
        float time = 0.0f;
        float duration = 1.0f;
        while (time <= duration)
        {
            groundShaderMaterial.SetFloat("_bLerp", (time / duration));
            time += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator FadeReverse()
    {
        float time = 1.0f;
        float duration = 1.0f;
        while (time >= 0.0f)
        {
            groundShaderMaterial.SetFloat("_bLerp", (time / duration));
            time -= Time.deltaTime;
            yield return null;
        }
    }

}

