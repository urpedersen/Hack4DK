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

    private ScreenFade m_ScreenFade;

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
        m_ScreenFade = FindObjectOfType<ScreenFade>();
        m_isMoving = false;
        m_isLookedAt = false;

        m_moveDirection = Vector3.zero;

        m_Camera = FindObjectOfType<OVRManager>().GetComponent<Transform>();

        if(m_text != null)
            m_text.GetComponent<Renderer>().enabled = false;
    }

    public void Update()
    {
        
        if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) & m_isLookedAt)
        {
            //Debug.Log("Pressed left click.");
            // m_moveDirection = transform.TransformDirection(m_GazeBase);
            if(!m_ScreenFade.IsFading)
            m_ScreenFade.StartScreenFadeOut(this, m_moveTo);
            //m_isMoving = true;
        }

        /*if (m_isMoving)
        {
            Vector3 desiredPosition = m_moveTo.transform.position;
            float distance = Vector3.Distance(desiredPosition, m_Camera.transform.position);
            if (distance>0.01) // desiredPosition != m_Camera.transform.position
            {
                m_Camera.transform.position = Vector3.MoveTowards(m_Camera.transform.position, desiredPosition, distance*m_speed);
                //stupidCounter++;
                /*if (stupidCounter>30)
                {
                    m_isMoving = false;
                    stupidCounter = 0;
                    desiredPosition = m_Camera.transform.position;
                }
            } else {
                m_isMoving = false;
            }
        }*/

    }

    //==============================================================================
    // Public
    //==============================================================================

    public void MoveTo(Transform newPosition)
    {
        m_Camera.transform.position = newPosition.position;
    }
    //==============================================================================
    public void OnGazeStart()
    {
        m_isLookedAt = true;

        // Display text
        if (m_text != null)
            m_text.GetComponent<Renderer>().enabled = true;
    }

    //==============================================================================
    public void OnGazeEnd()
    {
        m_isLookedAt = false;

        // Remove text
        if (m_text != null)
            m_text.GetComponent<Renderer>().enabled = false;
    }

}
