using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class Microwave : MonoBehaviour
{
    public static bool m_isFull;
    private BananaSettings m_bananaSettings;
    private BananaMove m_bananaMove;
    [SerializeField] private ChangeDoor m_changeDoorEvent1;
    [SerializeField] private ChangeDoor m_changeDoorEvent2;

    private void OnEnable()
    {
        m_changeDoorEvent1.DoorChanged += BananaCanMove;
        m_changeDoorEvent2.DoorChanged += BananaCanMove;
    }

    private void OnDisable()
    {
        m_changeDoorEvent1.DoorChanged -= BananaCanMove;
        m_changeDoorEvent2.DoorChanged -= BananaCanMove;
    }
    public void BananaCanMove(bool canMove)
    {
        if (m_bananaMove!=null)
        m_bananaMove.canMove = canMove;
    }

    public void SetBanana(BananaMove bananaMove, BananaSettings bananaSettings)
    {
        m_bananaSettings = bananaSettings;
        m_bananaMove = bananaMove;
    }


    
}
