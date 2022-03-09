using UnityEngine;

[CreateAssetMenu(fileName ="New Ownable", menuName = "Ownables")]
public class OwnableType : ScriptableObject
{
    public string OwnableTypeName;

    public int MoneyValue;
    public string ResourcePath;



    public EquipLocation equipLocation;
}
