using UnityEngine;

[CreateAssetMenu(fileName ="New Ownable", menuName = "Ownables")]
public class OwnableType : ScriptableObject
{
    public string OwnableTypeName;

    public int MoneyValue;

    public int lifeTime;
    public string ResourceName;
    public Color[] DefaultColors;


    public bool IsEquipable;
    public EquipLocation equipLocation;

    public Sprite GetSprite()
    {
        return Resources.Load<Sprite>("Art/Icons/ItemIcons/" + ResourceName+"Icon");
    }

}
