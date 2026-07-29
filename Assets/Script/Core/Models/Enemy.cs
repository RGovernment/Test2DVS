    using UnityEngine;

public class Enemy : Character
{
    public int damage;
    
    public Enemy(int id, string name, int maxHp, int nowHp, int speed, int armor, int damage)
        : base(id, name, maxHp, nowHp, speed, armor)
    {
        this.damage = damage;
    }

}
