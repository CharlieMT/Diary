using System.Collections.Generic;
using System.Linq;
using Diary.Models.Domains;
using Diary.Models.Wrappers;
using System.Data.Entity;
using Diary.Models.Converters;
using Diary.Models;

namespace Diary
{
    class Repository
    {

        public List<Group> GetGroups()
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Groups.ToList();
            }
        }

        public List<StudentWrapper> GetStudent(int groupID)
        {
            using(var context = new ApplicationDbContext())
            {
                var students = context.Students.Include(x => x.Group).Include(x => x.Raitings).AsQueryable();

                if (groupID != 0)
                    students = students.Where(x => x.GroupId == groupID);

                return students.ToList().Select(x => x.ToStudentWrapper()).ToList();
            }
        }


        internal void DeleteStudent(int id)
        {
            using( var context = new ApplicationDbContext())
            {
                var studentToDelete = context.Students.Find(id);
                context.Students.Remove(studentToDelete);
                context.SaveChanges();
            }
        }

        internal void UpdateStudent(StudentWrapper studentwraper)
        {
            var student = studentwraper.ToDao();
            var raitings = studentwraper.ToRaitingDao();

            using(var context = new ApplicationDbContext())
            {
                UpdateStudentProperties(context, student);

                var studentRaitings = GetStudentRaitings(context, student);

                UpdateRate(student, raitings, context, studentRaitings, Subject.Math);
                UpdateRate(student, raitings, context, studentRaitings, Subject.Phisics);
                UpdateRate(student, raitings, context, studentRaitings, Subject.Technology);
                UpdateRate(student, raitings, context, studentRaitings, Subject.PolishLang);
                UpdateRate(student, raitings, context, studentRaitings, Subject.ForeignLang);

                context.SaveChanges();
            }
        }

        private static List<Raiting> GetStudentRaitings(ApplicationDbContext context, Student student)
        {
            return context.Raitings.Where(x => x.StudentId == student.Id).ToList();
        }

        private void UpdateStudentProperties(ApplicationDbContext context, Student student)
        {
            var studentToUpdate = context.Students.Find(student.Id);
            studentToUpdate.FirstName = student.FirstName;
            studentToUpdate.LastName = student.LastName;
            studentToUpdate.GroupId = student.GroupId;
            studentToUpdate.Comments = student.Comments;
            studentToUpdate.Activities = student.Activities;
        }

        private static void UpdateRate(Student student, List<Raiting> newRaitings, ApplicationDbContext context, List<Raiting> studentRaiting, Subject subject)
        {
            var studentsRaitings = context.Raitings.Where(x => x.StudentId == student.Id).ToList();

            var subRatings = studentsRaitings.Where(x => x.SubjectId == (int)subject).Select(x => x.Rate);

            var newSubRatings = newRaitings.Where(x => x.SubjectId == (int)subject).Select(x => x.Rate);

            var subRaitingstoDelete = subRatings.Except(newSubRatings).ToList();

            var subRaitingsToAdd = newSubRatings.Except(subRatings).ToList();

            subRaitingstoDelete.ForEach(x =>
            {
                var raitingToDelete = context.Raitings.First(y =>
                y.Rate == x &&
                y.StudentId == student.Id &&
                y.SubjectId == (int)subject);

                context.Raitings.Remove(raitingToDelete);
            });

            subRaitingsToAdd.ForEach(x =>
            {
                var raitingToAdd = new Raiting
                {
                    Rate = x,
                    StudentId = student.Id,
                    SubjectId = (int)subject
                };

                context.Raitings.Add(raitingToAdd);
            });

        }

        internal void AddStudent(StudentWrapper studentwraper)
        {
            var student = studentwraper.ToDao();
            var raitings = studentwraper.ToRaitingDao();

            using(var context = new ApplicationDbContext())
            {
                var dbStudent = context.Students.Add(student);

                raitings.ForEach(x =>
                {
                    x.StudentId = dbStudent.Id;
                    context.Raitings.Add(x);
                });

                context.SaveChanges();
            }
        }
    }
}
