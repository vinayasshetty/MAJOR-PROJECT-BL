using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentResultManagement.DAL;
using StudentResultManagement.DTO;
namespace StudentResultManagement.BL
{
    public class ResultBusinessLogic
    {
        StudentResultDAL dal = new StudentResultDAL();
        public string AddAdminBL(AdminDTO ad)
        {

                var res = dal.GetAdminCount(ad);
                if (res == 1)
                {
                return "fail";
                }
                else
                {

                    dal.AddAdmin(ad);
                    return "success";
                }

            }
        public string GeneratePassword()
        {
            string PasswordLength = "8";
            string NewPassword = "";

            string allowedChars = "";
            allowedChars = "1,2,3,4,5,6,7,8,9,0";
            allowedChars += "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,";
            allowedChars +="@,$,!,*";
            allowedChars += "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z,";


            char[] sep = {
            ','
        };
            string[] arr = allowedChars.Split(sep);


            string IDString = "";
            string temp = "";

            Random rand = new Random();

            for (int i = 0; i < Convert.ToInt32(PasswordLength); i++)
            {
                temp = arr[rand.Next(0, arr.Length)];
                IDString += temp;
                NewPassword = IDString;

            }
            return NewPassword;
        }


        public List<AdminDTO> DisplayAllAdminsBL(AdminDTO admin)
        {
            StudentResultDAL dal= new StudentResultDAL();
            return dal.DisplayAllAdmins(admin);
        }

        
        public AdminDTO AdminInfoBL(int id)
        {
            StudentResultDAL dal = new StudentResultDAL();
            return dal.AdminInfo(id);
        }
        public string UpdateAdminPasswordBL(AdminDTO admin)
        {
            StudentResultDAL dal = new StudentResultDAL();
            return dal.UpdateAdminPassword(admin);
        }

        public string ForgetPasswordBL(AdminDTO id)
        {
            StudentResultDAL dal = new StudentResultDAL();
            return dal.ForgotPassword(id);
        }

        public string AdminBL(AdminDTO admin)
        {
            StudentResultDAL dal = new StudentResultDAL();
            return dal.LoginCredential(admin);

        }
        public AdminDTO GetAdminNameBL(string id)
        {
            StudentResultDAL dal = new StudentResultDAL();
            return dal.GetAdminName(id);
        }

        public List<StudentDetailDTO> SemesterListBL()
        {
            StudentResultDAL dal = new StudentResultDAL();
            return dal.SemesterList();
        }

        public List<StudentDetailDTO> SearchStudentBL(string student)
        {
            StudentResultDAL dal = new StudentResultDAL();
            return dal.SearchStudent(student);
        }


        public List<StudentDetailDTO> SearchStudentSemesterBL(int student)
        {
            StudentResultDAL dal = new StudentResultDAL();
            return dal.SearchStudentSemester(student);
        }
        public List<StudentDetailDTO> DisplayAllStudentsBL(StudentDetailDTO student)
        {
            StudentResultDAL dal = new StudentResultDAL();
            return dal.DisplayAllStudents(student);
        }

        public List<ScoreDTO> DisplayAllScoresBL(ScoreDTO score)
        {
            StudentResultDAL dal = new StudentResultDAL();
            return dal.DisplayAllScores(score);
        }

        public List<StudentResultDTO> ScoreInformationBL(StudentResultDTO student)
        {
            StudentResultDAL dal = new StudentResultDAL();
            return dal.ScoreInformation(student);
        }

        public List<StudentResultDTO> ResultBL()
        {
            StudentResultDAL dal = new StudentResultDAL();
            return dal.Result();
        }

        public List<StudentResultDTO> FetchResultBL(StudentResultDTO student)
        {
            StudentResultDAL dal = new StudentResultDAL();
            return dal.FetchResult(student);
        }


        public List<InsertScoreDTO> SemesterBranchListBL()
        {
            StudentResultDAL dal = new StudentResultDAL();
            return dal.SemesterBranchList();
        }

