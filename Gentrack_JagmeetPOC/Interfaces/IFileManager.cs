using System.Xml.Linq;

namespace Gentrack_JagmeetPOC.Interfaces
{
    /// <summary>
    /// todo
    /// </summary>
    public interface IFileManager
    {
        /// <summary>
        /// todo
        /// </summary>
        /// <param name="fileContent"></param>
        void CreateOrUpdateFile(string fileContent, string fileName);

        /// <summary>
        /// todo
        /// </summary>
        /// <returns></returns>
        XElement GetFileContent();
    }
}
