using System;
using System.Collections;
using System.Collections.Generic;

public class MetaManager
{
    private Dictionary<string, Action<Hashtable>> _map;
    public MetaManager()
    {
        _map = new Dictionary<string, Action<Hashtable>>();
        _map.Add("SkillAnimation", SkillAnimationMeta.AddMeta);
        _map.Add("SkillDamage", SkillAnimationMeta.AddMeta);
        _map.Add("Skill", SkillMeta.AddMeta);

    }
    public void AddMeta(Dictionary<string, List<KeyValuePair<string, Hashtable>>> map)
    {
        foreach (var metaMap in map)
        {
            Action<Hashtable> addAction;
            if (_map.TryGetValue(metaMap.Key, out addAction))
            {
                var list = metaMap.Value;
                foreach (var idPari in list)
                {
                    addAction.Invoke(idPari.Value);
                }
            }
        }
    }

    public static Dictionary<string,List<KeyValuePair<string, Hashtable>>> FakeMeta()
    {
        var map = new Dictionary<string, List<KeyValuePair<string, Hashtable>>>();

        map.Add("SkillAnimation", SkillAnimationMeta.FakeMeta());
        map.Add("SkillDamage", SkillDamageMeta.FakeMeta());
        map.Add("Skill", SkillMeta.FakeMeta());

        return map;
    }
}
