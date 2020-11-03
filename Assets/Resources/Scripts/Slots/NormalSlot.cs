using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NormalSlot : MonoBehaviour, Slot {
    private Animator anim;
    private BoxCollider2D bc;
    private Card card;
    private Card nullCard;

    private void Awake() {
        anim = GetComponent<Animator>();
        bc = GetComponent<BoxCollider2D>();
        nullCard = gameObject.AddComponent<NullCard>();
        card = nullCard;
    }

    public void setCard(Card card) {
        this.card = card;
        bc.enabled = false;
    }

    public void restoreSlotDefault() {
        card = nullCard;
        bc.enabled = true;
    }

    public void resolveCard() {
        anim.SetTrigger("resolve");
        card.resolveCard();
    }

    public UnityEvent getResolutionEvent() {
        return card.resolutionComplete;
    }

    public bool hasCard() {
        return card != nullCard;
    }
}
