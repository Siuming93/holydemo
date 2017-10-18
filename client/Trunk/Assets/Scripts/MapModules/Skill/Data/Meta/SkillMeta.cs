using System.Collections;
using System.Collections.Generic;

public class SkillMeta : BaseMeta<SkillMeta>
{
    public float cd;
    public float duration;
    public SkillAnimationMeta animationMeta;
    public SkillDamageMeta damageMeta;
    public SkillEffectMeta effectMeta;
    public override void UpdateForm(Hashtable properties)
    {
        base.UpdateForm(properties);
        this.cd = MetaUtil.GetFloatValue(properties, "cd");
        this.duration = MetaUtil.GetFloatValue(properties, "duration");
        this.damageMeta = SkillDamageMeta.GetMeta(MetaUtil.GetStringValue(properties, "damageId"));
        this.effectMeta = SkillEffectMeta.GetMeta(MetaUtil.GetStringValue(properties, "effectId"));
        this.animationMeta = SkillAnimationMeta.GetMeta(MetaUtil.GetStringValue(properties, "animationId"));
    }

    public static List<KeyValuePair<string, Hashtable>> FakeMeta()
    {
        var list = new List<KeyValuePair<string, Hashtable>>();


        list.Add(new KeyValuePair<string, Hashtable>("004", new Hashtable()
        {
            {"id", "004"},
            {"cd", "0"},
            {"duration", "2"},
            {"animationId", "004"},
            {"effectId", ""},
            {"damageId", "004"},
        }));

        list.Add(new KeyValuePair<string, Hashtable>("001", new Hashtable()
        {
            {"id", "001"},
            {"cd", "5"},
            {"duration", "2"},
            {"animationId", "001"},
            {"effectId", ""},
            {"damageId", "001"},
        }));

        list.Add(new KeyValuePair<string, Hashtable>("002", new Hashtable()
        {
            {"id", "002"},
            {"cd", "6"},
            {"duration", "2"},
            {"animationId", "002"},
            {"effectId", ""},
            {"damageId", "002"},
        }));

        list.Add(new KeyValuePair<string, Hashtable>("003", new Hashtable()
        {
            {"id", "003"},
            {"cd", "12"},
            {"duration", "2"},
            {"animationId", "003"},
            {"effectId", ""},
            {"damageId", "003"},
        }));

        return list;
    }
}
