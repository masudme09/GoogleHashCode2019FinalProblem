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

            output(filePath, false);
            Console.WriteLine();
            listAllDependencies = null;
            output(filePath, true);


            //Input input = new Input(filePath);

            //long compilationSteps = 0;          
            //foreach (target target in input.targetsBasedOnDeadline)
            //{
            //    listAllDependencies = new List<compiledFiles>();
            //    listAllDependencies = listOfAllDependencies(target.targettedFile);
            //    listAllDependencies.Reverse();
            //    /*
            //     * Test code to check dependencies
            //        foreach(compiledFiles compiledFiles in listAllDependencies)
            //        {
            //            Console.WriteLine(target.targettedFile.id+":\n"+compiledFiles.id+"\n");
            //        }
            //    */
            //    //assign dependencies to server and complete the target
            //    //Selected server to process this target
            //    List<Server> sortedServers = input.servers.OrderBy(x =>x.serverProcessTime).ToList();
            //    Server selectedServer = sortedServers[0];

            //    foreach (compiledFiles dependent in listAllDependencies)
            //    {
            //        if (dependent.compiled == false) //need to compile
            //        {
            //            if(dependent==target.targettedFile)
            //            {
            //                selectedServer.processFile(dependent);
            //            }
            //            else if(dependent.compilationTime<dependent.replicationTime)
            //            {
            //                selectedServer.processFile(dependent);
            //            }else if(dependent.compilationTime > dependent.replicationTime && sortedServers.Count>0)
            //            {
            //                sortedServers[1].processFile(dependent);
            //            }else
            //            {
            //                selectedServer.processFile(dependent);
            //            }

            //            compilationSteps++;
            //        }
            //        else //need to replicate or compile again..If compile again need to increase compilation steps
            //        {
            //            if(!selectedServer.processedFiles.Contains(dependent) && dependent.compilationTime<dependent.replicationTime)
            //            {
            //                selectedServer.processFile(dependent);
            //                compilationSteps++;
            //            }
            //        }
            //    }               


            //}

            //Console.WriteLine(compilationSteps);
            //foreach(string line in Server.outputs)
            //{
            //    Console.WriteLine(line);
            //}

            //Calculate Points


            Console.ReadKey();

        }

        public static List<compiledFiles> listOfAllDependencies(compiledFiles target)
        {
            listAllDependencies.Add(target);
            foreach (compiledFiles dependencies in target.dependencies)
            {
                //listAllDependencies.Add(dependencies);
                listOfAllDependencies(dependencies);
            }
            //listAllDependencies.Reverse();
            return listAllDependencies;
        }

        public static void output(string filePath, bool basedOn)
        {
            Input input = new Input(filePath);
            List<target> targets=new List<target>();
            if(basedOn==true)
            {
                targets = input.targetsBasedOnDeadline;
            }else
            {
                targets = input.targetsBasedOnGoalPoints;
            }
            long compilationSteps = 0;
            foreach (target target in targets)
            {
                listAllDependencies = new List<compiledFiles>();
                listAllDependencies = listOfAllDependencies(target.targettedFile);
                listAllDependencies.Reverse();
                /*
                 * Test code to check dependencies
                    foreach(compiledFiles compiledFiles in listAllDependencies)
                    {
                        Console.WriteLine(target.targettedFile.id+":\n"+compiledFiles.id+"\n");
                    }
                */
                //assign dependencies to server and complete the target
                //Selected server to process this target
                List<Server> sortedServers = input.servers.OrderBy(x => x.serverProcessTime).ToList();
                Server selectedServer = sortedServers[0];

                foreach (compiledFiles dependent in listAllDependencies)
                {
                    if (dependent.compiled == false) //need to compile
                    {
                        if (dependent == target.targettedFile)
                        {
                            selectedServer.processFile(dependent);
                        }
                        else if (dependent.compilationTime < dependent.replicationTime)
                        {
                            selectedServer.processFile(dependent);
                        }
                        else if (dependent.compilationTime > dependent.replicationTime && sortedServers.Count > 0)
                        {
                            sortedServers[1].processFile(dependent);
                        }
                        else
                        {
                            selectedServer.processFile(dependent);
                        }

                        compilationSteps++;
                    }
                    else //need to replicate or compile again..If compile again need to increase compilation steps
                    {
                        if (!selectedServer.processedFiles.Contains(dependent) && dependent.compilationTime < dependent.replicationTime)
                        {
                            selectedServer.processFile(dependent);
                            compilationSteps++;
                        }
                    }
                }


            }

            Console.WriteLine(compilationSteps);
            foreach (string line in Server.outputs)
            {
                Console.WriteLine(line);
            }
        }
        
    }
}
