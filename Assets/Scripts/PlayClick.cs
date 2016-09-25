using UnityEngine;
using System.Collections;

public class PlayClick : MonoBehaviour {

    //==============================================================================
    // Fields
    //==============================================================================
    [SerializeField]
    private AudioSource m_clickSound;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            // Debug.Log("Pressed left click.");
            m_clickSound.Play();
        }
    }
}
