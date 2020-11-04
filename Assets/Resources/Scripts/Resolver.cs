using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resolver : MonoBehaviour {
    DirectionEnum direction;

    private void Awake() {
        GameEvents.cardFinishedResolving.AddListener(continueSequence);
    }

    public void setDirection(DirectionEnum direction) {
        this.direction = direction;
    }

    public void attemptResolve() {
        RaycastHit2D ray = Physics2D.Raycast(transform.position, Vector2.zero, 0, LayerMask.GetMask("slot"));
        if (ray.collider != null) {
            Slot slot = ray.collider.GetComponent<Slot>();
            resolveSlot(slot);
            slot.restoreSlotDefault();
        }
        else {
            GameEvents.resolveFinalised.Invoke();
            Destroy(gameObject);
        }
    }

    private void continueSequence() {
        transform.position += Direction.getDir(direction) * Constants.SLOT_DISTANCE;
        attemptResolve();

    }

    public void resolveSlot(Slot slot) {
        slot.resolveCard();
    }
}
