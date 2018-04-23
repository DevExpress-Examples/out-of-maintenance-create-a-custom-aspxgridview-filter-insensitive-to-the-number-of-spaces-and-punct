using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Data.Filtering; //ICustomFunctionOperator

// How to register a custom filter function and use it in ASPxGridView
// see: http://www.devexpress.com/Support/Center/Example/Details/E4099

namespace SearchString
{
    public static class EasySearchStatic
    {
        public static string Name { get { return "EasySearch"; } }
    }

    /// <summary>
    ///
    /// </summary>
    public class EasySearchString : ICustomFunctionOperator
    {
        /// <summary>
        /// Property: Returns the name of this custom filter.
        /// </summary>
        public string Name
        {
            get { return EasySearchStatic.Name; }
        }

        /// <summary>
        /// Method: Returns the data type of the filter function which is bool.
        /// </summary>
        /// <param name="operands"></param>
        /// <returns></returns>
        public Type ResultType(params Type[] operands)
        {
            return typeof(bool);
        }

        /// <summary>
        /// Return the canonical value of a string, 
        /// i.e., without spaces, case, punctuation etc.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private string Canonical(string str_in)
        {
            string str_out = "";
            for (int i = 0; i < str_in.Length; i++)
            {
                char ch = str_in[i];
                if (ch != ' ' && ch != '.')               //remove spaces, punctuation
                    str_out = str_out + char.ToLower(ch); //remove case
            }
            return str_out;
        }

        /// <summary>
        /// Method: Returns TRUE if operands[1] satisfies some condition as specified in operands[0].
        /// This method is called for every (visible) row in the datagridview.
        /// </summary>
        /// <param name="operands"></param>
        /// <returns></returns>
        public object Evaluate(params object[] operands)
        {
            string SearchString = Canonical(operands[0].ToString());            
            string CellString   = Canonical(operands[1].ToString());
            int IndexNr = CellString.IndexOf(SearchString);            
            return IndexNr >= 0;
        }

    }//end class
}//end ns
