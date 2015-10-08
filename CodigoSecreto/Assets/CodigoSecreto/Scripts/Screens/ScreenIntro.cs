using UnityEngine;
using System.Collections;

public class ScreenIntro : MonoBehaviour
{
    public GUISkin guiSkin;
    private Rect guiRect;
    private GUIStyle textStyle;

    public Texture bg;
    public Texture menubg;
    public Texture boxBg;
    public Texture char1;
    public Texture[] char1Anim;
    public Texture[] char1Anim2;
    private bool charAnimUseFirst = true;
    private int animIndex = 0;
    private float animTime = 0;
    public float changeAnimAt = 0.1f;
    public Texture chinaIcon;
    public Texture egyptIcon;
    public Texture romanIcon;

    public GameObject popUpSettings;
    public GameObject screenHome;
    //public GameObject screenTutorial;
    public GameObject screenGame;

    private AudioController.SoundType audioType;

    void Start()
    {
        guiRect = new Rect(0, 0, 0, 0);

        textStyle = new GUIStyle(guiSkin.GetStyle("Text"));
        textStyle.fontSize = 18;

        audioType = AudioController.SoundType.Intro;
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

        GUIIntro();

        GUIMenu();
    }

    void GUIIntro()
    {
        guiRect.Set(-20, 20, 369, 547);
        GUI.DrawTexture(guiRect, char1);

        guiRect.Set(150, 145, boxBg.width, boxBg.height);
        GUI.DrawTexture(guiRect, boxBg);

        guiRect.Set(guiRect.x + 50, guiRect.y + 95, guiRect.width - 70, guiRect.height - 110);
        GUI.Label(guiRect, "<color=#000000ff>" + TextController.Instance.GetText("IntroMsg") + "</color>", textStyle);

        guiRect.Set(700, 82, chinaIcon.width, chinaIcon.height);
        GUI.DrawTexture(guiRect, chinaIcon);

        guiRect.Set(415, 91, romanIcon.width, romanIcon.height);
        GUI.DrawTexture(guiRect, romanIcon);

        guiRect.Set(480, 150, egyptIcon.width, egyptIcon.height);
        GUI.DrawTexture(guiRect, egyptIcon);
    }

    void GUIMenu()
    {
        guiRect.Set(0, Screen.height - menubg.height - 11, Screen.width, menubg.height);
        GUI.DrawTexture(guiRect, menubg);

        guiRect.Set(30, Screen.height - menubg.height - 2, 50, 49);
        if (GUI.Button(guiRect, "", "ButtonConfig"))
        {
            AudioController.Instance.Play(AudioController.SoundType.Button);

            Instantiate(popUpSettings);
        }

        guiRect.Set(110, Screen.height - menubg.height - 2, 56, 50);
        if (GUI.Button(guiRect, "", "ButtonHome"))
        {
            GoBack();
        }

        //guiRect.Set(196, Screen.height - menubg.height - 1, 32, 48);
        //if (GUI.Button(guiRect, "", "ButtonTutorial"))
        //{
        //    AudioController.Instance.Play(AudioController.SoundType.Button);


        //}

        guiRect.Set(Screen.width - 64, Screen.height - menubg.height - 2, 34, 49);
        if (GUI.Button(guiRect, "", "ButtonPlay"))
        {
            AudioController.Instance.Play(AudioController.SoundType.Button);

            Destroy(this.gameObject);

            Instantiate(screenGame);
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

            animTime += Time.deltaTime;
            CharAnim();
        }
        else if (!MySettings.isPause)
        {
            if (AudioController.Instance.IsPlaying(audioType))
            {
                AudioController.Instance.Pause(audioType);
            }
        }
        else if (MySettings.isPause)
        {
            animTime += Time.fixedDeltaTime;
            CharAnim();
        }
    }

    void CharAnim()
    {
        if (animTime >= changeAnimAt)
        {
            animIndex++;
            if (charAnimUseFirst)
            {
                if (animIndex >= char1Anim.Length)
                {
                    animIndex = 0;
                }
                char1 = char1Anim[animIndex];
                animTime = 0;

                if (!AudioController.Instance.IsPlaying(audioType))
                {
                    charAnimUseFirst = false;
                    animIndex = 0;
                    char1 = char1Anim2[animIndex];
                }
            }
            else
            {
                if (animIndex >= char1Anim2.Length)
                {
                    animIndex = 0;
                }
                char1 = char1Anim2[animIndex];
                animTime = 0;
            }
        }
    }

    void GoBack()
    {
        AudioController.Instance.Play(AudioController.SoundType.Button);

        Destroy(this.gameObject);

        Instantiate(screenHome);
    }

    void OnDestroy()
    {
        AudioController.Instance.Stop(audioType);
    }

}
