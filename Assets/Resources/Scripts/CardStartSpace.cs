using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardStartSpace : MonoBehaviour {
    public int number;
    private Sprite[] numberSprites;
    private SpriteRenderer numberPanelSpriteRenderer;
    private GameObject cardInSpaceGameObject;
    private Vector3 cardPosition;

    private void Awake() {
        numberSprites = Resources.LoadAll<Sprite>("Sprites/numbers");
        numberPanelSpriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        numberPanelSpriteRenderer.sprite = numberSprites[number];
        setupCardInSpace(transform.GetChild(1).gameObject);
        cardPosition = cardInSpaceGameObject.transform.position;
    }

    private void removeCardFromSpace(GameObject cardRemovedFromSpace) {
        cardRemovedFromSpace.GetComponent<MovableCard>().pickedUpFromStartSpace.RemoveListener(removeCardFromSpace);
        cardRemovedFromSpace.GetComponent<MovableCard>().putBackInStartSpace.AddListener(putCardBackInSpace);

        if (notInfinity()) {
            number--;
            numberPanelSpriteRenderer.sprite = numberSprites[number];
        }

        if (number > 0) {
            GameObject sameCardType = getSameCardTypeGameObject(cardRemovedFromSpace.GetComponent<Card>());
            GameObject nextCard = Instantiate(sameCardType, cardPosition, Quaternion.identity);
            nextCard.transform.parent = transform;
            setupCardInSpace(nextCard);
        }
        else {
            cardInSpaceGameObject = null;
        }
    }

    private void putCardBackInSpace(GameObject cardBeingPutBack) {
        cardBeingPutBack.GetComponent<MovableCard>().putBackInStartSpace.RemoveListener(putCardBackInSpace);

        if (notInfinity()) {
            number++;
            numberPanelSpriteRenderer.sprite = numberSprites[number];
        }

        if (cardInSpaceGameObject != null) {
            cardInSpaceGameObject.GetComponent<MovableCard>().pickedUpFromStartSpace.RemoveListener(removeCardFromSpace);
            Destroy(cardInSpaceGameObject);
        }
        setupCardInSpace(cardBeingPutBack);
    }

    private void setupCardInSpace(GameObject cardInSpace) {
        cardInSpaceGameObject = cardInSpace;
        MovableCard movableCard = cardInSpace.GetComponent<MovableCard>();
        movableCard.pickedUpFromStartSpace.AddListener(removeCardFromSpace);
    }

    private bool notInfinity() {
        return number != 10;
    }

    private GameObject getSameCardTypeGameObject(Card card) {
        if (card is RightCard) {
            return Resources.Load<GameObject>("Prefabs/Cards/right card");
        }
        else {
            return Resources.Load<GameObject>("Prefabs/Cards/blank card");
        }
    }
}
