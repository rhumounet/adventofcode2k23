using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp;

public static class FileHelper
{
    public static string GetContent(string path)
    {
        using var reader = new StreamReader(path);
        return reader.ReadToEnd();
    }
}
