using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Umbrella : MonoBehaviour
{

    [SerializeField] public float modifier;
    [SerializeField] public PlayerMovement player;

    [Tooltip("Set true for collapsing/reopening umbrella.")]
    public bool isCollapsible;
    public bool startsClosed;
    [Tooltip("Set true for disappearing umbrella.")]
    public bool isDisappearing;

    public bool jumpedOn = false;
    private Rigidbody2D umbrellaBody;
    private SpriteRenderer spriteRenderer;
    public Sprite openUmbrella;
    public Sprite closedUmbrella;
    private bool startingState = true;
    private Vector2 startingPosition;
    private bool umbrellaOpen = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        umbrellaBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (startsClosed) {
            changeUmbrella(false);
            umbrellaOpen = false;
        }
        startingState = umbrellaOpen;
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDisappearing && jumpedOn) {
            changeUmbrella(false);
        }
        if (isCollapsible && player.onUmbrella) {
            Debug.Log("collapsing/uncollapsing");
            umbrellaOpen = !umbrellaOpen;
            changeUmbrella(umbrellaOpen);
        }
    }
    private void changeUmbrella(bool open)
    {
        if (open)
        {
            umbrellaBody.simulated = true;
            spriteRenderer.enabled = true;
            Debug.Log("moving umbrella");
            umbrellaBody.MovePosition(startingPosition);
            spriteRenderer.sprite = openUmbrella;
            jumpedOn = true;
        }
        else
        {
            if (closedUmbrella != null)
            {
                spriteRenderer.sprite = closedUmbrella;
                umbrellaBody.simulated = false;
            }
            else
            {
                Vector2 position = startingPosition;
                position.y -= 20;
                umbrellaBody.MovePosition(position);
            }

            if (isDisappearing)
            {
                spriteRenderer.enabled = false;
            }
        }
    }


    public void resetUmbrella() {
        Debug.Log("resetting umbreller");
        changeUmbrella(startingState);
        jumpedOn = false;
    }


}
