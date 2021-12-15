using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

namespace Battle
{
    public class AniMgr
    {
        static string[] states;
        const int StateCount = 3;
        public AnimationCollector animationCollector;

        static AniMgr()
        {
            states = new string[StateCount];
            for (int i = 0; i < StateCount; i++)
            {
                states[i] = "State_" + (i + 1);
            }
        }

        //当前正在播放的动画数据
        public AnimationData currAnimationData = null;

        public Animator _animator;

        // key，动画数据，value，绑定的动画状态
        private Dictionary<AnimationData, string> stateMap = new Dictionary<AnimationData, string>();

        //所有未使用的状态名字
        private readonly List<string> _unUsedState = new List<string>(StateCount);

        private AnimatorOverrideController _overrideController;
        // private float _playedTime;

        public AniMgr(Animator animator)
        {
            _unUsedState.AddRange(states);
            this._animator = animator;
            _overrideController = new AnimatorOverrideController();
            _overrideController.runtimeAnimatorController = animator.runtimeAnimatorController;
            animator.runtimeAnimatorController = _overrideController;

            //绑定defaultBlend
            var collector = animator.gameObject.GetComponent<AnimationCollector>();
            _overrideController["Idle"] = collector.Idle;
            _overrideController["Run"] = collector.Run;
            _overrideController["Run2"] = collector.Run2;
            animationCollector = _animator.gameObject.GetComponent<AnimationCollector>();
        }

        AnimationData FindAnimationData(string aniName)
        {
            var dic = animationCollector.dataDic;
            return dic[aniName];
        }

        public void Update(float deltaTime)
        {
            if (currAnimationData == null || string.IsNullOrEmpty(currAnimationData.stateName)) return;
            currAnimationData.playedTime += deltaTime; //记录当前播放的动画，播放了多久，秒为单位
            //说明到时间了
            if (currAnimationData.playedTime >= currAnimationData.currentTime)
            {
                if (currAnimationData.wrapMode == WrapMode.Loop
                    && currAnimationData.loopCount < currAnimationData.loopCountMax)
                {
                    currAnimationData.loopCount++;
                    currAnimationData.playedTime = 0;
                    _animator.speed = currAnimationData.currentSpeed;
                    _animator.CrossFade(currAnimationData.stateName,
                        0.2f, currAnimationData.layer, 0);
                }
                else
                {
                    currAnimationData.playedTime = 0;
                    currAnimationData.loopCount = 0;
                    currAnimationData = null;
                    _animator.speed = 1;
                    _animator.CrossFade("Default", 0.2f, 0, 0);
                }
            }
        }

        public void PlayAnimationByClipName(string aniName)
        {
            PlayAnimationData(FindAnimationData(aniName));
        }


        /// <summary>
        /// data应该是动作配置里的对象，但我们要使用角色身上的数据
        /// </summary>
        /// <param name="data"></param>
        public void PlayAnimationData(AnimationData aniData)
        {
            var data = FindAnimationData(aniData.clipName);
            data.currentSpeed = aniData.currentSpeed;
            data.currentTime = aniData.currentTime;

            if (IsPlaying(data)) return;
            string state = "";
            //判断是否绑定状态
            if (stateMap.ContainsKey(data))
            {
                state = data.stateName;
            }
            else
            {
                //判断 是否有空闲的状态
                if (_unUsedState.Count > 0)
                {
                    var stateName = _unUsedState[_unUsedState.Count - 1];
                    _unUsedState.RemoveAt(_unUsedState.Count - 1);
                    _overrideController[stateName] = data.clip;
                    // 数据绑定
                    data.stateName = stateName;
                    stateMap[data] = stateName; //缓存 
                    state = stateName;
                }
                else
                {
                    _overrideController["State"] = data.clip;
                    data.stateName = "State";
                    stateMap[data] = "State"; 
                    state = "State";
                }
            }

            data.playedTime = 0;
            _animator.speed = data.currentSpeed;
            _animator.CrossFade(state, 0.2f, data.layer, 0);
            currAnimationData = data;
        }

        private bool IsPlaying(AnimationData data)
        {
            if (currAnimationData == null || string.IsNullOrEmpty(currAnimationData.stateName)) return false;
            return currAnimationData.stateName == data.stateName;
        }

        public bool IsPlaying(string clipName)
        {
            return IsPlaying(FindAnimationData(clipName));
        }
    }
}