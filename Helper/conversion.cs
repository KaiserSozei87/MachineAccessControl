using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper
{
    public static class conversion
    {

        public static int convertStringToInt32 (string convert)
        {
            int result = 0;
            try
            {
                result = Convert.ToInt32(convert);
                return result;
            }
            catch (OverflowException)
            {
                Console.WriteLine("{0} is outside the range of the Int32 type.", convert);
            }
            catch (FormatException)
            {
                Console.WriteLine("The {0} value '{1}' is not in a recognizable format.", convert.GetType().Name, convert);
            }
            return result;
           
        }


    }
}
