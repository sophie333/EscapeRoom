using UnityEngine;

public class BookBehavior : Moveable
{
    [SerializeField] private ClickObject player;
    [SerializeField] private GameObject openedBook;
    [SerializeField] private Material page1;

    private Transform m_transform;

    private void Start()
    {
        m_transform = transform;
    }

    protected override void Interact()
    {
        //transform opened to closed book
        openedBook.transform.position = m_transform.position;
        openedBook.transform.LookAt(player.transform);
        //parent openedbook
        openedBook.transform.parent = m_transform;
        //set textures
        openedBook.transform.GetComponent<Renderer>().material = page1;
        //make current book invisible
        m_transform.GetChild(0).gameObject.SetActive(false);
        //make opened book visible
        openedBook.SetActive(true);
    }

    public override void StopInteract()
    {
        base.StopInteract();
        openedBook.SetActive(false);
        m_transform.GetChild(0).gameObject.SetActive(true);
    }
}
