using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MovableCard : MonoBehaviour {
    public Card card;
    public readonly UnityEvent<GameObject> pickedUpFromStartSpace = new UnityEvent<GameObject>();
    public readonly UnityEvent<GameObject> putBackInStartSpace = new UnityEvent<GameObject>();
    private MouseInput mouseInput;
    private SpriteRenderer sr;
    private Func<Vector3> getTargetPosition;
    private Action spriteAdjustment;
    private Vector3 defaultPosition;
    private GameObject slot;
    private bool inSlot;
    private const float TRANSPARENT_ALPHA = 0.5f;
    private const float HELD_SCALE = 0.8f;

    private void Awake() {
        mouseInput = GameObject.Find("mouse input").GetComponent<MouseInput>();
        sr = GetComponent<SpriteRenderer>();
        enableMovement();
        GameObject.Find("resolve").GetComponent<Compressable>().buttonPressed.AddListener(disableMovement);
        GameObject.Find("slots").GetComponent<Slots>().allSlotsResolved.AddListener(enableMovement);
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
        else {
            pickedUpFromStartSpace.Invoke(gameObject);
        }
        setSpriteAlpha(TRANSPARENT_ALPHA);
        setTransformScale(HELD_SCALE);
        getTargetPosition = () => (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        spriteAdjustment = checkWhetherTransparent;
        sr.sortingOrder = 4;
        mouseInput.clickReleased.AddListener(stopBeingHeld);
    }

    private void stopBeingHeld() {
        setSpriteAlpha(1);
        setTransformScale(1);
        spriteAdjustment = () => { };
        sr.sortingOrder = 3;
        mouseInput.GetComponent<MouseInput>().clickReleased.RemoveListener(stopBeingHeld);
        findUnheldPosition();
    }

    private void findUnheldPosition() {
        if (overEmptySlot()) {
            placeInSlot();
        }
        else {
            placeInStartSpace();
        }
    }

    private bool overEmptySlot() {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D ray = Physics2D.Raycast(mousePos, Vector2.zero, 0, LayerMask.GetMask("normal slot"));
        return ray.collider != null && !ray.collider.GetComponent<NormalSlot>().hasCard;
    }

    private void releaseFromSlot() {
        inSlot = false;
        slot.GetComponent<NormalSlot>().restoreSlotDefault();
        slot = null;
    }

    private void placeInSlot() {
        inSlot = true;
        slot = getSlot();
        slot.GetComponent<NormalSlot>().setCard(card);
        transform.position = slot.transform.position;
        getTargetPosition = () => slot.transform.position;
    }

    private void placeInStartSpace() {
        transform.position = defaultPosition;
        getTargetPosition = () => defaultPosition;
        putBackInStartSpace.Invoke(gameObject);
    }

    private GameObject getSlot() {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D ray = Physics2D.Raycast(mousePos, Vector2.zero, 0, LayerMask.GetMask("normal slot"));
        return ray.collider.gameObject;
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
        Color temp = sr.color;
        temp.a = alpha;
        sr.color = temp;
    }

    private void setTransformScale(float scale) {
        transform.localScale = new Vector3(scale, scale, scale);
    }

    private void disableMovement() {
        GetComponent<Clickable>().clicked.RemoveListener(startBeingHeld);
    }

    private void enableMovement() {
        GetComponent<Clickable>().clicked.AddListener(startBeingHeld);
    }
}
