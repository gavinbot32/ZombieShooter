using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    public ParticleSystem particals;
    
    public void PSBurst()
    {
        particals.Stop();
        particals.Play();
    }
}
