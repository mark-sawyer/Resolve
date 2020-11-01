using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightCard : Card {
    public override void resolveCard() {
        base.resolveCard();
        character.getCard(this);
        character.resolveRightCard();
        print("right card");
    }

    public override void completeResolution() {
        base.completeResolution();
        Destroy(gameObject);
    }
}
