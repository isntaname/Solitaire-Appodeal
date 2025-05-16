using UnityEngine;
using Solitaire.Core.Command;

namespace Solitaire.Game.Commands
{
    public class MoveCardCommand : ICommand
    {
        private readonly Card _card;
        private readonly CardStack _fromStack;
        private readonly CardStack _toStack;

        public MoveCardCommand(Card card, CardStack fromStack, CardStack toStack)
        {
            _card = card;
            _fromStack = fromStack;
            _toStack = toStack;
        }

        public void Execute()
        {
            #if UNITY_EDITOR
            Debug.Log($"MoveCardCommand.Execute: Moving card '{_card.name}' from '{_fromStack.name}' to '{_toStack.name}'");
            #endif
            _fromStack.RemoveCard(_card);
            _toStack.AddCard(_card);
        }

        public void Undo()
        {
            #if UNITY_EDITOR
            Debug.Log($"MoveCardCommand.Undo: Moving card '{_card.name}' from '{_toStack.name}' back to '{_fromStack.name}'");
            #endif
            _toStack.RemoveCard(_card);
            _fromStack.AddCard(_card);
        }
    }
} 