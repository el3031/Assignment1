using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerMove : MonoBehaviour
{
    //instance variables needed for movement and rotation tracking
    private Rigidbody rb;
    private Collider playerCollider;
    private bool grounded;
    [SerializeField] private LayerMask groundLayer;
    private Quaternion newAngle = new Quaternion(0f, 0f, 0f, 0f);
    [SerializeField] private float maxMove;
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravityScale;
    private float depth;


    //instance variables needed for projectile shooting
    [SerializeField] private GameObject butt;

    //scoretracking features
    public float time;
    public int score;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text timeText;

    //animation stuff
    [SerializeField] private Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerCollider = GetComponent<Collider>();
        depth = playerCollider.bounds.extents.y + 0.1f;


        time = PlayerPrefs.GetFloat("time", 0f);
        score = PlayerPrefs.GetInt("score", 1000);
        ScoreChange(0);
    }

    void Update()
    {
        time += Time.deltaTime;
        timeText.text = ((int) Mathf.Floor(time)).ToString();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0) || Input.touchCount > 0)
        {
            //for testing in editor
            #if UNITY_EDITOR
                Vector3 mousePos = Input.mousePosition;
                Ray onScreenPoint = Camera.main.ScreenPointToRay(mousePos);
            #elif UNITY_IPHONE
                Touch touch = Input.GetTouch(0);
                Vector3 touchPos = touch.position;
                Ray onScreenPoint = Camera.main.ScreenPointToRay(touchPos);
            #endif

            RaycastHit hit;            
            if (Physics.Raycast(onScreenPoint.origin, onScreenPoint.direction, out hit) && grounded && !IsPointerOverUIObject())
            {                
                //finding the point that intersects with ground
                Vector3 loc = (hit.point == transform.position) ? Vector3.zero : hit.point;
                
                rb.AddForce(jumpForce * Vector3.up, ForceMode.Impulse);
                anim.SetTrigger("jump");  
                grounded = false;              

                //figure out the direction of movement in relation to current position
                Vector3 tryLoc = loc - transform.position;
                rb.velocity += tryLoc * Time.deltaTime * maxMove;

                //run this if we're changing direction
                if (loc != Vector3.zero)
                {
                    //finding out the new angle to face
                    //we're only paying attention to horizontal movement
                    Vector3 tryLocFlat = new Vector3(tryLoc.x, 0f, tryLoc.z);
                    newAngle = Quaternion.LookRotation(tryLocFlat);
                }
            }
            rb.rotation = Quaternion.Slerp(rb.rotation, newAngle.normalized, 0.9f);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            //detecting if hitting one of the wall or small rotator objects
            ScoreChange(10);
        }
        else if (other.gameObject.CompareTag("Ground"))
        {
            grounded = true;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        if (other.gameObject.name == "BigRotate1" || other.gameObject.name == "BigRotate2")
        {
           transform.parent = other.transform;
        }
    }

    void OnCollisionExit()
    {
        transform.parent = null;
    }

    void OnTriggerEnter(Collider other)
    {
        //scene 1 powers
        if (other.gameObject.name == "ProjectilePower")
        {
            butt.SetActive(true);
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Wall"))
        {
            ScoreChange(10);
        }
    }
    
    void ScoreChange(int change)
    {
        score -= change;
        scoreText.text = score.ToString();
    }

    private bool IsPointerOverUIObject() 
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}
