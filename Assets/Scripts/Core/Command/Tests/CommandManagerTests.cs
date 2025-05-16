using NUnit.Framework;
using System;

namespace Solitaire.Core.Command.Tests
{
    public class CommandManagerTests
    {
        private CommandManager _commandManager;

        [SetUp]
        public void Setup()
        {
            // Reset the CommandManager singleton for each test
            typeof(CommandManager)
                .GetField("_instance", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic)
                ?.SetValue(null, null);

            _commandManager = CommandManager.Instance;
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

            // Act - Undo
            _commandManager.Undo();
            Assert.AreEqual(5, command1.GetValue() + command2.GetValue());

            _commandManager.Undo();
            Assert.AreEqual(0, command1.GetValue() + command2.GetValue());
            Assert.IsFalse(_commandManager.CanUndo);
        }

        [Test]
        public void Undo_WhenNoCommands_ShouldNotThrow()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => _commandManager.Undo());
        }

        [Test]
        public void ExecuteCommand_WithNullCommand_ShouldThrowArgumentNullException()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => _commandManager.ExecuteCommand(null));
        }
    }
} 