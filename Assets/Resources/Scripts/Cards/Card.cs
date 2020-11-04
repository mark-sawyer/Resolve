using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Card : MonoBehaviour {
    private Action spriteAdjustment;
    private SpriteRenderer sr;
    protected Character character;

    private void Awake() {
        character = GameObject.Find("character").GetComponent<Character>();
        sr = GetComponent<SpriteRenderer>();
        spriteAdjustment = () => { };
    }

    private void Update() {
        spriteAdjustment();
    }

    public abstract void resolveCard();

    public abstract void completeResolution();

    public void startGrowAndFadeAnimation() {
        spriteAdjustment = growAndFade;
    }

    private void growAndFade() {
        Color temp = sr.color;
        temp.a -= 0.0075f;
        sr.color = temp;

        transform.localScale *= 1.002f;

        if (temp.a < 0.01) {
            spriteAdjustment = () => { };
        }
    }
}
