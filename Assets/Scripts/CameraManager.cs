using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private PlayerMovement Player;
    [SerializeField] private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        cam.transform.position = new Vector3(0, 0, -10);
    }

    // Update is called once per frame
    void Update()
    {
        if((Player.transform.position.x > -3.099999) && (Player.transform.position.x < 3.099999))
        {
            cam.transform.position = new Vector3(Player.transform.position.x, 0, -10);

        }

    }
}
