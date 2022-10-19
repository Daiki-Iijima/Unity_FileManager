using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileManagerCore {
    /// <summary>
    /// データの読み書きができるディレクトリのルートパス
    /// このパスは、環境によって変化するので注意
    /// </summary>
    public string RootPath { get; }

    public DirectoryInfo CurrentDirectory { get; private set; }

    public bool IsRootDir { get { return RootPath == CurrentDirectory.FullName.Replace("\\","/"); } }

    public FileManagerCore() {
        RootPath = Application.persistentDataPath;

        CurrentDirectory = new DirectoryInfo(RootPath);
    }

    /// <summary>
    /// 今いるディレクトリのフォルダ、ファイルをすべて取得する
    /// </summary>
    /// <returns>ディレクトリ内のすべての情報</returns>
    public List<FileSystemInfo> GetDirectoryAllInfo(){
        List<FileSystemInfo> fileSystemInfo = new List<FileSystemInfo>();

        FileSystemInfo[] folder = GetDirectoryInfo();
        FileSystemInfo[] files = GetFileInfo();

        fileSystemInfo.AddRange(folder);
        fileSystemInfo.AddRange(files);

        return fileSystemInfo;
    }

    /// <summary>
    /// 今のディレクトリの1つ上のディレクトリに移動する
    /// </summary>
    /// <returns>ルートディレクトリか</returns>
    public bool UpDirectory(){

        bool result = ChangeDirectory(CurrentDirectory.Parent);

        if (result) {
            //  一番上のディレクトリの場合
            return IsRootDir;
        } else {
            return false;
        }
    }

    public bool ChangeDirectory(string path){
        DirectoryInfo dir = new DirectoryInfo(path);
        if(dir == null){
            CurrentDirectory = dir;
            return true;
        }else{
            return false;
        }
    }
    public bool ChangeDirectory(DirectoryInfo dirInfo){
        DirectoryInfo dir = new DirectoryInfo(dirInfo.FullName);
        if(dir == null){
            return false;
        }else{
            CurrentDirectory = dir;
            return true;
        }
    }

    public FileInfo[] GetFileInfo() {
        if (CurrentDirectory == null) {
            return null;
        }

        return CurrentDirectory.GetFiles();
    }

    public DirectoryInfo[] GetDirectoryInfo() {
        if (CurrentDirectory == null) {
            return null;
        }

        return CurrentDirectory.GetDirectories();
    }

    /// <summary>
    /// フォルダを生成する
    /// すでにフォルダがある場合、フォルダは生成されない
    /// </summary>
    /// <param name="folderName"></param>
    /// <returns></returns>
    public DirectoryInfo CreateFolder(string folderName) {

        foreach (DirectoryInfo dir in GetDirectoryInfo()) {
            //  既存フォルダと同じ名前の場合
            if (dir.Name == folderName) {
                return null;
            }
        }

        //  新しいディレクトリを生成
        DirectoryInfo info = CurrentDirectory.CreateSubdirectory(folderName);

        return info;
    }

    public FileInfo CreateFile(string fileName) {

        foreach(FileInfo info in GetFileInfo()) {
            if(info.Name.Replace(".json","") == fileName) {
                Debug.Log("すでに同名ファイルが存在するため、上書きします");
            }
        }

        ProjectData data = new ProjectData("データ2");
        bool result = data.Save(CurrentDirectory,fileName);

        if (!result) {
            return null;
        }

        foreach(FileInfo info in GetFileInfo()) {
            if(info.Name.Replace(".json","") == fileName) {
                return info;
            }
        }

        return null;
    }

    public ProjectData LoadFileData(FileInfo info,string fileName) {
        ProjectData data = new ProjectData();
        bool result = data.Load(info);

        if (!result) {
            Debug.Log("読み込み失敗");
        }

        return data;
    }

    public void DeleteFile(){

    }
    public void DeleteFolder(){

    }

    public void MoveToDirectory(List<FileSystemInfo> fileSystemInfos,string destDir){
        foreach (FileSystemInfo info in fileSystemInfos) {
            if (info is FileInfo) {
                FileInfo file = (FileInfo)info;
                file.MoveTo(destDir);
            }
            if (info is DirectoryInfo) {
                DirectoryInfo dir = (DirectoryInfo)info;
                dir.MoveTo(destDir);
            }
        }
    }
}
