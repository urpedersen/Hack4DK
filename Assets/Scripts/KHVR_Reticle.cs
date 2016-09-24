/********************************************************************************//**
\file      KHVR_Reticle.cs
\brief     Reticle used by KHVR_Gaze
\copyright Copyright 2016 Khora VR, LLC All Rights reserved.
************************************************************************************/

using UnityEngine;
using System.Collections;

public class KHVR_Reticle : MonoBehaviour {

    //==============================================================================
    // Fields
    //==============================================================================

    [SerializeField]
    private float m_smoothing = 0.1f;
    [SerializeField]
    private Transform m_Graphics;
    [SerializeField]
    private MeshRenderer m_MeshRenderer;

    private Camera m_sceneCamera;
    private Vector3 m_OriginalScale;

    [SerializeField]
    private MeshRenderer m_LoadReticle;



    //==============================================================================
    // Properties
    //==============================================================================

    public Vector3 GetOrignalScale {
        get{ return m_OriginalScale; }
    }

    public Vector3 SetGraphicsScale {
        get { return m_Graphics.localScale; }
        set { m_Graphics.localScale = value; }
    }

    //==============================================================================
    // MonoBehaviours
    //==============================================================================

    //==============================================================================
    private void Start()
    {
        m_sceneCamera = Camera.main;
        if (m_Graphics != null) {
            m_OriginalScale = m_Graphics.localScale;
        }

        SetVisable(true);
    }

    //==============================================================================
    private void Update()
    {
        LookAt(m_sceneCamera.transform.position);
    }

    //==============================================================================
    // Public
    //==============================================================================
    //==============================================================================
    public void SetPosition(Vector3 newPosition)
    {
        Vector3 desiredPosition = newPosition;
        float distance = Vector3.Distance(desiredPosition, transform.position);
        if (desiredPosition != transform.position){
            transform.position = Vector3.MoveTowards(transform.position, desiredPosition, distance * m_smoothing);
        }
    }

    //==============================================================================
    public void SetScale(float distance) {
        m_Graphics.localScale = m_OriginalScale * distance;
    }

    //==============================================================================
    public void SetVisable(bool b) {
        m_MeshRenderer.enabled = b;
    }

    /*public void StartLoad() {
        StartCoroutine(StartLoading());
    }*/

    public void StopLoad(){
        StopAllCoroutines();
        //m_LoadReticle.material.SetFloat("_Cutoff", 1);
    }

    //==============================================================================
    // Private
    //==============================================================================

    //==============================================================================
    private void LookAt(Vector3 position)
    {
        transform.LookAt(position);
    }

    /*
    private IEnumerator StartLoading() {
        float time = 0;
        while (time < 0.95f)
        {
            time = time + 0.05f;
            m_LoadReticle.material.SetFloat("_Cutoff", 1 - time);
            yield return new WaitForSeconds(0.05f);
        }
        m_LoadReticle.material.SetFloat("_Cutoff", 1);
    }
    */

}
