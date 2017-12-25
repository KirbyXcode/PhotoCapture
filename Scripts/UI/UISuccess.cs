public class UISuccess : BasePanel 
{
    public override void OnEnter()
    {
        base.OnEnter();
        UIManager.Instance.PushPanel(UIPanelType.Complete);
    }

    public override void OnExit()
    {
        base.OnExit();
    }
}
