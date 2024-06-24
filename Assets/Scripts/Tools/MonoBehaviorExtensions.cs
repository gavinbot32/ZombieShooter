using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MonoBehaviorExtensions
{
    public static T SafeGetComponent<T>(this MonoBehaviour mb,T component) where T : Component
    {
        if (component == null)
        {
            return mb.GetComponent<T>();
        }
        return component;
    }
    public static T SafeGetComponentInChildren<T>(this MonoBehaviour mb, T component) where T : Component
    {
        if (component == null)
        {
            return mb.GetComponentInChildren<T>();
        }
        return component;
    }

    public static void ParticalSystemBurst(this MonoBehaviour mb, ParticleSystem ps)
    {
        ps.Stop();
        ps.Play();
    }
}
