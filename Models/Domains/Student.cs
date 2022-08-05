﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Diary.Models.Wrappers;

namespace Diary.Models.Domains
{
    public class Student
    {
        public Student()
        {
            Raitings = new Collection<Raiting>();
        }

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Comments { get; set; }
        public bool Activities { get; set; }

        public int GroupId { get; set; }

        public Group Group { get; set; }

        public ICollection<Raiting> Raitings { get; set; }
    }
}
