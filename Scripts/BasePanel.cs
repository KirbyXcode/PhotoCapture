using UnityEngine;

public class BasePanel : MonoBehaviour
{
    /// <summary>
    /// 界面显示
    /// </summary>
    public virtual void OnEnter()
    {
        gameObject.SetActive(true);
    }

    /// <summary>
    /// 次级界面隐藏
    /// </summary>
    public virtual void OnPause()
    {

    }

    /// <summary>
    /// 次级界面显示
    /// </summary>
    public virtual void OnResume()
    {

    }

    /// <summary>
    /// 界面隐藏
    /// </summary>
    public virtual void OnExit()
    {
        gameObject.SetActive(false);
    }
}
