using UnityEngine;
using UnityEngine.UI;

public class Moveable : MonoBehaviour {
    
    [SerializeField] private ClickObject fpsController;
    [SerializeField] private Text openText;
    [SerializeField] private Text closeText;
    private bool pickedUp;
    private bool opened;
    public bool removed;

    public virtual bool PickedUp
    {
        get
        {
            return this.pickedUp;
        }
        set
        {
            this.pickedUp = value;
        }
    }

    private void Update()
    {
        if (pickedUp)
        {
            removed = true;
            if (!opened)
            {
                openText.gameObject.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    openText.gameObject.SetActive(false);
                    closeText.gameObject.SetActive(true);
                    fpsController.distanceCam = 0.5f;
                    Interact();
                    opened = true;
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    closeText.gameObject.SetActive(false);
                    openText.gameObject.SetActive(true);
                    fpsController.distanceCam = 0.75f;
                    opened = false;
                    StopInteract();
                }
            }
        }/*
        else
        {
            openText.gameObject.SetActive(false);
            closeText.gameObject.SetActive(false);
            fpsController.distanceCam = 0.75f;
            opened = false;
            StopInteract();
            Debug.Log("hibaseSTOPinteract");
        }*/
    }

    protected virtual void Interact()
    {
    }

    public virtual void StopInteract()
    {
    }

    public void DropObject()
    {
        openText.gameObject.SetActive(false);
        closeText.gameObject.SetActive(false);
        fpsController.distanceCam = 0.75f;
        opened = false;
        StopInteract();
    }
}
