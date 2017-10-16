using System.Collections;
using System.Collections.Generic;

public abstract class BaseMeta<T> : IMeta where T : new()
{
    protected static Dictionary<string, T> map = new Dictionary<string, T>();
    public static T GetMeta(string id)
    {
        T t;
        map.TryGetValue(id, out t);
        return t;
    }

    public string id;

    public static void AddMeta(Hashtable properties)
    {
        string id = MetaUtil.GetStringValue(properties, "id");
        if (!map.ContainsKey(id))
        {
            T t = new T();
            map.Add(id, t);
        }
    }

    public abstract void UpdateForm(Hashtable properties);

}
