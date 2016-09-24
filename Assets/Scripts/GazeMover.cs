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
            float distance = Vector3.Distance(desiredPosition, transform.position);
            if (desiredPosition != transform.position)
            {
                transform.position = Vector3.MoveTowards(transform.position, desiredPosition, distance);
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
    }

    //==============================================================================
    public void OnGazeEnd()
    {
        m_isLookedAt = false;
    }


}
