using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlankCard : Card {
    public override void resolveCard() {
        print("blank card");
        Invoke("destroyCard", 3);
    }

    private void destroyCard() {
        completeResolution();
        Destroy(gameObject);
    }
}
