using TMPro;
using UnityEngine;

public class ARSon : ARObject
{
    [SerializeField] TextMeshProUGUI display;
    [SerializeField] Transform mainCamera;
    [SerializeField] Renderer renderer;
    [SerializeField] Animator animator;

    public string otherTag;

    void Awake()
    {
        display.text = tag;
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
                renderer.material.color = Color.red;
                animator.SetBool("IsDancing", true);
                CheckTag();
                break;
            case State.Off:
                display.text = tag;
                renderer.material.color = Color.blue; 
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
    
    public void setOtherTag(string tag)
    {
        otherTag = tag;
        Debug.Log(otherTag);
    }

    void CheckTag()
    {
        string result = "";

        if (tag == otherTag)
        {
            result = "Empate";
        }
        else if (
            (tag == "Piedra" && otherTag == "Tijera") ||
            (tag == "Papel" && otherTag == "Piedra") ||
            (tag == "Tijera" && otherTag == "Papel"))
        {
            result = "Ganaste";
        }
        else
        {
            result = "Perdiste";
        }

        display.text = result;

    }
}