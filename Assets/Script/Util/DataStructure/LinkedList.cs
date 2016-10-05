using UnityEngine;

public class LinkedList<T> where T : class {
    public class Node {
        public T    Element { get; private set; }
        public Node Next    { get; set; }

        public Node ( T element ) {
            Element  = element;
        }
    }

    public Node Head { get; private set; }
    public int Size  { get; private set; }

    public LinkedList() {
        Head = null;
        Size = 0;
    }

    public Node Add ( T element ) {
        Node n = new Node ( element );
        if (Head == null) {
            Head = n;
            n.Next = null;
        } else {
            n.Next = Head;
            Head = n;
        }
        ++Size;
        return n;
    }
}
