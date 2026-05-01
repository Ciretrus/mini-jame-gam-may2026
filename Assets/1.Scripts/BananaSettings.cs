using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaSettings : MonoBehaviour
{
    [SerializeField] private Gradient m_colorGradient1;
    [SerializeField] private Gradient m_colorGradient2;

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
    }
}
