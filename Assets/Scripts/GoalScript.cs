using UnityEngine;

public class GoalScript : MonoBehaviour
{

    public Sprite flowerBloom;
    public Sprite flowerWilt;
    private SpriteRenderer spriteRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player") {
            spriteRenderer.sprite = flowerBloom;
        }
    }

    private void restart() {
        spriteRenderer.sprite = flowerWilt;
    }
}
