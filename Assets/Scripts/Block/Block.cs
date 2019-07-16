using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Block
{
    public virtual string Name { get; set; }
    public virtual int BlockID { get; set; }

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

    public static Face GetFromIndex(int x, int y)
    {
        y++;
        var face = new Face()
        {
            UV4 = new Vector2(MeshGenerator.UVSize * x, 1 - MeshGenerator.UVSize * y),
            UV3 = new Vector2(MeshGenerator.UVSize * x + MeshGenerator.UVSize, 1 - MeshGenerator.UVSize * y),
            UV2 = new Vector2(MeshGenerator.UVSize * x, 1 - MeshGenerator.UVSize * y + MeshGenerator.UVSize),
            UV1 = new Vector2(MeshGenerator.UVSize * x + MeshGenerator.UVSize, 1 - MeshGenerator.UVSize * y + MeshGenerator.UVSize),
        };

        return face;
    }
}
