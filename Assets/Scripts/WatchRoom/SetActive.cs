using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SetActive : MonoBehaviour
{
    public KeyCode key;
    bool one=true;
    public bool FuWen;
    public GameObject Panel;

    public bool autoClose;

    private Sequence s;
    private void OnEnable()
    {

        if (autoClose)
        {
            var img = GetComponent<Image>();


            img.color = Color.clear;

            s = DOTween.Sequence();

            s.Append(img.DOColor(Color.white, 0.5f));
            s.Append(img.DOColor(Color.white, 2f));
            s.Append(img.DOColor(Color.white, 0.5f));
            s.AppendCallback(() => { Panel.SetActive(false); });

            // img.DOColor(Color.white, 0.5f).OnComplete(() =>
            // {
            //     img.DOColor(Color.white, 2f).OnComplete(() =>
            //     {
            //         img.DOColor(Color.clear, 0.5f).OnComplete(() =>
            //         {
            //             Panel.SetActive(false);
            //         });
            //     });
            // });}
        }
    }

    private void OnDisable()
    {
        s.Complete();
        Panel.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetKeyDown(key))
        {
            Panel.SetActive(false);
        }
        if(one&&FuWen)
        {
            Panel.SetActive(true);
            one = false;
        }
    }
    public void open()
    {
        Panel.SetActive(true);
    }
}
