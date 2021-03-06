﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.DAL.Entities
{
    public class Role
    {
        public Int32 Id { get; set; }
        public String Name { get; set; }

        public virtual ICollection<User> Users { get; set; } 
    }
}
