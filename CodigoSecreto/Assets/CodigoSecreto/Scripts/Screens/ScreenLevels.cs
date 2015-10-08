using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScreenLevels : MonoBehaviour
{
    public GUISkin guiSkin;
    private Rect guiRect;
    private Rect levelRect;

    public Texture bg;
    public Texture menubg;
    public Texture emptyStars;
    public Texture fullStar;
    public Texture levelBg;
    public Texture levelLock;
    public Texture[] numbers;
    public GameObject popUpSettings;
    public GameObject screenGame;
    public GameObject screenHome;
    public GameObject screenRanking;

    private Dictionary<int, int> ratingLevels = new Dictionary<int, int>();

    private AudioController.SoundType audioType;

    void Start()
    {
        guiRect = new Rect(0, 0, 0, 0);
        levelRect = new Rect(0, 0, 0, 0);

        for (int i = 1; i <= MySettings.unlockedLeves; i++)
        {
            if (PlayerPrefs.HasKey("ratingLevel" + i))
            {
                ratingLevels.Add(i, PlayerPrefs.GetInt("ratingLevel" + i));
            }
            else
            {
                ratingLevels.Add(i, 0);
                PlayerPrefs.SetInt("ratingLevel" + i, ratingLevels[i]);
            }
        }

        audioType = AudioController.SoundType.Levels;
        AudioController.Instance.Play(audioType);
    }

    public static void SaveNewRating(int level, int rating)
    {
        if (PlayerPrefs.HasKey("ratingLevel" + level))
        {
            PlayerPrefs.SetInt("ratingLevel" + level, Mathf.Max(PlayerPrefs.GetInt("ratingLevel" + level), rating));
        }
        else
        {
            PlayerPrefs.SetInt("ratingLevel" + level, rating);
        }

        MySettings.UnlockNextLevel(level);
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

        guiRect.Set(Screen.width * 0.5f - levelBg.width * 2.4f, (Screen.height - levelBg.height) * 0.5f - 30, levelBg.width, levelBg.height);
        GUILevel(1);

        guiRect.Set(Screen.width * 0.5f - levelBg.width * 0.5f, (Screen.height - levelBg.height) * 0.5f - 30, levelBg.width, levelBg.height);
        GUILevel(2);

        guiRect.Set(Screen.width * 0.5f + levelBg.width * 1.4f, (Screen.height - levelBg.height) * 0.5f - 30, levelBg.width, levelBg.height);
        GUILevel(3);

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

        //guiRect.Set(196, Screen.height - menubg.height + 2, 54, 42);
        //if (GUI.Button(guiRect, "", "ButtonRanking"))
        //{
        //    AudioController.Instance.Play(AudioController.SoundType.Button);

        //    Destroy(this.gameObject);

        //    Instantiate(screenRanking);
        //}
    }

    void GUILevel(int level)
    {
        levelRect.Set(guiRect.x, guiRect.y, guiRect.width, guiRect.height);

        if (GUI.Button(levelRect, "", "Text"))
        {
            if (level <= MySettings.unlockedLeves)
            {
                AudioController.Instance.Play(AudioController.SoundType.Good);

                Destroy(this.gameObject);

                ScreenGame sc = (Instantiate(screenGame) as GameObject).transform.GetComponent<ScreenGame>();
                sc.level = level;
            }
            else
            {
                AudioController.Instance.Play(AudioController.SoundType.Bad);
            }
        }

        GUI.DrawTexture(levelRect, levelBg);

        levelRect.Set(guiRect.x + (guiRect.width - numbers[level - 1].width) * 0.5f, guiRect.y + (guiRect.height - numbers[level - 1].height) * 0.5f, numbers[level - 1].width, numbers[level - 1].height);
        GUI.DrawTexture(levelRect, numbers[level - 1]);

        levelRect.Set(guiRect.x - 15, guiRect.y - 15, fullStar.width, fullStar.height);
        GUI.DrawTexture(levelRect, emptyStars);

        levelRect.Set(guiRect.x + 37, guiRect.y - 30, fullStar.width, fullStar.height);
        GUI.DrawTexture(levelRect, emptyStars);

        levelRect.Set(guiRect.x + 90, guiRect.y - 17, fullStar.width, fullStar.height);
        GUI.DrawTexture(levelRect, emptyStars);

        if (ratingLevels.ContainsKey(level))
        {
            if (ratingLevels[level] >= 1)
            {
                levelRect.Set(guiRect.x - 15, guiRect.y - 15, fullStar.width, fullStar.height);
                GUI.DrawTexture(levelRect, fullStar);
            }

            if (ratingLevels[level] >= 2)
            {
                levelRect.Set(guiRect.x + 37, guiRect.y - 30, fullStar.width, fullStar.height);
                GUI.DrawTexture(levelRect, fullStar);
            }

            if (ratingLevels[level] >= 3)
            {
                levelRect.Set(guiRect.x + 90, guiRect.y - 17, fullStar.width, fullStar.height);
                GUI.DrawTexture(levelRect, fullStar);
            }
        }

        if (level > MySettings.unlockedLeves)
        {
            levelRect.Set(guiRect.x + guiRect.width - levelLock.width * 0.65f, guiRect.y + guiRect.height - levelLock.height * 0.8f, levelLock.width, levelLock.height);
            GUI.DrawTexture(levelRect, levelLock);
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

        Destroy(this.gameObject);

        Instantiate(screenHome);
    }

    void OnDestroy()
    {
        AudioController.Instance.Stop(audioType);
    }

}
