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
    [SerializeField]
    public GameObject oldPrefab;
    [MenuItem("Tools/Blockout Editor")]
    public static void Init()
    {
        BlockoutEditor window = (BlockoutEditor)EditorWindow.GetWindow(typeof(BlockoutEditor));
        window.Show();
    }

    void OnGUI()
    {
        GUILayout.Label("Replace Prefabs", EditorStyles.boldLabel);
        oldPrefab = (GameObject)EditorGUILayout.ObjectField("Old Prefab", oldPrefab, typeof(GameObject), false);
        
        
        EditorGUILayout.Space(10);

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

        if (GUILayout.Button("Reset Prefab"))
        {
            BlockoutFixer[] blocks;
            blocks = GameObject.FindObjectsOfType<BlockoutFixer>();

            GameObject p = blocks[0].gameObject.transform.parent.gameObject;
            Debug.Log(p.transform.name + " parent");

            int i = 0;
            foreach (BlockoutFixer block in blocks)
            {
                GameObject o = block.gameObject;

                Debug.Log(o.transform.name + " " + o.transform.position);

                GameObject replacement = PrefabUtility.InstantiatePrefab(oldPrefab) as GameObject;
                replacement.transform.position = o.transform.position;
                replacement.transform.rotation = o.transform.rotation;
                replacement.name = "Block_3x3x3 " + i;
                replacement.transform.parent = p.transform;
                replacement.tag = o.tag;
                i++;

                o.SetActive(false);
            }
        }
    }
}
