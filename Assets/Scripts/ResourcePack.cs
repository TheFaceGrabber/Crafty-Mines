using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcePack : MonoBehaviour {

    public static void Load(string pack = null)
    {
        if(string.IsNullOrEmpty(pack))
        {
            //Load default
            var terrain = Resources.Load<Texture>("default/terrain");
            Shader.SetGlobalTexture("_TERRAIN_TEX", terrain);
        }
        else
        {
            //Load from folder
        }
    }

}
