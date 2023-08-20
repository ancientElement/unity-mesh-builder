using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
public class RingLike : MonoBehaviour
{
    [SerializeField] private MeshFilter meshFilter;

    [SerializeField] private List<int> triangles;
    [SerializeField] private List<Vector3> vertices;

    [SerializeField] float innerRadius;
    [SerializeField] float outerRadius;
    [SerializeField] int thetaSegments;
    [SerializeField] int radiusSegments;
    [SerializeField] float thetaStart = 0f;
    [SerializeField] float thetaLength = Mathf.PI * 2f;
    [SerializeField] float height = 0.5f;

    //左侧
    [SerializeField] List<Vector3> asideLeft = new List<Vector3>();
    //右侧
    [SerializeField] List<Vector3> asideRight = new List<Vector3>();
    //前侧
    [SerializeField] List<Vector3> asideForward = new List<Vector3>();
    //后侧
    [SerializeField] List<Vector3> asideBack = new List<Vector3>();

    //左侧
    [SerializeField] List<int> asideLeftIndex = new List<int>();
    //右侧
    [SerializeField] List<int> asideRightIndex = new List<int>();
    //前侧
    [SerializeField] List<int> asideForwardIndex = new List<int>();
    //后侧
    [SerializeField] List<int> asideBackIndex = new List<int>();

    private void Start()
    {
        StartCoroutine(GenerateVertical());
    }

