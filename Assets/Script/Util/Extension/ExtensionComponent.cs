using UnityEngine;

public static class ExtensionComponent {
    /* get or add a component, see: http://wiki.unity3d.com/index.php/GetOrAddComponent */
    public static T GameObjectComponent<T> ( this Component goComponent ) where T : Component {
        T c = goComponent.GetComponent<T> ();
        if ( c == null ) {
            c = goComponent.gameObject.AddComponent<T>();
        }
        return c;
    }
}