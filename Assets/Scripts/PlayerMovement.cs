using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    
    [SerializeField] public float jumpHeight;
    [SerializeField] private float speed;
    [SerializeField] public float accelerationRate;
    [SerializeField] public float gravityBase;
    [SerializeField] public float gravityScale;

    private Rigidbody2D body;
    private SpriteRenderer spriteRenderer;
    public bool onUmbrella;
    private Vector2 startPosition;
    public float jumpModifier = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        startPosition = transform.position;
        onUmbrella = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 velocity = body.linearVelocity;
        gravityBase = 1;

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
            spriteRenderer.flipX = true;
        } else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
            spriteRenderer.flipX = false;
        }

        velocity.x = Input.GetAxis("Horizontal")*speed*accelerationRate;
        if(velocity.y < 0) {
            gravityBase = gravityBase * gravityScale;
            body.gravityScale = gravityBase;
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
            jumpModifier = collision.gameObject.GetComponent<Umbrella>().modifier;
            collision.gameObject.GetComponent<Umbrella>().jumpedOn = true;
        }
        if (collision.gameObject.tag == "Floor" || collision.gameObject.tag == "Crow") {
            respawn();
        }
        if (collision.gameObject.tag == "Goal") {
            Debug.Log("yaaaay you did it");
            respawn();
        }
    }

    private void respawn() {
        onUmbrella = false;
        body.linearVelocity = Vector2.zero;
        body.angularVelocity = 0;
        body.MovePosition(startPosition);
        jumpModifier = 1;
    }

}
