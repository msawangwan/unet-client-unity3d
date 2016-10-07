using UnityEngine;

/*
    global constants -- requires 'using UnityEngine' (look into why)
*/

public static class StringConstant {
    public static class Tag {
        public const string Player = "Player";
    }

    public static class Path {
        public const string Root = "Assets/";
        public const string AssetDB = "Assets/AssetDB";
        public const string SaveLoad_debug = "Assets/Save";
        public static readonly string SaveLoad = Application.persistentDataPath;
    }

    public static class Filename {
        public const string Save = "game.save";
    }

    public static class SaveToLocation {
        public static readonly string Save = System.IO.Path.Combine(Path.SaveLoad, Filename.Save);
        public static readonly string Save_debug = System.IO.Path.Combine(Path.SaveLoad_debug, Filename.Save);
    }

    public static class AssetMenu {
        public const string Map = "Map/Node";
        public const string Star = "Star/Node";
    }

    public static class WindowMenu {
        public const string MapEditor = "Window/Map/MapEditor";
    }
}