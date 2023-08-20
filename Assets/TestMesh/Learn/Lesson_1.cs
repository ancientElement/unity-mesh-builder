using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson_1 : MonoBehaviour
{
    [SerializeField] int xSegments, ySegments;
    [SerializeField] float xSize, ySize;
    [SerializeField] private MeshFilter meshFilter;
    [SerializeField] private int[] triangles;
    [SerializeField] private Vector3[] vertices;

    private void Start()
    {
        StartCoroutine(GenerateVertical());
    }

    private IEnumerator GenerateVertical()
    {
        int veritcalLength = (xSegments + 1) * (ySegments + 1);
        vertices = new Vector3[veritcalLength];

        //顶点
        int xIndex;
        int yIndex;
        float singleQuadWidth = (float)xSize / xSegments;
        float singleQuadHeight = (float)ySize / ySegments;
        for (int i = 0; i < veritcalLength; i++)
        {
            yIndex = i / (xSegments + 1);
            xIndex = i % (xSegments + 1);
            vertices[i] = new Vector3(xIndex * singleQuadWidth, yIndex * singleQuadHeight);
            yield return new WaitForSeconds(0.1f);
        }

        //三角形
        triangles = new int[xSegments * ySegments * 6];
        //遍历所有面
        for (int quadIndex = 0; quadIndex < xSegments * ySegments; quadIndex++)
        {
            int trianglesIndex = quadIndex * 6;
            yIndex = quadIndex / xSegments;
            xIndex = quadIndex % xSegments;
            int vertexIndex = yIndex * (xSegments + 1) + xIndex;
            triangles[trianglesIndex] = vertexIndex;
            triangles[trianglesIndex + 1] = triangles[trianglesIndex] + xSegments + 1;
            triangles[trianglesIndex + 2] = vertexIndex + 1;
            triangles[trianglesIndex + 3] = triangles[trianglesIndex + 2];
            triangles[trianglesIndex + 4] = triangles[trianglesIndex + 1];
            triangles[trianglesIndex + 5] = triangles[trianglesIndex + 3] + xSegments + 1;
        }

        Mesh newMesh = new Mesh();
        newMesh.vertices = vertices;
        newMesh.triangles = triangles;
        newMesh.RecalculateNormals();
        meshFilter.mesh = newMesh;
    }

    private void OnDrawGizmos()
    {
        if (vertices == null || vertices.Length == 0) return;
        Gizmos.color = Color.black;
        foreach (Vector3 item in vertices)
        {
            Gizmos.DrawSphere(item, 0.1f);
        }
    }
}
