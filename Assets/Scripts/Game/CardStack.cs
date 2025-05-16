using System.Collections.Generic;
using UnityEngine;

namespace Solitaire.Game
{
    public class CardStack : MonoBehaviour
    {
        private readonly List<Card> _cards = new List<Card>();

        public void AddCard(Card card)
        {
            if (!_cards.Contains(card))
                _cards.Add(card);
            card.CurrentStack = this;
            card.transform.SetParent(transform);
            card.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        }

        public void RemoveCard(Card card)
        {
            _cards.Remove(card);
            if (card.CurrentStack == this)
                card.CurrentStack = null;
        }
    }
} 