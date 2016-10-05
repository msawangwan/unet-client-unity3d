using UnityEngine;


// https://code.tutsplus.com/tutorials/how-to-add-your-own-tools-to-unitys-editor--active-10047

public class GizmoGrid : MonoBehaviour {
    public float CellWidth = 32.0f;
    public float CellHeight = 32.0f;

    const float xAxisLines = 1200.0f;
    const float yAxisLines = 800.0f;
    const float KLargeValueMax = 1000000.0f;
    const float KLargeValueMin = -1000000.0f;

    Vector3 cameraPosition = Vector3.zero;
    Vector3 gridLineStart = Vector3.zero;
    Vector3 gridLineEnd = Vector3.zero;
    float yCurrent = 0.0f;
    float xCurrent = 0.0f;

    Camera cam {get { return Camera.main; } }

    float cellWidth { 
        get {
            if (CellWidth <= 0) {
                CellWidth = 0.1f;
            }
            return CellWidth;
        }
    }

    float cellHeight {
        get {
            if (CellHeight <= 0) {
                CellHeight = 0.1f;
            }
            return CellHeight;
        }
    }

    float xLineCountBase { get { return cameraPosition.x - xAxisLines; } }
    float yLineCountBase { get { return cameraPosition.y - yAxisLines; } }
    float xLineCountMax { get { return cameraPosition.x + xAxisLines; } }
    float yLineCountMax { get { return cameraPosition.y + yAxisLines; } }

    void OnDrawGizmos () {
        cameraPosition = cam.transform.position;

        for (float y = cameraPosition.y - yAxisLines; y < cameraPosition.y + yAxisLines; y += cellHeight) {
            yCurrent = Mathf.Floor ( y / cellHeight ) * cellHeight;
            gridLineStart = new Vector3 ( KLargeValueMin, yCurrent, 0.0f );
            gridLineEnd = new Vector3 ( KLargeValueMax, yCurrent, 0.0f );
            Gizmos.DrawLine ( gridLineStart, gridLineEnd );
        }

        for (float x = cameraPosition.x - xAxisLines; x < cameraPosition.x + xAxisLines; x++) {
            xCurrent = Mathf.Floor ( x / cellWidth ) * cellWidth;
            gridLineStart = new Vector3 ( xCurrent, KLargeValueMin, 0.0f );
            gridLineEnd = new Vector3 ( xCurrent, KLargeValueMax, 0.0f );
            Gizmos.DrawLine ( gridLineStart, gridLineEnd );
        }
    }
}
