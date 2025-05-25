using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PrepareUI : MonoBehaviour
{
    private Animator anim;
    private Action OnComplete;
    private void Start()
    {
        anim = GetComponent<Animator>();
        anim.enabled = false;
    }
    

    public void Show(Action OnComplete)
    {
        this.OnComplete = OnComplete;
        anim.enabled = true;
    }


    private void OnShouComplete()
    {
        OnComplete?.Invoke();
    }


}
