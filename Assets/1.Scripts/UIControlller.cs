using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIControlller : MonoBehaviour
{
    [SerializeField] private TimerManager m_timerManager;
    [SerializeField] private ShowBalance m_showBalance;
    [SerializeField] private BuyBanana m_buyBanana;
    [SerializeField] private SellBanana m_sellBanana;
    [SerializeField] private Microwave m_microwave;
    [SerializeField] private BananaSpawn m_bananaSpawn;
    [SerializeField] private RectTransform m_gameOverUI;
    [SerializeField] private TextMeshProUGUI m_highestScoreTMP;
    [SerializeField] private TextMeshProUGUI m_lastScoreTMP;
    [SerializeField] private Vector2 m_hidePoint = new Vector2(0, -500);
    [SerializeField] private float m_tweenTime = 1f;
    private float highestScore = -9999999f;
    private void OnEnable()
    {
        m_timerManager.OnStoped += GameOver;
    }

    private void OnDisable()
    {
        m_timerManager.OnStoped -= GameOver;    
    }

    private void ShowGameOver()
    {
        highestScore = Mathf.Max(highestScore, m_showBalance.GetBalance());
        m_highestScoreTMP.text = "Highest score: "+ highestScore;
        m_lastScoreTMP.text = "Last score: " + m_showBalance.GetBalance();
        m_gameOverUI.gameObject.SetActive(true);
        m_gameOverUI.DOAnchorPos(Vector2.zero, m_tweenTime).SetEase(Ease.OutBack);
    }

    public void HideGameOver()
    {
        m_gameOverUI.DOAnchorPos(m_hidePoint, m_tweenTime).SetEase(Ease.InBack).OnComplete(() =>
        {
            m_gameOverUI.gameObject.SetActive(false);
        });
    }

    private void GameOver()
    {
        m_sellBanana.m_canSell = false;
        m_buyBanana.HideAnimation();
        m_bananaSpawn.m_canPush = false;
        ShowGameOver();
    }

    public void Restart()
    {
        m_sellBanana.m_canSell = true;
        HideGameOver();
        m_bananaSpawn.m_canPush = true;
        m_timerManager.Restart();
        m_showBalance.RestartBalance();
        m_buyBanana.Restart();
        m_microwave.Restart();
        m_bananaSpawn.Restart();

    }
}
