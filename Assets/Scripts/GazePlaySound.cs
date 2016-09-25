using UnityEngine;
using System.Collections;

public class GazePlaySound : MonoBehaviour {

    //==============================================================================
    // Fields
    //==============================================================================
    private GazeBase m_GazeBase;

    [SerializeField] private AudioSource m_Sound;

    // Use this for initialization
    void Start () {
        m_GazeBase = GetComponent<GazeBase>();
        m_GazeBase.onGazeBeginCallBack += OnGazeStart;
        m_GazeBase.onGazeEndCallBack += OnGazeEnd;
    }
	
	// Update is called once per frame
	void Update () {

	}



    //==============================================================================
    // Public
    //==============================================================================

    //==============================================================================
    public void OnGazeStart()
    {
        if (m_Sound != null)
            m_Sound.Play();
    }

    //==============================================================================
    public void OnGazeEnd()
    {

    }
}
