using UnityEngine;

public static class NumericalConstant {
    public static class ScreenResolution {
        public static readonly Vector2 TopLeftOrthographic = new Vector2 (0f, Screen.height);
        public static readonly Vector2 TopRightOrthographic = new Vector2(Screen.width, Screen.height);
        public static readonly Vector2 BottomLeftOrthographic = new Vector2 (0f, 0f);
        public static readonly Vector2 BottomRightOrthographic = new Vector2 (Screen.width, 0f);
        public static readonly Rect TopLeftRect = new Rect(0, 0, 100, 50);
        public static readonly Rect TopRightRect = new Rect(Screen.width - 100, 0, 100, 50);
        public static readonly Rect BottomLeftRect = new Rect(0, Screen.height - 50, 100, 50);
        public static readonly Rect BottomRightRect = new Rect(Screen.width - 100, Screen.height - 50, 100, 50);
    }
}
