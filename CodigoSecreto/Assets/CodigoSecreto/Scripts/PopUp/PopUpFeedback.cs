using UnityEngine;
using System.Collections;

public class PopUpFeedback : MonoBehaviour
{
    public GUISkin guiSkin;
    private Rect guiRect;
    private GUIStyle textStyle;

    public Texture bg;
    public Texture starEmpty;
    public Texture starFull;

    public int level = 1;
    public int stars = 0;

    private GameObject go;

    public GameObject screenHome;
    public GameObject screenLevels;

    public void SetGO(GameObject goNew)
    {
        this.go = goNew;
    }

    void Start()
    {
        ScreenLevels.SaveNewRating(level, stars);

        guiRect = new Rect(0, 0, 0, 0);

        MySettings.showPopUp = true;

        textStyle = new GUIStyle(guiSkin.GetStyle("Text"));
        textStyle.fontSize = 30;

        if (level == 1)
        {
            AudioController.Instance.Play(AudioController.SoundType.Win1);
        }
        else if (level == 2)
        {
            AudioController.Instance.Play(AudioController.SoundType.Win2);
        }
        else if (level == 3)
        {
            AudioController.Instance.Play(AudioController.SoundType.Win3);
        }
    }

    void OnGUI()
    {
        GUI.skin = guiSkin;
        GUI.depth = 1;

        guiRect.Set(-5, -5, Screen.width + 10, Screen.height + 10);
        GUI.Box(guiRect, "", "Shadow");

        guiRect.Set((Screen.width - bg.width) * 0.5f, (Screen.height - bg.height) * 0.5f - 10, bg.width, bg.height);
        GUI.DrawTexture(guiRect, bg);

        guiRect.Set(guiRect.x + 40, guiRect.y + 60, guiRect.width - 80, guiRect.height * 0.45f);
        if (level == MySettings.maxLevels)
        {
            GUI.Label(guiRect, "<color=#b11e47ff><b>" + TextController.Instance.GetText("FeedbackFinal") + "</b></color>", textStyle);
        }
        else
        {
            GUI.Label(guiRect, "<color=#b11e47ff><b>" + TextController.Instance.GetText("FeedbackLvl") + "</b></color>", textStyle);
        }

        //draw stars
        if (stars >= 3)
        {
            guiRect.Set(515, 60, starFull.width, starFull.height);
            GUI.DrawTexture(guiRect, starFull);
        }
        else
        {
            guiRect.Set(515, 60, starEmpty.width, starEmpty.height);
            GUI.DrawTexture(guiRect, starEmpty);
        }
        if (stars >= 2)
        {
            guiRect.Set(438, 43, starFull.width, starFull.height);
            GUI.DrawTexture(guiRect, starFull);
        }
        else
        {
            guiRect.Set(438, 43, starEmpty.width, starEmpty.height);
            GUI.DrawTexture(guiRect, starEmpty);
        }
        if (stars >= 1)
        {
            guiRect.Set(363, 62, starFull.width, starFull.height);
            GUI.DrawTexture(guiRect, starFull);
        }
        else
        {
            guiRect.Set(363, 62, starEmpty.width, starEmpty.height);
            GUI.DrawTexture(guiRect, starEmpty);
        }

        guiRect.Set(Screen.width * 0.5f - 150, Screen.height * 0.76f - 64, 64, 64);
        if (GUI.Button(guiRect, "", "ButtonFainRestart"))
        {
            AudioController.Instance.Play(AudioController.SoundType.Button);

            Destroy(this.gameObject);

            if (go)
            {
                go.SendMessage("Reset", SendMessageOptions.DontRequireReceiver);
            }
        }

        guiRect.Set(Screen.width * 0.5f - 26, Screen.height * 0.76f - 64, 53, 64);
        if (GUI.Button(guiRect, "", "ButtonLevelsRed"))
        {
            AudioController.Instance.Play(AudioController.SoundType.Button);

            Destroy(this.gameObject);
            if (go)
            {
                Destroy(go);
            }

            Instantiate(screenLevels);
        }

        guiRect.Set(Screen.width * 0.5f + 87, Screen.height * 0.76f - 63, 41, 63);
        if (GUI.Button(guiRect, "", "ButtonPausePlay"))
        {
            GoBack();
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

        if (level == MySettings.maxLevels)
        {
            if (go)
            {
                Destroy(go);
            }

            Instantiate(screenHome);
        }
        else
        {
            if (go)
            {
                go.SendMessage("NextLevel", SendMessageOptions.DontRequireReceiver);
            }
        }
    }

    void OnDestroy()
    {
        if (level == 1)
        {
            AudioController.Instance.Stop(AudioController.SoundType.Win1);
        }
        else if (level == 2)
        {
            AudioController.Instance.Stop(AudioController.SoundType.Win2);
        }
        else if (level == 3)
        {
            AudioController.Instance.Stop(AudioController.SoundType.Win3);
        }

        MySettings.showPopUp = false;
    }
}
