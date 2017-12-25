using UnityEngine;
using UnityEngine.UI;

public class UIComplete : BasePanel 
{
    public float intervalTime = 60f;
    private float elapsedTime;

    private void Start()
    {
        Button button = transform.Find("Retake").GetComponent<Button>();
        button.onClick.AddListener(OnButtonRetake);
    }

    private void OnButtonRetake()
    {
        UIManager.Instance.PopPanel();
        UIManager.Instance.PopPanel();
        Global.ResetData();
        UIManager.Instance.PushPanel(UIPanelType.Camera);
        UIManager.Instance.PushPanel(UIPanelType.Capture);
    }

    public override void OnEnter()
    {
        base.OnEnter();

        elapsedTime = intervalTime;
    }

    public override void OnExit()
    {
        base.OnExit();

        elapsedTime = intervalTime;
    }

    private void Update()
    {
        elapsedTime -= Time.deltaTime;
        if (elapsedTime <= 0)
        {
            UIManager.Instance.PopPanel();
            UIManager.Instance.PopPanel();
            UIManager.Instance.PushPanel(UIPanelType.Pending);
            UIManager.Instance.PushPanel(UIPanelType.Ready);
        }
    }
}
