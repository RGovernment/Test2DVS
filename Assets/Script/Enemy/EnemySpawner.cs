using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using static Enums;
using SF = UnityEngine.SerializeField;
public class EnemySpawner : MonoBehaviour
{
    [SF] private float spawnRadius;
    [SF] private GameObject enemy;
    [SF] private GameObject player;
    [SF] private Transform enemyGroup;
    [SF] private float spawnInterval;
    [SF] private int spawnSize;

    public List<EnemyCombat> SpawnList = new ();

    private ObjectPool<EnemyCombat> enemyPool;
    private float spawnTimer;

    private void Awake()
    {
        enemyPool = new ObjectPool<EnemyCombat>(CreateEnemy, EnemyActive, EnemyDisable,
        EnemyDistroy, true, 20, 300);
    }

    private void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnInterval && enemyPool.CountActive < spawnSize)
        {
            spawnTimer = 0f;
            if(GetObject(enemyPool,out EnemyCombat enemy))
                SpawnEnemy(enemy);
        }
    }

    private void SpawnEnemy(EnemyCombat enemy)
    {
        // 생성된 이후 추가적인 로직이 있는 경우 이곳에 추가
    }

    private EnemyCombat CreateEnemy()
    {
        GameObject obj = Instantiate(enemy, enemyGroup);

        EnemyStatus data = EnemyData.Instance.GetEnemyData();
        EnemyCombat combat = obj.AddComponent<EnemyCombat>();
        combat.spawner = this;
        combat.data = data;
        combat.player = player;
        combat.spawnRadius = spawnRadius;

        SpawnList.Add(combat);

        return combat;
    }

    private void EnemyActive(EnemyCombat obj)
    {
        obj.gameObject.SetActive(true);
    }

    private void EnemyDisable(EnemyCombat obj)
    {
        obj.gameObject.SetActive(false);
    }

    private void EnemyDistroy(EnemyCombat obj)
    {
        Destroy(obj.gameObject);
    }

    public bool GetObject(ObjectPool<EnemyCombat> data, out EnemyCombat enemy)
    {
        if (enemyPool.CountActive >= 500)
        {
            enemy = null;
            return false;
        }

        data.Get(out enemy);

        Vector2 randomCirclePoint = Random.onUnitCircle;

        Vector2 spawnOffset = new Vector2(randomCirclePoint.x, randomCirclePoint.y) * spawnRadius;

        Vector2 finalSpawnPosition = (Vector2)player.transform.position + spawnOffset;

        enemy.transform.position = finalSpawnPosition;

        return true;
    }

    public void ReleaseObject(EnemyCombat obj)
    {
        if (obj.CompareTag("PoolOver"))
            Destroy(obj);
        else
            enemyPool.Release(obj);

    }

    private void OnDrawGizmosSelected()
    {
        if (player != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(player.transform.position, spawnRadius);
        }
    }
}
