using UnityEngine;
using UnityEngine.UI;

public class BookBehavior : Moveable
{    
    [SerializeField] private Text openText;
    [SerializeField] private Text closeText;
    [SerializeField] private GameObject openedBook;
    [SerializeField] private Material page1;
    [SerializeField] private Material page2;
    [SerializeField] private ClickObject fpsController;
    private bool opened = false;

    private Transform m_transform;

    private void Start()
    {
        m_transform = transform;
    }

    private void Update()
    {
        if(PickedUp)
        {
            if (!opened) 
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    openText.gameObject.SetActive(false);
                    closeText.gameObject.SetActive(true);
                    //transform opened to closed book
                    openedBook.transform.position = m_transform.position;
                    //parent openedbook
                    openedBook.transform.parent = m_transform;
                    //set textures
                    openedBook.transform.GetChild(0).GetComponent<Renderer>().material = page1;
                    openedBook.transform.GetChild(1).GetComponent<Renderer>().material = page2;
                    //make current book invisible
                    m_transform.GetChild(0).gameObject.SetActive(false);
                    fpsController.distanceCam = 0.5f;
                    //make opened book visible
                    openedBook.SetActive(true);
                    opened = true;
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    closeText.gameObject.SetActive(false);
                    openText.gameObject.SetActive(true);
                    openedBook.SetActive(false);
                    m_transform.GetChild(0).gameObject.SetActive(true);
                    fpsController.distanceCam = 0.75f;
                    opened = false;
                }
            }
        }
        else
        {
            openText.gameObject.SetActive(false);
            openedBook.SetActive(false);
            m_transform.GetChild(0).gameObject.SetActive(true);
            fpsController.distanceCam = 0.75f;
        }
    }
}
