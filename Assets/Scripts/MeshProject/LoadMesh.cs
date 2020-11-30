using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine.Networking;
using System;

public class LoadMesh : MonoBehaviour
{
    public GameObject Loadobj;


    private Action<GameObject> creatMeshBack;
    Mesh _myMesh;

    Vector3[] _vertexArray;
    ArrayList _vertexArrayList = new ArrayList();
    Vector3[] _normalArray;
    ArrayList _normalArrayList = new ArrayList();
    Vector2[] _uvArray;
    ArrayList _uvArrayList = new ArrayList();

    int[] _triangleArray;
    ArrayList _facesVertNormUV = new ArrayList();

    private void Start()
    {
        StartCoroutine(SomeFunction(Application.streamingAssetsPath + "/arrow.obj",
            Application.streamingAssetsPath + "/arrow.tga",
            _CreatMeshBack));
    }
    /// <summary>
    /// 回调函数
    /// </summary>
    /// <param name="obj">加载到的obj在场景中显示的物体</param>
    private void _CreatMeshBack(GameObject obj)
    {
        Loadobj = obj;
    }

    internal class PlacesByIndex
    {
        public PlacesByIndex(int index)
        {
            _index = index;
        }
        public int _index;
        public ArrayList _places = new ArrayList();
    }
    void initArrayLists()
    {
        _uvArrayList = new ArrayList();
        _normalArrayList = new ArrayList();
        _vertexArrayList = new ArrayList();
        _facesVertNormUV = new ArrayList();
    }
    /// <summary>
    /// 加载obj
    /// </summary>
    /// <param name="ObjPath">mesh</param>
    /// <param name="_texturepath">texture</param>
    /// <returns></returns>
    public IEnumerator SomeFunction(string ObjPath, string _texturepath, Action<GameObject> act)
    {
        if (File.Exists(ObjPath) && File.Exists(_texturepath))
        {
            creatMeshBack = act;
            GameObject obj_gameobject = new GameObject();
            obj_gameobject.name = "objload";

            initArrayLists();
            if (_myMesh != null)
                _myMesh.Clear();
            _myMesh = new Mesh();
            _myMesh.name = "objload";

            Debug.Log(ObjPath);

            //更具地址加载模型
            UnityWebRequest uwr = UnityWebRequest.Get("file://" + ObjPath);
            yield return uwr.SendWebRequest();

            if (uwr.isDone && string.IsNullOrEmpty(uwr.error))
            {
                string s = uwr.downloadHandler.text;

                s = s.Replace("  ", " ");
                s = s.Replace("  ", " ");
                LoadFile(s);
            }
            else
            {
                Debug.LogError(uwr.error);
                Debug.LogError(uwr.downloadHandler.text);
            }

            _myMesh.vertices = _vertexArray;
            _myMesh.triangles = _triangleArray;
            if (_uvArrayList.Count > 0)
                _myMesh.uv = _uvArray;
            if (_normalArrayList.Count > 0)
                _myMesh.normals = _normalArray;
            else
                _myMesh.RecalculateNormals();
            _myMesh.RecalculateBounds();
            if ((MeshFilter)obj_gameobject.GetComponent("MeshFilter") == null)
                obj_gameobject.AddComponent<MeshFilter>();
            MeshFilter temp;
            temp = (MeshFilter)obj_gameobject.GetComponent("MeshFilter");
            temp.mesh = _myMesh;

            var material = new Material(Shader.Find("Diffuse"));

            if ((MeshRenderer)obj_gameobject.GetComponent("MeshRenderer") == null)
                obj_gameobject.AddComponent<MeshRenderer>();
            if (_uvArrayList.Count > 0 && _texturepath != "")
            {
                //加载贴图
                UnityWebRequest wwwtx = UnityWebRequestTexture.GetTexture("file://" + _texturepath);
                yield return wwwtx.SendWebRequest();

                if (wwwtx.isDone && string.IsNullOrEmpty(wwwtx.error))
                {
                    Debug.Log(" @ !the tex has load");
                    material.mainTexture = ((DownloadHandlerTexture)wwwtx.downloadHandler).texture;
                }
                else
                {
                    Debug.LogError(" @ ! load file  texture  +" + _texturepath + " error : " + wwwtx.error);
                }
            }
            MeshRenderer temp2;
            temp2 = (MeshRenderer)obj_gameobject.GetComponent("MeshRenderer");
            if (_uvArrayList.Count > 0 && _texturepath != "")
            {
                temp2.material = material;
            }
            yield return new WaitForFixedUpdate();

            creatMeshBack(obj_gameobject);
        }
        else
        {
            creatMeshBack(null);
        }
    }

