using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;
using System.Collections.ObjectModel;

namespace TaskApplication.Models
{
    public class database
    {
        string dbPath;
        string connectionString;
        public database()
        {
            dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "tasks.db");
            connectionString = $"Data Source={dbPath}";
        }

        public bool InsertTask(task t)
        {
            bool result = false;
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();
                    string insertQuery = "INSERT INTO task (title, description,category,deadline,finished) VALUES (@title, @desc, @cat, @dead, @fin);";

                    using (SQLiteCommand cmd = new SQLiteCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@title", t.title);
                        cmd.Parameters.AddWithValue("@desc", t.description);
                        cmd.Parameters.AddWithValue("@cat", t.category);
                        cmd.Parameters.AddWithValue("@dead", t.deadline);
                        cmd.Parameters.AddWithValue("@fin", t.finished);
                        cmd.ExecuteNonQuery();
                    }
                    conn.Close();
                    result = true;
                }

            }
            catch (Exception e)
            {
                result = false;
            } 

           return result;
        }

        public ObservableCollection<task> GetTasks()
        {
            ObservableCollection<task> tasks = new ObservableCollection<task>();
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();
                    string selectQuery = "SELECT * FROM task;";
                    using (SQLiteCommand cmd = new SQLiteCommand(selectQuery, conn))
                    {
                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                task t = new task();
                                t.title = reader["title"].ToString();
                                t.description = reader["description"].ToString();
                                t.category = Convert.ToInt32(reader["category"]);
                                t.deadline = reader["deadline"].ToString();
                                t.finished = Convert.ToInt32(reader["finished"]);
                                tasks.Add(t);
                            }
                        }
                    }
                    conn.Close();
                }
            }
            catch (Exception e)
            {
                // maak een lege lijst
                tasks = new ObservableCollection<task>();
            }
            return tasks;
        }


        public bool SaveTasks(ObservableCollection<task> tasks)
        {
            // save the tasks to the database
            bool result = false;
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();
                    string deleteQuery = "DELETE FROM task;";
                    using (SQLiteCommand cmd = new SQLiteCommand(deleteQuery, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                    foreach (task t in tasks)
                    {
                        string insertQuery = "INSERT INTO task (title, description,category,deadline,finished) VALUES (@title, @desc, @cat, @dead, @fin);";
                        using (SQLiteCommand cmd = new SQLiteCommand(insertQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@title", t.title);
                            cmd.Parameters.AddWithValue("@desc", t.description);
                            cmd.Parameters.AddWithValue("@cat", t.category);
                            cmd.Parameters.AddWithValue("@dead", t.deadline);
                            cmd.Parameters.AddWithValue("@fin", t.finished);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    conn.Close();
                    result = true;
                }
            }
            catch (Exception e)
            {
                result = false;
            }
            return result;
        }



        public int TaskCount { 
            get {
                // This is a property that returns the number of tasks in the database
                int count = 0;
                try
                {
                    using (SQLiteConnection conn = new SQLiteConnection(connectionString))
                    {
                        conn.Open();
                        string countQuery = "SELECT COUNT(*) FROM task;";
                        using (SQLiteCommand cmd = new SQLiteCommand(countQuery, conn))
                        {
                            count = Convert.ToInt32(cmd.ExecuteScalar());
                        }
                        conn.Close();
                    }
                }
                catch (Exception e)
                {
                    count = 0;
                }
                return count;
            }  
        }

    }
}
