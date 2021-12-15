using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor;
using UnityEngine;

namespace Battle
{
    /// <summary>
    /// 挂在预制件上，为我们访问动画clip提供方便
    /// 动画clip和预制件绑在一起，比较方便；当然，为了热更新，其它方式也行
    /// </summary>
    public class AnimationCollector : MonoBehaviour
    {
        public AnimationClip Idle;
        public AnimationClip Run;
        public AnimationClip Run2;
        // public AnimationClip LitHit;
        
        public AnimationClip[] clips;
        // 角色动画所在的目录
        public string clipAssetDirPath;
        public AnimationData[] datas;
       
        public Dictionary<string, AnimationData> dataDic;

        private void Awake()
        {
            dataDic=new Dictionary<string, AnimationData>();
            foreach (var animationData in datas)
            {
                dataDic[animationData.clipName] = animationData;
            }
        }

        [ContextMenu("重新计算动画的速度与时长")]
        void ReCalculateSpeed()
        {
            foreach (var animationData in datas)
            {
                animationData.ReCalculate();
            }
        }
        
        

        [ContextMenu("获取指定路径[clipAssetDirPath]下所有的动画资源")]
        [Conditional("UNITY_EDITOR")]
        void SearchClips()
        {
           
            var guids = AssetDatabase.FindAssets("t:AnimationClip", new[] {"Assets/"+clipAssetDirPath  });
            clips = new AnimationClip[guids.Length];
            datas=new AnimationData[guids.Length];
           
            int i = 0;
            foreach (var guid in guids)
            {
                var p = AssetDatabase.GUIDToAssetPath(guid);
                var clip = AssetDatabase.LoadAssetAtPath<AnimationClip>(p);
             // var evets=   AnimationUtility.GetAnimationEvents(clip);
             //    for (var i1 = 0; i1 < evets.Length; i1++)
             //    {
             //        evets[i1]=new AnimationEvent();
             //        
             //    }
             //    AnimationUtility.SetAnimationEvents(clip,evets);
           
                clips[i] = clip;
                // 构造动画数据
                var data=new AnimationData(){clipName = clip.name,speedOrigin = 1,timeOrigin = clip.length};
                data.clip = clip;
                data.currentTime = data.timeOrigin;
                data.currentSpeed = data.speedOrigin;
                datas[i] = data;
                // dataDic[data.clipName] = data;
                i++;
            }
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

       
    }
}