using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems   ;

public class PlayerMove : MonoBehaviour
{
    //instance variables needed for movement and rotation tracking
    private Rigidbody rb;
    private bool grounded;
    private Quaternion newAngle = new Quaternion(0f, 0f, 0f, 0f);
    [SerializeField] private float maxMove;
    [SerializeField] private float jumpHeight;
    [SerializeField] private float gravityScale;
    [SerializeField] private Transform ground;
    private Vector3 startPosition;
    private Quaternion startRotation;

    //instance variables needed for projectile shooting
    [SerializeField] private GameObject butt;

    //scoretracking features
    private float time;
    private int score;
    [SerializeField] private Text scoreText;

    //animation stuff
    [SerializeField] private Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startPosition = transform.position;
        startRotation = transform.rotation;

        time = 0;
        score = 1000;
        ScoreChange(0);

    }

    void Update()
    {
        time += Time.deltaTime;

        if (transform.position.y <= ground.position.y - 1f /*adding this 1f as fudge factor*/)
        {
            transform.position = startPosition;
            transform.rotation = startRotation;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0) || Input.touchCount > 0)
        {
            Debug.Log("mouse down");            
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
            Debug.Log(grounded);
            
            if (Physics.Raycast(onScreenPoint.origin, onScreenPoint.direction, out hit) && grounded && !IsPointerOverUIObject())
            {
                Debug.Log("jump");
                
                //finding the point that intersects with ground
                Vector3 loc = (hit.point == transform.position) ? Vector3.zero : hit.point;
                
                //calculating jumpForce according to specified jumpHeight
                float jumpForce = Mathf.Sqrt(jumpHeight * -2 * Physics2D.gravity.y * gravityScale);
                rb.AddForce(jumpForce * Vector3.up, ForceMode.Impulse);
                anim.SetTrigger("jump");                
                grounded = !grounded;

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
                    Debug.Log(newAngle.eulerAngles);
                }
            }
            rb.rotation = Quaternion.Slerp(rb.rotation, newAngle.normalized, 0.9f);

            Debug.DrawRay(onScreenPoint.origin, onScreenPoint.direction, Color.red, 10f);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        //detecting grounded
        if (other.gameObject.layer == 3)
        {
            grounded = true;
        }
        if (other.gameObject.CompareTag("Wall"))
        {
            //detecting if hitting one of the wall or small rotator objects
            Debug.Log(true);
            ScoreChange(10);
        }

        //applies to scene 2
        if (other.gameObject.name == "BigRotate1" || other.gameObject.name == "BigRotate2")
        {
            //setting the player as a child of the big rotator so it rotates along
            transform.parent = other.transform;
        }
    }

    void OnCollisionExit(Collision other)
    {
        //once the player is not on the big rotator, it should stop rotating with it
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
    }

    void ScoreChange(int change)
    {
        score -= change;
        scoreText.text = "Score: " + score.ToString();
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
