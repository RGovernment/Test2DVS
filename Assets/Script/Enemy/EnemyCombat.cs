using UnityEngine;
using static Constants;
using Newtonsoft.Json.Linq;

public class EnemyCombat : MonoBehaviour, IDamageable
{
    private Enemy stat;

    private void Start()
    {
        EnemyStatus data = EnemyData.Instance.enemyList[0];
        string name = data.name;
        int hp = data.hp;
        int speed = data.speed;
        int damage = data.damage;
        int armor = data.armor;
        stat = new Enemy(EnemyData.Instance.GetEnemyID(), name, hp, hp, speed, armor, damage);
    }

    public void TakeDamage(int damage)
    {
        stat.NowHp = Mathf.Clamp(stat.NowHp - (damage - stat.Armor), 0, stat.MaxHp);

        if(stat.NowHp <= 0)
        {
            Destroy(gameObject);
        }

    }
}
