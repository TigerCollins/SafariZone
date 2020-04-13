using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapObject : MonoBehaviour
{
    public bool isAnimated;
    public Animator animator;
    public ParticleSystem particle;
    public ParticleSystem particleIdle;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
      //  particle = transform.GetChild(0).GetComponentInChildren<ParticleSystem>();
        TrapReset();
    }

    public void TrapPlaced()
    {
        //animator.SetBool("IsOn", false);
        
       // particleIdle.Stop();
    }

    public void TrapCaught()
    {
        //animator.SetBool("IsOn", true);
        isAnimated = true;
       // particle.Play();
    }

    public void TrapReset()
    {
        isAnimated = false;
        animator.SetBool("IsOn", false);
        //particle.Stop();
        //particleIdle.Play();
    }

}
