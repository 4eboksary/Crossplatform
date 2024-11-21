using System; 
using Xunit;
using Lab2;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace Lab2_Test
{
    public class UnitTest2
    {
        [Fact]
        public void Test_Count_Strings_N1()
        {
            int N = 1;
            int expected = 2; // "0", "1"

            int result = Lab2.Program.Count_Strings(N);

            Console.WriteLine($"Test for N={N} passed with result: {result}");

            Assert.Equal(expected, result);

            if (result == expected)
            {
                Console.WriteLine("Test passed");
            }
            else
            {
                Console.WriteLine("Test failed");
            }
            Console.WriteLine();
        }


        [Fact]
        public void Test_Count_Strings_N2()
        {
            int N = 2;
            int expected = 3; // "00", "01", "10"

            int result = Lab2.Program.Count_Strings(N);

            Console.WriteLine($"Test for N={N} passed with result: {result}");

            Assert.Equal(expected, result);

            if (result == expected)
            {
                Console.WriteLine("Test passed");
            }
            else
            {
                Console.WriteLine("Test failed");
            }
            Console.WriteLine();
        }

        [Fact]
        public void Test_Count_Strings_N3()
        {
            int N = 3;
            int expected = 5; // "000", "001", "010", "100", "101"

            int result = Lab2.Program.Count_Strings(N);

            Assert.Equal(expected, result);

            Console.WriteLine($"Test for N={N} passed with result: {result}");

            if (result == expected)
            {
                Console.WriteLine("Test passed");
            }
            else
            {
                Console.WriteLine("Test failed");
            }
            Console.WriteLine();
        }

        [Fact]
        public void Test_Count_Strings_OutOfRange()
        {
            int N = 1001;

            int result = Lab2.Program.Count_Strings(N);

            int expected = 0;

            Assert.Equal(expected, result);

            Console.WriteLine($"Test for N={N} passed: Output was 0 as expected for out of range input.");

            if (result == expected)
            {
                Console.WriteLine("Test passed");
            }
            else
            {
                Console.WriteLine("Test failed");
            }
            Console.WriteLine();
        }

        [Fact]
        public void Test_Count_Strings_N0()
        {
            int N = 0;

            int expected = 0;

            int result = Lab2.Program.Count_Strings(N);

            Assert.Equal(expected, result);

            Console.WriteLine($"Test for N={N} passed: Output was 0 as expected for out of range input.");

            if (result == expected)
            {
                Console.WriteLine("Test passed");
            }
            else
            {
                Console.WriteLine("Test failed");
            }
            Console.WriteLine();
        }

    }
}