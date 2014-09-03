namespace TrollStore.Reports
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.IO.Compression;
    using System.IO;

    public class ExtractZipFile
    {
        private string zipPath;
        private string extractPath;

        public ExtractZipFile(string zipPath, string extractPath)
        {
            this.zipPath = zipPath;
            this.extractPath = extractPath;
        }

        public void ExtractFromZIP()
        {
            try
            {
                ZipFile.ExtractToDirectory(this.zipPath, this.extractPath);
            }
            catch (IOException)
            {
                return;
            }
        }
    }
}