        public List<InsertScoreDTO> SemesterBranchPostBL(InsertScoreDTO insert)
        {
            StudentResultDAL dal = new StudentResultDAL();
            return dal.SemesterBranchPost(insert);
        }


        public string InsertScoreBL(List<string> insertvalues)
        {
            StudentResultDAL dal = new StudentResultDAL();
            var a = int.Parse(insertvalues[7]);
            var b = int.Parse(insertvalues[8]);
            var c = int.Parse(insertvalues[9]);
            var d = int.Parse(insertvalues[10]);
            var e = int.Parse(insertvalues[11]);
            var f = int.Parse(insertvalues[12]);

            var Total = a + b + c + d + e + f;
            int[] marks = { a, b, c, d, e, f };
            string[] Grade=new string[6];
            string[] Result = new string[6];
            for (int i = 0; i <= 5; i++)
            {
                if (marks[i] > 90 && marks[i] <= 100)
                {
                    Grade[i] = "S";
                    Result[i] = "Pass";
                }
                else if (marks[i] > 80 && marks[i] <= 90)
                {
                    Grade[i] = "A";
                    Result[i] = "Pass";
                }
                else if (marks[i] > 70 && marks[i] <= 80)
                {
                    Grade[i] = "B";
                    Result[i] = "Pass";
                }
                else if (marks[i] > 60 && marks[i] <= 70)
                {
                    Grade[i] = "C";
                    Result[i] = "Pass";
                }
                else if (marks[i] > 50 && marks[i] <= 60)
                {
                    Grade[i] = "D";
                    Result[i] = "Pass";
                }
                else if (marks[i] > 40 && marks[i] <= 50)
                {
                    Grade[i] = "E";
                    Result[i] = "Pass";
                }

                else
                {
                    Grade[i] = "F";
                    Result[i] = "Fail";
                }

            }

            return dal.InsertScore(insertvalues,Grade,Result,Total);
        }


        public List<StudentResultDTO> StudentScoreInfoBL(string UniversitySeatNumber)
        {
            StudentResultDAL dal = new StudentResultDAL();
            return dal.StudentScoreInfo(UniversitySeatNumber);
        }

        public void UpdateScoreBL(StudentResultDTO scores)
        {
            StudentResultDAL dal = new StudentResultDAL();
            var a=scores.Marks1 ;
            var b = scores.Marks2;
            var c = scores.Marks3;
            var d = scores.Marks4;
            var e = scores.Marks5;
            var f = scores.Marks6;
            int[] marks = { a, b, c, d, e, f };
            string[] Grade = new string[6];
            string[] Result = new string[6];
            var Total = a + b + c + d + e + f;
            for (int i = 0; i <= 5; i++)
            {
                if (marks[i] > 90 && marks[i] <= 100)
                {
                    Grade[i] = "S";
                    Result[i] = "Pass";
                }
                else if (marks[i] > 80 && marks[i] <= 90)
                {
                    Grade[i] = "A";
                    Result[i] = "Pass";
                }
                else if (marks[i] > 70 && marks[i] <= 80)
                {
                    Grade[i] = "B";
                    Result[i] = "Pass";
                }
                else if (marks[i] > 60 && marks[i] <= 70)
                {
                    Grade[i] = "C";
                    Result[i] = "Pass";
                }
                else if (marks[i] > 50 && marks[i] <= 60)
                {
                    Grade[i] = "D";
                    Result[i] = "Pass";
                }
                else if (marks[i] > 40 && marks[i] <= 50)
                {
                    Grade[i] = "E";
                    Result[i] = "Pass";
                }

                else
                {
                    Grade[i] = "F";
                    Result[i] = "Fail";
                }

            }
            dal.UpdateScore(scores,Total, Grade, Result);
        }

        public void DeleteAdminBL(int id)
        {
            StudentResultDAL dal = new StudentResultDAL();
            dal.DeleteAdmin(id);
        }

        public int AdminCountBL()
        {
            StudentResultDAL dal = new StudentResultDAL();
            return dal.AdminCount();
        }
        public int StudentCountBL()
        {
            StudentResultDAL dal = new StudentResultDAL();
            return dal.StudentCount();
        }
    }
}
