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
                    var ret = PythonEngine.RunString($"RESULTS_VARIABLE = {input}", globals.Handle, IntPtr.Zero);
                    
                    if (ret != null)
                    {
                        Console.WriteLine(globals.GetItem("RESULTS_VARIABLE"));
                    }
                    else
                    {
                        var exception = new PythonException();
                        ret = PythonEngine.RunString(input, globals.Handle, IntPtr.Zero);
                        if (ret == null)
                        {
                            exception = new PythonException();
                            Console.WriteLine(exception.Message);
                        }
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
