using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatchOS.Files.FilesFormat
{
    public static class FormatMGR
    {
        public static List<FORMAT> formats = new List<FORMAT>();

        public static void Init()
        {
            if (PMFAT.FileExists(PMFAT.Root + "REG/FS/FORMAT/REG.reg"))
            {
                formats.Add(new reg());
            }
            if (PMFAT.FileExists(PMFAT.Root + "REG/FS/FORMAT/HRP.reg"))
            {
                formats.Add(new hrp());
            }
        }

        public static void ExecuteFile(string path)
        {
            string[] typetemp = path.Split('.');
            string type =  typetemp[typetemp.Length].ToUpper();
                
            for (int i = 0; i < formats.Count; i++)
            {
                if (formats[i].FileType() == type)
                {
                    formats[i].Execute(path);
                    break;
                }
            }

        }
    }
}
