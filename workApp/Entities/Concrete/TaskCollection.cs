﻿using Core.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class TaskCollection : IEntity
    {
        public Guid Id { get; set; }
        public int UserId {  get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Creation_Date { get; set; }
    }
}