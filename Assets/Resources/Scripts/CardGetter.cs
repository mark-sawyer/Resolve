using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardGetter {
    public static GameObject getSameCardTypeGameObject(Card card) {
        if (card is DirectionCard) {
            return Resources.Load<GameObject>("Prefabs/Cards/right card");
        }
        else {
            return Resources.Load<GameObject>("Prefabs/Cards/blank card");
        }
    }
}
