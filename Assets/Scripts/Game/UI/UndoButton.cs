using UnityEngine;
using UnityEngine.UI;
using Solitaire.Core.Command;

namespace Solitaire.Game.UI
{
    public class UndoButton : MonoBehaviour
    {
        private Button _button;
        
        private void Awake()
        {
            _button = GetComponent<Button>();
            CommandManager.Instance.OnUndoStateChanged += HandleUndoStateChanged;
            _button.interactable = CommandManager.Instance.CanUndo;
            _button.onClick.AddListener(OnClick);
        }

        private void OnDestroy()
        {
            if (CommandManager.Instance != null)
            {
                CommandManager.Instance.OnUndoStateChanged -= HandleUndoStateChanged;
            }
            _button.onClick.RemoveListener(OnClick);
        }

        private void HandleUndoStateChanged(bool canUndo)
        {
            _button.interactable = canUndo;
        }

        private void OnClick()
        {
            CommandManager.Instance.Undo();
        }
    }
} 