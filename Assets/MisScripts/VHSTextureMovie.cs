using UnityEngine;
using System.Collections;

public class VHSTextureMovie : MonoBehaviour {
    public MovieTexture movieclips;
    public MeshRenderer meshRenderer;
   

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material.mainTexture = movieclips;
        movieclips.Play();
        


    }

}
