using UnityEngine;
using System.Collections;
using Tamboro;

public class Example : MonoBehaviour
{
    public AudioClip clip;



    //Here I'm declaring an SoundManagerEvent because I want to be notified when my "Opening" sound stops its execution.
    public SoundManagerEvent OnAudioFinish;

    void Start()
    {

        //Initializing the OnOpeningFinish -> It will call the function change color when the SoundClip named "Opening" reaches the end.
        OnAudioFinish = new SoundManagerEvent(NotifyOpening);
        //Play the sound and ask to be notified
        SoundManager.Instance.Play("Opening", OnAudioFinish);
    }



    void NotifyOpening()
    {
        Debug.LogWarning("  Opening sound has reached th end => you can do cool stuff here, like stop the character animation. Pretty easy, isn't it? ");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Application.LoadLevel(Application.loadedLevel);
        }

    }

    void OnGUI()
    {


        GUI.Label(new Rect(150, 250, 300, 50), "PRESS P TO RELOAD THE SCENE");

        if (GUI.Button(new Rect(0, 0, 300, 50), "Play by clip"))
        {
            SoundManager.Instance.Play(clip);
        }

        if (GUI.Button(new Rect(0, 50, 300, 50), "Play by name on XML"))
        {
            SoundManager.Instance.Play("FeedbackLevel1");
        }

        if (GUI.Button(new Rect(310, 0, 300, 50), "Pause ALL"))
        {
            SoundManager.Instance.Pause();
        }
        if (GUI.Button(new Rect(310, 50, 300, 50), "Unpause ALL"))
        {
            SoundManager.Instance.Unpause();
        }

        if (GUI.Button(new Rect(150, 150, 300, 50), "Stop ALL "))
        {
            SoundManager.Instance.StopAll();
        }
    }
}
