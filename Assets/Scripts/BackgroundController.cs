using UnityEngine;

public class BackgroundController : MonoBehaviour
{

    private float startPosition;
    private float length;
    public GameObject camera;
    public float parallax;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPosition = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // 0 has it move with the camera, 1 wont move, 0.5 is half speed
        float distance = camera.transform.position.x * parallax;
        float move = camera.transform.position.x * (1-parallax);

        transform.position = new Vector3(startPosition+distance, transform.position.y, transform.position.z);

        if (move > startPosition + length) {
            startPosition += length;
        } else if (move < startPosition - length) {
            startPosition -= length;
        }
    }
}
