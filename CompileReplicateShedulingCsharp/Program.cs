using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompileReplicateShedulingCsharp
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath=Console.ReadLine();
            Input input = new Input(filePath);

            //Algorithm based on deadline..all tasks are prioritize based on deadline
            //Sort targets smallest to largest based on deadlines
            //Order list of dependencies based on new sorted target

        }
    }
}
