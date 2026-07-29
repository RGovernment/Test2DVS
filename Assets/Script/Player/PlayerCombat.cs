using Newtonsoft.Json.Linq;
using UnityEngine;
using static Constants;
public class PlayerCombat : MonoBehaviour, IDamageable
{
    private Player stat;

    // JObject 로드 임시
    private JObject playerData = new();

    private float hitTime = 1.2f;
    private float hitTimer;

    private void Start()
    {
        playerData = new()
        {
            [ID] = 1000,
            [NAME] = "설정된 이름",
            [HP] = 300,
            [SPEED] = 5,
            [ARMOR] = 3
        };
        string name = playerData[NAME].GetValue<string>();
        int id = playerData[ID].GetValue<int>();
        int hp = playerData[HP].GetValue<int>();
        int speed = playerData[SPEED].GetValue<int>();
        int armor = playerData[ARMOR].GetValue<int>();
        stat = new Player(id, name, hp, hp, speed, armor);
    }

    private void Update()
    {
        hitTimer += Time.deltaTime;
    }

    public void TakeDamage(int damage)
    {
        if (hitTime > hitTimer) return;
        
        stat.NowHp = Mathf.Clamp(stat.NowHp - (damage - stat.Armor), 0, stat.MaxHp);
        Debug.Log(stat.NowHp);
        hitTimer = 0;


        if (stat.NowHp <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void OnDestroy()
    {
        Debug.Log("플레이어 패배 처리");
    }
}
