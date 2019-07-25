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
        List<Vector2> uv2 = new List<Vector2>();
        List<Color> colors = new List<Color>();
        List<List<int>> indeces = new List<List<int>>();
        for(int sub = 0; sub < BlockRegistrar.MaxSubmeshes + 1; sub++)
        {
            indeces.Add(new List<int>());
        }
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

                        colors.Add(b.TopFaceUV.VertexColor);
                        colors.Add(b.TopFaceUV.VertexColor);
                        colors.Add(b.TopFaceUV.VertexColor);
                        colors.Add(b.TopFaceUV.VertexColor);

                        uvs.Add(b.TopFaceUV.UV1);
                        uvs.Add(b.TopFaceUV.UV2);
                        uvs.Add(b.TopFaceUV.UV3);
                        uvs.Add(b.TopFaceUV.UV4);

                        uv2.Add(b.TopFaceUV.OverlayUV1);
                        uv2.Add(b.TopFaceUV.OverlayUV2);
                        uv2.Add(b.TopFaceUV.OverlayUV3);
                        uv2.Add(b.TopFaceUV.OverlayUV4);

                        indeces[b.Submesh].Add(vertCount + 0);
                        indeces[b.Submesh].Add(vertCount + 2);
                        indeces[b.Submesh].Add(vertCount + 1);

                        indeces[b.Submesh].Add(vertCount + 2);
                        indeces[b.Submesh].Add(vertCount + 3);
                        indeces[b.Submesh].Add(vertCount + 1);

                        vertCount += 4;
                    }
                    //Add Bottom
                    if (!chunk.HasBlockAt(x, y - 1, z))
                    {
                        verts.Add(new Vector3(x, space.Height - 1, z));
                        verts.Add(new Vector3(x + 1, space.Height - 1, z));
                        verts.Add(new Vector3(x, space.Height - 1, z + 1));
                        verts.Add(new Vector3(x + 1, space.Height - 1, z + 1));

                        colors.Add(b.BottomFaceUV.VertexColor);
                        colors.Add(b.BottomFaceUV.VertexColor);
                        colors.Add(b.BottomFaceUV.VertexColor);
                        colors.Add(b.BottomFaceUV.VertexColor);

                        uvs.Add(b.BottomFaceUV.UV1);
                        uvs.Add(b.BottomFaceUV.UV2);
                        uvs.Add(b.BottomFaceUV.UV3);
                        uvs.Add(b.BottomFaceUV.UV4);

                        uv2.Add(b.BottomFaceUV.OverlayUV1);
                        uv2.Add(b.BottomFaceUV.OverlayUV2);
                        uv2.Add(b.BottomFaceUV.OverlayUV3);
                        uv2.Add(b.BottomFaceUV.OverlayUV4);

                        indeces[b.Submesh].Add(vertCount + 1);
                        indeces[b.Submesh].Add(vertCount + 2);
                        indeces[b.Submesh].Add(vertCount + 0);

                        indeces[b.Submesh].Add(vertCount + 1);
                        indeces[b.Submesh].Add(vertCount + 3);
                        indeces[b.Submesh].Add(vertCount + 2);

                        vertCount += 4;
                    }
                    //Add Front
                    if (!chunk.HasBlockAt(x, y, z + 1))
                    {
                        verts.Add(new Vector3(x, space.Height, z + 1));
                        verts.Add(new Vector3(x + 1, space.Height, z + 1));
                        verts.Add(new Vector3(x, space.Height - 1, z + 1));
                        verts.Add(new Vector3(x + 1, space.Height - 1, z + 1));

                        colors.Add(b.FrontFaceUV.VertexColor);
                        colors.Add(b.FrontFaceUV.VertexColor);
                        colors.Add(b.FrontFaceUV.VertexColor);
                        colors.Add(b.FrontFaceUV.VertexColor);

                        uvs.Add(b.FrontFaceUV.UV1);
                        uvs.Add(b.FrontFaceUV.UV2);
                        uvs.Add(b.FrontFaceUV.UV3);
                        uvs.Add(b.FrontFaceUV.UV4);

                        uv2.Add(b.FrontFaceUV.OverlayUV1);
                        uv2.Add(b.FrontFaceUV.OverlayUV2);
                        uv2.Add(b.FrontFaceUV.OverlayUV3);
                        uv2.Add(b.FrontFaceUV.OverlayUV4);

                        indeces[b.Submesh].Add(vertCount + 0);
                        indeces[b.Submesh].Add(vertCount + 2);
                        indeces[b.Submesh].Add(vertCount + 1);
							   
                        indeces[b.Submesh].Add(vertCount + 2);
                        indeces[b.Submesh].Add(vertCount + 3);
                        indeces[b.Submesh].Add(vertCount + 1);

                        vertCount += 4;
                    }
                    //Add Back
                    if (!chunk.HasBlockAt(x, y, z - 1))
                    {
                        verts.Add(new Vector3(x, space.Height, z));
                        verts.Add(new Vector3(x + 1, space.Height, z));
                        verts.Add(new Vector3(x, space.Height - 1, z));
                        verts.Add(new Vector3(x + 1, space.Height - 1, z));

                        colors.Add(b.BackFaceUV.VertexColor);
                        colors.Add(b.BackFaceUV.VertexColor);
                        colors.Add(b.BackFaceUV.VertexColor);
                        colors.Add(b.BackFaceUV.VertexColor);

                        uvs.Add(b.BackFaceUV.UV1);
                        uvs.Add(b.BackFaceUV.UV2);
                        uvs.Add(b.BackFaceUV.UV3);
                        uvs.Add(b.BackFaceUV.UV4);

                        uv2.Add(b.BackFaceUV.OverlayUV1);
                        uv2.Add(b.BackFaceUV.OverlayUV2);
                        uv2.Add(b.BackFaceUV.OverlayUV3);
                        uv2.Add(b.BackFaceUV.OverlayUV4);

                        indeces[b.Submesh].Add(vertCount + 1);
                        indeces[b.Submesh].Add(vertCount + 2);
                        indeces[b.Submesh].Add(vertCount + 0);

                        indeces[b.Submesh].Add(vertCount + 1);
                        indeces[b.Submesh].Add(vertCount + 3);
                        indeces[b.Submesh].Add(vertCount + 2);

                        vertCount += 4;
                    }
                    //Add Left
                    if (!chunk.HasBlockAt(x - 1, y, z))
                    {
                        verts.Add(new Vector3(x, space.Height, z));
                        verts.Add(new Vector3(x, space.Height, z + 1));
                        verts.Add(new Vector3(x, space.Height - 1, z));
                        verts.Add(new Vector3(x, space.Height - 1, z + 1));

                        colors.Add(b.LeftFaceUV.VertexColor);
                        colors.Add(b.LeftFaceUV.VertexColor);
                        colors.Add(b.LeftFaceUV.VertexColor);
                        colors.Add(b.LeftFaceUV.VertexColor);

                        uvs.Add(b.LeftFaceUV.UV1);
                        uvs.Add(b.LeftFaceUV.UV2);
                        uvs.Add(b.LeftFaceUV.UV3);
                        uvs.Add(b.LeftFaceUV.UV4);

                        uv2.Add(b.LeftFaceUV.OverlayUV1);
                        uv2.Add(b.LeftFaceUV.OverlayUV2);
                        uv2.Add(b.LeftFaceUV.OverlayUV3);
                        uv2.Add(b.LeftFaceUV.OverlayUV4);

                        indeces[b.Submesh].Add(vertCount + 0);
                        indeces[b.Submesh].Add(vertCount + 2);
                        indeces[b.Submesh].Add(vertCount + 1);

                        indeces[b.Submesh].Add(vertCount + 2);
                        indeces[b.Submesh].Add(vertCount + 3);
                        indeces[b.Submesh].Add(vertCount + 1);

                        vertCount += 4;
                    }
                    //Add Right
                    if (!chunk.HasBlockAt(x + 1, y, z))
                    {
                        verts.Add(new Vector3(x + 1, space.Height, z));
                        verts.Add(new Vector3(x + 1, space.Height, z + 1));
                        verts.Add(new Vector3(x + 1, space.Height - 1, z));
                        verts.Add(new Vector3(x + 1, space.Height - 1, z + 1));

                        colors.Add(b.RightFaceUV.VertexColor);
                        colors.Add(b.RightFaceUV.VertexColor);
                        colors.Add(b.RightFaceUV.VertexColor);
                        colors.Add(b.RightFaceUV.VertexColor);

                        uvs.Add(b.RightFaceUV.UV1);
                        uvs.Add(b.RightFaceUV.UV2);
                        uvs.Add(b.RightFaceUV.UV3);
                        uvs.Add(b.RightFaceUV.UV4);

                        uv2.Add(b.RightFaceUV.OverlayUV1);
                        uv2.Add(b.RightFaceUV.OverlayUV2);
                        uv2.Add(b.RightFaceUV.OverlayUV3);
                        uv2.Add(b.RightFaceUV.OverlayUV4);

                        indeces[b.Submesh].Add(vertCount + 1);
                        indeces[b.Submesh].Add(vertCount + 2);
                        indeces[b.Submesh].Add(vertCount + 0);

                        indeces[b.Submesh].Add(vertCount + 1);
                        indeces[b.Submesh].Add(vertCount + 3);
                        indeces[b.Submesh].Add(vertCount + 2);

                        vertCount += 4;
                    }
                }
            }
        }

        mesh.subMeshCount = BlockRegistrar.MaxSubmeshes + 1;

        mesh.vertices = verts.ToArray();
        for(int i =0; i < indeces.Count; i++)
        {
            mesh.SetTriangles(indeces[i].ToArray(), i);
        }
        mesh.uv = uvs.ToArray();
        mesh.uv2 = uv2.ToArray();
        mesh.colors = colors.ToArray();
        mesh.RecalculateNormals();
        
        return mesh;
    }
}
