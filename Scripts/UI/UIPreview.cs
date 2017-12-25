using UnityEngine;
using UnityEngine.UI;

public class UIPreview : BasePanel 
{
    private Image photoImage;
    public Image PhotoImage
    {
        get
        {
            if(photoImage == null)
            {
                photoImage = Global.FindChild(transform, "Photo").GetComponent<Image>();
            }
            return photoImage;
        }
    }
    private Image backgroundImage;
    public Image BackgroundImage
    {
        get
        {
            if (backgroundImage == null)
            {
                backgroundImage = Global.FindChild(transform, "Background").GetComponent<Image>();
            }
            return backgroundImage;
        }
    }

    public Sprite[] bgSprites;

    private void Start()
    {
        BackgroundImage.sprite = bgSprites[0];
    }

    public void SetBackgournd()
    {
        switch (Global.background)
        {
            case Background.None:
                BackgroundImage.sprite = null;
                break;
            case Background.NewLook:
                BackgroundImage.sprite = bgSprites[0];
                break;
            case Background.Riverside:
                BackgroundImage.sprite = bgSprites[1];
                break;
            case Background.Surface:
                BackgroundImage.sprite = bgSprites[2];
                break;
            case Background.MorningSun:
                BackgroundImage.sprite = bgSprites[3];
                break;
            case Background.Buildings:
                BackgroundImage.sprite = bgSprites[4];
                break;
            case Background.Bridge:
                BackgroundImage.sprite = bgSprites[5];
                break;
            case Background.River:
                BackgroundImage.sprite = bgSprites[6];
                break;
            case Background.Happyness:
                BackgroundImage.sprite = bgSprites[7];
                break;
        }
    }

    public void SetPhotoPosition(Vector3 pos)
    {
        PhotoImage.transform.localPosition = pos;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        PhotoImage.sprite = Global.sprite;
    }

    public override void OnExit()
    {
        base.OnExit();
    }
}
