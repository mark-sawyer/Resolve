using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Slot : MonoBehaviour {
    public bool hasCard { get; private set; }
    private Animator anim;
    private BoxCollider2D bc;
    private Card card;
    private Card nullCard;

    private void Awake() {
        anim = GetComponent<Animator>();
        bc = GetComponent<BoxCollider2D>();
        nullCard = GetComponent<NullCard>();
        card = nullCard;
    }

    public void setCard(Card card) {
        this.card = card;
        hasCard = true;
        bc.enabled = false;
    }

    public void removeCard() {
        card = nullCard;
        hasCard = false;
        bc.enabled = true;
    }

    public void resolveCard() {
        anim.SetTrigger("resolve");
        card.resolveCard();
    }

    public UnityEvent getResolutionEvent() {
        return card.resolutionComplete;
    }
}
