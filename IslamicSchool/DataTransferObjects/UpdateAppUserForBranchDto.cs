﻿namespace IslamicSchool.DataTransferObjects
{
    public class UpdateAppUserForBranchDto
    {
        public Guid Id { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? FatherName { get; set; }
        public string? Address { get; set; }
        public int? BranchId { get; set; }
    }
}