using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum InteractType
{
    Pickup
}

public class Interactable : MonoBehaviour
{
    public string interactLabel;
    public InteractType interactType;
    public InteractHandler curInteracter;
    public UnityEvent interactEvent;
    public void Interact(InteractHandler interacter)
    {
        curInteracter = interacter;
        interactEvent.Invoke();
    }

    public void TestInteract(string testMessage)
    {
        print(testMessage);
    }

}
