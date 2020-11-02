using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionCard : Card {
    public Vector3 direction;

    public override void resolveCard() {
        base.resolveCard();
        character.getCard(this);
        character.resolveDirectionCard(direction);
    }

    public override void completeResolution() {
        base.completeResolution();
        Destroy(gameObject);
    }
}
