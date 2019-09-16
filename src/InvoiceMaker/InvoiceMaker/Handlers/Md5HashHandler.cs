using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace InvoiceMaker.Handlers
{
    public class Md5HashHandler : IHttpHandler
    {
        public bool IsReusable
        {
            get
            {
                return true;
            }
        }

        public void ProcessRequest(HttpContext context)
        {
            string fullPath = context.Request.FilePath;
            string fileExtension = Path.GetExtension(fullPath);

            // Remove the leading slash (i.e. "/") and
            // the file extension.
            string valueToHash = fullPath
                .Substring(1).Replace(fileExtension, string.Empty);

            // Hash the value.
            using (MD5 md5Hash = MD5.Create())
            {
                switch (fileExtension)
                {
                    case ".hash":
                        WriteTextResponse(context, valueToHash, md5Hash);
                        break;
                    case ".binhash":
                        WriteBinaryResponse(context, valueToHash, md5Hash);
                        break;
                    default:
                        throw new Exception("Unexpected file extension: " + fileExtension);
                }
            }
        }

        private static void WriteTextResponse(HttpContext context, string valueToHash, MD5 md5Hash)
        {
            string hash = GetMd5HashText(md5Hash, valueToHash);
            context.Response.ContentType = "text/plain";
            context.Response.Write(hash);
        }

        private static void WriteBinaryResponse(HttpContext context, string valueToHash, MD5 md5Hash)
        {
            byte[] hash = GetMd5HashBinary(md5Hash, valueToHash);
            context.Response.ContentType = "application/octet-stream";
            context.Response.AppendHeader("Content-Disposition", "attachment; filename=BinaryHash.bin");
            context.Response.BinaryWrite(hash);
        }

        // The following methods were taken from this MSDN article: 
        // https://docs.microsoft.com/en-us/dotnet/api/system.security.cryptography.md5

        static string GetMd5HashText(MD5 md5Hash, string input)
        {
            byte[] data = GetMd5HashBinary(md5Hash, input);

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        static byte[] GetMd5HashBinary(MD5 md5Hash, string input)
        {
            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            return data;
        }
    }
}