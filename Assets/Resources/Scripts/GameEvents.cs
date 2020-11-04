using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEvents : MonoBehaviour {
    public static readonly UnityEvent aResolveWasPressed = new UnityEvent();
    public static readonly UnityEvent cardFinishedResolving = new UnityEvent();
    public static readonly UnityEvent resolveFinalised = new UnityEvent();
}
