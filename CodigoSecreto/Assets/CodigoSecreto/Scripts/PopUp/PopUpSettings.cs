using UnityEngine;
using System.Collections;

public class PopUpSettings : MonoBehaviour
{
    public GUISkin guiSkin;
    private Rect guiRect;

    public Texture bg;
    public Texture soundOn;
    public Texture soundOff;

    void Start()
    {
        MySettings.isPause = true;

        guiRect = new Rect(0, 0, 0, 0);

        MySettings.showPopUp = true;
    }

    void OnGUI()
    {
        GUI.skin = guiSkin;
        GUI.depth = 1;

        guiRect.Set(-5, -5, Screen.width + 10, Screen.height + 10);
        GUI.Box(guiRect, "", "Shadow");

        guiRect.Set((Screen.width - bg.width) * 0.5f, (Screen.height - bg.height) * 0.5f - 10, bg.width, bg.height);
        GUI.DrawTexture(guiRect, bg);

        guiRect.Set(guiRect.x + guiRect.width - 62, guiRect.y - 16, 45, 45);
        if (GUI.Button(guiRect, "", "ButtonClose"))
        {
            GoBack();
        }

        guiRect.Set(Screen.width * 0.5f - 30 - 82, (Screen.height - 80) * 0.5f - 15, 82, 85);
        if (AudioListener.volume > 0)
        {
            GUI.DrawTexture(guiRect, soundOn);
        }
        else
        {
            if (GUI.Button(guiRect, "", "ButtonSoundOn"))
            {
                AudioController.Instance.Play(AudioController.SoundType.Button);

                AudioListener.volume = 1.0f;
            }
        }

        guiRect.Set(Screen.width * 0.5f + 30, (Screen.height - 94) * 0.5f - 15, 90, 100);
        if (AudioListener.volume == 0)
        {
            GUI.DrawTexture(guiRect, soundOff);
        }
        else
        {
            if (GUI.Button(guiRect, "", "ButtonSoundOff"))
            {
                AudioController.Instance.Play(AudioController.SoundType.Button);

                AudioListener.volume = 0.0f;
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            GoBack();
        }
    }

    void GoBack()
    {
        AudioController.Instance.Play(AudioController.SoundType.Button);

        Destroy(this.gameObject);
    }

    void OnDestroy()
    {
        MySettings.showPopUp = false;
        MySettings.isPause = false;
    }
}
