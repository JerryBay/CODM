using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class RecreateMesh : MonoBehaviour
{
    private string _path;
    private string _data;
    private List<Vector3> _vertices = new List<Vector3>();

    public GameObject _model;

    private void Awake()
    {
        StartCoroutine(LoadMesh());
        CalculateOffset();
    }

    private void CalculateOffset()
    {
        float maxY = 0, maxX = 0, minX = 0, maxZ = 0, minZ = 0;
        foreach (var vertex in _vertices)
        {
            maxY = Mathf.Max(maxY, vertex.y);
            maxX = Mathf.Max(maxX, vertex.x);
            minX = Mathf.Min(minX, vertex.x);
            maxZ = Mathf.Max(maxZ, vertex.z);
            minZ = Mathf.Min(minZ, vertex.z);
        }
        Debug.Log("maxY " + maxY);
        Debug.Log("maxX " + maxX);
        Debug.Log("minX " + minX);
        Debug.Log("maxZ " + maxZ);
        Debug.Log("minZ " + minZ);
    }

    private IEnumerator LoadMesh()
    {
        _path = $@"E:\Resources\{name}.txt";
        _data = File.ReadAllText(_path);


        string[] vertexs = _data.Split('\n');

        List<string[]> vertexsBranch = new List<string[]>();
        foreach (var item in vertexs)
        {
            vertexsBranch.Add(item.Split(','));
            //Debug.Log(vertexsBranch.Length);
        }


        int count = vertexsBranch[0].Length;
        for (int i = 0; i < count-1; i++)
        {
            Vector3 vertex = new Vector3(float.Parse(vertexsBranch[0][i]), float.Parse(vertexsBranch[2][i]), float.Parse(vertexsBranch[1][i]));
            vertex /= 100;
            _vertices.Add(vertex);

        }
        Debug.Log(count);

        if (!gameObject.GetComponent<SkinnedMeshRenderer>())
        {
            gameObject.AddComponent<SkinnedMeshRenderer>();
        }
        SkinnedMeshRenderer temp = gameObject.GetComponent<SkinnedMeshRenderer>();

        Mesh mesh = new Mesh();
        temp.sharedMesh = mesh;

        mesh.vertices = _vertices.ToArray();


        
        mesh.RecalculateNormals();
        mesh.RecalculateTangents();
        mesh.RecalculateBounds();

        mesh.triangles = _model.GetComponent<SkinnedMeshRenderer>().sharedMesh.triangles;
 

        yield break;
    }
}
