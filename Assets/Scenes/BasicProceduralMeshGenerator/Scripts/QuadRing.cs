using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(MeshFilter))]
public class QuadRing : MonoBehaviour
{
    //float radiousOutter => radiusInner + thickness;
    [Range(0.01f, 2)]
    [SerializeField] float radiusInner;

    [Range(0.01f, 2)]
    [SerializeField] float thickness;
    float radiousOutter { get { return radiusInner + thickness; } }
    
    [Range(3,256)]
    [SerializeField] int angularSegmentCount = 3;
    Mesh mesh;
    int vertexCount => angularSegmentCount * 2;
    private void OnDrawGizmosSelected()
    {
        Gizmofs.DrawWireCircle(transform.position, transform.rotation, radiusInner, angularSegmentCount);
        Gizmofs.DrawWireCircle(transform.position, transform.rotation, radiousOutter, angularSegmentCount);
    }
    private void Awake()
    {
        mesh = new Mesh();
        mesh.name = "QuadRing";
        GetComponent<MeshFilter>().sharedMesh = mesh;
    }
    private void Update()
    {
        GenerateMash();
    }
    private void GenerateMash()
    {
        mesh.Clear();
        int vCount = vertexCount;
        List<Vector3> vertices = new List<Vector3>(vCount);
        List<Vector3> normals = new List<Vector3>(vCount);
        for (int i = 0; i < angularSegmentCount; i++)
        {
            float defination = i / (float)angularSegmentCount;
            float angularRad = defination * Mathf.PI * 2;

            Vector2 direction = Mathfs.GetUnitVectorByAngle(angularRad);
            vertices.Add(direction * radiousOutter);
            vertices.Add(direction * radiusInner);
            normals.Add(Vector3.forward);
            normals.Add(Vector3.forward);
        }
        List<int> triangleIndices = new List<int>();
        for (int i = 0; i < angularSegmentCount; i++)
        {
            int indexRoot = i * 2;
            int indexInnerRoot = indexRoot + 1;
            int indexOuterNext = (indexRoot + 2) % vCount;
            int indexInnerNext = (indexRoot + 3) % vCount;

            triangleIndices.Add(indexRoot);
            triangleIndices.Add(indexOuterNext);
            triangleIndices.Add(indexInnerNext);

            triangleIndices.Add(indexRoot);
            triangleIndices.Add(indexInnerNext);
            triangleIndices.Add(indexInnerRoot);
        }
        mesh.SetVertices(vertices);
        mesh.SetTriangles(triangleIndices, 0);
        //mesh.RecalculateNormals();
        mesh.SetNormals(normals);
    }
}
