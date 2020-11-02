using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface Slot {
    void resolveCard();

    UnityEvent getResolutionEvent();

    void restoreSlotDefault();
}
