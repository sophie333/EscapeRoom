using UnityEngine;

public class ClickObject : MonoBehaviour
{
    [SerializeField] private Light pLight;
    [SerializeField] private Light sLight;
    [SerializeField] private AudioSource audio;
    [SerializeField] private GameObject lightSwitch;

    public bool isCarrying = false;
    private GameObject carriedObject;
    private Moveable movableObj;
    private Transform m_transform;

    public float distanceCam = 0.75f;
    public float distanceRay = 2.0f;
    public float smooth = 7.0f;

    private void Start()
    {
        m_transform = transform;
    }

    private void Update ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, distanceRay))
            {
                if (hit.transform != null)
                {
                    CheckInteraction(hit.transform.gameObject);
                }
            }
        }
        if (isCarrying)
        {
            carriedObject.transform.position = Vector3.Lerp(carriedObject.transform.position, m_transform.position + m_transform.forward * distanceCam, Time.deltaTime * smooth);
            carriedObject.transform.LookAt(m_transform);
            CheckDrop();
        }
	}

    private void CheckInteraction(GameObject obj)
    {
        Moveable moveable = obj.GetComponent<Moveable>();
        
        if (moveable)
        {
            if (!isCarrying)
            {
                isCarrying = true;
                carriedObject = obj;
                obj.GetComponent<Rigidbody>().isKinematic = true;
                moveable.PickedUp = true;
                movableObj = moveable;
            }
        }

        if (obj.name == "LightSwitch")
        {
            ToggleLight();
            audio.Play();
        }
    }

    private void CheckDrop()
    {
        if(Input.GetMouseButton(1))
        {
            movableObj.DropObject();
            isCarrying = false;
            carriedObject.GetComponent<Rigidbody>().isKinematic = false;
            carriedObject = null;
            movableObj.PickedUp = false;
            movableObj = null;
        }
    }

    public void Drop()
    {
        isCarrying = false;
        carriedObject = null;
        movableObj = null;
    }

    private void ToggleLight()
    {
        if (pLight.intensity == 0.0f)
        {
            pLight.intensity = 2.0f;
            sLight.intensity = 2.0f;
            lightSwitch.transform.eulerAngles = new Vector3(0, 0, 110);
        } 
        else
        {
            pLight.intensity = 0.0f;
            sLight.intensity = 0.0f;
            lightSwitch.transform.eulerAngles = new Vector3(0, 0, 45);
        }
    }
}
