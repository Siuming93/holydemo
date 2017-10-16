
using System.Collections;
using System.Collections.Generic;

public class SkillDamageMeta : BaseMeta<SkillDamageMeta>
{
    public float time;
    public int damage;
    public override void UpdateForm(Hashtable properties)
    {
        this.time = MetaUtil.GetFloatValue(properties, "time");
        this.damage = MetaUtil.GetIntValue(properties, "damage");
    }

    public static List<KeyValuePair<string, Hashtable>> FakeMeta()
    {
        var list = new List<KeyValuePair<string, Hashtable>>();

        list.Add(new KeyValuePair<string, Hashtable>("001", new Hashtable()
            {
                {"id","001"},
                {"time","1"},
                {"damage","300"},
            }));

        list.Add(new KeyValuePair<string, Hashtable>("002", new Hashtable()
            {
                {"id","002"},
                {"time","1"},
                {"damage","500"},
            }));

        list.Add(new KeyValuePair<string, Hashtable>("003", new Hashtable()
            {
                {"id","003"},
                {"time","1"},
                {"damage","1000"},
            }));

        list.Add(new KeyValuePair<string, Hashtable>("004", new Hashtable()
            {
                {"id","004"},
                {"time","1"},
                {"damage","100"},
            }));

        return list;
    }
}
