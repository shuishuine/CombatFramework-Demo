using Battle;
using Battle.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_SkillMgr : Singleton<UI_SkillMgr>
{
    private Transform uiParent;
    private GameObject uiObj;
    private List<SkillConfig> skillList = new List<SkillConfig>();
    private Dictionary<int,Image> uiDic = new Dictionary<int, Image>();

    public void Init(GameObject uiItem,Transform uiParent,int heroId)
    {
        this.uiParent = uiParent;
        this.uiObj = uiItem;
        this.skillList = ConfigsMgr.instance.GetSkillConfigs(heroId);
        Reset();
    }

    public void Reset()
    {
        foreach (var item in skillList)
        {
            if (item.index >= 0 && item.index <= 4)
            {
                var obj = GameObject.Instantiate(uiObj);
                obj.transform.parent = uiParent;

                var image = obj.GetComponent<Image>();
                image.sprite = Resources.Load<Sprite>("Skill_Icons/" + item.icon); ;
                
                uiDic.Add(item.index, image);
            }
        }
    }

    public override void Init()
    {
        
    }

    public override void Dispose()
    {
        
    }

    public void Change_SkillUI(int xb,int nextId)
    {
        uiDic[xb].sprite = Resources.Load<Sprite>("Skill_Icons/" + nextId);
    }
}
