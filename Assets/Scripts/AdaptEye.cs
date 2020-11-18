using System;
using System.Collections.Generic;
using UnityEngine;

public class AdaptEye : MonoBehaviour
{
    private const int _headVerticesNum = 20481;
    private const int _eyeballVerticesNum = 182;
    private const int _eyelashVerticesNum = 102;


    public string _pathSuffix;
    public GameObject _face;
    private float[] _head;

    private Vector3[] _eyeball = new Vector3[_eyeballVerticesNum];
    private Vector3[] _eyelash = new Vector3[_eyelashVerticesNum];



    public void Adapt(TextAsset personalFile,ref List<Vector3> vertices)
    {
        HeadImport(personalFile);
        CalculateEyeball(ref vertices);
        CalculateEyeLash(ref vertices);
    }


    private void HeadImport(TextAsset personalFile)
    {
        List<float> head = new List<float>();
        string[] headString = personalFile.text.Split('\n');
        string[] headX = headString[0].Split(',');
        string[] headY = headString[1].Split(',');
        string[] headZ = headString[2].Split(',');
        for (int i = 0; i < _headVerticesNum; i++)
        {
            head.Add(float.Parse(headX[i]));
        }
        for (int i = 0; i < _headVerticesNum; i++)
        {
            head.Add(float.Parse(headY[i]));
        }
        for (int i = 0; i < _headVerticesNum; i++)
        {
            head.Add(float.Parse(headZ[i]));
        }
        _head = head.ToArray();
    }

    private void CalculateEyeball(ref List<Vector3> vertices)
    {
        float[] outNum = new float[_eyeballVerticesNum * 3];
        IntPtr eyeballPtr = HeadDLLImport.AI_system_init_eyeball_qsmy(Application.dataPath + _pathSuffix);
        HeadDLLImport.AI_add_eyeball_qsmy(eyeballPtr, _head, outNum);
        for (int i = 0; i < outNum.Length; i++)
        {
            int axis = i / _eyeballVerticesNum;
            int index = i % _eyeballVerticesNum;
            switch (axis)
            {
                case 0:
                    _eyeball[index].x = outNum[i];
                    break;
                case 1:
                    _eyeball[index].y = outNum[i];
                    break;
                case 2:
                    _eyeball[index].z = outNum[i];
                    break;
                default:
                    break;
            }
        }
        for (int i = 0; i < _eyeball.Length; i++)
        {
            vertices[1223 + i] = OffsetAndScale.Correct(_eyeball[i]);
        }
    }

    private void CalculateEyeLash(ref List<Vector3> vertices)
    {
        float[] outNum = new float[_eyelashVerticesNum * 3];
        IntPtr eyelashPtr = HeadDLLImport.AI_system_init_eyelash_qsmy(Application.dataPath + _pathSuffix);
        HeadDLLImport.AI_add_eyelash_qsmy(eyelashPtr, _head, outNum);
        for (int i = 0; i < outNum.Length; i++)
        {
            int axis = i / _eyelashVerticesNum;
            int index = i % _eyelashVerticesNum;
            switch (axis)
            {
                case 0:
                    _eyelash[index].x = outNum[i];
                    break;
                case 1:
                    _eyelash[index].y = outNum[i];
                    break;
                case 2:
                    _eyelash[index].z = outNum[i];
                    break;
                default:
                    break;
            }
        }
        for (int i = 0; i < _eyelash.Length; i++)
        {
            vertices[4033 + i] = OffsetAndScale.Correct(_eyelash[i]);
        }
    }
}
