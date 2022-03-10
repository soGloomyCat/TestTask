using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SwipeController : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    [SerializeField] private PlayerMover _player;

    public void OnBeginDrag(PointerEventData eventData)
    {
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (Mathf.Abs(eventData.delta.x) > Mathf.Abs(eventData.delta.y))
        {
            if (eventData.delta.x > 0)
            {
                if (_player.IsAvailableNextClick)
                    _player.Move(3);
            }
            else
            {
                if (_player.IsAvailableNextClick)
                    _player.Move(2);
            }
        }
    }
}
