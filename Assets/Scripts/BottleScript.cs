using UnityEngine;
using Water2D;

public class BottleScript : MonoBehaviour
{
    public GameObject Ground;
    public GameObject Ceiling;
    public GameObject Camera;
    public TimeManager TimeManager;

    //public GameObject GameObject;
   
    Vector3 direction;
    Vector3 defaultLocalScale;

    private Rigidbody2D m_rigidbody2d;
    private float thrustAmount = 0f;
    [SerializeField]
    private float maxThrustAmount = 1500f;
    [SerializeField]
    private float thrustChargeAmount = 15f;
    private bool slowMotionActive = false;
    private bool releaseThrust = false;
    [SerializeField]
    private float slowMotionAmount = 0.1f;
    [SerializeField]
    private float chargedSize = 0.9f;
    private float timeElapsed = 0f;
    private float totalTime = 0.2f;

    void Start()
    {
        m_rigidbody2d = GetComponent<Rigidbody2D>();
        defaultLocalScale = transform.localScale;
        //wantedSize = new Vector3(chargedSize, chargedSize, 0);
        }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
            {
            if (!slowMotionActive)
                {
                TimeManager.DoSlowMotion(slowMotionAmount);
                slowMotionActive = true;
                }

            if (thrustAmount < maxThrustAmount) thrustAmount += thrustChargeAmount;
            direction = new Vector3(Ceiling.transform.position.x - Ground.transform.position.x, Ceiling.transform.position.y - Ground.transform.position.y, 0);

            //Debug.Log("Holding (" + thrustAmount + ")");
            }

        if (Input.GetKeyUp(KeyCode.Space))
            {
            if (slowMotionActive)
                {
                TimeManager.UndoSlowMotion();
                slowMotionActive = false;
                }
            releaseThrust = true;
            //Debug.Log("Released with force " + thrustAmount);
            }

        if (!Input.GetKey(KeyCode.Space))
            {
            if (defaultLocalScale.x < transform.localScale.x && defaultLocalScale.y < transform.localScale.y)
                {
                timeElapsed += Time.deltaTime;
                //ChangeSizeByLerp(Mathf.Lerp(chargedSize, .7f, timeElapsed / totalTime));
                }
            else timeElapsed = 0f;
            }

        Camera.transform.position = new Vector3(transform.position.x, Camera.transform.position.y, Camera.transform.position.z);
        }

    private void FixedUpdate()
        {
        if (releaseThrust) 
            {
            m_rigidbody2d.AddForce(direction * thrustAmount);
            thrustAmount = 0f;
            releaseThrust = false;
            }
        }

    private void ChangeSizeByLerp(float lerp)
        {
        Vector3 size = transform.localScale;
        size.x = lerp;
        size.y = lerp;
        transform.localScale = size;
        }

    private void ReturnDefaultSize()
        {
        transform.localScale = defaultLocalScale;
        }

    private void DecreaseInSize(float decrement)
        {
        Vector3 size = transform.localScale;
        size.x -= decrement;
        size.y -= decrement;
        transform.localScale = size;
        }
    }
