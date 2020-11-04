using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlankCard : Card {
    public override void resolveCard() {
        startGrowAndFadeAnimation();
        print("blank card");
        Invoke("destroyCard", 3);
    }

    public override void completeResolution() {
        GameEvents.cardFinishedResolving.Invoke();
    }

    private void destroyCard() {
        completeResolution();
        Destroy(gameObject);
    }
}
