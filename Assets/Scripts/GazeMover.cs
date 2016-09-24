using UnityEngine;
using System.Collections;



public class GazeMover : MonoBehaviour
{

    //==============================================================================
    // Fields
    //==============================================================================

    private GazeBase m_GazeBase;
    private Transform m_Camera;
    [SerializeField] private Transform m_moveTo;

    private bool m_isMoving;
    private bool m_isLookedAt;

    private Vector3 m_moveDirection;
    [SerializeField] private float m_speed;

    [SerializeField] private TextMesh m_text;


    //==============================================================================
    // MonoBehaviours
    //==============================================================================

    //==============================================================================
    public void Start()
    {

        m_GazeBase = GetComponent<GazeBase>();
        m_GazeBase.onGazeBeginCallBack += OnGazeStart;
        m_GazeBase.onGazeEndCallBack += OnGazeEnd;

        m_isMoving = false;
        m_isLookedAt = false;

        m_moveDirection = Vector3.zero;

        m_Camera = FindObjectOfType<OVRManager>().GetComponent<Transform>();

        m_text.GetComponent<Renderer>().enabled = false;
    }

    public void Update()
    {
        
        if (Input.GetMouseButtonDown(0) & m_isLookedAt)
        {
            Debug.Log("Pressed left click.");
           // m_moveDirection = transform.TransformDirection(m_GazeBase);
            m_isMoving = true;
        }

        if (m_isMoving)
        {
            Vector3 desiredPosition = m_moveTo.transform.position;
            float distance = Vector3.Distance(desiredPosition, m_Camera.transform.position);
            if (desiredPosition != m_Camera.transform.position)
            {
                m_Camera.transform.position = Vector3.MoveTowards(m_Camera.transform.position, desiredPosition, distance*m_speed);
            } else {
                m_isMoving = false;
            }
        }

    }

    //==============================================================================
    // Public
    //==============================================================================

    //==============================================================================
    public void OnGazeStart()
    {
        m_isLookedAt = true;
        m_text.GetComponent<Renderer>().enabled = true;
    }

    //==============================================================================
    public void OnGazeEnd()
    {
        m_isLookedAt = false;
        m_text.GetComponent<Renderer>().enabled = false;
    }


}
