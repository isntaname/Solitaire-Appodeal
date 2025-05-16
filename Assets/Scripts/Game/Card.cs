using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Solitaire.Core.Command;
using Solitaire.Game.Commands;

namespace Solitaire.Game
{
    public class Card : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
    {
        public CardStack CurrentStack { get; set; }
        private Vector2 _dragOffset;
        private RectTransform _rectTransform;
        private CardStack _originalStack;
        private Graphic _graphic;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _graphic = GetComponent<Graphic>();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _originalStack = CurrentStack;
            if (_graphic != null) _graphic.raycastTarget = false;
            transform.SetParent(transform.root);
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _rectTransform,
                eventData.position,
                eventData.pressEventCamera,
                out _dragOffset
            );
            transform.SetAsLastSibling();
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                (RectTransform)transform.parent,
                eventData.position,
                eventData.pressEventCamera,
                out Vector2 localPoint))
            {
                _rectTransform.localPosition = localPoint - _dragOffset;
            }
        }

        public void OnDrop(PointerEventData eventData)
        {
            // Removed command execution to prevent double execution
        }

        /// <summary>
        /// Finds a valid target stack for the dragged card by checking for rectangle overlaps.
        /// A stack is considered valid if:
        /// 1. It's not the original stack the card came from
        /// 2. The card's rectangle overlaps with the stack's rectangle in screen space
        /// </summary>
        /// <returns>The first valid target stack found, or null if no valid target exists</returns>
        private CardStack FindOverlappingStack()
        {
            // Get the screen rect of the dragged card
            Vector3[] corners = new Vector3[4];
            _rectTransform.GetWorldCorners(corners);
            Rect cardRect = new Rect(
                corners[0].x,
                corners[0].y,
                corners[2].x - corners[0].x,
                corners[2].y - corners[0].y
            );

            var allStacks = FindObjectsByType<CardStack>(FindObjectsSortMode.None);
            foreach (var stack in allStacks)
            {
                if (stack == _originalStack) continue;

                var stackRectTransform = stack.GetComponent<RectTransform>();
                Vector3[] stackCorners = new Vector3[4];
                stackRectTransform.GetWorldCorners(stackCorners);
                Rect stackRect = new Rect(
                    stackCorners[0].x,
                    stackCorners[0].y,
                    stackCorners[2].x - stackCorners[0].x,
                    stackCorners[2].y - stackCorners[0].y
                );

                if (cardRect.Overlaps(stackRect))
                {
                    return stack;
                }
            }
            return null;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (_graphic != null) _graphic.raycastTarget = true;
            
            var targetStack = FindOverlappingStack();
            if (targetStack != null)
            {
                CommandManager.Instance.ExecuteCommand(new MoveCardCommand(this, _originalStack, targetStack));
            }
            else if (transform.parent == transform.root)
            {
                _originalStack.AddCard(this);
            }
        }
    }
} 