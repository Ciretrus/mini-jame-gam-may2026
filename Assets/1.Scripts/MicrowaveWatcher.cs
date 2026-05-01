using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicrowaveWatcher : MonoBehaviour
{
    public BananaMove m_bananaMove;



    private void OnEnable()
    {
        m_bananaMove.InMicrowave += BananaStopMove;
    }
    private void OnDisable()
    {
        m_bananaMove.InMicrowave -= BananaStopMove;
    }

    public void BananaStopMove()
    {
        m_bananaMove.canMove = false;
    }
}
