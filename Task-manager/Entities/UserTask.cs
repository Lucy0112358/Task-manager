﻿namespace Task_Management.Entities
{
    public class UserTask
    {
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public int status_id { get; set; }
    }
}
