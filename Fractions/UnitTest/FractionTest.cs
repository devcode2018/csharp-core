namespace UnitTest
{
    using Fractions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;

    [TestClass]
    public class FractionTest
    {
        #region Add test
        [TestMethod]
        public void MixedFractionAddWholeNumber_ReturnsMixedFraction()
        {
            Fraction a = new Fraction("3_3/4");
            string result = a.Add(new Fraction("3")).ToString();
            Assert.AreEqual(result, "6_3/4");
        }

        [TestMethod]
        public void MixedFractionAddImproperFraction_ReturnsMixedFraction()
        {
            Fraction a = new Fraction("3_3/4");
            string result = a.Add(new Fraction("6/4")).ToString();
            Assert.AreEqual(result, "5_1/4");
        }

        [TestMethod]
        public void MixedFractionAddProperFraction_ReturnsMixedFraction()
        {
            Fraction a = new Fraction("3_3/4");
            string result = a.Add(new Fraction("2/4")).ToString();
            Assert.AreEqual(result, "4_1/4");
        }

        [TestMethod]
        public void MixedFractionAddNegativeProperFraction_ReturnsMixedFraction()
        {
            Fraction a = new Fraction("3_3/4");
            string result = a.Add(new Fraction("-2/4")).ToString();
            Assert.AreEqual(result, "3_1/4");
        }

        [TestMethod]
        public void WholeNumberAddWholeNumber_ReturnsFraction()
        {
            Fraction a = new Fraction("3");
            string result = a.Add(new Fraction("2")).ToString();
            Assert.AreEqual(result, "5");
        }
        #endregion

        #region substract test
        [TestMethod]
        public void ProperFractionSubstractProperFraction_ReturnsProperFraction()
        {
            Fraction a = new Fraction("4/5");
            string result = a.Subtract(new Fraction("3/5")).ToString();
            Assert.AreEqual(result, "1/5");
        }

        [TestMethod]
        public void ProperFractionSubstractMixedFraction_ReturnsMIxedFraction()
        {
            Fraction a = new Fraction("2/5");
            string result = a.Subtract(new Fraction("2_3/5")).ToString();
            Assert.AreEqual(result, "-2_1/5");
        }

        [TestMethod]
        public void WholeNumberSubstractMixedFraction_ReturnsMIxedFraction()
        {
            Fraction a = new Fraction("5");
            string result = a.Subtract(new Fraction("2_3/5")).ToString();
            Assert.AreEqual(result, "2_2/5");
        }

        [TestMethod]
        public void ProperFractionSubstractImproperFraction_ReturnsMIxedFraction()
        {
            Fraction a = new Fraction("1/2");
            string result = a.Subtract(new Fraction("4/2")).ToString();
            Assert.AreEqual(result, "-1_1/2");
        }

        [TestMethod]
        public void ProperFractionSubstractNegativeImproperFraction_ReturnsMIxedFraction()
        {
            Fraction a = new Fraction("1/2");
            string result = a.Subtract(new Fraction("-4/2")).ToString();
            Assert.AreEqual(result, "2_1/2");
        }
        #endregion

        #region multiply test
        [TestMethod]
        public void ProperFractionMultiplyProperFraction_ReturnsProperFraction()
        {
            Fraction a = new Fraction("1/2");
            string result = a.Multiply(new Fraction("2/5")).ToString();
            Assert.AreEqual(result, "1/5");
        }

        [TestMethod]
        public void MixedFractionMultiplyWholeNumber_ReturnsMixedFraction()
        {
            Fraction a = new Fraction("1_3/8");
            string result = a.Multiply(new Fraction("3")).ToString();
            Assert.AreEqual(result, "4_1/8");
        }

        [TestMethod]
        public void MixedFractionMultiplyMixedFraction_ReturnsMixedFraction()
        {
            Fraction a = new Fraction("1_1/2");
            string result = a.Multiply(new Fraction("2_1/5")).ToString();
            Assert.AreEqual(result, "3_3/10");
        }

        [TestMethod]
        public void MixedFractionMultiplyNegativeMixedFraction_ReturnsMixedFraction()
        {
            Fraction a = new Fraction("1_1/2");
            string result = a.Multiply(new Fraction("-2_1/5")).ToString();
            Assert.AreEqual(result, "-3_3/10");
        }

        [TestMethod]
        public void MixedFractionNegativeMultiplyNegativeMixedFraction_ReturnsMixedFraction()
        {
            Fraction a = new Fraction("-1_5/9");
            string result = a.Multiply(new Fraction("-2_1/7")).ToString();
            Assert.AreEqual(result, "3_1/3");
        }

        [TestMethod]
        public void WholeNumberMultiplyWholeNumber_ReturnsFraction()
        {
            Fraction a = new Fraction("4");
            string result = a.Multiply(new Fraction("3")).ToString();
            Assert.AreEqual(result, "12");
        }
        #endregion

        #region divide test
        [TestMethod]
        public void ProperFractionDivideProperFraction_ReturnsFraction()
        {
            Fraction a = new Fraction("1/2");
            string result = a.Divide(new Fraction("1/6")).ToString();
            Assert.AreEqual(result, "3");
        }

        [TestMethod]
        public void ProperFractionDivideWholeNumber_ReturnsFraction()
        {
            Fraction a = new Fraction("2/3");
            string result = a.Divide(new Fraction("5")).ToString();
            Assert.AreEqual(result, "2/15");
        }

        [TestMethod]
        public void WholeNumberDivideNegativeProperFraction_ReturnsFraction()
        {
            Fraction a = new Fraction("3");
            string result = a.Divide(new Fraction("-1/4")).ToString();
            Assert.AreEqual(result, "-12");
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void WholeNumberDivideByZero_ThrowsException()
        {
            Fraction a = new Fraction("3");
            string result = a.Divide(new Fraction("0")).ToString();
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void WholeNumberDivideByZeroDenominator_ThrowsException()
        {
            Fraction a = new Fraction("3");
            string result = a.Divide(new Fraction("3/0")).ToString();
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void WholeNumberDivideByZeroNumerator_ThrowsException()
        {
            Fraction a = new Fraction("3");
            string result = a.Divide(new Fraction("0/3")).ToString();
        }

        [TestMethod]
        public void FractionWithZeroNumeratorDivideWholeNumber_ReturnsZero()
        {
            Fraction a = new Fraction("0/3");
            string result = a.Divide(new Fraction("3")).ToString();
            Assert.AreEqual(result, "0");
        }
        #endregion
    }
}
