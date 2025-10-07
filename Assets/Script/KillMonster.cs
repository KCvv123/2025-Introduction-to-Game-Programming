using UnityEngine;

public class KillMonster : MonoBehaviour
{
    public GameObject smoke;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // 是否是踩的這個動作
            CharacterController playerController = other.GetComponent<CharacterController>();
            if (playerController != null && playerController.velocity.y < -1f)
            {
                Debug.Log("kill the monster");
                if (smoke != null)
                {

                    Instantiate(smoke, transform.position, Quaternion.identity);
                }

                // 踩踏的反彈
                Vector3 bounceVelocity = playerController.velocity;
                bounceVelocity.y = 10f; // 反彈力道
                playerController.Move(bounceVelocity * Time.deltaTime);

                Destroy(transform.parent.gameObject);
            }
        }
    }
}
