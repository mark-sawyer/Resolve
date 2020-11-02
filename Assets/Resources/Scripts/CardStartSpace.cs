using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardStartSpace : MonoBehaviour {
    public int number;
    public GameObject cardPrefab;
    private Sprite[] numberSprites;
    private SpriteRenderer numberPanelSpriteRenderer;
    private GameObject cardInSpaceGameObject;
    private Vector3 RELATIVE_CARD_POSITION = new Vector3(0f, -0.359375f, 0f);

    private void Awake() {
        numberSprites = Resources.LoadAll<Sprite>("Sprites/numbers");
        numberPanelSpriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        numberPanelSpriteRenderer.sprite = numberSprites[number];

        cardInSpaceGameObject = Instantiate(cardPrefab, transform.position + RELATIVE_CARD_POSITION, Quaternion.identity);
        cardInSpaceGameObject.transform.parent = transform;
        setupCardInSpace(cardInSpaceGameObject);
    }

    private void removeCardFromSpace(GameObject cardRemovedFromSpace) {
        cardRemovedFromSpace.GetComponent<MovableCard>().pickedUpFromStartSpace.RemoveListener(removeCardFromSpace);
        cardRemovedFromSpace.GetComponent<MovableCard>().putBackInStartSpace.AddListener(putCardBackInSpace);

        if (notInfinity()) {
            number--;
            numberPanelSpriteRenderer.sprite = numberSprites[number];
        }

        if (number > 0) {
            GameObject nextCard = Instantiate(cardPrefab, transform.position + RELATIVE_CARD_POSITION, Quaternion.identity);
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
}
