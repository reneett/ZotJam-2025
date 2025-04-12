using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    [SerializeField] public float jumpHeight;
    [SerializeField] private float speed;
    [SerializeField] public float accelerationRate;

    private Rigidbody2D body;
    private bool onUmbrella = false;
    private Vector2 startPosition;
    public int jumpModifier = 5;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 velocity = body.linearVelocity;

        bool isMovingLeft = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
        bool isMovingRight = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);
        
        if (isMovingLeft)
        {
            velocity.x = Mathf.MoveTowards(body.linearVelocity.x, -speed, accelerationRate * Time.deltaTime);
        }
        if (isMovingRight)
        {
            velocity.x = Mathf.MoveTowards(body.linearVelocity.x, speed, accelerationRate * Time.deltaTime);
        }

        if (onUmbrella) {
            velocity.y = jumpHeight*jumpModifier;
            onUmbrella = false;
        }

        body.linearVelocity = velocity;
       
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Umbrella") {
            onUmbrella = true;
            jumpModifier -= collision.gameObject.GetComponent<Umbrella>().modifier;
            if (jumpModifier <= 0) {
                respawn();
            }
        }
    }

    private void respawn() {
        transform.position = startPosition;
        jumpModifier = 5;
    }

}
