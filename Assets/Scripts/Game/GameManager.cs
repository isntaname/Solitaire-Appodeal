using UnityEngine;
namespace Solitaire.Game
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private CardStack[] _stacks;
        [SerializeField] private int _cardsPerStack = 5;
        [SerializeField] private Card _cardPrefab;

        private void Start()
        {
            InitializeGame();
        }

        private void InitializeGame()
        {
            foreach (var stack in _stacks)
            {
                for (int i = 0; i < _cardsPerStack; i++)
                {
                    var card = Instantiate(_cardPrefab, stack.transform);
                    card.gameObject.name = $"Card_{i}";
                    stack.AddCard(card);
                }
            }
        }
    }
} 