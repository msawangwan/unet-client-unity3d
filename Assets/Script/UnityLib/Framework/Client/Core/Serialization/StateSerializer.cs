using UnityEngine; 
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace UnityLib.Framework.Client.Core {
    public static class StateSerializer {
        private const string extension = ".save";

        public static void WriteSave(object obj, string fileDescriptor, int fileID = 0) {
            string json = JsonUtility.ToJson(obj, true);
            string path = PathFromFileName(fileDescriptor);
            StateSerializer.Save(json, path, fileID);
        }

        public static T LoadFromSave<T>(string fileDescriptor, int fileID = 0) where T : class {
            string path = PathFromFileName(fileDescriptor);
            if (File.Exists(path)) {
                BinaryFormatter serializer = new BinaryFormatter();
                FileStream fs = File.Open(path, FileMode.Open);
                string json = serializer.Deserialize(fs) as string;
                fs.Close();
                Debug.LogWarningFormat("found a save file at {1}. loaded: {0}", json, path);
                return JsonUtility.FromJson<T>(json);;
            } else {
                Debug.LogErrorFormat("no save file found at {0}", path);
                return null;
            }
        }

        private static void Save (object o, string path, int id) {
            BinaryFormatter serializer = new BinaryFormatter();
            FileStream fs = File.Create(path);
            serializer.Serialize(fs, o);
            fs.Close();

            #if UNITY_EDITOR
            Debug.LogWarningFormat("saved a file at {0}. file is {1}", path, o);
            UnityEditor.AssetDatabase.Refresh();
            #endif
        }

        private static string PathFromFileName(string name) {
            return Path.Combine (StringConstant.Path.SaveLoad_debug, string.Concat(name, extension) );
        }
    }
}
