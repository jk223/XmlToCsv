using Gentrack_JagmeetPOC.Interfaces;
using Gentrack_JagmeetPOC.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Gentrack_JagmeetPOC
{
    public class ProcessingEngine: IProcessingEngine
    {
        private readonly IFileManager _fileManager;
        private readonly IValidator _validator;

        public ProcessingEngine() : this(new FileManager(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
            ApplicationConstants.FileRelativePath)), new Validator())
        {
        }

        public ProcessingEngine(IFileManager fileManager, IValidator validator)
        {
            _fileManager = fileManager ?? throw new ArgumentNullException(nameof(fileManager));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        public void Process()
        {
            try
            {
                var xDoc = _fileManager.GetFileContent();
                var csvLines = GetCSVINternalData(xDoc);
                csvLines = ParseWhiteSpacesLines(csvLines);
                _validator.Validate(csvLines);
                GenerateCSVs(csvLines);

            }
            catch (ValidationException e)
            {
                //todo some logging if required;
                throw;
            }
            catch (Exception e)
            {
                Console.WriteLine("Something went wrong");
                //todo can use proper library and print meaningful messages
                throw;
            }

        }

        private IList<string> ParseWhiteSpacesLines(IList<string> content)
        {
            return content.Where(s => !string.IsNullOrWhiteSpace(s)).Select(s => s.Trim()).ToList();
        }

        private void GenerateCSVs(IList<string> csvLines)
        {
            //All validations are good here, files can be generated
            //Each CSV file should be named from the second field in the "200" row
            //current logic will override the file in case names are same. This case needs to be handled while clarifying the requirements
            var headerRow = csvLines.First();
            var footerRow = csvLines.Last();
            var csvBuilder=new StringBuilder();
            var fileName = string.Empty;
            for(var i=1; i < csvLines.Count-1;i++)
            {
                if (csvLines[i].StartsWith("200") && string.IsNullOrEmpty(fileName))
                {
                    fileName = GetFileName(csvLines[i]);
                    csvBuilder.Append(headerRow).Append("\n");
                    csvBuilder.Append(csvLines[i]).Append("\n");
                }
                else if (csvLines[i].StartsWith("200"))
                {
                    //for next csv
                    csvBuilder.Append(footerRow).Append("\n");
                    CreateCSV(csvBuilder.ToString(), fileName);

                    fileName = GetFileName(csvLines[i]);
                    csvBuilder.Clear();
                    csvBuilder.Append(headerRow).Append("\n");
                    csvBuilder.Append(csvLines[i]).Append("\n");
                }
                else if(i==csvLines.Count-2)
                {
                    csvBuilder.Append(footerRow).Append("\n");
                    CreateCSV(csvBuilder.ToString(), fileName);
                }
                else
                {
                    csvBuilder.Append(csvLines[i]).Append("\n");
                }
            }
        }

        private void CreateCSV(string fileContent, string fileName)
        {
            
            _fileManager.CreateOrUpdateFile(fileContent,fileName);
        }

        private string GetFileName(string row)
        {
            return row.Split(',')[1].Trim() + ".txt";
        }

        /// <summary>
        /// todo we can create separate class to XML parsing to optimize and clean the code
        /// </summary>
        /// <param name="rootElement"></param>
        /// <returns></returns>
        private IList<string> GetCSVINternalData(XElement rootElement)
        {
            try
            {
                //this logic can be optimized later
                var searilizedCsvString = rootElement.Descendants("Transactions").Descendants("Transaction")
                    .Descendants("MeterDataNotification").Descendants("CSVIntervalData").FirstOrDefault()?.Value;
               return searilizedCsvString.Split(new[] { "\n" }, StringSplitOptions.None);
               //todo logic can be improved if the required changes for more validation on how the newline is constructed
            }
            catch (Exception e)
            {
                throw new ValidationException("XML parsing failed, xml is not in the expected format");
            }
        }

    }
}
