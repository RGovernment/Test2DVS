using UnityEngine;

public class Player : Character, IDamageable
{
   public Player(int id, string name, int maxHp, int nowHp, int speed, int armor) 
        : base(id, name, maxHp, nowHp, speed, armor)
    {
    }

    public void TakeDamage(int damage)
    {
        
    }
}
