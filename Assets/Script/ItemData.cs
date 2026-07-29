using UnityEngine;
using static Enums;

[CreateAssetMenu(fileName ="items",menuName ="Scriptable Object/Items")]
public class ItemData : ScriptableObject
{
    public string Name = "name";
    public string Description = "description";
    public ItemType Type = ItemType.Exp;
    public int effect = 10;
}
