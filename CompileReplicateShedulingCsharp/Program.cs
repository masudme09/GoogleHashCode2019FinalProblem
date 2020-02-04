using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompileReplicateShedulingCsharp
{
    class Program
    {
        public static long clock = 0;
        public static List<compiledFiles> listAllDependencies;
        static void Main(string[] args)
        {
            string filePath=Console.ReadLine();
            Input input = new Input(filePath);

            //Algorithm based on deadline..all tasks are prioritize based on deadline
            //Sort targets smallest to largest based on deadlines
            //take the first target and get its dependencies.
            //Take first dependencies and assigned that on first server, take second one compare time=completion time of the task running at first server +compilation of this task
            //with time=compilation of this task + replication time..Do whatever smallest
            //If all dependencies of fist target finished write on console first target done with time stamp

            /*Target
             * -Get dependencies all
             * -Processing 
             * 
             * 
             * 
             * 
             */

                               
            
            
            long compilationSteps = 0;          
            foreach (target target in input.targets)
            {
                listAllDependencies = new List<compiledFiles>();
                listAllDependencies = listOfAllDependencies(target.targettedFile);

                /*
                 * Test code to check dependencies
                    foreach(compiledFiles compiledFiles in listAllDependencies)
                    {
                        Console.WriteLine(target.targettedFile.id+":\n"+compiledFiles.id+"\n");
                    }
                */
                //assign dependencies to server and complete the target
                foreach (Server server in input.servers)
                {
                    foreach (compiledFiles dependent in listAllDependencies)
                    {
                        if (dependent.compiled == false) //need to compile
                        {
                            server.processFile(dependent);
                            compilationSteps++;
                        }
                        else //need to replicate or compile again..If compile again need to increase compilation steps
                        {

                        }
                    }
                }        
                
            }
            Console.ReadKey();

        }

        public static List<compiledFiles> listOfAllDependencies(compiledFiles target)
        {
            //listAllDependencies.Add(target);
            foreach (compiledFiles dependencies in target.dependencies)
            {
                listAllDependencies.Add(dependencies);
                listOfAllDependencies(dependencies);
            }            

            return listAllDependencies;
        }

        
    }
}
