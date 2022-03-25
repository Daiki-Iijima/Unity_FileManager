using FileManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    
    void Start()
    {
        FileManager.FileManager manager = new FileManager.FileManager("TTT");
        manager.Save("テストん");
        manager.Save("テストん");
        manager.Save("テストん");
        manager.Save("テストん");

        Debug.Log("読み込み開始");

        string data = manager.Load();
        Debug.Log(data);

        Debug.Log(manager.GetFilePath());
    }

    void Update()
    {
        
    }
}
