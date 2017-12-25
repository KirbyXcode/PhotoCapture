using UnityEngine;

public class GameRoot : MonoBehaviour
{
	void Start()
    {
        UIManager.Instance.PushPanel(UIPanelType.Pending);
        UIManager.Instance.PushPanel(UIPanelType.Ready);
    }

    void OnApplicationQuit()
    {
        UIManager.Instance.ResetData();
    }
}
