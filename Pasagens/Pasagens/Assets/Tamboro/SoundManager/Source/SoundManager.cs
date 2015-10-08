using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System;

namespace Tamboro
{
    public class XMLSound
    {
        public string name;
        public string description;
        public string path;

        /// <summary>
        /// This entity represents a sound description in the XML file
        /// </summary>
        /// <param name="name">Name of the sound clip, it's use when calling Play() method</param>
        /// <param name="description">Free description of the sound clip. It's never showed to the final user</param>
        /// <param name="path">Path to follow in order to find the sound clip. It must be inside the folder Resources</param>
        public XMLSound(string name, string description, string path)
        {
            this.name = name;
            this.description = description;
            this.path = path;
        }
    }

    public delegate void SoundManagerEvent();

    public class SoundManager : MonoBehaviour
    {
        private static float volume = 1;
        private static List<Sound> sounds = new List<Sound>();
        private static GameObject soundPrefab;
        private static SoundManager instance;
        private XmlDocument soundsXML;
        private List<XMLSound> soundList;


        /// <summary>
        /// Singleton Instance of SoundManager
        /// </summary>
        public static SoundManager Instance
        {
            get
            {
                if (!instance)
                {
                    instance = GameObject.FindObjectOfType<SoundManager>();

                    if (!instance)
                    {
                        GameObject manager = new GameObject();
                        manager.AddComponent<Tamboro.SoundManager>();
                        manager.name = "Sound Manager";

                        instance = manager.GetComponent<SoundManager>();
                        instance.LoadXML();

                        soundPrefab = new GameObject();
                        soundPrefab.AddComponent<AudioSource>();
                        soundPrefab.AddComponent<Sound>();

                        DontDestroyOnLoad(manager);
                        DontDestroyOnLoad(soundPrefab);
                    }
                }

                return instance;
            }
        }

        /// <summary>
        /// Plays an audio clip given a reference to a AudioClip object
        /// </summary>
        /// <param name="clip">Reference to the AudioClip you want to play</param>
        public AudioSource Play(AudioClip clip, SoundManagerEvent callBack = null)
        {
            GameObject source = Instantiate(soundPrefab) as GameObject;
            Sound sound = source.GetComponent<Sound>();
            sound.OnStart += SoundManager_OnStart;
            sound.OnFinish += SoundManager_OnFinish;

            if (callBack != null)
            {
                sound.OnAudioFinish += callBack;
            }

            sound.SetSound(clip, volume);
            source.transform.parent = Instance.transform;
            sounds.Add(sound);
            return source.audio;
        }

        /// <summary>
        /// Plays an AudioClip given a sound clip name.
        /// </summary>
        /// <param name="soundName">Name of the sound clip. It must be the same in the XML songs.xml</param>
        public AudioSource Play(string soundName, SoundManagerEvent callBack = null)
        {
            string path = "";
            try
            {
                path = GetXMLSoundByName(soundName).path;
            }
            catch (NullReferenceException exception)
            {
                Debug.LogWarning("There is no " + soundName + " in your xml file. " + exception);
            }

            AudioClip clip = Resources.Load<AudioClip>(path);

            if (clip == null)
            {
                Debug.LogWarning("Sound Clip " + soundName + " was not found at  " + path);
                return null;
            }

            return Play(clip, callBack);
        }

        /// <summary>
        /// Received when an AudioClip starts its execution
        /// </summary>
        /// <param name="target">Object sound that dispatched the event</param>
        private void SoundManager_OnStart(Sound target)
        {
            //throw new System.NotImplementedException();
        }

        /// <summary>
        /// Received when an AudioClip stops its execution
        /// </summary>
        /// <param name="target">Object sound that dispatched the event</param>
        private void SoundManager_OnFinish(Sound target)
        {
            sounds.Remove(target);
            Destroy(target.gameObject);
        }

        /// <summary>
        /// Pauses all sound clips played by sound manager
        /// </summary>
        public void Pause()
        {
            sounds.ForEach(sound => sound.Pause());
        }

        /// <summary>
        /// Unpause all sounds being executed by SoundManager
        /// </summary>
        public void Unpause()
        {
            sounds.ForEach(sound => sound.Unpause());
        }

        /// <summary>
        /// Mutes the SoundManager. It only works for those sounds played by Tamboro SoundManager
        /// </summary>
        public void Mute()
        {
            SetVolume(volume = 0);
        }

        /// <summary>
        /// Unmutes the SoundManager. It only works for those sounds played by Tamboro SoundManager
        /// </summary>
        public void Unmute()
        {
            SetVolume(volume = 1);
        }

        /// <summary>
        /// Stops all sounds being executed by SoundManager
        /// </summary>
        public void StopAll()
        {
            while (sounds.Count > 0)
            {
                Destroy(sounds[0].gameObject);
                sounds.Remove(sounds[0]);
            }
        }

        /// <summary>
        /// Changes the volume
        /// </summary>
        /// <param name="volume">New volume</param>
        public void SetVolume(float volume)
        {
            sounds.ForEach(sound => sound.audio.volume = volume);
        }

        /// <summary>
        /// Loads the XML (sounds.xml)
        /// </summary>
        private void LoadXML()
        {
            soundList = new List<XMLSound>();
            TextAsset textAsset = (TextAsset)Resources.Load("sounds");
            soundsXML = new XmlDocument();

            try
            {
                soundsXML.LoadXml(textAsset.text);
            }
            catch (NullReferenceException exception)
            {
                Debug.LogWarning("sound.xml not found. You can still use Tamboro.SoundManager.Play() provided that use a sound clip instead of the name of the sound " + exception);
            }

            XmlNodeList elemList = soundsXML.GetElementsByTagName("sounds");
            for (int i = 0; i < elemList.Count; i++)
            {
                foreach (XmlNode chldNode in elemList[i].ChildNodes)
                {
                    XMLSound sound = new XMLSound(chldNode.ChildNodes[0].InnerText, chldNode.ChildNodes[1].InnerText, chldNode.ChildNodes[2].InnerText);
                    soundList.Add(sound);
                }
            }
        }

        /// <summary>
        /// Returns a XMLSound object given a sound clip name
        /// </summary>
        /// <param name="name">Name of the sound clip you want to retrieve from XML file</param>
        private XMLSound GetXMLSoundByName(string name)
        {
            return soundList.Find(item => item.name == name);
        }
    }
}