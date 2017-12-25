using UnityEngine;
using UnityEngine.UI;

public class UICapture : BasePanel 
{
    private Button button;

    public float intervalTime = 60f;
    private float elapsedTime;

    private void Start()
    {
        button = transform.Find("TakePhoto").GetComponent<Button>();
        button.onClick.AddListener(OnButtonTakePhoto);
    }

    private void OnButtonTakePhoto()
    {
        UIManager.Instance.PushPanel(UIPanelType.Timer);
        button.interactable = false;
    }

    public override void OnEnter()
    {
        base.OnEnter();

        elapsedTime = intervalTime;

        if (button != null)
        {
            button.interactable = true;
        }
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
