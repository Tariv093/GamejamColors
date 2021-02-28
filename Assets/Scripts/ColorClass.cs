using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ColorClass
{

    public enum color
    {
        Orange,
        Blue,
        Purple
    }
    public color colorObject;

    public Material mat;
    public color GetColor()
    {
        return colorObject;
    }
    public void SetColor(color c)
    {
        c = colorObject;
    }


}
