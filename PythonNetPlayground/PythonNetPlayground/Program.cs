using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Python.Runtime;

namespace PythonNetPlayground
{
    class Program
    {
        static void Main(string[] args)
        {
            PythonEngine.Initialize();

            var globals = CreateGlobals();
            while (true)
            { 
                Console.Write(">>> ");

                var input = Console.ReadLine();
                if (input == "exit()")
                {
                    return;
                }

                using (Py.GIL())
                {
                    var ret = PythonEngine.RunString(input, globals.Handle, IntPtr.Zero);
                    
                    if (ret != null)
                    {
                        //Print
                    }
                    else
                    {
                        var exp = new PythonException();
                    }
                }
            }
        }

        private static PyDict CreateGlobals()
        {
           var globals = new PyDict();
            globals.SetItem("random", PythonEngine.ImportModule("random"));
            return globals;
        }
    }
}
