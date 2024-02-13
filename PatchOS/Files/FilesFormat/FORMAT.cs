using Cosmos.System.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatchOS.Files.FilesFormat
{
    public abstract class FORMAT
    {
        internal FORMAT()
        {

        }
        internal abstract string FileType();
        internal abstract Bitmap icon();
        internal abstract void Execute(string path, string x = null, string y = null);

    }
}