using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace BmFramework.Core
{
    public class AudioNode : Object, IRef
    {
        public AudioSource player;
        public int handle;
        public bool isPause;
        public AudioNode()
        {
            GameObject tmp = new GameObject();
            tmp.transform.SetParent(AudioManager.instance.transform);
            player = tmp.AddComponent<AudioSource>();
            handle = tmp.GetHashCode();
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (obj.GetType() != typeof(AudioNode))
                return false;
            AudioNode c = obj as AudioNode;
            return (this.handle == c.handle && this.handle == c.handle);
        }

        public void Reset()
        {
            player.clip = null;
            player.playOnAwake = false;
        }

        public void Destroy()
        {
            Destroy(player.gameObject);
        }

        public void PlaySound2D(AudioClip clip, bool _loop=false, float _pitch=1.0f)
        {
            if(_loop)
            {
                player.clip = clip;
                player.loop = _loop;
                player.Play();
            }
            else
            {
                player.PlayOneShot(clip);
            }
            player.volume = AudioManager.instance.volume;
            player.pitch = _pitch;
            player.mute = false;
        }


        public void PlaySoundAtPoint(AudioClip clip, Vector3 Pos, bool _loop)
        {
            player.transform.position = Pos;
            player.clip = clip;
            player.loop = _loop;
            player.spatialBlend = 1;
            player.volume = AudioManager.instance.volume;
            player.mute = false;
            player.Play();
        }

        public bool isPlaying()
        {
            return isPause?true:player.isPlaying;
        }

        public void Pause(float _time=0)
        {
            if(_time==0)
            {
                isPause = true;
                player.Pause();
            }
            else
            {
                player.DOFade(0, _time).OnComplete(()=> {
                    isPause = true;
                    player.Pause();
                });
            }
        }

        public void Resume(float _time = 0)
        {
            if (_time == 0)
            {
                isPause = false;
                player.UnPause();
            }
            else
            {
                player.DOFade(AudioManager.instance.volume, _time).OnComplete(() => {
                    isPause = false;
                    player.UnPause();
                });
            }
        }

        public void Stop(float _time = 0)
        {
            if (_time == 0)
            {
                isPause = false;
                player.Stop();
            }
            else
            {
                player.DOFade(0, _time).OnComplete(() => {
                    isPause = false;
                    player.Stop();
                });
            }
        }

        public void Mute(float _time = 0, bool _isMute=true)
        {
            if (_time == 0)
            {
                player.mute = _isMute;
            }
            else
            {
                player.DOFade(_isMute?0:AudioManager.instance.volume, _time).OnComplete(() => {                    
                    player.mute = _isMute;
                });
            }
        }
    }
}

