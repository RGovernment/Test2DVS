using UnityEngine;
using static Constants;
using Newtonsoft.Json.Linq;

public class EnemyCombat : MonoBehaviour, IDamageable
{
    private Enemy stat;

    private void Start()
    {
        JObject data = EnemyData.Instance.enemyList[0];
        string name = data[NAME].GetValue<string>();
        int hp = data[HP].GetValue<int>();
        int speed = data[SPEED].GetValue<int>();
        int damage = data[DAMAGE].GetValue<int>();
        int armor = data[ARMOR].GetValue<int>();
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
