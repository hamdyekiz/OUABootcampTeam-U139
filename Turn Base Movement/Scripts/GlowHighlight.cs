using System.Collections.Generic;
using UnityEngine;

public class GlowHighlight : MonoBehaviour
{
    private Dictionary<Renderer, Material[]> originalMaterialDictionary = new Dictionary<Renderer, Material[]>();
    private Dictionary<Renderer, Material[]> glowMaterialDictionary = new Dictionary<Renderer, Material[]>();
    private Dictionary<Color, Material> cachedGlowMaterials = new Dictionary<Color, Material>();

    public Material glowMaterial;
    private bool isGlowing = false;

    private void Awake()
    {
        PrepareMaterialDictionaries();
    }

    private void PrepareMaterialDictionaries()
    {
        Renderer[] renderers = GetComponentsInChildren<Renderer>();

        foreach (Renderer renderer in renderers)
        {
            Material[] originalMaterials = renderer.sharedMaterials;
            originalMaterialDictionary[renderer] = originalMaterials;

            Material[] newMaterials = new Material[originalMaterials.Length];

            for (int i = 0; i < originalMaterials.Length; i++)
            {
                Material mat = null;
                Color materialColor = originalMaterials[i].color;

                if (!cachedGlowMaterials.TryGetValue(materialColor, out mat))
                {
                    mat = new Material(glowMaterial);
                    mat.color = materialColor;
                    cachedGlowMaterials[materialColor] = mat;
                }

                newMaterials[i] = mat;
            }

            glowMaterialDictionary[renderer] = newMaterials;
        }
    }

    private void SetGlowColor(Color color)
    {
        foreach (Material[] materials in glowMaterialDictionary.Values)
        {
            foreach (Material material in materials)
            {
                material.SetColor("_GlowColor", color);
            }
        }
    }

    internal void HighlightValidPath()
    {
        if (!isGlowing)
            return;

        SetGlowColor(Color.green);
    }

    internal void ResetGlowHighlight()
    {
        SetGlowColor(glowMaterial.GetColor("_GlowColor"));
    }

    public void ToggleGlow()
    {
        isGlowing = !isGlowing;

        if (isGlowing)
        {
            SetGlowColor(glowMaterial.GetColor("_GlowColor"));
            ApplyGlowMaterials();
        }
        else
        {
            RevertOriginalMaterials();
        }
    }

    private void ApplyGlowMaterials()
    {
        foreach (Renderer renderer in glowMaterialDictionary.Keys)
        {
            renderer.sharedMaterials = glowMaterialDictionary[renderer];
        }
    }

    private void RevertOriginalMaterials()
    {
        foreach (Renderer renderer in originalMaterialDictionary.Keys)
        {
            renderer.sharedMaterials = originalMaterialDictionary[renderer];
        }
    }

    public void ToggleGlow(bool state)
    {
        if (isGlowing == state)
            return;

        ToggleGlow();
    }
}
