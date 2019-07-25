using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Block
{
    public virtual string Name { get; set; }
    public virtual int BlockID { get; set; }
    public virtual int Submesh { get; set; }

    public virtual Face LeftFaceUV { get; set; }
    public virtual Face RightFaceUV { get; set; }

    public virtual Face FrontFaceUV { get; set; }
    public virtual Face BackFaceUV { get; set; }

    public virtual Face TopFaceUV { get; set; }
    public virtual Face BottomFaceUV { get; set; }

    public virtual bool IsStackable { get; set; }
    public virtual int MaxStack { get; set; }
}

public class Face
{
    public Vector2 UV1;
    public Vector2 UV2;
    public Vector2 UV3;
    public Vector2 UV4;

    public Vector2 OverlayUV1;
    public Vector2 OverlayUV2;
    public Vector2 OverlayUV3;
    public Vector2 OverlayUV4;

    public Color VertexColor;

    public static Face GetFromIndex(int x, int y, Color? color = null, int overlayX = -1, int overlayY = -1)
    {
        y++;
        var face = new Face()
        {
            UV4 = new Vector2(MeshGenerator.UVSize * x, 1 - MeshGenerator.UVSize * y),
            UV3 = new Vector2(MeshGenerator.UVSize * x + MeshGenerator.UVSize, 1 - MeshGenerator.UVSize * y),
            UV2 = new Vector2(MeshGenerator.UVSize * x, 1 - MeshGenerator.UVSize * y + MeshGenerator.UVSize),
            UV1 = new Vector2(MeshGenerator.UVSize * x + MeshGenerator.UVSize, 1 - MeshGenerator.UVSize * y + MeshGenerator.UVSize),
        };

        if(overlayX == -1 && overlayY == -1)
        {
            face.OverlayUV4 = face.UV4;
            face.OverlayUV3 = face.UV3;
            face.OverlayUV2 = face.UV2;
            face.OverlayUV1 = face.UV1;
        }
        else
        {
            face.OverlayUV4 = new Vector2(MeshGenerator.UVSize * overlayX, 1 - MeshGenerator.UVSize * overlayY);
            face.OverlayUV3 = new Vector2(MeshGenerator.UVSize * overlayX + MeshGenerator.UVSize, 1 - MeshGenerator.UVSize * overlayY);
            face.OverlayUV2 = new Vector2(MeshGenerator.UVSize * overlayX, 1 - MeshGenerator.UVSize * overlayY + MeshGenerator.UVSize);
            face.OverlayUV1 = new Vector2(MeshGenerator.UVSize * overlayX + MeshGenerator.UVSize, 1 - MeshGenerator.UVSize * overlayY + MeshGenerator.UVSize);
        }

        if(color.HasValue)
        {
            face.VertexColor = color.Value;
        }
        else
        {
            face.VertexColor = Color.white;
        }

        return face;
    }
}
