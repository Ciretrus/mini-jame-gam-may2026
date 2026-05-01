using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BananaSpawn : MonoBehaviour
{
    [SerializeField] private Microwave m_microwave;   
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

    void OnDisable()
    {
        foreach (var obj in m_activeBananas)
        {
            var bananaMove = obj.GetComponent<BananaMove>();
            bananaMove.InMicrowave -= BananaShare;
        }    
    }

    private void OnMouseDown()
    {
        if (m_activeBananas.Count < m_spawnCount && m_canPush)
        { 

            m_canPush = false;
            transform.DOMove(transform.position - m_buttonOffset, m_timebuttonTween).SetEase(Ease.OutBack).OnComplete(() => {
                
                transform.DOMove(transform.position + m_buttonOffset, m_timebuttonTween).SetEase(Ease.OutExpo).OnComplete(()=>
                {
                    
                });
            });
            m_truba.DOMove(m_truba.position - m_trubaOffset, m_timeTrubaTween).SetEase(Ease.OutBack).OnComplete(() => { SpawnBanana();
                m_truba.DOMove(m_truba.position + m_trubaOffset, m_timeTrubaTween).SetEase(Ease.InBack).OnComplete(() =>
                {
                    m_canPush = true;
                });
            });
            
            
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
        BananaSettings bananaSettings = newBanana.GetComponent<BananaSettings>();

        bananaMove.m_camera = Camera.main;
        bananaMove.m_innerMicrowavePosition = m_innerMicrowavePosition;
        bananaMove.InMicrowave += BananaShare;

        float randomFreshness = Random.Range(0, 2f);
        Debug.Log(randomFreshness);
        bananaSettings.ChangeFreshness(randomFreshness);
        m_activeBananas.Add(newBanana);
        
    }
    public void BananaShare(bool inMicrowave, BananaMove bananaMove)
    {
        Debug.Log("microwave"+inMicrowave);
        var bananaSettings = bananaMove.GetComponent<BananaSettings>();
        m_microwave.SetBanana(bananaMove, bananaSettings);
    }
}
