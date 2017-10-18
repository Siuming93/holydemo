using Monster.BaseSystem;

public class SkillProxy : BaseProxy
{
    public new const string NAME = "SkillProxy";

    public SkillVO attackVO;
    public SkillVO skill1VO;
    public SkillVO skill2VO;
    public SkillVO skill3VO;

    public bool isUseSkill = false;

    public SkillProxy()
        : base(NAME)
    {
        FakeVO();
    }
    public void UseSkill(SkillVO vo)
    {
        //todo 给服务器发数据

        if (isUseSkill)
            return;

        vo.lastUseMiliSceond = GameConfig.serverTime;
        isUseSkill = true;
        vp_Timer.In(vo.meta.duration, () => { isUseSkill = false; });

        SendNotification(NotificationConst.USE_SKILL, vo);
    }
    public void FakeVO()
    {
        attackVO = new SkillVO()
        {
            meta = SkillMeta.GetMeta("004"),
        };
        skill1VO = new SkillVO()
        {
            meta = SkillMeta.GetMeta("001"),
        };
        skill2VO = new SkillVO()
        {
            meta = SkillMeta.GetMeta("002"),
        };
        skill3VO = new SkillVO()
        {
            meta = SkillMeta.GetMeta("003"),
        };
    }
}
