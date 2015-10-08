using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;

public class AudioController : MonoBehaviour
{
    public enum SoundType
    {
        None, Button, Home, Intro, Levels, Ranks, 
        GameLvl1Tela1, GameLvl1Tela2, GameLvl2Tela1, GameLvl2Tela2, GameLvl3Tela1, 
        Tutorial1Part1, Tutorial1Part2, Tutorial2Part1, Tutorial2Part2, Tutorial3Part1, Tutorial3Part2, 
        Quit, Reset, FailLvl1, FailLvl2, FailLvl3, FailTimeIsOver, Pause, Win1, Win2, Win3, Good, Bad
    }


    private static AudioController instance;
    public static AudioController Instance
    {
        get
        {
            return instance;
        }
    }

    void Awake()
    {
        instance = this;

        StartCoroutine(LoadAudioConfig());
    }


    private Dictionary<SoundType, AudioSource> sources = new Dictionary<SoundType, AudioSource>();
    private Dictionary<string, string> audioLinks = new Dictionary<string, string>();
    private bool audioLinksLoaded = false;

    public GameObject audioLoading;

    IEnumerator LoadAudioConfig()
    {
        audioLinksLoaded = false;

        string url = "";
#if UNITY_EDITOR
        url = MySettings.editorUrl + MySettings.urlAudio;
#elif UNITY_STANDALONE
        if (File.Exists(Application.dataPath + "/Resources" + MySettings.urlAudio))
        {
            url = "file://" + Application.dataPath + "/Resources" + MySettings.urlAudio;
        }
#else
        url = Application.dataPath + MySettings.urlAudio;
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
                JSONObject newAudioConfig = new JSONObject(www.text);
                string audioLink;
                foreach (string key in newAudioConfig.keys)
                {
                    audioLink = newAudioConfig.GetField(key).str;
                    audioLink = audioLink.Replace("\\", "/");
                    if (audioLink.Length > 0 && audioLink.Substring(0, 1) != "/")
                    {
                        audioLink = "/" + audioLink;
                    }
                    audioLinks.Add(key, audioLink);
                }
            }
            catch (Exception ex)
            {
                Debug.Log(ex.Message);
            }
        }

        audioLinksLoaded = true;
    }

    public string GetAudioLink(string forWhat)
    {
        if (audioLinks.ContainsKey(forWhat))
        {
#if UNITY_EDITOR
            return MySettings.editorUrl + audioLinks[forWhat].Replace(" ", "%20");
#elif UNITY_STANDALONE
            return "file://" + Application.dataPath + "/Resources" + audioLinks[forWhat];
#else
            return Application.dataPath + audioLinks[forWhat].Replace(" ", "%20");
#endif
        }
        else
        {
            return "";
        }
    }

    public string GetAudioName(string forWhat)
    {
        if (audioLinks.ContainsKey(forWhat))
        {
            return audioLinks[forWhat].Substring(audioLinks[forWhat].LastIndexOf("/") + 1);
        }
        else
        {
            return "";
        }
    }

    public bool AudioLinksLoaded()
    {
        return audioLinksLoaded;
    }

    public void Play(SoundType st, float delay = 0)
    {
        if (st == SoundType.None)
        {
            return;
        }

        if (!sources.ContainsKey(st))
        {
            sources.Add(st, transform.FindChild(st.ToString()).GetComponent<AudioSource>());
        }
        AudioSource sound = sources[st];

        if (sound && sound.clip)
        {
            if (delay > 0)
            {
                sound.PlayDelayed(delay);
            }
            else
            {
                sound.Play();
            }
        }
        else
        {
            if (audioLoading)
            {
                AudioLoading al = (Instantiate(audioLoading) as GameObject).transform.GetComponent<AudioLoading>();
                al.audioSource = sound;
            }
        }
    }

    public void Pause(SoundType st)
    {
        if (st == SoundType.None)
        {
            return;
        }

        if (!sources.ContainsKey(st))
        {
            sources.Add(st, transform.FindChild(st.ToString()).GetComponent<AudioSource>());
        }
        AudioSource sound = sources[st];

        if (sound && sound.clip)
        {
            if (sound.isPlaying)
            {
                sound.Pause();
            }
        }
    }

    public void Stop(SoundType st)
    {
        if (st == SoundType.None)
        {
            return;
        }

        if (!sources.ContainsKey(st))
        {
            sources.Add(st, transform.FindChild(st.ToString()).GetComponent<AudioSource>());
        }
        AudioSource sound = sources[st];

        if (sound && sound.clip)
        {
            sound.Stop();
        }
    }

    public bool IsPlaying(SoundType st)
    {
        if (st == SoundType.None)
        {
            return false;
        }

        if (!sources.ContainsKey(st))
        {
            sources.Add(st, transform.FindChild(st.ToString()).GetComponent<AudioSource>());
        }
        AudioSource sound = sources[st];

        return sound.isPlaying;
    }

    public bool IsPaused(SoundType st)
    {
        if (st == SoundType.None)
        {
            return false;
        }

        if (!sources.ContainsKey(st))
        {
            sources.Add(st, transform.FindChild(st.ToString()).GetComponent<AudioSource>());
        }
        AudioSource sound = sources[st];

        if (sound.isPlaying)
        {
            return false;
        }

        if (sound.time <= 0.1f || sound.time >= sound.clip.length - 0.1f)
        {
            return false;
        }

        return true;
    }

}
