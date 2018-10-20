namespace Fractions
{
    using System;
    using System.Collections.Generic;

    public class Evaluator
    {
        private string[] tokens;
        private Evaluator() { }

        public Evaluator(string[] tokens)
        {
            if (tokens == null || tokens.Length < 3) throw new ArgumentException("Invalid input", "tokens");
            this.tokens = tokens;
        }

        /// <summary>
        /// Evaluates a given expression(operations on fractions and/or whole numbers) 
        /// </summary>
        /// <returns></returns>
        public string Evaluate()
        {
            Stack<Fraction> values = new Stack<Fraction>();
            Stack<string> operators = new Stack<string>();

            for (int i = 0; i < tokens.Length; i++)
            {
                if (tokens[i].Equals("("))
                {
                    operators.Push(tokens[i]);
                }
                else if (tokens[i].Equals(")"))
                {
                    while(operators.Peek() != "(")
                    {
                        values.Push(ApplyOp(operators.Pop(), values.Pop(), values.Pop()));
                    }
                    operators.Pop();
                }
                else if (tokens[i].Equals("+") || tokens[i].Equals("-") || tokens[i].Equals("*") || tokens[i].Equals("/"))
                {
                    while (operators.Count != 0 && HasPrecedence(tokens[i], operators.Peek()))
                    {
                        values.Push(ApplyOp(operators.Pop(), values.Pop(), values.Pop()));
                    }
                    operators.Push(tokens[i]);
                }
                else
                {
                    values.Push(new Fraction(tokens[i]));
                }
            }

            while (operators.Count != 0)
            {
                values.Push(ApplyOp(operators.Pop(), values.Pop(), values.Pop()));
            }
            return values.Pop().ToString();
        }

        /// <summary>
        /// Determines the precedence of operators.
        /// </summary>
        /// <param name="operatorOne"></param>
        /// <param name="operatorTwo"></param>
        /// <returns></returns>
        public bool HasPrecedence(string operatorOne, string operatorTwo)
        {
            if (operatorTwo == "(" || operatorTwo == ")")
                return false;
            if ((operatorOne == "*" || operatorOne == "/") && (operatorTwo == "+" || operatorTwo == "-"))
                return false;
            else
                return true;
        }

        /// <summary>
        /// Evaluates operands according to the given operator.
        /// </summary>
        /// <param name="ops">operator</param>
        /// <param name="b">operand</param>
        /// <param name="a">operand</param>
        /// <returns></returns>
        public Fraction ApplyOp(string ops, Fraction b, Fraction a)
        {
            switch (ops)
            {
                case "*":
                     return a.Multiply(b);
                case "+":
                    return a.Add(b);
                case "-":
                    return a.Subtract(b);
                case "/":
                    return a.Divide(b);
                default: throw new ArgumentException($"Invalid operator.Operator: {ops}");
            }
        }
    }
}
