using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditCardValidator.libs {
    class Validator {
        public static bool ValidateNumber(string cardNumber) {
            if (cardNumber.Length < 15) { return false; }
            int hi = cardNumber.Length - 1;
            int sum = 0;
            int doubleFlag = 0;
            for (int i = hi; i >= 0; i--) {
                int currDigit = (int)Char.GetNumericValue(cardNumber[i]);
                
                if (doubleFlag % 2 == 1) {
                    currDigit *= 2;
                }
                
                sum += (int)(currDigit % 10) + (int)(currDigit / 10);
                doubleFlag++;
            }

            return (sum % 10 == 0);
        }

        public static void CheckNumber(string cardNumber) {
            if (cardNumber.Length==0) { throw new ContainNonDigitException(); }
            int len = cardNumber.Length;
            for (int i = 0; i < len; i++) {
                if (!Char.IsDigit(cardNumber[i])) {
                    throw new ContainNonDigitException();
                }
            }
        }

    }

    [Serializable]
    public class ContainNonDigitException : Exception {
        public ContainNonDigitException() { }
        public ContainNonDigitException(string message) : base(message) { }
        public ContainNonDigitException(string message, Exception inner) : base(message, inner) { }
        protected ContainNonDigitException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
