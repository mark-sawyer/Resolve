using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Card : MonoBehaviour {
    public readonly UnityEvent resolutionComplete = new UnityEvent();

    public abstract void resolveCard();

    public void completeResolution() {
        resolutionComplete.Invoke();
        resolutionComplete.RemoveAllListeners();
    }
}
