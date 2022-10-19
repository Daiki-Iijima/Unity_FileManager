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
            //  �f�B���N�g���̈ړ�
            m_Manager.ChangeDirectory(dirInfo);
            UpdateDirectory();
        };

        ui.OnClickFile = (fileInfo) => {
            //  �I�����ꂽ�t�@�C���̏�񂪕Ԃ��Ă���
            ProjectData data = m_Manager.LoadFileData(fileInfo, "�V�����t�@�C��");
            Debug.Log(data.Name);
        };

        ui.OnClickUpDir = () => {
            m_Manager.UpDirectory();

            UpdateDirectory();
        };

        ui.OnClickCreateFolder = () => {
            DirectoryInfo dir = m_Manager.CreateFolder("�V�����t�H���_");
            //  �����t�H���_������ꍇ
            if(dir == null){
                Debug.Log("�����̊����t�H���_������܂�");
            }

            UpdateDirectory();
        };
        ui.OnClickCreateFile = () => { 
            //  .json�g���q�������ł���
            m_Manager.CreateFile("�V�����t�@�C��");
            UpdateDirectory();
        };
    }

    //  �J�����g�f�B���N�g���̃t�@�C���ƃt�H���_�����ׂĎ擾
    public void UpdateDirectory() {
        ui.ClearSpace();

        List<FileSystemInfo> fileSystemInfo = m_Manager.GetDirectoryAllInfo();
        foreach (FileSystemInfo info in fileSystemInfo) {
            if (info is FileInfo) {
                FileInfo file = (FileInfo)info;
                //  UI�ɔ��f
                ui.CreateFile(file);
            }
            if (info is DirectoryInfo) {
                DirectoryInfo dir = (DirectoryInfo)info;
                //  UI�ɔ��f
                ui.CreateFolder(dir);
            }
        }

        if(fileSystemInfo.Count == 0){
            ui.CreateEmptyText();
        }

        //  ���[�g�f�B���N�g���̏ꍇ�́A�߂�{�^��������
        ui.SetEnableUpDirBnt(!m_Manager.IsRootDir);
    }

    // Update is called once per frame
    void Update() {

    }
}
