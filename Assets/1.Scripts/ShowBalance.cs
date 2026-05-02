using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowBalance : MonoBehaviour
{
    [SerializeField] private TextMeshPro m_balance;
    [SerializeField] private BuyBanana m_buyBanana;
    [SerializeField] private Color m_negativeBalanceColor;
    [SerializeField] private float m_tweenTime;
    private int m_currentBalance = 0;
    private Color m_startColor;

    private void OnEnable()
    {
        m_buyBanana.OnBalanceChange += ChangeBalance;
    }
    private void Start()
    {
        m_startColor = m_balance.color;
    }
    private void OnDisable()
    {
        m_buyBanana.OnBalanceChange -= ChangeBalance;
    }
    private void ChangeBalance(float balance)
    {
        if (balance < 0)
            m_balance.color = m_negativeBalanceColor;
        else
            m_balance.color = m_startColor;
        DOTween.To(() => m_currentBalance, x => m_currentBalance = x, (int)balance, m_tweenTime)
                 .OnUpdate(() =>
                 {
                     m_balance.text = m_currentBalance.ToString("D4");
                 })
                 .SetEase(Ease.OutQuad);
        //m_currentBalance = (int)balance;
    }
    public void RestartBalance()
    {
        ChangeBalance(0f);
    }
    public int GetBalance()
    {
        return m_currentBalance;
    }
}
