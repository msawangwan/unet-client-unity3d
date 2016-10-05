using UnityEngine;

public static class ExtensionGameObject {
    /* get or add a component, see: http://wiki.unity3d.com/index.php/GetOrAddComponent */
    public static T GameObjectComponent<T> ( this GameObject go ) where T : Component {
        T c = go.GetComponent<T> ();
        if ( c == null ) {
            c = go.gameObject.AddComponent<T>();
        }
        return c;
    }

    /* a generic wrapper to get an interface as if it were a component */
    public static T GetComponentInterface<T> ( this GameObject go ) where T : class {
        return go.GetComponent ( typeof ( T ) ) as T;
    }
}
