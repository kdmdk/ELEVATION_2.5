using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;

public class UIAnimation : MonoBehaviour
{
    //private GameObject CText;
    //RectTransform rectTran;
    //Transform _trans;
    private Text UIText;
    public static bool isDmg = false;

    float countTime = 0;

    /*
    private RectTransform _rectTransform = null;
    private Tween _tween = null;
    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }
    */
    void Start()
    {
        //rectTran = GameObject.Find("Stage1/Canvas/COMBOText").transform as RectTransform;
        //rectTran = COMBOText.GetComponent<RectTransform>();
        //_trans = GameObject.Find("Canvas").transform.Find("COMBOText");
        UIText = GameObject.Find("COMBOText").GetComponent<Text>();
    }
    void Update()
    {
        if (isDmg && NewGame.Life > 0)
        {
            StartCoroutine(UIEffect());
            isDmg = false;
        }
        if(countTime > 3.0f && UIText.transform.localScale != new Vector3(1, 1, 1))
        {
            Debug.Log("UIText.transform.localScale" + UIText.transform.localScale);
            UIText.transform.localScale = new Vector3(1, 1, 1);
            countTime = 0;
        }
        countTime += Time.deltaTime;
    }

    IEnumerator UIEffect()
    {
        //Vector2 start = new Vector2(-145f, -50f);
        //Vector2 end = new Vector2(-145f, -50f);

        //UIエフェクト
        UIText.transform.DOPunchScale(new Vector3(0.5f, 0.5f), 0.5f);
        yield return new WaitForSeconds(0.05f);
        UIText.transform.DOSmoothRewind();

        //_rectTransform.anchoredPosition = start;
        //_tween = _rectTransform.DOAnchorPos(end, 10f).SetEase(Ease.OutQuart);

        //Debug.Log(UIText.transform);
    }
    /*
    private void OnDisable()
    {
        // Tween破棄
        if (DOTween.instance != null)
        {
            _tween?.Kill();
        }
    }
    */
}
