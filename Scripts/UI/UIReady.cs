using UnityEngine.UI;

public class UIReady : BasePanel 
{
    private void Start()
    {
        Button button = transform.Find("StartPhoto").GetComponent<Button>();
        button.onClick.AddListener(OnStartPhoto);
    }

    private void OnStartPhoto()
    {
        UIManager.Instance.PopPanel();
        UIManager.Instance.PopPanel();
        UIManager.Instance.PushPanel(UIPanelType.Camera);
        UIManager.Instance.PushPanel(UIPanelType.Capture);
    }

    public override void OnEnter()
    {
        base.OnEnter();
    }

    public override void OnExit()
    {
        base.OnExit();
    }
}
