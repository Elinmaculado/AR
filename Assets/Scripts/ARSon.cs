using TMPro;
using UnityEngine;

public class ARSon : ARObject
{
    [SerializeField] TextMeshProUGUI display;
    [SerializeField] Transform mainCamera;
    [SerializeField] Renderer renderer;

    [SerializeField] Animator animator;

    void Awake()
    {
        mainCamera = Camera.main.transform;
        renderer = GetComponent<Renderer>();
        animator = GetComponentInChildren<Animator>();
        //animator.SetBool("IsDancing", true);
    }
    
    protected override void Activate()
    {
        //SetState(State.Off);
    }

    protected override void SetState(State state)
    {
        base.SetState(state);

        switch (state)
        {
            case State.On:
                display.text = "Peleando";
                renderer.material.color = Color.red;
                animator.SetBool("IsDancing", true);
                break;
            case State.Off:
                renderer.material.color = Color.blue;
                display.text = "No pelien";
                animator.SetBool("IsDancing", false);
                break;
        }
    }

    void Update()
    {
        Vector3 lookDirection = display.transform.position - mainCamera.position;
        display.transform.rotation = Quaternion.LookRotation(lookDirection);
        if (!isColliding)
        {
            if (state != State.Off)
            {
                SetState(State.Off);
            }
        }

        //isColliding = false;
    }
}