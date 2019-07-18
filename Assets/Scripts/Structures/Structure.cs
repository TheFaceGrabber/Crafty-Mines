using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Structure", menuName = "Structure")]
public class Structure : ScriptableObject 
{
    public int sizeX = 0, sizeY = 0;


    public List<KeyItem> Key = new List<KeyItem>();
    public List<Layer> Layers = new List<Layer>();
}

[System.Serializable]
public class KeyItem
{
    public char Key;
    public int Value;
    public float ChanceToSpawn = 1;
}

[System.Serializable]
public class Layer
{
    public char[] Grid;
}
