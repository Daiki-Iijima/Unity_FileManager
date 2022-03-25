using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;


namespace FileManager
{
    public class FileManager
    {

        private string m_path;

        public IFileIO m_fileIO;

        public Action<string> OnFileIOError;

        public FileManager(string filePath)
        {
            m_path = Application.persistentDataPath + "/" + filePath;

            m_fileIO = new FileIO();

            m_fileIO.OnError += (str) => { OnFileIOError?.Invoke(str); };
        }

        public bool Save(string data,bool isAppend = true)
        {
            return m_fileIO.Save(m_path, data, Encoding.GetEncoding("UTF-8"), isAppend);
        }
        public string Load()
        {
            return m_fileIO.Load(m_path, Encoding.GetEncoding("UTF-8"));
        }

        public string GetFilePath(){
            return m_path;
        }
    }
}
