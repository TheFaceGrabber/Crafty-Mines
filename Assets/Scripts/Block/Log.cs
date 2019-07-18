using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : Block 
{
    public override string Name
    {
        get
        {
            return "Log";
        }
    }

    public override int BlockID
    {
        get
        {
            return 5;
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
            return Face.GetFromIndex(4, 1);
        }
    }

    public override Face BackFaceUV
    {
        get
        {
            return Face.GetFromIndex(4, 1);
        }
    }

    public override Face TopFaceUV
    {
        get
        {
            return Face.GetFromIndex(5, 1);
        }
    }

    public override Face BottomFaceUV
    {
        get
        {
            return Face.GetFromIndex(5, 1);
        }
    }

    public override Face LeftFaceUV
    {
        get
        {
            return Face.GetFromIndex(4, 1);
        }
    }

    public override Face RightFaceUV
    {
        get
        {
            return Face.GetFromIndex(4, 1);
        }
    }
}
