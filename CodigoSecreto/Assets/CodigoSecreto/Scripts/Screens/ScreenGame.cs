using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ScreenGame : MonoBehaviour
{
    public GUISkin guiSkin;
    private Rect guiRect;
    private GUIStyle textStyle;

    public GameObject popUpGeneral;
    public GameObject popUpSettings;
    public GameObject popUpTutorial;
    public GameObject popUpFeedback;
    public GameObject screenLevels;

    public Texture menubg;

    public Texture char1;
    public Texture[] char1Anim;
    public Texture[] char1Anim2;
    private bool charAnimUseFirst = true;
    private int animIndex = 0;
    private float animTime = 0;
    public float changeAnimAt = 0.1f;

    public Texture[] bg = new Texture[6];
    public Texture level2Button;
    public Texture[] simbolsBg = new Texture[6];
    public Texture speachBox;
    public Texture timeBg;

    public Texture[] signsLevel1Part1 = new Texture[7];
    public Texture[] signsLevel1Part2 = new Texture[7];
    //public int[] signsLevel1Values = new int[7] { 1, 10, 100, 1000, 10000, 100000, 1000000 };
    //public int[] level1Sums = new int[4] { 15000, 72500, 2855, 5000 };
    //public int[] level1SumsActual = new int[4] { 0, 0, 0, 0 };
    private Rect[] level1Rects = new Rect[4];
    private Rect[] level1SignsRects = new Rect[7];
    private List<int>[] level1Answer = new List<int>[4];
    private List<int>[] level1AnswerCorrect = new List<int>[4];
    private bool[] correct1Answer = new bool[4];

    public Texture[] signsLevel2Part1 = new Texture[7];
    public Texture[] signsLevel2Part2 = new Texture[7];
    //public int[] signsLevel2Values = new int[7] { 1, 5, 10, 50, 100, 500, 1000 };
    //public int[] level2Sums = new int[5] { 52, 9505, 4, 2400, 9505 };
    //public int[] level2SumsActual = new int[5] { 0, 0, 0, 0, 0 };
    private Rect[] level2Rects = new Rect[5];
    private Rect[] level2SignsRects = new Rect[7];
    private List<int>[] level2Answer = new List<int>[5];
    private List<int>[] level2AnswerCorrect = new List<int>[5];
    private bool[] correct2Answer = new bool[5];

    public Texture[] signsLevel3Part1 = new Texture[9];
    public Texture[] signsLevel3Part2 = new Texture[9];
    public int[] signsLevel3Values = new int[9] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
    //public int level3Sums = 15;
    //public int[] level3SumsActual = new int[8] { 0, 0, 0, 0, 0, 0, 0, 0 };
    private Rect[] level3Rects = new Rect[5];
    private Rect[] level3SignsRects = new Rect[9];
    private int[] level3Answer = new int[5];
    private int[] level3AnswerCorrect = new int[5];
    private bool[] correct3Answer = new bool[5];

    private int dragSign = -1;

    public Texture[] doorLvl1 = new Texture[7];
    public Texture[] doorLvl2 = new Texture[7];
    public Texture[] doorLvl3 = new Texture[7];
    private bool doorIsOpening = false;
    private int currentDoorIndex = -1;

    public int level = 1;
    public int tela = 1;
    private bool tela1tutorial = false;
    private float remainedTime = 0;
    private string remainedTimeStr;
    private bool isFail = false;

    private Vector2 mousePos = Vector2.zero;

    private Color guiColor;
    public Color wrongAnswerColor = Color.red;

    private AudioController.SoundType audioType = AudioController.SoundType.None;

    void Start()
    {
        guiRect = new Rect(0, 0, 0, 0);

        textStyle = new GUIStyle(guiSkin.GetStyle("Text"));
        textStyle.fontSize = 14;

        level1SignsRects[0] = new Rect(840, 95, 66, 36);
        level1SignsRects[1] = new Rect(840, 145, 66, 32);
        level1SignsRects[2] = new Rect(840, 188, 66, 35);
        level1SignsRects[3] = new Rect(840, 234, 66, 36);
        level1SignsRects[4] = new Rect(840, 284, 66, 34);
        level1SignsRects[5] = new Rect(840, 332, 66, 34);
        level1SignsRects[6] = new Rect(840, 380, 66, 36);

        level1Rects[0] = new Rect(439, 90, 144, 26);
        level1Rects[1] = new Rect(440, 132, 144, 26);
        level1Rects[2] = new Rect(425, 200, 252, 26);
        level1Rects[3] = new Rect(425, 273, 152, 26);

        level2SignsRects[0] = new Rect(840, 97, 66, 36);
        level2SignsRects[1] = new Rect(840, 145, 66, 32);
        level2SignsRects[2] = new Rect(840, 190, 66, 35);
        level2SignsRects[3] = new Rect(840, 236, 66, 36);
        level2SignsRects[4] = new Rect(840, 286, 66, 34);
        level2SignsRects[5] = new Rect(840, 334, 66, 34);
        level2SignsRects[6] = new Rect(840, 382, 66, 36);

        level2Rects[0] = new Rect(640, 46, 60, 30);
        level2Rects[1] = new Rect(578, 129, 175, 30);
        level2Rects[2] = new Rect(628, 210, 32, 30);
        level2Rects[3] = new Rect(608, 267, 116, 30);
        level2Rects[4] = new Rect(608, 372, 116, 30);

        level3SignsRects[0] = new Rect(863, 100, 43, 36);
        level3SignsRects[1] = new Rect(864, 137, 42, 38);
        level3SignsRects[2] = new Rect(864, 176, 42, 36);
        level3SignsRects[3] = new Rect(864, 213, 42, 36);
        level3SignsRects[4] = new Rect(864, 250, 42, 35);
        level3SignsRects[5] = new Rect(864, 286, 42, 36);
        level3SignsRects[6] = new Rect(865, 323, 41, 36);
        level3SignsRects[7] = new Rect(865, 360, 41, 38);
        level3SignsRects[8] = new Rect(865, 400, 41, 34);

        level3Rects[0] = new Rect(580, 128, 60, 48);
        level3Rects[1] = new Rect(642, 128, 54, 48);
        level3Rects[2] = new Rect(520, 177, 58, 48);
        level3Rects[3] = new Rect(641, 177, 54, 48);
        level3Rects[4] = new Rect(580, 225, 58, 46);

        for (int i = 0; i < level1Answer.Length; i++)
        {
            level1Answer[i] = new List<int>();
        }
        //Answer 1
        level1AnswerCorrect[0] = new List<int>();
        level1AnswerCorrect[0].Add(4);
        for (int i = 0; i < 5; i++)
        {
            level1AnswerCorrect[0].Add(3);
        }
        //Answer 2
        level1AnswerCorrect[1] = new List<int>();
        for (int i = 0; i < 7; i++)
        {
            level1AnswerCorrect[1].Add(4);
        }
        level1AnswerCorrect[1].Add(3);
        level1AnswerCorrect[1].Add(3);
        for (int i = 0; i < 5; i++)
        {
            level1AnswerCorrect[1].Add(2);
        }
        //Answer 3
        level1AnswerCorrect[2] = new List<int>();
        level1AnswerCorrect[2].Add(3);
        level1AnswerCorrect[2].Add(3);
        for (int i = 0; i < 8; i++)
        {
            level1AnswerCorrect[2].Add(2);
        }
        for (int i = 0; i < 5; i++)
        {
            level1AnswerCorrect[2].Add(1);
        }
        for (int i = 0; i < 5; i++)
        {
            level1AnswerCorrect[2].Add(0);
        }
        //Answer 4
        level1AnswerCorrect[3] = new List<int>();
        for (int i = 0; i < 5; i++)
        {
            level1AnswerCorrect[3].Add(3);
        }

        for (int i = 0; i < level2Answer.Length; i++)
        {
            level2Answer[i] = new List<int>();
        }
        //Answer 1
        level2AnswerCorrect[0] = new List<int>();
        level2AnswerCorrect[0].Add(3);
        level2AnswerCorrect[0].Add(0);
        level2AnswerCorrect[0].Add(0);
        //Answer 2
        level2AnswerCorrect[1] = new List<int>();
        for (int i = 0; i < 9; i++)
        {
            level2AnswerCorrect[1].Add(6);
        }
        level2AnswerCorrect[1].Add(5);
        level2AnswerCorrect[1].Add(1);
        //Answer 3
        level2AnswerCorrect[2] = new List<int>();
        level2AnswerCorrect[2].Add(0);
        level2AnswerCorrect[2].Add(1);
        //Answer 4
        level2AnswerCorrect[3] = new List<int>();
        level2AnswerCorrect[3].Add(6);
        level2AnswerCorrect[3].Add(6);
        level2AnswerCorrect[3].Add(4);
        level2AnswerCorrect[3].Add(5);
        //Answer 5
        level2AnswerCorrect[4] = new List<int>();
        for (int i = 0; i < 5; i++)
        {
            level2AnswerCorrect[4].Add(6);
        }
        level2AnswerCorrect[4].Add(5);

        level3AnswerCorrect[0] = 8;
        level3AnswerCorrect[1] = 1;
        level3AnswerCorrect[2] = 2;
        level3AnswerCorrect[3] = 6;
        level3AnswerCorrect[4] = 0;

        Reset();
    }

    void OnGUI()
    {
        GUI.skin = guiSkin;
        GUI.depth = 2;

        if (MySettings.showPopUp || doorIsOpening)
        {
            GUI.enabled = false;
        }
        else
        {
            tela1tutorial = false;
        }

        GUIGame();

        GUIMenu();
    }

    void GUIGame()
    {
        if (level == 1)
        {
            if (tela == 1 && tela1tutorial == false)
            {
                GameLvl1Teta1();
            }
            else
            {
                GameLvl1Teta2();
            }
        }
        else if (level == 2)
        {
            if (tela == 1 && tela1tutorial == false)
            {
                GameLvl2Teta1();
            }
            else
            {
                GameLvl2Teta2();
            }
        }
        else if (level == 3)
        {
            if (tela == 1 && tela1tutorial == false)
            {
                GameLvl3Teta1();
            }
            else
            {
                GameLvl3Teta2();
            }
        }
    }

    void GameLvl1Teta1()
    {
        guiRect.Set(0, 0, Screen.width, Screen.height);
        GUI.DrawTexture(guiRect, bg[0]);

        guiRect.Set(Screen.width + 40, 65, -360, 534);
        GUI.DrawTexture(guiRect, char1);

        guiRect.Set(Screen.width - simbolsBg[0].width - 10, Screen.height - menubg.height - 10 - simbolsBg[0].height, simbolsBg[0].width, simbolsBg[0].height);
        GUI.DrawTexture(guiRect, simbolsBg[0]);

        //Signs
        guiRect.Set(guiRect.x + 51 - signsLevel1Part1[0].width * 0.5f, guiRect.y + 32 - signsLevel1Part1[0].height * 0.5f, signsLevel1Part1[0].width, signsLevel1Part1[0].height);
        GUI.DrawTexture(guiRect, signsLevel1Part1[0]);

        guiRect.Set(guiRect.x + 44, guiRect.y - 1, signsLevel1Part1[1].width, signsLevel1Part1[1].height);
        GUI.DrawTexture(guiRect, signsLevel1Part1[1]);

        guiRect.Set(guiRect.x + 60, guiRect.y - 1, signsLevel1Part1[2].width, signsLevel1Part1[2].height);
        GUI.DrawTexture(guiRect, signsLevel1Part1[2]);

        guiRect.Set(guiRect.x + 59, guiRect.y + 1, signsLevel1Part1[3].width, signsLevel1Part1[3].height);
        GUI.DrawTexture(guiRect, signsLevel1Part1[3]);

        guiRect.Set(guiRect.x + 63, guiRect.y, signsLevel1Part1[4].width, signsLevel1Part1[4].height);
        GUI.DrawTexture(guiRect, signsLevel1Part1[4]);

        guiRect.Set(guiRect.x + 45, guiRect.y + 2, signsLevel1Part1[5].width, signsLevel1Part1[5].height);
        GUI.DrawTexture(guiRect, signsLevel1Part1[5]);

        guiRect.Set(guiRect.x + 65, guiRect.y, signsLevel1Part1[6].width, signsLevel1Part1[6].height);
        GUI.DrawTexture(guiRect, signsLevel1Part1[6]);

        //Speach
        guiRect.Set(370, 55, speachBox.width, speachBox.height);
        GUI.DrawTexture(guiRect, speachBox);

        guiRect.Set(guiRect.x + 10, guiRect.y + 14, guiRect.width - 85, guiRect.height - 30);
        GUI.Label(guiRect, "<color=#000000ff><size=18>" + TextController.Instance.GetText("GameLvl1Tela1") + "</size></color>", textStyle);

        //Time
        guiRect.Set(Screen.width - timeBg.width - 10, 10, timeBg.width, timeBg.height);
        GUI.DrawTexture(guiRect, timeBg);

        guiRect.Set(guiRect.x + 10, guiRect.y, guiRect.width - 20, guiRect.height - 20);
        GUI.Label(guiRect, "<size=32><b>" + remainedTimeStr + "</b></size>", textStyle);

        //Button
        guiRect.Set(410, 365, 30, 40);
        if (GUI.Button(guiRect, "", "Text"))
        {
            NextTela();
        }
    }

    void GameLvl1Teta2()
    {
        guiRect.Set(0, 0, Screen.width, Screen.height);
        GUI.DrawTexture(guiRect, bg[1]);

        //Time
        guiRect.Set(Screen.width - timeBg.width - 10, 10, timeBg.width, timeBg.height);
        GUI.DrawTexture(guiRect, timeBg);

        guiRect.Set(guiRect.x + 10, guiRect.y, guiRect.width - 20, guiRect.height - 20);
        GUI.Label(guiRect, "<size=32><b>" + remainedTimeStr + "</b></size>", textStyle);

        //Signs
        guiRect.Set(Screen.width - simbolsBg[1].width - 30, 80, simbolsBg[1].width, simbolsBg[1].height);
        GUI.DrawTexture(guiRect, simbolsBg[1]);

        for (int i = 0; i < signsLevel1Part2.Length; i++)
        {
            guiRect.Set(level1SignsRects[i].x + (level1SignsRects[i].width - signsLevel1Part2[i].width) * 0.5f,
                        level1SignsRects[i].y + (level1SignsRects[i].height - signsLevel1Part2[i].height) * 0.5f, 
                        signsLevel1Part2[i].width, signsLevel1Part2[i].height);
            GUI.DrawTexture(guiRect, signsLevel1Part2[i]);
        }

        float leftMargin = 0;
        for (int r = 0; r < level1Rects.Length; r++)
        {
            leftMargin = -5;
            guiColor = GUI.color;
            if (!correct1Answer[r])
                GUI.color = wrongAnswerColor;
            for (int i = 0; i < level1Answer[r].Count; i++)
            {
                guiRect.Set(level1Rects[r].x + leftMargin,
                            level1Rects[r].y + (level1Rects[r].height - signsLevel1Part2[level1Answer[r][i]].height) * 0.5f,
                            signsLevel1Part2[level1Answer[r][i]].width, signsLevel1Part2[level1Answer[r][i]].height);
                GUI.DrawTexture(guiRect, signsLevel1Part2[level1Answer[r][i]]);
                leftMargin += signsLevel1Part2[level1Answer[r][i]].width - 16;
            }
            GUI.color = guiColor;
        }

        guiRect.Set(470, 375, 167, 55);
        if (GUI.Button(guiRect, "", "ButtonGameConfirm"))
        {
            CheckWin();
        }

        if (currentDoorIndex >= 0)
        {
            guiRect.Set(25, 5, doorLvl1[currentDoorIndex].width, doorLvl1[currentDoorIndex].height - 5);
            GUI.DrawTexture(guiRect, doorLvl1[currentDoorIndex]);
        }

        if (dragSign >= 0)
        {
            guiRect.Set(mousePos.x - signsLevel1Part2[dragSign].width * 0.5f, mousePos.y - signsLevel1Part2[dragSign].height * 0.5f, 
                        signsLevel1Part2[dragSign].width, signsLevel1Part2[dragSign].height);
            GUI.DrawTexture(guiRect, signsLevel1Part2[dragSign]);
        }
    }

    void GameLvl2Teta1()
    {
        guiRect.Set(0, 0, Screen.width, Screen.height);
        GUI.DrawTexture(guiRect, bg[2]);

        guiRect.Set(Screen.width + 40, 65, -360, 534);
        GUI.DrawTexture(guiRect, char1);

        guiRect.Set(Screen.width - simbolsBg[2].width - 10, Screen.height - menubg.height - 11 - simbolsBg[2].height, simbolsBg[2].width, simbolsBg[2].height);
        GUI.DrawTexture(guiRect, simbolsBg[2]);

        //Signs
        guiRect.Set(guiRect.x + 51 - signsLevel2Part1[0].width * 0.5f, guiRect.y + 34 - signsLevel2Part1[0].height * 0.5f, signsLevel2Part1[0].width, signsLevel2Part1[0].height);
        GUI.DrawTexture(guiRect, signsLevel2Part1[0]);

        guiRect.Set(guiRect.x + 44, guiRect.y - 1, signsLevel2Part1[1].width, signsLevel2Part1[1].height);
        GUI.DrawTexture(guiRect, signsLevel2Part1[1]);

        guiRect.Set(guiRect.x + 60, guiRect.y - 4, signsLevel2Part1[2].width, signsLevel2Part1[2].height);
        GUI.DrawTexture(guiRect, signsLevel2Part1[2]);

        guiRect.Set(guiRect.x + 59, guiRect.y, signsLevel2Part1[3].width, signsLevel2Part1[3].height);
        GUI.DrawTexture(guiRect, signsLevel2Part1[3]);

        guiRect.Set(guiRect.x + 55, guiRect.y + 4, signsLevel2Part1[4].width, signsLevel2Part1[4].height);
        GUI.DrawTexture(guiRect, signsLevel2Part1[4]);

        guiRect.Set(guiRect.x + 58, guiRect.y, signsLevel2Part1[5].width, signsLevel2Part1[5].height);
        GUI.DrawTexture(guiRect, signsLevel2Part1[5]);

        guiRect.Set(guiRect.x + 60, guiRect.y, signsLevel2Part1[6].width, signsLevel2Part1[6].height);
        GUI.DrawTexture(guiRect, signsLevel2Part1[6]);

        //Speach
        guiRect.Set(370, 55, speachBox.width, speachBox.height);
        GUI.DrawTexture(guiRect, speachBox);

        guiRect.Set(guiRect.x + 10, guiRect.y + 13, guiRect.width - 85, guiRect.height - 30);
        GUI.Label(guiRect, "<color=#000000ff><size=18>" + TextController.Instance.GetText("GameLvl2Tela1") + "</size></color>", textStyle);

        //Time
        guiRect.Set(Screen.width - timeBg.width - 10, 10, timeBg.width, timeBg.height);
        GUI.DrawTexture(guiRect, timeBg);

        //Button
        guiRect.Set(guiRect.x + 10, guiRect.y, guiRect.width - 20, guiRect.height - 20);
        GUI.Label(guiRect, "<size=32><b>" + remainedTimeStr + "</b></size>", textStyle);

        guiRect.Set(392, 245, 52, 186);
        if (GUI.Button(guiRect, "", "Text"))
        {
            NextTela();
        }
        GUI.DrawTexture(guiRect, level2Button);
    }

    void GameLvl2Teta2()
    {
        guiRect.Set(0, 0, Screen.width, Screen.height);
        GUI.DrawTexture(guiRect, bg[3]);

        //Time
        guiRect.Set(Screen.width - timeBg.width - 10, 10, timeBg.width, timeBg.height);
        GUI.DrawTexture(guiRect, timeBg);

        guiRect.Set(guiRect.x + 10, guiRect.y, guiRect.width - 20, guiRect.height - 20);
        GUI.Label(guiRect, "<size=32><b>" + remainedTimeStr + "</b></size>", textStyle);

        //Signs
        guiRect.Set(Screen.width - simbolsBg[3].width - 30, 80, simbolsBg[3].width, simbolsBg[3].height);
        GUI.DrawTexture(guiRect, simbolsBg[3]);

        for (int i = 0; i < signsLevel2Part2.Length; i++)
        {
            guiRect.Set(level2SignsRects[i].x + (level2SignsRects[i].width - signsLevel2Part2[i].width) * 0.5f,
                        level2SignsRects[i].y + (level2SignsRects[i].height - signsLevel2Part2[i].height) * 0.5f,
                        signsLevel2Part2[i].width, signsLevel2Part2[i].height);
            GUI.DrawTexture(guiRect, signsLevel2Part2[i]);
        }

        float leftMargin = 0;
        for (int r = 0; r < level2Rects.Length; r++)
        {
            leftMargin = -3;
            guiColor = GUI.color;
            if (!correct2Answer[r])
                GUI.color = wrongAnswerColor;
            for (int i = 0; i < level2Answer[r].Count; i++)
            {
                guiRect.Set(level2Rects[r].x + leftMargin,
                            level2Rects[r].y + (level2Rects[r].height - signsLevel2Part2[level2Answer[r][i]].height) * 0.5f,
                            signsLevel2Part2[level2Answer[r][i]].width, signsLevel2Part2[level2Answer[r][i]].height);
                GUI.DrawTexture(guiRect, signsLevel2Part2[level2Answer[r][i]]);
                leftMargin += signsLevel2Part2[level2Answer[r][i]].width - 9;
            }
            GUI.color = guiColor;
        }

        guiRect.Set(580, 408, 167, 55);
        if (GUI.Button(guiRect, "", "ButtonGameConfirm"))
        {
            CheckWin();
        }

        if (currentDoorIndex >= 0)
        {
            guiRect.Set(122, 0, doorLvl2[currentDoorIndex].width, doorLvl2[currentDoorIndex].height - 5);
            GUI.DrawTexture(guiRect, doorLvl2[currentDoorIndex]);
        }

        if (dragSign >= 0)
        {
            guiRect.Set(mousePos.x - signsLevel2Part2[dragSign].width * 0.5f, mousePos.y - signsLevel2Part2[dragSign].height * 0.5f,
                        signsLevel2Part2[dragSign].width, signsLevel2Part2[dragSign].height);
            GUI.DrawTexture(guiRect, signsLevel2Part2[dragSign]);
        }
    }

    void GameLvl3Teta1()
    {
        guiRect.Set(0, 0, Screen.width, Screen.height);
        GUI.DrawTexture(guiRect, bg[4]);

        guiRect.Set(Screen.width + 40, 65, -360, 534);
        GUI.DrawTexture(guiRect, char1);

        guiRect.Set(Screen.width - simbolsBg[4].width - 10, Screen.height - menubg.height - 11 - simbolsBg[4].height, simbolsBg[4].width, simbolsBg[4].height);
        GUI.DrawTexture(guiRect, simbolsBg[4]);

        //Signs
        guiRect.Set(guiRect.x + 46 - signsLevel3Part1[0].width * 0.5f, guiRect.y + 33 - signsLevel3Part1[0].height * 0.5f, signsLevel3Part1[0].width, signsLevel3Part1[0].height);
        GUI.DrawTexture(guiRect, signsLevel3Part1[0]);

        guiRect.Set(guiRect.x + 43, guiRect.y - 5, signsLevel3Part1[1].width, signsLevel3Part1[1].height);
        GUI.DrawTexture(guiRect, signsLevel3Part1[1]);

        guiRect.Set(guiRect.x + 48, guiRect.y - 3, signsLevel3Part1[2].width, signsLevel3Part1[2].height);
        GUI.DrawTexture(guiRect, signsLevel3Part1[2]);

        guiRect.Set(guiRect.x + 46, guiRect.y, signsLevel3Part1[3].width, signsLevel3Part1[3].height);
        GUI.DrawTexture(guiRect, signsLevel3Part1[3]);

        guiRect.Set(guiRect.x + 44, guiRect.y, signsLevel3Part1[4].width, signsLevel3Part1[4].height);
        GUI.DrawTexture(guiRect, signsLevel3Part1[4]);

        guiRect.Set(guiRect.x + 44, guiRect.y - 1, signsLevel3Part1[5].width, signsLevel3Part1[5].height);
        GUI.DrawTexture(guiRect, signsLevel3Part1[5]);

        guiRect.Set(guiRect.x + 46, guiRect.y - 1, signsLevel3Part1[6].width, signsLevel3Part1[6].height);
        GUI.DrawTexture(guiRect, signsLevel3Part1[6]);

        guiRect.Set(guiRect.x + 44, guiRect.y + 2, signsLevel3Part1[7].width, signsLevel3Part1[7].height);
        GUI.DrawTexture(guiRect, signsLevel3Part1[7]);

        guiRect.Set(guiRect.x + 49, guiRect.y - 1, signsLevel3Part1[8].width, signsLevel3Part1[8].height);
        GUI.DrawTexture(guiRect, signsLevel3Part1[8]);

        //Speach
        guiRect.Set(370, 55, speachBox.width, speachBox.height);
        GUI.DrawTexture(guiRect, speachBox);

        guiRect.Set(guiRect.x + 10, guiRect.y + 15, guiRect.width - 85, guiRect.height - 30);
        GUI.Label(guiRect, "<color=#000000ff><size=15>" + TextController.Instance.GetText("GameLvl3Tela1") + "</size></color>", textStyle);

        //Time
        guiRect.Set(Screen.width - timeBg.width - 10, 10, timeBg.width, timeBg.height);
        GUI.DrawTexture(guiRect, timeBg);

        //Button
        guiRect.Set(guiRect.x + 10, guiRect.y, guiRect.width - 20, guiRect.height - 20);
        GUI.Label(guiRect, "<size=32><b>" + remainedTimeStr + "</b></size>", textStyle);

        guiRect.Set(460, 310, 50, 80);
        if (GUI.Button(guiRect, "", "Text"))
        {
            NextTela();
        }
    }

    void GameLvl3Teta2()
    {
        guiRect.Set(0, 0, Screen.width, Screen.height);
        GUI.DrawTexture(guiRect, bg[5]);

        //Time
        guiRect.Set(Screen.width - timeBg.width - 10, 10, timeBg.width, timeBg.height);
        GUI.DrawTexture(guiRect, timeBg);

        guiRect.Set(guiRect.x + 10, guiRect.y, guiRect.width - 20, guiRect.height - 20);
        GUI.Label(guiRect, "<size=32><b>" + remainedTimeStr + "</b></size>", textStyle);

        //Signs
        guiRect.Set(Screen.width - simbolsBg[5].width - 30, 80, simbolsBg[5].width, simbolsBg[5].height);
        GUI.DrawTexture(guiRect, simbolsBg[5]);

        for (int i = 0; i < signsLevel3Part2.Length; i++)
        {
            guiRect.Set(level3SignsRects[i].x + (level3SignsRects[i].width - signsLevel3Part2[i].width) * 0.5f,
                        level3SignsRects[i].y + (level3SignsRects[i].height - signsLevel3Part2[i].height) * 0.5f,
                        signsLevel3Part2[i].width, signsLevel3Part2[i].height);
            GUI.DrawTexture(guiRect, signsLevel3Part2[i]);
        }

        for (int i = 0; i < level3Answer.Length; i++)
        {
            if (level3Answer[i] >= 0)
            {
                guiColor = GUI.color;
                if (!correct3Answer[i])
                    GUI.color = wrongAnswerColor;

                guiRect.Set(level3Rects[i].x + (level3Rects[i].width - signsLevel3Part2[level3Answer[i]].width) * 0.5f,
                            level3Rects[i].y + (level3Rects[i].height - signsLevel3Part2[level3Answer[i]].height) * 0.5f, 
                            signsLevel3Part2[level3Answer[i]].width, signsLevel3Part2[level3Answer[i]].height);
                GUI.DrawTexture(guiRect, signsLevel3Part2[level3Answer[i]]);

                GUI.color = guiColor;
            }
        }

        guiRect.Set(525, 40, 167, 55);
        if (GUI.Button(guiRect, "", "ButtonGameConfirm"))
        {
            CheckWin();
        }

        if (currentDoorIndex >= 0)
        {
            guiRect.Set(51, 17, doorLvl3[currentDoorIndex].width, doorLvl3[currentDoorIndex].height - 5);
            GUI.DrawTexture(guiRect, doorLvl3[currentDoorIndex]);
        }

        if (dragSign >= 0)
        {
            guiRect.Set(mousePos.x - signsLevel3Part2[dragSign].width * 0.5f, mousePos.y - signsLevel3Part2[dragSign].height * 0.5f,
                        signsLevel3Part2[dragSign].width, signsLevel3Part2[dragSign].height);
            GUI.DrawTexture(guiRect, signsLevel3Part2[dragSign]);
        }
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

        //if (tela == 1)
        //{
        //    guiRect.Set(196, Screen.height - menubg.height - 1, 42, 49);
        //    if (GUI.Button(guiRect, "", "ButtonLevels"))
        //    {
        //        AudioController.Instance.Play(AudioController.SoundType.Button);

        //        Destroy(this.gameObject);

        //        Instantiate(screenLevels);
        //    }
        //}
        //else if (tela == 2)
        //{
            guiRect.Set(196, Screen.height - menubg.height - 1, 32, 48);
            if (GUI.Button(guiRect, "", "ButtonTutorial"))
            {
                AudioController.Instance.Play(AudioController.SoundType.Button);

                PopUpTutorial pt = (Instantiate(popUpTutorial) as GameObject).GetComponent<PopUpTutorial>();
                pt.tutorialNr = level;
                if (tela == 1)
                {
                    tela1tutorial = true;
                }
            }

            guiRect.Set(258, Screen.height - menubg.height - 1, 42, 49);
            if (GUI.Button(guiRect, "", "ButtonLevels"))
            {
                AudioController.Instance.Play(AudioController.SoundType.Button);

                Destroy(this.gameObject);

                Instantiate(screenLevels);
            }

            //guiRect.Set(Screen.width - 241, Screen.height - menubg.height + 3, 56, 41);
            //if (GUI.Button(guiRect, "", "ButtonBack"))
            //{
            //    AudioController.Instance.Play(AudioController.SoundType.Button);

            //    tela = 1;

            //    Reset();
            //}
        //}

        guiRect.Set(Screen.width - 155, Screen.height - menubg.height + 1, 44, 45);
        if (GUI.Button(guiRect, "", "ButtonPause"))
        {
            AudioController.Instance.Play(AudioController.SoundType.Button);

            PopUpGeneral popUpGen = (Instantiate(popUpGeneral) as GameObject).GetComponent<PopUpGeneral>();
            popUpGen.isPause = true;
        }

        guiRect.Set(Screen.width - 81, Screen.height - menubg.height - 2, 51, 51);
        if (GUI.Button(guiRect, "", "ButtonRestart"))
        {
            AudioController.Instance.Play(AudioController.SoundType.Button);

            PopUpGeneral popUpGen = (Instantiate(popUpGeneral) as GameObject).GetComponent<PopUpGeneral>();
            popUpGen.isReset = true;
            popUpGen.SetGO(this.gameObject);
        }
    }

    void Update()
    {
        if (!MySettings.showPopUp && !doorIsOpening)
        {
            if (AudioController.Instance.IsPaused(audioType))
            {
                AudioController.Instance.Play(audioType);
            }

            if (Input.GetKeyUp(KeyCode.Escape))
            {
                GoBack();
            }

            if (tela == 1)
            {
                animTime += Time.deltaTime;
                CharAnim();
            }
            else
            {
                mousePos.Set(Input.mousePosition.x, Screen.height - Input.mousePosition.y);
                if (Input.GetMouseButtonDown(0))
                {
                    if (level == 1)
                    {
                        for (int i = 0; i < level1SignsRects.Length; i++)
                        {
                            if (level1SignsRects[i].Contains(mousePos))
                            {
                                dragSign = i;
                                break;
                            }
                        }
                    }
                    else if (level == 2)
                    {
                        for (int i = 0; i < level2SignsRects.Length; i++)
                        {
                            if (level2SignsRects[i].Contains(mousePos))
                            {
                                dragSign = i;
                                break;
                            }
                        }
                    }
                    else if (level == 3)
                    {
                        for (int i = 0; i < level3SignsRects.Length; i++)
                        {
                            if (level3SignsRects[i].Contains(mousePos))
                            {
                                dragSign = i;
                                break;
                            }
                        }
                    }
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    if (dragSign >= 0)
                    {
                        if (level == 1)
                        {
                            for (int i = 0; i < level1Rects.Length; i++)
                            {
                                if (level1Rects[i].Contains(mousePos))
                                {
                                    level1Answer[i].Add(dragSign);
                                    correct1Answer[i] = true;
                                }
                            }
                        }
                        else if (level == 2)
                        {
                            for (int i = 0; i < level2Rects.Length; i++)
                            {
                                if (level2Rects[i].Contains(mousePos))
                                {
                                    level2Answer[i].Add(dragSign);
                                    correct2Answer[i] = true;
                                }
                            }
                        }
                        else if (level == 3)
                        {
                            for (int i = 0; i < level3Rects.Length; i++)
                            {
                                if (level3Rects[i].Contains(mousePos))
                                {
                                    level3Answer[i] = dragSign;
                                    correct3Answer[i] = true;
                                }
                            }
                        }

                        dragSign = -1;
                    }
                    else
                    {
                        if (level == 1)
                        {
                            for (int i = 0; i < level1Rects.Length; i++)
                            {
                                if (level1Rects[i].Contains(mousePos))
                                {
                                    if (level1Answer[i].Count > 0)
                                        level1Answer[i].RemoveAt(level1Answer[i].Count - 1);
                                    correct1Answer[i] = true;
                                }
                            }
                        }
                        else if (level == 2)
                        {
                            for (int i = 0; i < level2Rects.Length; i++)
                            {
                                if (level2Rects[i].Contains(mousePos))
                                {
                                    if (level2Answer[i].Count > 0)
                                        level2Answer[i].RemoveAt(level2Answer[i].Count - 1);
                                    correct2Answer[i] = true;
                                }
                            }
                        }
                        else if (level == 3)
                        {
                            for (int i = 0; i < level3Rects.Length; i++)
                            {
                                if (level3Rects[i].Contains(mousePos))
                                {
                                    level3Answer[i] = -1;
                                    correct3Answer[i] = true;
                                }
                            }
                        }
                    }
                }
            }

            if (remainedTime <= 0)
            {
                Fail();
            }
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

        if (tela == 2)
        {
            if (remainedTime > 0)
            {
                remainedTimeStr = Mathf.FloorToInt(remainedTime / 60).ToString("D2") + ":" + Mathf.FloorToInt(remainedTime % 60).ToString("D2");
                if (!MySettings.showPopUp)
                {
                    remainedTime -= Time.deltaTime;
                }
                else if (isFail)
                {
                    remainedTime -= Time.fixedDeltaTime;
                }
            }
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

    void CheckWin()
    {
        bool won = true;

        if (level == 1)
        {
            for (int i = 0; i < level1Answer.Length; i++)
            {
                if (level1Answer[i].Count != level1AnswerCorrect[i].Count)
                {
                    won = false;
                    correct1Answer[i] = false;
                }
                else
                {
                    for (int j = 0; j < level1Answer[i].Count; j++)
                    {
                        if (level1Answer[i][j] != level1AnswerCorrect[i][j])
                        {
                            won = false;
                            correct1Answer[i] = false;
                        }
                    }
                }
            }
        }
        else if (level == 2)
        {
            for (int i = 0; i < level2Answer.Length; i++)
            {
                if (level2Answer[i].Count != level2AnswerCorrect[i].Count)
                {
                    won = false;
                    correct2Answer[i] = false;
                }
                else
                {
                    for (int j = 0; j < level2Answer[i].Count; j++)
                    {
                        if (level2Answer[i][j] != level2AnswerCorrect[i][j])
                        {
                            won = false;
                            correct2Answer[i] = false;
                        }
                    }
                }
            }
        }
        else if (level == 3)
        {
            for (int i = 0; i < level3Answer.Length; i++)
            {
                if (level3Answer[i] != level3AnswerCorrect[i])
                {
                    won = false;
                    correct3Answer[i] = false;
                }
            }
            //if (won)
            //{
            //    if (4 + signsLevel3Values[level3Answer[0]] + signsLevel3Values[level3Answer[1]] != 15 ||
            //        signsLevel3Values[level3Answer[2]] + 5 + signsLevel3Values[level3Answer[3]] != 15 ||
            //        8 + signsLevel3Values[level3Answer[4]] + 6 != 15 ||
            //        4 + signsLevel3Values[level3Answer[2]] + 8 != 15 ||
            //        signsLevel3Values[level3Answer[0]] + 5 + signsLevel3Values[level3Answer[4]] != 15 ||
            //        signsLevel3Values[level3Answer[1]] + signsLevel3Values[level3Answer[3]] + 6 != 15 ||
            //        //4 + 5 + 6 != 15 ||
            //        signsLevel3Values[level3Answer[1]] + 5 + 8 != 15)
            //    {
            //        won = false;
            //    }
            //}
        }

        if (won)
        {
            StartCoroutine(OpenDoor());
        }
        else
        {
            Fail();
        }
    }

    void Fail()
    {
        isFail = true;

        PopUpGeneral pg = (Instantiate(popUpGeneral) as GameObject).GetComponent<PopUpGeneral>();
        pg.isFail = true;
        pg.failLevel = level;
        pg.SetGO(this.gameObject);

        if (remainedTime <= 0)
        {
            pg.failTimeIsOver = true;
        }
    }

    void Win()
    {
        int stars = 0;
        if (remainedTime / (float)MySettings.timeLevels[level - 1] >= 0.6f)
        {
            stars = 3;
        }
        else if (remainedTime / (float)MySettings.timeLevels[level - 1] >= 0.3f)
        {
            stars = 2;
        }
        else
        {
            stars = 1;
        }

        PopUpFeedback popf = (Instantiate(popUpFeedback) as GameObject).GetComponent<PopUpFeedback>();
        popf.level = level;
        popf.stars = stars;
        popf.SetGO(this.gameObject);
    }

    IEnumerator OpenDoor()
    {
        doorIsOpening = true;

        currentDoorIndex = 0;
        if (level == 1)
        {
            for (int i = 0; i < doorLvl1.Length; i++)
            {
                yield return new WaitForSeconds(MySettings.doorOpenSpeed);
                currentDoorIndex = i;
            }
        }
        else if (level == 2)
        {
            for (int i = 0; i < doorLvl2.Length; i++)
            {
                yield return new WaitForSeconds(MySettings.doorOpenSpeed);
                currentDoorIndex = i;
            }
        }
        else if (level == 3)
        {
            for (int i = 0; i < doorLvl3.Length; i++)
            {
                yield return new WaitForSeconds(MySettings.doorOpenSpeed);
                currentDoorIndex = i;
            }
        }
        yield return new WaitForSeconds(MySettings.doorOpenSpeed);

        doorIsOpening = false;

        Win();
    }

    public void Reset()
    {
        if (audioType != AudioController.SoundType.None)
        {
            AudioController.Instance.Stop(audioType);
        }
        SetAudioType();
        StartCoroutine(PlayAudioDelayed());

        remainedTime = MySettings.timeLevels[level - 1];
        remainedTimeStr = Mathf.FloorToInt(remainedTime / 60).ToString("D2") + ":" + Mathf.FloorToInt(remainedTime % 60).ToString("D2");

        charAnimUseFirst = true;
        isFail = false;
        currentDoorIndex = -1;

        ResetAnswers();
    }

    void ResetAnswers()
    {
        for (int i = 0; i < level1Answer.Length; i++)
        {
            level1Answer[i].Clear();
            correct1Answer[i] = true;
        }
        for (int i = 0; i < level2Answer.Length; i++)
        {
            level2Answer[i].Clear();
            correct2Answer[i] = true;
        }
        for (int i = 0; i < level3Answer.Length; i++)
        {
            level3Answer[i] = -1;
            correct3Answer[i] = true;
        }
    }

    IEnumerator PlayAudioDelayed()
    {
        yield return null;
        AudioController.Instance.Play(audioType);
    }

    void SetAudioType()
    {
        if (level == 1)
        {
            if (tela == 1)
            {
                audioType = AudioController.SoundType.GameLvl1Tela1;
            }
            else if (tela == 2)
            {
                audioType = AudioController.SoundType.GameLvl1Tela2;
            }
            else
            {
                audioType = AudioController.SoundType.None;
            }
        }
        else if (level == 2)
        {
            if (tela == 1)
            {
                audioType = AudioController.SoundType.GameLvl2Tela1;
            }
            else if (tela == 2)
            {
                audioType = AudioController.SoundType.GameLvl2Tela2;
            }
            else
            {
                audioType = AudioController.SoundType.None;
            }
        }
        else if (level == 3)
        {
            if (tela == 1)
            {
                audioType = AudioController.SoundType.GameLvl3Tela1;
            }
            else
            {
                audioType = AudioController.SoundType.None;
            }
        }
        else
        {
            audioType = AudioController.SoundType.None;
        }
    }

    public void NextLevel()
    {
        level++;
        tela = 1;
        Reset();
    }

    public void NextTela()
    {
        tela++;
        if (tela == 3)
        {
            level++;
            tela = 1;
        }
        Reset();
    }

    void GoBack()
    {
        AudioController.Instance.Play(AudioController.SoundType.Button);

        PopUpGeneral popUpGen = (Instantiate(popUpGeneral) as GameObject).GetComponent<PopUpGeneral>();
        popUpGen.isQuit = true;
        popUpGen.goBackHome = true;
        popUpGen.SetGO(this.gameObject);
    }

    void OnDestroy()
    {
        AudioController.Instance.Stop(audioType);
    }

}


