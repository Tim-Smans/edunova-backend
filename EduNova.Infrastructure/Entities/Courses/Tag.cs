﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduNova.Infrastructure.Entities.Courses
{
    public class Tag
    {

        public Guid Id { get; set; }
        public string Title { get; set; }
        public Guid TenantId { get; set; }
    }
}
