
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesMgr : Singleton<ResourcesMgr>
{
    private Dictionary<string, Object> objs = new Dictionary<string, Object>();
    public T Load<T>(string path)
    {
        if (objs.TryGetValue(path,out var obj))
        {
            return (T)(object)obj;
        }
       var obj2 = Resources.Load(path);
        objs.Add(path, obj2);
        return (T)((object)obj2);
    }


    public override void Dispose()
    {
        
    }

    public override void Init()
    {
        
    }
}
