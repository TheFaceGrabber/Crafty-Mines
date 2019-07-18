using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MeshGenerator 
{
    public const float UVSize = (1f / 16f);

    public static Mesh GenerateMesh(Chunk chunk)
    {
        Mesh mesh = new Mesh();
        List<Vector3> verts = new List<Vector3>();
        List<Vector2> uvs = new List<Vector2>();
        List<int> indeces = new List<int>();
        int vertCount = 0;
        for(int x = 0; x < Chunk.ChunkSize; x++)
        {
            for(int y = 0; y < Chunk.ChunkHeight; y++) 
            {
                for (int z = 0; z < Chunk.ChunkSize; z++)
                {
                    Space space = chunk.Blocks[x, y, z];
                    if (space.Block == (int)Blocks.Air) continue;
                    Block b = BlockRegistrar.GetBlock((int)space.Block);
                    if (b == null) continue;
                    //Draw top
                    if (!chunk.HasBlockAt(x, y + 1, z))
                    {
                        verts.Add(new Vector3(x, space.Height, z));
                        verts.Add(new Vector3(x + 1, space.Height, z));
                        verts.Add(new Vector3(x, space.Height, z + 1));
                        verts.Add(new Vector3(x + 1, space.Height, z + 1));

                        uvs.Add(b.TopFaceUV.UV1);
                        uvs.Add(b.TopFaceUV.UV2);
                        uvs.Add(b.TopFaceUV.UV3);
                        uvs.Add(b.TopFaceUV.UV4);

                        indeces.Add(vertCount + 0);
                        indeces.Add(vertCount + 2);
                        indeces.Add(vertCount + 1);

                        indeces.Add(vertCount + 2);
                        indeces.Add(vertCount + 3);
                        indeces.Add(vertCount + 1);

                        vertCount += 4;
                    }
                    //Add Bottom
                    if (!chunk.HasBlockAt(x, y - 1, z))
                    {
                        verts.Add(new Vector3(x, space.Height - 1, z));
                        verts.Add(new Vector3(x + 1, space.Height - 1, z));
                        verts.Add(new Vector3(x, space.Height - 1, z + 1));
                        verts.Add(new Vector3(x + 1, space.Height - 1, z + 1));

                        uvs.Add(b.BottomFaceUV.UV1);
                        uvs.Add(b.BottomFaceUV.UV2);
                        uvs.Add(b.BottomFaceUV.UV3);
                        uvs.Add(b.BottomFaceUV.UV4);

                        indeces.Add(vertCount + 1);
                        indeces.Add(vertCount + 2);
                        indeces.Add(vertCount + 0);

                        indeces.Add(vertCount + 1);
                        indeces.Add(vertCount + 3);
                        indeces.Add(vertCount + 2);

                        vertCount += 4;
                    }
                    //Add Front
                    if (!chunk.HasBlockAt(x, y, z + 1))
                    {
                        verts.Add(new Vector3(x, space.Height, z + 1));
                        verts.Add(new Vector3(x + 1, space.Height, z + 1));
                        verts.Add(new Vector3(x, space.Height - 1, z + 1));
                        verts.Add(new Vector3(x + 1, space.Height - 1, z + 1));

                        uvs.Add(b.FrontFaceUV.UV1);
                        uvs.Add(b.FrontFaceUV.UV2);
                        uvs.Add(b.FrontFaceUV.UV3);
                        uvs.Add(b.FrontFaceUV.UV4);

                        indeces.Add(vertCount + 0);
                        indeces.Add(vertCount + 2);
                        indeces.Add(vertCount + 1);

                        indeces.Add(vertCount + 2);
                        indeces.Add(vertCount + 3);
                        indeces.Add(vertCount + 1);

                        vertCount += 4;
                    }
                    //Add Back
                    if (!chunk.HasBlockAt(x, y, z - 1))
                    {
                        verts.Add(new Vector3(x, space.Height, z));
                        verts.Add(new Vector3(x + 1, space.Height, z));
                        verts.Add(new Vector3(x, space.Height - 1, z));
                        verts.Add(new Vector3(x + 1, space.Height - 1, z));

                        uvs.Add(b.BackFaceUV.UV1);
                        uvs.Add(b.BackFaceUV.UV2);
                        uvs.Add(b.BackFaceUV.UV3);
                        uvs.Add(b.BackFaceUV.UV4);

                        indeces.Add(vertCount + 1);
                        indeces.Add(vertCount + 2);
                        indeces.Add(vertCount + 0);

                        indeces.Add(vertCount + 1);
                        indeces.Add(vertCount + 3);
                        indeces.Add(vertCount + 2);

                        vertCount += 4;
                    }
                    //Add Left
                    if (!chunk.HasBlockAt(x - 1, y, z))
                    {
                        verts.Add(new Vector3(x, space.Height, z));
                        verts.Add(new Vector3(x, space.Height, z + 1));
                        verts.Add(new Vector3(x, space.Height - 1, z));
                        verts.Add(new Vector3(x, space.Height - 1, z + 1));

                        uvs.Add(b.LeftFaceUV.UV1);
                        uvs.Add(b.LeftFaceUV.UV2);
                        uvs.Add(b.LeftFaceUV.UV3);
                        uvs.Add(b.LeftFaceUV.UV4);

                        indeces.Add(vertCount + 0);
                        indeces.Add(vertCount + 2);
                        indeces.Add(vertCount + 1);

                        indeces.Add(vertCount + 2);
                        indeces.Add(vertCount + 3);
                        indeces.Add(vertCount + 1);

                        vertCount += 4;
                    }
                    //Add Right
                    if (!chunk.HasBlockAt(x + 1, y, z))
                    {
                        verts.Add(new Vector3(x + 1, space.Height, z));
                        verts.Add(new Vector3(x + 1, space.Height, z + 1));
                        verts.Add(new Vector3(x + 1, space.Height - 1, z));
                        verts.Add(new Vector3(x + 1, space.Height - 1, z + 1));

                        uvs.Add(b.RightFaceUV.UV1);
                        uvs.Add(b.RightFaceUV.UV2);
                        uvs.Add(b.RightFaceUV.UV3);
                        uvs.Add(b.RightFaceUV.UV4);

                        indeces.Add(vertCount + 1);
                        indeces.Add(vertCount + 2);
                        indeces.Add(vertCount + 0);

                        indeces.Add(vertCount + 1);
                        indeces.Add(vertCount + 3);
                        indeces.Add(vertCount + 2);

                        vertCount += 4;
                    }
                }
            }
        }

        mesh.vertices = verts.ToArray();
        mesh.triangles = indeces.ToArray();
        mesh.uv = uvs.ToArray();
        mesh.RecalculateNormals();
        
        return mesh;
    }
}
