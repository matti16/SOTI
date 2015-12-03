using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Reflection;

namespace SOTI.Model
{
    class DataLayer
    {
        //Database Connection
        string connectionString =
            @"Provider=Microsoft.ACE.OLEDB.12.0;" +
            @"Data Source=C:\Users\Mattia\Documents\GitHubVisualStudio\SOTI\SOTI.accdb;" +
            @"User Id=;Password=;";

        OleDbConnection conn = null;

        private void ConnectDB()
        {
            try
            {
                conn = new OleDbConnection(connectionString);
                conn.Open();
            }
            catch
            {
                ConnectDB();
            }
        }


        //PROPERTIES
        public List<Cibo> cibi;
        public List<Ricetta> ricette;
        public List<Allergia> allergie;

        //Constructor
        public DataLayer()
        {
            ConnectDB();
            this.cibi = ReadCibi();
            this.ricette = ReadRicette();
            this.allergie = ReadAllergie();
            ReadPassi();
            conn.Close();
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
                    newItem.nome = (String)reader["Nome"];
                    newItem.allergia = (String)reader["Allergia"];
                    newItem.immagine = reader["Immagine"].ToString();
                    newItem.passi = new List<Passo>();

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
                    newItem.nome = (String)reader["Nome"];
                    newItem.descrizione = (String)reader["Descrizione"];
                    newItem.uovo = (bool)reader["Uovo"];
                    newItem.latte = (bool)reader["Latte"];
                    newItem.pesce = (bool)reader["Pesce"];
                    newItem.frutta = (bool)reader["Frutta"];
                    newItem.immagine = reader["Immagine"].ToString();

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
