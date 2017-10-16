using System.Collections;
using System.Collections.Generic;

public class SkillAnimationMeta : BaseMeta<SkillAnimationMeta>
{
    public float time;
    public string name;
    public override void UpdateForm(Hashtable properties)
    {
        this.time = MetaUtil.GetFloatValue(properties, "time");
        this.name = MetaUtil.GetStringValue(properties, "name");
    }

    public static List<KeyValuePair<string, Hashtable>> FakeMeta()
    {
        var list = new List<KeyValuePair<string, Hashtable>>();

        list.Add(new KeyValuePair<string, Hashtable>("001", new Hashtable()
            {
                {"id","001"},
                {"time","1"},
                {"name","Skill1"},
            }));

        list.Add(new KeyValuePair<string, Hashtable>("002", new Hashtable()
            {
                {"id","002"},
                {"time","1"},
                {"name","Skill2"},
            }));

        list.Add(new KeyValuePair<string, Hashtable>("003", new Hashtable()
            {
                {"id","003"},
                {"time","1"},
                {"name", "Skill3"},
            }));

        list.Add(new KeyValuePair<string, Hashtable>("004", new Hashtable()
            {
                {"id","004"},
                {"time","1"},
                {"name", "Attack"},
            }));

        return list;
    }
}
