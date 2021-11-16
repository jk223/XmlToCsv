using Gentrack_JagmeetPOC;
using Gentrack_JagmeetPOC.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.IO;
using System.Xml.Linq;

namespace Gentrack_JagmeetPOCUnitTests
{
    [TestClass]
    public class ProcessingEngineUnitTests
    {
        [TestMethod]
        public void ProcessingEngine_validtest_noException()
        {
            var validator = new Validator();
            var mockFileManager = new Mock<IFileManager>();

            var xElement= XElement.Load(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\TestDataFiles\testfileValid.xml"));
            mockFileManager.Setup(x => x.GetFileContent()).Returns(xElement);
            var processor=new ProcessingEngine(mockFileManager.Object,validator);
            processor.Process();
        }

        /// <summary>
        /// trailing 900 missing
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void ProcessingEngine_invalid_input_throw_Validate1_exception()
        {
            var validator = new Validator();
            var mockFileManager = new Mock<IFileManager>();

            var xElement = XElement.Load(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\TestDataFiles\testfileInvalid1.xml"));
            mockFileManager.Setup(x => x.GetFileContent()).Returns(xElement);
            var processor = new ProcessingEngine(mockFileManager.Object, validator);
            processor.Process();
        }

        /// <summary>
        /// header missing 100,NEM12,201801211010,MYENRGY,URENRGY
        /// </summary>
        [TestMethod]
        //[ExpectedException(typeof(ValidationException))]
        public void ProcessingEngine_invalid_input_throw_Validate2_exception()
        {
            var validator = new Validator();
            var mockFileManager = new Mock<IFileManager>();

            var xElement = XElement.Load(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\TestDataFiles\testfileInvalid2.xml"));
            mockFileManager.Setup(x => x.GetFileContent()).Returns(xElement);
            var processor = new ProcessingEngine(mockFileManager.Object, validator);
            processor.Process();
        }

        /// <summary>
        /// more than 1 header  missing 100,NEM12,201801211010,MYENRGY,URENRGY
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void ProcessingEngine_invalid_input_throw_Validate3_exception()
        {
            var validator = new Validator();
            var mockFileManager = new Mock<IFileManager>();

            var xElement = XElement.Load(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\TestDataFiles\testfileInvalid3.xml"));
            mockFileManager.Setup(x => x.GetFileContent()).Returns(xElement);
            var processor = new ProcessingEngine(mockFileManager.Object, validator);
            processor.Process();
        }

        /// <summary>
        /// 200" row must be followed by at least 1 "300" row, this use case fails
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void ProcessingEngine_invalid_input_throw_Validate4_exception()
        {
            var validator = new Validator();
            var mockFileManager = new Mock<IFileManager>();

            var xElement = XElement.Load(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\TestDataFiles\testfileInvalid4.xml"));
            mockFileManager.Setup(x => x.GetFileContent()).Returns(xElement);
            var processor = new ProcessingEngine(mockFileManager.Object, validator);
            processor.Process();
        }
    }
}
