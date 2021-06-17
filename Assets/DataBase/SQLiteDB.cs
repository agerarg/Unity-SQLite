using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;

public class SQLiteDB : MonoBehaviour
{
    public static SQLiteDB instance;
    private string dbName = "URI=file:DataBase.db";
    private void Awake()
    {
        instance=this;
    }
    void Start()
    {
        CreateTable();
        Query("INSERT INTO user (name, password) VALUES ('Jonny', '123456');");
        Query("SELECT * FROM user;");
    }

    private void CreateTable()
    {
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                string sqlcreation="";


                sqlcreation += "CREATE TABLE IF NOT EXISTS user(";
                sqlcreation += "id INTEGER NOT NULL ";
                sqlcreation += "PRIMARY KEY AUTOINCREMENT,";
                sqlcreation += "name     VARCHAR(50) NOT NULL,";
                sqlcreation += "password VARCHAR(50) NOT NULL";
                sqlcreation += ");";

                command.CommandText = sqlcreation;
                command.ExecuteNonQuery();
            }

            connection.Close();
        }
    }


    public void Query(string q)
    {
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = q;
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Debug.Log("name: " + reader["name"] + " password: " + reader["password"]);
                        
                    }
                }
            }

            connection.Close();
        }
    }
}
