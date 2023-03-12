using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class UiIndicator : MonoBehaviour
{
    public GameObject itemSender;
    public TextMeshProUGUI indicatorText;
    public GameObject indicatorImage;

    public int currentAmount;
    public int amount;

    //Animation variables
    public bool hasAnimation;
    public bool isGradualy;
    public float durationTime = 2f;

    private float maxTime;
    private float currentTime;

    //postfix thats added after the amount
    public string postfix;

    public void UpdateIndicator(uiItemInfo uiItemInfo)
    {
        amount = uiItemInfo.value;
        postfix = uiItemInfo.postfix;
        itemSender = uiItemInfo.sender;
        if (isGradualy)
        {
            maxTime = durationTime / amount;
        }
        if (hasAnimation)
        {
            Instantiate(indicatorImage, itemSender.transform);
        }
        transform.DOShakePosition(0.5f,2f);    
    }

    private void Update()
    {
        if (currentAmount == amount) { currentTime = maxTime; return; }

        if (isGradualy)
        {
            if (currentTime <= 0)
            {
                currentAmount++;
                currentTime = maxTime;
            }
            else
            {
                currentTime = -Time.deltaTime;
            }
        }
        else
        {
            currentAmount = amount;
        }
        indicatorText.SetText(currentAmount.ToString() + postfix);

    }

}
