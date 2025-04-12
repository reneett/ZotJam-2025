using UnityEngine;

public class Umbrella : MonoBehaviour
{
    /*
    >>only need different modifiers
    bouncy umbrella : double jump height
    old umbrella : half jump height
    horizontal: bounce at an angle

    >>need access to bool to determine availability
    collapsing : collapses/reopens on each jump

    >>on collision, destroy
    disappearing : bounce once, then destroyed

    >>transform
    moving : umbrella moves side to side
    */

    [SerializeField] public float modifier;
    [SerializeField] public PlayerMovement player;

    [Tooltip("How fast the moving umbrella will move side to side.")]
    [SerializeField] private float movementSpeed;
    [Tooltip("Set true for collapsing/reopening umbrella.")]
    [SerializeField] private bool isCollapsible;
    [Tooltip("Set true for disappearing umbrella.")]
    [SerializeField] private bool isDisappearing;

    public bool jumpedOn = false;
    private Rigidbody2D umbrellaBody;
    private bool umbrellaOpen = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        umbrellaBody = GetComponent<Rigidbody2D>();
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
        if (movementSpeed > 0) {
            //nothing rn
        }
    }

    private void changeUmbrella(bool open) {
        //closes or opens the umbrella, false: close umbrella, true: open umbrella
        if (open) {
            umbrellaBody.simulated = true;
            transform.GetComponent<Renderer>().enabled = true;
        } else {
            umbrellaBody.simulated = false;
            transform.GetComponent<Renderer>().enabled = false;
        }
    }


}
