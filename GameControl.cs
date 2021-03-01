using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameControl : MonoBehaviour
{
    public static event Action SpinTapped = delegate { };

    [SerializeField]
    private Text prizeText;

    [SerializeField]
    private Row[] rows;

    [SerializeField]
    private Transform spinbtn;

    public GameObject  _SelectedSq;

    private int prizeValue;

    private bool resultsChecked = false;

    public static bool _HandledPulled;

    public static bool _HeartSelected, _CherrySelected, _HorseShoeSelected, _BellSelected, _NoneSelected;          //If any selected will always be the winning type.

    // Update is called once per frame
    void Update()
    {
        SelectedSpinTypes();

        if (!rows[0].rowStopped || !rows[1].rowStopped || !rows[2].rowStopped) {
            prizeValue = 0;
            prizeText.enabled = false;
            resultsChecked = false;
        }

        if (rows[0].rowStopped && rows[1].rowStopped && rows[2].rowStopped && !resultsChecked) {
            CheckResults();
            prizeText.enabled = true;
            prizeText.text = "Prize: $" + prizeValue;
        }
    }

    //Disables all that has been selected.
    void DisableAllSelections() {
        _HeartSelected = false;
        _CherrySelected = false;
        _HorseShoeSelected = false;
        _BellSelected = false;
        _NoneSelected = false;
    }

    //Move the selected box back and forth between selected spin types.
    void SelectedSpinTypes() {
        //Create a blank variable pos
        var pos = _SelectedSq.transform.position;

        if (Input.GetKeyDown(KeyCode.H)) {
            DisableAllSelections();
            _HeartSelected = true;
            _SelectedSq.SetActive(true);
            pos.x = -6.32f;
            _SelectedSq.transform.position = pos;
        }

        if (Input.GetKeyDown(KeyCode.C)) {
            DisableAllSelections();
            _CherrySelected = true;
            _SelectedSq.SetActive(true);
            pos.x = -2.78f;
            _SelectedSq.transform.position = pos;
        }

        if (Input.GetKeyDown(KeyCode.U)) {
            DisableAllSelections();
            _HorseShoeSelected = true;
            _SelectedSq.SetActive(true);
            pos.x = 0.71f;
            _SelectedSq.transform.position = pos;
        }

        if (Input.GetKeyDown(KeyCode.B)) {
            DisableAllSelections();
            _BellSelected = true;
            _SelectedSq.SetActive(true);
            pos.x = 4.08f;
            _SelectedSq.transform.position = pos;
        }

        if (Input.GetKeyDown(KeyCode.D)) {
            DisableAllSelections();
            _NoneSelected = true;
            _SelectedSq.SetActive(false);
        }
    }

    private void OnMouseDown() {
        if (!_HandledPulled) {
            if (rows[0].rowStopped && rows[1].rowStopped && rows[2].rowStopped)
            SpinButton();
        }
    }

    void SpinButton() {
        if (GameManager._BetLevel == 1) {
            GameManager._Currency -= 1.50f;
        } else if (GameManager._BetLevel == 2) {
            GameManager._Currency -= 3.00f;
        }
        _HandledPulled = true;
       SpinTapped();

    }

    private void CheckResults() {
        if (rows[0].stoppedSlot == "Heart"
        && rows[1].stoppedSlot == "Heart"
        && rows[2].stoppedSlot == "Heart")
        prizeValue = (int)2.00f;

        else if (rows[0].stoppedSlot == "Cherry"
        && rows[1].stoppedSlot == "Cherry"
        && rows[2].stoppedSlot == "Cherry")
        prizeValue = (int)3.00;

        else if (rows[0].stoppedSlot == "Horseshoe"
        && rows[1].stoppedSlot == "Horseshoe"
        && rows[2].stoppedSlot == "Horseshoe")
        prizeValue = (int)5.00;

        else if (rows[0].stoppedSlot == "Bell"
        && rows[1].stoppedSlot == "Bell"
        && rows[2].stoppedSlot == "Bell")
        prizeValue = (int)6.00;
    }
}
