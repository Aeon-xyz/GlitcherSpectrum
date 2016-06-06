using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
//[AddComponentMenu("Image Effects/GlitchEffect")]
public class ImageGlitchControl : MonoBehaviour
{
    public Texture2D displacementMap;
    public Material material;
    float glitchup, glitchdown, flicker,
            glitchupTime = 0.05f, glitchdownTime = 0.05f, flickerTime = 0.5f;

    public float intensity;

    // Called by camera to apply image effect
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {

        material.SetFloat("_Intensity", intensity);
        material.SetTexture("_DispTex", displacementMap);

        glitchup += Time.deltaTime * intensity;
        glitchdown += Time.deltaTime * intensity;
        flicker += Time.deltaTime * intensity;

        if (flicker > flickerTime)
        {
            material.SetFloat("filterRadius", Random.Range(-3f, 3f) * intensity);
            flicker = 0;
            flickerTime = Random.value;
        }

        if (glitchup > glitchupTime)
        {
            if (Random.value < 0.1f * intensity)
                material.SetFloat("flip_up", Random.Range(0, 1f) * intensity);
            else
                material.SetFloat("flip_up", 0);

            glitchup = 0;
            glitchupTime = Random.value / 10f;
        }

        if (glitchdown > glitchdownTime)
        {
            if (Random.value < 0.1f * intensity)
                material.SetFloat("flip_down", 1 - Random.Range(0, 1f) * intensity);
            else
                material.SetFloat("flip_down", 1);

            glitchdown = 0;
            glitchdownTime = Random.value / 10f;
        }

        if (Random.value < 0.05 * intensity)
        {
            material.SetFloat("displace", Random.value * intensity);
            material.SetFloat("scale", 1 - Random.value * intensity);
        }
        else
            material.SetFloat("displace", 0);

        Graphics.Blit(source, destination, material);
    }
}