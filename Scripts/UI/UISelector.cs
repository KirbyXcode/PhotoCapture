using System;
using UnityEngine;
using UnityEngine.UI;

public class UISelector : BasePanel 
{
    private UIPreview previewPanel;

    public float intervalTime = 60f;
    private float elapsedTime;

    private void Start()
    {
        Button retakeButton = transform.Find("RetakePhoto").GetComponent<Button>();
        Button nextButton = transform.Find("Next").GetComponent<Button>();
        Button middleButton = transform.Find("Middle").GetComponent<Button>();
        Button rightButton = transform.Find("Right").GetComponent<Button>();
        Button leftButton = transform.Find("Left").GetComponent<Button>();

        retakeButton.onClick.AddListener(OnButtonRetake);
        nextButton.onClick.AddListener(OnButtonNext);
        middleButton.onClick.AddListener(OnButtonMiddle);
        rightButton.onClick.AddListener(OnButtonRight);
        leftButton.onClick.AddListener(OnButtonLeft);

        InitToggleListener();

        previewPanel = DictionaryExtension.TryGet(UIManager.Instance.PanelDict, UIPanelType.Preview) as UIPreview;
    }

    private void InitToggleListener()
    {
        Toggle[] toggles = transform.Find("PhotoBackground").GetComponentsInChildren<Toggle>();

        toggles[0].onValueChanged.AddListener(OnButtonBG01Select);
        toggles[1].onValueChanged.AddListener(OnButtonBG02Select);
        toggles[2].onValueChanged.AddListener(OnButtonBG03Select);
        toggles[3].onValueChanged.AddListener(OnButtonBG04Select);
        toggles[4].onValueChanged.AddListener(OnButtonBG05Select);
        toggles[5].onValueChanged.AddListener(OnButtonBG06Select);
        toggles[6].onValueChanged.AddListener(OnButtonBG07Select);
        toggles[7].onValueChanged.AddListener(OnButtonBG08Select);
    }

    private void OnButtonBG01Select(bool isOn)
    {
        if(isOn)
        {
            Global.background = Background.NewLook;
            previewPanel.SetBackgournd();
        } 
    }

    private void OnButtonBG02Select(bool isOn)
    {
        if (isOn)
        {
            Global.background = Background.Riverside;
            previewPanel.SetBackgournd();
        }
    }

    private void OnButtonBG03Select(bool isOn)
    {
        if (isOn)
        {
            Global.background = Background.Surface;
            previewPanel.SetBackgournd();
        }
    }

    private void OnButtonBG04Select(bool isOn)
    {
        if (isOn)
        {
            Global.background = Background.MorningSun;
            previewPanel.SetBackgournd();
        }
    }

    private void OnButtonBG05Select(bool isOn)
    {
        if (isOn)
        {
            Global.background = Background.Buildings;
            previewPanel.SetBackgournd();
        }
    }

    private void OnButtonBG06Select(bool isOn)
    {
        if (isOn)
        {
            Global.background = Background.Bridge;
            previewPanel.SetBackgournd();
        }
    }

    private void OnButtonBG07Select(bool isOn)
    {
        if (isOn)
        {
            Global.background = Background.River;
            previewPanel.SetBackgournd();
        }
    }

    private void OnButtonBG08Select(bool isOn)
    {
        if (isOn)
        {
            Global.background = Background.Happyness;
            previewPanel.SetBackgournd();
        }
    }

    private void OnButtonRetake()
    {
        UIManager.Instance.PopPanel();
        UIManager.Instance.PopPanel();
        UIManager.Instance.PushPanel(UIPanelType.Camera);
        UIManager.Instance.PushPanel(UIPanelType.Capture);
    }

    private void OnButtonNext()
    {
        UIManager.Instance.PopPanel();
        UIManager.Instance.PushPanel(UIPanelType.Email);
    }

    private void OnButtonLeft()
    {
        previewPanel.SetPhotoPosition(new Vector3(-320, 0, 0));
    }

    private void OnButtonRight()
    {
        previewPanel.SetPhotoPosition(new Vector3(320, 0, 0));
    }

    private void OnButtonMiddle()
    {
        previewPanel.SetPhotoPosition(Vector3.zero);
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
