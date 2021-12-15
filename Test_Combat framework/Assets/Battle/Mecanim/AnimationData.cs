using System;
using UnityEngine;

namespace Battle
{
    [Serializable]
    public class AnimationData
    {
        public AnimationClip clip;
        public int layer = 0;
        public string stateName; //状态的名字
        public string clipName; //动画的名字
        public float timeOrigin; //动画的时长  自动生成
        public float speedOrigin = 1; //播放动画的速度  默认一直为 1
        public int loopCountMax = 0;// 当前动画最大循环次数
        public int loopCount = 0;//记录已经循环的次数
        public float currentTime; //当前动画的时长，需要配置的时长
        public float currentSpeed; //根据currentTime计算出来的速度，需要设置给animator

        public float playedTime; //动画已经播放的时长
        
        //将功能交给控制 器来设置，这里没用
        public WrapMode wrapMode = WrapMode.Once;

        public void ReCalculate()
        {
            this.currentTime = speedOrigin * timeOrigin / currentSpeed;
        }
    }
}