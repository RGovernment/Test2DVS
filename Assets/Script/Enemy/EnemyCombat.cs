using UnityEngine;
using static Constants;
using Newtonsoft.Json.Linq;
using Unity.VisualScripting;

public class EnemyCombat : MonoBehaviour, IDamageable
{
    public EnemyStatus data;
    [HideInInspector] public EnemySpawner spawner;
    private Enemy stat;
    private SpriteRenderer spriteRenderer;
    public GameObject player;
    public float spawnRadius;
    private int spawnCount = 0;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        string name = data.Name;
        int hp = data.Hp;
        int speed = data.Speed;
        int damage = data.Damage;
        int armor = data.Armor;
        //sr.sprite = data.Sprite;

        stat = new Enemy(EnemyData.Instance.GetEnemyID(), name, hp, hp, speed, armor, damage);

        Vector2 randomCirclePoint = Random.onUnitCircle;

        Vector2 spawnOffset = new Vector2(randomCirclePoint.x, randomCirclePoint.y) * spawnRadius;

        Vector2 finalSpawnPosition = (Vector2)player.transform.position + spawnOffset;

        transform.position = finalSpawnPosition;
        spawnCount++;
    }

    private void OnEnable()
    {
        if(spawnCount > 0)
            stat.NowHp = data.Hp;
    }

    public void TakeDamage(int damage)
    {
        stat.NowHp = Mathf.Clamp(stat.NowHp - (damage - stat.Armor), 0, stat.MaxHp);

        if(stat.NowHp <= 0)
            spawner.ReleaseObject(this);
        

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision != null && collision.collider.CompareTag("Player"))
        {
            if(collision.collider.TryGetComponent(out IDamageable data))
            {
                data.TakeDamage(stat.damage);
            }
        }
    }
}
