using UnityEngine;
using System.Collections;

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
                element.audio.Play();
                text.SetActive(false);
                player.Drop();
                element.GetComponent<Rigidbody>().isKinematic = true;
                element.transform.position = Vector3.Lerp(element.transform.position, m_transform.position, 1.0f);
                counter++;
                StartCoroutine(WaitAndReveal(5.0f, element));
                if (counter == 4)
                {
                    Debug.Log(counter);
                    blankCode.SetActive(false);
                }
            }
        }
    }

    IEnumerator WaitAndReveal(float waitTime, ElementBehavior element)
    {
        yield return new WaitForSeconds(waitTime);
        Destroy(element.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        text.SetActive(false);
    }
}
