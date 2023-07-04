using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UnitManager : MonoBehaviour
{
    [SerializeField] private HexGrid hexGrid;
    [SerializeField] private MovementSystem movementSystem;
    [SerializeField] private GameObject confirmationPopup;

    public bool PlayersTurn { get; private set; } = true;

    private Unit selectedUnit;
    private Hex previouslySelectedHex;
    public Slider healthBar1;
    public float healthDecreaseRate = 1f;
    public float currentHealth = 100f;

    private bool isGameOver = false;
    private bool isEndOfTurn = false;
    private bool isPopupActive = false;

    public GameManagerScript gameManager;

    public void HandleUnitSelected(GameObject unit)
    {
        if (!PlayersTurn)
            return;

        Unit unitReference = unit.GetComponent<Unit>();

        if (CheckIfTheSameUnitSelected(unitReference))
            return;

        PrepareUnitForMovement(unitReference);
    }

    private bool CheckIfTheSameUnitSelected(Unit unitReference)
    {
        if (selectedUnit == unitReference)
        {
            ClearOldSelection();
            return true;
        }
        return false;
    }

    public void HandleTerrainSelected(GameObject hexGO)
    {
        if (selectedUnit == null || !PlayersTurn)
            return;

        Hex selectedHex = hexGO.GetComponent<Hex>();

        if (HandleHexOutOfRange(selectedHex.HexCoords) || HandleSelectedHexIsUnitHex(selectedHex.HexCoords))
            return;

        HandleTargetHexSelected(selectedHex);
    }

    private void PrepareUnitForMovement(Unit unitReference)
    {
        if (selectedUnit != null)
            ClearOldSelection();

        selectedUnit = unitReference;
        selectedUnit.Select();
        movementSystem.ShowRange(selectedUnit, hexGrid);
    }

    private void ClearOldSelection()
    {
        previouslySelectedHex = null;
        selectedUnit.Deselect();
        movementSystem.HideRange(hexGrid);
        selectedUnit = null;
    }

    private void HandleTargetHexSelected(Hex selectedHex)
    {
        if (previouslySelectedHex == null || previouslySelectedHex != selectedHex)
        {
            previouslySelectedHex = selectedHex;
            movementSystem.ShowPath(selectedHex.HexCoords, hexGrid);
        }
        else
        {
            movementSystem.MoveUnit(selectedUnit, hexGrid);

            currentHealth -= healthDecreaseRate;
            UpdateHealthBar();

            if (currentHealth <= 0 && !isGameOver)
            {
                isGameOver = true;
                gameManager.gameOver();
            }
            else
            {
                PlayersTurn = false;
                selectedUnit.MovementFinished += ResetTurn;
                ClearOldSelection();
                isEndOfTurn = true;
            }
        }
    }

    private void UpdateHealthBar()
    {
        float fillAmount = currentHealth / 100f;
        healthBar1.value = fillAmount;
    }

    private bool HandleSelectedHexIsUnitHex(Vector3Int hexPosition)
    {
        if (hexPosition == hexGrid.GetClosestHex(selectedUnit.transform.position))
        {
            selectedUnit.Deselect();
            ClearOldSelection();
            return true;
        }
        return false;
    }

    private bool HandleHexOutOfRange(Vector3Int hexPosition)
    {
        if (!movementSystem.IsHexInRange(hexPosition))
        {
            Debug.Log("Hex Out of range!");
            return true;
        }
        return false;
    }

    private void ResetTurn(Unit selectedUnit)
    {
        selectedUnit.MovementFinished -= ResetTurn;
        isEndOfTurn = true;
        isPopupActive = false;
    }

    private void ShowConfirmationPopup()
    {
        confirmationPopup.SetActive(true);
    }

    private void HandleTurnEnd()
    {
        confirmationPopup.SetActive(true);
    }

    public void HandleConfirmation(bool confirmed)
    {
        if (confirmed)
        {
          
        }
        else
        {
           
            PlayersTurn = true;
        }

        confirmationPopup.SetActive(false);
    }

    private void Update()
    {
        if (isEndOfTurn && !isPopupActive)
        {
            isPopupActive = true;
            Invoke("HandleTurnEnd", 1.3f);
        }
    }
}