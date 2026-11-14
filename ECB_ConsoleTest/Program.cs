using System.Runtime.InteropServices;
using System.Text;
using ConsoleTables;
using EverythingConnetionBridge;

namespace ECB_ConsoleTest
{
    internal struct Config
    {
        public Config() { }
        public bool Jumpable { get; set; } = false;
        public bool Path { get; set; } = false;
        public bool Size { get; set; } = false;
        public bool DateModified { get; set; } = false;

        public void Set(char arg)
        {
            switch (arg)
            {
                case 'j':
                    {
                        Jumpable = true;
                        break;
                    }
                case 'p':
                    {
                        Path = true;
                        break;
                    }
                case 's':
                    {
                        Size = true;
                        break;
                    }
                case 'd':
                    {
                        DateModified = true;
                        break;
                    }
            }
        }
    };


    internal class Program
    {
        static void Main(string[] args)
        {
            var config = new Config();

            string? prompt = null;
            if (args.Length >= 1)
            {
                char[] arguments = args.Where(s => s.StartsWith('-')).SelectMany(s => s.Replace("-", "")).Distinct().ToArray();
                foreach (var arg in arguments)
                    config.Set(arg);
                prompt = args.First(s => !s.StartsWith('-'));
            }
            else
            {
                Console.Write("ECB prompt >> ");
                prompt = Console.ReadLine();
            }
            if (prompt is null)
                return;
            EverythingSearch(prompt, config);
            return;
        }

        internal static void EverythingSearch(string prompt, Config config)
        {
            ECB.SetSearch(prompt);
            ECB.SetRequestFlags((uint)(EVERYTHING.REQUEST.FILE_NAME |
                                       EVERYTHING.REQUEST.PATH |
                                       EVERYTHING.REQUEST.DATE_MODIFIED |
                                       EVERYTHING.REQUEST.SIZE));
            ECB.SetSort((uint)EVERYTHING.SORT.DATE_MODIFIED_ASCENDING);
            ECB.Query(true);

            uint total = ECB.GetNumResults();

            if (!config.Jumpable)
            {
                ConsoleTable table = new("File Name");

                List<String> newColumns = [];
                if (config.Path) newColumns.Add("Path");
                if (config.Size) newColumns.Add("Size");
                if (config.DateModified) newColumns.Add("Date Modified");
                if (newColumns.Count() != 0) table.AddColumn(newColumns);

                for (uint i = 0; i < total; i++)
                {
                    var rowData = new List<object> { Marshal.PtrToStringUni(ECB.GetResultFileName(i)) ?? String.Empty };

                    if (config.Path)
                    {
                        StringBuilder sb = new(260);
                        ECB.GetResultFullPathName(i, sb, 260);
                        if (config.Path) rowData.Add(sb.ToString());
                    }

                    if (config.Size)
                    {
                        ECB.GetResultSize(i, out long size);
                        bool isFile = ECB.IsFileResult(i);
                        rowData.Add(isFile ? size.ToString() : String.Empty);
                    }

                    if (config.DateModified)
                    {
                        ECB.GetResultDateModified(i, out long date_modified);
                        rowData.Add(DateTime.FromFileTime(date_modified).ToString("yyyy/MM/dd HH:mm"));
                    }

                    table.AddRow(rowData.ToArray());
                }

                if (total > 0)
                    table.Write(Format.Minimal);
            }
            else
            {
                for (uint i = 0; i < total; i++)
                {
                    bool isFile = ECB.IsFileResult(i);
                    if (!isFile)
                        continue;
                    StringBuilder sb = new(260);
                    ECB.GetResultFullPathName(i, sb, 260);
                    sb.Append(":0:");
                    Console.WriteLine(sb.ToString());
                }
            }
            Console.WriteLine($"\nTotal results: {total}");
            return;
        }
    }
}
