using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class ProjectileData : MonoBehaviour
{
    public int Id;
    public string ProjectileName;
    public string Damage;
    public Sprite Sprite;
    public BoxCollider2D collider;
    public Rigidbody2D rb;

    private void Awake()
    {
        collider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();

    }
}
