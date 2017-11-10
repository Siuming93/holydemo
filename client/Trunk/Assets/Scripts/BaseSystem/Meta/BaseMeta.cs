using System.Collections;
using System.Collections.Generic;

public abstract class BaseMeta<T> : IMeta where T : IMeta,new()
{
    protected static Dictionary<int, T> map = new Dictionary<int, T>();
    public static T GetMeta(int id)
    {
        T t;
        map.TryGetValue(id, out t);
        return t;
    }

    public int id;

    public static void AddMeta(Hashtable properties)
    {
        int id = MetaUtil.GetIntValue(properties, "id");
        if (!map.ContainsKey(id))
        {
            T t = new T();
            t.UpdateForm(properties);
            map.Add(id, t);
        }
    }

    public virtual void UpdateForm(Hashtable properties)
    {
        this.id = MetaUtil.GetIntValue(properties, "id");
    }

}
