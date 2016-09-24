using UnityEngine;
using System.Collections;

public class Rotation : MonoBehaviour {

    //==============================================================================
    // Fields
    //==============================================================================

        [SerializeField]
    private float m_Speed = 8f;
    private float m_Rotation = 0;

    //==============================================================================
    // MonoBehaviour
    //==============================================================================
    void FixedUpdate()
    {
        m_Rotation += m_Speed * Time.deltaTime;
        transform.eulerAngles = new Vector3(transform.rotation.eulerAngles.x, m_Rotation, transform.rotation.eulerAngles.z);
    }
}
