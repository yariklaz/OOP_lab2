using System.Collections.Generic;
using System.IO;
using System.Xml.Xsl;

namespace XmlStudentManager
{
    public class XmlContext
    {
        private ISearchStrategy _strategy;

        public void SetStrategy(ISearchStrategy strategy)
        {
            _strategy = strategy;
        }

        public List<string> PerformSearch(string filePath, Student criteria)
        {
            if (_strategy == null)
                throw new System.Exception("Стратегія аналізу не обрана!");

            if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
                throw new FileNotFoundException("Файл XML не знайдено.");

            return _strategy.Search(filePath, criteria);
        }

        public void TransformToHtml(string xmlPath, string xslPath, string outputPath)
        {
            if (!File.Exists(xmlPath) || !File.Exists(xslPath))
                throw new FileNotFoundException("XML або XSL файл не знайдено.");

            XslCompiledTransform xslt = new XslCompiledTransform();
            xslt.Load(xslPath);
            xslt.Transform(xmlPath, outputPath);
        }
    }
}