    private IEnumerator GenerateVertical()
    {
        thetaSegments = Mathf.Max(3, thetaSegments);
        radiusSegments = Mathf.Max(1, radiusSegments);

        var radiusStep = ((outerRadius - innerRadius) / radiusSegments);
        var radius = innerRadius;


        //上面顶点
        for (int j = 0; j <= radiusSegments; j++)
        {
            for (int i = 0; i <= thetaSegments; i++)
            {
                float startTheta = thetaStart + 1f * i / thetaSegments * thetaLength;
                Vector3 vertex = new Vector3(
                    radius * Mathf.Cos(startTheta),
                     0f,
                    radius * Mathf.Sin(startTheta)
                );
                vertices.Add(vertex);
                //左侧
                if (i == thetaSegments)
                {
                    asideLeftIndex.Add(vertices.Count - 1);
                }
                //右侧
                if (i == 0)
                {
                    asideRightIndex.Add(vertices.Count - 1);
                }
                //前侧
                if (j == radiusSegments)
                {
                    asideForwardIndex.Add(vertices.Count - 1);
                }
                //后侧
                if (j == 0)
                {
                    asideBackIndex.Add(vertices.Count - 1);
                }
                yield return new WaitForSeconds(0.1f);
            }
            radius += radiusStep;
        }

        //上面三角形
        for (int j = 0; j < radiusSegments; j++)
        {
            var thetaSegmentLevel = j * (thetaSegments + 1);
            for (int i = 0; i < thetaSegments; i++)
            {
                var segment = i + thetaSegmentLevel;
                var a = segment;
                var b = segment + thetaSegments + 1;
                var c = segment + thetaSegments + 2;
                var d = segment + 1;
                triangles.Add(a); triangles.Add(d); triangles.Add(b);
                triangles.Add(b); triangles.Add(d); triangles.Add(c);
            }
        }

        radiusStep = ((outerRadius - innerRadius) / radiusSegments);
        radius = innerRadius;

        //下面顶点
        for (int j = 0; j <= radiusSegments; j++)
        {
            for (int i = 0; i <= thetaSegments; i++)
            {
                float startTheta = thetaStart + 1f * i / thetaSegments * thetaLength;
                Vector3 vertex = new Vector3(
                    radius * Mathf.Cos(startTheta),
                     -height,
                    radius * Mathf.Sin(startTheta)
                );
                vertices.Add(vertex);
                //左侧
                if (i == thetaSegments)
                {
                    asideLeftIndex.Add(vertices.Count - 1);
                }
                //右侧
                if (i == 0)
                {
                    asideRightIndex.Add(vertices.Count - 1);
                }
                //前侧
                if (j == radiusSegments)
                {
                    asideForwardIndex.Add(vertices.Count - 1);
                }
                //后侧
                if (j == 0)
                {
                    asideBackIndex.Add(vertices.Count - 1);
                }
                yield return new WaitForSeconds(0.1f);
            }
            radius += radiusStep;
        }

        //下面三角形
        for (int j = radiusSegments + 1; j <= radiusSegments * 2; j++)
        {
            var thetaSegmentLevel = j * (thetaSegments + 1);
            for (int i = 0; i < thetaSegments; i++)
            {
                var segment = i + thetaSegmentLevel;
                var a = segment;
                var b = segment + thetaSegments + 1;
                var c = segment + thetaSegments + 2;
                var d = segment + 1;
                triangles.Add(a); triangles.Add(b); triangles.Add(d);
                triangles.Add(b); triangles.Add(c); triangles.Add(d);
            }
        }

        //右侧三角形
        for (int i = 0; i < radiusSegments; i++)
        {
            var a = asideRightIndex[i];
            var b = asideRightIndex[i + radiusSegments + 1];
            var c = asideRightIndex[i + radiusSegments + 2];
            var d = asideRightIndex[i + 1];
            triangles.Add(a); triangles.Add(d); triangles.Add(b);
            triangles.Add(b); triangles.Add(d); triangles.Add(c);
        }

        //左侧
        for (int i = 0; i < radiusSegments; i++)
        {
            var a = asideLeftIndex[i];
            var b = asideLeftIndex[i + radiusSegments + 1];
            var c = asideLeftIndex[i + radiusSegments + 2];
            var d = asideLeftIndex[i + 1];
            triangles.Add(a); triangles.Add(b); triangles.Add(d);
            triangles.Add(b);  triangles.Add(c); triangles.Add(d);
        }

        //前侧
        for (int i = 0; i < thetaSegments; i++)
        {
            var a = asideForwardIndex[i];
            var b = asideForwardIndex[i + thetaSegments + 1];
            var c = asideForwardIndex[i + thetaSegments + 2];
            var d = asideForwardIndex[i + 1];
            triangles.Add(a); triangles.Add(d); triangles.Add(b);
            triangles.Add(b); triangles.Add(d); triangles.Add(c);
        }

        //后侧
        for (int i = 0; i < thetaSegments; i++)
        {
            var a = asideBackIndex[i];
            var b = asideBackIndex[i + thetaSegments + 1];
            var c = asideBackIndex[i + thetaSegments + 2];
            var d = asideBackIndex[i + 1];
            triangles.Add(a); triangles.Add(b); triangles.Add(d);
            triangles.Add(b); triangles.Add(c); triangles.Add(d);
        }

        var mesh = new Mesh();
        mesh.SetVertices(vertices);
        mesh.SetIndices(triangles, MeshTopology.Triangles, 0);
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        meshFilter.mesh = mesh;
    }

    private void OnDrawGizmos()
    {
        if (vertices == null || vertices.Count == 0) return;
        Gizmos.color = Color.black;
        foreach (Vector3 item in vertices)
        {
            Gizmos.DrawSphere(item, 0.05f);
        }
        Gizmos.color = Color.white;
        foreach (Vector3 item in asideLeft)
        {
            Gizmos.DrawCube(item, new Vector3(0.1f, 0.05f, 0.05f));
        }
        Gizmos.color = Color.green;
        foreach (Vector3 item in asideForward)
        {
            Gizmos.DrawCube(item, new Vector3(0.05f, 0.1f, 0.05f));
        }
        Gizmos.color = Color.red;
        foreach (Vector3 item in asideRight)
        {
            Gizmos.DrawCube(item, new Vector3(0.05f, 0.1f, 0.05f));
        }
        Gizmos.color = Color.blue;
        foreach (Vector3 item in asideBack)
        {
            Gizmos.DrawCube(item, new Vector3(0.05f, 0.05f, 0.05f));
        }
    }
}
