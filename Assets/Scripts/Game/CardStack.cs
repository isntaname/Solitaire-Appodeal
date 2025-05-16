using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Solitaire.Game
{
    public class CardStack : MonoBehaviour
    {
        private readonly List<Card> _cards = new List<Card>();
        private LayoutGroup _layoutGroup;

        private void Awake()
        {
            _layoutGroup = GetComponent<LayoutGroup>();
        }

        public IReadOnlyList<Card> GetCards() => _cards;

        public void AddCard(Card card)
        {
            if (!_cards.Contains(card))
                _cards.Add(card);
            card.CurrentStack = this;
            card.transform.SetParent(transform);
            card.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

            // Force layout rebuild
            if (_layoutGroup != null)
            {
                LayoutRebuilder.ForceRebuildLayoutImmediate(transform as RectTransform);
            }
        }

        public void RemoveCard(Card card)
        {
            _cards.Remove(card);
            if (card.CurrentStack == this)
                card.CurrentStack = null;

            // Force layout rebuild after removal
            if (_layoutGroup != null)
            {
                LayoutRebuilder.ForceRebuildLayoutImmediate(transform as RectTransform);
            }
        }
    }
} 