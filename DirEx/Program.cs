class Program
{
    static void Main(string[] args)
    {
        string path = @"C:\windows";


        Console.WriteLine("----------Without LINQ------------");
        ShowLargeFilesWithoutLinq(path);

        Console.WriteLine("----------LINQ Using **Methods** Syntax------------");
        ShowLargeFilesWithLinqMethods(path);

        Console.WriteLine("----------LINQ Using **Query** Syntax------------");
        ShowLargeFilesWithLinqQuery(path);

    }

    //LINQ Using Query Syntax
    private static void ShowLargeFilesWithLinqQuery(string path)
    {
        var query = from fi in new DirectoryInfo(path).GetFiles()
                    orderby fi.Length descending
                    select fi;

        //Take 5 elemnt using TakeWhile coz LINQ Take & TakeWhile operator is Not Supported in C# query syntax
        var result = query.TakeWhile((_, i) => i < 5);
        // or Using Take
        //var result = query.Take(5);

        foreach (var file in result)
        {
            Console.WriteLine($"{file.Name,-20} : {file.Length,10:N0}");
        }
    }


    //LINQ Using Methods Syntax
    private static void ShowLargeFilesWithLinqMethods(string path)
    {
        DirectoryInfo directory = new DirectoryInfo(path);
        FileInfo[] files = directory.GetFiles();
        Array.Sort(files, new FileInfoComparer());
        for (int i = 0; i < 5; i++)
        {
            FileInfo file = files[i];
            Console.WriteLine($"{file.Name,-20} : {file.Length,10:N0}");
        }
    }


    //Without Linq 
    private static void ShowLargeFilesWithoutLinq(string path)
    {
        DirectoryInfo directory = new DirectoryInfo(path);
        FileInfo[] files = directory.GetFiles();
        Array.Sort(files, new FileInfoComparer());
        for (int i = 0; i < 5; i++)
        {
            FileInfo file = files[i];
            Console.WriteLine($"{file.Name,-20} : {file.Length,10:N0}");
        }
    }
}

public class FileInfoComparer : IComparer<FileInfo>
{
    public int Compare(FileInfo x, FileInfo y)
    {
        return y.Length.CompareTo(x.Length);
    }
}
