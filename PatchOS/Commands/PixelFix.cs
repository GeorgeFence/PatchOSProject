using Cosmos.Core.IOGroup;
using Cosmos.HAL;
using Cosmos.System.Graphics;
using PatchOS.Files;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using PIT = Cosmos.HAL.PIT;

namespace PatchOS.Commands
{
    class PixelFix : Command
    {

        public PixelFix()
        {
            this.Name = "pixelfix";
            this.Help = "start fixing pixels";

        }

        public override void Execute(string line,String[] args)
        {
#pragma warning disable CS0168 // Variable is declared but never used
            try
            {
                Console.Write(new object());
            }
            catch(Exception ex)
            {

            }
#pragma warning restore CS0168 // Variable is declared but never used
        }
    }
}
