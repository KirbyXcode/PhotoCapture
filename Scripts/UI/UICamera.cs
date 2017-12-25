using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UICamera : BasePanel 
{
    public string deviceName;
    private RawImage background;

    private void Start()
    {
        background = GetComponentInChildren<RawImage>();
        StartCoroutine(InvokeCamera());
    }

    public override void OnEnter()
    {
        base.OnEnter();
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    /// <summary>
    /// 调用摄像头
    /// </summary>
    private IEnumerator InvokeCamera()
    {
        yield return Application.RequestUserAuthorization(UserAuthorization.WebCam);
        if(Application.HasUserAuthorization(UserAuthorization.WebCam))
        {
            WebCamDevice[] devices = WebCamTexture.devices;
            deviceName = devices[0].name;

            //接收返回的图片数据并设置摄像机摄像的区域
            WebCamTexture texture = new WebCamTexture(deviceName);
            background.texture = texture;
            //启动摄像头
            texture.Play();
        }
    }
}
