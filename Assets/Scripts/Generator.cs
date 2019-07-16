using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Generator/Basic", fileName = "Basic Generator")]
public class Generator : ScriptableObject
{
    public float NoiseScale = 10;
    public float HeightScale = 2;

    /// <summary>
    /// Called just before generation - do anything required before main call here.
    /// </summary>
    /// <param name="chunk">Chunk.</param>
    public virtual void OnPreGenerate(Chunk chunk)
    {

    }

    /// <summary>
    /// Ons the generate.
    /// </summary>
    /// <param name="chunk">Chunk.</param>
    public virtual void OnGenerate(Chunk chunk)
    {
        for (int x = 0; x < Chunk.ChunkSize; x++)
        {
            for (int z = 0; z < Chunk.ChunkSize; z++)
            {
                var pos = chunk.NoisePos;
                pos += new Vector2(x, z);
                int height = Mathf.RoundToInt(GetNoise(pos) * HeightScale) + 25;
                for (int y = 0; y < height; y++)
                {
                    if (y > height)
                    {
                        chunk.Blocks[x, y, z].Block = Blocks.Air;
                    }
                    else if (y == height - 1)
                    {
                        if (height - 1 > Chunk.SeaLevel)
                            chunk.Blocks[x, y, z].Block = Blocks.Grass;
                        else
                            chunk.Blocks[x, y, z].Block = Blocks.Sand;
                    }
                    else if (y <= height - 5)
                    {
                        chunk.Blocks[x, y, z].Block = Blocks.Stone;
                    }
                    else
                    {
                        if (y > Chunk.SeaLevel)
                            chunk.Blocks[x, y, z].Block = Blocks.Dirt;
                        else
                            chunk.Blocks[x, y, z].Block = Blocks.Sand;
                    }
                    chunk.Blocks[x, y, z].Height = y;
                }
            }
        }
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
        float x = pos.x / Chunk.ChunkSize * NoiseScale;
        float y = pos.y / Chunk.ChunkSize * NoiseScale;
        return Mathf.PerlinNoise(x, y) * 2 - 1;
    }
}
