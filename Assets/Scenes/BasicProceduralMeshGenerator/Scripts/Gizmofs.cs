using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Gizmofs
{
    public static void DrawWireCircle(Vector3 position, Quaternion rotation, float radius, int segments = 32)
    {
        Vector3[] points3D = new Vector3[segments];
        for (int i = 0; i < segments; i++)
        {
            float defination = (i / (float)segments);
            float angularRad = defination * Mathf.PI * 2;

            Vector2 point2D = Mathfs.GetUnitVectorByAngle(angularRad) * radius;
            points3D[i] = position + rotation * point2D;
        }
        for (int i = 0; i < segments - 1; i++)
        {
            Gizmos.DrawLine(points3D[i], points3D[i + 1]);
        }
        Gizmos.DrawLine(points3D[segments - 1], points3D[0]);
    }
}
