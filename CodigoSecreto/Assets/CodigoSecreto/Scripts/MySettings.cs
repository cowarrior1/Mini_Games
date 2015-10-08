using UnityEngine;
using System.Collections;
using System;
using System.IO;

public class MySettings : MonoBehaviour
{
    public bool resetPrefs = false;

    private static bool showPopUpVal = false;
    public static bool showPopUp
    {
        set
        {
            showPopUpVal = value;
            if (showPopUpVal)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
        }
        get
        {
            return showPopUpVal;
        }
    }

    public static bool isPause = false;

    public static string editorUrl = "http://localhost:9004";
    public static string urlText = "/config/text.json";
    public static string urlAudio = "/config/audio.json";
    public static string urlConfig = "/config/config.json";

    public static int maxLevels = 3;
    public static int unlockedLeves = 1;

    //config variables
    public static int[] timeLevels = new int[3];
    public static float doorOpenSpeed = 0.2f;

    void Awake()
    {
        if (resetPrefs)
        {
            ResetPrefs();
        }
    }

    void ResetPrefs()
    {
        PlayerPrefs.DeleteAll();
    }

    void Start()
    {
        if (PlayerPrefs.HasKey("unlockedLeves"))
        {
            unlockedLeves = PlayerPrefs.GetInt("unlockedLeves");
        }
        else
        {
            PlayerPrefs.SetInt("unlockedLeves", unlockedLeves);
        }

        StartCoroutine(LoadConfig());
    }

    public static void UnlockNextLevel(int currentLevel)
    {
        int levelToUnlock = 0;
        if (currentLevel + 1 <= maxLevels)
        {
            levelToUnlock = currentLevel + 1;
        }
        else
        {
            levelToUnlock = currentLevel;
        }
        unlockedLeves = Mathf.Max(PlayerPrefs.GetInt("unlockedLeves"), levelToUnlock);
        PlayerPrefs.SetInt("unlockedLeves", unlockedLeves);
    }

    IEnumerator LoadConfig()
    {
        string url = "";
#if UNITY_EDITOR
        url = editorUrl + urlConfig;
#elif UNITY_STANDALONE
        if (File.Exists(Application.dataPath + "/Resources" + urlConfig))
        {
            url = "file://" + Application.dataPath + "/Resources" + urlConfig;
        }
#else
        url = Application.dataPath + urlConfig;
#endif

        WWW www = new WWW(url);
        yield return www;
        if (www.error != null)
        {
            Debug.Log("ERROR getting " + url + " file!");
            Debug.Log(www.error);
        }
        else
        {
            try
            {
                JSONObject newConfig = new JSONObject(www.text);

                int.TryParse(newConfig.GetField("timeLvl1").str, out timeLevels[0]);
                int.TryParse(newConfig.GetField("timeLvl2").str, out timeLevels[1]);
                int.TryParse(newConfig.GetField("timeLvl3").str, out timeLevels[2]);

                float.TryParse(newConfig.GetField("doorOpenSpeed").str, out doorOpenSpeed);
            }
            catch (Exception ex)
            {
                Debug.Log(ex.Message);
            }
        }
    }

}
