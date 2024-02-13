using Cosmos.System.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatchOS.Files.FilesFormat
{
    public class hrp : FORMAT
    {
        internal override string FileType() => "HRF";

        internal override Bitmap icon() => null;

        internal override void Execute(string path, string x = null, string y = null)
        {
            //int[] data = PMFAT.ReadBytes(path);

            Bitmap bmp;
            //bmp.RawData = data;
        }
    }
}
