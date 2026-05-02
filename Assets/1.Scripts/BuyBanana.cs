using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyBanana : MonoBehaviour
{
    [SerializeField] private Transform m_cloud;
    [SerializeField] private GameObject[] m_bananas;
    [SerializeField] private SellBanana m_seller;
    private float m_balance = 0f;

    private void Awake()
    {
        m_seller.OnSell += Buy;
    }

    private void Buy(float freshness)
    {
        float coast = freshness * 100;
    }
}
