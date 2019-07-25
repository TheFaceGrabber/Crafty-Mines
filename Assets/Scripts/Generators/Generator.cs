using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(menuName = "Generator/Basic", fileName = "Basic Generator")]
public class Generator : ScriptableObject
{
    public int Seed = 10;

    //For Blending
    public float NoiseMultiplier { get; set; }
    public Generator PreviousGenerator { get; set; }

    /// <summary>
    /// Called just before generation - do anything required before main call here.
    /// </summary>
    /// <param name="chunk">Chunk.</param>
    public virtual void OnPreGenerate(Chunk chunk)
    {
        Random.InitState(Seed * (int)(chunk.WorldPos.x + chunk.WorldPos.z));
    }

    /// <summary>
    /// Ons the generate.
    /// </summary>
    /// <param name="chunk">Chunk.</param>
    public virtual void OnGenerate(Chunk chunk)
    {

    }

    public static bool CheckChance(float chance)
    {
        var r = Random.value * 100;
        if (r > 100 - chance)
        {
            return true;
        }

        return false;
    }
    /// <summary>
    /// Ons the post generate.
    /// </summary>
    /// <param name="chunk">Chunk.</param>
    public virtual void OnPostGenerate(Chunk chunk)
    {

    }
    public virtual float GetNoise(Vector2 pos)
    {
        return 0;
    }
}
