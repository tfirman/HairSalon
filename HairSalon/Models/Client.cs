using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace HairSalonDB.Models
{
    public class Client
    {
        private string _name;
        private int _id;
        private int _stylistId;

        public Client(string name, int stylistid, int id = 0)
        {
            _name = name;
            _id = id;
            _stylistId = stylistid;
        }
        public override bool Equals(System.Object otherClient)
        {
            if (!(otherClient is Client))
            {
                return false;
            }
            else
            {
                Client newClient = (Client) otherClient;
                bool idEquality = (this.GetId() == newClient.GetId());
                bool nameEquality = (this.GetName() == newClient.GetName());
                return (idEquality && nameEquality);;
            }
        }
        public override int GetHashCode()
        {
            return this.GetId().GetHashCode();
        }
        public string GetName()
        {
            return _name;
        }
        public int GetId()
        {
            return _id;
        }
        public int GetStylistId()
        {
            return _stylistId;
        }

        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO clients (name, stylistid) VALUES (@name, @stylistid);";

            MySqlParameter name = new MySqlParameter();
            name.ParameterName = "@name";
            name.Value = _name;
            cmd.Parameters.Add(name);
            MySqlParameter stylistid = new MySqlParameter();
            stylistid.ParameterName = "@stylistid";
            stylistid.Value = _stylistId;
            cmd.Parameters.Add(stylistid);

            cmd.ExecuteNonQuery();
            _id = (int) cmd.LastInsertedId;
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public void Delete()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM clients WHERE id = @ClientId;";

            MySqlParameter clid = new MySqlParameter();
            clid.ParameterName = "ClientId";
            clid.Value = _id;
            cmd.Parameters.Add(clid);
            cmd.ExecuteNonQuery();

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public void Edit(string newName, int newStylist)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"UPDATE clients SET name = @nName, stylistid = @newStylist WHERE id = @thisId;";

            MySqlParameter clid = new MySqlParameter();
            clid.ParameterName = "thisId";
            clid.Value = _id;
            cmd.Parameters.Add(clid);
            MySqlParameter name = new MySqlParameter();
            name.ParameterName = "@nName";
            name.Value = newName;
            cmd.Parameters.Add(name);
            MySqlParameter stylst = new MySqlParameter();
            stylst.ParameterName = "@newStylist";
            stylst.Value = newStylist;
            cmd.Parameters.Add(stylst);

            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static Client Find(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM clients WHERE id = @thisId;";

            MySqlParameter thisId = new MySqlParameter();
            thisId.ParameterName = "@thisId";
            thisId.Value = id;
            cmd.Parameters.Add(thisId);

            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            int clientId = 0;
            string clientName = "";
            int stylistId = 0;

            while (rdr.Read())
            {
                clientId = rdr.GetInt32(0);
                clientName = rdr.GetString(1);
                stylistId = rdr.GetInt32(2);
            }
            Client foundClient = new Client(clientName, stylistId, clientId);

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }

           return foundClient;
        }

        public static List<Client> GetAll()
        {
            List<Client> allClients = new List<Client> {};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM clients;";
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
              int ClientId = rdr.GetInt32(0);
              string ClientName = rdr.GetString(1);
              int StylistId = rdr.GetInt32(2);
              Client newClient = new Client(ClientName, StylistId, ClientId);
              allClients.Add(newClient);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allClients;
        }

        public static void DeleteAll()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM clients;";
            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
    }
}
