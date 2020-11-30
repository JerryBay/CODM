/** 
* 
*----------Dragon be here!----------/ 
* 　　　┏┓　　　┏┓ 
* 　　┏┛┻━━━┛┻┓ 
* 　　┃　　　　　　　┃ 
* 　　┃　　　━　　　┃ 
* 　　┃　┳┛　┗┳　┃ 
* 　　┃　　　　　　　┃ 
* 　　┃　　　┻　　　┃ 
* 　　┃　　　　　　　┃ 
* 　　┗━┓　　　┏━┛ 
* 　　　　┃　　　┃神兽保佑 
* 　　　　┃　　　┃代码无BUG！ 
* 　　　　┃　　　┗━━━┓ 
* 　　　　┃　　　　　　　┣┓ 
* 　　　　┃　　　　　　　┏┛ 
* 　　　　┗┓┓┏━┳┓┏┛ 
* 　　　　　┃┫┫　┃┫┫ 
* 　　　　　┗┻┛　┗┻┛ 
* ━━━━━━神兽出没━━━━━━
*-------------------------------------/
*    by:  Jerry
*-------------------------------------/
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using UnityEngine.Rendering;

public class Mesh2Text : MonoBehaviour
{
    public List<GameObject> _gos;

    private Mesh _mesh;

    private List<Vector3> _vertices = new List<Vector3>();

    private string _name;

    private string _path ;
    private void Awake()
    {
        //Vertices2Text();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            foreach (var item in _gos)
            {
                Init(item);
            }
        }
        if(Input.GetKeyDown(KeyCode.V))
        {
            Vertices2Text();
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
        _mesh.GetVertices(_vertices);
        _name = go.name;
        _path = $@"E:\Resources\{_name}.txt";


        int submeshCount = _mesh.subMeshCount;
        int count = 0;
        for (int i = 0; i < submeshCount; i++)
        {
            count += _mesh.GetSubMesh(i).vertexCount;
            Debug.Log(count);
        }
    }

    public void Vertices2Text()
    {
        foreach (var go in _gos)
        {
            Init(go);
            for (int i = 0; i < _vertices.Count; i++)
            {
                File.AppendAllText(_path, _vertices[i].x.ToString() + ",", Encoding.UTF8);
            }
            File.AppendAllText(_path, "\n");
            for (int i = 0; i < _vertices.Count; i++)
            {
                File.AppendAllText(_path, _vertices[i].y.ToString() + ",", Encoding.UTF8);
            }
            File.AppendAllText(_path, "\n");
            for (int i = 0; i < _vertices.Count; i++)
            {
                File.AppendAllText(_path, _vertices[i].z.ToString() + ",", Encoding.UTF8);
            }
        }
    }
}
