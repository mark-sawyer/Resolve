using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ResolvePointer : MonoBehaviour {
    private Compressable compressable;
    private GameObject resolver;
    public DirectionEnum direction;
    public Sprite disabledSprite;

    private void Awake() {
        resolver = Resources.Load<GameObject>("Prefabs/resolver");
        compressable = GetComponent<Compressable>();
        compressable.buttonPressed.AddListener(resolvePressed);
        GameEvents.aResolveWasPressed.AddListener(disableButton);
        GameEvents.resolveFinalised.AddListener(enableButton);
    }

    private void resolvePressed() {
        GameObject newResolver = Instantiate(resolver, transform.position + (Direction.getDir(direction) * Constants.SLOT_DISTANCE), Quaternion.identity);
        newResolver.GetComponent<Resolver>().setDirection(direction);
        newResolver.GetComponent<Resolver>().attemptResolve();
        GameEvents.aResolveWasPressed.Invoke();
    }

    private void disableButton() {
        GetComponent<SpriteRenderer>().sprite = disabledSprite;
        compressable.disableButton();
    }

    private void enableButton() {
        GetComponent<SpriteRenderer>().sprite = GetComponent<Compressable>().uncompressed;
        compressable.enableButton();
    }
}

public class Direction {
    public static Vector3 getDir(DirectionEnum direction) {
        switch (direction) {
            case DirectionEnum.UP:
                return Vector3.up;
            case DirectionEnum.RIGHT:
                return Vector3.right;
            case DirectionEnum.DOWN:
                return Vector3.down;
            case DirectionEnum.LEFT:
                return Vector3.left;
        }

        return Vector2.zero;
    }
}

public enum DirectionEnum {
    UP,
    RIGHT,
    DOWN,
    LEFT
};
