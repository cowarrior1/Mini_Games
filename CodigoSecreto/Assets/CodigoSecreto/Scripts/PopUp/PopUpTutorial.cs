using UnityEngine;
using System.Collections;

public class PopUpTutorial : MonoBehaviour
{
    public GUISkin guiSkin;
    private Rect guiRect;
    private GUIStyle textStyle;

    public int tutorialNr = 1;
    private int tutorialPart = 1;
    public Texture bg;

    public Texture boxBg1;
    public Texture boxArrow1;
    public Texture boxBg2;
    public Texture boxArrow2;

    private AudioController.SoundType audioType = AudioController.SoundType.None;

    void Start()
    {
        guiRect = new Rect(0, 0, 0, 0);

        MySettings.showPopUp = true;

        textStyle = new GUIStyle(guiSkin.GetStyle("Text"));
        textStyle.fontSize = 18;

        tutorialPart = 1;
        SetAudioType();
        AudioController.Instance.Play(audioType);
    }

    void SetAudioType()
    {
        if (tutorialNr == 1)
        {
            audioType = AudioController.SoundType.Tutorial1Part1;
        }
        else if (tutorialNr == 2)
        {
            audioType = AudioController.SoundType.Tutorial2Part1;
        }
        else if (tutorialNr == 3)
        {
            audioType = AudioController.SoundType.Tutorial3Part1;
        }
    }

    void OnGUI()
    {
        GUI.skin = guiSkin;
        GUI.depth = 1;

        guiRect.Set(0, 0, Screen.width, Screen.height);
        GUI.DrawTexture(guiRect, bg);

        if (tutorialNr == 1)
        {
            guiRect.Set(475, 70, boxArrow1.width, boxArrow1.height);
            GUI.DrawTexture(guiRect, boxArrow1);

            guiRect.Set(215, 70, boxBg1.width, boxBg1.height);
            GUI.DrawTexture(guiRect, boxBg1);
        }
        else if (tutorialNr == 2)
        {
            guiRect.Set(480, 90, boxArrow1.width, boxArrow1.height);
            GUI.DrawTexture(guiRect, boxArrow1);

            guiRect.Set(220, 90, boxBg1.width, boxBg1.height);
            GUI.DrawTexture(guiRect, boxBg1);
        }
        else
        {
            guiRect.Set(480, 130, boxArrow1.width, boxArrow1.height);
            GUI.DrawTexture(guiRect, boxArrow1);

            guiRect.Set(220, 130, boxBg1.width, boxBg1.height);
            GUI.DrawTexture(guiRect, boxBg1);
        }

        guiRect.Set(guiRect.x + 10, guiRect.y + 10, guiRect.width - 20, guiRect.height - 20);
        GUI.Label(guiRect, "<color=#000000ff>" + TextController.Instance.GetText("Tutorial" + tutorialNr + "Part1") + "</color>", textStyle);

        if (tutorialNr == 1 || tutorialNr == 2)
        {
            guiRect.Set(740, 300, boxArrow2.width, boxArrow2.height);
            GUI.DrawTexture(guiRect, boxArrow2);

            guiRect.Set(480, 240, boxBg2.width, boxBg2.height);
            GUI.DrawTexture(guiRect, boxBg2);
        }
        else
        {
            guiRect.Set(760, 300, boxArrow2.width, boxArrow2.height);
            GUI.DrawTexture(guiRect, boxArrow2);

            guiRect.Set(500, 240, boxBg2.width, boxBg2.height);
            GUI.DrawTexture(guiRect, boxBg2);
        }

        guiRect.Set(guiRect.x + 10, guiRect.y + 10, guiRect.width - 20, guiRect.height - 20);
        GUI.Label(guiRect, "<color=#000000ff>" + TextController.Instance.GetText("Tutorial" + tutorialNr + "Part2") + "</color>", textStyle);

        guiRect.Set(Screen.width - 241, Screen.height - 64, 56, 41);
        if (GUI.Button(guiRect, "", "ButtonBack"))
        {
            GoBack();
        }
    }

    void Update()
    {
        if (tutorialPart == 1)
        {
            if (tutorialNr == 1)
            {
                if (!AudioController.Instance.IsPlaying(AudioController.SoundType.Tutorial1Part1))
                {
                    StartCoroutine(PlayDelayed(AudioController.SoundType.Tutorial1Part2));
                    tutorialPart = 2;
                }
            }
            else if (tutorialNr == 2)
            {
                if (!AudioController.Instance.IsPlaying(AudioController.SoundType.Tutorial2Part1))
                {
                    StartCoroutine(PlayDelayed(AudioController.SoundType.Tutorial2Part2));
                    tutorialPart = 2;
                }
            }
            else if (tutorialNr == 3)
            {
                if (!AudioController.Instance.IsPlaying(AudioController.SoundType.Tutorial3Part1))
                {
                    StartCoroutine(PlayDelayed(AudioController.SoundType.Tutorial3Part2));
                    tutorialPart = 2;
                }
            }
        }

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            GoBack();
        }
    }

    IEnumerator PlayDelayed(AudioController.SoundType audioPart2)
    {
        yield return null;
        yield return null;
        AudioController.Instance.Play(audioPart2);
    }


    void GoBack()
    {
        AudioController.Instance.Play(AudioController.SoundType.Button);

        Destroy(this.gameObject);
    }

    void OnDestroy()
    {
        MySettings.showPopUp = false;

        if (tutorialNr == 1)
        {
            AudioController.Instance.Stop(AudioController.SoundType.Tutorial1Part1);
            AudioController.Instance.Stop(AudioController.SoundType.Tutorial1Part2);
        }
        else if (tutorialNr == 2)
        {
            AudioController.Instance.Stop(AudioController.SoundType.Tutorial2Part1);
            AudioController.Instance.Stop(AudioController.SoundType.Tutorial2Part2);
        }
        else if (tutorialNr == 3)
        {
            AudioController.Instance.Stop(AudioController.SoundType.Tutorial3Part1);
            AudioController.Instance.Stop(AudioController.SoundType.Tutorial3Part2);
        }
    }

}
