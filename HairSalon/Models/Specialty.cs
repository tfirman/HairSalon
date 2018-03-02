using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace HairSalonDB.Models
{
    public class Specialty
    {
        private string _name;
        private int _id;

        public Specialty(string name, int id = 0)
        {
            _name = name;
            _id = id;
        }

        public override bool Equals(System.Object otherSpecialty)
        {
            if (!(otherSpecialty is Specialty))
            {
                return false;
            }
            else
            {
                Specialty newSpecialty = (Specialty) otherSpecialty;
                bool idEquality = (this.GetId() == newSpecialty.GetId());
                bool nameEquality = (this.GetName() == newSpecialty.GetName());
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

        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO specialties (name) VALUES (@specName);";

            MySqlParameter specName = new MySqlParameter();
            specName.ParameterName = "@specName";
            specName.Value = _name;
            cmd.Parameters.Add(specName);

            cmd.ExecuteNonQuery();
            _id = (int) cmd.LastInsertedId;
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static Specialty Find(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM specialties WHERE id = @thisId;";

            MySqlParameter thisId = new MySqlParameter();
            thisId.ParameterName = "@thisId";
            thisId.Value = id;
            cmd.Parameters.Add(thisId);

            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            int specialtyId = 0;
            string specialtyName = "";

            while (rdr.Read())
            {
                specialtyId = rdr.GetInt32(0);
                specialtyName = rdr.GetString(1);
            }
            Specialty foundSpecialty = new Specialty(specialtyName, specialtyId);

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }

           return foundSpecialty;
        }

        public static List<Specialty> GetAll()
        {
            List<Specialty> allSpecialties = new List<Specialty> {};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM specialties;";
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
              int SpecialtyId = rdr.GetInt32(0);
              string SpecialtyName = rdr.GetString(1);
              Specialty newSpecialty = new Specialty(SpecialtyName, SpecialtyId);
              allSpecialties.Add(newSpecialty);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allSpecialties;
        }

        public static void DeleteAll()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM specialties;
                DELETE FROM specialties_stylists;";
            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public void AddStylist(Stylist newStylist)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO specialties_stylists (specialty_id, stylist_id) VALUES (@SpecialId, @StyleId);";

            MySqlParameter special_id = new MySqlParameter();
            special_id.ParameterName = "@SpecialId";
            special_id.Value = _id;
            cmd.Parameters.Add(special_id);

            MySqlParameter style_id = new MySqlParameter();
            style_id.ParameterName = "@StyleId";
            style_id.Value = newStylist.GetId();
            cmd.Parameters.Add(style_id);

            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public List<Stylist> GetStylists()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT stylists.* FROM specialties
                JOIN specialties_stylists ON (specialties.id = specialties_stylists.specialty_id)
                JOIN stylists ON (specialties_stylists.stylist_id = stylists.id)
                WHERE specialties.id = @SpecialtyId;";

            MySqlParameter specialtyIdParameter = new MySqlParameter();
            specialtyIdParameter.ParameterName = "@SpecialtyId";
            specialtyIdParameter.Value = _id;
            cmd.Parameters.Add(specialtyIdParameter);
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;

            List<Stylist> stylists = new List<Stylist>{};
            while(rdr.Read())
            {
                int stylistId = rdr.GetInt32(0);
                string stylistName = rdr.GetString(1);
                string stylistDescription = rdr.GetString(2);
                Stylist newStylist = new Stylist(stylistName, stylistDescription, stylistId);
                stylists.Add(newStylist);
            }

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return stylists;
        }
    }
}
