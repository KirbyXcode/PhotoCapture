using System.Collections;
using UnityEngine.UI;
using WindowsInput;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System;
using System.Text.RegularExpressions;
using UnityEngine;
using System.IO;
using DG.Tweening;

public class UIEmail : BasePanel 
{
    private InputField input;

    private SmtpClient smtpClinet;
    private const string SMTP = "smtp.qq.com";
    private const string AUTHNUM = "srbgkaxuggiabeff";

    private const string SUBJECT = "魔幻背景合成照";
    private const string FROM = "634204583@qq.com";
    private const string BODY = "请查收照片";
    private string path;
    private CanvasGroup messageCG;

    public float intervalTime = 60f;
    private float elapsedTime;

    private void Start()
    {
        input = transform.Find("InputField").GetComponent<InputField>();
        InitListener();

        SetSmtpServer();

        messageCG = transform.Find("Message").GetComponent<CanvasGroup>();
        messageCG.gameObject.SetActive(false);
    }

    private void InitListener()
    {
        Button deleteButton = transform.Find("Delete").GetComponent<Button>();
        deleteButton.onClick.AddListener(OnButtonDelete);

        Button deleteAllButton = transform.Find("DeleteAll").GetComponent<Button>();
        deleteAllButton.onClick.AddListener(OnButtonDeleteAll);

        Button sendButton = transform.Find("Send").GetComponent<Button>();
        sendButton.onClick.AddListener(OnButtonSend);

        Button backButton = transform.Find("Back").GetComponent<Button>();
        backButton.onClick.AddListener(OnButtonBack);

        Button[] buttons = transform.Find("Keyboard").GetComponentsInChildren<Button>();
        buttons[0].onClick.AddListener(OnButtonOne);
        buttons[1].onClick.AddListener(OnButtonTwo);
        buttons[2].onClick.AddListener(OnButtonThree);
        buttons[3].onClick.AddListener(OnButtonFour);
        buttons[4].onClick.AddListener(OnButtonFive);
        buttons[5].onClick.AddListener(OnButtonSix);
        buttons[6].onClick.AddListener(OnButtonSeven);
        buttons[7].onClick.AddListener(OnButtonEight);
        buttons[8].onClick.AddListener(OnButtonNine);
        buttons[9].onClick.AddListener(OnButtonZero);

        buttons[10].onClick.AddListener(OnButtonQ);
        buttons[11].onClick.AddListener(OnButtonW);
        buttons[12].onClick.AddListener(OnButtonE);
        buttons[13].onClick.AddListener(OnButtonR);
        buttons[14].onClick.AddListener(OnButtonT);
        buttons[15].onClick.AddListener(OnButtonY);
        buttons[16].onClick.AddListener(OnButtonU);
        buttons[17].onClick.AddListener(OnButtonI);
        buttons[18].onClick.AddListener(OnButtonO);
        buttons[19].onClick.AddListener(OnButtonP);

        buttons[20].onClick.AddListener(OnButtonA);
        buttons[21].onClick.AddListener(OnButtonS);
        buttons[22].onClick.AddListener(OnButtonD);
        buttons[23].onClick.AddListener(OnButtonF);
        buttons[24].onClick.AddListener(OnButtonG);
        buttons[25].onClick.AddListener(OnButtonH);
        buttons[26].onClick.AddListener(OnButtonJ);
        buttons[27].onClick.AddListener(OnButtonK);
        buttons[28].onClick.AddListener(OnButtonL);
        buttons[29].onClick.AddListener(OnButtonMinus);

        buttons[30].onClick.AddListener(OnButtonZ);
        buttons[31].onClick.AddListener(OnButtonX);
        buttons[32].onClick.AddListener(OnButtonC);
        buttons[33].onClick.AddListener(OnButtonV);
        buttons[34].onClick.AddListener(OnButtonB);
        buttons[35].onClick.AddListener(OnButtonN);
        buttons[36].onClick.AddListener(OnButtonM);
        buttons[37].onClick.AddListener(OnButtonComma);
        buttons[38].onClick.AddListener(OnButtonPeriod);
        buttons[39].onClick.AddListener(OnButtonAt);
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

    private void SetSmtpServer()
    {
        smtpClinet = new SmtpClient(SMTP);
        smtpClinet.Port = 587;
        smtpClinet.Credentials = new NetworkCredential(FROM, AUTHNUM) as ICredentialsByHost;
        smtpClinet.EnableSsl = true;

        ServicePointManager.ServerCertificateValidationCallback =
             delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
             { return true; };
    }

    private void SendEmail(string to)
    {
        bool b = CheckEmailFormat(to);

        if(!b) return;

        //----ScreenShot();

        //设置邮件信息
        MailMessage mail = new MailMessage(FROM, to);
        mail.Subject = SUBJECT;
        mail.Body = BODY;

        //设置附件信息
        if (!string.IsNullOrEmpty(path))
        {
            Attachment attacment = new Attachment(path);
            mail.Attachments.Add(attacment);
        }

        try
        {
            smtpClinet.Send(mail);
            Global.returnCode = ReturnCode.Success;
        }
        catch (Exception)
        {
            Global.returnCode = ReturnCode.Failure;
        }

        UIManager.Instance.PopPanel();
        UIManager.Instance.PopPanel();
        UIManager.Instance.PushPanel(UIPanelType.Email);
        UIManager.Instance.PushPanel(UIPanelType.Sending);
        UIManager.Instance.PushPanel(UIPanelType.Mask);
    }

    private bool CheckEmailFormat(string address)
    {
        Regex r = new Regex(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        if (r.IsMatch(address))
        {
            HideMessage();
            return true;
        }
        else
        {
            ShowMessage();
            return false;
        }
    }

    private void ScreenShot()
    {
        Texture2D texture = new Texture2D(1858, 1018, TextureFormat.RGB24, false);
        texture.ReadPixels(new Rect(31, 1111, 1858, 1018), 0, 0, false);
        texture.Apply();
        byte[] bytes = texture.EncodeToJPG();
        path = Application.streamingAssetsPath + "/" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg";
        File.WriteAllBytes(path, bytes);
    }

    private void HideMessage()
    {
        if (!messageCG.gameObject.activeInHierarchy) return;
        messageCG.alpha = 1;
        messageCG.DOFade(0, 0.5f).OnComplete(() => messageCG.gameObject.SetActive(false));
    }

    private void ShowMessage()
    {
        if(messageCG.gameObject.activeInHierarchy) return;
        messageCG.alpha = 0;
        messageCG.DOFade(1, 0.5f).OnComplete(() => messageCG.gameObject.SetActive(true));
    }

    public void SetInputEmpty()
    {
        input.text = "";
    }

    private void OnButtonBack()
    {
        UIManager.Instance.PopPanel();
        UIManager.Instance.PopPanel();
        UIManager.Instance.PushPanel(UIPanelType.Preview);
        UIManager.Instance.PushPanel(UIPanelType.Selector);
    }

    private void OnButtonSend()
    {
        if (string.IsNullOrEmpty(input.text)) return;

        SendEmail(input.text);
    }

    #region Virtual Keys

    private void OnButtonDeleteAll()
    {
        if (string.IsNullOrEmpty(input.text)) return;
        HideMessage();
        input.text = "";
    }

    private void OnButtonDelete()
    {
        if (string.IsNullOrEmpty(input.text)) return;

        HideMessage();

        input.ActivateInputField();

        StartCoroutine(OnKeyDelete());
    }
    private IEnumerator OnKeyDelete()
    {
        yield return null;
        input.MoveTextEnd(false);
        InputSimulator.SimulateKeyPress(VirtualKeyCode.BACK);
    }

    private void OnButtonOne()
    {
        input.ActivateInputField();

        HideMessage();

        StartCoroutine(OnKeyOne());
    }
    private IEnumerator OnKeyOne()
    {
        yield return null;
        input.MoveTextEnd(false);
        InputSimulator.SimulateKeyPress(VirtualKeyCode.VK_1);
    }

    private void OnButtonTwo()
    {
        input.ActivateInputField();

        HideMessage();

        StartCoroutine(OnKeyTwo());
    }
    private IEnumerator OnKeyTwo()
    {
        yield return null;
        input.MoveTextEnd(false);
        InputSimulator.SimulateKeyPress(VirtualKeyCode.VK_2);
    }

    private void OnButtonThree()
    {
        input.ActivateInputField();

        HideMessage();

        StartCoroutine(OnKeyThree());
    }
    private IEnumerator OnKeyThree()
    {
        yield return null;
        input.MoveTextEnd(false);
        InputSimulator.SimulateKeyPress(VirtualKeyCode.VK_3);
    }

    private void OnButtonFour()
    {
        input.ActivateInputField();

        HideMessage();

        StartCoroutine(OnKeyFour());
    }
    private IEnumerator OnKeyFour()
    {
        yield return null;
        input.MoveTextEnd(false);
        InputSimulator.SimulateKeyPress(VirtualKeyCode.VK_4);
    }

    private void OnButtonFive()
    {
        input.ActivateInputField();

        HideMessage();

        StartCoroutine(OnKeyFive());
    }
    private IEnumerator OnKeyFive()
    {
        yield return null;
        input.MoveTextEnd(false);
        InputSimulator.SimulateKeyPress(VirtualKeyCode.VK_5);
    }

    private void OnButtonSix()
    {
        input.ActivateInputField();

        HideMessage();

        StartCoroutine(OnKeySix());
    }
    private IEnumerator OnKeySix()
    {
        yield return null;
        input.MoveTextEnd(false);
        InputSimulator.SimulateKeyPress(VirtualKeyCode.VK_6);
    }

    private void OnButtonSeven()
    {
        input.ActivateInputField();

        HideMessage();

        StartCoroutine(OnKeySeven());
    }
    private IEnumerator OnKeySeven()
    {
        yield return null;
        input.MoveTextEnd(false);
        InputSimulator.SimulateKeyPress(VirtualKeyCode.VK_7);
    }

    private void OnButtonEight()
    {
        input.ActivateInputField();

        HideMessage();

        StartCoroutine(OnKeyEight());
    }
    private IEnumerator OnKeyEight()
    {
        yield return null;
        input.MoveTextEnd(false);
        InputSimulator.SimulateKeyPress(VirtualKeyCode.VK_8);
    }

    private void OnButtonNine()
    {
        input.ActivateInputField();

        HideMessage();

        StartCoroutine(OnKeyNine());
    }
    private IEnumerator OnKeyNine()
    {
        yield return null;
        input.MoveTextEnd(false);
        InputSimulator.SimulateKeyPress(VirtualKeyCode.VK_9);
    }

    private void OnButtonZero()
    {
        input.ActivateInputField();

        HideMessage();

        StartCoroutine(OnKeyZero());
    }
    private IEnumerator OnKeyZero()
    {
        yield return null;
        input.MoveTextEnd(false);
        InputSimulator.SimulateKeyPress(VirtualKeyCode.VK_0);
    }

    private void OnButtonQ()
    {
        input.ActivateInputField();

        HideMessage();

        StartCoroutine(OnKeyQ());
    }
    private IEnumerator OnKeyQ()
    {
        yield return null;
        input.MoveTextEnd(false);
        InputSimulator.SimulateKeyPress(VirtualKeyCode.VK_Q);
    }

    private void OnButtonW()
    {
        input.ActivateInputField();

        HideMessage();

        StartCoroutine(OnKeyW());
    }
    private IEnumerator OnKeyW()
    {
        yield return null;
        input.MoveTextEnd(false);
        InputSimulator.SimulateKeyPress(VirtualKeyCode.VK_W);
    }

    private void OnButtonE()
    {
        input.ActivateInputField();

        HideMessage();

        StartCoroutine(OnKeyE());
    }
    private IEnumerator OnKeyE()
    {
        yield return null;
        input.MoveTextEnd(false);
        InputSimulator.SimulateKeyPress(VirtualKeyCode.VK_E);
    }

    private void OnButtonR()
    {
        input.ActivateInputField();

        HideMessage();

        StartCoroutine(OnKeyR());
    }
    private IEnumerator OnKeyR()
    {
        yield return null;
        input.MoveTextEnd(false);
        InputSimulator.SimulateKeyPress(VirtualKeyCode.VK_R);
    }

    private void OnButtonT()
    {
        input.ActivateInputField();

        HideMessage();

        StartCoroutine(OnKeyT());
    }
    private IEnumerator OnKeyT()
    {
        yield return null;
        input.MoveTextEnd(false);
        InputSimulator.SimulateKeyPress(VirtualKeyCode.VK_T);
    }

    private void OnButtonY()
    {
        input.ActivateInputField();

        HideMessage();

        StartCoroutine(OnKeyY());
    }
    private IEnumerator OnKeyY()
    {
        yield return null;
        input.MoveTextEnd(false);
        InputSimulator.SimulateKeyPress(VirtualKeyCode.VK_Y);
    }

    private void OnButtonU()
    {
        input.ActivateInputField();

        HideMessage();

        StartCoroutine(OnKeyU());
    }
    private IEnumerator OnKeyU()
    {
        yield return null;
        input.MoveTextEnd(false);
        InputSimulator.SimulateKeyPress(VirtualKeyCode.VK_U);
    }

    private void OnButtonI()
    {
        input.ActivateInputField();

        HideMessage();

        StartCoroutine(OnKeyI());
    }
    private IEnumerator OnKeyI()
    {
        yield return null;
        input.MoveTextEnd(false);
        InputSimulator.SimulateKeyPress(VirtualKeyCode.VK_I);
    }

    private void OnButtonO()
    {
        input.ActivateInputField();

        HideMessage();

        StartCoroutine(OnKeyO());
    }
    private IEnumerator OnKeyO()
    {
        yield return null;
        input.MoveTextEnd(false);
        InputSimulator.SimulateKeyPress(VirtualKeyCode.VK_O);
    }

    private void OnButtonP()
    {
        input.ActivateInputField();

        HideMessage();

        StartCoroutine(OnKeyP());
    }
    private IEnumerator OnKeyP()
    {
        yield return null;
        input.MoveTextEnd(false);
        InputSimulator.SimulateKeyPress(VirtualKeyCode.VK_P);
    }

    private void OnButtonA()
    {
        input.ActivateInputField();

        HideMessage();

        StartCoroutine(OnKeyA());
    }
    private IEnumerator OnKeyA()
    {
        yield return null;
        input.MoveTextEnd(false);
        InputSimulator.SimulateKeyPress(VirtualKeyCode.VK_A);
    }

    private void OnButtonS()
    {
        input.ActivateInputField();

        HideMessage();

        StartCoroutine(OnKeyS());
    }
    private IEnumerator OnKeyS()
    {
        yield return null;
        input.MoveTextEnd(false);
        InputSimulator.SimulateKeyPress(VirtualKeyCode.VK_S);
    }

    private void OnButtonD()
    {
        input.ActivateInputField();

        HideMessage();

        StartCoroutine(OnKeyD());
    }
    private IEnumerator OnKeyD()
    {
        yield return null;
        input.MoveTextEnd(false);
        InputSimulator.SimulateKeyPress(VirtualKeyCode.VK_D);
    }

    private void OnButtonF()
    {
        input.ActivateInputField();

        HideMessage();

        StartCoroutine(OnKeyF());
    }
    private IEnumerator OnKeyF()
    {
        yield return null;
        input.MoveTextEnd(false);
        InputSimulator.SimulateKeyPress(VirtualKeyCode.VK_F);
    }

    private void OnButtonG()
    {
        input.ActivateInputField();

        HideMessage();

        StartCoroutine(OnKeyG());
    }
    private IEnumerator OnKeyG()
    {
        yield return null;
        input.MoveTextEnd(false);
        InputSimulator.SimulateKeyPress(VirtualKeyCode.VK_G);
    }

    private void OnButtonH()
    {
        input.ActivateInputField();

        HideMessage();

        StartCoroutine(OnKeyH());
    }
    private IEnumerator OnKeyH()
    {
        yield return null;
        input.MoveTextEnd(false);
        InputSimulator.SimulateKeyPress(VirtualKeyCode.VK_H);
    }

    private void OnButtonJ()
    {
        input.ActivateInputField();

        HideMessage();

        StartCoroutine(OnKeyJ());
    }
    private IEnumerator OnKeyJ()
    {
        yield return null;
        input.MoveTextEnd(false);
        InputSimulator.SimulateKeyPress(VirtualKeyCode.VK_J);
    }

    private void OnButtonK()
    {
        input.ActivateInputField();

        HideMessage();

        StartCoroutine(OnKeyK());
    }
    private IEnumerator OnKeyK()
    {
        yield return null;
        input.MoveTextEnd(false);
        InputSimulator.SimulateKeyPress(VirtualKeyCode.VK_K);
    }

    private void OnButtonL()
    {
        input.ActivateInputField();

        HideMessage();

        StartCoroutine(OnKeyL());
    }
    private IEnumerator OnKeyL()
    {
        yield return null;
        input.MoveTextEnd(false);
        InputSimulator.SimulateKeyPress(VirtualKeyCode.VK_L);
    }

    private void OnButtonMinus()
    {
        input.ActivateInputField();

        HideMessage();

        StartCoroutine(OnKeyMinus());
    }
    private IEnumerator OnKeyMinus()
    {
        yield return null;
        input.MoveTextEnd(false);
        InputSimulator.SimulateKeyPress(VirtualKeyCode.OEM_MINUS);
    }

    private void OnButtonZ()
    {
        input.ActivateInputField();

        HideMessage();

        StartCoroutine(OnKeyZ());
    }
    private IEnumerator OnKeyZ()
    {
        yield return null;
        input.MoveTextEnd(false);
        InputSimulator.SimulateKeyPress(VirtualKeyCode.VK_Z);
    }

    private void OnButtonX()
    {
        input.ActivateInputField();

        HideMessage();

        StartCoroutine(OnKeyX());
    }
    private IEnumerator OnKeyX()
    {
        yield return null;
        input.MoveTextEnd(false);
        InputSimulator.SimulateKeyPress(VirtualKeyCode.VK_X);
    }

    private void OnButtonC()
    {
        input.ActivateInputField();

        HideMessage();

        StartCoroutine(OnKeyC());
    }
    private IEnumerator OnKeyC()
    {
        yield return null;
        input.MoveTextEnd(false);
        InputSimulator.SimulateKeyPress(VirtualKeyCode.VK_C);
    }

    private void OnButtonV()
    {
        input.ActivateInputField();

        HideMessage();

        StartCoroutine(OnKeyV());
    }
    private IEnumerator OnKeyV()
    {
        yield return null;
        input.MoveTextEnd(false);
        InputSimulator.SimulateKeyPress(VirtualKeyCode.VK_V);
    }

    private void OnButtonB()
    {
        input.ActivateInputField();

        HideMessage();

        StartCoroutine(OnKeyB());
    }
    private IEnumerator OnKeyB()
    {
        yield return null;
        input.MoveTextEnd(false);
        InputSimulator.SimulateKeyPress(VirtualKeyCode.VK_B);
    }

    private void OnButtonN()
    {
        input.ActivateInputField();

        HideMessage();

        StartCoroutine(OnKeyN());
    }
    private IEnumerator OnKeyN()
    {
        yield return null;
        input.MoveTextEnd(false);
        InputSimulator.SimulateKeyPress(VirtualKeyCode.VK_N);
    }

    private void OnButtonM()
    {
        input.ActivateInputField();

        HideMessage();

        StartCoroutine(OnKeyM());
    }
    private IEnumerator OnKeyM()
    {
        yield return null;
        input.MoveTextEnd(false);
        InputSimulator.SimulateKeyPress(VirtualKeyCode.VK_M);
    }

    private void OnButtonComma()
    {
        input.ActivateInputField();

        HideMessage();

        StartCoroutine(OnKeyComma());
    }
    private IEnumerator OnKeyComma()
    {
        yield return null;
        input.MoveTextEnd(false);
        InputSimulator.SimulateKeyPress(VirtualKeyCode.OEM_COMMA);
    }

    private void OnButtonPeriod()
    {
        input.ActivateInputField();

        HideMessage();

        StartCoroutine(OnKeyPeriod());
    }
    private IEnumerator OnKeyPeriod()
    {
        yield return null;
        input.MoveTextEnd(false);
        InputSimulator.SimulateKeyPress(VirtualKeyCode.OEM_PERIOD);
    }

    private void OnButtonAt()
    {
        input.ActivateInputField();

        HideMessage();

        StartCoroutine(OnKeyAt());
    }
    private IEnumerator OnKeyAt()
    {
        yield return null;
        input.MoveTextEnd(false);
        InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.SHIFT, VirtualKeyCode.VK_2);
    }
    #endregion

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
