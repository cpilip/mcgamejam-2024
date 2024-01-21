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
    
    private BlockoutManager bManager;

    [MenuItem("Tools/Blockout Editor")]
    public static void Init()
    {
        BlockoutEditor window = (BlockoutEditor)EditorWindow.GetWindow(typeof(BlockoutEditor));
        window.Show();

    }

    void OnGUI()
    {
        bManager = FindObjectOfType<BlockoutManager>();
        GUILayout.Label("Replace Prefabs", EditorStyles.boldLabel);
        oldPrefab = (GameObject)EditorGUILayout.ObjectField("Old Prefab", oldPrefab, typeof(GameObject), false);
        
        
        EditorGUILayout.Space(10);

        if (GUILayout.Button("buildTerra"))
        {
            bManager.buildTerra();
        }

        if (GUILayout.Button("buildMountains"))
        {
            bManager.buildMountains();
        }


        if (GUILayout.Button("buildValleys"))
        {
            bManager.buildValleys();
        }

        if (GUILayout.Button("buildCaves"))
        {
            bManager.buildCaves();
        }

        if (GUILayout.Button("buildWater"))
        {
            bManager.buildWater();
        }


        if (GUILayout.Button("Reset Prefab DO NOT CLICK"))
        {
            GameObject block = GameObject.FindObjectOfType<BlockoutFixer>().gameObject;

            for (int i = 0; i < block.transform.childCount; i++)
            {
                GameObject o = block.transform.GetChild(i).gameObject;
                if (o.tag == "Untagged")
                {
                    Debug.Log(o.transform.name + " " + o.transform.position);
                    o.tag = "Terra";
                }
            }

        }
    }
}
