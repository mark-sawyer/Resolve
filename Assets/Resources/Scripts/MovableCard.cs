using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableCard : MonoBehaviour {
    private GameObject mouseInput;
    private GameObject slot;
    private SpriteRenderer sprite;
    private Func<Vector3> getTargetPosition;
    private Action spriteAdjustment;
    private Vector3 defaultPosition;
    private bool inSlot;
    private const float TRANSPARENT_ALPHA = 0.5f;
    private const float HELD_SCALE = 0.8f;

    private void Awake() {
        mouseInput = GameObject.Find("mouse input");
        sprite = GetComponent<SpriteRenderer>();
        GetComponent<Clickable>().clicked.AddListener(startBeingHeld);
        defaultPosition = transform.position;
        getTargetPosition = () => defaultPosition;
        spriteAdjustment = () => { };
    }

    private void Update() {
        transform.position = getTargetPosition();
        spriteAdjustment();
    }

    private void startBeingHeld() {
        if (inSlot) {
            releaseFromSlot();
        }
        setSpriteAlpha(TRANSPARENT_ALPHA);
        setTransformScale(HELD_SCALE);
        getTargetPosition = () => (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        spriteAdjustment = checkWhetherTransparent;
        sprite.sortingOrder = 1;
        mouseInput.GetComponent<MouseInput>().clickReleased.AddListener(stopBeingHeld);
    }

    private void stopBeingHeld() {
        setSpriteAlpha(1);
        setTransformScale(1);
        spriteAdjustment = () => { };
        sprite.sortingOrder = 0;
        mouseInput.GetComponent<MouseInput>().clickReleased.RemoveListener(stopBeingHeld);
        findUnheldPosition();
    }

    private void findUnheldPosition() {
        if (overEmptySlot()) {
            placeInSlot();
        }
        else {
            transform.position = defaultPosition;
            getTargetPosition = () => defaultPosition;
        }
    }

    private bool overEmptySlot() {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D ray = Physics2D.Raycast(mousePos, Vector2.zero, 0, LayerMask.GetMask("slot"));
        return ray.collider != null && !ray.collider.GetComponent<Slot>().hasCard;
    }

    private GameObject getSlot() {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D ray = Physics2D.Raycast(mousePos, Vector2.zero, 0, LayerMask.GetMask("slot"));
        return ray.collider.gameObject;
    }

    private void releaseFromSlot() {
        inSlot = false;
        slot.GetComponent<Slot>().removeCard();
        slot = null;
    }

    private void placeInSlot() {
        inSlot = true;
        slot = getSlot();
        slot.GetComponent<Slot>().setCard(gameObject);
        transform.position = slot.transform.position;
        getTargetPosition = () => slot.transform.position;
    }

    private void checkWhetherTransparent() {
        if (overEmptySlot()) {
            setSpriteAlpha(1);
        }
        else {
            setSpriteAlpha(TRANSPARENT_ALPHA);
        }
    }

    private void setSpriteAlpha(float alpha) {
        Color temp = sprite.color;
        temp.a = alpha;
        sprite.color = temp;
    }

    private void setTransformScale(float scale) {
        transform.localScale = new Vector3(scale, scale, scale);
    }
}
