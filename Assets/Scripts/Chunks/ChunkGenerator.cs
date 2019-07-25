using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

//[System.Serializable]
public class Chunk
{
    public const int ChunkSize = 16;
    public const int ChunkHeight = 64;
    public const int SeaLevel = 12;

    public Chunk(Vector3 WorldPosition)
    {
        WorldPos = WorldPosition;
        WorldPos2D = new Vector2(WorldPos.x, WorldPos.z);
        Blocks = new Space[ChunkSize, ChunkHeight, ChunkSize];
        Heightmap = new int[ChunkSize, ChunkSize];

        FrontNeighbour = null;
        BackNeighbour = null;
        LeftNeighbour = null;
        RightNeighbour = null;
    }

    public Vector3 WorldPos { get; set; }
    public Vector2 WorldPos2D { get; set; }
    public Vector2 ChunkPos { get { return WorldPos / ChunkSize; } }
    public Vector2 NoisePos { get { return new Vector2(WorldPos.x, WorldPos.z); } }
    public Vector2 Size { get { return Vector2.one * ChunkSize; } }

    public Space[,,] Blocks { get; set; }
    public int[,] Heightmap { get; set; }

    public Chunk FrontNeighbour;
    public Chunk BackNeighbour;
    public Chunk LeftNeighbour;
    public Chunk RightNeighbour;

    public ChunkGenerator Generator;

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
        if (y < 0 || y > ChunkHeight - 1)
            return;

        if (x < 0)
        {
            Generator.UpdateNeighbours();

            LeftNeighbour.ApplyBlock(ChunkSize - x - 2, y, z, id);
            LeftNeighbour.Generator.UpdateMesh();
        }
        else if (x > ChunkSize - 1)
        {
            Generator.UpdateNeighbours();
            RightNeighbour.ApplyBlock(x - ChunkSize, y, z, id);
            RightNeighbour.Generator.UpdateMesh();
        }
        else if (z < 0)
        {
            Generator.UpdateNeighbours();

            BackNeighbour.ApplyBlock(x, y, ChunkSize - z - 2, id);
            BackNeighbour.Generator.UpdateMesh();
        }
        else if (z > ChunkSize - 1)
        {
            Generator.UpdateNeighbours();

            FrontNeighbour.ApplyBlock(x, y, z - ChunkSize, id);
            FrontNeighbour.Generator.UpdateMesh();
        }
        else
        {
            Blocks[x, y, z].Block = id;
            Blocks[x, y, z].Height = y;
        }
    }
}

public struct Space
{
    public int Block;
    public int Height;
}

public class ChunkGenerator : MonoBehaviour 
{
    public ChunkFollower Manager;

    public Generator Generator;

    public Chunk Reference;

    public bool ForceGenerate;

    public MeshCollider MeshCollider;

    void Awake()
    {
        MeshCollider = gameObject.AddComponent<MeshCollider>();
        BlockRegistrar.Init();
        Run();
    }

    public void UpdateNeighbours()
    {
        //if(Reference.FrontNeighbour == null)
        //{
        //if (Reference == null) return;

        var f = Manager.Chunks.SingleOrDefault(x => x.Position == Reference.WorldPos2D + new Vector2(0, Chunk.ChunkSize));
        if(f != null)
            Reference.FrontNeighbour = f.ChunkObject.Reference;
        //}
        //if (Reference.BackNeighbour == null)
        //{
        var b = Manager.Chunks.SingleOrDefault(x => x.Position == Reference.WorldPos2D - new Vector2(0, Chunk.ChunkSize));
        if(b != null)    
            Reference.BackNeighbour = b.ChunkObject.Reference;
        //}
        //if (Reference.LeftNeighbour == null)
        //{
        var l = Manager.Chunks.SingleOrDefault(x => x.Position == Reference.WorldPos2D - new Vector2(Chunk.ChunkSize, 0));
        if(l != null) 
           Reference.LeftNeighbour = l.ChunkObject.Reference;
        //}
        //if (Reference.RightNeighbour == null)
        //{
        var r = Manager.Chunks.SingleOrDefault(x => x.Position == Reference.WorldPos2D + new Vector2(Chunk.ChunkSize, 0));
        if(r != null)
            Reference.RightNeighbour = r.ChunkObject.Reference;
        //}
    }

    public void Run()
    {
        Reference = new Chunk(transform.position)
        {
            Generator = this
        };
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
