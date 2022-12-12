using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace AdventOfCode
{
    class Day7
    {
        private string inputPath;

        private struct Nodes
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

        private struct Directory
        {
            public string directoryName { get; }
            public string directoryParent { get; }
            public string directoryPath { get; }
            public int directorySize { get; set; }
            public List<Directory> childDirectories { get; set; }
            public List<Files> childFiles { get; set; }
            public List<Directory> subDirectories { get; set; }
            public List<Files> subFiles { get; set; }

            public Directory(string directoryParent, string directoryName, string directoryPath)
            {
                this.directoryParent = directoryParent;
                this.directoryName = directoryName;
                this.directoryPath = directoryPath;
                this.directorySize = 0;
                
                childDirectories = new List<Directory>();
                childFiles = new List<Files>();
                subDirectories = new List<Directory>();
                subFiles = new List<Files>();
            }
        }

        private struct Files
        {
            public string fileName { get; }
            public string fileParentPath { get; }
            public string filePath { get; set; }
            public int fileSize { get; }

            public Files(string fileName, string fileParentPath, string filePath, int fileSize)
            {
                this.fileName = fileName;
                this.fileParentPath = fileParentPath;
                this.filePath = filePath;
                this.fileSize = fileSize;
            }
        }

        private List<Nodes> nodeList = new List<Nodes>();

        private Directory rootDirectory;

        public Day7()
        {
            this.inputPath = $"{Core.directoryPath}/Day7-Input.txt";

            ReadInput();
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

                    // Update the parentPath if the current input line tells us to do so.
                    if (currentLine.Split()[0] == "$" && currentLine.Split()[1] == "cd" && currentLine.Split()[2] != "..")
                    {
                        if (currentLine.Split()[2] == "/") // Rename the input's root directory name ('/') to root
                        {
                            currentParentPath.Add("root");
                            currentParentName += $"root/";
                        }
                        else // Add the current directory name to the previous parentPath
                        {
                            currentParentPath.Add(currentLine.Split()[2]);
                            currentParentName += $"{currentLine.Split()[2]}/";
                        }
                    }
                    else if (currentLine.Split()[0] == "$" && currentLine.Split()[1] == "cd" && currentLine.Split()[2] == "..")
                    {
                        currentParentName = currentParentName.Substring(0, currentParentName.Length - Convert.ToInt32(currentParentPath[currentParentPath.Count - 1].Length + 1));

                        currentParentPath.RemoveAt(currentParentPath.Count - 1);
                    }

                    // If the input line is not a command, extract and create a node with it's name and size.
                    if (currentLine.Split()[0] != "$")
                    {
                        CreateNodeList(currentLine, currentParentName);
                    }
                }
            }

            CreateFileExplorer();
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
            List<Directory> directoriesList = new List<Directory>();
            List<Files> filesList = new List<Files>();

            rootDirectory = new Directory("", "root/", "root/");

            // Create root directory
            directoriesList.Add(rootDirectory);

            // Convert nodeList into directories and files
            for (int index = 0; index < nodeList.Count; index++)
            {
                if (nodeList[index].nodeType == "dir")
                {
                    // Create a directory from the nodeList then add it to the directoriesList. For example Directory("bnl/", "dhw/")
                    directoriesList.Add(new Directory(nodeList[index].parentPath, nodeList[index].nodeName + "/", nodeList[index].parentPath + nodeList[index].nodeName + "/"));

                    // Add the newly created directory to the root directory's subdirectory list
                    rootDirectory.subDirectories.Add(directoriesList[directoriesList.Count - 1]);
                }
                else if (nodeList[index].nodeType == "file")
                {
                    // Create a files from the nodeList then add it to the filesList. For example Files("dncdssn.hdr", "root/", 272080)
                    filesList.Add(new Files(nodeList[index].nodeName, nodeList[index].parentPath, nodeList[index].parentPath + nodeList[index].nodeName + "/", nodeList[index].nodeSize));

                    // Add the newly created files to the root directory's subfiles list
                    rootDirectory.subFiles.Add(filesList[filesList.Count - 1]);
                }
            }

            PopulateDirectories(directoriesList, filesList);
            GetSizes();

            OutputDirectories();
        }

        private void FreeSpaceForUpdate()
        {
            int currentFreeSpace = 70000000 - rootDirectory.directorySize;

            List<Directory> tempSubDirectories = rootDirectory.subDirectories;
            List<Directory> subDirectoriesOrdered = tempSubDirectories.OrderBy(x => x.directorySize).ToList();

            for (int i = 0; i < subDirectoriesOrdered.Count; i++)
            {
                if (currentFreeSpace + subDirectoriesOrdered[i].directorySize >= 30000000)
                {
                    Console.WriteLine($"The smallest directory's size to have enough free space is: {subDirectoriesOrdered[i].directorySize}.");
                    return;
                }
            }

        }

        public void WriteSmallDirectoriesSum()
        {
            int sum = 0;

            for (int index = 0; index < rootDirectory.subDirectories.Count; index++)
            {
                if (rootDirectory.subDirectories[index].directorySize <= 100000)
                {
                    sum += rootDirectory.subDirectories[index].directorySize;
                }
            }

            if (rootDirectory.directorySize <= 100000)
            {
                sum += rootDirectory.directorySize;
            }

            Console.WriteLine($"The sum of the files with less than the size of 100000: {sum}.");
            FreeSpaceForUpdate();
        }

        private void GetSizes()
        {
            int mostInDepth = 0;

            for (int directoryIndex = 0; directoryIndex < rootDirectory.subDirectories.Count; directoryIndex++)
            {
                if (rootDirectory.subDirectories[directoryIndex].directoryPath.Split('/').Length > mostInDepth)
                {
                    mostInDepth = rootDirectory.subDirectories[directoryIndex].directoryPath.Split('/').Length;
                }
            }

            // Loop through every subDirectory based on the directoryPath's depth
            for (int inDepthIndex = mostInDepth; inDepthIndex > 0; inDepthIndex--)
            {
                List<Directory> currentDirectoryWithDepthIndex = rootDirectory.subDirectories.Where(x => x.directoryPath.Split('/').Length == inDepthIndex).ToList();

                if (currentDirectoryWithDepthIndex != null)
                {
                    // Loop through every directory in the currentDirectoryWithDepthIndex list
                    for (int currentDirectoryIndex = 0; currentDirectoryIndex < currentDirectoryWithDepthIndex.Count; currentDirectoryIndex++)
                    {
                        Directory currentDirectory = currentDirectoryWithDepthIndex[currentDirectoryIndex];

                        // Loop through every child directory and add their size to the current directory's size
                        for (int childDirectoryIndex = 0; childDirectoryIndex < currentDirectory.childDirectories.Count; childDirectoryIndex++)
                        {
                            currentDirectory.directorySize += currentDirectory.childDirectories[childDirectoryIndex].directorySize;
                        }

                        // Loop through every child file and add their size to the current directory's size
                        for (int childFileIndex = 0; childFileIndex < currentDirectory.childFiles.Count; childFileIndex++)
                        {
                            currentDirectory.directorySize += currentDirectory.childFiles[childFileIndex].fileSize;
                        }

                        currentDirectoryWithDepthIndex[currentDirectoryIndex] = currentDirectory;
                    }

                    // Loop through every directory in the currentDirectoryWithDepthIndex list and update the directory in subDirectory base on the current directory
                    for (int currentDirectoryIndex = 0; currentDirectoryIndex < currentDirectoryWithDepthIndex.Count; currentDirectoryIndex++)
                    {
                        for (int subDirectoryIndex = 0; subDirectoryIndex < rootDirectory.subDirectories.Count; subDirectoryIndex++)
                        {
                            if (currentDirectoryWithDepthIndex[currentDirectoryIndex].directoryPath == rootDirectory.subDirectories[subDirectoryIndex].directoryPath)
                            {
                                rootDirectory.subDirectories[subDirectoryIndex] = currentDirectoryWithDepthIndex[currentDirectoryIndex];
                            }
                        }
                    }
                }

                UpdateDirectories();
            }

            // Loop through the root directory's childDirectories and add their size to the root directory's size
            for (int rootChildrenDirectoryIndex = 0; rootChildrenDirectoryIndex < rootDirectory.childDirectories.Count; rootChildrenDirectoryIndex++)
            {
                rootDirectory.directorySize += rootDirectory.childDirectories[rootChildrenDirectoryIndex].directorySize;
            }

            // Loop through the root directory's childFiles and add their size to the root directory's size
            for (int rootChildrenFilesIndex = 0; rootChildrenFilesIndex < rootDirectory.childFiles.Count; rootChildrenFilesIndex++)
            {
                rootDirectory.directorySize += rootDirectory.childFiles[rootChildrenFilesIndex].fileSize;
            }

            UpdateDirectories();
        }

        private void UpdateDirectories()
        {
            // Update root's children directories
            for (int childDirectoryIndex = 0; childDirectoryIndex < rootDirectory.childDirectories.Count; childDirectoryIndex++)
            {
                string childDirectoryPath = rootDirectory.childDirectories[childDirectoryIndex].directoryPath;

                rootDirectory.childDirectories[childDirectoryIndex] = rootDirectory.subDirectories.Where(x => x.directoryPath == childDirectoryPath).ToList()[0];
            }

            // Update children directories
            for (int directoryIndex = 0; directoryIndex < rootDirectory.subDirectories.Count; directoryIndex++)
            {
                Directory currentParentDirectory = rootDirectory.subDirectories[directoryIndex];

                for (int childDirectoryIndex = 0; childDirectoryIndex < currentParentDirectory.childDirectories.Count; childDirectoryIndex++)
                {
                    string childDirectoryPath = currentParentDirectory.childDirectories[childDirectoryIndex].directoryPath;

                    currentParentDirectory.childDirectories[childDirectoryIndex] = rootDirectory.subDirectories.Where(x => x.directoryPath == childDirectoryPath).ToList()[0];
                }
            }
        }

        private void PopulateDirectories(List<Directory> directoriesList, List<Files> filesList)
        {
            // Populate parent directories with it's children directories
            for (int directoriesListIndex = 0; directoriesListIndex < directoriesList.Count; directoriesListIndex++)
            {
                // Get the current directory's parentPath in the loop and find that directory in the directoriesList.
                List<Directory> childrenDirectoriesList = directoriesList.Where(x => x.directoryParent == directoriesList[directoriesListIndex].directoryParent + directoriesList[directoriesListIndex].directoryName).ToList();

                if (childrenDirectoriesList != null)
                {
                    for (int currentDirectoryIndex = 0; currentDirectoryIndex < childrenDirectoriesList.Count; currentDirectoryIndex++)
                    {
                        // Add the children directory to the parent directory's directoryList.
                        directoriesList[directoriesListIndex].childDirectories.Add(childrenDirectoriesList[currentDirectoryIndex]);
                    }
                }
            }

            // Populate directories with files
            for (int filesListIndex = 0; filesListIndex < filesList.Count; filesListIndex++)
            {
                // Get the current file's parentPath in the loop and find that directory in the directoriesList.
                List<Directory> filesParentDirectory = directoriesList.Where(x => x.directoryParent + x.directoryName == filesList[filesListIndex].fileParentPath).ToList();

                if (filesParentDirectory != null)
                {
                    filesParentDirectory[0].childFiles.Add(filesList[filesListIndex]);
                }
            }
        }

        private void OutputDirectories()
        {
            Directory currentDirectory = rootDirectory;

            List<string> directoryPaths = new List<string>();
            List<string> currentDirectoryTreePaths = new List<string>();

            currentDirectoryTreePaths.Add(rootDirectory.directoryPath);
            
            string output = "";
            output += AddMessageToOutput(rootDirectory);

            Start:

            if (currentDirectory.childDirectories.Count >= 1)
            {
                // Iterate over every child directory
                for (int currentChildIndex = 0; currentChildIndex < currentDirectory.childDirectories.Count; currentChildIndex++)
                {
                    // If child directory haven't been processed before go into that directory
                    if (directoryPaths.Contains(currentDirectory.childDirectories[currentChildIndex].directoryPath) == false)
                    {
                        directoryPaths.Add(currentDirectory.childDirectories[currentChildIndex].directoryPath);
                        currentDirectoryTreePaths.Add(currentDirectory.childDirectories[currentChildIndex].directoryPath);
                        currentDirectory = rootDirectory.subDirectories.Where(x => x.directoryPath == currentDirectoryTreePaths[currentDirectoryTreePaths.Count - 1]).ToList()[0];

                        output += AddMessageToOutput(currentDirectory);

                        goto Start;
                    }
                }

                goto GetBackToParentDirectory;
            }
            else if(currentDirectory.childDirectories.Count == 0)
            {
                goto GetBackToParentDirectory;
            }

            GetBackToParentDirectory:
                currentDirectoryTreePaths.RemoveAt(currentDirectoryTreePaths.Count - 1);

                if (currentDirectoryTreePaths[currentDirectoryTreePaths.Count - 1] == "root/")
                {
                    currentDirectory = rootDirectory;
                }
                else
                {
                    currentDirectory = rootDirectory.subDirectories.Where(x => x.directoryPath == currentDirectoryTreePaths[currentDirectoryTreePaths.Count - 1]).ToList()[0];
                }
                
                if (currentDirectory.directoryPath == rootDirectory.directoryPath && directoryPaths.Count == rootDirectory.subDirectories.Count)
                {
                    goto End;
                }
                else
                {
                    goto Start;
                }

            End:

            File.WriteAllText("FileExplorer.txt", output);
        }

        private string AddMessageToOutput(Directory currentDirectory)
        {
            string messageToReturn = "";
            int spaceCounter = (currentDirectory.directoryPath.Substring(0, currentDirectory.directoryPath.Length - 1).Split('/').Length - 1) * 2;

            messageToReturn += new String(' ', spaceCounter) + "- " + currentDirectory.directoryPath.Substring(0, currentDirectory.directoryPath.Length - 1).Split('/').Last() + 
                $" (directory, size: {currentDirectory.directorySize})" + "\n";

            for (int fileIndex = 0; fileIndex < currentDirectory.childFiles.Count; fileIndex++)
            {
                messageToReturn += new String(' ', spaceCounter + 2) + "- " + currentDirectory.childFiles[fileIndex].fileName + 
                    $" (file, size: {currentDirectory.childFiles[fileIndex].fileSize})" + "\n";
            }

            return messageToReturn;
        }
    }
}
