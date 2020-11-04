using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MouseInput : MonoBehaviour {
    public readonly UnityEvent clickReleased = new UnityEvent();

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            click();
        }

        if (Input.GetMouseButtonUp(0)) {
            release();
        }
    }

    private void click() {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D[] ray = Physics2D.RaycastAll(mousePos, Vector2.zero);
        if (ray.Length != 0) {
            for (int i = 0; i < ray.Length; i++) {
                if (ray[i].collider.GetComponent<Clickable>() != null) {
                    ray[i].collider.GetComponent<Clickable>().respondToClick();
                    break;
                }
            }
        }
    }

    private void release() {
        clickReleased.Invoke();
    }
}
