using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FadeImage : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Image image;

    public bool fading = false;

    // Start is called before the first frame update
    void Start()
    {
        FadeOut();
    }

    void Update()
    {
        
    }

    /// <summary>
    /// Fades in this image.
    /// </summary>
    public void FadeIn()
    {
        //Fully fade in Image (1) with the duration of 2
        image.CrossFadeAlpha(1, 2.0f, false);
    }

    /// <summary>
    /// Fades out this image.
    /// </summary>
    public void FadeOut()
    {
        //Fade out to nothing (0) the Image with a duration of 2
        image.CrossFadeAlpha(0, 2.0f, false);
    }

    /*
    void OnGUI()
    {
        //Fetch the Toggle's state
        fading = GUI.Toggle(new Rect(0, 0, 100, 30), fading, "Fade In/Out");
    }
    */
}
