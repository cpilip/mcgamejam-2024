// Date: 1.20.2024
// Author: cpilip
// Automate manipulation of level blockout.

using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class BlockoutEditor : EditorWindow
{
    public Material invisibleWallMaterial;

    [MenuItem("Tools/Blockout Editor")]
    public static void SetUpUtility()
    {
        BlockoutEditor window = GetWindow<BlockoutEditor>();
        window.position = new Rect(Screen.width / 2, Screen.height / 2, 400, 150);
        window.titleContent = new GUIContent("Blockout Editor");
    }

    void OnGUI()
    {

        if (GUILayout.Button("Hide/Show All Invisible Walls"))
        {
            GameObject[] wallBlocks;
            wallBlocks = GameObject.FindGameObjectsWithTag("Invisible");
            foreach (GameObject block in wallBlocks)
            {
                MeshRenderer rnd = block.GetComponent<MeshRenderer>();
                bool state = rnd.enabled;
                rnd.enabled = !state;
            }
        }

        if (GUILayout.Button("Hide/Show All Invisible Riverbed Walls"))
        {
            GameObject[] wallBlocks;
            wallBlocks = GameObject.FindGameObjectsWithTag("InvisibleRiverbed");
            foreach (GameObject block in wallBlocks)
            {
                MeshRenderer rnd = block.GetComponent<MeshRenderer>();
                bool state = rnd.enabled;
                rnd.enabled = !state;
            }
        }


        if (GUILayout.Button("Hide/Show All Water"))
        {
            GameObject[] waterBlocks;
            waterBlocks = GameObject.FindGameObjectsWithTag("Water");
            foreach (GameObject block in waterBlocks)
            {
                MeshRenderer rnd = block.GetComponent<MeshRenderer>();
                bool state = rnd.enabled;
                rnd.enabled = !state;
                //block.transform.gameObject.tag = "Riverbed";
            }
        }

        if (GUILayout.Button("Hide/Show All Mountain"))
        {
            GameObject[] mountainBlocks;
            mountainBlocks = GameObject.FindGameObjectsWithTag("Mountain");
            foreach (GameObject block in mountainBlocks)
            {
                MeshRenderer rnd = block.GetComponent<MeshRenderer>();
                bool state = rnd.enabled;
                rnd.enabled = !state;
            }

            GameObject[] mountainRiverbedBlocks;
            mountainRiverbedBlocks = GameObject.FindGameObjectsWithTag("MountainRiverbed");
            foreach (GameObject block in mountainRiverbedBlocks)
            {
                MeshRenderer rnd = block.GetComponent<MeshRenderer>();
                bool state = rnd.enabled;
                rnd.enabled = !state;
            }
        }

        if (GUILayout.Button("Hide/Show All Riverbed"))
        {
            GameObject[] riverbedBlocks;
            riverbedBlocks = GameObject.FindGameObjectsWithTag("Riverbed");
            foreach (GameObject block in riverbedBlocks)
            {
                MeshRenderer rnd = block.GetComponent<MeshRenderer>();
                bool state = rnd.enabled;
                rnd.enabled = !state;
            }
        }

        if (GUILayout.Button("Hide/Show All Caves"))
        {
            GameObject[] caveBlocks;
            caveBlocks = GameObject.FindGameObjectsWithTag("Caves");
            foreach (GameObject block in caveBlocks)
            {
                MeshRenderer rnd = block.GetComponent<MeshRenderer>();
                bool state = rnd.enabled;
                rnd.enabled = !state;
            }
        }
    }
}
