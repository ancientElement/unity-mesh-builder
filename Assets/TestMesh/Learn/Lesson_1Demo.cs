using MeshBuilder;
using UnityEngine;

public class Lesson_1Demo : DemoBase
{
    [SerializeField] int xSegments, ySegments;
    [SerializeField] float xSize, ySize;

    protected override void Build(MeshFilter filter)
    {
        filter.mesh = Lesson_1Build.Build(xSegments, ySegments, xSize, ySize);
    }
}

