using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk
{
    public const int ChunkSize = 16;
    public const int ChunkHeight = 64;
    public const int SeaLevel = 12;

    public Chunk(Vector3 WorldPosition)
    {
        WorldPos = WorldPosition;
        Blocks = new Space[ChunkSize, ChunkHeight, ChunkSize];
        Heightmap = new int[ChunkSize, ChunkSize];
    }

    public Vector3 WorldPos { get; set; }
    public Vector2 NoisePos { get { return new Vector2(WorldPos.x, WorldPos.z); } }
    public Vector2 Size { get { return Vector2.one * ChunkSize; } }

    public Space[,,] Blocks { get; set; }
    public int[,] Heightmap { get; set; }

    public bool HasBlockAt(int x, int y, int z)
    {
        if (x >= ChunkSize || y >= ChunkHeight || z >= ChunkSize || z < 0 || y < 0 || x < 0)
            return false;

        var val = Blocks[x, y, z];
        return val.Block != global::Blocks.Air;
    }

    public Vector3 WorldToLocal(Vector3 Pos)
    {
        var diff = Pos - WorldPos;
        return new Vector3(Mathf.Floor(diff.x), Mathf.Ceil(diff.y), Mathf.Floor(diff.z));
    }

    public Vector3 LocalToWorld(Vector3 local)
    {
        return local + WorldPos;
    }

    public void ApplyBlock(int x, int y, int z, int id)
    {
        if (x < 0 || y < 0 || z < 0 || x > ChunkSize - 1 || y > ChunkHeight - 1 || z > ChunkSize - 1)
            return;

        Blocks[x, y, z].Block = id;
        Blocks[x, y, z].Height = y;
    }
}

public struct Space
{
    public int Block;
    public int Height;
}

public class ChunkGenerator : MonoBehaviour 
{
    public Generator Generator;

    public Chunk Reference;

    public bool ForceGenerate;

    public MeshCollider MeshCollider;

    void Start()
    {
        MeshCollider = gameObject.AddComponent<MeshCollider>();
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
        MeshCollider.sharedMesh = m;
    }
}
