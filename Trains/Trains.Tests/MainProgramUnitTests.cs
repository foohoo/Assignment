using System;
using System.IO;
using NUnit.Framework;

namespace Trains.Tests
{
    [TestFixture]
    public class MainProgramUnitTests
    {

        [Test]
        public void Should_Throw_Exception_If_Path_Argument_Is_Empty()
        {
            Assert.Throws<ArgumentException>(() => Program.Main(new[] { "" }));
        }

        [Test]
        public void Should_Throw_Exception_If_No_Argument_Provided()
        {
            Assert.Throws<ArgumentException>(() => Program.Main(new string[] { }));
        }

        [Test]
        public void Should_Throw_Exception_If_More_Than_One_Argument_Provided()
        {
            Assert.Throws<ArgumentException>(() => Program.Main(new[] { "Arg1", "Arg2" }));
        }

        [Test]
        public void Should_Throw_Exception_If_Input_Text_File_Not_Found_At_Path()
        {
            var filePath = AppDomain.CurrentDomain.BaseDirectory + "\\NotFound.txt";

            Assert.Throws<FileNotFoundException>(() => Program.Main(new[] { filePath }));
        }

        [Test]
        public void Should_Not_Throw_Exception_If_Input_Text_File_Found_At_Path()
        {
            var filePath = AppDomain.CurrentDomain.BaseDirectory + "\\TestInput.txt";

            Assert.DoesNotThrow(() => Program.Main(new[] { filePath }));
        }

    }
}
