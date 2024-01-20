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
            GameObject[] walls;
            walls = GameObject.FindGameObjectsWithTag("Invisible");
            foreach (GameObject wall in walls)
            {
                MeshRenderer rnd = wall.GetComponent<MeshRenderer>();
                bool state = rnd.enabled;
                rnd.enabled = !state;
            }
        }
    }
}
