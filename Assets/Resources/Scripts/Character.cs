using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {
    private Action action;
    private Vector3 goalPosition;
    private Card currentCard;

    private void Awake() {
        action = () => { };
    }

    private void Update() {
        action();
    }

    public void getCard(Card card) {
        currentCard = card;
    }

    public void resolveRightCard() {
        if (spaceIsAvailable(Vector3.right)) {
            goalPosition = transform.position + Vector3.right;
            action = moveToGoalPosition;
        }
        else {
            currentCard.Invoke("completeResolution", 2);
        }
    }

    private bool spaceIsAvailable(Vector3 direction) {
        RaycastHit2D ray = Physics2D.Raycast(transform.position + direction, Vector2.zero, 0, LayerMask.GetMask("tile"));
        return ray.collider != null;
    }

    private void moveToGoalPosition() {
        Vector3 direction = goalPosition - transform.position;
        transform.position += direction * 0.05f;
        float distance = (goalPosition - transform.position).magnitude;
        if (distance < 0.01f) {
            transform.position = goalPosition;
            action = () => { };
            currentCard.completeResolution();
        }
    }
}
