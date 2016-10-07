using UnityEngine; 
using System;
using System.IO;
using System.Collections.Generic; 
using System.Runtime.Serialization.Formatters.Binary;

/*
https://gamedevelopment.tutsplus.com/tutorials/how-to-save-and-load-your-players-progress-in-unity--cms-20934
https://www.sitepoint.com/mastering-save-and-load-functionality-in-unity-5/
*/

public static class StateSerializer {
    public class Data {
        public string FileToWrite = string.Empty;
        public List<string> CollectionToWrite = null;
        
        public Data(string fileToWrite) {
            FileToWrite = fileToWrite;
        }

        public Data(List<string> collectionToWrite) {
            CollectionToWrite = collectionToWrite;
        }
    }

    public static List<string> JSONObjectGraph = new List<string>();
    public static Action<StateSerializer.Data> OnRaiseNewSaveGameWrite { get; set; }

    public static void Save (object o) {
        string json = JsonUtility.ToJson(o, true);
        Debug.LogFormat("{0}", json);
        BinaryFormatter serializer = new BinaryFormatter();
        FileStream fs = File.Create(StringConstant.SaveToLocation.Save_debug);
        serializer.Serialize(fs, json);
        fs.Close();
    }

    public static void Load () {
        if (File.Exists(StringConstant.SaveToLocation.Save_debug)) {
            BinaryFormatter deserializer = new BinaryFormatter();
            FileStream fs = File.Open(StringConstant.SaveToLocation.Save_debug, FileMode.Open);
            //JSONObjectGraph = deserializer.Deserialize(fs) as List<string>;
            string json = deserializer.Deserialize(fs) as string;
            Star star = JsonUtility.FromJson<Star>(json);
            Debug.LogFormat("{0}", star.StarProperties.Designation);
            fs.Close();
        }
    }
}
