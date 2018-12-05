using UnityEngine;

public class BookBehavior : Moveable
{    
    [SerializeField] private GameObject openedBook;
    [SerializeField] private Material page1;
    [SerializeField] private Material page2;

    private Transform m_transform;

    private void Start()
    {
        m_transform = transform;
    }

    protected override void Interact()
    {
        //transform opened to closed book
        openedBook.transform.position = m_transform.position;
        //parent openedbook
        openedBook.transform.parent = m_transform;
        //set textures
        openedBook.transform.GetChild(0).GetComponent<Renderer>().material = page1;
        openedBook.transform.GetChild(1).GetComponent<Renderer>().material = page2;
        //make current book invisible
        m_transform.GetChild(0).gameObject.SetActive(false);
        //make opened book visible
        openedBook.SetActive(true);
    }

    protected override void StopInteract()
    {
        openedBook.SetActive(false);
        m_transform.GetChild(0).gameObject.SetActive(true);
    }
}
