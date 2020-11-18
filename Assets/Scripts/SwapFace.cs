using System.Collections.Generic;
using UnityEngine;

public class SwapFace : MonoBehaviour
{
    public CharacterAssets _character;
    public GameObject _face;
    public AdaptEye _adaptEye;

    private Mesh _mesh;
    private Material _material;
    private List<Vector3> _vertices=new List<Vector3>();
    public TextAsset _indexFile;
    public TextAsset _lockFile;

    private void Awake()
    {
        Init();
    }
    private void OnDisable()
    {
        ResetVertices();
    }

    private void Init()
    {
        if (_face.GetComponent<SkinnedMeshRenderer>())
        {
            _mesh = _face.GetComponent<SkinnedMeshRenderer>().sharedMesh;
        }
        else
        {
            Debug.LogError("No SkinnedMeshRenderer");
        }
        _mesh.GetVertices(_vertices);
        List<Material> materials = new List<Material>();
        _face.GetComponent<SkinnedMeshRenderer>().GetMaterials(materials);
        _material = materials[0];
    }


    public void SwapVertices(PictureCombination pictComb)
    {
        string[] indexs= _indexFile.text.Split('\n');
        string[] bodyIndex = indexs[0].Split(',');
        string[] headIndex = indexs[1].Split(',');

        int count = bodyIndex.Length;

        string[] datas = pictComb._personalFile.text.Split('\n');
        string[] x = datas[0].Split(',');
        string[] y = datas[1].Split(',');
        string[] z = datas[2].Split(',');

        //string[] lockDatas = _lockFile.text.Split(',');

        // RecalculateVertices
        List<Vector3> vertices = new List<Vector3>();
        _mesh.GetVertices(vertices);
        for (int i = 0; i < count; i++)
        {
            int body = int.Parse(bodyIndex[i]);
            int head = int.Parse(headIndex[i]);
            Vector3 vertex = new Vector3(float.Parse(x[head]), float.Parse(y[head]), float.Parse(z[head]));
            vertex = OffsetAndScale.Correct(vertex);
            vertices[body] = vertex;
        }
        _adaptEye.Adapt(pictComb._personalFile, ref vertices);
        _mesh.vertices = vertices.ToArray();
        _mesh.RecalculateNormals();

        SwapTexture(pictComb);
    }

    private void SwapTexture(PictureCombination pictComb)
    {
        if (pictComb._material!=null)
        {
            _face.GetComponent<SkinnedMeshRenderer>().materials[0].CopyPropertiesFromMaterial(pictComb._material) ;
        }
        else
        {
            _face.GetComponent<SkinnedMeshRenderer>().materials[0]=_material;
        }
    }

    public void ResetVertices()
    {
        _mesh.SetVertices(_vertices);
        _mesh.RecalculateNormals();

        _face.GetComponent<SkinnedMeshRenderer>().materials[0]=_material;
    }
}
