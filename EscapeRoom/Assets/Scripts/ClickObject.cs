using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickObject : MonoBehaviour
{
    [SerializeField]
    private Light pLight;

    private bool isCarrying = false;
    private GameObject carriedObject;
    private Transform m_transform;

    public float distance;
    public float smooth;

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

            if (Physics.Raycast(ray, out hit, 2.0f))
            {
                if (hit.transform != null)
                {
                    print(hit.transform.gameObject);
                    CheckInteraction(hit.transform.gameObject);
                }
            }
        }

        if (isCarrying)
        {
            carriedObject.transform.position = Vector3.Lerp(carriedObject.transform.position, m_transform.position + m_transform.forward * distance, Time.deltaTime * smooth);
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
                obj.transform.eulerAngles = new Vector3(0, 90, 0);
            }
        }

        if (obj.name == "LightSwitch")
        {
            ToggleLight();
        }
    }

    private void CheckDrop()
    {
        if(Input.GetMouseButton(1))
        {
            isCarrying = false;
            carriedObject.GetComponent<Rigidbody>().isKinematic = false;
            carriedObject = null;
        }
    }

    private void ToggleLight()
    {
        if (pLight.intensity == 0.0f)
        {
            pLight.intensity = 2.0f;
        } 
        else
        {
            pLight.intensity = 0.0f;
        }
    }
}
