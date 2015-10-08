using UnityEngine;
using System.Collections;

namespace Tamboro
{
    public delegate void SoundEvent(Sound target);

    public class Sound : MonoBehaviour
    {
        public event SoundEvent OnStart;
        public event SoundEvent OnFinish;

        public event SoundManagerEvent OnAudioFinish;

        public AudioClip clip;

        public void SetSound(AudioClip clip, float volume)
        {
            this.audio.clip = this.clip = clip;
            this.audio.volume = volume;
            this.audio.Play();

            if (OnStart != null)
                OnStart.Invoke(this);

            StartCoroutine("KillSound", clip.length);
        }

        private IEnumerator KillSound(float time)
        {
            yield return new WaitForSeconds(time);

            if (OnFinish != null)
            {
                OnFinish.Invoke(this);
            }

            if (OnAudioFinish != null)
            {
                OnAudioFinish.Invoke();
            }

            Destroy(gameObject);
        }

        public void Pause()
        {
            audio.Pause();
            StopCoroutine("KillSound");
        }

        public void Unpause()
        {
            audio.Play();
            StartCoroutine("KillSound", clip.length - audio.time);
        }
    }
}