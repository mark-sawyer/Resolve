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
        RaycastHit2D ray = Physics2D.Raycast(mousePos, Vector2.zero);
        if (ray.collider != null && ray.collider.GetComponent<Clickable>() != null) {
            Clickable clickable = ray.collider.GetComponent<Clickable>();
            clickable.respondToClick();
        }
    }

    private void release() {
        clickReleased.Invoke();
    }
}
