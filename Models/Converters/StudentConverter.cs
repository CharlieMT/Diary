using System.Collections.Generic;
using System.Linq;
using Diary.Models.Wrappers;
using Diary.Models.Domains;

namespace Diary.Models.Converters
{
    public static class StudentConverter
    {
        public static StudentWrapper ToStudentWrapper(this Student model)
        {
            return new StudentWrapper
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Comments = model.Comments,
                Activities = model.Activities,
                Group = new GroupWrapper { Id = model.Id, Name = model.Group.Name },
                Math = string.Join(",", model.Raitings.Where(y => y.SubjectId == (int)Subject.Math).Select(y => y.Rate)),

                Physics = string.Join(",", model.Raitings.Where(y => y.SubjectId == (int)Subject.Phisics).Select(y => y.Rate)),

                Technology = string.Join(",", model.Raitings.Where(y => y.SubjectId == (int)Subject.Technology).Select(y => y.Rate)),

                PolishLang = string.Join(",", model.Raitings.Where(y => y.SubjectId == (int)Subject.PolishLang).Select(y => y.Rate)),

                ForeignLang = string.Join(",", model.Raitings.Where(y => y.SubjectId == (int)Subject.ForeignLang).Select(y => y.Rate))

            };
        }

        public static Student ToDao(this StudentWrapper model)
        {
            return new Student
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Comments = model.Comments,
                GroupId = model.Group.Id,
                Activities = model.Activities
            };
        }

        public static List<Raiting> ToRaitingDao(this StudentWrapper model)
        {
            var ratings = new List<Raiting>();

            if (!string.IsNullOrWhiteSpace(model.Math))
            {
                model.Math.Split(',').ToList().ForEach(x => ratings.Add(
                    new Raiting
                    {
                        Rate = int.Parse(x),
                        StudentId = model.Id,
                        SubjectId = (int)Subject.Math
                    }));
            }

            if (!string.IsNullOrWhiteSpace(model.Physics))
            {
                model.Physics.Split(',').ToList().ForEach(x => ratings.Add(
                    new Raiting
                    {
                        Rate = int.Parse(x),
                        StudentId = model.Id,
                        SubjectId = (int)Subject.Phisics
                    }));
            }

            if (!string.IsNullOrWhiteSpace(model.Technology))
            {
                model.Technology.Split(',').ToList().ForEach(x => ratings.Add(
                    new Raiting
                    {
                        Rate = int.Parse(x),
                        StudentId = model.Id,
                        SubjectId = (int)Subject.Technology
                    }));
            }

            if (!string.IsNullOrWhiteSpace(model.PolishLang))
            {
                model.PolishLang.Split(',').ToList().ForEach(x => ratings.Add(
                    new Raiting
                    {
                        Rate = int.Parse(x),
                        StudentId = model.Id,
                        SubjectId = (int)Subject.PolishLang
                    }));
            }

            if (!string.IsNullOrWhiteSpace(model.ForeignLang))
            {
                model.ForeignLang.Split(',').ToList().ForEach(x => ratings.Add(
                    new Raiting
                    {
                        Rate = int.Parse(x),
                        StudentId = model.Id,
                        SubjectId = (int)Subject.ForeignLang
                    }));
            }
            return ratings;
        }
    }
}
