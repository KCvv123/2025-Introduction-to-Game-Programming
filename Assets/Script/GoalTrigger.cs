using UnityEngine;

public class GoalTrigger : MonoBehaviour
{
    public GameObject victoryUIPanel;
    private GameManager gameManager;

    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(gameManager != null)
            {
                gameManager.isGameActive = false;
            }

            victoryUIPanel.SetActive(true);
            
            Animator playerAnimator = other.GetComponent<Animator>();
            if (playerAnimator != null) playerAnimator.SetTrigger("Win");
        }
    }
}
