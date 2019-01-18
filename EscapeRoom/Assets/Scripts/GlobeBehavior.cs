using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GlobeBehavior : MonoBehaviour
{
    [SerializeField] private ClickObject player;
    [SerializeField] private GameObject text;
    [SerializeField] private GameObject display;
    [SerializeField] private Material code;
    private Transform m_transform;
    private int counter = 0;
    [SerializeField] private Text openText;
    [SerializeField] private Text closeText;

    private void Start()
    {
        m_transform = transform;
    }

    private void OnTriggerStay(Collider other)
    {
        ElementBehavior element = other.GetComponent<ElementBehavior>();

        if (element && !element.GetUsed())
        {
            text.SetActive(true);

            if (Input.GetKeyDown(KeyCode.R))
            {
                element.audio.Play();
                element.SetUsed(true);
                text.SetActive(false);
                player.Drop();
                element.GetComponent<Rigidbody>().isKinematic = true;
                element.transform.position = Vector3.Lerp(element.transform.position, m_transform.position, 1.0f);
                counter++;
                StartCoroutine(WaitAndReveal(5.0f, element));
                if (counter == 4)
                {
                    Debug.Log(counter);
                    display.GetComponent<Renderer>().material = code;
                }
                openText.gameObject.SetActive(false);
                closeText.gameObject.SetActive(false);
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
