using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float distance = 5.0f;
    public float mouseSensitivity = 2.0f;
    public float pitchMin = -40f;
    public float pitchMax = 80f;
    public LayerMask collisionLayers; 

    private float yaw = 0.0f;
    private float pitch = 0.0f;
    
    private GameManager gameManager;

    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
    }

    void LateUpdate()
    {
        if (gameManager != null && !gameManager.isGameActive)
        {
            return; 
        }

        if (player == null) return;

        yaw += mouseSensitivity * Input.GetAxis("Mouse X");
        pitch -= mouseSensitivity * Input.GetAxis("Mouse Y");
        pitch = Mathf.Clamp(pitch, pitchMin, pitchMax);

        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
        Vector3 idealPosition = player.position - (rotation * Vector3.forward * distance);

        RaycastHit hit;
        if (Physics.Linecast(player.position, idealPosition, out hit, collisionLayers))
        {
            transform.position = hit.point;
        }
        else
        {
            transform.position = idealPosition;
        }

        transform.LookAt(player);
    }
}
