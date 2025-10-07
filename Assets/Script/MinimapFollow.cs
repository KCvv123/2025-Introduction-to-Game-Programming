using UnityEngine;

public class MinimapFollow : MonoBehaviour
{
    public Transform playerTransform;
    private Vector3 offs;
    void Start()
    {
        if (playerTransform != null)
        {
            offs = transform.position - playerTransform.position;
        }
    }

    void LateUpdate()
    {
        if (playerTransform != null)
        {
            Vector3 newPosition = playerTransform.position + offs;
            transform.position = newPosition;
        }
    }   
}
