using UnityEngine;
using System.Collections;

public class SpecktrumCubeS : MonoBehaviour {

    public AudioSource miaudio;
    float[] spectrum = new float[256];
    // mis modelos y shaders
    public Renderer miModelo;
    public Shader CubeGlitch;
    public float miValor;
    void Start()
    {
        //audio = GetComponent<AudioSource>();
    }

    void Update()
    {
        miaudio.GetSpectrumData(spectrum, 0, FFTWindow.BlackmanHarris);
        Renderer rend = miModelo;
        rend.material.shader = CubeGlitch;
        rend.material.SetFloat("_CubeSpec", miValor);
        miValor = spectrum[1] + spectrum[2] * 2500.00f ;
    }

}
