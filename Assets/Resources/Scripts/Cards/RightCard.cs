using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightCard : Card {
    public override void resolveCard() {
        print("right card");
        Invoke("destroyCard", 3);
    }

    private void destroyCard() {
        completeResolution();
        Destroy(gameObject);
    }
}
