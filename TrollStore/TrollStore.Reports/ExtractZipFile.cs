namespace TrollStore.Reports
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.IO.Compression;

    public class ExtractZipFile
    {
        private string zipPath;
        private string extractPath;

        public ExtractZipFile(string zipPath, string extractPath)
        {
            this.zipPath = zipPath;
            this.extractPath = extractPath;
        }

        public void ExtractFromZIP(string zipPath, string extractPath)
        {
            ZipFile.ExtractToDirectory(zipPath, extractPath);
        }
    }
}
