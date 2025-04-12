using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    [SerializeField] public float jumpHeight;
    [SerializeField] private float speed;

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
        body.linearVelocity = new Vector2(Input.GetAxis("Horizontal")*speed, body.linearVelocity.y);
        if (onUmbrella) {
            bounce();
        }
    }

    private void bounce() {
        body.linearVelocity = new Vector2(body.linearVelocity.x, jumpHeight*jumpModifier);
        onUmbrella = false;
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
