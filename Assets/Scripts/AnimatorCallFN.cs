using System;
using UnityEngine;

public class AnimatorCallFN : MonoBehaviour
{

    protected Action<int> m_CallFN;


    public void InitAction(Action<int> callFN)
    {
        m_CallFN = callFN;
    }

    public void CallFNAction(int p_val)
    {
        if( m_CallFN != null)
        {
            m_CallFN(p_val);
        }
    }



    //void Start()
    //{
    //    
    //}


    //void Update()
    //{
    //    
    //}
}
