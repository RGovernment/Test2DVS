using UnityEngine;
using UnityEngine.UIElements;
using SF = UnityEngine.SerializeField;
public class PlayerMoveController : MonoBehaviour
{
    [SF, Range(5, 50)] private int speed;

    private BoxCollider2D col;
    private Rigidbody2D rb;
    private Vector2 moveVector = Vector2.zero;

    private void Awake()
    {
        col = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {

    }

    private void FixedUpdate()
    {
        rb.linearVelocity = moveVector * speed;
    }

    private void Update()
    {
        float moveY = Input.GetAxisRaw("Horizontal");
        float moveX = Input.GetAxisRaw("Vertical");

        moveVector = new Vector2(moveY, moveX).normalized;
    }
}
