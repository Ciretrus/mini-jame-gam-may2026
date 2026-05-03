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
    [SerializeField] private ParticleSystem m_greenParticle;
    [SerializeField] private ParticleSystem m_brownParticle;

    [SerializeField] private Transform m_truba;
    [SerializeField] private Vector3 m_trubaOffset;
    [SerializeField] private Vector3 m_buttonOffset;
    [SerializeField] private float m_timeTrubaTween = 0.5f;
    [SerializeField] private float m_timebuttonTween = 0.5f;
    [SerializeField] private AudioSource m_brownBananaBoom;
    [SerializeField] private AudioSource m_greenBananaBoom;
    [SerializeField] private AudioSource m_audioSource;

    private List<GameObject> m_activeBananas = new List<GameObject>();
    private int m_bananaCounter = 0;
    public bool m_canPush = true;

    //void OnDisable()
    //{
    //    foreach (var obj in m_activeBananas)
    //    {
    //        var bananaMove = obj.GetComponent<BananaMove>();
    //        bananaMove.InMicrowave -= BananaShare;
    //    }    
    //}

    void BananaDestroyed(BananaMove banana)
    {
        m_activeBananas.Remove(banana.gameObject);
        banana.OnDestroyed -= BananaDestroyed;
        banana.InMicrowave -= BananaShare;
        m_bananaCounter --;
    }

    private void OnMouseDown()
    {
        if (m_activeBananas.Count < m_spawnCount && m_canPush)
        {
            m_audioSource.Play();
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
        bananaMove.m_microwave = m_microwave;
        bananaMove.m_innerMicrowavePosition = m_innerMicrowavePosition;
        bananaMove.InMicrowave += BananaShare;
        bananaMove.OnDestroyed += BananaDestroyed;
        float randomFreshness = Random.Range(0, 2f);

        Debug.Log(randomFreshness);
        bananaSettings.ChangeFreshness(randomFreshness);
        bananaSettings.m_brownParticle = m_brownParticle;
        bananaSettings.m_greenParticle = m_greenParticle;
        bananaSettings.m_brownBananaBoom = m_brownBananaBoom;
        bananaSettings.m_greenBananaBoom = m_greenBananaBoom;
        m_activeBananas.Add(newBanana);


    }
    private void BananaShare(bool inMicrowave, BananaMove bananaMove)
    {
        Debug.Log("microwave"+inMicrowave);
        if (inMicrowave)
        {
            var bananaSettings = bananaMove.GetComponent<BananaSettings>();
            m_microwave.SetBanana(bananaMove, bananaSettings);
        }
        else
        {
            m_microwave.SetBanana(null,null);
        }
    }

    public void Restart()
    {
        foreach (var obj in m_activeBananas)
        {
            Destroy(obj);
        }
        m_bananaCounter = 0;
        m_canPush = true;
    }
}
