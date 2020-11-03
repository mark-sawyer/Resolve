using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ResolvePointer : MonoBehaviour {
    private GameEvents gameEvents;
    private Compressable compressable;
    public DirectionEnum direction;
    public Sprite disabledSprite;
    private List<Slot> slots;
    public int numberOfSlots;
    private int slotsResolved;
    private float JUMP_AMOUNT = 3;

    private void Awake() {
        slotsResolved = 0;
        slots = new List<Slot>();
        compressable = GetComponent<Compressable>();
        compressable.buttonPressed.AddListener(resolvePressed);
        gameEvents = GameObject.Find("event handler").GetComponent<GameEvents>();
        gameEvents.aResolveWasPressed.AddListener(disableButton);
        gameEvents.resolveFinalised.AddListener(enableButton);
    }

    private void Start() {
        Vector3 directionVector = Direction.getDir(direction);
        bool foundNoSlot = false;
        int spacesAhead = 1;
        while (!foundNoSlot) {
            Vector2 location = transform.position + (directionVector * spacesAhead * JUMP_AMOUNT);
            RaycastHit2D ray = Physics2D.Raycast(location, Vector2.zero, 0, LayerMask.GetMask("slot"));
            if (ray.collider != null) {
                slots.Add(ray.collider.GetComponent<Slot>());
                spacesAhead++;
            }
            else {
                foundNoSlot = true;
                numberOfSlots = slots.Count;
            }
        }
    }

    private void resolvePressed() {
        gameEvents.aResolveWasPressed.Invoke();
        slotsResolved = 0;
        resolveNextCard();
    }

    private void resolveNextCard() {
        Slot currentSlot = slots[slotsResolved];
        currentSlot.resolveCard();
        slotsResolved++;
        if (slotsResolved < numberOfSlots) {
            currentSlot.getResolutionEvent().AddListener(resolveNextCard);
        }
        else {
            currentSlot.getResolutionEvent().AddListener(finalResolution);
        }

        currentSlot.restoreSlotDefault();
    }

    private void finalResolution() {
        gameEvents.resolveFinalised.Invoke();
    }

    private void disableButton() {
        GetComponent<SpriteRenderer>().sprite = disabledSprite;
        compressable.disableButton();
    }

    private void enableButton() {
        GetComponent<SpriteRenderer>().sprite = GetComponent<Compressable>().uncompressed;
        compressable.enableButton();
    }
}

public class Direction {
    public static Vector3 getDir(DirectionEnum direction) {
        switch (direction) {
            case DirectionEnum.UP:
                return Vector3.up;
            case DirectionEnum.RIGHT:
                return Vector3.right;
            case DirectionEnum.DOWN:
                return Vector3.down;
            case DirectionEnum.LEFT:
                return Vector3.left;
        }

        return Vector2.zero;
    }
}

public enum DirectionEnum {
    UP,
    RIGHT,
    DOWN,
    LEFT
};
