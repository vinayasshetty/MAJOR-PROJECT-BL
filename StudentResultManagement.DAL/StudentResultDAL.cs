using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentResultManagement.DTO;
using System.Data.SqlClient;
namespace StudentResultManagement.DAL
{
    public class StudentResultDAL
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /*Query for adding the Admin*/
        public void AddAdmin(AdminDTO ad)
        {
            try
            {
                using (var dbctx = new StudentResultManagementSystemProjectEntities1())
                {
                    Admin admin = new Admin
                    {
                        Name=ad.Name,
                        EmailId=ad.EmailId,
                        Password=ad.Password

                    };
                    dbctx.Admins.Add(admin);
                    dbctx.SaveChanges();
                }

            }
            catch(Exception ex)
            {
                throw new Exception("ad",ex);
            }
            
            
        }
      

        /*Query to check whether Admin Exists*/
        public int GetAdminCount(AdminDTO admin)
        {
            try
            {
                using (var dbctx = new StudentResultManagementSystemProjectEntities1())
                {

                    var count = dbctx.Admins.Where(o => o.EmailId == admin.EmailId).Count();
                    return count;
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("admin",ex);
            }
        }

        /*Query to display Admins*/
        public List<AdminDTO> DisplayAllAdmins(AdminDTO admin)
        {
            try
            {
                using (var dbctx = new StudentResultManagementSystemProjectEntities1())
                {
                    var result = dbctx.Admins.Select(o => new AdminDTO
                    {

                     AdminId=o.AdminId,
                     Name=o.Name,
                     EmailId=o.EmailId,
                     

                    }).ToList();
                    return result;
                }
            }
            catch(Exception ex)
            {
                throw new Exception("admin",ex);
            }
        }

        /*Query to display Admin details for edit operation*/
        public AdminDTO AdminInfo(int id)
        {
            using (var dbctx = new StudentResultManagementSystemProjectEntities1())
            {
                var record = dbctx.Admins.Where(o => o.AdminId == id).Select(o => new AdminDTO
                {
                  AdminId=o.AdminId,
                  Name=o.Name,
                  EmailId=o.EmailId,
                  Password=o.Password
           

                }).SingleOrDefault();
                return record;

            }

        }

        /*Query to Update Admin Data*/
        public string UpdateAdminPassword(AdminDTO admin)
        {
           
            try
            {

                using (var dbctx = new StudentResultManagementSystemProjectEntities1())
                {
                    var result = dbctx.Admins.Where(o => o.AdminId == admin.AdminId).SingleOrDefault();
                    result.EmailId = admin.EmailId;
                    result.Password = admin.Password;
                    dbctx.SaveChanges();
                  
                }
            }
            catch (Exception ex)
            {
                throw new Exception("admin",ex);
            }

            return "success";
        }

        /*Query for Login*/
        public string LoginCredential(AdminDTO admin)
        {
            try
            {
                
                using (var dbctx = new StudentResultManagementSystemProjectEntities1())
                {
                    if (dbctx.Admins.Where(o => o.EmailId == admin.EmailId && o.Password == admin.Password).SingleOrDefault() != null)
                    {
                        
                        return "success";
                    }
                    else
                    {
                        return "failure";
                        throw new Exception();
                      
                    }

                }

            }
            catch (Exception ex)
            {
                throw new Exception("admin",ex);

            }
        }

        /*Query for returning the Password from database*/
        public string ForgotPassword(AdminDTO id)
        {
            try
            {
                using (var dbctx = new StudentResultManagementSystemProjectEntities1())
                {
                    var result = dbctx.Admins.Where(o => o.EmailId == id.EmailId).SingleOrDefault();
                    
                    if (result!= null)
                    {
                        return result.Password;
                    }
                    else
                    {
                        throw new Exception();
                    }
                }

            }
            catch (SqlException ex)
            {
                throw new Exception("id",ex);
            }
        }

        /*Query to fetch AdminId and Name for Session Purpose*/
        public AdminDTO GetAdminName(string id)
        {
            try

            {
                using (var dbctx = new StudentResultManagementSystemProjectEntities1())
                {
                    var result = dbctx.Admins.Where(o => o.EmailId == id).SingleOrDefault();
                    AdminDTO admin = new AdminDTO
                    {
                      AdminId = result.AdminId,
                      Name = result.Name
                    };
                    return admin;
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("id",ex);
            }
        }

        public List<StudentDetailDTO> SemesterList()
        {
            var dbctx = new StudentResultManagementSystemProjectEntities1();

            var result = dbctx.Students.Select(o => new StudentDetailDTO
            {
                SemesterNumber = (int)o.SemesterNumber
            }).Distinct();

            return result.ToList(); ;
        }

        /*Query for searching student by USN*/
        public List<StudentDetailDTO> SearchStudent(string student)
        {
            try
            {
                using (var dbctx = new StudentResultManagementSystemProjectEntities1())
                {

                    var result = from s in dbctx.Students
                                 join sd in dbctx.StudentDetails on s.UniversitySeatNumber equals sd.UniversitySeatNumber
                                 where(s.UniversitySeatNumber == student)
                                 //where (StartWith(s.UniversitySeatNumber == student))
                                 //where (s.UniversitySeatNumber.StartsWith(s.UniversitySeatNumber)==student.StartsWith(s.UniversitySeatNumber))
                                 select (new StudentDetailDTO
                                 {
                                     UniversitySeatNumber = sd.UniversitySeatNumber,
                                     FirstName = sd.FirstName,
                                     LastName = sd.LastName,
                                     EmailId = sd.EmailId,
                                     Address = sd.Address,
                                     ContactNumber = (long)sd.ContactNumber,
                                     OptionalContactNumber = (long)sd.OptionalContactNumber,
                                     DateOfBirth = (DateTime)sd.DateOfBirth,
                                     SemesterNumber = (int)s.SemesterNumber,


                                 });
                    return result.ToList();
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("student",ex);
            }
        }


        /*Query for searching student by Semester*/
        public List<StudentDetailDTO> SearchStudentSemester(int student)
        {
            try
            {
                using (var dbctx = new StudentResultManagementSystemProjectEntities1())
                {

                    var result = from s in dbctx.Students
                                 join sd in dbctx.StudentDetails on s.UniversitySeatNumber equals sd.UniversitySeatNumber
                                 where (s.SemesterNumber == student)
                                 select (new StudentDetailDTO
                                 {
                                     UniversitySeatNumber = sd.UniversitySeatNumber,
                                     FirstName = sd.FirstName,
                                     LastName = sd.LastName,
                                     EmailId = sd.EmailId,
                                     Address = sd.Address,
                                     ContactNumber = (long)sd.ContactNumber,
                                     OptionalContactNumber = (long)sd.OptionalContactNumber,
                                     DateOfBirth = (DateTime)sd.DateOfBirth,
                                     SemesterNumber = (int)s.SemesterNumber,


                                 });
                    return result.ToList();
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("student",ex);
            }
        }

        /*Query for Displaying Students*/
        public List<StudentDetailDTO> DisplayAllStudents(StudentDetailDTO student)
        {
            try
            {
                using (var dbctx = new StudentResultManagementSystemProjectEntities1())
                {
                    var result = from s in dbctx.Students
                                 join sd in dbctx.StudentDetails on s.UniversitySeatNumber equals sd.UniversitySeatNumber
                                 select (new StudentDetailDTO
                                 {
                                     UniversitySeatNumber = sd.UniversitySeatNumber,
                                     FirstName = sd.FirstName,
                                     LastName = sd.LastName,
                                     EmailId = sd.EmailId,
                                     Address = sd.Address,
                                     ContactNumber = (long)sd.ContactNumber,
                                     OptionalContactNumber = (long)sd.OptionalContactNumber,
                                     DateOfBirth = (DateTime)sd.DateOfBirth,
                                     SemesterNumber = (int)s.SemesterNumber,


                                 });
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("student", ex);
            }
        }

        /*Query for displaying top 3 students*/
        public List<ScoreDTO> DisplayAllScores(ScoreDTO score)
        {
            using (var dbctx = new StudentResultManagementSystemProjectEntities1())
            {
                var result = dbctx.TotalScores.OrderByDescending(o=>o.Total).Select(o => new ScoreDTO
                {
                  UniversitySeatNumber=o.UniversitySeatNumber,
                  Total=(int)o.Total
                }).Take(3);
                return result.ToList();
            }

        }


        public List<StudentResultDTO> ScoreInformation(StudentResultDTO student)
        {
            using (var dbctx = new StudentResultManagementSystemProjectEntities1())
            {
                var result = from s in dbctx.StudentResults
                             join sd in dbctx.Students on s.UniversitySeatNumber equals sd.UniversitySeatNumber

                             select (new StudentResultDTO
                             {
                                 AdminId = (int)s.AdminId,
                                 UniversitySeatNumber = sd.UniversitySeatNumber,
                                 SemesterNumber = (int)sd.SemesterNumber
                             });
                return result.Distinct().ToList();

            }
        }



        /*Query for fetching semesters in dropdown*/
        public List<StudentResultDTO> Result()
        {
            try
            {
                using (var dbctx = new StudentResultManagementSystemProjectEntities1())
                {
                    var student = dbctx.Students.Distinct().Select(o => new StudentResultDTO
                    {
                        SemesterNumber = (int)o.SemesterNumber
                    }).Distinct();
                    return student.ToList();
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(" ", ex);
            }
        }

        /*Query for displaying results*/
        public List<StudentResultDTO> FetchResult(StudentResultDTO student)
        {
            try
            {
                using (var dbctx = new StudentResultManagementSystemProjectEntities1())
                {
                    var result = dbctx.StudentResults.Where(o => o.UniversitySeatNumber == student.UniversitySeatNumber).Select(o => new StudentResultDTO
                    {

                        SubjectId = o.SubjectId,
                        Marks = (int)o.Marks,
                        Grade = o.Grade,
                        Result = o.Result

                    }).ToList();
                    return result;
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("student", ex);
            }
        }


        /*Query for branch and semester list*/
        public List<InsertScoreDTO> SemesterBranchList()
        {
            var dbctx = new StudentResultManagementSystemProjectEntities1();

            var branch = dbctx.Subjects.Select(o => new InsertScoreDTO
            {
                BranchId = o.BranchId,
                
            }).Distinct().ToList();
            var semester = dbctx.Subjects.Select(o => new InsertScoreDTO
            {
                SemesterNumber=(int)o.SemesterNumber

            }).Distinct().ToList();
            List<InsertScoreDTO> list = new List<InsertScoreDTO>(10);
            for (int i = 0; i < semester.Count; i++)
            {
                var rs = new InsertScoreDTO();
                rs.BranchId = branch[i].BranchId;
                rs.SemesterNumber = semester[i].SemesterNumber;
                list.Add(rs);
            }

            return list.ToList();
        }

        /*Query for selecting BranchId*/
        public List<InsertScoreDTO> SemesterBranchPost(InsertScoreDTO insert)
        {
            try
            {
                using (var dbctx = new StudentResultManagementSystemProjectEntities1())
                {
                    var result = dbctx.Subjects.Where(o => o.BranchId == insert.BranchId && o.SemesterNumber == insert.SemesterNumber).Select(o => new InsertScoreDTO
                    {
                        SubjectId = o.SubjectId,


                    }).ToList();
                    return result;
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("insert", ex);
            }
        }

        /*Query for Inserting*/
        public string InsertScore(List<string> insertvalues, string[] Grade, string[] Result, int Total)
        {

            try
            {
                using (var dbctx = new StudentResultManagementSystemProjectEntities1())
                {

                    StudentResult result = new StudentResult
                    {
                        AdminId=int.Parse(insertvalues[13]),
                        UniversitySeatNumber = insertvalues[0],
                        SubjectId = insertvalues[1],
                        Marks = int.Parse(insertvalues[8]),
                        Grade = Grade[0],
                        Result = Result[0]
                    };
                    dbctx.StudentResults.Add(result);

                    StudentResult result1 = new StudentResult
                    {
                       AdminId = int.Parse(insertvalues[13]),
                        UniversitySeatNumber = insertvalues[0],
                        SubjectId = insertvalues[2],
                        Marks = int.Parse(insertvalues[9]),
                        Grade = Grade[1],
                        Result = Result[1],
                    };
                    dbctx.StudentResults.Add(result1);
                    StudentResult result2 = new StudentResult
                    {
                       AdminId = int.Parse(insertvalues[13]),
                        UniversitySeatNumber = insertvalues[0],
                        SubjectId = insertvalues[3],
                        Marks = int.Parse(insertvalues[10]),
                        Grade = Grade[2],
                        Result = Result[2],
                    };
                    dbctx.StudentResults.Add(result2);
                    StudentResult result3 = new StudentResult
                    {
                       AdminId = int.Parse(insertvalues[13]),
                        UniversitySeatNumber = insertvalues[0],
                        SubjectId = insertvalues[4],
                        Marks = int.Parse(insertvalues[11]),
                        Grade = Grade[3],
                        Result = Result[3],
                    };
                    dbctx.StudentResults.Add(result3);
                    StudentResult result4 = new StudentResult
                    {
                      AdminId = int.Parse(insertvalues[13]),
                        UniversitySeatNumber = insertvalues[0],
                        SubjectId = insertvalues[5],
                        Marks = int.Parse(insertvalues[12]),
                        Grade = Grade[4],
                        Result = Result[4],
                    };
                    dbctx.StudentResults.Add(result4);
                    StudentResult result5 = new StudentResult
                    {
                        AdminId = int.Parse(insertvalues[13]),
                        UniversitySeatNumber = insertvalues[0],
                        SubjectId = insertvalues[6],
                        Marks = int.Parse(insertvalues[12]),
                        Grade = Grade[5],
                        Result = Result[5],
                    };
                    dbctx.StudentResults.Add(result5);
                    TotalScore score = new TotalScore
                    {
                        UniversitySeatNumber = insertvalues[0],
                        Total = Total
                    };
                    dbctx.TotalScores.Add(score);
                    dbctx.SaveChanges();


                }

            }
            catch (SqlException ex)
            {

                throw new Exception("", ex);
            }
            return "success";

        }

        /*Query for fetching update data*/
        public List<StudentResultDTO> StudentScoreInfo(string UniversitySeatNumber)
        {
            using (var dbctx = new StudentResultManagementSystemProjectEntities1())
            {
                var record = dbctx.StudentResults.Where(o => o.UniversitySeatNumber == UniversitySeatNumber).Select(o => new StudentResultDTO
                {
                    UniversitySeatNumber=o.UniversitySeatNumber,
                    SubjectId=o.SubjectId,
                    Marks=(int)o.Marks,
                    

                }).ToList();
                return record;

            }

        }

        /*query to post the updated data*/
        public void UpdateScore(StudentResultDTO scores,int Total,string[] Grade,string[] Result)
        {
            try
            {
                using (var dbctx = new StudentResultManagementSystemProjectEntities1())
                {
                    var record = dbctx.StudentResults.Where(o => o.UniversitySeatNumber == scores.UniversitySeatNumber).ToList();
                    record[0].AdminId = scores.AdminId;
                    record[0].Marks = scores.Marks1;
                    record[0].Grade = Grade[0];
                    record[0].Result = Result[0];
                    record[1].AdminId = scores.AdminId;
                    record[1].Marks = scores.Marks2;
                    record[1].Grade = Grade[1];
                    record[1].Result = Result[1];
                    record[2].AdminId = scores.AdminId;
                    record[2].Marks = scores.Marks3;
                    record[2].Grade = Grade[2];
                    record[2].Result = Result[2];
                    record[3].AdminId = scores.AdminId;
                    record[3].Marks = scores.Marks4;
                    record[3].Grade = Grade[3];
                    record[3].Result = Result[3];
                    record[4].AdminId = scores.AdminId;
                    record[4].Marks = scores.Marks5;
                    record[4].Grade = Grade[4];
                    record[4].Result = Result[4];
                    record[5].AdminId = scores.AdminId;
                    record[5].Marks = scores.Marks6;
                    record[5].Grade = Grade[5];
                    record[5].Result = Result[5];
                    var record1 = dbctx.TotalScores.Where(o => o.UniversitySeatNumber == scores.UniversitySeatNumber).ToList();
                    record1[0].Total = Total;
                    dbctx.SaveChanges();


                }



            }
            catch (SqlException e)
            {
                throw new Exception("", e);
            }


        }

        /*Query to Deleting Data*/
        public void DeleteAdmin(int id)
        {
            try
            {
                using (var dbctx = new StudentResultManagementSystemProjectEntities1())
                {
                    var record = dbctx.Admins.Where(o => o.AdminId == id).SingleOrDefault();
                    dbctx.Admins.Remove(record);
                    dbctx.SaveChanges();
                }
            }
            catch(Exception ex)
            {
                throw new Exception("id", ex);
            }
        }

        public int AdminCount()
        {
            try
            {
                using (var dbctx = new StudentResultManagementSystemProjectEntities1())
                {
                    var result = dbctx.Admins.Count();
                    return result;
                }
               
            }
            
            catch (Exception e)
            {
                return 0;
            }
        }
        public int StudentCount()
        {
            try
            {
                using (var dbctx = new StudentResultManagementSystemProjectEntities1())
                {
                    var result = dbctx.StudentDetails.Count();
                    return result;
                }

            }

            catch (Exception e)
            {
                return 0;
            }
        }
    }
}
