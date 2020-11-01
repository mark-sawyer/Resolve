using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResolveButton : MonoBehaviour {
    private Compressable compressable;

    void Awake() {
        compressable = GetComponent<Compressable>();
        compressable.buttonPressed.AddListener(disableButton);
        GameObject.Find("slots").GetComponent<Slots>().allSlotsResolved.AddListener(enableButton);
    }

    private void disableButton() {
        compressable.disableButton();
    }

    private void enableButton() {
        compressable.enableButton();
    }
}
