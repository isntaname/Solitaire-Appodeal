namespace Solitaire.Core.Command.Tests
{
    public class IncrementCommand : ICommand
    {
        private readonly int _amount;
        private int _value;

        public IncrementCommand(int amount)
        {
            _amount = amount;
            _value = 0;
        }

        public void Execute()
        {
            _value += _amount;
        }

        public void Undo()
        {
            _value -= _amount;
        }

        public int GetValue() => _value;
    }
} 