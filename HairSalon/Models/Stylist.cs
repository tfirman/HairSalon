using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace HairSalonDB.Models
{
    public class Stylist
    {
        private int _id;
        private string _name;
        private string _description;

        public Stylist(string name, string description, int iD = 0)
        {
            _id = iD;
            _name = name;
            _description = description;
        }

        public override bool Equals(System.Object otherStylist)
        {
            if (!(otherStylist is Stylist))
            {
                return false;
            }
            else
            {
                Stylist newStylist = (Stylist) otherStylist;
                bool idEquality = (this.GetId() == newStylist.GetId());
                bool nameEquality = (this.GetName() == newStylist.GetName());
                bool descriptionEquality = this.GetDescription() == newStylist.GetDescription();
                return (idEquality && descriptionEquality && nameEquality);
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

        public string GetDescription()
        {
            return _description;
        }

        public void SetDescription(string newDescription)
        {
            _description = newDescription;
        }
        public int GetId()
        {
            return _id;
        }

        public static List<Stylist> GetAll()
        {
            List<Stylist> allStylist = new List<Stylist> {};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM stylists;";
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
                  int stylistId = rdr.GetInt32(0);
                  string stylistName = rdr.GetString(1);
                  string stylistDescription = rdr.GetString(2);
                  Stylist newStylist = new Stylist(stylistName, stylistDescription, stylistId);
                  allStylist.Add(newStylist);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allStylist;
        }

        public static void DeleteAll()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM stylists;";
            cmd.ExecuteNonQuery();

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO stylists (name, description) VALUES (@Sty_name, @Sty_description);";

            MySqlParameter description = new MySqlParameter();
            description.ParameterName = "@Sty_description";
            description.Value = _description;
            cmd.Parameters.Add(description);

            MySqlParameter name = new MySqlParameter();
            name.ParameterName = "@Sty_name";
            name.Value = _name;
            cmd.Parameters.Add(name);

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
            cmd.CommandText = @"DELETE FROM stylists WHERE id = @StylistId;
              DELETE FROM clients WHERE stylistid = @StylistId;";

            MySqlParameter styd = new MySqlParameter();
            styd.ParameterName = "StylistId";
            styd.Value = _id;
            cmd.Parameters.Add(styd);
            cmd.ExecuteNonQuery();

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public void Edit(string newName, string newDescription)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"UPDATE stylists SET name = @nName, description = @newDescription WHERE id = @thisId;";

            MySqlParameter styd = new MySqlParameter();
            styd.ParameterName = "thisId";
            styd.Value = _id;
            cmd.Parameters.Add(styd);
            MySqlParameter name = new MySqlParameter();
            name.ParameterName = "@nName";
            name.Value = newName;
            cmd.Parameters.Add(name);
            MySqlParameter descrip = new MySqlParameter();
            descrip.ParameterName = "@newDescription";
            descrip.Value = newDescription;
            cmd.Parameters.Add(descrip);

            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static Stylist Find(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM stylists WHERE id = @thisId;";

            MySqlParameter thisId = new MySqlParameter();
            thisId.ParameterName = "@thisId";
            thisId.Value = id;
            cmd.Parameters.Add(thisId);

            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            int stylistId = 0;
            string stylistName = "";
            string stylistDescription = "";

            while (rdr.Read())
            {
                stylistId = rdr.GetInt32(0);
                stylistName = rdr.GetString(1);
                stylistDescription = rdr.GetString(2);
            }

            Stylist foundStylist= new Stylist(stylistName, stylistDescription, stylistId);

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }

           return foundStylist;
        }

        public List<Client> GetClients()
        {
            List<Client> allStylistClients = new List<Client> {};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM clients WHERE stylistid = @stylist_id;";

            MySqlParameter stylistId = new MySqlParameter();
            stylistId.ParameterName = "@stylist_id";
            stylistId.Value = this._id;
            cmd.Parameters.Add(stylistId);

            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
                int clientId = rdr.GetInt32(0);
                string clientName = rdr.GetString(1);
                int clientStylistId = rdr.GetInt32(2);
                Client newClient = new Client(clientName, clientStylistId, clientId);
                allStylistClients.Add(newClient);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allStylistClients;
        }

    }
}
