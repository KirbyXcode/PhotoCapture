using System.Collections;
using UnityEngine;
using UnityEngine.UI;
//using DG.Tweening;

public class UITimer : BasePanel 
{
    private Text timerText;
    public Text TimerText
    {
        get
        {
            if (timerText == null)
            {
                timerText = GetComponentInChildren<Text>();
            }
            return timerText;
        }
    }

    private CanvasGroup cg;
    public CanvasGroup Cg
    {
        get
        {
            if (cg == null)
            {
                cg = GetComponent<CanvasGroup>();
            }
            return cg;
        }
    }

    public override void OnEnter()
    {
        base.OnEnter();
        StartCoroutine(StartTime());
    }

    public override void OnExit()
    {
        //ScreenShot();

        MattingTest();
        base.OnExit();
    }

    private void MattingTest()
    {
        Texture2D texture = Resources.Load<Texture2D>("img");

        Camera.main.GetComponent<UChromaKey>().chromaKeyTexture = texture;

        Global.sprite = Sprite.Create(texture, new Rect(0, 0, 1024, 512), Vector2.zero);
    }

    private IEnumerator StartTime()
    {
        //PlayAnimation();
        for (int i = 3; i >= 0; i--)
        {
            SetTime(i);
            yield return new WaitForSeconds(1);
        }

        //退出Timer面板
        UIManager.Instance.PopPanel();

        //-----------------------------------

        //退出Capture面板
        UIManager.Instance.PopPanel();
        //退出Camera面板
        UIManager.Instance.PopPanel();

        UIManager.Instance.PushPanel(UIPanelType.Preview);
        UIManager.Instance.PushPanel(UIPanelType.Selector);
    }

    private void SetTime(int time)
    {
        TimerText.text = time.ToString();
    }

    ////private void PlayAnimation()
    ////{
    ////    Cg.alpha = 1;
    ////    TimerText.transform.localScale = Vector3.one;
    ////    TimerText.transform.DOScale(2f, 1f).SetLoops(4, LoopType.Restart);
    ////    Cg.DOFade(0f, 1f).SetLoops(4, LoopType.Restart);
    ////}

    private void ScreenShot()
    {
        Texture2D texture = new Texture2D(1858, 1018, TextureFormat.RGB24, false);
        texture.ReadPixels(new Rect(31, 1111, 1858, 1018), 0, 0, false);
        texture.Apply();

        Camera.main.GetComponent<UChromaKey>().chromaKeyTexture = texture;

        Global.sprite = Sprite.Create(texture, new Rect(0, 0, 1858, 1018), new Vector2(0, 0));

        //退出Capture面板
        UIManager.Instance.PopPanel();
        //退出Camera面板
        UIManager.Instance.PopPanel();

        UIManager.Instance.PushPanel(UIPanelType.Preview);
        UIManager.Instance.PushPanel(UIPanelType.Selector);
    }
}
