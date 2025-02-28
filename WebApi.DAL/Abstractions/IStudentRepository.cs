using DTOs;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.DAL.Abstractions
{
    public interface IStudentRepository
    {
        Student GetStudentID(int StudentId);
        List<Student> GetAll();
        StudentDto AddStudent(StudentDto studentdto);
        bool DeleteStudent(int Id);
        StudentDto UpdateStudent(StudentDto updatedStudent);
        List<StudentDto> GetStudentsWhoBorrowedMostBooks();
    }
}
