using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BananaSpawn : MonoBehaviour
{
    [SerializeField] private GameObject[] m_bananas;
    [SerializeField] private Transform m_spawnPoint;
    [SerializeField] private Camera m_camera;
    [SerializeField] private Transform m_innerMicrowavePosition;
    [SerializeField] private int m_spawnCount = 5;

    [SerializeField] private Transform m_truba;
    [SerializeField] private Vector3 m_trubaOffset;
    [SerializeField] private Vector3 m_buttonOffset;
    [SerializeField] private float m_timeTrubaTween = 0.5f;
    [SerializeField] private float m_timebuttonTween = 0.5f;

    private List<GameObject> m_activeBananas = new List<GameObject>();
    private bool m_canPush = true;

    void Start()
    {
        
    }

    private void OnMouseDown()
    {
        if (m_activeBananas.Count < m_spawnCount && m_canPush)
        {
            m_canPush = false;
            transform.DOMove(transform.position - m_buttonOffset, m_timebuttonTween).SetEase(Ease.OutBack);
            transform.DOMove(transform.position + m_buttonOffset, m_timebuttonTween).SetEase(Ease.OutExpo).OnComplete(() => { m_canPush = true; }); 
            m_truba.DOMove(m_truba.position - m_trubaOffset, m_timeTrubaTween).SetEase(Ease.OutBack).OnComplete(() => { SpawnBanana(); });
            
            m_truba.DOMove(m_truba.position + m_trubaOffset, m_timeTrubaTween).SetEase(Ease.InBack);
        }
        else 
        {
            Debug.Log("too many bananas");
        }
    }
    private void SpawnBanana() 
    {
            int randomInt = Random.Range(0, m_bananas.Length);
            GameObject newBanana = Instantiate(m_bananas[randomInt], m_spawnPoint);
            BananaMove bananaMove = newBanana.GetComponent<BananaMove>();
            bananaMove.m_camera = Camera.main;
            bananaMove.m_innerMicrowavePosition = m_innerMicrowavePosition;
            m_activeBananas.Add(newBanana);
        
    }
}
