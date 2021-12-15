
using System.IO;
using UnityEngine;
using XLua;

public class LuaMgr : Singleton<LuaMgr>
{
    public readonly LuaEnv luaEnv = new LuaEnv();
    public override void Init()
    {
        // 自定义lua代码字节加载器
        luaEnv.AddLoader(CustomLoader);
        //加载 全局模块
        // Require("Common.AbilityBehaviour");
        // Require("Common.AbilityUnitTargetTeam");
        // Require("Common.SoliderStatus");
        //luaEnv.DoString("require ('Test.Main')");
    }

    /// <summary>
    /// 根据技能脚本路径加载
    /// </summary>
    /// <param name="scriptPath">用.分隔路径，如 Abilitys.AbilityBehaviour </param>
    public void Require(string scriptPath)
    {
        luaEnv.DoString($"require ('{scriptPath}')", scriptPath);
    }
    public override void Dispose()
    {
        throw new System.NotImplementedException();
    }
    /// <summary>
    /// 相对于LuaScripts加载
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    private byte[] CustomLoader(ref string filePath)
    {
        //在编辑器下的路径，应该是/ 
        var p = filePath.Replace('.', System.IO.Path.DirectorySeparatorChar);
        string scriptStr = null;
#if UNITY_EDITOR
        scriptStr= File.ReadAllText(Application.dataPath + "/LuaScripts/" + p + ".lua");
            
#endif
        return System.Text.Encoding.UTF8.GetBytes(scriptStr);
    }
    // Start is called before the first frame update
   
}