    public void LoadFile(string s)
    {
        string[] lines = s.Split("\n"[0]);

        foreach (string item in lines)
        {
            ReadLine(item);
        }
        ArrayList tempArrayList = new ArrayList();
        for (int i = 0; i < _facesVertNormUV.Count; ++i)
        {
            if (_facesVertNormUV[i] != null)
            {
                PlacesByIndex indextemp = new PlacesByIndex(i);
                indextemp._places.Add(i);
                for (int j = 0; j < _facesVertNormUV.Count; ++j)
                {
                    if (_facesVertNormUV[j] != null)
                    {
                        if (i != j)
                        {
                            Vector3 iTemp = (Vector3)_facesVertNormUV[i];
                            Vector3 jTemp = (Vector3)_facesVertNormUV[j];
                            if (iTemp.x == jTemp.x && iTemp.y == jTemp.y)
                            {
                                indextemp._places.Add(j);
                                _facesVertNormUV[j] = null;
                            }
                        }
                    }
                }
                tempArrayList.Add(indextemp);
            }
        }
        _vertexArray = new Vector3[tempArrayList.Count];
        _uvArray = new Vector2[tempArrayList.Count];
        _normalArray = new Vector3[tempArrayList.Count];
        _triangleArray = new int[_facesVertNormUV.Count];
        int teller = 0;
        foreach (PlacesByIndex item in tempArrayList)
        {
            foreach (int item2 in item._places)
            {
                _triangleArray[item2] = teller;
            }
            Vector3 vTemp = (Vector3)_facesVertNormUV[item._index];
            _vertexArray[teller] = (Vector3)_vertexArrayList[(int)vTemp.x - 1];
            if (_uvArrayList.Count > 0)
            {
                Vector3 tVec = (Vector3)_uvArrayList[(int)vTemp.y - 1];
                _uvArray[teller] = new Vector2(tVec.x, tVec.y);
            }
            if (_normalArrayList.Count > 0)
            {
                _normalArray[teller] = (Vector3)_normalArrayList[(int)vTemp.z - 1];
            }
            teller++;
        }
    }

    public void ReadLine(string s)
    {
        char[] charsToTrim = { ' ', '\n', '\t', '\r' };
        s = s.TrimEnd(charsToTrim);
        string[] words = s.Split(" "[0]);
        foreach (string item in words)
            item.Trim();
        if (words[0] == "v")
            _vertexArrayList.Add(new Vector3(System.Convert.ToSingle(words[1], CultureInfo.InvariantCulture), System.Convert.ToSingle(words[2], CultureInfo.InvariantCulture), System.Convert.ToSingle(words[3], CultureInfo.InvariantCulture)));

        if (words[0] == "vn")
            _normalArrayList.Add(new Vector3(System.Convert.ToSingle(words[1], CultureInfo.InvariantCulture), System.Convert.ToSingle(words[2], CultureInfo.InvariantCulture), System.Convert.ToSingle(words[3], CultureInfo.InvariantCulture)));
        if (words[0] == "vt")
            _uvArrayList.Add(new Vector3(System.Convert.ToSingle(words[1], CultureInfo.InvariantCulture), System.Convert.ToSingle(words[2], CultureInfo.InvariantCulture)));
        if (words[0] == "f")
        {
            ArrayList temp = new ArrayList();
            ArrayList triangleList = new ArrayList();
            for (int j = 1; j < words.Length; ++j)
            {
                Vector3 indexVector = new Vector3(0, 0);
                string[] indices = words[j].Split("/"[0]);
                indexVector.x = System.Convert.ToInt32(indices[0], CultureInfo.InvariantCulture);
                if (indices.Length > 1)
                {
                    if (indices[1] != "")
                        indexVector.y = System.Convert.ToInt32(indices[1], CultureInfo.InvariantCulture);
                }
                if (indices.Length > 2)
                {
                    if (indices[2] != "")
                        indexVector.z = System.Convert.ToInt32(indices[2], CultureInfo.InvariantCulture);
                }
                temp.Add(indexVector);
            }
            for (int i = 1; i < temp.Count - 1; ++i)
            {
                triangleList.Add(temp[0]);
                triangleList.Add(temp[i]);
                triangleList.Add(temp[i + 1]);
            }

            foreach (Vector3 item in triangleList)
            {
                _facesVertNormUV.Add(item);
            }
        }
    }
}