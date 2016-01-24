using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;

namespace SOTI.Model
{
    public class DataLayer
    {
        //Database Connection

        static string path = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName)
                     + "\\SOTI.accdb;";

        static string connectionString =
            @"Provider=Microsoft.ACE.OLEDB.12.0;" +
            @"Data Source=" + path +
            @"User Id=Admin;";

        OleDbConnection conn = null;
        
        private void ConnectDB()
        {
            conn = new OleDbConnection(connectionString);
            if (conn.State != System.Data.ConnectionState.Open)
            {
                conn.Open();
            }
            
        }


        //PROPERTIES
        public List<Cibo> cibi;
        public List<Ricetta> ricette;
        public List<Allergia> allergie;
        public List<Tags> tags;

        //Constructor
        public DataLayer()
        {
            ConnectDB();
            this.cibi = ReadCibi();
            this.allergie = ReadAllergie();
            this.ricette = ReadRicette();
            this.tags = ReadTags();
            ReadPassi();
        }

        private List<Tags> ReadTags()
        {
            OleDbDataReader reader = null;
            List<Tags> tags = new List<Tags>();
            try
            {
                OleDbCommand cmd =
                    new OleDbCommand("Select * FROM Tags", conn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Tags newItem = new Tags();
                    newItem.tag = reader["Tag"].ToString();
                    newItem.id = reader["IdCibo"].ToString();
                    tags.Add(newItem);
                }

            }
            finally
            {
                if (reader != null) reader.Close();
            }
            return tags;
        }
    

        private void ReadPassi()
        {
            OleDbDataReader reader = null;
            try
            {
                OleDbCommand cmd =
                    new OleDbCommand("Select * FROM PassiRicetta ORDER BY Ordine", conn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Passo newItem = new Passo();
                    newItem.idRicetta = (int)reader["IdRicetta"];
                    newItem.ordine = (int)reader["Ordine"];
                    newItem.passoDoppio = (bool)reader["PassoDoppio"];
                    int idCiboGiusto = (int)reader["IngredienteGiusto"];
                    int idCiboSbagliato = (int)reader["IngredienteSbagliato"];
                    foreach (Cibo cibo in cibi)
                    {
                        if (cibo.id.Equals(idCiboGiusto)){
                            newItem.ciboGiusto = cibo;
                        }else if (cibo.id.Equals(idCiboSbagliato)){
                            newItem.ciboSbagliato = cibo;
                        }
                    }

                    foreach (Ricetta ricetta in ricette)
                    {
                        if (ricetta.id.Equals(newItem.idRicetta)) {
                            ricetta.passi.Add(newItem);
                        }
                    }
                    
                }

            }
            finally
            {
                if (reader != null) reader.Close();
            }
        }

        //Load Allergie from DataBase
        private List<Allergia> ReadAllergie()
        {
            OleDbDataReader reader = null;
            List<Allergia> allergie = new List<Allergia>();
            try
            {
                OleDbCommand cmd =
                    new OleDbCommand("Select * FROM Allergie", conn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Allergia newItem = new Allergia();
                    newItem.nome = reader["Nome"].ToString();
                    newItem.descrizione = reader["Descrizione"].ToString();
                    newItem.immagine = reader["Immagine"].ToString();
                    allergie.Add(newItem);
                }

            }
            finally
            {
                if (reader != null) reader.Close();
            }
            return allergie;
        }


        //Load ricette from DataBase
        private List<Ricetta> ReadRicette()
        {
            OleDbDataReader reader = null;
            List<Ricetta> ricette = new List<Ricetta>();
            try
            {
                OleDbCommand cmd =
                    new OleDbCommand("Select * FROM Ricette", conn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Ricetta newItem = new Ricetta();
                    newItem.id = (int)reader["ID"];
                    newItem.nome = reader["Nome"].ToString();
                    string allergia = reader["Allergia"].ToString();
                    newItem.immagine = reader["Immagine"].ToString();
                    newItem.passi = new List<Passo>();

                    foreach (var item in allergie)
                    {
                        if (item.nome.Equals(allergia)){
                            newItem.allergia = item;
                        }
                    }
                    ricette.Add(newItem);
                }

            }
            finally
            {
                if (reader != null) reader.Close();
            }
            return ricette;

        }
        

        //Load Cibi from DataBase
        private List<Cibo> ReadCibi()
        {
            OleDbDataReader reader = null;
            List<Cibo> cibi = new List<Cibo>();
            try
            {
                OleDbCommand cmd =
                    new OleDbCommand("Select * FROM Cibi", conn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Cibo newItem = new Cibo();
                    newItem.id = (int)reader["ID"];
                    newItem.nome = reader["Nome"].ToString();
                    newItem.descrizione = reader["Descrizione"].ToString();
                    newItem.uovo = (bool)reader["Uovo"];
                    newItem.latte = (bool)reader["Latte"];
                    newItem.pesce = (bool)reader["Pesce"];
                    newItem.frutta = (bool)reader["Frutta"];
                    newItem.immagine = reader["Immagine"].ToString();
                    newItem.prezzo = (int)reader["Prezzo"];

                    cibi.Add(newItem);
                }

            }
            finally
            {
                if (reader != null) reader.Close();
            }
            return cibi;
        }

    }
}
