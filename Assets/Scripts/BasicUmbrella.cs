using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Umbrella : MonoBehaviour
{

    [SerializeField] public float modifier;
    [SerializeField] public PlayerMovement player;

    [Tooltip("Set true for collapsing/reopening umbrella.")]
    public bool isCollapsible;
    [Tooltip("Set true for disappearing umbrella.")]
    public bool isDisappearing;

    public bool jumpedOn = false;
    private Rigidbody2D umbrellaBody;
    private SpriteRenderer spriteRenderer;
    public Sprite openUmbrella;
    public Sprite closedUmbrella;
    private bool umbrellaOpen = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        umbrellaBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDisappearing && jumpedOn) {
            changeUmbrella(false);
        }
        if (isCollapsible && player.onUmbrella) {
            umbrellaOpen = !umbrellaOpen;
            changeUmbrella(umbrellaOpen);
        }
    }

    private void changeUmbrella(bool open) {
        //closes or opens the umbrella, false: close umbrella, true: open umbrella
        if (open) {
            umbrellaBody.simulated = true;
            spriteRenderer.enabled = true;
            spriteRenderer.sprite = openUmbrella;
        } else {
            umbrellaBody.simulated = false;
            if (closedUmbrella == null) {
                spriteRenderer.enabled = false;
            } else {
                spriteRenderer.sprite = closedUmbrella;
            }
        }
    }


}
