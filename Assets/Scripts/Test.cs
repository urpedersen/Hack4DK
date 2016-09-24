using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour
{

    Transform transform;
    float incement = 1;

    void Start()
    {
        transform = GetComponent<Transform>();
        GameObject audio = new GameObject();
        audio.AddComponent<AudioSource>();
    }

    void Update()
    {
        incement++;
        Vector3 newPostition = new Vector3(incement, 0, 0);
        transform.position = newPostition;

    }
}
