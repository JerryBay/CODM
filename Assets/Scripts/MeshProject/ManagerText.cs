using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class ManagerText : MonoBehaviour
{
    public string _path=$@"E:\Resources\idx_COD_head.txt";
    public TextAsset _indexFile;
    public int _difference;

    void Start()
    {
        string[] indexs = _indexFile.text.Split('\n');
        string[] bodyIndex = indexs[0].Split(',');
        string[] headIndex = indexs[1].Split(',');

        for (int i = 0; i < bodyIndex.Length; i++)
        {
            bodyIndex[i] = (int.Parse(bodyIndex[i]) + _difference).ToString();
        }

        for (int i = 0; i < bodyIndex.Length; i++)
        {
            File.AppendAllText(_path, bodyIndex[i] + ",");
        }
        File.AppendAllText(_path, "\n");
        for (int i = 0; i < headIndex.Length; i++)
        {
            File.AppendAllText(_path, headIndex[i]+ ",");
        }
    }
}
