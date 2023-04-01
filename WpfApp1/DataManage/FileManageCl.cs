using System;
using System.IO;


namespace Lab01Sydorova.DataManage
{
    internal static class FileManageCl
    {
        private static readonly string AppDataPath =
           Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        internal static readonly string AppFolderPath =
            Path.Combine(AppDataPath, "Lab01Sydorova");

        internal static readonly string StorageFilePath =
            Path.Combine(AppFolderPath, "DBPeople.txt");

        internal static bool CreateFolderAndCheckFileExistence(string filePath)
        {
            var file = new FileInfo(filePath);
            return file.CreateFolderAndCheckFileExistence();
        }

        internal static bool CreateFolderAndCheckFileExistence(this FileInfo file)
        {
            if (!file.Directory.Exists)
            {
                file.Directory.Create();
            }
            return file.Exists;
        }
    }
}
