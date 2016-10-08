using UnityEngine; 
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

/*
https://gamedevelopment.tutsplus.com/tutorials/how-to-save-and-load-your-players-progress-in-unity--cms-20934
https://www.sitepoint.com/mastering-save-and-load-functionality-in-unity-5/
*/

public static class StateSerializer {
    private const string extension = ".save";

    public static void WriteSave(object obj, string fileDescriptor, int fileID = 0) {
        string json = JsonUtility.ToJson(obj, true);
        string path = PathFromFileName(fileDescriptor);
        StateSerializer.Save(obj, path, fileID);
    }

    public static T LoadFromSave<T>(string fileDescriptor, int fileID = 0) where T : class {
        string path = PathFromFileName(fileDescriptor);
        if (File.Exists(path)) {
            BinaryFormatter deserializer = new BinaryFormatter();
            FileStream fs = File.Open(path, FileMode.Open);
            string json = deserializer.Deserialize(fs) as string;
            fs.Close();
            return JsonUtility.FromJson<T>(json);
        } else {
            return null;
        }
    }

    private static void Save (object o, string filename, int id) {
        BinaryFormatter serializer = new BinaryFormatter();
        FileStream fs = File.Create(filename);
        serializer.Serialize(fs, o);
        fs.Close();
    }

    private static string PathFromFileName(string name) {
        return Path.Combine (StringConstant.Path.SaveLoad_debug, string.Concat(name, extension) );
    }
}
