using MeshBuilder;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
public class Lesson_1Build : MeshBuilderBase
{
    [SerializeField] private int xSegments, ySegments;
    [SerializeField] private float xSize, ySize;

    public static Mesh Build(int xSegments, int ySegments, float xSize, float ySize)
    {
        int veritcalLength = (xSegments + 1) * (ySegments + 1);
        Vector3[] vertices = new Vector3[veritcalLength];

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
        }

        //三角形
        int[] triangles = new int[xSegments * ySegments * 6];
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

        return newMesh;
    }

  
}