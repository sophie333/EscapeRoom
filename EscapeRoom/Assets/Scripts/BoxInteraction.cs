using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class BoxInteraction : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject interactText;
    [SerializeField] private GameObject stopInteractText;
    [SerializeField] private GameObject codeText;
    [SerializeField] private GameObject heartKey;
    [SerializeField] private BookBehavior bookOnTop;
    [SerializeField] private AudioSource buzzSound;
    [SerializeField] private AudioSource unlockSound;
    [SerializeField] private string actualCode = "625";
    private KeyCode[] keycodeArray = { KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5, KeyCode.Alpha6, KeyCode.Alpha7, KeyCode.Alpha8, KeyCode.Alpha9 };
    private Transform m_transform;
    private Vector3 playerPos;
    private bool isInteracting;
    private bool solved;
    private bool notPlayedYet = true;
    private string code = "";
    private int counter = 0;


    private void Start()
    {
        m_transform = transform;
    }

    private void OnTriggerStay(Collider other)
    {
        FirstPersonController player = other.GetComponent<FirstPersonController>();

        if (player)
        {
            if (!isInteracting && !solved && bookOnTop.removed)
            {
                interactText.SetActive(true);
                playerPos = player.transform.position;

                if (Input.GetKeyDown(KeyCode.R))
                {
                    stopInteractText.SetActive(true);
                    isInteracting = true;
                    interactText.SetActive(false);
                    player.GetComponent<FirstPersonController>().enabled = false;
                    //player.transform.position = new Vector3(0.08f, 0.537f, -1.76f);
                    player.transform.position = new Vector3(0.36f, 1.07f, -1.73f);
                    player.transform.rotation = Quaternion.Euler(0, 0, 0);
                    player.transform.GetChild(0).gameObject.transform.rotation = Quaternion.Euler(90, -90, 0);
                }
            }
            else
            {
                EnterCode();

                if (Input.GetKeyDown(KeyCode.F))
                {
                    isInteracting = false;
                    stopInteractText.SetActive(false);
                    player.transform.position = new Vector3(1.35f, 1.02f, -1.8f); //playerPos;
                    player.transform.eulerAngles = new Vector3(0, 0, 0);
                    player.GetComponent<FirstPersonController>().enabled = true;
                    if (!solved)
                    {
                        interactText.SetActive(true);
                    }
                    else
                    {
                        anim.Play("DrawerAnim");
                        StartCoroutine(WaitAndReveal(1.0f));
                    }
                }
            }
        }
    }

    IEnumerator WaitAndReveal(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        heartKey.GetComponent<Rigidbody>().isKinematic = false;
    }

    private void OnTriggerExit(Collider other)
    {
        interactText.SetActive(false);
    }

    private void EnterCode()
    {
        string key = "";
        if (!solved)
        {
            foreach (KeyCode vKey in keycodeArray)
            {
                if (Input.GetKeyDown(vKey))
                {
                    key = vKey.ToString();
                    counter++;
                }
            }
            if (key != "")
            {
                code += key[5];
                codeText.GetComponent<TextMesh>().text = code;
            }
        }

        if (counter >= 3)
        {
            if (code == actualCode)
            {
                if(!unlockSound.isPlaying && notPlayedYet)
                {
                    unlockSound.Play();
                    notPlayedYet = false;
                }
                solved = true;
            }
            else
            {
                buzzSound.Play();
                code = "";
                codeText.GetComponent<TextMesh>().text = "___";
                counter = 0;
            }
        }
    }
}