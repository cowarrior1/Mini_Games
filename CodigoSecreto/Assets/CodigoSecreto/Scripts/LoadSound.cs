using UnityEngine;
using System.Collections;
using System;
using System.IO;

public class LoadSound : MonoBehaviour
{
    public AudioClip defaultAudioClip;

    void Start()
    {
        StartCoroutine(WaitToLoadLink());
    }

    IEnumerator WaitToLoadLink()
    {
        while (!AudioController.Instance.AudioLinksLoaded())
        {
            yield return null;
        }

        StartCoroutine(LoadAudioClip());
    }

    IEnumerator LoadAudioClip()
    {
        string audioLink = AudioController.Instance.GetAudioLink(this.gameObject.name);
        AudioClip ac = null;

        if(audioLink.LastIndexOf('.') >= Mathf.Max(audioLink.Length - 5, 0))
        {
            WWW www = new WWW(audioLink);
            yield return www;
            if (www.error != null)
            {
                Debug.Log("ERROR downloading audio clip " + AudioController.Instance.GetAudioLink(this.gameObject.name));
                Debug.Log(www.error);
                if (defaultAudioClip)
                {
                    ac = defaultAudioClip;
                    ac.name = defaultAudioClip.name;
                }
            }
            else
            {
                if(www.audioClip != null)
                {
                    ac = www.audioClip;
                    ac.name = AudioController.Instance.GetAudioName(this.gameObject.name);
                }
                else
                {
                    Debug.Log("ERROR 2 downloading audio clip " + AudioController.Instance.GetAudioLink(this.gameObject.name) + " (no audioClip)");
                    if (defaultAudioClip)
                    {
                        ac = defaultAudioClip;
                        ac.name = defaultAudioClip.name;
                    }
                }

                if (ac != null)
                {
                    while (!ac.isReadyToPlay)
                    {
                        yield return null;
                    }
                }
            }
        }
        else
        {
            Debug.Log("ERROR 3 not an audio clip valid link " + AudioController.Instance.GetAudioLink(this.gameObject.name) + " (no audioClip)");
            if (defaultAudioClip)
            {
                ac = defaultAudioClip;
                ac.name = defaultAudioClip.name;
            }
        }

        while (this.audio.isPlaying)
        {
            yield return null;
        }

        this.audio.clip = ac;
    }

}
