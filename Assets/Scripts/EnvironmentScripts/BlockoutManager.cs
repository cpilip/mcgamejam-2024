// Date: #DATETIME#
// Author: #DEVELOPER#
// Write a short description.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockoutManager : MonoBehaviour
{
    private GameObject[] invisibleRiverbedBlocks;
    private GameObject[] waterBlocks;
    private GameObject[] mountainBlocks;
    private GameObject[] mountainRiverbedBlocks;
    private GameObject[] riverbedBlocks;
    private GameObject[] caveBlocks;

    [SerializeField]
    private int stage = 0;

    private void Awake()
    {
        invisibleRiverbedBlocks = GameObject.FindGameObjectsWithTag("InvisibleRiverbed");
        waterBlocks = GameObject.FindGameObjectsWithTag("Water");
        mountainBlocks = GameObject.FindGameObjectsWithTag("Mountain");
        mountainRiverbedBlocks = GameObject.FindGameObjectsWithTag("MountainRiverbed");
        riverbedBlocks = GameObject.FindGameObjectsWithTag("Riverbed");
        caveBlocks = GameObject.FindGameObjectsWithTag("Caves");
    }

    public void buildStage()
    {
        if (stage >= 0 && stage <= 2)
        {
            if (stage == 0)
            {
                foreach (GameObject block in mountainBlocks)
                {
                    MeshRenderer rnd = block.GetComponent<MeshRenderer>();
                    bool state = rnd.enabled;
                    rnd.enabled = !state;
                }

                foreach (GameObject block in mountainRiverbedBlocks)
                {
                    MeshRenderer rnd = block.GetComponent<MeshRenderer>();
                    bool state = rnd.enabled;
                    rnd.enabled = !state;
                }

                foreach (GameObject block in riverbedBlocks)
                {
                    MeshRenderer rnd = block.GetComponent<MeshRenderer>();
                    bool state = rnd.enabled;
                    rnd.enabled = !state;
                }

                foreach (GameObject block in caveBlocks)
                {
                    MeshRenderer rnd = block.GetComponent<MeshRenderer>();
                    rnd.enabled = true;
                }

                Debug.Log("game: Built mountains.");
            }

            if (stage == 1)
            {
                foreach (GameObject block in riverbedBlocks)
                {
                    MeshRenderer rnd = block.GetComponent<MeshRenderer>();
                    bool state = rnd.enabled;
                    rnd.enabled = !state;
                }

                foreach (GameObject block in invisibleRiverbedBlocks)
                {
                    BoxCollider col = block.GetComponent<BoxCollider>();
                    bool state = col.enabled;
                    col.enabled = !state;
                }

                Debug.Log("game: Built riverbeds.");
            }

            if (stage == 2)
            {
                foreach (GameObject block in waterBlocks)
                {
                    MeshRenderer rnd = block.GetComponent<MeshRenderer>();
                    bool state = rnd.enabled;
                    rnd.enabled = !state;
                }

                Debug.Log("game: Built water.");
            }

            stage++;
        }
        else
        {
            Debug.LogError("error: Could not build stage - out of range! [" + stage + "]");
        }
    }

    public void destroyStage()
    {
        if (stage >= 1 && stage <= 3)
        {
            if (stage == 1)
            {
                foreach (GameObject block in mountainBlocks)
                {
                    MeshRenderer rnd = block.GetComponent<MeshRenderer>();
                    bool state = rnd.enabled;
                    rnd.enabled = !state;
                }

                foreach (GameObject block in mountainRiverbedBlocks)
                {
                    MeshRenderer rnd = block.GetComponent<MeshRenderer>();
                    bool state = rnd.enabled;
                    rnd.enabled = !state;
                }

                foreach (GameObject block in riverbedBlocks)
                {
                    MeshRenderer rnd = block.GetComponent<MeshRenderer>();
                    bool state = rnd.enabled;
                    rnd.enabled = !state;
                }

                foreach (GameObject block in caveBlocks)
                {
                    MeshRenderer rnd = block.GetComponent<MeshRenderer>();
                    rnd.enabled = false;
                }
                
                Debug.Log("game: Destroyed mountains.");
            }

            if (stage == 2)
            {
                foreach (GameObject block in riverbedBlocks)
                {
                    MeshRenderer rnd = block.GetComponent<MeshRenderer>();
                    bool state = rnd.enabled;
                    rnd.enabled = !state;
                }

                foreach (GameObject block in invisibleRiverbedBlocks)
                {
                    BoxCollider col = block.GetComponent<BoxCollider>();
                    bool state = col.enabled;
                    col.enabled = !state;
                }

                Debug.Log("game: Destroyed riverbeds.");
            }

            if (stage == 3)
            {
                foreach (GameObject block in waterBlocks)
                {
                    MeshRenderer rnd = block.GetComponent<MeshRenderer>();
                    bool state = rnd.enabled;
                    rnd.enabled = !state;
                }

                Debug.Log("game: Destroyed water.");
            }

            stage--;
        }
        else
        {
            Debug.LogError("error: Could not destroy stage - out of range! [" + stage + "]");
        }
    }

    public void buildCaves()
    {
        if (stage >= 1)
        {
            foreach (GameObject block in caveBlocks)
            {
                MeshRenderer rnd = block.GetComponent<MeshRenderer>();
                rnd.enabled = false;
            }
        }
    }

    public void destroyCaves()
    {
        if (stage >= 1)
        {
            foreach (GameObject block in caveBlocks)
            {
                MeshRenderer rnd = block.GetComponent<MeshRenderer>();
                rnd.enabled = true;
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            buildStage();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            destroyStage();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            buildCaves();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            destroyCaves();
        }
    }

}
