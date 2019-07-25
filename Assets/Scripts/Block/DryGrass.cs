using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DryGrass : Block 
{
    public override string Name
    {
        get
        {
            return "Dry Grass";
        }
    }

    public override int BlockID
    {
        get
        {
            return 8;
        }
    }

    public override int Submesh
    {
        get
        {
            return 1;
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
            return Face.GetFromIndex(3, 0, overlayX: 6, overlayY: 3, 
                color: new Color(165f / 255f, 168f / 255f, 88f / 255f));
        }
    }

    public override Face BackFaceUV
    {
        get
        {
            return Face.GetFromIndex(3, 0, overlayX: 6, overlayY: 3, 
                color: new Color(165f / 255f, 168f / 255f, 88f / 255f));
        }
    }

    public override Face TopFaceUV
    {
        get
        {
            return Face.GetFromIndex(0, 0, new Color(165f / 255f, 168f/ 255f, 88f/ 255f));
        }
    }

    public override Face BottomFaceUV
    {
        get
        {
            return Face.GetFromIndex(2, 0);
        }
    }

    public override Face LeftFaceUV
    {
        get
        {
            return Face.GetFromIndex(3, 0, overlayX: 6, overlayY: 3, 
                color: new Color(165f / 255f, 168f / 255f, 88f / 255f));
        }
    }

    public override Face RightFaceUV
    {
        get
        {
            return Face.GetFromIndex(3, 0, overlayX: 6, overlayY: 3, 
                color: new Color(165f / 255f, 168f / 255f, 88f / 255f));
        }
    }
}
