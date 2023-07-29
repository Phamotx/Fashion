using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Counter : MonoBehaviour
{
    [SerializeField] private GameObject barParent;
    [SerializeField] private Image bar;
    [SerializeField] private ItemType itemType;

    private void Start()
    {
        barParent.SetActive(false);
        bar.fillAmount = 0;
    }
    private void OnTriggerEnter(Collider other)
    {
        barParent.SetActive(true);
        CheckAndAddItem();
    }

    private void OnTriggerExit(Collider other)
    {
        DOTween.Kill(bar);
        barParent.SetActive(false);
    }

    void CheckAndAddItem()
    {
        DOTween.Kill(bar);
        bar.fillAmount = 0;
        bar.DOFillAmount(1, 2f).OnComplete(() =>
           {
               GameManager.instance.AddItem(itemType);
               CheckAndAddItem();
           }
       );
    }
}
