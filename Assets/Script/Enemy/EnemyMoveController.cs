using UnityEngine;
using static Constants;
using SF = UnityEngine.SerializeField;

public class EnemyMoveController : MonoBehaviour
{
    [SF] private EnemyValue eValue;
    [SF] private GameObject player;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public  void Start()
    {
        player = GameObject.FindGameObjectWithTag(PLAYER_TAG);
    }

    private void FixedUpdate()
    {
        if(player != null && gameObject.activeSelf)
        {
            Vector2 moveVector = 
                (new Vector2(player.transform.position.x, player.transform.position.y) 
                - rb.position).normalized;

            rb.linearVelocity = moveVector * eValue.speed;
        }
    }

}
