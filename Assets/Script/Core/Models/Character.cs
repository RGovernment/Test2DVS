using UnityEngine;

public class Character
{
    public int id;
    public string name;
    public int MaxHp;
    public int NowHp;
    public int Speed;
    public int Armor;

    public Character(int id, string name, int maxHp, int nowHp, int speed, int armor)
    {
        this.id = id;
        this.name = name;
        MaxHp = maxHp;
        NowHp = nowHp;
        Speed = speed;
        Armor = armor;
    }
}
