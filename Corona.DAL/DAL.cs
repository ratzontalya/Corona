using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using BE;
using System.Web;

namespace DAL
{
    public class DAL:IDAL
    {
        #region INITIALIZE
        private MySqlConnection connection;
        public DAL()
        {
            Initialize();
        }
        private void Initialize()
        {
            using (StreamReader r = new StreamReader(HttpContext.Current.Server.MapPath("~/App_Data/databaseConnection.json")))
            {
                string json = r.ReadToEnd();
                DatabaseConnection database = JsonConvert.DeserializeObject<DatabaseConnection>(json);
                connection = new MySqlConnection();
                connection.ConnectionString = $"server={database.server};user id={database.username};password={database.password};database={database.database}";
            }
        }
        //open connection to database
        private bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                //When handling errors, you can your application's response based 
                //on the error number.
                //The two most common error numbers when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                switch (ex.Number)
                {
                    case 0:
                        throw new Exception("Cannot connect to server.  Contact administrator");
                    case 1045:
                        throw new Exception("Invalid username/password, please try again");
                }
                return false;
            }
        }
        //Close connection
        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
        #region SELECT
        public IEnumerable<Patient> GetPatients()
        {
            List<Patient> result = new List<Patient>();
            string query = "SELECT * FROM patients WHERE active=true";
            if (OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    result.Add(new Patient
                    {
                        id = int.Parse(dataReader["id"] + ""),
                        firstName = dataReader["firstName"] + "",
                        lastName = dataReader["lastName"] + "",
                        address = dataReader["address"] + "",
                        phone = dataReader["phone"] + "",
                        telephone = dataReader["telephone"] + "",
                        profileImage = dataReader["profileImage"] + "",
                        birthDate = (DateTime)dataReader["birthDate"],
                        sickDate = (DateTime)dataReader["sickDate"],
                        recoveryDate = (DateTime)dataReader["recoveryDate"],
                        active = true
                    });
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                CloseConnection();

                //return list to be displayed
                return result;
            }
            else
            {
                return result;
            }
        }

        public Patient GetPatientById(int id)
        {
            Patient result = null;
            string query = $"SELECT * FROM patients WHERE id = {id}";
            if (OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.Read())
                {
                    result = new Patient
                    {
                        id = int.Parse(dataReader["id"] + ""),
                        firstName = dataReader["firstName"] + "",
                        lastName = dataReader["lastName"] + "",
                        address = dataReader["address"] + "",
                        phone = dataReader["phone"] + "",
                        telephone = dataReader["telephone"] + "",
                        profileImage = dataReader["profileImage"] + "",
                        birthDate = (DateTime)dataReader["birthDate"],
                        sickDate = (DateTime)dataReader["sickDate"],
                        recoveryDate = (DateTime)dataReader["recoveryDate"],
                        active = true
                    };
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                CloseConnection();

                //return list to be displayed
                return result;
            }
            else
            {
                return result;
            }
        }
        public bool DeletePatient(int id)
        {
            try
            {
                if (OpenConnection() == true)
                {
                    string query = $"UPDATE patients SET active=false " +
                        $"where id = {id}";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                    CloseConnection();
                }
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }

        public bool UpdatePatient(Patient patient)
        {
            string query = $"UPDATE patients SET firstName='{patient.firstName}', lastName='{patient.lastName}',birthDate='{patient.birthDate.ToString("yyyy/MM/dd HH:mm:ss")}', address='{patient.address}', phone='{patient.phone}', telephone='{patient.telephone}', sickDate='{patient.sickDate.ToString("yyyy/MM/dd HH:mm:ss")}', recoveryDate='{patient.recoveryDate.ToString("yyyy/MM/dd HH:mm:ss")}', profileImage='{patient.profileImage}' " +
    $"where id = {patient.id}";
            try
            {
                if (OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                    CloseConnection();
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public bool UpdateVaccineOfPatient(Vaccine vaccine)
        {
            string query = $"UPDATE vaccines SET `date`='{vaccine.date.ToString("yyyy/MM/dd HH:mm:ss")}', `manufacturer`='{vaccine.manufacturer}' " +
   $"where autoId = {vaccine.autoId}";
            try
            {
                if (OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                    CloseConnection();
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public String GetManufacturerName(int id)
        {

            String result = null;
            string query = $"SELECT fullName FROM manufacturers WHERE id = {id}";
            if (OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.Read())
                {
                    result = dataReader["firstName"] + "";
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                CloseConnection();

                //return list to be displayed
                return result;
            }
            else
            {
                return result;
            }
        }
        #endregion
        public List<Vaccine> getVaccinesOfPatient(int id)
        {
            List<Vaccine> result = new List<Vaccine>();
            string query = $"SELECT * FROM Vaccines WHERE id={id}";
            if (OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    result.Add(new Vaccine
                    {
                        id = int.Parse(dataReader["id"] + ""),
                        autoId = int.Parse(dataReader["autoId"] + ""),
                        manufacturer = dataReader["manufacturer"] + "",
                        date = (DateTime)dataReader["date"],
                    });
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                CloseConnection();

                //return list to be displayed
                return result;
            }
            else
            {
                return result;
            }
        }
        public bool CreatePatient(Patient patient)
        {

            string query = $"INSERT INTO `patients` (`id`, `firstName`, `lastName`, `address`, `birthDate`, `phone`,`telephone`,`sickDate`,`recoveryDate`) VALUES ('{patient.id}', '{patient.firstName}', '{patient.lastName}', '{patient.address}', '{patient.birthDate.ToString("yyyy/MM/dd HH:mm:ss")}', '{patient.phone}','{patient.telephone}','{patient.sickDate.ToString("yyyy/MM/dd HH:mm:ss")}', '{patient.recoveryDate.ToString("yyyy/MM/dd HH:mm:ss")}')";
            try
            {
                if (OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                    CloseConnection();
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public Vaccine CreateVaccineForPatient(Vaccine vaccine)
        {
            string query = $"INSERT INTO `vaccines` (`id`,`date`,`manufacturer`) VALUES ('{vaccine.id}', '{vaccine.date.ToString("yyyy/MM/dd HH:mm:ss")}', '{vaccine.manufacturer}')";
            try
            {
                if (OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                    // get auto increment primary key
                    query = "SELECT LAST_INSERT_ID() from vaccines";
                    cmd = new MySqlCommand(query, connection);
                    var autoId = cmd.ExecuteScalar();
                    vaccine.autoId = int.Parse(autoId.ToString());
                    CloseConnection();
                }
                return vaccine;
            }
            catch (Exception e)
            {
                return vaccine;
            }
        }

        public List<int> GetSickPatientsInLastMonth()
        {
            DateTime start = DateTime.Now.AddMonths(-1);
            DateTime end = DateTime.Now;
            List<int> result = new List<int>();
            while (end != start) { 
            string query = $"SELECT date, SUM(*) AS sickNum " +
                $"FROM patients" +
                $"where active = {true} AND sickDate =< '{start:yyyy-MM-dd}' AND recoveryDate > '{start:yyyy-MM-dd}' " +
                $"GROUP BY date";
                if (OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    MySqlDataReader dataReader = cmd.ExecuteReader();
                    int prev = 0;
                    while (dataReader.Read())
                    {
                        if (((DateTime)dataReader["date"]).Day == prev + 1)
                        {
                            result.Add((int)dataReader["sickNum"]);
                        }
                        else
                        {
                            result.Add(0);
                        }
                        prev += 1;
                    }
                    //close Data Reader
                    dataReader.Close();
                }


                //close Connection
                CloseConnection();
            }
            return result;
        }
    }

}
