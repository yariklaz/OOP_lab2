using System.Collections.Generic;

namespace XmlStudentManager
{
    public interface ISearchStrategy
    {
        List<string> Search(string filePath, Student criteria);
    }
}