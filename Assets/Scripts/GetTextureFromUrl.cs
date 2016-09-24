using UnityEngine;
using System.Collections;

public class GetTextureFromUrl : MonoBehaviour {

    [SerializeField]
    private int m_StartIndex = 5;

    [SerializeField]
    private string m_URL;
    private Renderer m_Renderer;
    private Texture m_Texture;

    private float m_RetriggerTime = 5f;
    private float m_NextTriggerTime;

    private bool m_IsReady = false;

    [SerializeField]
    private GetHTMLFromUrl m_GetHTMLFromUrl;

    private void Awake() {
        m_Renderer = GetComponent<Renderer>();
        m_GetHTMLFromUrl.onIsReady += FetchImage;
    }


    private void Update() {
        if (m_IsReady) {
            if (Time.timeSinceLevelLoad > m_NextTriggerTime) { 
                FetchImage();
                int random = Random.Range(0, 3);
                m_NextTriggerTime = Time.timeSinceLevelLoad + m_RetriggerTime + random;
            }
        }
    }


    private void Start() {

        /*if (m_GetHTMLFromUrl.IsReady) { 
            
        }*/
    }

    private void FetchImage() {
        StartCoroutine(GetImage());
    }

    



    // ============================
    //  Image
    // ============================

    IEnumerator GetImage() {
        m_IsReady = false;
        int random = Random.Range(1, m_GetHTMLFromUrl.DataLength);
        string pictureURL = m_URL + m_GetHTMLFromUrl.GetImageUrl(random);
        WWW www = new WWW(pictureURL);
        yield return www;
        
        m_Renderer.material.mainTexture = www.texture;
        m_IsReady = true;
    }
        
}
