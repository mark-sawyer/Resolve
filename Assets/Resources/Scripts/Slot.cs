using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour {
    public bool hasCard { get; private set; }
    private GameObject card;

    public void setCard(GameObject card) {
        this.card = card;
        hasCard = true;
    }

    public void removeCard() {
        card = null;
        hasCard = false;
    }
}
