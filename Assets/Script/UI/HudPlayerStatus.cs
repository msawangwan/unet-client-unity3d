using UnityEngine;
using UnityEngine.UI;

public class HudPlayerStatus : MonoBehaviour {
    public GameObject FuelStatus = null;
    public GameObject O2Status = null;

    private Text m_fuelStatusText = null;
    private Text fuelStatusText {
        get {
            if (m_fuelStatusText == null) {
                m_fuelStatusText = FuelStatus.GetComponent<Text>();
            }
            return m_fuelStatusText;
        }
    }

    private Text m_o2StatusText = null;
    private Text o2StatusText {
        get {
            if (m_o2StatusText == null) {
                m_o2StatusText = O2Status.GetComponent<Text>();
            }
            return m_o2StatusText;
        }
    }

    void Start () {
        fuelStatusText.text = "fuel: 100";
        o2StatusText.text = "o2: 100";
    }
}
