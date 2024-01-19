// Date: 1.18.2024
// Author: ethearian
// Generate project folder structure.

using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.IO;

[InitializeOnLoad]
public class GenerateFolderStructure
{
    static GenerateFolderStructure()
    {
        if (EditorApplication.timeSinceStartup < 60.0f)
        {
            Debug.Log("editor: folder structure generated.");

            List<string> folders = new List<string>
            {
                "Animations",
                "Audio",
                "Editor",
                "Materials",
                "Meshes",
                "Particles",
                "Prefabs",
                "Scenes",
                "Scripts",
                "ScriptTemplates",
                "Settings",
                "Shaders",
                "Textures",
                "ThirdParty",
                "UI"
            };

            // Create folders
            foreach (string folder in folders)
            {
                if (!Directory.Exists("Assets/" + folder))
                {
                    Directory.CreateDirectory("Assets/" + folder);
                }
            }

            List<string> uiFolders = new List<string>
            {
                "Assets",
                "Fonts",
                "Icons"
            };

            // Create UI subfolders
            foreach (string subfolder in uiFolders)
            {
                if (!Directory.Exists("Assets/UI/" + subfolder))
                {
                    Directory.CreateDirectory("Assets/UI/" + subfolder);
                }
            }

            AssetDatabase.Refresh();
        }
    }
}
