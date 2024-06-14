using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SimpleHint : MonoBehaviour
{
    private CanvasGroup group;
    
    // Start is called before the first frame update
    void OnEnable()
    {
        group = GetComponent<CanvasGroup>();
        group.alpha = 0;

        group.DOFade(1, 0.5f).OnComplete(() =>
        {
            group.DOFade(1, 2f).OnComplete(() =>
            {
                group.DOFade(0, 0.5f).OnComplete(() =>
                {
                    Destroy(gameObject);
                });
            });


        });
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
