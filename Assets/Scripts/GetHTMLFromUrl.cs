using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class GetHTMLFromUrl : MonoBehaviour {

    [SerializeField]
    private string m_Url;

    [SerializeField]
    public string m_FileName = "DRDataPictureURL";

    private TextAsset asset; // Gets assigned through code. Reads the file.
    private StreamWriter writer; // This is the writer that writes to the file

    private DataEntry[] m_DateEntries;

    private string[] m_TextureUrls;

    private string[] m_Entries;
    
    private string HTML;

    public delegate void GetHTMLFromUrlEventHandler();
    public GetHTMLFromUrlEventHandler onIsReady;

    private bool isReady;
    public bool IsReady { get { return isReady; } }
    public int DataLength { get { return m_TextureUrls.Length; } }

    public string GetImageUrl(int index) {
        return m_TextureUrls[index];
    }

    private void Start () {
        StartCoroutine(GetHTML());
	}


    private IEnumerator GetHTML() {
        WWW www = new WWW(m_Url);
        yield return www;
        HTML = www.text;
        //Debug.Log(HTML);
        string[] entries = HTML.Split(new string[] { "<img src=\"" }, System.StringSplitOptions.RemoveEmptyEntries);

        m_Entries = new string[entries.Length];
        m_TextureUrls = new string[entries.Length];

        for (int i = 1; i < entries.Length; i++) {
            string[] url = entries[i].Split(new string[] { "\" width=\"" }, System.StringSplitOptions.RemoveEmptyEntries);

            string urlPath = url[0];

            //string[] width = url[1].Split(new string[] { "width=\"" }, System.StringSplitOptions.RemoveEmptyEntries);
            //Debug.Log(width);
            //m_TextureUrls[i] = url[0];

            m_TextureUrls[i] = urlPath;



        }
        
        if (onIsReady != null) {
            onIsReady();
        }

        //WriteToConsole();


        //m_Entries = new string[entries.Length];
        //m_Entries = entries;

    }

    private void WriteToConsole()
    {
        string s = string.Empty;
        for (int i = 1; i < m_TextureUrls.Length; i++)
        {
            s = s + m_TextureUrls[i] + " \n";
            
        }

        Debug.Log(s);
    }


    //==============================================================================
    public struct DataEntry
    {
        public string url;
        public int width;
        public int height;

        public DataEntry(int width, int height, string url)
        {
            this.url = url;
            this.width = width;
            this.height = height;
        }
    }
}
