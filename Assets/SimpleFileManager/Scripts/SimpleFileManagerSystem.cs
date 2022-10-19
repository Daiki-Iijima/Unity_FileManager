using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SimpleFileManagerSystem : MonoBehaviour
{
    private FileManagerCore m_Manager;

    [SerializeField] private SimpleFileManagerUI ui;

    void Start() {
        m_Manager = new FileManagerCore();

        UpdateDirectory();

        ui.OnClickDirectory = (dirInfo) => {
            //  ディレクトリの移動
            m_Manager.ChangeDirectory(dirInfo);
            UpdateDirectory();
        };

        ui.OnClickFile = (fileInfo) => {
            //  選択されたファイルの情報が返ってくる
            ProjectData data = m_Manager.LoadFileData(fileInfo, "新しいファイル");
            Debug.Log(data.Name);
        };

        ui.OnClickUpDir = () => {
            m_Manager.UpDirectory();

            UpdateDirectory();
        };

        ui.OnClickCreateFolder = () => {
            DirectoryInfo dir = m_Manager.CreateFolder("新しいフォルダ");
            //  既存フォルダがある場合
            if(dir == null){
                Debug.Log("同名の既存フォルダがあります");
            }

            UpdateDirectory();
        };
        ui.OnClickCreateFile = () => { 
            //  .json拡張子を自動でつける
            m_Manager.CreateFile("新しいファイル");
            UpdateDirectory();
        };
    }

    //  カレントディレクトリのファイルとフォルダをすべて取得
    public void UpdateDirectory() {
        ui.ClearSpace();

        List<FileSystemInfo> fileSystemInfo = m_Manager.GetDirectoryAllInfo();
        foreach (FileSystemInfo info in fileSystemInfo) {
            if (info is FileInfo) {
                FileInfo file = (FileInfo)info;
                //  UIに反映
                ui.CreateFile(file);
            }
            if (info is DirectoryInfo) {
                DirectoryInfo dir = (DirectoryInfo)info;
                //  UIに反映
                ui.CreateFolder(dir);
            }
        }

        if(fileSystemInfo.Count == 0){
            ui.CreateEmptyText();
        }

        //  ルートディレクトリの場合は、戻るボタンを消す
        ui.SetEnableUpDirBnt(!m_Manager.IsRootDir);
    }

    // Update is called once per frame
    void Update() {

    }
}
