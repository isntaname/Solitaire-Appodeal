using NUnit.Framework;
using System;
using Solitaire.Core.Command;

namespace Solitaire.Core.Command.Tests
{
    public class CommandManagerTests
    {
        private CommandManager _commandManager;
        private bool _lastUndoState;
        private int _undoStateChangeCount;

        [SetUp]
        public void Setup()
        {
            // Reset the CommandManager singleton for each test
            typeof(CommandManager)
                .GetField("_instance", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic)
                ?.SetValue(null, null);

            _commandManager = CommandManager.Instance;
            _lastUndoState = false;
            _undoStateChangeCount = 0;
            _commandManager.OnUndoStateChanged += (canUndo) => 
            {
                _lastUndoState = canUndo;
                _undoStateChangeCount++;
            };
        }

        [Test]
        public void ExecuteCommand_ShouldUpdateValue()
        {
            // Arrange
            var command = new IncrementCommand(5);

            // Act
            _commandManager.ExecuteCommand(command);

            // Assert
            Assert.AreEqual(5, command.GetValue());
            Assert.IsTrue(_commandManager.CanUndo);
            Assert.IsTrue(_lastUndoState);
            Assert.AreEqual(1, _undoStateChangeCount);
        }

        [Test]
        public void Undo_ShouldRestorePreviousState()
        {
            // Arrange
            var command = new IncrementCommand(5);
            _commandManager.ExecuteCommand(command);

            // Act
            _commandManager.Undo();

            // Assert
            Assert.AreEqual(0, command.GetValue());
            Assert.IsFalse(_commandManager.CanUndo);
            Assert.IsFalse(_lastUndoState);
            Assert.AreEqual(2, _undoStateChangeCount);
        }

        [Test]
        public void MultipleCommands_ShouldExecuteAndUndoInCorrectOrder()
        {
            // Arrange
            var command1 = new IncrementCommand(5);
            var command2 = new IncrementCommand(3);

            // Act
            _commandManager.ExecuteCommand(command1);
            _commandManager.ExecuteCommand(command2);

            // Assert
            Assert.AreEqual(8, command1.GetValue() + command2.GetValue());
            Assert.IsTrue(_commandManager.CanUndo);
            Assert.IsTrue(_lastUndoState);
            Assert.AreEqual(2, _undoStateChangeCount);

            // Act - Undo
            _commandManager.Undo();
            Assert.AreEqual(5, command1.GetValue() + command2.GetValue());
            Assert.IsTrue(_lastUndoState);
            Assert.AreEqual(3, _undoStateChangeCount);

            _commandManager.Undo();
            Assert.AreEqual(0, command1.GetValue() + command2.GetValue());
            Assert.IsFalse(_commandManager.CanUndo);
            Assert.IsFalse(_lastUndoState);
            Assert.AreEqual(4, _undoStateChangeCount);
        }

        [Test]
        public void Undo_WhenNoCommands_ShouldNotThrow()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => _commandManager.Undo());
            Assert.AreEqual(0, _undoStateChangeCount);
        }

        [Test]
        public void ExecuteCommand_WithNullCommand_ShouldThrowArgumentNullException()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => _commandManager.ExecuteCommand(null));
            Assert.AreEqual(0, _undoStateChangeCount);
        }

        [Test]
        public void OnUndoStateChanged_ShouldBeTriggeredOnStateChange()
        {
            // Arrange
            var command = new IncrementCommand(5);

            // Act & Assert - Execute
            _commandManager.ExecuteCommand(command);
            Assert.IsTrue(_lastUndoState);
            Assert.AreEqual(1, _undoStateChangeCount);

            // Act & Assert - Undo
            _commandManager.Undo();
            Assert.IsFalse(_lastUndoState);
            Assert.AreEqual(2, _undoStateChangeCount);
        }
    }
} 