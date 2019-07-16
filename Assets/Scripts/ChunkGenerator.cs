using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk
{
    public const int ChunkSize = 16;
    public const int ChunkHeight = 64;
    public const int SeaLevel = 21;

    public Chunk(Vector3 WorldPosition)
    {
        WorldPos = WorldPosition;
        Blocks = new Space[ChunkSize, ChunkHeight, ChunkSize];
    }

    public Vector3 WorldPos;
    public Vector2 NoisePos { get { return new Vector2(WorldPos.x, WorldPos.z); } }
    public Vector2 Size { get { return Vector2.one * ChunkSize; } }

    public Space[,,] Blocks;

    public bool HasBlockAt(int x, int y, int z)
    {
        if (x >= ChunkSize || y >= ChunkHeight || z >= ChunkSize || z < 0 || y < 0 || x < 0)
            return false;

        var val = Blocks[x, y, z];
        return val.Block != global::Blocks.Air;
    }
}

public struct Space
{
    public Blocks Block;
    public int Height;
}

public class ChunkGenerator : MonoBehaviour 
{
    public float NoiseScale = 10;
    public float HeightScale = 2;

    public Chunk Reference;

    public bool ForceGenerate;

    void Start()
    {
        BlockRegistrar.Init();
        Run();
    }

    private void Update()
    {
        if (ForceGenerate)
        {
            ForceGenerate = false;
            Generate();
            UpdateMesh();
        }
    }

    public void Run()
    {
        Generate();
        UpdateMesh();
    }

    public void UpdateMesh()
    {
        var m = MeshGenerator.GenerateMesh(Reference);
        GetComponent<MeshFilter>().mesh = m;
        var col = gameObject.AddComponent<MeshCollider>();
        col.sharedMesh = m;
    }

    void Generate()
    {
        Reference = new Chunk(transform.position);
        for (int x = 0; x < Chunk.ChunkSize; x++)
        {
            for (int z = 0; z < Chunk.ChunkSize; z++)
            {
                var pos = Reference.NoisePos;
                pos += new Vector2(x, z);
                int height = Mathf.RoundToInt(GeneratorNoise(pos) * HeightScale) + 25;
                for(int y = 0; y < height; y++)
                {
                    if(y > height)
                    {
                        Reference.Blocks[x, y, z].Block = Blocks.Air;
                    }
                    else if(y == height - 1)
                    {
                        if(height -1 > Chunk.SeaLevel)
                            Reference.Blocks[x, y, z].Block = Blocks.Grass;
                        else
                            Reference.Blocks[x, y, z].Block = Blocks.Sand;
                    }
                    else if(y <= height - 5)
                    {
                        Reference.Blocks[x, y, z].Block = Blocks.Stone;
                    }
                    else
                    {
                        if(y > Chunk.SeaLevel)
                            Reference.Blocks[x, y, z].Block = Blocks.Dirt;
                        else
                            Reference.Blocks[x, y, z].Block = Blocks.Sand;
                    }
                    Reference.Blocks[x, y,z ].Height = y;
                }
            }
        }
    }

    float GeneratorNoise(Vector2 pos)
    {
        float x = pos.x / Chunk.ChunkSize * NoiseScale;
        float y = pos.y / Chunk.ChunkSize * NoiseScale;
        return Mathf.PerlinNoise(x, y) * 2 - 1;
    }
}
