using UnityEngine;
using System.Collections.Generic; 
using System.Runtime.Serialization.Formatters.Binary; 
using System.IO;

public class StateSerializer : MonoBehaviour {
    //public static List<
    public static void  Save(object o) {
        string json = JsonUtility.ToJson(o, true);
        Debug.Log(json);
    }
}
