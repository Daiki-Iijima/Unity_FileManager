using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SimpleFileManagerUI: MonoBehaviour
{
    [SerializeField] private Transform contetSpace;

    [SerializeField] private GameObject folderPrefab;
    [SerializeField] private GameObject filePrefab;
    [SerializeField] private GameObject emptyTextPrefab;

    [SerializeField] private Button upDirBtn;
    [SerializeField] private Button createFolderBtn;
    [SerializeField] private Button createFileBtn;

    public Action<DirectoryInfo> OnClickDirectory;
    public Action<FileInfo> OnClickFile;

    public Action OnClickUpDir;
    public Action OnClickCreateFolder;
    public Action OnClickCreateFile;

    void Start()
    {
        upDirBtn.onClick.AddListener(() => {
            OnClickUpDir?.Invoke();
        });

        createFolderBtn.onClick.AddListener(() => { OnClickCreateFolder?.Invoke(); });
        createFileBtn.onClick.AddListener(() => { OnClickCreateFile?.Invoke(); });
    }

    public void SetEnableUpDirBnt(bool flag){
        upDirBtn.gameObject.SetActive(flag);
    }

    public void ClearSpace(){
        foreach (Transform t in contetSpace.transform) {
            Destroy(t.gameObject);
        }
    }

    public void CreateFolder(DirectoryInfo info) {
        GameObject obj = Instantiate(folderPrefab, contetSpace);
        Button btn = obj.GetComponent<Button>();

        obj.transform.Find("Text").GetComponent<Text>().text = info.Name;

        btn.onClick.AddListener(() => { OnClickDirectory?.Invoke(info); });
    }

    public void CreateFile(FileInfo info){
        GameObject obj = Instantiate(filePrefab, contetSpace);
        Button btn = obj.GetComponent<Button>();

        obj.transform.Find("Text").GetComponent<Text>().text = info.Name;

        btn.onClick.AddListener(() => { OnClickFile?.Invoke(info); });
    }

    public void CreateEmptyText(){
        GameObject obj = Instantiate(emptyTextPrefab, contetSpace);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
