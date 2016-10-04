using UnityEngine;
using System.IO;

public class PropDrawTest : MonoBehaviour {
    public enum AUnit { 
        rifle,
        gunner,
        scout,
        lancer,
    }

    [System.Serializable]
    public class AUnitGuy {
        string name;
        int amount = 11;
        AUnit aunit;
    }

    public AUnitGuy potResult;
    public AUnitGuy[] potResultssss;
}