
-- skillId        技能唯一编号
-- heroId          所属英雄编号
-- skillName        技能名称
-- skillTreePath        技能树路径
-- bulletPath           技能子弹路径
-- skillTreeBulletPath      技能子弹树路径
-- icon         ui路径
-- power        伤害值
-- distance     子弹距离
-- speed        子弹速度
-- many         子弹可穿透数量
-- index        技能下标
-- lastId       上一个技能编号
-- nextId       下一个技能编号

SkillConfigs = {
    {
        skillId = 1001,
        heroId = 1,
        skillName="天音波",
        icon="1001",
        index = 1,
    },
    {
        skillId = 1002,
        heroId = 1,
        skillName="振荡电磁波",
        icon="1002",
        index = 2;
    },
    -- {
    --     skillId = 1003,
    --     heroId = 1,
    --     skillName="嚼火者手雷",
    --     skillTreePath= "Skill_Tree_1003",  
    --     icon="1003",
    --     index = 3;
    -- },
    {
        skillId = 1004,
        heroId = 1,
        skillName="超究极死神飞弹",
        icon="1004",
        index = 4;
    },

    {
        skillId = 1006,
        heroId = 1,
        skillName="普通攻击",
        icon="",
        index = -1,
    },
}
