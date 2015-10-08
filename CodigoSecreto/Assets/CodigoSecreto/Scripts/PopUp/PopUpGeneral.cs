using UnityEngine;
using System.Collections;

public class PopUpGeneral : MonoBehaviour
{
    public GUISkin guiSkin;
    private Rect guiRect;
    private GUIStyle textStyle;

    public Texture bg1;
    public Texture bg2;
    public Texture bgFail;

    public bool goBackHome = false;
    public GameObject screenHome;
    public bool isQuit = false;
    public bool isReset = false;
    public bool isFail = false;
    public int failLevel = 1;
    public bool failTimeIsOver = false;
    public bool isPause = false;

    private GameObject go;

    public void SetGO(GameObject goNew)
    {
        this.go = goNew;
    }

    void Start()
    {
        guiRect = new Rect(0, 0, 0, 0);

        MySettings.showPopUp = true;

        textStyle = new GUIStyle(guiSkin.GetStyle("Text"));
        textStyle.fontSize = 30;

        if (isQuit)
        {
            AudioController.Instance.Play(AudioController.SoundType.Quit);
        }
        else if (isReset)
        {
            AudioController.Instance.Play(AudioController.SoundType.Reset);
        }
        else if (isPause)
        {
            AudioController.Instance.Play(AudioController.SoundType.Pause);
        }
        else if (isFail)
        {
            if (failTimeIsOver)
            {
                AudioController.Instance.Play(AudioController.SoundType.FailTimeIsOver);
            }
            else if (failLevel == 1)
            {
                AudioController.Instance.Play(AudioController.SoundType.FailLvl1);
            }
            else if (failLevel == 2)
            {
                AudioController.Instance.Play(AudioController.SoundType.FailLvl2);
            }
            else if (failLevel == 3)
            {
                AudioController.Instance.Play(AudioController.SoundType.FailLvl3);
            }
        }
    }

