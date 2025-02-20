using System. Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    public GameManager gameManager;
    public AudioSource CoinAudioSource;
    public float walkSpeed = 8f; 
    public float jumpSpeed = 20f;
    public HudManager hud;

    Rigidbody rb;

    Collider coll;

    bool pressedJump = false;

 
    void Start() {
        //get the rigid body component for later use 
        rb = GetComponent<Rigidbody>();
        coll = GetComponent<Collider>();
        hud.Refresh();
    }
    
    // Update is called once per frame
    void Update() {
     // Handle player walking
        WalkHandler();

        JumpHandler();
    }
    // Make the player walk according to user input
    void WalkHandler(){
        // Set x and z velocities to zero
        rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);

        // Distance ( speed - distance / time --> distance - speed * time)
        float distance = walkSpeed * Time.deltaTime;
        // Input on x ("Horizontal")
        float hAxis = Input.GetAxis("Horizontal");
        // Input on z ("Vertical")
        float vAxis = Input.GetAxis("Vertical");
        // Movement vector
        Vector3 movement =  new Vector3(hAxis * distance, 0f, vAxis * distance);
        // Current position
        Vector3 currPosition = transform.position;
        // New position
        Vector3 newPosition = currPosition + movement;
        // Move the rigid body
        rb.MovePosition(newPosition);
    }

    void JumpHandler(){
        float jAxis = Input.GetAxis("Jump");
        bool isGrounded = CheckGrounded();

        if(jAxis > 0f){
            if(!pressedJump && isGrounded){
                pressedJump = true;
                Vector3 jumpVector = new Vector3(0f, jumpSpeed, 0f);
                rb.linearVelocity = rb.linearVelocity + jumpVector;
            }
        } 
        else {
            pressedJump = false;
        }
    }

    bool CheckGrounded(){
        float sizeX = coll.bounds.size.x;
        float sizeZ = coll.bounds.size.z;
        float sizeY = coll.bounds.size.y;
        
        Vector3 corner1 = transform.position + new Vector3(sizeX / 2, -sizeY / 2 + 0.01f, sizeZ / 2); 
        Vector3 corner2 = transform.position + new Vector3(-sizeX / 2, -sizeY / 2 + 0.01f, sizeZ / 2); 
        Vector3 corner3 = transform.position + new Vector3(sizeX / 2, -sizeY / 2 + 0.01f, -sizeZ / 2); 
        Vector3 corner4 = transform.position + new Vector3(-sizeX / 2, -sizeY / 2 + 0.01f, -sizeZ / 2); 

        bool grounded1 = Physics.Raycast(corner1, new Vector3(0,-1,0), 0.01f);
        bool grounded2 = Physics.Raycast(corner2, new Vector3(0,-1,0), 0.01f);
        bool grounded3 = Physics.Raycast(corner3, new Vector3(0,-1,0), 0.01f);
        bool grounded4 = Physics.Raycast(corner4, new Vector3(0,-1,0), 0.01f);

        return (grounded1 || grounded2 || grounded3 || grounded4);
    }

    void OnTriggerEnter(Collider collider){
        if(collider.gameObject.tag == "Coin"){
            print("Grabbing coin...");

            GameManager.instance.IncreaseScore(1);

            hud.Refresh();

            CoinAudioSource.Play();

            Destroy(collider.gameObject);
        }
        else if(collider.gameObject.tag == "Enemy"){
            print("Game over");
            SceneManager.LoadScene("Game Over");
        }
        else if(collider.gameObject.tag == "Goal"){
            print("next level");
            GameManager.instance.IncreaseLevel();
        }
    }
}
