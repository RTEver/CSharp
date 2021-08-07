using System;
using IronPython.Hosting;

namespace IronPython_.NET_Framework_
{
    internal static class Program : Object
    {
        private static void Main(String[] args)
        {
            ExecuteSomeExpression();

            ExecuteSomeFile();

            ScriptScope_Example();

            ScriptScope_UseFunction();
        }

        private static void ExecuteSomeExpression()
        {
            var engine = Python.CreateEngine();

            var expression = @"print('Hello, World!')";

            engine.Execute(expression);
        }

        private static void ExecuteSomeFile()
        {
            var engine = Python.CreateEngine();
            
            engine.ExecuteFile(@"Python scripts/HelloWorld.py");
        }

        private static void ScriptScope_Example()
        {
            var engine = Python.CreateEngine();

            var scope = engine.CreateScope();

            scope.SetVariable("y", 15);

            engine.ExecuteFile(@"Python scripts/ScopeExample.py", scope);

            dynamic x = scope.GetVariable("x");

            dynamic y = scope.GetVariable("y");

            dynamic z = scope.GetVariable("z");

            Console.WriteLine("x = {0}", x);

            Console.WriteLine("y = {0}", y);

            Console.WriteLine("z = {0}", z);
        }

        private static void ScriptScope_UseFunction()
        {
            var engine = Python.CreateEngine();

            var scope = engine.CreateScope();

            engine.ExecuteFile(@"Python scripts/factorial.py", scope);

            dynamic function = scope.GetVariable("factorial");

            dynamic factorial = function(5);

            Console.WriteLine(function.GetType());

            Console.WriteLine("5! = {0}", factorial);
        }
    }
}