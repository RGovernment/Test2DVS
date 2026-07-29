using UnityEngine;


[CreateAssetMenu(fileName = "ProjectileData",menuName = "Scriptable Object/Projectile")]
public class Projectile : ScriptableObject
{
    public int Id;
    public string ProjectileName;
    public string Damage;
    public Sprite Sprite;
}
