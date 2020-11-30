using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using UnityEngine;

public class RecreateObj : MonoBehaviour
{
    public List<GameObject> _gos;
    private Mesh _mesh;
    private string _path;
    private Vector3[] _vertices;
    private Vector3[] _normals;
    private Vector2[] _uv;
    int[] _triangles;

    private void Awake()
    {
        foreach (var item in _gos)
        {
            GameObject go = item;
            Init(item); 
            
        }

        //Debug.Log("_vertices"+_vertices.Length);
        //Debug.Log("_normals" + _normals.Length);
        //Debug.Log("_tangents" + _tangents.Length);
        //foreach (var item in _triangles)
        //{
        //    if(item==_vertices.Length|| item == _vertices.Length-1|| item == _vertices.Length-2)
        //    {
        //        Debug.Log(item);
        //    }
        //    else
        //    {
        //        Debug.Log("no");
        //    }
        //}
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            Init(_gos[0]);
            Create();
        }
    }

    private void Init(GameObject go)
    {
        if (go.GetComponent<SkinnedMeshRenderer>())
        {
            _mesh = go.GetComponent<SkinnedMeshRenderer>().sharedMesh;
        }
        else
        {
            _mesh = go.GetComponent<MeshFilter>().sharedMesh;
        }
        _path = $@"E:\Resources\{go.name}.obj";
        _vertices= _mesh.vertices;
        _normals = _mesh.normals;
        _triangles = _mesh.triangles;
        _uv = _mesh.uv;
    }

    private void Create()
    {
        for (int i = 0; i < _vertices.Length; i++)
        {
            File.AppendAllText(_path,$"v {_vertices[i].x} {_vertices[i].y} {_vertices[i].z}");
            File.AppendAllText(_path,"\n");
        }
        for (int i = 0; i < _uv.Length; i++)
        {
            File.AppendAllText(_path, $"vt {_uv[i].x} {_uv[i].y}");
            File.AppendAllText(_path, "\n");
        }
        for (int i = 0; i < _normals.Length; i++)
        {
            File.AppendAllText(_path, $"vn {_normals[i].x} {_normals[i].y} {_normals[i].z}");
            File.AppendAllText(_path, "\n");
        }
        for (int i = 0; i < _triangles.Length; i+=3)
        {
            int idx1 = _triangles[i], idx2 = _triangles[i+1], idx3 = _triangles[i+2];

            File.AppendAllText(_path, $"f {idx1+1}/{idx1+1}/{idx1+1} {idx2 + 1}/{idx2 + 1}/{idx2 + 1} {idx3 + 1}/{idx3 + 1}/{idx3 + 1}");
            File.AppendAllText(_path, "\n");
        }
    }
}
