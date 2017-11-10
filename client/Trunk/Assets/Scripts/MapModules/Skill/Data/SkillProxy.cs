using Monster.BaseSystem;
using Monster.Protocol;

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
        RegisterMessageHandler(MsgIDDefine.ScPlayerUseSkill, OnScPlayerUseSkill);
    }

    private void OnScPlayerUseSkill(object data)
    {
        ScPlayerUseSkill msg = data as ScPlayerUseSkill;;
        var vo = GetSkillVO(msg.skillId);
        vo.lastUseMiliSceond = GameConfig.serverTime;
        isUseSkill = true;
        vp_Timer.In(vo.meta.duration, () => { isUseSkill = false; });

        SendNotification(NotificationConst.USE_SKILL, vo);
    }

    public void UseSkill(SkillVO vo)
    {
        //todo 给服务器发数据

        if (isUseSkill)
            return;

        netManager.SendMessage(new CsPlayerUseSkill()
        {
            skillId = vo.meta.id,
        });
    }

    private SkillVO GetSkillVO(int id)
    {
        if (attackVO.meta.id == id)
            return attackVO;
        if (skill1VO.meta.id == id)
            return skill1VO;
        if (skill2VO.meta.id == id)
            return skill2VO;
        if (skill3VO.meta.id == id)
            return skill3VO;
        return null;
    }

    public void FakeVO()
    {
        attackVO = new SkillVO()
        {
            meta = SkillMeta.GetMeta(004),
        };
        skill1VO = new SkillVO()
        {
            meta = SkillMeta.GetMeta(001),
        };
        skill2VO = new SkillVO()
        {
            meta = SkillMeta.GetMeta(002),
        };
        skill3VO = new SkillVO()
        {
            meta = SkillMeta.GetMeta(003),
        };
    }
}
