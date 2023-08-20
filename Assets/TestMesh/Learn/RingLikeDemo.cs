using MeshBuilder;
using UnityEngine;

public class RingLikeDemo : DemoBase
{

    [SerializeField, Range(0f, 50f)] float innerRadius;
    [SerializeField, Range(0.01f, 50f)] float outerRadius;
    [SerializeField, Range(3, 25)] int thetaSegments;
    [SerializeField, Range(1, 25)] int radiusSegments;
    [SerializeField] float thetaStart = 0f;
    [SerializeField] float thetaLength = Mathf.PI * 2f;
    [SerializeField] float height = 0.5f;

    protected override void Build(MeshFilter filter)
    {
        filter.mesh = RingLikeBuilder.Build(innerRadius, outerRadius, thetaSegments, radiusSegments, thetaStart, thetaLength, height);
    }
}

