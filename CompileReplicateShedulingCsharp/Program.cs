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
            //take the first target and get its dependencies.
            //Take first dependencies and assigned that on first server, take second one compare time=completion time of the task running at first server +compilation of this task
            //with time=compilation of this task + replication time..Do whatever smallest
            //If all dependencies of fist target finished write on console first target done with time stamp

            long clock = 0;
            long compilationSteps = 0;
            List<target> targets = input.targets;
            targets = targets.OrderBy(x => x.deadline).ToList();
            foreach(target target in targets)
            {
                string targetId = target.idOfTarget;
                //Search with targetId on the files and get dependencies
                compiledFiles compiledFileOnlist = input.compiledFiles.Find(x => x.id == targetId);

                //Now get dependencies, inner dependent come first
                List<compiledFiles> dependencies = new List<compiledFiles>();
                foreach(string de in compiledFileOnlist.dependencies)
                {
                    dependencies.Add(input.compiledFiles.Find(x => x.id == de));
                }
                
                //Now if this dependencies have further dependencies than add them also on the list and make unique list

            }


        }

        
    }
}
