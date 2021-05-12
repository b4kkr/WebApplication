using System;
using System.Text;

namespace WebApplication.Constants
{
    public class PasswordGenerator
    {

        private static PasswordGenerator _passwordGenerator;
        public static PasswordGenerator Instance
        {
            get
            {
                if (_passwordGenerator == null)
                {
                    _passwordGenerator = new PasswordGenerator();
                }
                return _passwordGenerator;
            }
        }

        private PasswordGenerator(){}

        public string Generate(int stringLength)
        {
            StringBuilder sb = new StringBuilder();
            int numGuidsToConcat = (((stringLength - 1) / 32) + 1);
            for(int i = 1; i <= numGuidsToConcat; i++)
            {
                sb.Append(Guid.NewGuid().ToString("N"));
            }

            return sb.ToString(0, stringLength);
            
        }
    }
}