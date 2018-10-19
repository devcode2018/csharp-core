namespace Fractions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Fraction
    {
        public int Numerator { get; set; }
        public int Denominator { get; set; }

        public Fraction() { }

        /// <summary>
        /// constructor: extracts the numerator and denominator from a given operand.
        /// </summary>
        /// <param name="operand"></param>
        public Fraction(string operand)
        {
            if (string.IsNullOrWhiteSpace(operand)) throw new ArgumentException($"Invalid operand.Operand:{operand}");
            operand.Trim();

            HashSet<char> chars = new HashSet<char>(operand.ToArray());
            if (chars.Contains('(') ||
                chars.Contains(')') ||
                chars.Contains('*') ||
                chars.Contains('+'))
            {
                throw new ArgumentException($"Invalid operand.Ensure operands and operators are seperated by one or more spaces. Operand:{operand}");
            }
           

            int underScoreIndex = operand.IndexOf('_');
            int slashIndex = operand.IndexOf('/');
            if (underScoreIndex != -1)
            {
                //Its mixed fraction
                int wholePart = int.Parse(operand.Substring(0, underScoreIndex));
                this.Denominator = int.Parse(operand.Substring(slashIndex + 1));

                string s = operand.Substring(underScoreIndex + 1, slashIndex - underScoreIndex - 1);
                int numerator = int.Parse(s);
                this.Numerator = Math.Abs(wholePart) * this.Denominator + numerator;
                if (wholePart < 0) this.Numerator *= -1;
            }
            else if (slashIndex != -1)
            {
                //Its proper or improper fraction
                this.Numerator = int.Parse(operand.Substring(0, slashIndex));
                this.Denominator = int.Parse(operand.Substring(slashIndex + 1));
            }
            else
            {
                //Its a whole number
                this.Numerator = int.Parse(operand);
                if(this.Numerator == 0) throw new ArgumentException($"Invalid operand.Operand:{operand}");
                this.Denominator = 1;
            }
        }

        /// <summary>
        /// Adds two fractions
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public Fraction Add(Fraction b)
        {
            if (b == null) throw new ArgumentNullException("One of the operand or fraction cannot be null.","b");
            if(Denominator == 0 || b.Denominator == 0) throw new ArgumentException($"One of the denominators is invalid.Denominator:{Denominator} or {b.Denominator}");

            int commonDenominator = LCD(Denominator, b.Denominator);

            Fraction commonA = Convert(commonDenominator);
            Fraction commonB = b.Convert(commonDenominator);

            Fraction sum = new Fraction
            {
                Numerator = commonA.Numerator + commonB.Numerator,
                Denominator = commonDenominator
            };
            return sum.Reduce();
        }

        /// <summary>
        /// Substracts two fractions
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public Fraction Subtract(Fraction b)
        {
            if (b == null) throw new ArgumentNullException("One of the operand or fraction cannot be null.","b");
            if (Denominator == 0 || b.Denominator == 0) throw new ArgumentException($"One of the denominators is invalid.Denominator:{Denominator} or {b.Denominator}");

            int commonDenominator = LCD(Denominator, b.Denominator);

            Fraction commonA = Convert(commonDenominator);
            Fraction commonB = b.Convert(commonDenominator);

            Fraction difference = new Fraction
            {
                Numerator = commonA.Numerator - commonB.Numerator,
                Denominator = commonDenominator
            };
            return difference.Reduce();
        }

        /// <summary>
        /// Multiplies two fractions
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public Fraction Multiply(Fraction b)
        {
            if (b == null) throw new ArgumentNullException("One of the operand or fraction cannot be null.","b");
            if (Denominator == 0 || b.Denominator == 0) throw new ArgumentException($"One of the denominators is invalid.Denominator:{Denominator} or {b.Denominator}");

            Fraction product = new Fraction
            {
                Numerator = this.Numerator * b.Numerator,
                Denominator = this.Denominator * b.Denominator
            };
            return product.Reduce();
        }


        /// <summary>
        /// Divides two fractions
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public Fraction Divide(Fraction b)
        {
            if (b == null) throw new ArgumentNullException("One of the operand or fraction cannot be null.");
            if (this.Denominator == 0 || b.Denominator == 0) throw new ArgumentException($"One of the denominators is invalid.Denominator:{this.Denominator} or {b.Denominator}");
            
            int sign = (this.Numerator < 0 ? -1 : 1) * (b.Numerator < 0 ? -1 : 1);
            Fraction result = new Fraction
            {
                Numerator = this.Numerator * b.Denominator * sign,
                Denominator = Math.Abs(this.Denominator * b.Numerator)
            };
            return result.Reduce();
        }
                
        public override string ToString()
        {
            if(this.Denominator == 0) throw new ArgumentException($"Invalid denominator.Denominator:{this.Denominator}");

            int wholePart = this.Numerator / this.Denominator;
            int remainder = this.Numerator % this.Denominator;
            if (Math.Abs(wholePart) > 0 && Math.Abs(remainder) > 0)
            {
                return wholePart + "_" + (wholePart < 0 ? remainder * -1 : remainder) + "/" + this.Denominator;
            }
            return this.Numerator + "/" + this.Denominator;
        }

        /// <summary>
        /// Gets lowest common denominator used to add and substract fractions.
        /// </summary>
        /// <param name="leftOperandDenominator"></param>
        /// <param name="rightOperandDenominator"></param>
        /// <returns></returns>
        private int LCD(int leftOperandDenominator, int rightOperandDenominator)
        {
            int factor = leftOperandDenominator;
            while (leftOperandDenominator % rightOperandDenominator != 0)
            {
                leftOperandDenominator += factor;
            }
            return leftOperandDenominator;
        }

        /// <summary>
        /// Converts a fraction to an equivalent one based on a lowest common denominator.
        /// </summary>
        /// <param name="commonDenominator"></param>
        /// <returns></returns>
        private Fraction Convert(int commonDenominator)
        {
            int factor = commonDenominator / Denominator;
            return new Fraction {
               Numerator = this.Numerator * factor,
               Denominator = commonDenominator
            };
        }


        /// <summary>
        /// Gets the greatest common denominator used to reduce fractions.
        /// </summary>
        /// <param name="leftOperandDenominator"></param>
        /// <param name="rightOperandDenominator"></param>
        /// <returns></returns>
        private int GCD(int leftOperandDenominator, int rightOperandDenominator)
        {
            int factor = rightOperandDenominator;
            while (rightOperandDenominator != 0)
            {
                factor = rightOperandDenominator;
                rightOperandDenominator = leftOperandDenominator % rightOperandDenominator;
                leftOperandDenominator = factor;
            }
            return leftOperandDenominator;
        }

        /// <summary>
        /// Converts the "this" fraction to an equivalent one based on a greatest common denominator.
        /// </summary>
        /// <returns></returns>
        private Fraction Reduce()
        {
            int numerator = Math.Abs(this.Numerator);
            int denominator = Math.Abs(this.Denominator);

            int commonDenominator = 0;
            if (numerator > denominator)
            {
                commonDenominator = GCD(numerator, denominator);
            }
            else if (numerator < denominator)
            {
                commonDenominator = GCD(denominator, numerator);
            }
            else
            {
                commonDenominator = numerator;
            }

            return new Fraction
            {
                Numerator = this.Numerator / commonDenominator,
                Denominator = this.Denominator / commonDenominator
            };
        }
    }
}
