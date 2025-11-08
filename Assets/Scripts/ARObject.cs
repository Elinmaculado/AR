using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class ARObject : MonoBehaviour
{
    public List<ARObject> interactables = new List<ARObject>();

    public bool isColliding;

    
    
    public enum State
    {
        On,
        Off
    }
    protected State state;

    void Start()
    {
        SetState(State.Off);
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("CollisionEnter");
        if (other.TryGetComponent<ARObject>(out ARObject aRObject))
        {
            if (this is ARSon son)
                son.setOtherTag(aRObject.gameObject.tag);
        SetState(State.On);
        }
    }

    void OnTriggerStay(Collider other)
    {
        // Debug.Log("CollisionStay");
        isColliding = true;

        if(state != State.On)
        {
            SetState(State.Off);
        }
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("CollisionExit");
        if (other.TryGetComponent<ARObject>(out ARObject aRObject))
        {
            SetState(State.Off);
        }
        isColliding = false;
    }

    protected abstract void Activate();
    protected virtual void SetState(State state){}
}
