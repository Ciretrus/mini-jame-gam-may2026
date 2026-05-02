using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BananaSettings : MonoBehaviour
{
    [SerializeField] private Gradient m_colorGradient1;
    [SerializeField] private Gradient m_colorGradient2;
    
    [SerializeField] private float m_tweenTime = 0.3f;
    [SerializeField] private float m_greenBoomFreshness = -0.5f;
    [SerializeField] private float m_brownBoomFreshness = 2.5f;

    [SerializeField] private float m_greenBoomSize = 1.25f;
    [SerializeField] private float m_brownBoomSize = 0.1f;
    public event Action<BananaSettings> OnDestroyed;
    public ParticleSystem m_greenParticle;
    public ParticleSystem m_brownParticle;
    private float m_freshness = 2f;
    private SpriteRenderer m_sr;

    void Awake()
    {
        m_sr = GetComponent<SpriteRenderer>();
        ChangeFreshness(m_freshness);

    }
    private void Start()
    {
        //ChangeFreshness(m_freshness);
    }


    public void ChangeFreshness(float freshness)
    {
        m_freshness = freshness;
        if (freshness>1f)
        {
            m_sr.color = m_colorGradient1.Evaluate(m_freshness-1f);
        }
        else
        {
            m_sr.color = m_colorGradient2.Evaluate(m_freshness);
        }
        if (freshness < m_greenBoomFreshness) 
        {
            GreenBananaBoom();
        }
        if (freshness > m_brownBoomFreshness)
        {
            BrownBananaBoom();
        }
    }

    private void GreenBananaBoom()
    {
        Vector3 newScale = transform.localScale* m_greenBoomSize;
        transform.DOScale(newScale, m_tweenTime).SetEase(Ease.InExpo).OnComplete(() =>
        {
            m_greenParticle.Play();
            Destroy(gameObject);
        });
    }
    private void BrownBananaBoom()
    {
        Vector3 newScale = transform.localScale * m_brownBoomSize;
        transform.DOScale(newScale, m_tweenTime).SetEase(Ease.OutExpo).OnComplete(() =>
        {
            m_brownParticle.Play();
            Destroy(gameObject);
        });
    }
    public float GetFreshness()
    {
        return m_freshness;
    }
    private void OnDestroy()
    {
        OnDestroyed?.Invoke(this);
    }
}
