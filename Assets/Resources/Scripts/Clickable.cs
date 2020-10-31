using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Clickable : MonoBehaviour {
    public readonly UnityEvent clicked = new UnityEvent();

    public void respondToClick() {
        clicked.Invoke();
    }
}
