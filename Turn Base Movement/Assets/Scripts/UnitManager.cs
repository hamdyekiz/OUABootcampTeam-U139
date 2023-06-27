using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitManager : MonoBehaviour
{
    [SerializeField] private HexGrid hexGrid;
    [SerializeField] private MovementSystem movementSystem;
    [SerializeField] private Image healthBar;

    public bool PlayersTurn { get; private set; } = true;

    private Unit selectedUnit;
    private Hex previouslySelectedHex;
    public float healthDecreaseRate = 5f; // Can azalma h�z�
    public float currentHealth = 100f;

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
            // Hareket tamamland�ktan sonra can azaltma i�lemi
            currentHealth -= healthDecreaseRate;
            UpdateHealthBar();

            PlayersTurn = false;
            selectedUnit.MovementFinished += ResetTurn;
            ClearOldSelection();
        }
    }

    private void UpdateHealthBar()
    {
        float healthBarAmount = currentHealth / 100f;
        healthBar.fillAmount = healthBarAmount;
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
        PlayersTurn = true;
    }
}
