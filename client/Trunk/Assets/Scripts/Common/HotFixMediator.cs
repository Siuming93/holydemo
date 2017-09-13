
public abstract class HotFixMediator : AbstractMediator
{


    public HotFixMediator(string mediatorName) : base(mediatorName)
    {
    }

    //protected virtual void CheckLuaScript()
    //{
    //    scriptEnv = luaEnv.NewTable();
    //    LuaTable meta = luaEnv.NewTable();
    //    meta.Set("__index", luaEnv.Global);
    //    scriptEnv.SetMetaTable(meta);
    //    meta.Dispose();

    //    scriptEnv.Set("self", this);

    //    luaEnv.DoString(GetLuaScript(), NAME, scriptEnv);
    //}

    //~HotFixMediator()
    //{
    //    if (scriptEnv != null)
    //    {
    //        scriptEnv.Dispose();
    //    }
    //}

    //protected abstract string GetLuaScript();
}
