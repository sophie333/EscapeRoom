using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestrictionBehavior : MonoBehaviour
{
    [SerializeField] private ClickObject player;
    [SerializeField] private GameObject text;
    [SerializeField] private GameObject lock1;
    [SerializeField] private Animator anim1;
    [SerializeField] private Animator anim2;
    private Transform m_transform;
    private bool unlocked;

    private void Start()
    {
        m_transform = transform;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Key")
        {
            if (!unlocked)
            {
                text.SetActive(true);

                if (Input.GetKeyDown(KeyCode.R))
                {
                    text.SetActive(false);
                    anim1.Play("OpenDoor1");
                    anim2.Play("OpenDoor2");
                    lock1.GetComponent<Rigidbody>().isKinematic = false;
                    player.Drop();
                    Destroy(other.gameObject);
                    unlocked = true;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        text.SetActive(false);
    }
}
