using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.SceneManagement;

public class GoalTrigger : MonoBehaviour {
    
    [SerializeField] private GameObject text;
    [SerializeField] private GameObject canvas;

    private void OnTriggerEnter(Collider other)
    {
        FirstPersonController player = other.GetComponent<FirstPersonController>();

        if (player)
        {
            text.SetActive(true);
            canvas.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        FirstPersonController player = other.GetComponent<FirstPersonController>();

        if (player)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                RestartGame();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        FirstPersonController player = other.GetComponent<FirstPersonController>();

        if (player)
        {
            text.SetActive(false);
            canvas.SetActive(false);
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Main");
    }
}
