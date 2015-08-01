using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Core.Interceptor;
using System.ComponentModel;

namespace ReachMailDriver.Util.Aspects
{
    public class ConsoleAspect : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            Console.WriteLine(new String('-', Console.WindowWidth - 1));
            Console.WriteLine("Invoking: {0}", invocation.Method.Name);
            Console.WriteLine("With argument count: {0}", invocation.Arguments.Length);
            if (invocation.Arguments.Length > 0)
            {

                for (int i = 0; i < invocation.Arguments.Length; i++)
                {
                    writeObjectToConsole(invocation.Arguments[i]);
                }
            }

            invocation.Proceed();
            Console.WriteLine("Finished invocation {0}", invocation.Method.Name);

            if (invocation.ReturnValue != null)
            {
                Console.WriteLine("Return value");
                writeObjectToConsole(invocation.ReturnValue);
            }

            Console.WriteLine(new String('-', Console.WindowWidth - 1));
        }

        private void writeObjectToConsole(object theObject)
        {
            Console.WriteLine("\t{");

            if (theObject.GetType().IsValueType)
            {
                Console.WriteLine("\t\t{0}", theObject);
            }
            else
            {
                foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(theObject))
                {
                    string name = descriptor.Name;
                    object value = descriptor.GetValue(theObject);
                    Console.WriteLine("\t\t{0}={1}", name, value);
                }
            }
            Console.WriteLine("\t}");
        }
    }
}
