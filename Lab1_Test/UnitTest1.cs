using Xunit;
using Lab1;

namespace Lab1_Tests
{
    public class ProgramTests
    {
        [Fact]
        public void CalculateGameResult_ShouldReturnZero_ForEmptyArray()
        {
            // Arrange
            var columns = new Program.Column[0];

            // Act
            var result = Program.CalculateGameResult(columns);

            // Assert
            Assert.Equal(0, result);
        }

        [Fact]
        public void CalculateGameResult_ShouldReturnCorrectResult_ForSingleColumn()
        {
            // Arrange
            var columns = new Program.Column[]
            {
                new Program.Column(10, 5)
            };

            // Act
            var result = Program.CalculateGameResult(columns);

            // Assert
            Assert.Equal(10, result);
        }

        [Fact]
        public void CalculateGameResult_ShouldReturnCorrectResult_ForMultipleColumns()
        {
            // Arrange
            var columns = new Program.Column[]
            {
                new Program.Column(10, 5),
                new Program.Column(3, 7),
                new Program.Column(8, 6)
            };

            // Act
            var result = Program.CalculateGameResult(columns);

            // Assert
            Assert.Equal(7, result); // Результат для цього прикладу залежить від логіки обчислення
        }
    }
}
