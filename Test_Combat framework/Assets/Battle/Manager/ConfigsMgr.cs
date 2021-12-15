using System.Collections.Generic;
using UnityEngine;

namespace Battle.Manager
{
    public class ConfigsMgr : Singleton<ConfigsMgr>
    {
        public List<SkillConfig> skillConfigs; //所有技能的配置
        public List<SoliderConfig> soliderConfigs; //所有角色的配置

        public override void Init()
        {
            LuaMgr.instance.Require("SkillConfigs");
            skillConfigs = LuaMgr.instance.luaEnv.Global.Get<List<SkillConfig>>("SkillConfigs");
            //
            LuaMgr.instance.Require("SoliderConfigs");
            soliderConfigs = LuaMgr.instance.luaEnv.Global.Get<List<SoliderConfig>>("SoliderConfigs");
        }

        public SoliderConfig GetSoliderConfig(string name)
        {
            foreach (var soliderConfig in soliderConfigs)
            {
                if (soliderConfig.heroName == name)
                {
                    return soliderConfig;
                }
            }

            return null;
        }

        public List<SkillConfig> GetSkillConfigs(int heroId)
        {
            List<SkillConfig> skillConfigs = new List<SkillConfig> ();
            foreach (var skillConfig in this.skillConfigs)
            {
                if (skillConfig.heroId == heroId)
                {
                    skillConfigs.Add(skillConfig);
                }
            }

            return skillConfigs;
        }

        public override void Dispose()
        {
            throw new System.NotImplementedException();
        }
    }
}