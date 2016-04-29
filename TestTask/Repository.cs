using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using TestTask.Models;

namespace TestTask
{
    public class Repository  {
        private static Repository repo = new Repository();
        private static List<DataFolder> sqlDataFolder = new List<DataFolder>();
        private static List<DataFile> sqlDataFile = new List<DataFile>();
        string connectionString = ConfigurationManager.ConnectionStrings["DataFolder"].ConnectionString;

        public static Repository GetRepository()
        {
            return repo;
        }
        public List<DataFile> GetFilesByFolderId(int parentFolderId)
        {
            sqlDataFile = new List<DataFile>();
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.Text;
                    command.CommandText = "SELECT * FROM Files WHERE ParentFolderId = " + parentFolderId;
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DataFile file = new DataFile();
                            file.Id = (int)reader["Id"];
                            file.FileName = (string)reader["FileName"];
                            file.ParentFolderId = (int)reader["ParentFolderId"];                      
                            sqlDataFile.Add(file);
                        }
                    }
                }
            }
            return sqlDataFile;
        }
        public void AddFile(HttpPostedFile file, int folderId)
        {
            //GetAll();

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    String FileName = file.FileName;
                    byte[] fileData = null;
                    using (var binaryReader = new BinaryReader(file.InputStream))
                    {
                        fileData = binaryReader.ReadBytes(file.ContentLength);
                    }  

                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.Text;
                    command.CommandText = "INSERT INTO [dbo].[Files] ([ParentFolderId], [FileName], [DocumentFile]) VALUES (@ParentFolderId, @FileName, @DocumentFile);";

                    command.Parameters.AddWithValue("@ParentFolderId", folderId);
                    command.Parameters.Add("@FileName", System.Data.SqlDbType.NVarChar, 256).Value = FileName;
                    command.Parameters.Add("@DocumentFile", System.Data.SqlDbType.VarBinary).Value = fileData;

                    command.ExecuteNonQuery();
                }             
            }
        }
        public List<DataFolder> GetAll()
        {
            sqlDataFolder = new List<DataFolder>();
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.Text;
                    command.CommandText = "SELECT * FROM Folders";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DataFolder folder = new DataFolder();
                            folder.Id = (int)reader["Id"];
                            folder.Name = (string)reader["Name"];
                            folder.Level = (int)reader["Level"];
                            folder.ParentId = (int)reader["ParentId"];
                            sqlDataFolder.Add(folder);
                        }
                    }
                }
            }
            return sqlDataFolder;
        }
        public int GetFolderLevel(int folderId)
        {
            //sqlDataFolder = new List<DataFolder>();
            int level = 0;
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.Text;
                    command.CommandText = "SELECT [Level] FROM Folders WHERE Id = " + folderId;
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            level = (int)reader["Level"];
                        }
                    }
                }
            }
            return level;
        }
        public List<DataFolder> GetMainFolders()
        {
            sqlDataFolder = new List<DataFolder>();
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.Text;
                    command.CommandText = "SELECT * FROM Folders WHERE [Level] = 0";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DataFolder folder = new DataFolder();
                            folder.Id = (int)reader["Id"];
                            folder.Name = (string)reader["Name"];
                            folder.Level = (int)reader["Level"];
                            folder.ParentId = (int)reader["ParentId"];
                            sqlDataFolder.Add(folder);
                        }
                    }
                }
            }
            return sqlDataFolder;
        }
        public List<DataFolder> GetChild(int ParentId, int Level)
        {
            sqlDataFolder = new List<DataFolder>();
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.Text;
                    command.CommandText = "SELECT * FROM Folders WHERE ParentId = " + ParentId + "AND [Level] = " + Level;
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DataFolder folder = new DataFolder();
                            folder.Id = (int)reader["Id"];
                            folder.Name = (string)reader["Name"];
                            folder.Level = (int)reader["Level"];
                            folder.ParentId = (int)reader["ParentId"];
                            sqlDataFolder.Add(folder);
                        }
                    }
                }
            }
            return sqlDataFolder;
        }
        public void AddFolder(DataFolder folder)
        {
            GetAll();

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.Text;
                    command.CommandText = "INSERT INTO [dbo].[Folders] ([Name], [Level], [ParentId]) VALUES (@Name, @Level, @ParentId);";

                    //command.Parameters.AddWithValue("@Id", folder.Id);
                    command.Parameters.AddWithValue("@Name", folder.Name);
                    command.Parameters.AddWithValue("@Level", folder.Level);
                    command.Parameters.AddWithValue("@ParentId", folder.ParentId);

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}