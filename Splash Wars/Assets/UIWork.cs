using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using DG.Tweening;

public class UIWork : MonoBehaviour
{
    public static UIWork instance;
    [SerializeField]
    RectTransform menuPanel;
    [SerializeField]
    RectTransform shadePanel;
    [SerializeField]
    RectTransform levelPanel;
    [SerializeField]
    RectTransform retryPanel;
    [SerializeField]
    GameObject playerCar;

    [SerializeField]
    Ease easetype;
    [SerializeField]
    Transform checkPoint;
    private void Awake()
    {
        
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

    }
   

    // Start is called before the first frame update
    void Start()
    {
        playerCar.SetActive(false);
        menuPanel.DOAnchorPos(Vector2.zero, 0.15f).SetEase(easetype);
        shadePanel.DOAnchorPos(Vector2.zero, 0.15f).SetEase(easetype);
    }

    public void TweenPlayButton()
    {
        Sequence sequence = DOTween.Sequence();

        sequence.Append(menuPanel.DOAnchorPos(new Vector2(0, -4585f), 0.15f).SetEase(easetype));
        sequence.Append(shadePanel.DOAnchorPos(new Vector2(0, -120f), 0.15f).SetEase(easetype)).OnComplete(normalBehaviour);
    }

    void normalBehaviour()
    {
        playerCar.SetActive(true);
    }
    public void TweenLevelCompeletePanel()
    {
        levelPanel.DOScale(Vector2.one, 0.15f).SetEase(easetype);
        playerCar.SetActive(false);
    }
    public void TweenRetryPanel()
    {
        retryPanel.DOScale(Vector2.one, 0.15f).SetEase(easetype);
    }
}
