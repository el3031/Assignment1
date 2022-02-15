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
    private Quaternion newAngle;
    private Vector3 moveVector;
    [SerializeField] private float maxMove;
    [SerializeField] private float sensitivity;
    private Vector3 startPosition;
    private Quaternion startRotation;
    [SerializeField] private Transform ground;

    //for player anim
    [SerializeField] private Animator anim;

    //for touch tracking
    private HashSet<int> untrackedFingers;

    //for keeping track of score
    public float time;
    public int score;
    [SerializeField] private Text scoreText;
    
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        untrackedFingers = new HashSet<int>();

        startRotation = transform.rotation;
        startPosition = transform.position;

        score = PlayerPrefs.GetInt("score");
        time = PlayerPrefs.GetFloat("time"); 
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        foreach (Touch _touch in TouchScreenInputWrapper.touches)
        {            
            if (_touch.phase == UnityEngine.TouchPhase.Began && IsPointerOverUIObject())
            {
                untrackedFingers.Add(_touch.fingerId);
            }
            else if (_touch.phase == UnityEngine.TouchPhase.Moved && !IsPointerOverUIObject() && !untrackedFingers.Contains(_touch.fingerId))
            {                
                Vector3 newRot = _touch.deltaPosition.x * Vector3.up * sensitivity;
                transform.Rotate(newRot, Space.Self);
                Debug.Log("rotating");
            }
            else if (_touch.phase == UnityEngine.TouchPhase.Canceled || _touch.phase == UnityEngine.TouchPhase.Ended)
            {
                if (untrackedFingers.Contains(_touch.fingerId))
                {
                    untrackedFingers.Remove(_touch.fingerId);
                }
            }
        }
        rb.velocity += moveVector * Time.deltaTime * maxMove;
        
        Vector2 horizontalVelocity = new Vector2(rb.velocity.x, rb.velocity.z);
        Debug.Log(horizontalVelocity.magnitude);
        if (horizontalVelocity.magnitude > 0.1f)
        {
            anim.SetTrigger("walk");
        }
        else
        {
            anim.SetTrigger("idle");
        }
    }

    void Update()
    {
        if (transform.position.y <= ground.position.y - 1f /*adding this 1f as fudge factor*/)
        {
            transform.position = startPosition;
            transform.rotation = startRotation;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            ScoreChange(10);
        }
    }

    public void OnJump()
    {
        rb.AddForce(10f * Vector3.up, ForceMode.Impulse);
        anim.SetTrigger("jump");
    }

    private bool IsPointerOverUIObject() 
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    public void OnMove(InputAction.CallbackContext context){
        Vector2 direction = context.ReadValue<Vector2>();
        //Debug.Log("direction: " + direction + ", transform.right: " + transform.right + ", transform.forward: " + transform.forward + ", directionRelative: " + directionRelative);

        moveVector = transform.right * direction.x + transform.forward * direction.y;
    }

    void ScoreChange(int change)
    {
        score -= change;
        scoreText.text = "Score: " + score.ToString();
    }
}
