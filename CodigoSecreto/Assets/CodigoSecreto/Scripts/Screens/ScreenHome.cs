using UnityEngine;
using System.Collections;
using System;

public class ScreenHome : MonoBehaviour
{
    public GUISkin guiSkin;
    private Rect guiRect;

    public Texture bg;
    public Texture logo;
    public Texture menubg;

    public GameObject popUpSettings;
    public GameObject popUpQuit;
    public GameObject screenIntro;

    private AudioController.SoundType audioType;

    void Start()
    {
        guiRect = new Rect(0, 0, 0, 0);

        audioType = AudioController.SoundType.Home;
        AudioController.Instance.Play(audioType);
    }

    void OnGUI()
    {
        GUI.skin = guiSkin;
        GUI.depth = 2;

        if (MySettings.showPopUp)
        {
            GUI.enabled = false;
        }

        guiRect.Set(0, 0, Screen.width, Screen.height);
        GUI.DrawTexture(guiRect, bg);

        guiRect.Set((Screen.width - logo.width) * 0.5f, (Screen.height - logo.height - menubg.height) * 0.5f - 20, logo.width, logo.height);
        GUI.DrawTexture(guiRect, logo);

        guiRect.Set(0, Screen.height - menubg.height - 11, Screen.width, menubg.height);
        GUI.DrawTexture(guiRect, menubg);

        guiRect.Set(30, Screen.height - menubg.height - 2, 50, 49);
        if (GUI.Button(guiRect, "", "ButtonConfig"))
        {
            AudioController.Instance.Play(AudioController.SoundType.Button);

            Instantiate(popUpSettings);
        }

        guiRect.Set(Screen.width - 64, Screen.height - menubg.height - 2, 34, 49);
        if (GUI.Button(guiRect, "", "ButtonPlay"))
        {
            AudioController.Instance.Play(AudioController.SoundType.Button);

            Destroy(this.gameObject);

            Instantiate(screenIntro);
        }
    }

    void Update()
    {
        if (!MySettings.showPopUp)
        {
            if (AudioController.Instance.IsPaused(audioType))
            {
                AudioController.Instance.Play(audioType);
            }

            if (Input.GetKeyUp(KeyCode.Escape))
            {
                GoBack();
            }
        }
        else if (!MySettings.isPause)
        {
            if (AudioController.Instance.IsPlaying(audioType))
            {
                AudioController.Instance.Pause(audioType);
            }
        }
    }

    void GoBack()
    {
        AudioController.Instance.Play(AudioController.SoundType.Button);

        PopUpGeneral gen = (Instantiate(popUpQuit) as GameObject).GetComponent<PopUpGeneral>();
        gen.isQuit = true;
    }

    void OnDestroy()
    {
        AudioController.Instance.Stop(audioType);
    }

}
