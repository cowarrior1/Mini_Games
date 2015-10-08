using UnityEngine;
using System.Collections;
using PlatformIntegration.SVO;

public class ScreenRanking : MonoBehaviour
{
    public GUISkin guiSkin;
    private Rect guiRect;
    private Rect guiRect2;
    private GUIStyle textStyle;
    private GUIStyle styleTextLeft;

    public Texture bg;
    public Texture menubg;
    public Texture titleImg;
    public Texture menuTopSeparator;
    public Texture menuBottomSeparator;
    public Texture playerBg;
    public Texture playersBg;
    public Texture boxSeparator;
    public Texture star;
    public Texture loader;
    private bool loading = false;
    public Texture buttonDia;
    public Texture buttonSemana;
    public Texture buttonMes;
    public Texture buttonTodos;

    public GameObject popUpSettings;
    public GameObject screenGame;
    public GameObject screenLevels;

    private int page = 1;
    private string period = RequestHelper.PERIOD_ALL;
    private GetMinigameRankingSVO ranking = null;

    private float rotateAngle = 0;
    private Vector2 pivotPoint = Vector2.zero;

    private AudioController.SoundType audioType;

    void Start()
    {
        guiRect = new Rect(0, 0, 0, 0);
        guiRect2 = new Rect(0, 0, 0, 0);
        pivotPoint.Set(Screen.width * 0.5f, Screen.height * 0.94f * 0.5f);

        styleTextLeft = new GUIStyle(guiSkin.GetStyle("Text"));
        styleTextLeft.alignment = TextAnchor.MiddleLeft;
        styleTextLeft.fontSize = 15;

        textStyle = new GUIStyle(guiSkin.GetStyle("Text"));
        textStyle.fontSize = 22;

        StartCoroutine(GetRanks());

        audioType = AudioController.SoundType.Ranks;
        AudioController.Instance.Play(audioType);
    }

    void OnGUI()
    {
        GUI.skin = guiSkin;
        GUI.depth = 2;

        if (MySettings.showPopUp || loading)
        {
            GUI.enabled = false;
        }

        guiRect.Set(0, 0, Screen.width, Screen.height);
        GUI.DrawTexture(guiRect, bg);

        guiRect.Set(-5, -5, Screen.width + 10, Screen.height + 10);
        GUI.Box(guiRect, "", "Shadow");

        GUIRanks();

        GUIMenu();

        GUILoading();
    }

    void GUIRanks()
    {
        guiRect.Set((Screen.width - titleImg.width) * 0.5f, 30, titleImg.width, titleImg.height);
        GUI.DrawTexture(guiRect, titleImg);

        guiRect.Set(300, (Screen.height - playersBg.height) * 0.5f - 15, 37, 27);
        if (period == RequestHelper.PERIOD_DAY)
        {
            GUI.DrawTexture(guiRect, buttonDia);
        }
        else
        {
            if (GUI.Button(guiRect, "", "ButtonDia"))
            {
                AudioController.Instance.Play(AudioController.SoundType.Button);

                period = RequestHelper.PERIOD_DAY;
                page = 1;
                StartCoroutine(GetRanks());
            }
        }

        guiRect.Set(340, (Screen.height - playersBg.height) * 0.5f - 15, menuTopSeparator.width, menuTopSeparator.height);
        GUI.DrawTexture(guiRect, menuTopSeparator);

        guiRect.Set(354, (Screen.height - playersBg.height) * 0.5f - 15, 79, 27);
        if (period == RequestHelper.PERIOD_WEEK)
        {
            GUI.DrawTexture(guiRect, buttonSemana);
        }
        else
        {
            if (GUI.Button(guiRect, "", "ButtonSemana"))
            {
                AudioController.Instance.Play(AudioController.SoundType.Button);

                period = RequestHelper.PERIOD_WEEK;
                page = 1;
                StartCoroutine(GetRanks());
            }
        }

        guiRect.Set(436, (Screen.height - playersBg.height) * 0.5f - 15, menuTopSeparator.width, menuTopSeparator.height);
        GUI.DrawTexture(guiRect, menuTopSeparator);

        guiRect.Set(450, (Screen.height - playersBg.height) * 0.5f - 15, 43, 27);
        if (period == RequestHelper.PERIOD_MONTH)
        {
            GUI.DrawTexture(guiRect, buttonMes);
        }
        else
        {
            if (GUI.Button(guiRect, "", "ButtonMes"))
            {
                AudioController.Instance.Play(AudioController.SoundType.Button);

                period = RequestHelper.PERIOD_MONTH;
                page = 1;
                StartCoroutine(GetRanks());
            }
        }

        guiRect.Set(496, (Screen.height - playersBg.height) * 0.5f - 15, menuTopSeparator.width, menuTopSeparator.height);
        GUI.DrawTexture(guiRect, menuTopSeparator);

        guiRect.Set(510, (Screen.height - playersBg.height) * 0.5f - 15, 163, 27);
        if (period == RequestHelper.PERIOD_ALL)
        {
            GUI.DrawTexture(guiRect, buttonTodos);
        }
        else
        {
            if (GUI.Button(guiRect, "", "ButtonTodosOsTempos"))
            {
                AudioController.Instance.Play(AudioController.SoundType.Button);

                period = RequestHelper.PERIOD_ALL;
                page = 1;
                StartCoroutine(GetRanks());
            }
        }

        guiRect.Set((Screen.width - playersBg.width) * 0.5f, (Screen.height - playersBg.height) * 0.5f + 15, playersBg.width, playersBg.height);
        GUI.DrawTexture(guiRect, playersBg);

        guiRect.Set(guiRect.x, guiRect.y - guiRect.height * 0.1f, guiRect.width, guiRect.height * 0.1f);
        if (ranking != null)
        {
            foreach (MinigameRankingSVO rankLine in ranking.minigameRankingList)
            {
                guiRect.Set(guiRect.x, guiRect.y + guiRect.height, guiRect.width, guiRect.height);
                GUIPlayerRankDetails(rankLine);
            }
        }

        guiRect.Set((Screen.width - boxSeparator.width) * 0.5f, (Screen.height + playersBg.height) * 0.5f + 20, boxSeparator.width, boxSeparator.height);
        GUI.DrawTexture(guiRect, boxSeparator);

        guiRect.Set((Screen.width - playerBg.width) * 0.5f, (Screen.height + playersBg.height) * 0.5f + 25 + boxSeparator.height, playerBg.width, playerBg.height);
        GUI.DrawTexture(guiRect, playerBg);

        if (ranking != null)
        {
            GUIPlayerRankDetails(ranking.userMinigameRanking);
        }
    }

