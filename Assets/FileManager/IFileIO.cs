using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace FileManager
{
    public interface IFileIO
    {
        Action<string> OnError { get; set; }

        string Load(string path, Encoding encoding);

        bool Save(string path, string data, Encoding encoding, bool isAppend);
    }
}
