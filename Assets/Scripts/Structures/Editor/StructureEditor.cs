using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

[CustomEditor(typeof(Structure))]
public class StructureEditor : Editor
{
    List<string> AllBlocksNames = new List<string>();

    bool Keys;
    bool Layers;

    int selectedLayer = -1;

    Structure Structure;

    private void Awake()
    {
        BlockRegistrar.Init();
        foreach(var block in BlockRegistrar.Blocks.Keys)
        {
            AllBlocksNames.Add(BlockRegistrar.Blocks[block].Name);
        }

        Structure = (Structure)this.target;
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Size X: ");
        Structure.sizeX = EditorGUILayout.IntField(Structure.sizeX);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Size Y: ");
        Structure.sizeY = EditorGUILayout.IntField(Structure.sizeY);
        EditorGUILayout.EndHorizontal();
        if(GUILayout.Button("Update Sizes"))
        {
            foreach(Layer l in Structure.Layers)
            {
                l.Grid = new char[Structure.sizeX * Structure.sizeY];
            }
        }
        Keys = EditorGUILayout.Foldout(Keys, "Keys");
        if(Keys)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Add Key: ");
            if(GUILayout.Button("+"))
            {
                Structure.Key.Add(new KeyItem());
            }
            EditorGUILayout.EndHorizontal();
            for(int i = 0; i < Structure.Key.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Key");
                Structure.Key[i].Key = EditorGUILayout.TextField(Structure.Key[i].Key.ToString())[0];
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Block");
                Structure.Key[i].Value = EditorGUILayout.Popup(Structure.Key[i].Value, AllBlocksNames.ToArray());

                
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Chance To Spawn");
                Structure.Key[i].ChanceToSpawn = EditorGUILayout.Slider(Structure.Key[i].ChanceToSpawn, 0, 1);
                EditorGUILayout.EndHorizontal();

                if (GUILayout.Button("-"))
                {
                    Structure.Key.RemoveAt(i);
                }

                EditorGUILayout.Separator();
            }
        }


        Layers = EditorGUILayout.Foldout(Layers, "Layers");
        if (Layers)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Add Layer: ");
            if (GUILayout.Button("+"))
            {
                Structure.Layers.Add(new Layer()
                {
                    Grid = new char[Structure.sizeX * Structure.sizeY]
                });
            }
            EditorGUILayout.EndHorizontal();
            List<string> layers = new List<string>();
            Structure.Layers.ForEach(x => layers.Add(Structure.Layers.IndexOf(x).ToString()));

            selectedLayer = GUILayout.SelectionGrid(selectedLayer, layers.ToArray(), 10);

            if(selectedLayer < layers.Count && layers.Count > 0 && selectedLayer >= 0)
            {
                EditorGUILayout.LabelField("Selected Layer");
                if (GUILayout.Button("-"))
                {
                    Structure.Layers.RemoveAt(selectedLayer);
                    selectedLayer--;
                }

                if (layers.Count == 0) return;
                Layer l = Structure.Layers[selectedLayer];

                for (int y = 0; y < Structure.sizeY; y++)
                {
                    EditorGUILayout.BeginHorizontal();
                    for (int x = 0; x < Structure.sizeX; x++)
                    {
                        Structure.Layers[selectedLayer].Grid[x + y * Structure.sizeX] =
                            EditorGUILayout.TextField(Structure.Layers[selectedLayer].Grid[x + y * Structure.sizeX].ToString(),
                            GUILayout.Width(16))[0];
                        if(!Structure.Key.Any(v => v.Key == Structure.Layers[selectedLayer].Grid[x + y * Structure.sizeX]))
                        {
                            Structure.Layers[selectedLayer].Grid[x + y * Structure.sizeX] = ' ';
                        }
                    }
                   EditorGUILayout.EndHorizontal();
                }
            }
        }
        if(GUI.changed)
            EditorUtility.SetDirty(target);
    }
}
