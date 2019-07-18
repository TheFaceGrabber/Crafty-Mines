using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(menuName = "Generator/Basic", fileName = "Basic Generator")]
public class Generator : ScriptableObject
{
    public float NoiseScale = 10;
    public float HeightScale = 2;
    public float ChanceOfTree = 20;
    public int TreeCount = 20;
    public int Seed = 10;

    public Structure Tree;

    /// <summary>
    /// Called just before generation - do anything required before main call here.
    /// </summary>
    /// <param name="chunk">Chunk.</param>
    public virtual void OnPreGenerate(Chunk chunk)
    {
        Random.InitState(Seed);
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
                chunk.Heightmap[x, z] = height;
                for (int y = 0; y < height; y++)
                {
                    if (y > height)
                    {
                        chunk.ApplyBlock(x, y, z, Blocks.Air);
                    }
                    else if (y == height - 1)
                    {
                        if (height - 1 > Chunk.SeaLevel)
                            chunk.ApplyBlock(x, y, z, Blocks.Grass);
                        else if(!chunk.HasBlockAt(x,y+1,z))
                            chunk.ApplyBlock(x, y, z, Blocks.Sand);
                    }
                    else if (y <= height - 5)
                    {
                        chunk.ApplyBlock(x, y, z, Blocks.Stone);
                    }
                    else
                    {
                        if (y > Chunk.SeaLevel)
                            chunk.ApplyBlock(x, y, z, Blocks.Dirt);
                        else
                            chunk.ApplyBlock(x, y, z, Blocks.Sand);
                    }
                }

                /*if (height - 1 > Chunk.SeaLevel + 2 
                    && x > 3 && x < Chunk.ChunkSize-4 
                    && z > 3 && z < Chunk.ChunkSize-4)
                {
                    var r = Random.value * 100;
                    if (r > 100 - ChanceOfTree)
                    {
                        AddTree(x, height, z, chunk);
                    }
                }*/
            }
        }

        List<Vector2> treePoses = new List<Vector2>();
        int treeSize = 1;

        for(int i = 0; i < TreeCount; i++)
        {
            int xPos = Random.Range(2, Chunk.ChunkSize - 2);
            int yPos = Random.Range(2, Chunk.ChunkSize - 2);

            if (treePoses.Any(v => Mathf.Abs(v.x - xPos) <= treeSize && Mathf.Abs(v.y - yPos) <= treeSize))
                continue;

            treePoses.Add(new Vector2(xPos, yPos));
            if (CheckChance(ChanceOfTree))
            {
                AddTree(xPos, chunk.Heightmap[xPos,yPos], yPos, chunk);
            }
        }
    }

    void AddTree(int x, int y, int z, Chunk chunk)
    {
        if (y - 1 > Chunk.SeaLevel + 1)
        {
            int height = Random.Range(2, 5);
            CreateColumn(x, y, z, height, chunk);
            TrySpawnStructure(Tree, x, y + height, z, chunk);
        }
    }

    public void CreateColumn(int x, int y, int z, int height, Chunk chunk)
    {
        for (int i = 0; i < height; i++)
        {
            chunk.ApplyBlock(x, y + i, z, (int)Blocks.Log);
        }
    }

    public void TrySpawnStructure(Structure str, int xPos, int yPos, int zPos, Chunk chunk)
    {
        int centerX = Mathf.CeilToInt(str.sizeX / 2);
        int centerY = Mathf.CeilToInt(str.sizeY / 2);
        for (int i = 0; i < str.Layers.Count; i++)
        {
            var layer = str.Layers[i];
            for (int x = 0; x < str.sizeX; x++)
            {
                for (int z = 0; z < str.sizeY; z++)
                {
                    int xActual = (x - centerX) + xPos;
                    int yActual = yPos + i;
                    int zActual = (z - centerY) + zPos;

                    if (layer.Grid[x + z * str.sizeX] == ' ')
                        continue;

                    var block = str.Key.SingleOrDefault(v => v.Key == layer.Grid[x + z * str.sizeX]);
                    float r = Random.value;
                    if (block.ChanceToSpawn <= r || block.ChanceToSpawn == 1)
                    {
                        int blockVal = block.Value + 1;

                        chunk.ApplyBlock(xActual, yActual, zActual, blockVal);
                    }
                }
            }
        }
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
        float x = pos.x / Chunk.ChunkSize * NoiseScale;
        float y = pos.y / Chunk.ChunkSize * NoiseScale;
        return Perlin.Fbm(x,y,5) * 2 - 1;
    }
}
