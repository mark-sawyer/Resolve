using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEvents : MonoBehaviour {
    public readonly UnityEvent aResolveWasPressed = new UnityEvent();
    public readonly UnityEvent resolveFinalised = new UnityEvent();
}
