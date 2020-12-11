using UnityEngine;
using System.Collections.Generic;

public class OffsetAndScale : MonoBehaviour
{
    public static Vector3 Correct(Vector3 vertex)
    {
        vertex.z = -vertex.z;
        vertex /= 100;
        float temp = vertex.z;
        vertex.z = vertex.y;
        vertex.y = temp;

        return vertex;
    }

    public static void AdaptEye(ref string[] x,ref string[] y,ref string[] z)
    {
        const int _headVerticesNum = 20481;
        string[] temp = y;
        y = z;
        z = temp;
        for (int i = 0; i < _headVerticesNum; i++)
        {
            x[i] = (float.Parse(x[i]) * 100).ToString();
        }
        for (int i = 0; i < _headVerticesNum; i++)
        {
            y[i] = (float.Parse(y[i]) * 100).ToString();
        }
        for (int i = 0; i < _headVerticesNum; i++)
        {
            z[i] = (-float.Parse(z[i]) * 100).ToString();
        }
        List<float> head = new List<float>();
        string[] headX = x;
        string[] headY = y;
        string[] headZ = z;
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
    }
}
