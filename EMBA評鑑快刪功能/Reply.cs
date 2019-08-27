using System;
using FISCA.UDT;

namespace EMBA評鑑快刪功能
{
    /// <summary>
    /// 教學評鑑做答
    /// </summary>
    [FISCA.UDT.TableName("ischool.emba.teaching_evaluation.reply")]
    public class Reply : ActiveRecord
    {
        /// <summary>
        /// 學生系統編號
        /// </summary>
        [Field(Field = "ref_student_id", Indexed = true)]
        public int StudentID { get; set; }

        /// <summary>
        /// 問卷樣版系統編號
        /// </summary>
        [Field(Field = "ref_survey_id", Indexed = true)]
        public int SurveyID { get; set; }

        /// <summary>
        /// 課程系統編號
        /// </summary>
        [Field(Field = "ref_course_id", Indexed = true)]
        public int CourseID { get; set; }

        /// <summary>
        /// 教師系統編號
        /// </summary>
        [Field(Field = "ref_teacher_id", Indexed = true)]
        public int TeacherID { get; set; }

        /// <summary>
        /// 評鑑做答狀態：0-->暫存、1-->送出
        /// </summary>
        [Field(Field = "status", Indexed = true)]
        public int Status { get; set; }

        /// <summary>
        /// 評鑑做答，格式如下：
        /// <Answers>
        ///     <Question QuestionID="">
        ///         <Answer CaseID="" Score="3">尚可</Answer>
        ///         <Answer CaseID="123" Score="5">很滿意</Answer>
        ///     </Question>
        /// </Answers>
        /// </summary>
        [Field(Field = "answer", Indexed = false)]
        public string Answer { get; set; }
        
        ///// <summary>
        ///// Log，格式如下：
        ///// <Log RespondentsID="" RespondentsType="STUDENT/TEACHER" FillTime="2012/12/22 15:34:31" HostName="EePC" HostAddress="140.13.21.32" />
        ///// </summary>
        //[Field(Field = "log", Indexed = false, Caption = "log")]
        //public string Log { get; set; }


        public string s_Status { get; set; }
        public string s_SchoolYear { get; set; }
        public string s_Semester { get; set; }
        public string s_Category { get; set; }
        public string s_Course { get; set; }
        public string s_Teacher { get; set; }

        /// <summary>
        /// 淺層複製物件
        /// </summary>
        /// <returns></returns>
        public Reply Clone()
        {
            return this.MemberwiseClone() as Reply;
        }
    }
}
