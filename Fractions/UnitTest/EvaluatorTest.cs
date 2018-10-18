namespace UnitTest
{
    using Fractions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;

    [TestClass]
    public class EvaluatorTest
    {
        [TestMethod]
        public void InputWithWellFormattedParenthesis_ReturnsFraction()
        {
            Evaluator evaluator = new Evaluator("1/2  * ( 2_3/8 + 9/8 ) / 12".Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));
            string result = evaluator.Evaluate();
            Assert.AreEqual(result, "7/48");
        }

        [TestMethod]
        public void InputWithParameters_ReturnsFraction()
        {
            Evaluator evaluator = new Evaluator("1/2 * 3_3/4".Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));
            string result = evaluator.Evaluate();
            Assert.AreEqual(result, "1_7/8");
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void InputMalFormedParenthesis_ReturnsException()
        {
            Evaluator evaluator = new Evaluator("1/2  * (2_3/8 + 9/8) / 12".Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));
            string result = evaluator.Evaluate();
        }
    }
}
