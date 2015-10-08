using UnityEngine;
using System.Collections;

public class AudioLoading : MonoBehaviour
{
    private Rect guiRect;

    public Texture loader;
    private float rotateAngle = 0;
    private Vector2 pivotPoint = Vector2.zero;

    public AudioSource audioSource;

    void Start()
    {
        guiRect = new Rect(0, 0, 0, 0);
        pivotPoint.Set(Screen.width * 0.5f, Screen.height * 0.94f * 0.5f);

        MySettings.showPopUp = true;
    }

    void OnGUI()
    {
        GUI.depth = 1;

        guiRect.Set(-5, -5, Screen.width + 10, Screen.height + 10);
        GUI.Box(guiRect, "");
        GUI.Box(guiRect, "");

        GUIUtility.RotateAroundPivot(rotateAngle, pivotPoint);
        guiRect.Set((Screen.width - loader.width) * 0.5f, (Screen.height * 0.94f - loader.height) * 0.5f, loader.width, loader.height);
        GUI.DrawTexture(guiRect, loader);
    }

    void Update()
    {
        rotateAngle += 15;

        if (audioSource.clip)
        {
            audioSource.Play();

            Destroy(this.gameObject);
        }
    }

    void OnDestroy()
    {
        MySettings.showPopUp = false;
    }

}
