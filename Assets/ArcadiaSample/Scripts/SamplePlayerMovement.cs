using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (CircleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class SamplePlayerMovement : MonoBehaviour
{

    private Vector3 direction;
    public float Gravity = -9.8f;
    public float strength = 5f;

     public Transform spawnPoint;

    public int health = 1;
    public SampleGameManager Manager;
    private Rigidbody2D _rigidBody;

    public bool pressingLeft;
    public bool pressingRight;
    public bool _thrusting;
    public float _turnDirection;

    public float thrustSpeed;
    public float turnSpeed;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>(); 
    } 
        
  //Keyboard input mostly
    void Update()
    {
               
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) )
        {
            direction = Vector3.up * strength ;
        } 
    if(Input.touchCount > 0 )
        {
            Touch touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Began)
            {
                direction = Vector3.up * strength;
            }
        }

        direction.y += Gravity * Time.deltaTime;
        transform.position += direction * Time.deltaTime;

    }

    //Give it a little kick when the game starts to catch your attention
    private void OnEnable()
    {
        direction = Vector3.up * strength;
    }

  
    //Hit the wall?
    private void OnTriggerEnter2D(Collider2D collision)
    {
    
        if(collision.gameObject.name != "Score")
        {
            _rigidBody.velocity = Vector3.zero;
            _rigidBody.angularVelocity = 0.0f;

            this.gameObject.SetActive(false);
            Manager.GameOver();
        
        }
        else
        {
            Manager.score++;
        }
       
    }

}
