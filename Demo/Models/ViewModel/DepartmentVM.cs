﻿using Demo.Models;

namespace Demo.Models.ViewModel
{
    public class DepartmentVM
    {
        public int currentPageIndex { get; set; }
        public int PageCount { get; set; }
        public List<Department> departmentsList { get; set; }
    }
}
