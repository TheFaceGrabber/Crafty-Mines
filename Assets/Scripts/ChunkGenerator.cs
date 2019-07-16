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
    public Generator Generator;

    public Chunk Reference;

    public bool ForceGenerate;

    void Start()
    {
        BlockRegistrar.Init();
        Run();
    }

    public void Run()
    {
        Reference = new Chunk(transform.position);
        Generator.OnPreGenerate(Reference);
        Generator.OnGenerate(Reference);
        Generator.OnPostGenerate(Reference);
        UpdateMesh();
    }

    public void UpdateMesh()
    {
        var m = MeshGenerator.GenerateMesh(Reference);
        GetComponent<MeshFilter>().mesh = m;
        var col = gameObject.AddComponent<MeshCollider>();
        col.sharedMesh = m;
    }
}
