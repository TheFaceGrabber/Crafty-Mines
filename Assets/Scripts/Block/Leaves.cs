using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaves : Block 
{
    public override string Name
    {
        get
        {
            return "Leaves";
        }
    }

    public override int BlockID
    {
        get
        {
            return 6;
        }
    }

    public override bool IsStackable
    {
        get
        {
            return true;
        }
    }

    public override Face FrontFaceUV
    {
        get
        {
            return Face.GetFromIndex(4, 3, new Color(102f / 255f, 168f / 255f, 88f / 255f));
        }
    }

    public override Face BackFaceUV
    {
        get
        {
            return Face.GetFromIndex(4, 3, new Color(102f / 255f, 168f / 255f, 88f / 255f));
        }
    }

    public override Face TopFaceUV
    {
        get
        {
            return Face.GetFromIndex(4, 3, new Color(102f / 255f, 168f / 255f, 88f / 255f));
        }
    }

    public override Face BottomFaceUV
    {
        get
        {
            return Face.GetFromIndex(4, 3, new Color(102f / 255f, 168f / 255f, 88f / 255f));
        }
    }

    public override Face LeftFaceUV
    {
        get
        {
            return Face.GetFromIndex(4, 3, new Color(102f / 255f, 168f / 255f, 88f / 255f));
        }
    }

    public override Face RightFaceUV
    {
        get
        {
            return Face.GetFromIndex(4, 3, new Color(102f / 255f, 168f / 255f, 88f / 255f));
        }
    }
}
