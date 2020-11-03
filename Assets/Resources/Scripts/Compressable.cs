using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Compressable : MonoBehaviour {
    public Sprite uncompressed;
    public Sprite compressed;
    public readonly UnityEvent buttonPressed = new UnityEvent();
    private Collider2D bc;
    private MouseInput mouseInput;
    private SpriteRenderer sr;
    private Action spriteAdjustment;

    void Awake() {
        enableButton();
        sr = GetComponent<SpriteRenderer>();
        bc = GetComponent<Collider2D>();
        mouseInput = GameObject.Find("mouse input").GetComponent<MouseInput>();
        spriteAdjustment = () => { };
    }

    void Update() {
        spriteAdjustment();
    }

    private void compress() {
        sr.sprite = compressed;
        mouseInput.clickReleased.AddListener(clickReleased);
        spriteAdjustment = getCorrectSprite;
    }

    private void clickReleased() {
        sr.sprite = uncompressed;
        mouseInput.clickReleased.RemoveListener(clickReleased);
        spriteAdjustment = () => { };
        if (overButton()) {
            buttonPressed.Invoke();
        }
    }

    private bool overButton() {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D ray = Physics2D.Raycast(mousePos, Vector2.zero, 0);
        return ray.collider == bc;
    }

    private void getCorrectSprite() {
        if (overButton()) {
            sr.sprite = compressed;
        }
        else {
            sr.sprite = uncompressed;
        }
    }

    public void disableButton() {
        GetComponent<Clickable>().clicked.RemoveListener(compress);
    }

    public void enableButton() {
        GetComponent<Clickable>().clicked.AddListener(compress);
    }
}
