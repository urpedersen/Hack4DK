/********************************************************************************//**
\file      KHVR_Gaze.cs
\brief     Gaze implementation using reticle and colliders.
\copyright Copyright 2016 Khora VR, LLC All Rights reserved.
************************************************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KHVR_Gaze : MonoBehaviour {

    //==============================================================================
    // Nested
    //==============================================================================

    private static class Constants
    {
        public const float nearClippingDistance = 0.4f;
        public const float defaultHeight = 1.75f;
    }

    //==============================================================================
    // Fields
    //==============================================================================

    [SerializeField]
    private KHVR_Reticle m_reticle;

    private bool m_showDebugRay = true;
    private float m_rayLenght = 100;
    private GameObject m_CurrentlyGazedOn;

    private Dictionary<Transform, GazeBase> m_GazeObjects = new Dictionary<Transform, GazeBase>();
    private GazeBase m_CurrentlyGazedObject;

    //==============================================================================
    // MonoBehaviours
    //==============================================================================

    //==============================================================================
    private void Awake()
    {
        if (m_reticle != null)
        {
            m_reticle = Instantiate(m_reticle, new Vector3(0, 0, 0), Quaternion.identity) as KHVR_Reticle;
        }  
    }
    //==============================================================================
    private void Update()
    {
        EyeRaycast();
    }

    //==============================================================================
    // Public
    //==============================================================================

    public void RegisterGazeObject(Transform key, GazeBase value)
    {
        m_GazeObjects.Add(key, value);
    }

    //==============================================================================
    // Private
    //==============================================================================

    //==============================================================================


    // TODO: This is way to long - needs to be abstracted a bit
    //==============================================================================
    private void EyeRaycast()
    {

        // start position for raycast
        Vector3 startPosition = transform.position;

        // direction for raycast
        Vector3 direction = transform.forward;

        // the ray
        Ray ray = new Ray(startPosition, direction);

        // debug ray in editor mode
        if (m_showDebugRay)
        {
            Debug.DrawRay(ray.origin, ray.direction * m_rayLenght, Color.red);
        }

        // raycasthit information
        RaycastHit hit;

        if (Physics.Raycast(startPosition, direction * m_rayLenght, out hit, m_rayLenght))
        {
            m_reticle.SetPosition(hit.point);
            m_reticle.SetScale(hit.distance);

            
            if (hit.transform.gameObject != m_CurrentlyGazedOn)
            {
                //==============================================================================
                // Gaze Objects
                //==============================================================================
                if (m_GazeObjects.ContainsKey(hit.transform))
                {
                    m_GazeObjects[hit.transform].StartGaze();
                    m_CurrentlyGazedObject = m_GazeObjects[hit.transform];
                }
                else {
                    m_reticle.StopLoad();
                    
                    if (m_CurrentlyGazedObject != null) { 
                        m_CurrentlyGazedObject.StopGaze();
                        m_CurrentlyGazedObject = null;
                    }
                }
                //==============================================================================
            }

            m_CurrentlyGazedOn = hit.transform.gameObject;
        }
    }
}
