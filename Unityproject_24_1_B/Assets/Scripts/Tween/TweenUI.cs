using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;


public class TweenUI : MonoBehaviour
{

    public float duration = 1f;
    public Image image;
 
    void Start()
    {
        image = GetComponent<Image>();          //이미지 컨포넌트를 가져온다.

        image.DOFade(0f, duration)              //UI Fade 를 한다. 0 : 투명처리
            .SetEase(Ease.InOutQuad)            //옵션 값 설정
            .SetAutoKill(false)
            .Pause()
            .OnComplete(() => Debug.Log("UI 완료"));  // 익명함수에서 로그 활성화 [() =>

        image.DOPlay();
    }
}
