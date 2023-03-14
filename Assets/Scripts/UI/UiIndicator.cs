using DG.Tweening;
using TMPro;
using UnityEngine;

public class UiIndicator : MonoBehaviour
{
    [Header("Objects assigned in editor")]
    [SerializeField] private TextMeshProUGUI indicatorText;
    [SerializeField] private GameObject indicatorImage;

    [Header("Scriptable objects")]
    [SerializeField] private UISettings indicatorSettings;
    [SerializeField] private InventoryBlock value;

    [Header("Values on runtime")]
    [SerializeField] private int currentDisplayedValue;
    [SerializeField] private string postfix;
    private void OnEnable()
    {
        postfix = value.isCapped ? "/" + value.maxAmount : "";
        indicatorText.SetText(currentDisplayedValue.ToString() + postfix);
        value.amountUpdateEvent.AddListener(UpdateIndicator);
    }
    private void OnDisable()
    {
        value.amountUpdateEvent?.RemoveListener(UpdateIndicator);
    }

    public void UpdateIndicator(Vector3 position, int amount)
    {
        postfix = value.isCapped ? "/" + value.maxAmount : "";
        Sequence sequence = DOTween.Sequence();
        Tween tween;
        
        if (indicatorSettings.hasAnimation)
        {
            sequence.PrependInterval(0.5f);
            GameObject icon =Instantiate(indicatorImage, Camera.main.WorldToScreenPoint(position), Quaternion.identity,transform);
            
            tween = icon.transform.DOMove(indicatorImage.transform.position, indicatorSettings.animationLength);
            icon.SetActive(false);
            tween.onPlay += () => icon.SetActive(true);
            tween.onComplete += () => Destroy(icon);
            sequence.Append(tween);
        }

        tween = transform.DOShakePosition(0.5f, 2f);
        sequence.Append(tween);

        if (indicatorSettings.isGradual)
        {
            tween = DOTween.To(() => currentDisplayedValue, x => currentDisplayedValue = x, value.currentAmount, indicatorSettings.durationTime);
            tween.onUpdate += () => indicatorText.SetText(currentDisplayedValue.ToString()+postfix);
            sequence.Append(tween);
        }
        sequence.Play();
        if (!indicatorSettings.isGradual)
        {
            currentDisplayedValue = value.currentAmount;
            indicatorText.SetText(currentDisplayedValue.ToString() + postfix);
        }

    }


}
