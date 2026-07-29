using UnityEngine;
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class ProjectileData : MonoBehaviour
{
    public int Id;
    public string ProjectileName;
    public int Damage;
    public SpriteRenderer sr;
    public Sprite Sprite;
    public new BoxCollider2D collider;
    public Rigidbody2D rb;
    public Collider2D mainCollider;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        collider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start() 
    {
        sr.sprite = Sprite;
        sr.sortingLayerName = "Projectile";
        collider.size = sr.sprite.bounds.size;
        collider.offset = sr.sprite.bounds.center;
        collider.isTrigger = true;
        rb.gravityScale = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!gameObject.activeSelf) return;

        if (collision.TryGetComponent(out IDamageable obj) && !collision.CompareTag("AttackArea"))
        {
            obj.TakeDamage(Damage);
            gameObject.SetActive(false);
        }
            
        

    }
}
