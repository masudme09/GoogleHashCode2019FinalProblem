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
        public List<Server> servers = new List<Server>();

       
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

            //Instantiate servers
            for(int i=0;i<numberOfServers;i++)
            {
                servers.Add(new Server(i));
            }

            for(int i=1; i<=numberOfFiles*2;i=i+2)//Adding compiled files
            {
                string[] containLines=new string[2];
                containLines[0] = allContents[i];
                containLines[1]= allContents[i+1];
                compiledFiles.Add(new CompileReplicateShedulingCsharp.compiledFiles(containLines, compiledFiles));
            }

            for(int i=numberOfFiles*2+1;i<=numberOfFiles * 2+numberOfTargets;i++)//Adding target files
            {
                targets.Add(new target(allContents[i], compiledFiles));
            }

            targets = targets.OrderBy(x => x.deadline).ToList();// to sort based on deadline



        }
    }

    public class target
    {
        public string idOfTarget { get; set; }
        public int deadline { get; set; }
        public int goalPoints { get; set; }
        public compiledFiles targettedFile;

        /// <summary>
        /// Pass a line that contain target
        /// </summary>
        /// <param name="targetContainLine"></param>
        public target(string targetContainLine, List<compiledFiles> compiledFiles)
        {
            idOfTarget = targetContainLine.Split(' ')[0];
            deadline = Convert.ToInt32(targetContainLine.Split(' ')[1]);
            goalPoints = Convert.ToInt32(targetContainLine.Split(' ')[2]);
            targettedFile= compiledFiles.Find(x => x.id == idOfTarget);
        }

    }

    public class compiledFiles
    {
        public string id { get; set; }// ex. c0
        public int compilationTime { get; set; }
        public int replicationTime { get; set; }
        public int numberOfDependencies { get; set; }
        public List<compiledFiles> dependencies = new List<compiledFiles>(); //contain ids of the dependencies
        public bool compiled;

        /// <summary>
        /// Pass two lines that contain definition of the compiled files
        /// </summary>
        /// <param name="linesContainDefinition"></param>
        public compiledFiles(string[] linesContainDefinition,List<compiledFiles> currentList)
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
                    dependencies.Add(currentList.Find(x=>x.id==line2.Split(' ')[i])); //Adding id of the dependencies
                }
            }

        }
    }

    public class Server
    {
        public int serverId;
        //List<compiledFiles> onQue = new List<compiledFiles>();
        public List<compiledFiles> processedFiles = new List<compiledFiles>();
        public long serverProcessTime = 0;
        public static List<string> outputs = new List<string>();

        public Server(int id)
        {
            serverId = id;
        }

        public void processFile(compiledFiles file)
        {
            processedFiles.Add(file);
            file.compiled = true;
            serverProcessTime = serverProcessTime + file.compilationTime;
            outputs.Add(file.id + " " + serverId);
        }
    }
}
