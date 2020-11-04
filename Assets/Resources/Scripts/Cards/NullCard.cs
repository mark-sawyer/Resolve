using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NullCard : Card {
    public override void resolveCard() {
        print("null card");
        Invoke("completeResolution", 3);
    }
    public override void completeResolution() {
        GameEvents.cardFinishedResolving.Invoke();
    }
}
