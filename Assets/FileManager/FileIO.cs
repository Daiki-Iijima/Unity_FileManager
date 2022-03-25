using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Text;
using System;

namespace FileManager
{
    public class FileIO : IFileIO
    {
        /// <summary>
        /// エラー発生時に発火するイベント
        /// </summary>
        public Action<string> OnError { get; set; }

        /// <summary>
        /// データを保存する
        /// </summary>
        /// <param name="path"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public string Load(string path, Encoding encoding)
        {
            try
            {
                using var fs = new StreamReader(path, encoding);
                string result = fs.ReadToEnd();
                return result;
            }
            catch
            {
                OnError?.Invoke(path + ":読み込み失敗");
                return "";
            }
        }

        /// <summary>
        /// ファイルに保存する
        /// </summary>
        /// <param name="path">保存するファイルのパス</param>
        /// <param name="data">保存するデータ</param>
        /// <param name="encoding">文字コード</param>
        /// <param name="isAppend">上書きするか</param>
        /// <returns>正常に保存できたか</returns>
        public bool Save(string path, string data, Encoding encoding, bool isAppend)
        {
            try
            {
                using var fs = new StreamWriter(path, isAppend, encoding);
                fs.Write(data);
                return true;
            }
            catch
            {
                OnError?.Invoke(path + ":保存失敗");
                return false;
            }
        }

    }
}
