using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

using UnityEngine.UI;

public class TweenStuff : MonoBehaviour
{
    [SerializeField]
    RectTransform settingsPanel;
    [SerializeField]
    Transform shopPanel;
    [SerializeField]
    Transform newPanel;
    [SerializeField]
    Transform playPanel;
    [SerializeField]
    Transform playButton;
    [SerializeField]
    Ease easetye;

    bool hasPressed;
    bool hasPressedShop;
    bool hasPressedPanel;
    bool hasPressedPlay;
    int childCount;
    // Start is called before the first frame update
    void Start()
    {
        hasPressed = false;
        hasPressedShop = false;
        hasPressedPanel = false;
        hasPressedPlay = false;
        childCount = settingsPanel.transform.childCount-2;
    }

    
    public void TweenSettingsPanel()
    {
        if (!hasPressed)
        {
            for(int i=0;i<=childCount;i++)
            {
                settingsPanel.transform.GetChild(i).GetComponent<RectTransform>().DOAnchorPosY(-50f * (i + 1), 0.15f).SetEase(easetye);
            }
            hasPressed = true;
        }

        else
        {
            for (int i = 0; i <= childCount; i++)
            {
                settingsPanel.transform.GetChild(i).GetComponent<RectTransform>().DOAnchorPosY(0, 0.15f).SetEase(easetye);
            }
            hasPressed = false;
        }

    }

    public void TweenShopPanel()
    {
        if(!hasPressedShop)
        {
            shopPanel.DOScale(Vector3.one, 0.15f).SetEase(easetye);
            newPanel.DOScale(Vector3.zero, 0.15f).SetEase(easetye);
            hasPressedShop = true;
            hasPressedPanel = false;
        }
        else
        {
            shopPanel.DOScale(Vector3.zero, 0.15f).SetEase(easetye);
            hasPressedShop = false;
        }
    }

    public void TweenNewPanel()
    {
        if (!hasPressedPanel)
        {
            newPanel.DOScale(Vector3.one, 0.15f).SetEase(easetye);
            shopPanel.DOScale(Vector3.zero, 0.15f).SetEase(easetye);
            hasPressedPanel = true;
            hasPressedShop = false;
        }
        else
        {
            newPanel.DOScale(Vector3.zero, 0.15f).SetEase(easetye);
            hasPressedPanel = false;
        }

    }
    //public void TweenPlayPanel()
    //{
    //    if(!hasPressedPlay)
    //    {
    //        Sequence sequence = DOTween.Sequence();
    //        sequence.Append(playButton.DOScale(new Vector2(10, 10), 0.15f).SetEase(easetye));
            

    //       sequence.Append( playPanel.DOScale(Vector3.one, 0.15f).SetEase(easetye));
    //        hasPressedPlay = true;
    //    }
    //    else
    //    {
    //        hasPressedPlay = false;
    //    }
    //}
}
