using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerMoveScenes34 : MonoBehaviour
{
    //for player movement and rotation
    private Rigidbody rb;
    private Collider playerCollider;
    private Quaternion parentRot;
    private Vector3 up;
    private Vector3 moveVector;
    [SerializeField] private float maxMove;
    [SerializeField] private float sensitivity;
    private bool grounded;
    [SerializeField] private LayerMask groundLayer;
    private float depth;


    //for player anim
    [SerializeField] private Animator anim;

    //for touch tracking
    private HashSet<int> untrackedFingers;

    //for keeping track of score
    public float time;
    public int score;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text timeText;
    
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerCollider = GetComponent<Collider>();
        untrackedFingers = new HashSet<int>();

        score = PlayerPrefs.GetInt("score");
        time = PlayerPrefs.GetFloat("time"); 
        ScoreChange(0);

        depth = playerCollider.bounds.extents.y + 0.1f;
    }

    void Update()
    {
        time += Time.deltaTime;
        timeText.text = ((int) Mathf.Floor(time)).ToString();
    }

    void FixedUpdate()
    {
        //keep track of which touches are rotating the player
        foreach (Touch _touch in TouchScreenInputWrapper.touches)
        {            
            if (_touch.phase == UnityEngine.TouchPhase.Began && IsPointerOverUIObject())
            {
                untrackedFingers.Add(_touch.fingerId);
            }
            else if (_touch.phase == UnityEngine.TouchPhase.Moved && !IsPointerOverUIObject() && !untrackedFingers.Contains(_touch.fingerId))
            {                
                Vector3 fingerRot = _touch.deltaPosition.x * Vector3.up * sensitivity;
                transform.Rotate(fingerRot);
            }
            else if (_touch.phase == UnityEngine.TouchPhase.Canceled || _touch.phase == UnityEngine.TouchPhase.Ended)
            {
                if (untrackedFingers.Contains(_touch.fingerId))
                {
                    untrackedFingers.Remove(_touch.fingerId);
                }
            }
        }
        
        //move the player according to input system
        rb.velocity += moveVector * Time.deltaTime * maxMove;
        
        //track horizontal velocity to change the player animation
        Vector2 horizontalVelocity = new Vector2(rb.velocity.x, rb.velocity.z);
        if (horizontalVelocity.magnitude > 0.1f)
        {
            anim.SetTrigger("walk");
        }
        else
        {
            anim.SetTrigger("idle");
        }

        //checking if grounded
        RaycastHit hit;
        grounded = Physics.Raycast(transform.position, Vector3.down, out hit, depth, groundLayer);
        if (grounded && hit.collider.CompareTag("Ground"))
        {
            //if grounded, rotate the player so they're perpendicular to the ground
            up = hit.normal;
            Quaternion newRot = Quaternion.LookRotation(transform.forward, up);
            rb.rotation = Quaternion.Slerp(transform.rotation, newRot, Time.deltaTime * 2f);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            ScoreChange(10);
            Debug.Log("Score change");
        }
        else if (other.gameObject.CompareTag("Ground"))
        {
            RaycastHit hit;
            //check that player is actually on the ground
            if (Physics.Raycast(transform.position, Vector3.down, out hit, depth, groundLayer) && hit.collider.gameObject.name == other.gameObject.name)
            {
                //change parent to accommodate rotating platforms
                transform.parent = other.transform;
            }        
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //for catching the player when they fall
        if (other.gameObject.CompareTag("Wall"))
        {
            ScoreChange(10);
            Debug.Log("Score change");
        }
    }

    public void OnJump()
    {
        if (grounded)
        {
            rb.AddForce(10f * Vector3.up, ForceMode.Impulse);
            anim.SetTrigger("jump");
        }
    }

    // for checking if the touch is over an UI element
    //if yes, we shouldn't rotate the player
    private bool IsPointerOverUIObject() 
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    //get movement vectors, only in x and z direction
    public void OnMove(InputAction.CallbackContext context){
        Vector2 direction = context.ReadValue<Vector2>();

        moveVector = transform.right * direction.x + transform.forward * direction.y;
    }

    void ScoreChange(int change)
    {
        score -= change;
        scoreText.text = score.ToString();
    }

}
