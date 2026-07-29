using UnityEngine;

public class Player : Character
{
    public int LV = 1;
    public int EXP = 0;

   public Player(int id, string name, int maxHp, int nowHp, int speed, int armor) 
        : base(id, name, maxHp, nowHp, speed, armor)
    {
    }
}
