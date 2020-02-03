using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompileReplicateShedulingCsharp
{
    class Input
    {
        public int numberOfFiles;
        public int numberOfTargets { get; set; }
        public int numberOfServers { get; set; }
        public List<compiledFiles> compiledFiles = new List<compiledFiles>();
        public List<target> targets = new List<target>();

        /// <summary>
        /// Pass target path and it will do further processing to complete input structure
        /// </summary>
        /// <param name="filePath"></param>
        public Input(string filePath)
        {
            string[] allContents = File.ReadAllLines(filePath);
            numberOfFiles = Convert.ToInt32(allContents[0].Split(' ')[0]);
            numberOfTargets = Convert.ToInt32(allContents[0].Split(' ')[1]);
            numberOfServers= Convert.ToInt32(allContents[0].Split(' ')[2]);

            for(int i=1; i<=numberOfFiles*2;i=i+2)//Adding compiled files
            {
                string[] containLines=new string[2];
                containLines[0] = allContents[i];
                containLines[1]= allContents[i+1];
                compiledFiles.Add(new CompileReplicateShedulingCsharp.compiledFiles(containLines));
            }

            for(int i=numberOfFiles*2+1;i<=numberOfFiles * 2+numberOfTargets;i++)//Adding target files
            {
                targets.Add(new target(allContents[i]));
            }
        }
    }

    public class target
    {
        public string idOfTarget { get; set; }
        public int deadline { get; set; }
        public int goalPoints { get; set; }

        /// <summary>
        /// Pass a line that contain target
        /// </summary>
        /// <param name="targetContainLine"></param>
        public target(string targetContainLine)
        {
            idOfTarget = targetContainLine.Split(' ')[0];
            deadline = Convert.ToInt32(targetContainLine.Split(' ')[1]);
            goalPoints = Convert.ToInt32(targetContainLine.Split(' ')[2]);
        }

    }

    public class compiledFiles
    {
        public string id { get; set; }// ex. c0
        public int compilationTime { get; set; }
        public int replicationTime { get; set; }
        public int numberOfDependencies { get; set; }
        public List<string> dependencies = new List<string>(); //contain ids of the dependencies
        
        /// <summary>
        /// Pass two lines that contain definition of the compiled files
        /// </summary>
        /// <param name="linesContainDefinition"></param>
        public compiledFiles(string[] linesContainDefinition)
        {
            string line1 = linesContainDefinition[0];
            string line2 = linesContainDefinition[1];
            id = line1.Split(' ')[0];
            compilationTime = Convert.ToInt32(line1.Split(' ')[1]);
            replicationTime = Convert.ToInt32(line1.Split(' ')[2]);
            numberOfDependencies = Convert.ToInt32(line2.Split(' ')[0]);
            if(numberOfDependencies>0)
            {
                for(int i=1; i<=numberOfDependencies;i++)
                {
                    dependencies.Add(line2.Split(' ')[i]); //Adding id of the dependencies
                }
            }

        }
    }
}
