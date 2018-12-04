using UnityEngine;

public class Moveable : MonoBehaviour {

    private bool pickedUp;

    protected virtual void DoSomething()
    {
    }

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
}
