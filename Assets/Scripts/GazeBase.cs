/********************************************************************************//**
\file      GazeAnimation.cs
\brief     Baseclass for gazing
\copyright Copyright 2016 Khora VR, LLC All Rights reserved.
************************************************************************************/

using UnityEngine;
using System.Collections;

public class GazeBase : MonoBehaviour {

    //==============================================================================
    // Fields
    //==============================================================================

    private KHVR_Gaze m_Gaze;

    //==============================================================================
    // Delegates
    //==============================================================================

    public delegate void GazeEventHandler();
    public GazeEventHandler onGazeBeginCallBack;
    public GazeEventHandler onGazeEndCallBack;

    //==============================================================================
    // MonoBehaviours
    //==============================================================================

    //==============================================================================
    private void Start()
    {
        Initialize();
        RegisterToGaze();
    }

    //==============================================================================
    // Public
    //==============================================================================
    
    //==============================================================================
    public void StartGaze(){
        if (onGazeBeginCallBack != null)
        {
            onGazeBeginCallBack();
        }
    }

    //==============================================================================
    public void StopGaze(){
        if (onGazeEndCallBack != null)
        {
            onGazeEndCallBack();
        }
        StopAllCoroutines();
    }
    //==============================================================================
    // Private
    //==============================================================================

    //==============================================================================
    private void Initialize()
    {
        m_Gaze = Camera.main.GetComponent<KHVR_Gaze>();
    }

    //==============================================================================
    private void RegisterToGaze()
    {
        if (m_Gaze != null)
        {
            m_Gaze.RegisterGazeObject(this.transform, this);
        }
    }
}
