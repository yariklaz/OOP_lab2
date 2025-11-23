using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace XmlStudentManager
{
    public class DomSearchStrategy : ISearchStrategy
    {
        public List<string> Search(string filePath, Student criteria)
        {
            var results = new List<string>();
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);

            string xpath = $"//Student[contains(@Name, '{criteria.Name}')]";


            if (!string.IsNullOrEmpty(criteria.Discipline))
            {
                xpath += $"[Session/Subject/@Title='{criteria.Discipline}']";
            }

            XmlNodeList nodes = doc.SelectNodes(xpath);
            foreach (XmlNode node in nodes)
            {
                string name = node.Attributes["Name"]?.Value;
                string faculty = node.ParentNode.ParentNode.Attributes["Name"]?.Value;
                results.Add($"[DOM] Студент: {name}, Факультет: {faculty}");
            }
            return results;
        }
    }

    public class SaxSearchStrategy : ISearchStrategy
    {
        public List<string> Search(string filePath, Student criteria)
        {
            var results = new List<string>();
            using (var reader = XmlReader.Create(filePath))
            {
                string currentFaculty = "";
                string currentDept = "";

                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        if (reader.Name == "Faculty")
                            currentFaculty = reader.GetAttribute("Name");

                        if (reader.Name == "Student")
                        {
                            string name = reader.GetAttribute("Name");
                            if (name.Contains(criteria.Name))
                            {
                                results.Add($"[SAX] Студент: {name}, Факультет: {currentFaculty}");
                            }
                        }
                    }
                }
            }
            return results;
        }
    }

    public class LinqSearchStrategy : ISearchStrategy
    {
        public List<string> Search(string filePath, Student criteria)
        {
            var results = new List<string>();
            var doc = XDocument.Load(filePath);

            var query = from student in doc.Descendants("Student")
                        where student.Attribute("Name").Value.Contains(criteria.Name)
                        select new
                        {
                            Name = student.Attribute("Name").Value,
                            Faculty = student.Parent.Parent.Attribute("Name").Value,
                            Subjects = student.Descendants("Subject")
                        };

            if (!string.IsNullOrEmpty(criteria.Discipline))
            {
                query = query.Where(s => s.Subjects.Any(sub => sub.Attribute("Title").Value == criteria.Discipline));
            }

            foreach (var item in query)
            {
                results.Add($"[LINQ] Студент: {item.Name}, Факультет: {item.Faculty}");
            }

            return results;
        }
    }
}
