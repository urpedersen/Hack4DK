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
    public GazeEventHandler onCompleteGazeCallBack;
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

        StartCoroutine(StartGazeCorutine());
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

    //==============================================================================
    private IEnumerator StartGazeCorutine() {

        float time = 0;

        while (time < 1f) {
            time = time + 0.1f;
            yield return new WaitForSeconds(0.1f);
        }

        OnGazeComplete();
    }

    //==============================================================================
    private void OnGazeComplete() {
        if (onCompleteGazeCallBack != null) {
            onCompleteGazeCallBack();
        }
    }
}
