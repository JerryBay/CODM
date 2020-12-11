using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SwapFace : MonoBehaviour
{
    public CharacterAssets _character;
    public GameObject _face;
    public AdaptEye _adaptEye;

    private Mesh _mesh;
    //public Material _material;
    public Texture _texture;
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
    }


    public void SwapVertices(PictureCombination pictComb)
    {
        string[] indexs= _indexFile.text.Split('\n');
        string[] bodyIndex = indexs[0].Split(',');
        string[] personalIndex = indexs[1].Split(',');

        int count = bodyIndex.Length;

        string[] datas = pictComb._personalFile.text.Split('\n');

        string[] headX;
        string[] headY;
        string[] headZ;
        switch (pictComb._flag)
        {
            case 0:
                headX = datas[0].Split(',');
                headY = datas[1].Split(',');
                headZ = datas[2].Split(',');
                break;
            case 1:
                headX = datas[0].Split(' ');
                headY = datas[1].Split(' ');
                headZ = datas[2].Split(' ');
                break;
            default:
                headX = datas[0].Split(',');
                headY = datas[1].Split(',');
                headZ = datas[2].Split(',');
                break;
        }

        string[] lockDatas = _lockFile.text.Split('\n');


        // RecalculateVertices
        List<Vector3> vertices = new List<Vector3>();
        _mesh.GetVertices(vertices);
        for (int i = 0; i < count; i++)
        {
            int body = int.Parse(bodyIndex[i]);
            if (lockDatas.Contains((body - 146).ToString()))
            {
                continue;
            }
            int personal = int.Parse(personalIndex[i]);           
            Vector3 vertex = new Vector3(float.Parse(headX[personal]), float.Parse(headY[personal]), float.Parse(headZ[personal]));
            vertices[body] = vertex;
        }
        _adaptEye.Adapt(ref headX, ref headY, ref headZ, ref vertices);
        _mesh.vertices = vertices.ToArray();
        _mesh.RecalculateNormals();

        SwapTexture(pictComb);
    }

    private void SwapTexture(PictureCombination pictComb)
    {
        if (pictComb._texture != null)
        {
            _face.GetComponent<SkinnedMeshRenderer>().materials[0].SetTexture("Texture2D_36645CC3", pictComb._texture);
        }
        else
        {
            _face.GetComponent<SkinnedMeshRenderer>().materials[0].SetTexture("Texture2D_36645CC3",_texture);
        }
    }

    public void ResetVertices()
    {
        _mesh.SetVertices(_vertices);
        _mesh.RecalculateNormals();

        _face.GetComponent<SkinnedMeshRenderer>().materials[0].SetTexture("Texture2D_36645CC3", _texture);
    }
}
