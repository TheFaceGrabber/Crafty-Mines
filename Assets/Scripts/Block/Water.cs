using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : Block 
{
    public override string Name
    {
        get
        {
            return "Water";
        }
    }

    public override int BlockID
    {
        get
        {
            return 7;
        }
    }

    public override int Submesh
    {
        get
        {
            return 2;
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
            return Face.GetFromIndex(13, 12);
        }
    }

    public override Face BackFaceUV
    {
        get
        {
            return Face.GetFromIndex(13, 12);
        }
    }

    public override Face TopFaceUV
    {
        get
        {
            return Face.GetFromIndex(13, 12);
        }
    }

    public override Face BottomFaceUV
    {
        get
        {
            return Face.GetFromIndex(13, 12);
        }
    }

    public override Face LeftFaceUV
    {
        get
        {
            return Face.GetFromIndex(13, 12);
        }
    }

    public override Face RightFaceUV
    {
        get
        {
            return Face.GetFromIndex(13, 12);
        }
    }
}
