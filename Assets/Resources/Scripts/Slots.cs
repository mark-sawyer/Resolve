using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Slots : MonoBehaviour {
    public readonly UnityEvent allSlotsResolved = new UnityEvent();
    public int numberOfSlots;
    private int slotsResolved;
    private List<Slot> slots;

    private void Awake() {
        slotsResolved = 0;
        slots = new List<Slot>(numberOfSlots);
        for (int i = 0; i < numberOfSlots; i++) {
            slots.Add(transform.GetChild(i).gameObject.GetComponent<Slot>());
        }

        GameObject.Find("resolve").GetComponent<Compressable>().buttonPressed.AddListener(resolvePressed);
    }

    private void resolvePressed() {
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

        currentSlot.removeCard();
    }

    private void finalResolution() {
        allSlotsResolved.Invoke();
    }
}
