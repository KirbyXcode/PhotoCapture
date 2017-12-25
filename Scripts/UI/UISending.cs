using System.Collections;
using UnityEngine;

public class UISending : BasePanel 
{
    public override void OnEnter()
    {
        base.OnEnter();
        StartCoroutine(SendingMail());
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    private IEnumerator SendingMail()
    {
        yield return new WaitForSeconds(2);
        UIEmail emailPanel = DictionaryExtension.TryGet<UIPanelType, BasePanel>(UIManager.Instance.PanelDict, UIPanelType.Email) as UIEmail;
        emailPanel.SetInputEmpty();

        switch (Global.returnCode)
        {

            case ReturnCode.Success:
                UIManager.Instance.PopPanel();
                UIManager.Instance.PopPanel();
                UIManager.Instance.PopPanel();
                UIManager.Instance.PushPanel(UIPanelType.Success);
                break;
            case ReturnCode.Failure:
                UIManager.Instance.PopPanel();
                UIManager.Instance.PopPanel();
                UIManager.Instance.PushPanel(UIPanelType.Failure);
                break;
        }
    }
}
