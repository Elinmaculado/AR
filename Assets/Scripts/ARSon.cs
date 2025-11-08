using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class ARSon : ARObject
{
    [SerializeField] TextMeshProUGUI display;
    [SerializeField] Transform mainCamera;
    [SerializeField] Renderer renderer;
    [SerializeField] Animator animator;

    [SerializeField] InputAction change;
    string otherTag;

    private List<string> tagOptions = new List<string> { "Piedra", "Papel", "Tijera" };
    private int currentTagIndex = 0;


    void Awake()
    {
        change.Enable();
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
                //renderer.material.color = Color.red;
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
        if (change.WasPressedThisFrame())
        {
            ChangeTag();
        }
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
            renderer.material.color = Color.gray;
            result = "Empate";
        }
        else if (
            (tag == "Piedra" && otherTag == "Tijera") ||
            (tag == "Papel" && otherTag == "Piedra") ||
            (tag == "Tijera" && otherTag == "Papel"))
        {
            renderer.material.color = Color.green;
            result = "Ganaste";
        }
        else
        {
            renderer.material.color = Color.red;
            result = "Perdiste";
        }

        display.text = result;

    }
    
    public void ChangeTag()
    {
        currentTagIndex = (currentTagIndex + 1) % tagOptions.Count;
        
        tag = tagOptions[currentTagIndex];
        gameObject.tag = tag;
        
        display.text = tag;
    }

}