using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileManagerCore {
    /// <summary>
    /// �f�[�^�̓ǂݏ������ł���f�B���N�g���̃��[�g�p�X
    /// ���̃p�X�́A���ɂ���ĕω�����̂Œ���
    /// </summary>
    public string RootPath { get; }

    public DirectoryInfo CurrentDirectory { get; private set; }

    public bool IsRootDir { get { return RootPath == CurrentDirectory.FullName.Replace("\\","/"); } }

    public FileManagerCore() {
        RootPath = Application.persistentDataPath;

        CurrentDirectory = new DirectoryInfo(RootPath);
    }

    /// <summary>
    /// ������f�B���N�g���̃t�H���_�A�t�@�C�������ׂĎ擾����
    /// </summary>
    /// <returns>�f�B���N�g�����̂��ׂĂ̏��</returns>
    public List<FileSystemInfo> GetDirectoryAllInfo(){
        List<FileSystemInfo> fileSystemInfo = new List<FileSystemInfo>();

        FileSystemInfo[] folder = GetDirectoryInfo();
        FileSystemInfo[] files = GetFileInfo();

        fileSystemInfo.AddRange(folder);
        fileSystemInfo.AddRange(files);

        return fileSystemInfo;
    }

    /// <summary>
    /// ���̃f�B���N�g����1��̃f�B���N�g���Ɉړ�����
    /// </summary>
    /// <returns>���[�g�f�B���N�g����</returns>
    public bool UpDirectory(){

        bool result = ChangeDirectory(CurrentDirectory.Parent);

        if (result) {
            //  ��ԏ�̃f�B���N�g���̏ꍇ
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
    /// �t�H���_�𐶐�����
    /// ���łɃt�H���_������ꍇ�A�t�H���_�͐�������Ȃ�
    /// </summary>
    /// <param name="folderName"></param>
    /// <returns></returns>
    public DirectoryInfo CreateFolder(string folderName) {

        foreach (DirectoryInfo dir in GetDirectoryInfo()) {
            //  �����t�H���_�Ɠ������O�̏ꍇ
            if (dir.Name == folderName) {
                return null;
            }
        }

        //  �V�����f�B���N�g���𐶐�
        DirectoryInfo info = CurrentDirectory.CreateSubdirectory(folderName);

        return info;
    }

    public FileInfo CreateFile(string fileName) {

        foreach(FileInfo info in GetFileInfo()) {
            if(info.Name.Replace(".json","") == fileName) {
                Debug.Log("���łɓ����t�@�C�������݂��邽�߁A�㏑�����܂�");
            }
        }

        ProjectData data = new ProjectData("�f�[�^2");
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
            Debug.Log("�ǂݍ��ݎ��s");
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
