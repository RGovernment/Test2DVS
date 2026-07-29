using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Pool;
using static Constants;
using static Enums;
using SF = UnityEngine.SerializeField;
public class PlayerAttack : MonoBehaviour
{
    [SF] private PlayerAttackArea attackArea;
    [SF] private Projectile[] projectile;
    [SF] private List<ProjectileData> projectileData; 
    [SF] private GameObject bullet;
    [SF] private Transform muzzlePoint;
    [SF] private float bulletSpeed = 15f;
    [SF] private float lifeTime = 10f;
    [SF] private Transform muzzlePointOffset;

    private ObjectPool<ProjectileData> firePool;

    private SpriteRenderer sr;
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        projectileData = new List<ProjectileData>();
        firePool = new ObjectPool<ProjectileData>(CreateFire, ProjectileActive, ProjectileDisable,
        ProjectileDistroy, true, 20, POOL_MAX_SIZE);
    }

    private void Update()
    {
        if (attackArea.enemyList.Count <= 0) return;
        ActiveProjectile();
    }

    private ProjectileData CreateFire()
    {
        int itemIndex = 0;

        GameObject obj = new(ProjectileType.Fire.ToString());

        obj.transform.parent = muzzlePoint;

        ProjectileData data = obj.AddComponent<ProjectileData>();
        data.Id = projectile[itemIndex].Id;
        data.ProjectileName = projectile[itemIndex].ProjectileName;
        data.Damage = projectile[itemIndex].Damage;
        data.Sprite = projectile[itemIndex].Sprite;

        projectileData.Add(data);

        return data;
    }

    private void ActiveProjectile()
    {
        // 파이어 작동(기본 공격) n 주기 마다 작동하도록 유지
        if(GetObject(firePool, out ProjectileData data) != null) 
            FireActive(data);
    }


    private void FireActive(ProjectileData obj)
    {
        if(attackArea.GetClosestTarget(out Transform target) != null) 
        {
            obj.transform.parent = muzzlePoint;
            obj.transform.LookAt(target);

            obj.rb.linearVelocity = 
                (new Vector2(target.position.x,target.position.y) - obj.rb.position).normalized * bulletSpeed;
            StartCoroutine(ProjectileRelease(obj));
        }
        
    }

    private IEnumerator ProjectileRelease(ProjectileData obj)
    {
        yield return new WaitForSeconds(3);
        ReleaseObject(obj);
    }

    private void ProjectileActive(ProjectileData obj)
    {
        obj.gameObject.SetActive(true);
    }

    private void ProjectileDisable(ProjectileData obj)
    {
        obj.gameObject.SetActive(false);
    }

    private void ProjectileDistroy(ProjectileData obj)
    {
        Destroy(obj);
    }

    public ProjectileData GetObject(ObjectPool<ProjectileData> data, out ProjectileData projectile)
    {
        // 1단계 방지턱 체크
        if (firePool.CountActive >= POOL_MAX_SIZE) return projectile = null;

        data.Get(out projectile);

        return projectile;
    }

    public void ReleaseObject(ProjectileData obj)
    {
        if (obj.CompareTag("PoolOver"))
            Destroy(obj);
        else
            firePool.Release(obj);
        
    }

}
