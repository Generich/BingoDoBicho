using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    Vector2 _direction;
    public Controls PlayerControls;
    public Rigidbody2D rb;
    public bool playerInteract;
    public float _speed = 5f;
    public bool speak;
    public int howManyLeft = 5;
    public bool isPause = false;
    private bool facinRight = true;
    private float lastPositionX;

    private InputAction move;
    private InputAction interact;
    private InputAction dialogue;

    [SerializeField] private GameObject dialogueSystem;
    [SerializeField] private CollectedItems collect;
    [SerializeField] private Timer timer;


    private void Awake()
    {
        PlayerControls = new Controls();
    }

    public void OnEnable()
    {
        move = PlayerControls.Player.Move;
        move.Enable();


        interact = PlayerControls.Player.Interact;
        interact.Enable();
        //interact.performed += PlayerInteraction;
        interact.started += PlayerInteraction => {
            playerInteract = true;
            Debug.Log("playerInteract: " + playerInteract);     
            
        };

        interact.canceled += PlayerInteraction => {
            playerInteract = false;
            Debug.Log("playerInteract: " + playerInteract);
        };

        dialogue = PlayerControls.Player.Dialogues;
        dialogue.Enable();
        dialogue.performed += PlayerDialogue;
    }
    
    public void OnPause()
    {
        move.Disable();
        dialogue.Disable();
    }

    public void OffPause()
    {
        move.Enable();
        dialogue.Enable();
    }

    public void disableUntilDeliver(){
        interact.Disable();
    }

    public void enableInteract(){
        interact.Enable();
    }

    // Checa se a quantidade de animais capturados � igual a quantidade de animais totais a serem capturados
    public bool checkBingoCard(){
        return collect.totalAnimals == howManyLeft;
    }

    // Update is called once per frame
    void Update()
    {
        _direction = move.ReadValue<Vector2>();
        if (!speak)
	        move.Enable();
	    else
	        move.Disable();

        if (!isPause)
            OffPause();
        else
            OnPause();

    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(_direction.x * _speed, _direction.y * _speed);

        if (transform.position.x > lastPositionX && !facinRight)
            Flip();

        if (transform.position.x < lastPositionX && facinRight)
            Flip();

        lastPositionX = transform.position.x;
    }

    public void PlayerInteraction(InputAction.CallbackContext context)
    {
        Debug.Log("Interagiu!");
    }

    public void PlayerDialogue(InputAction.CallbackContext context){
        dialogueSystem.transform.GetComponent<DialogueSystem>().NextDialogue();
    }

    public void Pause(InputAction.CallbackContext context){
        if (context.performed){
            timer.Pause();
            isPause = !isPause;
        }
    }

    private void Flip() {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;

        facinRight = !facinRight;
        transform.localScale = theScale;
    }
}
