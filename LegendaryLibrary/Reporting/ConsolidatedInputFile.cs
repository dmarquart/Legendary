using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace LegendaryLibrary
{
    public class ConsolidatedInputFile
    {

        private ConsolidatedInputFile() { }
        public ConsolidatedInputFile(string fullPath)
        {
            FullPath = fullPath;
            FileName = System.IO.Path.GetFileNameWithoutExtension(fullPath);
            FileExtension = System.IO.Path.GetExtension(fullPath);
        }

        public ConsolidatedInputFile(FileInfo fileInfo)
        {
            FullPath = fileInfo.FullName;
            FileName = fileInfo.Name;
            FileExtension = fileInfo.Extension;
        }

        public override string ToString()
        {
            return FileName;
        }

        public string FullPath { get; }
        public string FileName { get; }
        public string FileExtension { get; }

        static public SortedList<string, ConsolidatedInputFile> GetInputFileListFromDate(string directoryPath, DateTime reportDate)
        {
            var inputFileList = new SortedList<string, ConsolidatedInputFile>();
            var directoryInfo = new DirectoryInfo(directoryPath);
            var allFiles = directoryInfo.GetFiles($"*.xl*");
            foreach (var file in allFiles)
            {
                var newConsolidateInputFile = new ConsolidatedInputFile(file);
                inputFileList.Add(file.Name, newConsolidateInputFile);
            }
            return inputFileList;
        }
    }
}
