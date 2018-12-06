using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class BoxInteraction : MonoBehaviour
{
    [SerializeField] private GameObject text;
    private Transform m_transform;
    private Vector3 playerPos;
    private bool isInteracting;

    private void Start()
    {
        m_transform = transform;
    }

    private void OnTriggerStay(Collider other)
    {
        FirstPersonController player = other.GetComponent<FirstPersonController>();

        if (player)
        {
            text.SetActive(true);
            
            if (!isInteracting)
            {
                playerPos = player.transform.position;
                if (Input.GetKeyDown(KeyCode.R))
                {
                    isInteracting = true;
                    text.SetActive(false);
                    player.GetComponent<FirstPersonController>().enabled = false;
                    player.transform.position = new Vector3(0.113f, 0.537f, -1.76f);
                    player.transform.rotation = Quaternion.Euler(0, 0, 0);
                    player.transform.GetChild(0).gameObject.transform.rotation = Quaternion.Euler(90, -90, 0);
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.R))
                {
                    isInteracting = false;
                    text.SetActive(true);
                    player.transform.position = playerPos;
                    player.transform.eulerAngles = new Vector3(0, 0, 0);
                    player.GetComponent<FirstPersonController>().enabled = true;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        text.SetActive(false);
    }
}
