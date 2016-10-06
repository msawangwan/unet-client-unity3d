using UnityEngine;

[CreateAssetMenuAttribute (menuName = StringConstant.AssetMenu.Map, order = 0)]
public class Star_deprecated_so : ScriptableObject {
    public string Name = "star";
    public int FuelSupply = 100;
    public int OxygenSupply = 100;
}
