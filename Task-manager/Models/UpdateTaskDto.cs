namespace Task_Management.Models
{
    public class UpdateTaskDto
    {
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public int status_id { get; set; }
    }
}
