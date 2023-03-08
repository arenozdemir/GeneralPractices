using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{
    [SerializeField]
    private Transform pointPrefab;
    
    [Range(10, 100)]
    [SerializeField] int resolution = 10;

    [SerializeField, Range(0, 2)]
    int function;
    
    Transform[] points;
    private Vector3 position;
    private void Awake()
    {
        float step = 2f / resolution;
        var position = Vector3.zero;
        var scale = Vector3.one * step;
        points = new Transform[resolution];
        for (int i = 0; i < points.Length; i++)
        {
            Transform point = points[i] = Instantiate(pointPrefab);
            position.x = (i + 0.5f) * step - 1f;
            point.localPosition = position;
            point.localScale = scale;
            point.SetParent(transform, false);
        }
    }
    private void Update()
    {
        float time = Time.time;
        for (int i = 0; i < points.Length; i++)
        {
            Transform point = points[i];
            Vector3 position = point.localPosition;
            if (function == 0) position.y = FunctionLibrary.Wave(position.x, time);
            else if (function == 1) position.y = FunctionLibrary.MultiWave(position.x, time);
            else if (function == 2) position.y = FunctionLibrary.Ripple(position.x, time);
            point.localPosition = position;
        }
    }
}
