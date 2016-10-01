using UnityEngine;

public static class ExtensionGameObject {
    /* get or add a component, see: http://wiki.unity3d.com/index.php/GetOrAddComponent */
    public static T GameObjectComponent<T> ( this GameObject goComponent ) where T : Component {
        T c = goComponent.GetComponent<T> ();
        if ( c == null ) {
            c = goComponent.gameObject.AddComponent<T>();
        }
        return c;
    }
}
