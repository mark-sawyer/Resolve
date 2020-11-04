using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionCard : Card {
    public Vector3 direction;

    public override void resolveCard() {
        startGrowAndFadeAnimation();
        character.getCard(this);
        character.resolveDirectionCard(direction);
    }

    public override void completeResolution() {
        GameEvents.cardFinishedResolving.Invoke();
    }
}
