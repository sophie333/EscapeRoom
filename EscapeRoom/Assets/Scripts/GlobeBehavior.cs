using UnityEngine;

public class GlobeBehavior : MonoBehaviour
{
    [SerializeField] private ClickObject player;
    [SerializeField] private GameObject text;
    [SerializeField] private GameObject blankCode;
    private Transform m_transform;
    private int counter = 0;

    private void Start()
    {
        m_transform = transform;
    }

    private void OnTriggerStay(Collider other)
    {
        ElementBehavior element = other.GetComponent<ElementBehavior>();

        if (element)
        {
            text.SetActive(true);

            if (Input.GetKeyDown(KeyCode.R))
            {
                player.Drop();
                element.GetComponent<Rigidbody>().isKinematic = true;
                element.transform.position = Vector3.Lerp(element.transform.position, m_transform.position, 1.0f);
                Destroy(element.gameObject);
                counter++;
                if (counter == 4)
                {
                    blankCode.SetActive(false);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        text.SetActive(false);
    }
}
