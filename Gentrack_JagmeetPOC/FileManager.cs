using Gentrack_JagmeetPOC.Interfaces;
using System;
using System.IO;
using System.Threading;
using System.Xml.Linq;

namespace Gentrack_JagmeetPOC
{
    /// <summary>
    /// Initializes a new instance of the <see cref="IFileManager"/>
    /// </summary>
    public class FileManager : IFileManager, IDisposable
    {
        // To detect redundant calls
        private bool _disposedValue;
        private readonly string _fullFilePath;
        private static readonly ReaderWriterLock _fileLocker = new ReaderWriterLock();
        private const int LockTimeoutInMillisecond = 10000;

        public FileManager(string fullFilePath)
        {
            if (string.IsNullOrWhiteSpace(fullFilePath)) throw new ArgumentException(nameof(fullFilePath));
            _fullFilePath = fullFilePath;
        }

        /// <inheritdoc />
        public void CreateOrUpdateFile(string fileContent, string fileName)
        {
            CheckFileExists();
            try
            {
                _fileLocker.AcquireWriterLock(LockTimeoutInMillisecond);
                File.WriteAllText(fileName, fileContent); //file will be created in bin directory. Path optimizations can be done
            }
            catch (Exception ex)
            {
                //log some information based upon logging business requirements
                throw; //propagate this error
            }
            finally
            {
                _fileLocker.ReleaseWriterLock();
            }
        }

        /// <inheritdoc />
        public XElement GetFileContent()
        {
            CheckFileExists();
            try
            {
                _fileLocker.AcquireReaderLock(LockTimeoutInMillisecond);
                return XElement.Load(_fullFilePath);
            }
            catch (Exception ex)
            {
                //log some information based upon logging business requirements
                throw; //propagate this error
            }
            finally
            {
                _fileLocker.ReleaseReaderLock();
            }
        }

        private void CheckFileExists()
        {
            if (!File.Exists(_fullFilePath))
                throw new FileNotFoundException();
        }

        /// <inheritdoc />
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        //todo to use later
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                _disposedValue = true;
            }
        }

        ~FileManager() => Dispose(false);
    }
}
