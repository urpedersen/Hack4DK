/************************************************************************************

Copyright   :   Copyright 2014 Oculus VR, LLC. All Rights reserved.

Licensed under the Oculus VR Rift SDK License Version 3.3 (the "License");
you may not use the Oculus VR Rift SDK except in compliance with the License,
which is provided at the time of installation or download, or which
otherwise accompanies this software in either electronic or hard copy form.

You may obtain a copy of the License at

http://www.oculus.com/licenses/LICENSE-3.3

Unless required by applicable law or agreed to in writing, the Oculus VR SDK
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.

************************************************************************************/

using UnityEngine;
using System.Collections; // required for Coroutines

public class ScreenFade : MonoBehaviour
{

    public float fadeTime = 2.0f;

    public Color fadeColor = new Color(0.01f, 0.01f, 0.01f, 1.0f);

    private Material fadeMaterial = null;
    private bool isFading = false;
    private YieldInstruction fadeInstruction = new WaitForEndOfFrame();

    public bool IsFading { get { return isFading; } }

    private void Awake()
    {
        fadeMaterial = new Material(Shader.Find("Oculus/Unlit Transparent Color"));
    }

    private void OnEnable()
    {
        StartCoroutine(FadeIn());
    }

    private void OnLevelWasLoaded(int level)
    {
        StartCoroutine(FadeIn());
    }

    private void OnDestroy()
    {
        if (fadeMaterial != null)
        {
            Destroy(fadeMaterial);
        }
    }

    private IEnumerator FadeIn()
    {
        float elapsedTime = 0.0f;
        fadeMaterial.color = fadeColor;
        Color color = fadeColor;
        isFading = true;
        while (elapsedTime < fadeTime)
        {
            yield return fadeInstruction;
            elapsedTime += Time.deltaTime;
            color.a = 1.0f - Mathf.Clamp01(elapsedTime / fadeTime);
            fadeMaterial.color = color;
        }
        isFading = false;
    }

    private IEnumerator FadeOut(GazeMover gazeMover, Transform newPosition)
    {
        float elapsedTime = 0.0f;
        Color color = fadeColor;
        isFading = true;
        while (elapsedTime < fadeTime)
        {
            yield return fadeInstruction;
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsedTime / fadeTime);
            fadeMaterial.color = color;
        }
        fadeMaterial.color = Color.black;
        gazeMover.MoveTo(newPosition);
        isFading = false;
        StartCoroutine(FadeIn());
    }

    private void OnPostRender()
    {
        if (isFading)
        {
            fadeMaterial.SetPass(0);
            GL.PushMatrix();
            GL.LoadOrtho();
            GL.Color(fadeMaterial.color);
            GL.Begin(GL.QUADS);
            GL.Vertex3(0f, 0f, -12f);
            GL.Vertex3(0f, 1f, -12f);
            GL.Vertex3(1f, 1f, -12f);
            GL.Vertex3(1f, 0f, -12f);
            GL.End();
            GL.PopMatrix();
        }
    }

    //==============================================================================
    // Public
    //==============================================================================

    public void StartScreenFadeIn()
    {
        StartCoroutine(FadeIn());
    }

    public void StartScreenFadeOut(GazeMover gazeMover, Transform to)
    {
        StartCoroutine(FadeOut(gazeMover, to));
    }
}
