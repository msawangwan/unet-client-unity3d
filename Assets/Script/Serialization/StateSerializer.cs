using UnityEngine;
using System.Collections.Generic; 
using System.Runtime.Serialization.Formatters.Binary; 
using System.IO;

public static class StateSerializer {
    public static List<string> JSONObjectGraph = new List<string>();

    public static void Save(object o) {
        string json = JsonUtility.ToJson(o, true);
        BinaryFormatter serializer = new BinaryFormatter();
        FileStream fs = File.Create(StringConstant.SaveToLocation.Save_debug);
        serializer.Serialize(fs, JSONObjectGraph);
        fs.Close();
    }

    public static void Load () {
        if (File.Exists(StringConstant.SaveToLocation.Save_debug)) {
            BinaryFormatter deserializer = new BinaryFormatter();
            FileStream fs = File.Open(StringConstant.SaveToLocation.Save_debug, FileMode.Open);
            JSONObjectGraph = deserializer.Deserialize(fs) as List<string>;
            fs.Close();
        }
    }
}
