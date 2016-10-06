using UnityEngine;

public interface Descriptor {
    string NameField { get; } // todo: prepend UI prefix
    string DescriptionField { get; }
}
