using UnityEngine;

public interface Descriptor {
    string NameField { get; set; } // todo: prepend UI prefix
    string DescriptionField { get; set; }
}
