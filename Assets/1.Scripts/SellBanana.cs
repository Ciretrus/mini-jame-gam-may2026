using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SellBanana : MonoBehaviour
{

    [SerializeField] private Transform m_sellPoint;
    [SerializeField] private float m_tweenTime;
    [SerializeField] private AudioSource m_audioSource;
    private bool m_started = false;
    public bool m_canSell = false;
    public event Action<float,int> OnSell;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision !=null && collision.CompareTag("Banana"))
        {
            BananaMove bananaMove = collision.GetComponent<BananaMove>();
            if (bananaMove != null && !bananaMove.isDrag && !m_started && m_canSell) 
            {
                m_started = true;
                bananaMove.canMove = false;
                BananaSettings bananaSettings = collision.GetComponent<BananaSettings>();
                float freshness = bananaSettings.GetFreshness();
                int bananaType = bananaSettings.GetBananaType();
                bananaMove.transform.DOMove(m_sellPoint.position, m_tweenTime).OnComplete(() =>
                {
                    Destroy(bananaMove.gameObject);
                    OnSell?.Invoke(freshness,bananaType);
                    m_audioSource.Play();
                    m_started = false;
                });
            }

        }
    }
}
