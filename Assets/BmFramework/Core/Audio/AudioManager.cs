using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BmFramework.Core
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager instance;

        public float volume
        {
            set
            {
                _volume = value;
                for (int i = 0; i < usingNodes.Count; i++)
                {
                    usingNodes[i].player.volume = _volume;
                }
            }
            get
            {
                return _volume;
            }
        }

        RefPool nodePool;
        List<AudioNode> usingNodes = new List<AudioNode>();
        [Range(0, 1)]
        private float _volume = 1;
        internal void Init()
        {
            nodePool = new RefPool(FrameworkMain.instance.sound_pool_max);
        }

        internal void Logic(float _time)
        {
            for(int i=0; i< usingNodes.Count; i++)
            {
                if(!usingNodes[i].isPlaying())
                {
                    Free(usingNodes[i]);
                }
            }
        }

        AudioNode Allocate()
        {
            AudioNode t = nodePool.Get<AudioNode>();
            if(usingNodes.Contains(t))
            {
                Free(t);
            }
            usingNodes.Add(t);
            return t;
        }


        void Free(AudioNode _node)
        {
            usingNodes.Remove(_node);          
            nodePool.Recycle(_node);
        }

        AudioNode GetPlayingNode(int _handle)
        {
            int id = -1;
            for(int i=0; i<usingNodes.Count; i++)
            {
                if(_handle==usingNodes[i].handle)
                {
                    return usingNodes[i];
                }
            }
            return null;
        }

        #region Public
        public int PlaySound2D(AudioClip clip, bool isLoop=false)
        {
            AudioNode node = Allocate();
            node.PlaySound2D(clip, isLoop);
            return node.handle;
        }

        public int PlaySoundAtPoint(AudioClip clip, Vector3 Pos, bool isLoop = false)
        {
            AudioNode node = Allocate();
            node.PlaySoundAtPoint(clip, Pos, isLoop);
            return node.handle;
        }

        /// <summary>
        /// pause sound by handle
        /// </summary>
        /// <param name="_handle"></param>
        /// <param name="_time"></param>
        public void Pause(int _handle, float _time = 0)
        {
            AudioNode _node = GetPlayingNode(_handle);
            _node?.Pause(_time);
        }

        /// <summary>
        /// resume sound by handle
        /// </summary>
        /// <param name="_handle"></param>
        /// <param name="_time"></param>
        public void Resume(int _handle, float _time = 0)
        {
            AudioNode _node = GetPlayingNode(_handle);
            _node?.Resume(_time);
        }

        /// <summary>
        /// stop sound by handle
        /// </summary>
        /// <param name="_handle"></param>
        /// <param name="_time"></param>
        public void Stop(int _handle, float _time = 0)
        {
            AudioNode _node = GetPlayingNode(_handle);
            _node?.Stop(_time);
        }

        /// <summary>
        /// pause all sound
        /// </summary>
        /// <param name="_time"></param>
        public void PauseAll(float _time = 0)
        {
            for (int i = 0; i < usingNodes.Count; i++)
            {
                usingNodes[i].Pause(_time);
            }
        }

        /// <summary>
        /// Resume all sound
        /// </summary>
        /// <param name="_time"></param>
        public void ResumeAll(float _time = 0)
        {
            for (int i = 0; i < usingNodes.Count; i++)
            {
                usingNodes[i].Resume(_time);
            }
        }

        /// <summary>
        /// stop all sound
        /// </summary>
        /// <param name="_time"></param>
        public void StopAll(float _time = 0)
        {
            for (int i = 0; i < usingNodes.Count; i++)
            {
                usingNodes[i].Stop(_time);
            }
        }
        #endregion
    }
}

