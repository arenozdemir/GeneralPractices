using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Mathfs
{
    public static Vector2 GetUnitVectorByAngle(float radiantAngle)
    {
        return new Vector2(Mathf.Sin(radiantAngle), Mathf.Cos(radiantAngle));
    }
}
