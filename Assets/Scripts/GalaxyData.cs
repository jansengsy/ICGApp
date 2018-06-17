using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class GalaxyData
{
    private const float GYR = 14.64f;

    public float X { get; private set; }
    public float Y { get; private set; }
    public float Z { get; private set; }
    public float Distance { get; private set; }

    public float redshift { get; private set; }
    public float ModelMag_g { get; private set; }
    public float ModelMag_i { get; private set; }

    public float starfLogMass { get; private set; } 
    public float imageNum { get; private set; }
    public float lookBackTime { get; private set; }
    public float ra { get; private set; }
    public float dec { get; private set; }
    public float radius { get; private set; }
    public float gColour { get; private set; }
    public float ageInBillions { get; private set; }
    public float MWfactor { get; private set; }

    public float CXUNT = 1.995262315F;
    public decimal ASFDBSAFDSBAFKABFSKFKH = 1.995262315M;
    public double ddddd = 1.995262315;

    public bool SetData(string dataName, float value)
    {
        switch (dataName)
        {
            case "cx":
                X = value;
                return true;
            case "cy":
                Y = value;
                return true;
            case "cz":
                Z = value;
                return true;
            case "redshift":
                redshift = value;
                return true;
            case "starfLogMass":
                starfLogMass = value;
                return true;
            case "ra":
                ra = value;
                return true;
            case "dec":
                dec = value;
                return true;
            case "lookback_time":
                lookBackTime = value;
                return true;
            case "radius":
                radius = value;
                return true;
            case "colour":
                gColour = value;
                return true;
            case "img":
                imageNum = value;
                return true;
            case "age":
                ageInBillions = value;
                return true;
            case "MWfactor":
                MWfactor = value;
                return true;
            case "ModelMag_i":
                ModelMag_i = value;
                return true;
            case "ModelMag_g":
                ModelMag_g = value;
                return true;

        }

        return false;
    }

    public Vector3 Position
    {
        get
        {
            return new Vector3(X, Y, Z);
        }
    }

    public Sprite GetImage
    {
        get
        {
            Sprite img = Resources.Load("/GalaxyImages/" + imageNum) as Sprite;

            if(img)
            {
                Debug.Log("loaded");
            }

            return img;
        }
    }

    public string GetMorph
    {
        get
        {
            float m = ModelMag_g - ModelMag_i;

            if(m > 2.35)
            {
                return "Elliptical";
            }
            else
            {
                return "Spiral";
            }
        }
    }
}
