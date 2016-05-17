using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ImageGenerator.Tests
{
    [TestClass]
    public class ImageGeneratorTests
    {
        [TestMethod]
        public void test1()
        {

            Application.ResourceAssembly = Assembly.Load("ImageGenerator");
//            Assembly.GetManifestResourceNames().ToList();
            var a = new ImageGenerator();
        }
    }
}
