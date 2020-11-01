using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardGetter {
    public GameObject getSameCardType(Card card) {
        if (card is RightCard) {
            return Resources.Load<GameObject>("Prefabs/right card");
        }
        else {
            return Resources.Load<GameObject>("Prefabs/blank card");
        }
    }
}
