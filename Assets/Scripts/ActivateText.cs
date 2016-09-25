using UnityEngine;
using System.Collections;

public class ActivateText : MonoBehaviour {

    [SerializeField]
    private GameObject textMesh;

	// Use this for initialization
	void Start () {
        if(textMesh != null)
        textMesh.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (other.GetComponent<KHVR_Gaze>())
        {
            if (textMesh != null)
                textMesh.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<KHVR_Gaze>())
        {
            if (textMesh != null)
                textMesh.SetActive(false);
        }

    }

}