    void GUIPlayerRankDetails(MinigameRankingSVO playerRank)
    {
        guiRect2.Set(guiRect.x, guiRect.y, guiRect.width * 0.05f, guiRect.height);
        GUI.Label(guiRect2, "<color=#000000ff><b><i>" + playerRank.ranking + "</i></b></color>", textStyle);

        guiRect2.Set(guiRect.x + guiRect.width * 0.1f, guiRect.y, guiRect.width * 0.7f, guiRect.height);
        GUI.Label(guiRect2, "<color=#000000ff>" + playerRank.nickName + "</color>", styleTextLeft);

        guiRect2.Set(guiRect.x + guiRect.width * 0.88f - 10, guiRect.y, guiRect.width * 0.12f, guiRect.height);
        GUI.Label(guiRect2, "<color=#000000ff><b><i><size=18>" + playerRank.score + "</size></i></b></color>", textStyle);

        guiRect2.Set(guiRect.x + guiRect.width * 0.88f - 15 - star.width, guiRect.y + 1, star.width, star.height);
        GUI.DrawTexture(guiRect2, star);
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

        guiRect.Set(196, Screen.height - menubg.height - 1, 42, 49);
        if (GUI.Button(guiRect, "", "ButtonLevels"))
        {
            AudioController.Instance.Play(AudioController.SoundType.Button);

            Destroy(this.gameObject);

            Instantiate(screenLevels);
        }

        guiRect.Set(Screen.width * 0.5f - 110, Screen.height - menubg.height + 14, 108, 19);
        if (GUI.Button(guiRect, "", "ButtonPrev10"))
        {
            AudioController.Instance.Play(AudioController.SoundType.Button);

            if (ranking != null)
            {
                if (page > 1)
                {
                    page--;
                    StartCoroutine(GetRanks());
                }
            }
        }

        guiRect.Set(Screen.width * 0.5f, Screen.height - menubg.height + 10, menuBottomSeparator.width, menuBottomSeparator.height);
        GUI.DrawTexture(guiRect, menuBottomSeparator);

        guiRect.Set(Screen.width * 0.5f + menuBottomSeparator.width, Screen.height - menubg.height + 11, 97, 21);
        if (GUI.Button(guiRect, "", "ButtonNext10"))
        {
            AudioController.Instance.Play(AudioController.SoundType.Button);

            if (ranking != null)
            {
                if (ranking.hasNextPage)
                {
                    page++;
                    StartCoroutine(GetRanks());
                }
            }
        }
    }

    void GUILoading()
    {
        if (loading)
        {
            guiRect.Set(-5, -5, Screen.width + 10, Screen.height + 10);
            GUI.Box(guiRect, "");
            GUI.Box(guiRect, "");

            GUIUtility.RotateAroundPivot(rotateAngle, pivotPoint);
            guiRect.Set((Screen.width - loader.width) * 0.5f, (Screen.height * 0.94f - loader.height) * 0.5f, loader.width, loader.height);
            GUI.DrawTexture(guiRect, loader);
        }
    }

    IEnumerator GetRanks()
    {
        loading = true;

        yield return new WaitForEndOfFrame();

        ranking = null;

        RankSystem.Instance.GetRanking(period, page);

        while (RankSystem.Instance.ranking == null)
        {
            yield return null;
        }

        if (RankSystem.Instance.ranking.minigameRankingList != null && RankSystem.Instance.ranking.minigameRankingList.Count > 0)
        {
            ranking = RankSystem.Instance.ranking;
        }

        loading = false;
    }

    void Update()
    {
        if (loading)
        {
            rotateAngle += 15;
        }

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
        else if(!MySettings.isPause)
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

        Instantiate(screenGame);
    }

    void OnDestroy()
    {
        AudioController.Instance.Stop(audioType);
    }

}
