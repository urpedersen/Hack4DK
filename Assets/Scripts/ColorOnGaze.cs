using UnityEngine;
using System.Collections;

public class ColorOnGaze : MonoBehaviour
{

    //==============================================================================
    // Fields
    //==============================================================================

    [SerializeField]
    private Color m_HoverColor = Color.red;

    [SerializeField]
    private Color m_DefaultColor = Color.white;

    private GazeBase m_GazeBase;
    private Renderer m_Renderer;

    //==============================================================================
    // MonoBehaviours
    //==============================================================================

    //==============================================================================
    public void Start()
    {
        
        m_GazeBase = GetComponent<GazeBase>();
        m_Renderer = GetComponent<Renderer>();
        m_GazeBase.onGazeBeginCallBack += OnGazeStart;
        m_GazeBase.onGazeEndCallBack += OnGazeEnd;

        SetColor(m_DefaultColor);
    }

    //==============================================================================
    // Public
    //==============================================================================

    //==============================================================================
    public void OnGazeStart() {
        SetColor(m_HoverColor);
    }

    //==============================================================================
    public void OnGazeEnd()
    {
        SetColor(m_DefaultColor);
    }


    //==============================================================================
    public void SetColor(Color c)
    {
        m_Renderer.material.SetColor("_Color",c);
    }
}
