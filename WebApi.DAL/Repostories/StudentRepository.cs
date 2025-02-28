using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.DAL.DBContext;
using Entities;
using WebApi.DAL.Abstractions;
using DTOs;
using Microsoft.EntityFrameworkCore;

namespace WebApi.DAL.Repostories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly DBContext.ApplicationDbContext _context;

        public StudentRepository(DBContext.ApplicationDbContext context)
        {
            _context = context;
        }
        public Student GetStudentID(int StudentID )
        {
            return _context.Students.FirstOrDefault(x => x.StudentId == StudentID);
        }

        public List<Student> GetAll()
        {
            return _context.Students.ToList();

        }

        public StudentDto AddStudent(StudentDto studentdto)
        {
            var studentData = Entities(studentdto);
            _context.Students.Add(studentData);
            _context.SaveChanges();
            return studentdto;
        }
    
        public bool DeleteStudent(int studentId)
        {
            var student = _context.Students.Find(studentId);
            if (student == null) return false;
            _context.Students.Remove(student);
            _context.SaveChanges();
            return true;
        }
        public StudentDto UpdateStudent(StudentDto updatedStudent)
        {
            var student = _context.Students.Find(updatedStudent.StudentId);
            if (student != null)
            {
                student.Age = updatedStudent.Age;

                _context.SaveChanges();
            }
            return updatedStudent;
        }
       
        public List<StudentDto> GetStudentsWhoBorrowedMostBooks()
        {
            return _context.Students
                .Include(b => b.Borrows)
                .OrderByDescending(stu => stu.Borrows.Count)
                .Take(5)
                .Select(student => new StudentDto
                {
                    StudentId = student.StudentId,
                    Name = student.Name,
                    Age = student.Age,
                    BorrowCount = student.Borrows.Count

                })
                .ToList();
        }

        private Student Entities(StudentDto studentDto)
        {
            Student student = new Student();
            student.StudentId = studentDto.StudentId;
            student.Name = studentDto.Name;
            student.Age = studentDto.Age;
            return student;


        }
    }
}
