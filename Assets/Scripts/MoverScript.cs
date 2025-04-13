using UnityEngine;

public class moverScript : MonoBehaviour
{
    [Tooltip("How fast the object will move.")]
    [SerializeField] public float movementSpeed;
    [SerializeField] public bool isFlippable;

    public GameObject movingThing;
    private SpriteRenderer spriteRenderer;
    public Transform startPosition;
    public Transform endPosition;
    private int direction = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = movingThing.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Transform target = startPosition;
        if (direction == 1) {
            movingThing.transform.position = Vector2.MoveTowards(movingThing.transform.position, startPosition.position, movementSpeed*Time.deltaTime);
        } else {
            target = endPosition;
            movingThing.transform.position = Vector2.MoveTowards(movingThing.transform.position, endPosition.position, movementSpeed*Time.deltaTime);
        }
            
        if (Vector2.Distance(movingThing.transform.position, target.position) < 0.01f) {
            direction *= -1;
            if (isFlippable) {
                spriteRenderer.flipX = direction == 1;
            }
        }
    
    }
}
