using UnityEngine;

public class ElementBehavior : Moveable {

    public AudioSource audio;
    private bool used;

    public void SetUsed(bool _used)
    {
        used = _used;
    }

    public bool GetUsed()
    {
        return used;
    }
}
