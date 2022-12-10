using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace AdventOfCode
{
    class Day7
    {
        private string inputPath;

        struct Nodes
        {
            public string parentPath { get; }
            public string nodeName { get; }
            public string nodeType { get; }
            public int nodeSize { get; }

            public Nodes(string parentPath, string nodeName, string nodeType, int nodeSize)
            {
                this.parentPath = parentPath;
                this.nodeName = nodeName;
                this.nodeType = nodeType;
                this.nodeSize = nodeSize;
            }
        }

        struct Directory
        {
            public string directoryName { get; }
            public string directoryParent { get; }

            public List<Files> files { get; set; }
            public List<Directory> directories { get; set; }

            public Directory(string directoryParent, string directoryName)
            {
                this.directoryParent = directoryParent;
                this.directoryName = directoryName;

                files = new List<Files>();
                directories = new List<Directory>();
            }
        }

        struct Files
        {
            public string fileName { get; }
            public int fileSize { get; }

            public Files(string fileName, int fileSize)
            {
                this.fileName = fileName;
                this.fileSize = fileSize;
            }
        }

        List<Nodes> nodeList = new List<Nodes>();

        public Day7()
        {
            this.inputPath = $"{Core.directoryPath}/Day7-Input.txt";

            ReadInput();

            WriteThingy();

            CreateFileExplorer();
        }

        private void ReadInput()
        {
            List<string> currentParentPath = new List<string>();
            string currentParentName = "";

            using (StreamReader reader = new StreamReader(inputPath))
            {
                while (reader.EndOfStream == false)
                {
                    string currentLine = reader.ReadLine();

                    if (currentLine.Split()[0] == "$" && currentLine.Split()[1] == "cd" && currentLine.Split()[2] != "..")
                    {
                        if (currentLine.Split()[2] == "/")
                        {
                            currentParentPath.Add("root");
                            currentParentName += $"root/";
                        }
                        else
                        {
                            currentParentPath.Add(currentLine.Split()[2]);
                            currentParentName += $"{currentLine.Split()[2]}/";
                        }
                    }
                    else if (currentLine.Split()[0] == "$" && currentLine.Split()[1] == "cd" && currentLine.Split()[2] == "..")
                    {
                        currentParentName = currentParentName.Substring(0, currentParentName.Length - Convert.ToInt32(currentParentPath[currentParentPath.Count - 1].Count() + 1));

                        currentParentPath.Remove(currentParentPath[currentParentPath.Count - 1]);
                    }

                    if (currentLine.Split()[0] != "$")
                    {
                        CreateNodeList(currentLine, currentParentName);
                    }
                }
            }
        }

        private void CreateNodeList(string currentLine, string currentParent)
        {
            if (currentLine.Split()[0] == "dir")
            {
                nodeList.Add(new Nodes(currentParent, currentLine.Split()[1], "dir", 0));
            }
            else
            {
                nodeList.Add(new Nodes(currentParent, currentLine.Split()[1], "file", Convert.ToInt32(currentLine.Split()[0])));
            }
        }

        private void CreateFileExplorer()
        {
            List<Directory> directories = new List<Directory>();

            Directory root = new Directory("", "root/");

            directories.Add(root);

            for (int index = 0; index < nodeList.Count; index++)
            {
                if (nodeList[index].nodeType == "dir")
                {
                    // Create a directory with the parentDirectoryName of [a-Z]/ and add it to the directories list. For example Directory("bnl/", "dhw")
                    directories.Add(new Directory(nodeList[index].parentPath, nodeList[index].nodeName + "/"));

                    // Find the current directory's parent directory and add current directory to it's directory list
                    for (int i = 0; i < directories.Count; i++)
                    {
                        if (nodeList[index].parentPath == directories[i].directoryParent + directories[i].directoryName) // Find the directory where it's the parent
                        {
                            directories[i].directories.Add(new Directory(nodeList[index].parentPath, nodeList[index].nodeName + "/"));
                        }
                    }
                }
                else if (nodeList[index].nodeType == "file")
                {
                    for (int i = 0; i < directories.Count; i++)
                    {
                        if (nodeList[index].parentPath == directories[i].directoryParent + directories[i].directoryName)
                        {
                            directories[i].files.Add(new Files(nodeList[index].nodeName, nodeList[index].nodeSize));
                        }
                    }
                }
            }

            int indexy = 0;

            for (int i = 0; i < directories.Count; i++)
            {
                if (directories[i].directoryParent + directories[i].directoryName == "root/jvwtm/wbgcsw/bnl/mzfrdl/hwdnrqns/")
                {
                    indexy = i;
                }
            }

            for (int i = 0; i < directories[indexy].directories.Count; i++)
            {
                Console.WriteLine(directories[indexy].directories[i].directoryName);
            }
        }

        private void WriteThingy()
        {
            List<string> dirNames = new List<string>();

            string output = "";

            for (int i = 0; i < nodeList.Count; i++)
            {
                if (dirNames.Contains(nodeList[i].parentPath) == false)
                {
                    dirNames.Add(nodeList[i].parentPath);
                }
            }

            for (int i = 0; i < dirNames.Count; i++)
            {
                output += dirNames[i] + "\n";

                for (int index = 0; index < nodeList.Count; index++)
                {
                    if (dirNames[i] == nodeList[index].parentPath)
                    {
                        output += $"\t- {nodeList[index].nodeName} ({nodeList[index].nodeType} {nodeList[index].nodeSize})\n";
                    }
                }
            }

            File.WriteAllText("tempy.txt", output);
        }
    }
}
