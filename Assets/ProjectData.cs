using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ProjectData : JsonData<ProjectData>
{
    public string Name;

    public ProjectData() {
        Name = "";
    }

    [JsonConstructor]
    public ProjectData(string name) {
        Name = name;
    }
    
    public override ProjectData FromJson(string json) {
        return FromJsonData(json);
    }

    public override bool Load(FileInfo fileInfo) {
        ProjectData data = LoadData(fileInfo);
        if(data == null) {
            return false;
        }

        //  ì«Ç›çûÇÒÇæÉfÅ[É^ÇÃë„ì¸
        Name = data.Name;

        return true;
    }

    public override bool Save(DirectoryInfo directoryInfo,string fileName) {
        return SaveData(this,directoryInfo, fileName);
    }
}
