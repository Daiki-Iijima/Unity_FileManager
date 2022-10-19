using Newtonsoft.Json;
using System;
using System.IO;

public abstract class JsonData<T>
{
    [JsonIgnore]
    public string SavePath { get; private set; }

    public JsonData() {
    }

    public abstract bool Save(DirectoryInfo directoryInfo,string fileName);
    public abstract bool Load(FileInfo fileInfo);
    public abstract T FromJson(string json);

    protected bool SaveData(T data, DirectoryInfo directoryInfo, string fileName) {
        try {
            string json = JsonConvert.SerializeObject(data);
            FileStream fs = File.Create(directoryInfo.FullName + "\\" + fileName + ".json");
            StreamWriter sw = new StreamWriter(fs);
            sw.Write(json);
            sw.Flush();
            fs.Close();
            sw.Close();
        } catch (System.IO.FileNotFoundException) {
            return false;
        } catch (Exception) {
            return false;
        }

        return true;
    }

    protected T LoadData(FileInfo fileInfo) {
        try {
            FileStream fs = new FileStream(fileInfo.FullName,FileMode.Open);
            StreamReader sr = new StreamReader(fs);
            string json = sr.ReadToEnd();
            fs.Close();
            sr.Close();

            return FromJsonData(json);

        } catch (Exception) {
            return default(T);
        }
    }

    protected T FromJsonData(string json) {
        try {
            T result = JsonConvert.DeserializeObject<T>(json);
            return result;
        } catch (Exception) {
            return default(T);
        }
    }
}
