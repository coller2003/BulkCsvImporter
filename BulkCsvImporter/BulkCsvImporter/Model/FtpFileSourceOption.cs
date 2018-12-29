using BulkCsvImporter.Abstract;
using BulkCsvImporter.Constant;
using BulkCsvImporter.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BulkCsvImporter.Model
{
    public class FtpFileSourceOption : FileSourceOption
    {
        public FtpFileSourceOption(string host, string username, string password) : base()
        {
            FileSourceType = FileSourceType.Ftp;
            Host = host;
            UserName = username;
            Password = password;
        }

        public string Host
        {
            get
            {
                return Options[FileSourceOptionKey.KEY_FILESOURCETYPE_FTP_HOST];
            }
            set
            {
                Options[FileSourceOptionKey.KEY_FILESOURCETYPE_FTP_HOST] = value;
            }
        }

        public string Password
        {
            get
            {
                return Options[FileSourceOptionKey.KEY_FILESOURCETYPE_FTP_PASSWORD];
            }
            set
            {
                Options[FileSourceOptionKey.KEY_FILESOURCETYPE_FTP_PASSWORD] = value;
            }
        }

        public string UserName
        {
            get
            {
                return Options[FileSourceOptionKey.KEY_FILESOURCETYPE_FTP_USERNAME];
            }
            set
            {
                Options[FileSourceOptionKey.KEY_FILESOURCETYPE_FTP_USERNAME] = value;
            }
        }
    }
}
