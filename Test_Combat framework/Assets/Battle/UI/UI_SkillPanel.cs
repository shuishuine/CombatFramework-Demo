using Battle;
using Battle.Manager;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 技能UI的面板及数据管理
/// </summary>
public class UI_SkillPanel : MonoBehaviour
{
   /* private static UI_SkillPanel ins;
    public static UI_SkillPanel Ins
    {
        get
        {
            if (ins == null)
            {
                ins = new UI_SkillPanel();
            }
            return ins;
        }
    }

    private GameObject skillItem;
    private List<SkillConfig> skillList = new List<SkillConfig>();
    private Dictionary<int,GameObject> skillUIDic = new Dictionary<int, GameObject>();

    private void Awake()
    {
        ins = this;
        skillItem = Resources.Load<GameObject>("UI/Skill_Item");
    }

    private void Start()
    {
        skillList = ConfigsMgr.instance.GetSkillConfigs(1);
        ResetSkills();
    }

    /// <summary>
    /// 重置技能为初始
    /// </summary>
    public void ResetSkills()
    {
        foreach (var item in skillList)
        {
            if (item.index <= 4 && item.index >= 0)
            {
                var uiItem = Instantiate(skillItem);
                var sprite = Resources.Load<Sprite>("Skill_Icons/" + item.icon);
                uiItem.transform.SetParent(transform, false);
                skillUIDic.Add(item.index, uiItem);
                skillUIDic[item.index].GetComponent<Image>().sprite = sprite;
            }
        }
    }

    /// <summary>
    /// 更改技能
    /// </summary>
    /// <param name="xb"></param>
    /// <param name="nextId"></param>
    public void ChangeSkillUI(int xb,int nextId = -1)
    {
        if (nextId == -1)
        {
            foreach (var item in skillList)
            {
                if (item.index == xb)
                {
                    var sprite = Resources.Load<Sprite>("Skill_Icons/" + item.icon);
                    skillUIDic[xb].GetComponent<Image>().sprite = sprite;
                }
            }
            return;
        }
        
        foreach (var item in skillList)
        {
            if (item.skillId == nextId)
            {
                var sprite = Resources.Load<Sprite>("Skill_Icons/" + item.icon);
                skillUIDic[xb].GetComponent<Image>().sprite = sprite;
            }
        }
    }*/
}
