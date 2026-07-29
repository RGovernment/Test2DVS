using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStatus", menuName = "Scriptable Object/EnemyStatus")]
public class EnemyStatus : ScriptableObject
{
    public string Name = "더미";
    public int Hp = 100;
    public int Speed = 3;
    public int Damage = 10;
    public int Armor = 0;
    public Sprite Sprite;
}
