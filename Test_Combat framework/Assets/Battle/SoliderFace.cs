using System;
using System.Collections.Generic;
using System.Diagnostics;
using Battle.Manager;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

namespace Battle
{
    /// <summary>
    /// 不知道，如何运行时动态添加任务；所以，让一颗总树管理所有的技能树不妥
    /// 战斗单位
    /// 所以，还是让所有的树，挂在单位身上，统一由soliderFace管理；树之间的数据，靠soliderFace管理共享；
    /// </summary>
    public class SoliderFace : MonoBehaviour
    {
        public AniMgr aniMgr;
        public string heroName;

        private string str;

        internal bool SkillInput = true;
        internal bool MouseInput = true;
        internal bool NavMeshMove = true;
        internal NavMeshAgent agent;
        internal Transform q_Attack;

        private float speedNow = 0;
        private Transform effectParent;          //特效父级

        private SoliderData soliderData;         //当前角色的信息
        private Dictionary<int, SkillTree> nowSkillTrees = new Dictionary<int, SkillTree>();        //所有显示技能 下标为键
        private Dictionary<int, SkillTree> skillTrees = new Dictionary<int, SkillTree>();  //该角色所有技能 下标为键
        private LinkedList<BuffTree> buffTrees = new LinkedList<BuffTree>();               //身上的所有buff

        internal SoliderData SoliderData { get => soliderData; }


        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            aniMgr = new AniMgr(GetComponent<Animator>());
            effectParent = transform.Find("aureole");
            q_Attack = null;

            InitConfigs();
        }

        private void Start()
        {
            
        }

        private void Update()
        {
            aniMgr.Update(Time.deltaTime);
        }

        private void FixedUpdate()
        {
            CheckMove();
        }


        /// <summary>
        /// 初始化所有配置
        /// </summary>
        private void InitConfigs()
        {
            
            //角色信息
            var config = ConfigsMgr.instance.GetSoliderConfig(heroName);
            if (config == null) return;
            
            //技能信息
            var skillconfigs = ConfigsMgr.instance.GetSkillConfigs(config.heroId);
            foreach (var item in skillconfigs)
            {
                var skillTree = gameObject.AddComponent<SkillTree>();
                skillTree.StartWhenEnabled = false;
                skillTree.ExternalBehavior = Resources.Load<ExternalBehavior>("Skill_Trees/" + item.skillId);
                skillTree.skillConfig = item;
                skillTree.BehaviorName = item.skillName;
                skillTrees.Add(item.index, skillTree);
                if (item.index <= 4) nowSkillTrees[item.index] = skillTree;
            }
            soliderData = new SoliderData(config);
            agent.speed = config.speed;
        }

        /// <summary>
        /// 移动
        /// </summary>
        /// <param name="h"></param>
        /// <param name="v"></param>
        public void Move(Vector3 point)
        {
            if (agent && agent.isActiveAndEnabled && NavMeshMove)
            {
                agent.SetDestination(point);
            }
        }

        /// <summary>
        /// 播放技能树 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="tarObj"></param>
        public void ReleaseSkill(int index,GameObject tarObj = null)
        {
            var tree = nowSkillTrees[index];
            if (tree != null)
            {
                tree.EnableBehavior();
                if (tarObj)
                {
                    tree.SetVariable("targetObj", new SharedGameObject());
                    tree.SetVariableValue("targetObj", tarObj);
                }
            }
            else
            {
                UnityEngine.Debug.Log("该下表位置没有技能");
            }
        }

        /// <summary>
        /// 给指定下表树发送消息
        /// </summary>
        /// <param name="index"></param>
        /// <param name="even"></param>
        public void SendEventSkill(int index,string even)
        {
            var tree = nowSkillTrees[index];
            tree.SendEvent(even);
        }

        /// <summary>
        /// 受伤
        /// </summary>
        /// <param name="power"></param>
        public void GetDamage(float power)
        {
            aniMgr.PlayAnimationByClipName("n2017_hit");
            this.soliderData.config.blood -= power;
            UnityEngine.Debug.Log(this.soliderData.config.heroName + " 当前血量:  " + this.soliderData.config.blood);
        }

        /// <summary>
        /// 改变技能的下一个形态
        /// </summary>
        /// <param name="xb"></param>
        /// <param name="nextId"></param>
        public void ChangeSkillUI(int xb, int nextId)
        {
            UI_SkillMgr.instance.Change_SkillUI(xb, nextId);
        }

        /// <summary>
        /// 添加特效
        /// </summary>
        /// <param name="effectName"></param>
        public void AddEffect(string effectName)
        {
            var effect = GameObject.Instantiate(ResourcesMgr.instance.Load<GameObject>("Effect/" + effectName)) ;
            effect.name = effectName;
            effect.transform.SetParent(effectParent);
            effect.transform.localPosition = Vector3.zero;
        }

        /// <summary>
        /// 移除特效
        /// </summary>
        /// <param name="effectName"></param>
        public void RemoveEffect(string effectName)
        {
            
            var trans = effectParent.GetComponentsInChildren<Transform>();
            for (int i = 0; i < trans.Length; i++)
            {
                if (trans[i].name == effectName)
                {
                    Destroy(trans[i].gameObject);
                }
            }
        }

        /// <summary>
        /// 添加buff
        /// </summary>
        /// <param name="buffName"></param>
        /// <param name="belongSkill"></param>
        public void AddBuff(string buffName,SkillTree belongSkill)
        {
            var buffTree = gameObject.AddComponent<BuffTree>();
            var prefabTree = Resources.Load<ExternalBehavior>("Buff_Trees/" + buffName);
            buffTree.Init(buffName,belongSkill);
            buffTree.ExternalBehavior = Instantiate(prefabTree);
            buffTrees.AddLast(buffTree);
        }

        /// <summary>
        /// 移除buff
        /// </summary>
        /// <param name="buffName"></param>
        /// <param name="skillTree"></param>
        public void RemoveBuff(string buffName, SkillTree skillTree)
        {
            BuffTree removeBuff = null;
            foreach (var buffTree in buffTrees)
            {
                if (buffTree.buffName == buffName && buffTree.skillTree == skillTree)
                {
                    removeBuff = buffTree;
                    break;
                }
            }

            if (removeBuff)
            {
                buffTrees.Remove(removeBuff);
            }
        }

        private int GetSkillIndex_ById(int id)
        {
            if (skillTrees.TryGetValue(id, out var tree))
            {
                return tree.skillConfig.index;
            } 
            return -1;
        }

        private void CheckMove()
        {
            if (gameObject && agent)
            {
                if (agent.isActiveAndEnabled && agent.remainingDistance >= 0.1f)
                {
                    speedNow = Mathf.Lerp(speedNow, agent.speed, Time.deltaTime * 5);
                    aniMgr._animator.SetFloat("Blend", speedNow);
                }
                else
                {
                    speedNow = Mathf.Lerp(speedNow, 0, Time.deltaTime * 5);
                    aniMgr._animator.SetFloat("Blend", speedNow);
                }
            }
        }
    }
}