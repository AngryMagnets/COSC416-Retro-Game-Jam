using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(PolygonCollider2D))]
public class MeshBuilder : MonoBehaviour
{
    [SerializeField] Vector2[] cornerPoints = new Vector2[2];
    [SerializeField] float bezierIntensity = 0.5f;
    [SerializeField] Vector3 bezierSqueeze; //x squeezes bottom half, 1-y sets width
    [SerializeField] int bezierVertexCount = 10;

    [SerializeField] bool generateCollider = false;

    void OnEnable()
    {

        var mesh = new Mesh
        {
            name = "Procedural Mesh"
        };

        Vector3[] vertices = new Vector3[bezierVertexCount * 2];
        Vector2[] vertices2D = new Vector2[bezierVertexCount * 2];
        Vector2 P0 = cornerPoints[0], P2 = cornerPoints[1], P1 = Vector2.Lerp(P2,P0,0.5f) + (Vector2.Perpendicular(P2-P0)*bezierIntensity);
        for (int i = 0; i<bezierVertexCount; i++)
        {
            float t = (float)i / ((float)bezierVertexCount - 1);
            vertices[i] = ((1-t)*(1-t))*P0 + (2*(1-t)*t)*P1 + (t*t)*P2;
            //vertices[i + bezierVertexCount] = vertices[i];
            //vertices[i + bezierVertexCount].Scale(bezierSqueeze);
            vertices2D[i] = vertices[i];
            //vertices2D[bezierVertexCount * 2 - i - 1] = vertices[i + bezierVertexCount];
        }

        P0.Scale(bezierSqueeze); P2.Scale(bezierSqueeze); P1 = Vector2.Lerp(P2, P0, 0.5f) + (Vector2.Perpendicular(P2 - P0) * bezierIntensity);
        for (int i = 0; i < bezierVertexCount; i++)
        {
            float t = (float)i / ((float)bezierVertexCount - 1);
            vertices[i+ bezierVertexCount] = ((1 - t) * (1 - t)) * P0 + (2 * (1 - t) * t) * P1 + (t * t) * P2;
            vertices2D[bezierVertexCount * 2 - i - 1] = vertices[i + bezierVertexCount];
        }
        mesh.vertices = vertices;

        int[] indices = new int[(bezierVertexCount-1)*6];
        for (int i = 0; i<bezierVertexCount-1; i++)
        {
            indices[i * 6] = i;
            indices[i * 6 + 1] = i+1;
            indices[i * 6 + 2] = i+bezierVertexCount;

            indices[i * 6 + 3] = i+1;
            indices[i * 6 + 4] = i + 1 + bezierVertexCount;
            indices[i * 6 + 5] = i + bezierVertexCount;
        }
        mesh.triangles = indices;

        Vector3[] normals = new Vector3[bezierVertexCount*2];
        for (int i = 0; i<normals.Length; i++)
        {
            normals[i] = Vector3.back;
        }
        mesh.normals = normals;

        Vector2[] uvs = new Vector2[bezierVertexCount * 2];
        for (int i = 0; i<bezierVertexCount; i++)
        {
            float t = (float)i / ((float)bezierVertexCount - 1);
            uvs[i] = Vector2.Lerp(Vector2.up,Vector2.up+Vector2.right, t);
            uvs[i+bezierVertexCount] = Vector2.Lerp(Vector2.zero, Vector2.right, t);
        }
        mesh.uv = uvs;

        if (generateCollider) GetComponent<PolygonCollider2D>().SetPath(0, vertices2D);
        GetComponent<MeshFilter>().mesh = mesh;
    }
}
