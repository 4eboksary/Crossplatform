using System;
using System.Collections.Generic;
using System.IO;
using Xunit;
using Lab1;

namespace Lab1_Test
{
    public class ProgramTests
    {
        [Fact]
        public void Test_EqualScores()
        {
            // Arrange
            var columns = new Program.Column[]
            {
                new Program.Column(3, 3),
                new Program.Column(2, 2),
                new Program.Column(1, 1)
            };

            // Act
            var result = Program.CalculateGameResult(columns);

            // Assert
            Assert.Equal(0, result); // Сума очок рівна
        }

        [Fact]
        public void Test_FirstPlayerWins()
        {
            // Arrange
            var columns = new Program.Column[]
            {
                new Program.Column(5, 1),
                new Program.Column(3, 2),
                new Program.Column(4, 3)
            };

            // Act
            var result = Program.CalculateGameResult(columns);

            // Assert
            Assert.Equal(6, result); // Перший гравець перемагає
        }

        [Fact]
        public void Test_SecondPlayerWins()
        {
            // Arrange
            var columns = new Program.Column[]
            {
                new Program.Column(2, 5),
                new Program.Column(3, 6),
                new Program.Column(1, 7)
            };

            // Act
            var result = Program.CalculateGameResult(columns);

            // Assert
            Assert.Equal(-9, result); // Другий гравець перемагає
        }

        [Fact]
        public void Test_OneColumn()
        {
            // Arrange
            var column = new Program.Column[] { new Program.Column(4, 3) };

            // Act
            var result = Program.CalculateGameResult(column);

            // Assert
            Assert.Equal(4, result); // Лише один стовпець, перший гравець перемагає
        }

        [Fact]
        public void Test_LargeValues()
        {
            // Arrange
            var columns = new Program.Column[]
            {
                new Program.Column(1000, 1),
                new Program.Column(900, 100),
                new Program.Column(800, 200)
            };

            // Act
            var result = Program.CalculateGameResult(columns);

            // Assert
            Assert.Equal(1699, result); // Перший гравець отримує перевагу
        }
    }
 }