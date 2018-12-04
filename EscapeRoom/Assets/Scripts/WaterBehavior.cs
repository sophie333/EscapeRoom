using UnityEngine;

public class WaterBehavior : MonoBehaviour {

    private Transform m_transform;
    public float timeInterval = 10;
    public float speed;
    private float timer = 0;

    private void Start()
    {
        m_transform = transform;
    }

    private void Update ()
    {
        timer += Time.deltaTime;
        if (timer >= timeInterval)
        {
            m_transform.Translate(0.0f, speed, 0.0f);
            timer = 0;
        }
	}
}
