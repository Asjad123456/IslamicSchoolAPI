﻿namespace IslamicSchool.Entities
{
    public class Branch
    {
        public int Id { get; set; }
        public string BranchName { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public int BranchCode { get; set; }
        public int BranchAdminId { get; set; }
        public BranchAdmin BranchAdmin { get; set; }
    }
}
