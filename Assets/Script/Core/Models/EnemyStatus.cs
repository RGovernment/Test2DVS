using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStatus", menuName = "Scriptable Object/EnemyStatus")]
public class EnemyStatus : ScriptableObject
{
    public string name = "더미";
    public int hp = 100;
    public int speed = 3;
    public int damage = 10;
    public int armor = 0;
}
