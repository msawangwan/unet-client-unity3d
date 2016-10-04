using UnityEngine;

/*
    global constants -- requires 'using UnityEngine' (look into why)
*/

public static class StringConstant {
    public static class Tag {
        public const string Player = "Player";
    }

    public static class Path {
        public const string RootDir = "Assets/";
        public const string AssetDB = "Assets/AssetDB";
    }

    public static class AssetMenu {
        public const string Map = "Map/Star";
    }

    public static class WindowMenu {
        public const string MapEditor = "Window/Map/MapEditor";
    }
}