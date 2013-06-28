using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace StringCalculator
{
    public class Calculator
    {
        public int Add(string numbers)
        {
            char delimiter;
            if (numbers.StartsWith("//")) //check for other delimiters defined after //
            {
                delimiter = numbers[2]; //delimiter is 3rd character
                numbers = numbers.Substring(4); //numbers string now starts after //;/n
            }
            else
            {
                delimiter = ',';
            }           
            //empty string
            if (string.IsNullOrEmpty(numbers))
                return 0;
            //one number (delimiter not found)
            if (numbers.IndexOf(delimiter).Equals(-1))
                return Convert.ToInt32(numbers);
            //more than one number

            var answer = 0;
            var values = numbers.Split(',','\n',delimiter);
            var negativesList = "";
            foreach (var s in values)
            {
                if (Convert.ToInt32(s) < 0)
                {
                    negativesList = negativesList + ',' + s;
                }
                if (Convert.ToInt32(s) > 1000)
                {
                    answer += 0;
                }
                else
                {
                    answer += Convert.ToInt32(s);                    
                }
            }
            if (string.IsNullOrEmpty(negativesList))
            {
                return answer;                
            }
            else
            {
                throw new ArgumentException("Negatives not allowed:"+negativesList);
            }
        }
    }

    [TestFixture]
    public class CalculatorTests
    {
        [Test]
        public void EmptyStringShouldReturnZero()
        {
            var calc = new Calculator();
            var result = calc.Add("");
            Assert.AreEqual(0,result);
        }

        [Test]
        public void SingleNumberShouldReturnSameNumber()
        {
            var calc = new Calculator();
            var result = calc.Add("1");
            Assert.AreEqual(1,result);
        }

        [Test]
        public void TwoNumbersShouldReturnSum()
        {
            var calc = new Calculator();
            var result = calc.Add("1,2");
            Assert.AreEqual(3,result);
        }

        [Test]
        public void ThreeNumbersShouldReturnSum()
        {
            var calc = new Calculator();
            var result = calc.Add("1,2,3");
            Assert.AreEqual(6, result);            
        }

        [Test]
        public void ThreeNumbersWithNewLineShouldReturnSum()
        {
            var calc = new Calculator();
            var result = calc.Add("1\n2,3");
            Assert.AreEqual(6, result);
        }

        [Test]
        public void DifferentDelimiterShouldReturnSum()
        {
            var calc = new Calculator();
            var result = calc.Add("//;\n1;2");
            Assert.AreEqual(3, result);
        }

        [Test]
        public void NegativeNumberShouldThrowException()
        {
            var calc = new Calculator();
            var result = calc.Add("-1,2");
            Assert.AreEqual("Negatives not allowed:,-1", result);
        }

        [Test]
        public void NumberGreaterThanThousandShouldBeIgnored()
        {
            var calc = new Calculator();
            var result = calc.Add("1001,2");
            Assert.AreEqual(2, result);
        }

    }
}