    void OnGUI()
    {
        GUI.skin = guiSkin;
        GUI.depth = 1;

        guiRect.Set(-5, -5, Screen.width + 10, Screen.height + 10);
        GUI.Box(guiRect, "", "Shadow");

        if (isQuit || isReset)
        {
            guiRect.Set((Screen.width - bg1.width) * 0.5f, (Screen.height - bg1.height) * 0.5f - 10, bg1.width, bg1.height);
            GUI.DrawTexture(guiRect, bg1);
        }
        else if (isFail && !failTimeIsOver)
        {
            guiRect.Set((Screen.width - bgFail.width) * 0.5f, (Screen.height - bgFail.height) * 0.5f - 10, bgFail.width, bgFail.height);
            GUI.DrawTexture(guiRect, bgFail);
        }
        else
        {
            guiRect.Set((Screen.width - bg2.width) * 0.5f, (Screen.height - bg2.height) * 0.5f - 10, bg2.width, bg2.height);
            GUI.DrawTexture(guiRect, bg2);
        }

        if (isQuit)
        {
            guiRect.Set(guiRect.x + 120, guiRect.y + 50, guiRect.width - 240, guiRect.height * 0.45f);
            GUI.Label(guiRect, "<color=#000000ff><b>" + TextController.Instance.GetText("QuitMsg") + "</b></color>", textStyle);

            guiRect.Set(Screen.width * 0.5f - 115, Screen.height * 0.65f - 41, 85, 41);
            if (GUI.Button(guiRect, "", "buttonYES"))
            {
                GoBackScreen();
            }

            guiRect.Set(Screen.width * 0.5f + 30, Screen.height * 0.65f - 51, 97, 51);
            if (GUI.Button(guiRect, "", "buttonNO"))
            {
                GoBack();
            }
        }
        else if (isReset)
        {
            guiRect.Set(guiRect.x + 120, guiRect.y + 50, guiRect.width - 240, guiRect.height * 0.45f);
            GUI.Label(guiRect, "<color=#000000ff><b>" + TextController.Instance.GetText("ResetMsg") + "</b></color>", textStyle);

            guiRect.Set(Screen.width * 0.5f - 115, Screen.height * 0.65f - 41, 85, 41);
            if (GUI.Button(guiRect, "", "buttonYES"))
            {
                GoBackScreen();
            }

            guiRect.Set(Screen.width * 0.5f + 30, Screen.height * 0.65f - 51, 97, 51);
            if (GUI.Button(guiRect, "", "buttonNO"))
            {
                GoBack();
            }
        }
        else if (isFail)
        {
            if (failTimeIsOver)
            {
                guiRect.Set(guiRect.x + 100, guiRect.y + 40, guiRect.width - 200, guiRect.height * 0.45f);
                GUI.Label(guiRect, "<color=#b11e47ff><b>" + TextController.Instance.GetText("FailTimeIsOver") + "</b></color>", textStyle);

                guiRect.Set(Screen.width * 0.5f - 32, Screen.height * 0.7f - 64, 64, 64);
                if (GUI.Button(guiRect, "", "ButtonFainRestart"))
                {
                    GoBackScreen();
                }
            }
            else
            {
                guiRect.Set(guiRect.x + 35, guiRect.y + 30, guiRect.width - 80, guiRect.height * 0.6f);
                if (failLevel == 1)
                {
                    GUI.Label(guiRect, "<color=#b11e47ff>" + TextController.Instance.GetText("FailLvl1") + "</color>", textStyle);
                }
                else if (failLevel == 2)
                {
                    GUI.Label(guiRect, "<color=#b11e47ff>" + TextController.Instance.GetText("FailLvl2") + "</color>", textStyle);
                }
                else if (failLevel == 3)
                {
                    GUI.Label(guiRect, "<color=#b11e47ff>" + TextController.Instance.GetText("FailLvl3") + "</color>", textStyle);
                }

                guiRect.Set(Screen.width * 0.5f - 20, Screen.height * 0.75f - 63, 41, 63);
                if (GUI.Button(guiRect, "", "ButtonPausePlay"))
                {
                    GoBack();
                }
            }
        }
        else if (isPause)
        {
            guiRect.Set(guiRect.x + 120, guiRect.y + 40, guiRect.width - 240, guiRect.height * 0.45f);
            GUI.Label(guiRect, "<color=#b11e47ff><b>" + TextController.Instance.GetText("PauseMsg") + "</b></color>", textStyle);

            guiRect.Set(Screen.width * 0.5f - 20, Screen.height * 0.7f - 63, 41, 63);
            if (GUI.Button(guiRect, "", "ButtonPausePlay"))
            {
                GoBack();
            }
        }
    }

    void GoBackScreen()
    {
        AudioController.Instance.Play(AudioController.SoundType.Button);

        Destroy(this.gameObject);

        if (isQuit)
        {
            if (goBackHome)
            {
                if (go)
                {
                    Destroy(go);
                }

                Instantiate(screenHome);
            }
            else
            {
                Application.Quit();
            }
        }
        else if (isReset || (isFail && failTimeIsOver))
        {
            if (go)
            {
                go.SendMessage("Reset", SendMessageOptions.DontRequireReceiver);
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (isFail && failTimeIsOver)
            {
                GoBackScreen();
            }
            else
            {
                GoBack();
            }
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

        if (isQuit)
        {
            AudioController.Instance.Stop(AudioController.SoundType.Quit);
        }
        else if (isReset)
        {
            AudioController.Instance.Stop(AudioController.SoundType.Reset);
        }
        else if (isPause)
        {
            AudioController.Instance.Stop(AudioController.SoundType.Pause);
        }
        else if (isFail)
        {
            if (failTimeIsOver)
            {
                AudioController.Instance.Stop(AudioController.SoundType.FailTimeIsOver);
            }
            else if (failLevel == 1)
            {
                AudioController.Instance.Stop(AudioController.SoundType.FailLvl1);
            }
            else if (failLevel == 2)
            {
                AudioController.Instance.Stop(AudioController.SoundType.FailLvl2);
            }
            else if (failLevel == 3)
            {
                AudioController.Instance.Stop(AudioController.SoundType.FailLvl3);
            }
        }
    }
}
