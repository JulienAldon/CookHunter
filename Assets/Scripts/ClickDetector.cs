using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickDetector : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        Game.recipes[0].validateIngredient(eventData.pointerCurrentRaycast.gameObject.GetComponent<Ingredient>().type);
        if (Game.recipes[0].isRecipeDone()) {
            Game.recipes.RemoveAt(0);
            Game.score += 1;
        }
        Destroy(eventData.pointerCurrentRaycast.gameObject);
    }
}
