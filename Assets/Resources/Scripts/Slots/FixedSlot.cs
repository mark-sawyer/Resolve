using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FixedSlot : MonoBehaviour, Slot {
    public GameObject cardPrefab;
    private GameObject cardGameObject;
    private Card card;

    private void Awake() {
        setNewCard();
        card = cardGameObject.GetComponent<Card>();
    }

    public void resolveCard() {
        cardGameObject.GetComponent<SpriteRenderer>().sortingOrder = 10;
        card.resolveCard();
        setNewCard();
    }

    private void setNewCard() {
        cardGameObject = Instantiate(cardPrefab, transform.position, Quaternion.identity);
        cardGameObject.transform.parent = transform;
        Destroy(cardGameObject.GetComponent<MovableCard>());
        Destroy(cardGameObject.GetComponent<Clickable>());
    }

    public UnityEvent getResolutionEvent() {
        return card.resolutionComplete;
    }

    public void restoreSlotDefault() {
        card = cardGameObject.GetComponent<Card>();
    }

    public bool hasCard() {
        return true;
    }
}
