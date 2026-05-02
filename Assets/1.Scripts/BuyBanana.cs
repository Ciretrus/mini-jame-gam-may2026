using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyBanana : MonoBehaviour
{
    [SerializeField] private Transform m_cloud;
    [SerializeField] private float m_cloudMaxSize;
    [SerializeField] private float m_cloudMinSize;
    [SerializeField] private float m_tweenTime1 = 0.4f;
    [SerializeField] private float m_tweenTime2 = 0.3f;
    [SerializeField] private GameObject[] m_bananas;
    [SerializeField] private SellBanana m_seller;
    [SerializeField] private Transform m_spawnPoint;
    [SerializeField] private float m_punishment = 100f;
    public event Action OnBalanceChange;
    private float m_freshness = 0f;
    private int m_bananaType = 0; 
    private float m_balance = 0f;
    private GameObject m_currentbanana;

    private void Awake()
    {
        m_seller.OnSell += Buy;
    }

    private void Start()
    {
        ShowAnimation();
    }

    private void ShowBanana()
    {
        int randomBanana = UnityEngine.Random.Range(0, m_bananas.Length);
        m_currentbanana = Instantiate(m_bananas[randomBanana],m_spawnPoint);
        if (m_currentbanana != null)
        {
            var bananaSettings = m_currentbanana.GetComponent<BananaSettings>();
            float randomFreshness = UnityEngine.Random.Range(0, 2f);
            m_freshness = randomFreshness;
            bananaSettings.ChangeFreshness(m_freshness);
            m_bananaType = bananaSettings.GetBananaType();
            m_currentbanana.transform.localScale = Vector3.zero;
        }

    }
    private void ShowAnimation()
    {
        m_cloud.DOKill();
        if (m_currentbanana != null) {
            m_currentbanana.transform.DOScale(Vector3.zero, m_tweenTime1).SetEase(Ease.InBack);
                m_cloud.DOScale(Vector3.zero, m_tweenTime1).SetEase(Ease.InBack).OnComplete(() =>
                {
                    m_cloud.DOScale(Vector3.one * m_cloudMaxSize, m_tweenTime1).SetEase(Ease.InBack).OnComplete(() =>
                {
                    ShowBanana();
                    m_currentbanana.transform.DOScale(Vector3.one, m_tweenTime2).SetEase(Ease.InBack);
                });
            });
        }

    }

    private void Buy(float freshness,int bananaType)
    {
        freshness = Mathf.Clamp(freshness, 0f, 2f);
        float startCoast = 100f;
        if (m_bananaType != bananaType)
            startCoast = 50f;
        float coast = startCoast - Mathf.Abs(freshness-m_freshness)* m_punishment;
        m_balance += coast;
        OnBalanceChange?.Invoke();
        ShowAnimation();

    }
    public float GetBalance()
    {
        return m_balance;
    }
}
