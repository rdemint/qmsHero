using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace QDoc.Core
{
    public class QFileChangeInspector
    {
        public QFileChangeInspector()
        {
        }

        public bool FileHashHasChanged(byte[] fileHashValueComparision, FileInfo fileInfo)
        {

            byte[] newHash = GetFileHash(fileInfo);
            return newHash.SequenceEqual(fileHashValueComparision);
        }

        public byte[] GetFileHash(FileInfo fileInfo)
        {
            byte[] newHash;
            using (SHA1 sha1 = SHA1.Create())
            {
                using (FileStream fs = new FileStream(fileInfo.FullName, FileMode.Open, FileAccess.Read))
                {
                    newHash = sha1.ComputeHash(fs);
                }

            }
           return newHash;
        }


    }
}
