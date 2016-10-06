using UnityEngine;

public class Star {
    public LinkedList<Star>.Node NodeLink = null;

    public Star () {
        NodeLink = StarMap.Stars.Add ( this );
    }
}
