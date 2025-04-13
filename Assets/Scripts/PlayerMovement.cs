using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour
{
    
    [SerializeField] public float jumpHeight;
    [SerializeField] private float speed;
    [SerializeField] public float accelerationRate;
    [SerializeField] public float gravityBase;
    [SerializeField] public float gravityScale;

    private Rigidbody2D body;
    //private SpriteRenderer spriteRenderer;
    private Animator animator;
    public bool onUmbrella;
    private Vector2 startPosition;
    public float jumpModifier = 1;
    private bool facingRight = true;
    public bool levelClear = false;
    public bool died = false;
    private List<Umbrella> umbrellas;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        umbrellas = new List<Umbrella>();
        //spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        startPosition = transform.position;
        onUmbrella = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!levelClear) {
            died = false;
            //only move if the level has not been cleared
            Vector2 velocity = body.linearVelocity;
            gravityBase = 1;

            if ((Input.GetKey(KeyCode.A) && facingRight) || (Input.GetKey(KeyCode.D) && !facingRight)) {
                facingRight = !facingRight;
                transform.localScale = new Vector3(transform.localScale.x *-1, transform.localScale.y, transform.localScale.z);
            } 

            velocity.x = Input.GetAxis("Horizontal")*speed*accelerationRate;
            if(velocity.y < 0) {
                gravityBase = gravityBase * gravityScale;
                body.gravityScale = gravityBase;
                animator.Play("raindropfloat");
            } 

            if (onUmbrella) {
                velocity.y = jumpHeight*jumpModifier;
                onUmbrella = false;
                animator.Play("raindropjump");
            }

            body.linearVelocity = velocity;
        }
       
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Umbrella") {
            onUmbrella = true;
            jumpModifier = collision.gameObject.GetComponent<Umbrella>().modifier;
            collision.gameObject.GetComponent<Umbrella>().jumpedOn = true;
            if (collision.gameObject.GetComponent<Umbrella>().isDisappearing) {
                umbrellas.Add(collision.gameObject.GetComponent<Umbrella>());
            }
        }
        if (collision.gameObject.tag == "Floor" || collision.gameObject.tag == "Crow") {
            resetLevel();
            respawn();
        }
        if (collision.gameObject.tag == "Goal") {
            Debug.Log("yaaaay you did it");
            levelClear = true;
        }
    }

    public void respawn() {
        died = true;
        onUmbrella = false;
        body.linearVelocity = Vector2.zero;
        body.angularVelocity = 0;
        if (!facingRight) {
            facingRight = !facingRight;
            transform.localScale = new Vector3(transform.localScale.x *-1, transform.localScale.y, transform.localScale.z);
        }
        jumpModifier = 1;
        body.MovePosition(startPosition);
    }

    public void resetLevel() {
        Debug.Log(umbrellas.Count);
        levelClear = false;
        foreach (Umbrella u in umbrellas) {
            u.resetUmbrella();
        }
        umbrellas.Clear();
    }

    public bool isDead() {
        return died;
    }

}
