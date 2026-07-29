using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Pool;
using SF = UnityEngine.SerializeField;
public class PlayerAttack : MonoBehaviour
{
    [SF] private GameObject bullet;
    [SF] private Transform muzzlePoint;
    [SF] private float bulletSpeed = 15f;
    [SF] private float lifeTime = 10f;
    [SF] private Transform muzzlePointOffset;

    private ObjectPool<GameObject> bulletsPool;

    private SpriteRenderer sr;
    private float direction;
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();

        bulletsPool = new ObjectPool<GameObject>(CreateBullet, BulletActive, BulletDisable,
        BulletDistroy, true, 40, 400);
    }

    private void Update()
    {
        if (Keyboard.current.sKey.isPressed) Fire(GetObject());


        direction = sr.flipX ? -1 : 1;
        muzzlePoint.localPosition = 
            new(muzzlePointOffset.localPosition.x * direction , 
            muzzlePoint.localPosition.y, 
            muzzlePoint.localPosition.z);
    }

    private void Fire(GameObject obj)
    {
        //GameObject bullets = Instantiate(bullet, muzzlePoint.position, muzzlePoint.rotation);
        obj.transform.parent = muzzlePoint;
        obj.transform.SetPositionAndRotation(muzzlePoint.position, muzzlePoint.rotation);

        Rigidbody2D rb2D = obj.GetComponent<Rigidbody2D>();

        rb2D.linearVelocity = muzzlePoint.right * bulletSpeed * direction;
        StartCoroutine(BulletRelease(obj));
    }

    private IEnumerator BulletRelease(GameObject obj)
    {
        yield return new WaitForSeconds(3);
        ReleaseObject(obj);
    }

    private GameObject CreateBullet()
    {
        return Instantiate(bullet);
    }

    private void BulletActive(GameObject obj)
    {
        obj.SetActive(true);
    }

    private void BulletDisable(GameObject obj)
    {
        obj.SetActive(false);
    }

    private void BulletDistroy(GameObject obj)
    {
        Destroy(obj);
    }

    public GameObject GetObject()
    {
        GameObject sel = null;

        if (bulletsPool.CountActive >= 400)
        {
            sel = CreateBullet();
            sel.tag = "PoolOver";
        }
        else
        {
            sel = bulletsPool.Get();
        }

        return sel;
    }

    public void ReleaseObject(GameObject obj)
    {
        if (obj.CompareTag("PoolOver"))
        {
            Destroy(obj);
        }
        else
        {
            bulletsPool.Release(obj);
        }
    }

}
