using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class YA_BossBig : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Vector3 sca;
    Vector3 change;

    private void Start()
    {
        change = new Vector3(1.5f, 1.5f, 1.5f);
        sca = transform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
            transform.localScale = change;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
            transform.localScale = sca;
    }
}
