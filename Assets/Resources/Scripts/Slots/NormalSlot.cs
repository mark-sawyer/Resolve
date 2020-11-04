using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NormalSlot : MonoBehaviour, Slot {
    private Animator anim;
    private Card card;
    private Card nullCard;

    private void Awake() {
        anim = GetComponent<Animator>();
        nullCard = gameObject.AddComponent<NullCard>();
        card = nullCard;
    }

    public void setCard(Card card) {
        this.card = card;
    }

    public void restoreSlotDefault() {
        card = nullCard;
    }

    public void resolveCard() {
        anim.SetTrigger("resolve");
        card.resolveCard();
    }

    public bool hasCard() {
        return card != nullCard;
    }
}
