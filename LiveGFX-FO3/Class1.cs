using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace LiveGFX_FO3
{
    class db
    {
        string connectionstring = "Server=localhost;Database=GPL;Uid=root;Charset=utf8;";
        public List<string> getdata(string type, string data)
        {
            MySqlConnection connection = new MySqlConnection(connectionstring);
            MySqlCommand cmd;
            connection.Open();
            cmd = connection.CreateCommand();
            string comma = "SELECT " + type + " FROM " + data + ";";
            cmd.CommandText = comma;
            MySqlDataReader reader = cmd.ExecuteReader();
            List<string> info = new List<string>();
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    info.Add(reader[i].ToString());
                }

            }
            connection.Close();
            return info;

        }
        public List<string> getdata(string type, string data, string val, string name)
        {
            MySqlConnection connection = new MySqlConnection(connectionstring);
            MySqlCommand cmd;
            connection.Open();
            cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT " + type + " FROM " + data + " WHERE " + val + " = " + "\"" + name + "\"" + ";";
            MySqlDataReader reader = cmd.ExecuteReader();
            List<string> info = new List<string>();
            while (reader.Read())
            {

                for (int i = 0; i < reader.FieldCount; i++)
                {
                    info.Add(reader[i].ToString());
                }

            }
            connection.Close();
            return info;
        }
        public void insertdata(string data, string[] info, string[] type)
        {
            MySqlConnection connection = new MySqlConnection(connectionstring);
            MySqlCommand cmd;
            connection.Open();
            cmd = connection.CreateCommand();
            string comma = "INSERT INTO ";
            comma += "`" + data + "`" + "(";
            for (int i = 0; i < type.Length; i++)
            {

                if (i != type.Length - 1)
                {
                    comma += "`" + type[i] + "`" + ",";
                }
                else
                {
                    comma += "`" + type[i] + "`";
                }
            }
            comma += ") VALUES (";
            for (int i = 0; i < type.Length; i++)
            {
                if (i != type.Length - 1)
                {
                    comma += "?" + type[i] + ",";
                }
                else
                {
                    comma += "?" + type[i];
                }
            }
            comma += ")";
            cmd.CommandText = comma;
            for (int i = 0; i < type.Length; i++)
            {
                cmd.Parameters.AddWithValue("?" + type[i], info[i]);
            }
            cmd.ExecuteNonQuery();
            connection.Close();
        }
        public void updatedata(string data, string[] info, string[] type, string[] name)
        {
            MySqlConnection connection = new MySqlConnection(connectionstring);
            MySqlCommand cmd;
            connection.Open();
            cmd = connection.CreateCommand();
            string comma = "UPDATE ";
            comma += "`" + data + "`" + " SET ";
            for (int i = 0; i < type.Length; i++)
            {

                if (i != type.Length - 1)
                {
                    comma += "`" + type[i] + "`" + "=?" + type[i] + ",";
                }
                else
                {
                    comma += "`" + type[i] + "`" + "=?" + type[i];
                }
            }

            comma += " WHERE " + name[0] + "=" + "\"" + name[1] + "\"";
            cmd.CommandText = comma;
            for (int i = 0; i < type.Length; i++)
            {
                cmd.Parameters.AddWithValue("?" + type[i], info[i]);
            }
            cmd.ExecuteNonQuery();
            connection.Close();
        }
        public void deletedata(string data, string[] name)
        {
            MySqlConnection connection = new MySqlConnection(connectionstring);
            MySqlCommand cmd;
            connection.Open();
            cmd = connection.CreateCommand();
            cmd.CommandText = "DELETE FROM " + data + " WHERE " + name[0] + "=" + "\"" + name[1] + "\"";
            cmd.ExecuteNonQuery();
            connection.Close();
        }
        public int checkexist(string data, string[] name)
        {
            MySqlConnection connection = new MySqlConnection(connectionstring);
            MySqlCommand cmd;
            connection.Open();
            cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT COUNT(*) FROM " + data + " WHERE " + name[0] + "=" + "?val";
            cmd.Parameters.AddWithValue("?val", name[1]);
            int exist = Convert.ToInt32(cmd.ExecuteScalar());
            connection.Close();
            return exist;
        }
    }
}
