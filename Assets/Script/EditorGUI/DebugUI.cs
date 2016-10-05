using UnityEngine;

public class DebugUI : MonoBehaviour {
    void OnGUI(){
        GUI.Box (NumericalConstant.ScreenResolution.TopLeftRect, "Top-left");
        GUI.Box (NumericalConstant.ScreenResolution.TopRightRect, "Top-right");
        GUI.Box (NumericalConstant.ScreenResolution.BottomLeftRect, "Bottom-left");
        GUI.Box (NumericalConstant.ScreenResolution.BottomRightRect, "Bottom-right");
    }
}
