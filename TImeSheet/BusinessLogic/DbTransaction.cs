using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TImeSheet.BusinessLogic
{
    public class DbTransaction
    {
        private static string connectionString = ConfigurationManager.AppSettings["ConnectionString"];
        private static SqlConnection sqlConnection = new SqlConnection(connectionString);

       
        /// <summary>
        /// Insert data to TimeSheetDetails
        /// </summary>
        /// <param name="taskID"></param>
        /// <param name="taskName"></param>
        /// <param name="isCoded"></param>
        /// <param name="isReviewed"></param>
        /// <param name="isCheckin"></param>
        /// <param name="taskDate"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="comment"></param>
        public void TaskStarted(string taskID, string taskName, string clientName, string isCoded, string isReviewed, string isCheckin,string taskDate, string startTime, string endTime=null, string comment=null)
        {
            string insertQuerry = "INSERT INTO TimeSheetDetails values('"
                + taskID + "','"
                + taskName + "','"
                + clientName + "','"
                + taskDate + "','"
                + startTime + "',"
                + (endTime == null ? "NULL" : "'" + endTime + "'") + ",'"
                + isCoded + "','"
                + isReviewed + "','"
                + isCheckin + "',"
                + (comment == null ? "NULL" : "'" + comment + "'" ) + ")";

            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(insertQuerry, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
        }        

        public void TaskEnded(string taskID, string taskName, string clientName, string isCoded, string isReviewed, string isCheckin, string endTime, string comment = null)
        {
            int lastID = GetLastUnfinshedTaskID();
            string updateQuerry = "UPDATE TimeSheetDetails SET "
                + "TaskID='" + taskID + "'"
                + ", TaskName='" + taskName + "'"
                + ", ClientName='" + clientName + "'"
                + ", TaskEndTime='" + endTime + "'"
                + ", IsCoded=" + isCoded
                + ", IsReviewed=" + isReviewed
                + ", IsCheckin=" + isCheckin
                + ", Comment=" + (comment == null ? "NULL" : "'" + comment + "'")
                + " WHERE ID=" + lastID;

            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(updateQuerry, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();

        }

        private int GetLastUnfinshedTaskID()
        {
            int ID = 0;
            string querryString = "SELECT TOP 1 ID FROM TimeSheetDetails WHERE TaskEndTime IS NULL ORDER BY TaskStartTime DESC";
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(querryString, sqlConnection);
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            if(sqlDataReader.Read())
            {
                int.TryParse(sqlDataReader["ID"].ToString(),out ID);
            }
            sqlConnection.Close();

            return ID;

        }


        public TimeSheetDetailsTable GetUnfinshedTaskDetails()
        {
            TimeSheetDetailsTable unFinishedTask = new TimeSheetDetailsTable();
            int ID = 0;

            string querryString = "SELECT TOP 1 * FROM TimeSheetDetails WHERE TaskEndTime IS NULL "
                + "AND ID=" + GetLastUnfinshedTaskID() + " ORDER BY TaskStartTime DESC";
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(querryString, sqlConnection);
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            if (sqlDataReader.Read())
            {
                int.TryParse(sqlDataReader["ID"].ToString(), out ID);
                unFinishedTask.ID = ID;
                unFinishedTask.TaskID = sqlDataReader["TaskID"].ToString();
                unFinishedTask.TaskName = sqlDataReader["TaskName"].ToString();
                unFinishedTask.ClientName = sqlDataReader["ClientName"].ToString();
                unFinishedTask.TaskDate = sqlDataReader["TaskDate"].ToString();
                unFinishedTask.TaskStartTime = sqlDataReader["TaskStartTime"].ToString();
                unFinishedTask.TaskEndTime = sqlDataReader["TaskEndTime"].ToString();
                unFinishedTask.IsCoded = sqlDataReader["IsCoded"].ToString();
                unFinishedTask.IsReviewed = sqlDataReader["IsReviewed"].ToString();
                unFinishedTask.IsCheckin = sqlDataReader["IsCheckin"].ToString();
                unFinishedTask.Comment = sqlDataReader["Comment"].ToString();
            }
            sqlConnection.Close();           

            return ID == 0 ? null : unFinishedTask;
        }

        /// <summary>
        /// List of all tasks in a date range
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public List<TimeSheetDetailsTable> GetAllTasksForPeriod(string startDate, string endDate)
        {
            List<TimeSheetDetailsTable> allTasksList = new List<TimeSheetDetailsTable>();

            string querryString = "SELECT * FROM TimeSheetDetails WHERE TaskDate >= '" + startDate 
                + "' AND TaskDate <= '" + endDate + "'"; 
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(querryString, sqlConnection);
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            while (sqlDataReader.Read())
            {
                allTasksList.Add(new TimeSheetDetailsTable()
                {
                    ID = int.Parse(sqlDataReader["ID"].ToString()),
                    TaskID = sqlDataReader["TaskID"].ToString(),
                    TaskName = sqlDataReader["TaskName"].ToString(),
                    ClientName = sqlDataReader["ClientName"].ToString(),
                    TaskDate = sqlDataReader["TaskDate"].ToString(),
                    TaskStartTime = sqlDataReader["TaskStartTime"].ToString(),
                    TaskEndTime = sqlDataReader["TaskEndTime"].ToString(),
                    IsCoded = sqlDataReader["IsCoded"].ToString(),
                    IsReviewed = sqlDataReader["IsReviewed"].ToString(),
                    IsCheckin = sqlDataReader["IsCheckin"].ToString(),
                    Comment = sqlDataReader["Comment"].ToString()
                });

            }
            sqlConnection.Close();
            return allTasksList;
        }

        public List<ClientListTable> GetAllClientList()
        {
            List<ClientListTable> allClientList = new List<ClientListTable>();

            string querryString = "SELECT * FROM ClientDetails";
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(querryString, sqlConnection);
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            while (sqlDataReader.Read())
            {
                allClientList.Add(new ClientListTable()
                {
                    ID = int.Parse(sqlDataReader["ID"].ToString()),
                    ClientName = sqlDataReader["ClientName"].ToString(),
                    ClientDescription = sqlDataReader["ClientDescription"].ToString()
                });

            }
            sqlConnection.Close();

            return allClientList;
        }
        
        /// <summary>
        /// update the client table if not exist
        /// </summary>
        /// <param name="clientName"></param>
        /// <param name="clientDescription"></param>

        public void AddNonExistingClient(string clientName, string clientDescription=null)
        {
            string querryString = "SELECT * FROM ClientDetails WHERE ClientName='" + clientName + "'";
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(querryString, sqlConnection);
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            if (!sqlDataReader.Read())
            {
                sqlDataReader.Close();
                querryString = "INSERT INTO ClientDetails VALUES ('"
                    + clientName + "',"
                    + (clientDescription == null ? "NULL)" : "'" + clientDescription + "')");
                sqlCommand = new SqlCommand(querryString, sqlConnection);
                sqlCommand.ExecuteNonQuery();
            }
            sqlConnection.Close();
        }

        public void GetExportData(string fromDate, string toDate, out List<EmailTemplate> emailData, out List<SentrifugoTemplate> sentrifugoData)
        {
            List<TimeSheetDetailsTable> taskDetailsList = GetAllTasksForPeriod(fromDate, toDate);
            emailData = GetEmailData(taskDetailsList);
            sentrifugoData = GetSentrifugoData(taskDetailsList);

        }

        public List<EmailTemplate> GetEmailData(List<TimeSheetDetailsTable> taskDetailsList)
        {
            List<EmailTemplate> emailData = taskDetailsList.GroupBy(x => new { x.TaskName, x.ClientName, x.TaskID }).Select(x =>
            new EmailTemplate
            {
                TaskID = x.Key.TaskID.ToString(),
                TaskName = x.Key.TaskName.ToString(),
                ClientName = x.Key.ClientName.ToString(),
                Status = GetStatusString(x.OrderByDescending(y => y.TaskEndTime).First().IsCoded, x.OrderByDescending(y => y.TaskEndTime).First().IsReviewed, x.OrderByDescending(y => y.TaskEndTime).First().IsCheckin)
            }).ToList();

            return emailData;
        }

        

        public List<SentrifugoTemplate> GetSentrifugoData(List<TimeSheetDetailsTable> taskDetailsList)
        {
            List<SentrifugoTemplate> sentrifugoData = taskDetailsList.GroupBy(x => new { x.ClientName, DateTime.Parse(x.TaskDate).Date }).Select(y =>
            new SentrifugoTemplate
            {
                ClientName = y.Key.ClientName.ToString(),
                TaskNames = string.Join(",", y.OrderBy(q => q.TaskDate).Select(t => t.TaskName)),
                TaskDate = y.Key.Date.ToString("yyyy-MM-dd"),
                Duration = GetDuration(y.Sum(z => TimeSpan.Parse(z.TaskEndTime).TotalMinutes - TimeSpan.Parse(z.TaskStartTime).TotalMinutes))
            }
            ).ToList();
            
            return sentrifugoData;
        }

        private string GetDuration(double minutes)
        {
            return $"{ (int)minutes / 60 } : {(int)minutes % 60 }";
        }


        private string GetStatusString(string isCoded, string isReviewd, string isCheckin)
        {
            string status = $"Coding {(bool.Parse(isCoded) ? "done" : "pending")}, Review {(bool.Parse(isReviewd) ? "done" : "pending")}, Checkin {(bool.Parse(isCheckin) ? "done" : "pending")}";
            return status;
        }




    }
}
