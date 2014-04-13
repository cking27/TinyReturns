﻿using System;
using System.Linq;

namespace Dimensional.TinyReturns.Core
{
    public class FinancialMath
    {
        public FinancialMathResult PerformGeometricLinking(decimal[] values)
        {
            var returnValue = values[0];
            var calculation = values[0].ToString();

            for (int i = 1; i < values.Length; ++i)
            {
                returnValue = (1 + returnValue) * (1 + values[i]) - 1;
                calculation = string.Format("((1 + {0}) * (1 + {1}) - 1)", calculation, values[i]);
            }

            var result = new FinancialMathResult(
                returnValue,
                calculation + " = " + returnValue);

            return result;
        }

        public FinancialMathResult AnnualizeByMonth(
            decimal value,
            int monthCount)
        {
            const int monthsPerYear = 12;

            var valueAsDouble = Convert.ToDouble(value);

            var baseVal = 1 + valueAsDouble;
            var baseValCalc = string.Format("(1 + {0})", value);

            var exponentVal = monthsPerYear * 1.0 / monthCount;
            var exponentValCalc = string.Format("({0} * 1 / {1})", monthsPerYear, monthCount);

            var pow = Math.Pow(baseVal, exponentVal) - 1;
            var calculation = string.Format("({0} ^ {1}) - 1", baseValCalc, exponentValCalc);

            var result = new FinancialMathResult();
            result.Value = Convert.ToDecimal(pow);
            result.Calculation = calculation + " = " + Convert.ToDecimal(pow);

            return result;
        }

        public FinancialMathResult CalculateStandardDeviation(
            decimal[] values)
        {
            var standardDeviationCalculation = "";

            var mean = values.Average();
            var meanCaluclation = GetMeanCalculation(values);

            var sumOfSquaredDiffs = 0m;

            foreach (var ret in values)
            {
                var diff = ret - mean;
                var diffCalculation = string.Format("{0} - {1}", ret, meanCaluclation);

                var squarOfDiff = diff * diff;
                var squarOfDiffCalc = string.Format("({0})^2", diffCalculation);

                sumOfSquaredDiffs += squarOfDiff;

                if (string.IsNullOrEmpty(standardDeviationCalculation))
                    standardDeviationCalculation = squarOfDiffCalc;
                else
                    standardDeviationCalculation = string.Format("{0} + {1}", standardDeviationCalculation, squarOfDiffCalc);
            }

            var divideSumOfSqares = sumOfSquaredDiffs / (values.Length - 1);
            var standardDeviationValue = SqareRoot(divideSumOfSqares);

            var numeratorCalculation = string.Format("({0} - 1)", values.Length);
            standardDeviationCalculation = string.Format("Sqrt({0} / {1})", standardDeviationCalculation, numeratorCalculation);

            var result = new FinancialMathResult();

            result.Value = standardDeviationValue;
            result.Calculation = standardDeviationCalculation;

            return result;
        }

        private decimal SqareRoot(decimal d)
        {
            return Convert.ToDecimal(Math.Sqrt(Convert.ToDouble(d)));
        }

        private string GetMeanCalculation(
            decimal[] values)
        {
            var addAllValuesCalculation = values
                .Select(r => r.ToString())
                .Aggregate((f, s) => string.Format("({0} + {1})", f, s));

            var meanCaluclation = string.Format("({0} / {1})", addAllValuesCalculation, values.Length);

            return meanCaluclation;
        }
    }

    public class FinancialMathResult
    {
        public FinancialMathResult()
        {
        }

        public FinancialMathResult(decimal value, string calculation)
        {
            Value = value;
            Calculation = calculation;
        }

        public decimal Value { get; set; }
        public string Calculation { get; set; }
    }

}