using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using UnityEngine;

public class HeadObj : MonoBehaviour
{
    public List<GameObject> _gos;
    public int _indexObj;
    private Mesh _mesh;
    private string _path;
    private Vector3[] _vertices;
    private Vector3[] _normals;
    private Vector2[] _uv;
    private int[] _triangles;
    private int leftBound = 0;
    private int rightBound = 0;

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
        leftBound = rightBound = 0;
        if (go.GetComponent<SkinnedMeshRenderer>())
        {
            _mesh = go.GetComponent<SkinnedMeshRenderer>().sharedMesh;
        }
        else
        {
            _mesh = go.GetComponent<MeshFilter>().sharedMesh;
        }
        _path = $@"E:\Resources\COD\OutPut\{go.name} {_indexObj}.obj";
        int submeshCount = _mesh.subMeshCount;
        for (int i = 0; i < _indexObj; i++)
        {
            leftBound += _mesh.GetSubMesh(i).vertexCount;
            rightBound += _mesh.GetSubMesh(i).vertexCount;
        }
        rightBound += _mesh.GetSubMesh(_indexObj).vertexCount;

        _vertices = _mesh.vertices;
        _normals = _mesh.normals;
        _triangles = _mesh.GetTriangles(_indexObj);
        _uv = _mesh.uv;

        Debug.Log(leftBound);
        Debug.Log(rightBound);
        Debug.Log(_triangles.Length);
    }

    private void Create()
    {
        for (int i = leftBound; i < rightBound; i++)
        {
            File.AppendAllText(_path, $"v {_vertices[i].x} {_vertices[i].y} {_vertices[i].z}");
            File.AppendAllText(_path, "\n");
        }
        for (int i = leftBound; i < rightBound; i++)
        {
            File.AppendAllText(_path, $"vt {_uv[i].x} {_uv[i].y}");
            File.AppendAllText(_path, "\n");
        }
        for (int i = leftBound; i < rightBound; i++)
        {
            File.AppendAllText(_path, $"vn {_normals[i].x} {_normals[i].y} {_normals[i].z}");
            File.AppendAllText(_path, "\n");
        }
        for (int i = 0; i < _triangles.Length; i += 3)
        {
            int offset = leftBound;
            int idx1 = _triangles[i] - offset, idx2 = _triangles[i + 1] - offset, idx3 = _triangles[i + 2] - offset;
            //if (idx1<=0|| idx2 <= 0 || idx3 <= 0)
            //{
            //    continue;
            //}
            File.AppendAllText(_path, $"f {idx1 + 1}/{idx1 + 1}/{idx1 + 1} {idx2 + 1}/{idx2 + 1}/{idx2 + 1} {idx3 + 1}/{idx3 + 1}/{idx3 + 1}");
            File.AppendAllText(_path, "\n");
        }
    }
}
