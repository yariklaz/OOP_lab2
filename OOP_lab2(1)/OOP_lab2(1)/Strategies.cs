using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Xsl;

namespace XmlStudentManager
{
    // === 1. Критерії пошуку ===
    public class StudentSearchCriteria
    {
        public string Name { get; set; } = "";
        public string Faculty { get; set; } = "";
        public string Discipline { get; set; } = "";
    }

    // === 2. Інтерфейс Стратегії ===
    public interface ISearchStrategy
    {
        List<string> Search(string filePath, StudentSearchCriteria criteria);
    }

    // === 3. Контекст ===
    public class XmlContext
    {
        private ISearchStrategy _strategy;

        public void SetStrategy(ISearchStrategy strategy)
        {
            _strategy = strategy;
        }

        public List<string> PerformSearch(string filePath, StudentSearchCriteria criteria)
        {
            if (_strategy == null) return new List<string> { "Стратегія не обрана!" };
            if (!File.Exists(filePath)) return new List<string> { "Файл не знайдено!" };

            return _strategy.Search(filePath, criteria);
        }

        // === НОВЕ: Метод для отримання унікальних значень для випадаючих списків ===
        public List<string> GetUniqueValues(string filePath, string tagName, string attributeName)
        {
            if (!File.Exists(filePath)) return new List<string>();

            // Використовуємо LINQ to XML для швидкого витягування унікальних даних
            var doc = XDocument.Load(filePath);
            return doc.Descendants(tagName)
                      .Select(x => x.Attribute(attributeName)?.Value)
                      .Where(val => !string.IsNullOrEmpty(val)) // Прибираємо пусті
                      .Distinct() // Прибираємо дублікати
                      .OrderBy(val => val) // Сортуємо за алфавітом
                      .ToList();
        }

        public void TransformToHtml(string xmlPath, string xslPath, string outputPath)
        {
            if (!File.Exists(xmlPath) || !File.Exists(xslPath))
                throw new FileNotFoundException("Файли не знайдено.");

            XslCompiledTransform xslt = new XslCompiledTransform();
            xslt.Load(xslPath);
            xslt.Transform(xmlPath, outputPath);
        }
    }

    // === 4. Стратегії (DOM, SAX, LINQ) ===

    // DOM Strategy
    public class DomSearchStrategy : ISearchStrategy
    {
        public List<string> Search(string filePath, StudentSearchCriteria criteria)
        {
            var results = new List<string>();
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);

            StringBuilder xpathBuilder = new StringBuilder("//Student");
            List<string> conditions = new List<string>();

            if (!string.IsNullOrEmpty(criteria.Name))
                conditions.Add($"contains(@Name, '{criteria.Name}')");

            if (!string.IsNullOrEmpty(criteria.Faculty))
                conditions.Add($"ancestor::Faculty[contains(@Name, '{criteria.Faculty}')]");

            if (!string.IsNullOrEmpty(criteria.Discipline))
                conditions.Add($"Session/Subject[@Title='{criteria.Discipline}']");

            if (conditions.Count > 0)
            {
                xpathBuilder.Append("[");
                xpathBuilder.Append(string.Join(" and ", conditions));
                xpathBuilder.Append("]");
            }

            XmlNodeList nodes = doc.SelectNodes(xpathBuilder.ToString());
            if (nodes != null)
            {
                foreach (XmlNode node in nodes) results.Add(FormatResult(node));
            }
            return results;
        }

        private string FormatResult(XmlNode node)
        {
            string info = "[DOM] ";
            if (node.Attributes != null)
            {
                foreach (XmlAttribute attr in node.Attributes) info += $"{attr.Name}: {attr.Value}; ";
            }
            var faculty = node.SelectSingleNode("ancestor::Faculty")?.Attributes["Name"]?.Value;
            if (faculty != null) info += $"| Ф-т: {faculty}";
            return info;
        }
    }

    // SAX Strategy
    public class SaxSearchStrategy : ISearchStrategy
    {
        public List<string> Search(string filePath, StudentSearchCriteria criteria)
        {
            var results = new List<string>();
            using (var reader = XmlReader.Create(filePath))
            {
                string currentFaculty = "";
                Dictionary<string, string> studentAttrs = new Dictionary<string, string>();
                bool matchFound = false;

                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        if (reader.Name == "Faculty") currentFaculty = reader.GetAttribute("Name") ?? "";
                        if (reader.Name == "Student")
                        {
                            studentAttrs.Clear();
                            matchFound = true;
                            if (reader.HasAttributes)
                            {
                                while (reader.MoveToNextAttribute()) studentAttrs[reader.Name] = reader.Value;
                                reader.MoveToElement();
                            }

                            string name = studentAttrs.ContainsKey("Name") ? studentAttrs["Name"] : "";

                            if (!string.IsNullOrEmpty(criteria.Name) && !name.Contains(criteria.Name)) matchFound = false;
                            if (!string.IsNullOrEmpty(criteria.Faculty) && !currentFaculty.Contains(criteria.Faculty)) matchFound = false;

                            if (matchFound && string.IsNullOrEmpty(criteria.Discipline))
                                results.Add(FormatSaxResult(studentAttrs, currentFaculty));
                        }
                        if (matchFound && reader.Name == "Subject" && !string.IsNullOrEmpty(criteria.Discipline))
                        {
                            if (reader.GetAttribute("Title") == criteria.Discipline)
                            {
                                results.Add(FormatSaxResult(studentAttrs, currentFaculty));
                                matchFound = false;
                            }
                        }
                    }
                }
            }
            return results;
        }

        private string FormatSaxResult(Dictionary<string, string> attrs, string faculty)
        {
            string info = "[SAX] ";
            foreach (var kvp in attrs) info += $"{kvp.Key}: {kvp.Value}; ";
            info += $"| Ф-т: {faculty}";
            return info;
        }
    }

    // LINQ Strategy
    public class LinqSearchStrategy : ISearchStrategy
    {
        public List<string> Search(string filePath, StudentSearchCriteria criteria)
        {
            var results = new List<string>();
            var doc = XDocument.Load(filePath);
            var query = doc.Descendants("Student").AsQueryable();

            if (!string.IsNullOrEmpty(criteria.Name))
                query = query.Where(s => s.Attribute("Name") != null && s.Attribute("Name").Value.Contains(criteria.Name));

            if (!string.IsNullOrEmpty(criteria.Faculty))
                query = query.Where(s => s.Ancestors("Faculty").Any(f => f.Attribute("Name") != null && f.Attribute("Name").Value.Contains(criteria.Faculty)));

            if (!string.IsNullOrEmpty(criteria.Discipline))
                query = query.Where(s => s.Descendants("Subject").Any(sub => sub.Attribute("Title") != null && sub.Attribute("Title").Value == criteria.Discipline));

            foreach (var s in query)
            {
                string info = "[LINQ] ";
                foreach (var attr in s.Attributes()) info += $"{attr.Name}: {attr.Value}; ";
                var fac = s.Ancestors("Faculty").FirstOrDefault()?.Attribute("Name")?.Value;
                if (fac != null) info += $"| Ф-т: {fac}";
                results.Add(info);
            }
            return results;
        }
    }
}