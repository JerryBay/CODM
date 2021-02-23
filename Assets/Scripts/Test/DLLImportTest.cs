using System;
using System.Collections.Generic;
using UnityEngine;


public class DLLImportTest : MonoBehaviour
{
    private const int _vertices = 20481;
    private float[] _head;
    private float[] _out;
    private string _path = @"E:\Resources\Models";

    public TextAsset _headText;
    private void Awake()
    {
        HeadImport();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            Xueer();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            QSMY();
        }
    }
    private void Xueer()
    {
        Console.WriteLine("________________eyeball______________\n");
        HeadDLLImport.AI_system_init_eyeball(_path);
        double[] eulerL = new double[3];
        double[] eulerR = new double[3];
        double[] translationL = new double[3];
        double[] translationR = new double[3];
        double outScale = 0;
        HeadDLLImport.AI_add_eyeball(_head, 1, eulerL, eulerR, translationL, translationR, ref outScale);
        PrintVector3(eulerL);
        PrintVector3(eulerR);
        PrintVector3(translationL);
        PrintVector3(translationR);
        Debug.Log(outScale);

        Console.WriteLine("________________TBN______________\n");
        _out = new float[_vertices * 9];
        HeadDLLImport.AI_system_init_TBN(_path);
        HeadDLLImport.AI_get_TBN(_head, _out);
        for (int i = 0; i < 100; i++)
        {
            Debug.Log($"XueerTBN {_out[i]}  ");
        }

        Console.WriteLine("________________BS______________\n");
        _out = new float[_vertices * 18];
        HeadDLLImport.AI_system_initDynamic_BS(_path);
        HeadDLLImport.AI_get_BS(_head, _out);

        for (int i = 0; i < 100; i++)
        {
            Debug.Log($"XueerBS {_out[i]}  ");
        }
    }

    private void QSMY()
    {
        Console.WriteLine("________________headswap______________\n");
        _out = new float[_vertices * 9];
        IntPtr swapHeadPtr = HeadDLLImport.AI_system_init_swaphead(Application.dataPath + @"\Eyes\C_F_urban_Tracker");
        HeadDLLImport.AI_swap_head(swapHeadPtr, _head, _out);
        for (int i = 0; i < 100; i++)
        {
            Debug.Log($"QSMYSwapHead {_out[i]}  ");
        }

        Console.WriteLine("________________眼球______________\n");
        _out = new float[_vertices * 9];
        IntPtr eyeballPtr = HeadDLLImport.AI_system_init_eyeball_qsmy(Application.dataPath + @"\Eyes\C_F_urban_Tracker\");
        if (eyeballPtr == IntPtr.Zero)
        {
            Debug.Log("eyeball path failed");
        }
        HeadDLLImport.AI_add_eyeball_qsmy(eyeballPtr, _head, _out);
        for (int i = 0; i < 100; i++)
        {
            Debug.Log($"qsmyeyeball {_out[i]}  ");
        }

        Console.WriteLine("________________睫毛______________\n");
        _out = new float[_vertices * 9];
        IntPtr eyelashPtr = HeadDLLImport.AI_system_init_eyeball_qsmy(Application.dataPath + @"\Eyes\C_F_urban_Tracker\");
        if (eyelashPtr == IntPtr.Zero)
        {
            Debug.Log("eyelash path failed");
        }
        HeadDLLImport.AI_add_eyelash_qsmy(eyelashPtr, _head, _out);
        for (int i = 0; i < 100; i++)
        {
            Debug.Log($"qsmyeyelash {_out[i]}  ");
        }

        Console.WriteLine("________________TBN______________\n");
        string TBNPath = _path + @"\QSMY\girl\TBN\";
        IntPtr TBNptr = HeadDLLImport.AI_system_init_TBN_g2(TBNPath + "head_tri.obj", TBNPath + "idx_xueer2head.txt", TBNPath + "idx_head2ue4.txt");
        _out = new float[_vertices * 9];
        if (TBNptr == IntPtr.Zero)
        {
            Debug.Log("eyeTBN path failed");
        }
        HeadDLLImport.AI_get_TBN_g(TBNptr, _head, _out);
        for (int i = 0; i < 100; i++)
        {
            Debug.Log($"qsmyTBN {_out[i]}  ");
        }
    }


    private void HeadImport()
    {
        List<float> head = new List<float>();
        string[] headString = _headText.text.Split('\n');
        string[] headX = headString[0].Split(',');
        string[] headY = headString[1].Split(',');
        string[] headZ = headString[2].Split(',');
        for (int i = 0; i < 20481; i++)
        {
            head.Add(float.Parse(headX[i]));
        }
        for (int i = 0; i < 20481; i++)
        {
            head.Add(float.Parse(headY[i]));
        }
        for (int i = 0; i < 20481; i++)
        {
            head.Add(float.Parse(headZ[i]));
        }
        for (int i = 0; i < 2048100; i++)
        {
            head.Add(1f);
        }
        _head = head.ToArray();
    }

    private void PrintVector3(double[] vector)
    {
        Debug.Log($"{vector[0]} ,{vector[1]} ,{vector[2]}");
    }

    private void Message(KeyCode keyCode,object message)
    {
        if (Input.GetKeyDown(keyCode))
        {
            Debug.Log(message);
        }
    }
}
