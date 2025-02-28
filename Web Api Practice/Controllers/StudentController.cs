using DTOs;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using WebApi.DAL.Abstractions;
using WebApi.DAL.DBContext;
using WebApi.DAL.Repositories;

namespace Web_Api_Practice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly ApplicationDbContext _context; // Add ApplicationDbContext here

        private readonly IStudentRepository _studentRepository;
        private readonly IConfiguration _configuration;

 

        public StudentController(ApplicationDbContext context, IStudentRepository studentRepository, IConfiguration configuration) // Add ApplicationDbContext parameter here
        {
            _context = context; 
            _studentRepository = studentRepository;
            _configuration = configuration;
        }

        // GET: api/Students
        [HttpGet]
        public ActionResult<List<StudentDto>> GetAll()
        {
            var students = _studentRepository.GetAll();
            if (students == null || !students.Any())
                return NotFound("No students found.");

            return Ok(students);
        }

        // GET: api/Students/5
        [HttpGet("{id}")]
        public ActionResult<StudentDto> GetStudentID(int id)
        {
            var student = _studentRepository.GetStudentID(id);

            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }

        // POST: api/Students
        [HttpPost]
        public ActionResult<StudentDto> AddStudent(StudentDto studentDto)
        {
            var addedStudent = _studentRepository.AddStudent(studentDto);
            return CreatedAtAction(nameof(GetStudentID), new { id = addedStudent.StudentId }, addedStudent);
        }

        // PUT: api/Students/5
        [HttpPut("{id}")]
        public IActionResult UpdateStudent(int id, StudentDto studentDto)
        {
            if (id != studentDto.StudentId)
            {
                return BadRequest("Mismatched student ID.");
            }

            var updatedStudent = _studentRepository.UpdateStudent(studentDto);
            if (updatedStudent == null)
            {
                return NotFound("Student not found.");
            }

            return NoContent();
        }

        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(int id)
        {
            bool result = _studentRepository.DeleteStudent(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpPost("ExecuteAddingStudents")]
        public IActionResult ExecuteAddingStudents()
        {
            try
            {
                _context.ExecuteAddingStudents(); // Assuming _context is your ApplicationDbContext instance
                return Ok("Stored procedure executed successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


        // GET: api/Students/GetStudentsWhoBorrowedMostBooks
        [HttpGet("GetStudentsWhoBorrowedMostBooks")]
        public ActionResult GetStudentsWhoBorrowedMostBooks()
        {
            var mostBorrowedStudents = _studentRepository.GetStudentsWhoBorrowedMostBooks();
            if (mostBorrowedStudents == null || !mostBorrowedStudents.Any())
                return NotFound("No borrowing records found.");

            return Ok(mostBorrowedStudents);
        }

        [HttpGet("SPAddingStudents")]

        public ActionResult ExecuteStoreProcedure()
        
        {
            Student stu = new Student();
            stu.Name = "Ali";
            stu.Age = 22;
            stu.StudentId = 1;
            // Serialize the Person object to JSON
            string json = JsonConvert.SerializeObject(stu);

            // Output the JSON string
            Console.WriteLine(json);

            Student std = JsonConvert.DeserializeObject<Student>(json);

            string connectionString = _configuration.GetConnectionString("default");

            // Name of the stored procedure
            string storedProcedureName = "AddingStudents";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Create the SqlCommand object
                using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
                {
                    // Set the command type to StoredProcedure
                    command.CommandType = CommandType.StoredProcedure;

                    try
                    {
                        // Open the connection
                        connection.Open();

                        // Execute the stored procedure
                        int rowsAffected = command.ExecuteNonQuery();

                        // Return the number of rows affected
                        return Ok("Store procedure is executed");
                    }
                    catch (SqlException ex)
                    {
                        // Handle any errors that may have occurred
                        Console.WriteLine("SQL Error: " + ex.Message);
                        return StatusCode(500, "Internal server error");
                    }
                }
            }
        }
    }
    //        string connectionString = _configuration.GetConnectionString("default");
    //        Student s = new Student();
    //        // Name of the stored procedure
    //        string storedProcedureName = "AddingStudents";

    //        using (SqlConnection connection = new SqlConnection(connectionString))
    //        {
    //            // Create the SqlCommand object
    //            using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
    //            {
    //                // Set the command type to StoredProcedure
    //                command.CommandType = CommandType.StoredProcedure;

    //                // Add any parameters the stored procedure requires
    //                // For example, if the stored procedure has an input parameter called "ParameterName"
    //                //command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
    //                //command.Parameters["@id"].Value = 3;

    //                try
    //                {
    //                    // Open the connection
    //                    connection.Open();

    //                    // Execute the stored procedure
    //                    // Use ExecuteNonQuery for non-query operations like insert, update, delete
    //                    // Use ExecuteScalar to retrieve a single value (e.g., a count)
    //                    // Use ExecuteReader to retrieve a result set
    //                    using (SqlDataReader reader = command.ExecuteReader())
    //                    {
    //                        // Process the result set
    //                        while (reader.Read())
    //                        {
    //                            s.Name = reader[1].ToString();
                               
    //                        }
                          
    //                    }
    //                }
    //                catch (SqlException ex)
    //                {
    //                    // Handle any errors that may have occurred
    //                    Console.WriteLine("SQL Error: " + ex.Message);
    //                }
    //            }
    //        }
    //        return Ok(s);
    //    }

    //}
}