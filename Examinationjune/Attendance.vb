Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data
'Imports dbnewcls
Public Class Attendance
    Private cmd As dbnew = New dbnew()
    'assign subject all subjjet grid 1
    Dim s As SqlParameter()
    Public Class markfeed
        Public Studentid As Integer
        Public Courseid As Integer
        Public Sem As Integer
        Public classesid As Integer
        Public obtMarks As String
        Public Attendance As String
        Public Pratical As String
        Public viva As String
        Public Recd As String
        Public Asstotal As Integer
        Public Assignment As String
        Public AttObt As String
        Public remark As String
    End Class
    Public Shared Function SpecialAttendance(ByVal subjectid As Integer, _
                                           ByVal dated As String, _
                                          ByVal Userid As Integer, _
                                          ByVal AttMax As Integer, _
                                          ByVal StuMarks As System.Collections.Generic.List(Of markfeed)) As Integer

        Dim cmd As New SqlCommand With {.Connection = db.Con}
        cmd.Parameters.AddWithValue("Sessionid", 20182019) 'Hari.DataLayer.Fee.GetCorrentSession)

        'cmd.Parameters.AddWithValue("CourseID", CourseID)
        'cmd.Parameters.AddWithValue("Sem", Sem)
        'cmd.Parameters.AddWithValue("Classesid", classesid)
        cmd.Parameters.AddWithValue("Dated", dated)
        cmd.Parameters.AddWithValue("subjectid", SubjectID)

        cmd.Parameters.AddWithValue("Studentid", DBNull.Value)
        cmd.Parameters.AddWithValue("CourseID", DBNull.Value)
        cmd.Parameters.AddWithValue("Sem", DBNull.Value)
        cmd.Parameters.AddWithValue("Classesid", DBNull.Value)
        cmd.Parameters.AddWithValue("Userid", 1197)
        cmd.Parameters.AddWithValue("AttMax", AttMax)
        cmd.Parameters.AddWithValue("AttObt", DBNull.Value)
        cmd.Parameters.AddWithValue("Attendance", DBNull.Value)
        db.UpdateParameter(cmd.Parameters)

        'cmd.CommandText = "DELETE FROM specialAttendance " & _
        '            "WHERE     (CourseID = @CourseID) and (Sem=@Sem) AND   (UserID = @UserID) and (Subjectid=Subjectid) and classesid=@classesid and dated=dated "
        'cmd.ExecuteNonQuery()



        'cmd.CommandText = "INSERT INTO specialAttendance (StudentID, CourseID, Sem,classesid,dated,subjectid, AttMax, AttObt, UserID) " & _
        '                    "VALUES     (@StudentID,@CourseID,@Sem,@classesid,@dated,@subjectid,@AttMax,@AttObt,@UserID)"

        For Each Item As markfeed In StuMarks
            If Not Item.AttObt = "" Then


                cmd.Parameters("StudentID").Value = Item.Studentid
                cmd.Parameters("Courseid").Value = Item.Courseid
                cmd.Parameters("sem").Value = Item.Sem
                cmd.Parameters("classesid").Value = Item.classesid

                cmd.Parameters("AttObt").Value = IIf(Item.AttObt = "", DBNull.Value, Item.AttObt)
                'cmd.Parameters("Attendance").Value = Item.Attendance

                cmd.CommandText = "DELETE FROM specialAttendance " & _
                    "WHERE     (Studentid = @Studentid) and (Sem=@Sem) and subjectid =@subjectid "
                cmd.ExecuteNonQuery()


                'cmd.CommandText = "INSERT INTO specialAttendance (StudentID, CourseID, Sem,classesid,dated,subjectid, AttMax, AttObt, UserID) " & _
                '           "VALUES     (@StudentID,@CourseID,@Sem,@classesid,@dated,@subjectid,@AttMax,@AttObt,@UserID)"
                cmd.CommandText = "INSERT INTO specialAttendance (StudentID, CourseID, Sem,classesid,subjectid, AttMax, AttObt, UserID) " & _
                          "VALUES     (@StudentID,@CourseID,@Sem,@classesid,@subjectid,@AttMax,@AttObt,@UserID)"

                cmd.ExecuteNonQuery()
            End If
        Next
        Return Nothing
    End Function
    Public Shared Function SpecialAttendancedelete(ByVal subjectid As Integer, _
                                           ByVal dated As String, _
                                          ByVal Userid As Integer, _
                                          ByVal AttMax As Integer, _
                                          ByVal StuMarks As System.Collections.Generic.List(Of markfeed)) As Integer

        Dim cmd As New SqlCommand With {.Connection = db.Con}
        cmd.Parameters.AddWithValue("Sessionid", 20182019) 'Hari.DataLayer.Fee.GetCorrentSession)
        'cmd.Parameters.AddWithValue("CourseID", CourseID)
        'cmd.Parameters.AddWithValue("Sem", Sem)
        'cmd.Parameters.AddWithValue("Classesid", classesid)
        cmd.Parameters.AddWithValue("Dated", dated)
        cmd.Parameters.AddWithValue("subjectid", subjectid)

        cmd.Parameters.AddWithValue("Studentid", DBNull.Value)
        cmd.Parameters.AddWithValue("CourseID", DBNull.Value)
        cmd.Parameters.AddWithValue("Sem", DBNull.Value)
        cmd.Parameters.AddWithValue("Classesid", DBNull.Value)
        cmd.Parameters.AddWithValue("Userid", 1197)
        cmd.Parameters.AddWithValue("AttMax", AttMax)
        cmd.Parameters.AddWithValue("AttObt", DBNull.Value)
        cmd.Parameters.AddWithValue("Attendance", DBNull.Value)
        db.UpdateParameter(cmd.Parameters)
        For Each Item As markfeed In StuMarks
            If Not Item.AttObt = "" Then
                cmd.Parameters("StudentID").Value = Item.Studentid
                cmd.Parameters("Courseid").Value = Item.Courseid
                cmd.Parameters("sem").Value = Item.Sem
                cmd.Parameters("classesid").Value = Item.classesid

                cmd.Parameters("AttObt").Value = IIf(Item.AttObt = "", DBNull.Value, Item.AttObt)
                'cmd.Parameters("Attendance").Value = Item.Attendance

                cmd.CommandText = "DELETE FROM specialAttendance " & _
                    "WHERE     (Studentid = @Studentid) and (Sem=@Sem) AND  (Subjectid=@Subjectid)  "
                cmd.ExecuteNonQuery()

                'cmd.CommandText = "INSERT INTO specialAttendance (StudentID, CourseID, Sem,classesid,dated,subjectid, AttMax, AttObt, UserID) " & _
                '           "VALUES     (@StudentID,@CourseID,@Sem,@classesid,@dated,@subjectid,@AttMax,@AttObt,@UserID)"

                ' cmd.ExecuteNonQuery()
            End If
        Next
        Return Nothing
    End Function
    Public Sub assingsuballsub(ByVal grd As GridView, ByVal collegeid As Integer, ByVal sessionid As Integer)
        s = {New SqlParameter("@q", "allsub"), New SqlParameter("@cid", collegeid), New SqlParameter("@sessionid", sessionid)}
        cmd.FillGrd(grd, cmd.getDataTable("[dbo].[Att_assignsub]", s, "tbl"))
    End Sub
    Public Sub assingsubcoursesub(ByVal grd As GridView, ByVal sessionid As Integer, ByVal evenodd As Integer, ByVal courseid As Integer, ByVal sem As Integer, ByVal classesid As Integer)
        Dim s() As SqlParameter = {New SqlParameter("@q", "subcourse"), New SqlParameter("@sessionid", sessionid), New SqlParameter("@evenodd", evenodd), New SqlParameter("@cid", courseid), New SqlParameter("@sem", sem), New SqlParameter("@cls", classesid)}
        cmd.FillGrd(grd, cmd.getDataTable("[dbo].[Att_assignsub]", s, "tbl"))
    End Sub
    Public Sub assingsubcoursesub_facultyloack(ByVal grd As GridView, ByVal sessionid As Integer, ByVal evenodd As Integer, ByVal courseid As Integer, ByVal sem As Integer, ByVal classesid As Integer)
        Dim s() As SqlParameter = {New SqlParameter("@q", "subject_FacultyLock"), New SqlParameter("@sessionid", sessionid), New SqlParameter("@evenodd", evenodd), New SqlParameter("@cid", courseid), New SqlParameter("@sem", sem), New SqlParameter("@cls", classesid)}
        cmd.FillGrd(grd, cmd.getDataTable("[dbo].[Att_assignsub]", s, "tbl"))
    End Sub
    Public Sub assingsubcoursesub_facultyloack_new(ByVal grd As GridView, ByVal id As Integer)
        Dim s() As SqlParameter = {New SqlParameter("@q", "subject_FacultyLock_new"), New SqlParameter("@uid", id)}
        cmd.FillGrd(grd, cmd.getDataTable("[dbo].[Att_assignsub]", s, "tbl"))
    End Sub
    Public Sub assingsubcoursesub_new(ByVal grd As GridView, ByVal sessionid As Integer, ByVal evenodd As Integer, ByVal courseid As Integer, ByVal sem As Integer, ByVal classesid As Integer)
        Dim s() As SqlParameter = {New SqlParameter("@q", "subcourse_new_Subject"), New SqlParameter("@batchid", sessionid), New SqlParameter("@evenodd", evenodd), New SqlParameter("@cid", courseid), New SqlParameter("@sem", sem), New SqlParameter("@cls", classesid)}
        cmd.FillGrd(grd, cmd.getDataTable("[dbo].[Att_assignsub]", s, "tbl"))
    End Sub
    Public Sub subjectmappingsectionwise(ByVal grd As GridView, ByVal sessionid As Integer, ByVal evenodd As Integer, ByVal courseid As Integer, ByVal sem As Integer, ByVal classesid As Integer)
        Dim s() As SqlParameter = {New SqlParameter("@q", "subjectmappingsectionwise"), New SqlParameter("@ayid", sessionid), New SqlParameter("@evenodd", evenodd), New SqlParameter("@cid", courseid), New SqlParameter("@sem", sem), New SqlParameter("@cls", classesid)}
        cmd.FillGrd(grd, cmd.getDataTable("[dbo].[Att_assignsub]", s, "tbl"))
    End Sub

    Public Sub subjectmappingsemwise(ByVal grd As GridView, ByVal sessionid As Integer, ByVal evenodd As Integer, ByVal courseid As Integer, ByVal sem As Integer, ByVal classesid As Integer)
        Dim s() As SqlParameter = {New SqlParameter("@q", "subjectmappingsemwise"), New SqlParameter("@ayid", sessionid), New SqlParameter("@evenodd", evenodd), New SqlParameter("@cid", courseid), New SqlParameter("@sem", sem), New SqlParameter("@cls", classesid)}
        cmd.FillGrd(grd, cmd.getDataTable("[dbo].[Att_assignsub]", s, "tbl"))
    End Sub

    Public Sub assingsubelective_new(ByVal grd As GridView, ByVal sessionid As Integer, ByVal evenodd As Integer, ByVal courseid As Integer, ByVal sem As Integer, ByVal classesid As Integer)
        Dim s() As SqlParameter = {New SqlParameter("@q", "subtype_new_Subject"), New SqlParameter("@batchid", sessionid), New SqlParameter("@evenodd", evenodd), New SqlParameter("@cid", courseid), New SqlParameter("@sem", sem), New SqlParameter("@cls", classesid)}
        cmd.FillGrd(grd, cmd.getDataTable("[dbo].[Att_assignsub]", s, "tbl"))
    End Sub

    Public Sub assingsubelective(ByVal grd As GridView, ByVal sessionid As Integer, ByVal evenodd As Integer, ByVal courseid As Integer, ByVal sem As Integer, ByVal classesid As Integer)
        Dim s() As SqlParameter = {New SqlParameter("@q", "subtype"), New SqlParameter("@batchid", sessionid), New SqlParameter("@evenodd", evenodd), New SqlParameter("@cid", courseid), New SqlParameter("@sem", sem), New SqlParameter("@cls", classesid)}
        cmd.FillGrd(grd, cmd.getDataTable("[dbo].[Att_assignsub]", s, "tbl"))
    End Sub
    Public Function assingsubgetteacher(ByVal collegeid As Integer, Optional ByVal deptid As Integer = 0) As DataTable
        s = {New SqlParameter("@q", "emp"), New SqlParameter("@cid", IIf(collegeid = 0, DBNull.Value, collegeid)), New SqlParameter("@TeachingStaff", 1), New SqlParameter("@deptid", IIf(deptid = 0, DBNull.Value, deptid))}
        Return cmd.getDataTable("[dbo].[Att_Fill]", s, "tbl")
    End Function
    '---------------------------assig subject close

    Public Function daytimetable1(ByVal dt As Date, ByVal teacherid As Integer, ByVal session As Integer, ByVal evenodd As Integer, ByVal classesid As Integer) As String
        Dim aa As SqlParameter() = {New SqlParameter("@D", dt), New SqlParameter("@t", teacherid), New SqlParameter("@s", session), New SqlParameter("@e", evenodd), New SqlParameter("@classesid", classesid)}
        Return Replace(Replace(cmd.execScaler("[dbo].[AttTT]", "Procedure", aa), "&lt;", "<"), "&gt;", ">")
    End Function

    Public Function GetStudentatt1(ByVal dt As Date, ByVal a As Integer, ByVal Sessionid As String, ByVal Courseid As String, ByVal classesid As String, ByVal Sem As String) As String
        Dim aa As SqlParameter() = {New SqlParameter("@dt", dt), New SqlParameter("@id", CInt(a)), New SqlParameter("@Sessionid", Sessionid), New SqlParameter("@COurseid", Courseid), New SqlParameter("@Classesid", classesid), New SqlParameter("@sem", Sem)}
        '   Return Bind(cmd.getDataTable("dbo.getattstu3", aa, "dt1"))
        Dim search() As String = {"<th>"}
        Return Replace(generate(Replace(Replace(Replace(cmd.execScaler("dbo.Attgetstu", "Procedure", aa), "&lt;", "<"), "&gt;", ">"), "<th>", "®œ")), "class=""tbl""", "id=""myTable""  class=""tbl""  ")
    End Function

    Public Function GetStudentattcoursewise(ByVal courseid As Integer, ByVal sem As Integer, ByVal secid As Integer, ByVal type As String, ByVal dated As Date, ByVal Period As Integer, ByVal Subject As Integer, ByVal tearcher As Integer, ByVal teachtype As String, ByVal grp As String, ByVal Elect As String, ByVal Combine As String) As String
        '  Dim aa As SqlParameter() = {New SqlParameter("@dt", dt), New SqlParameter("@id", CInt(a))}

        Dim aa As SqlParameter() = {New SqlParameter("@course", courseid), New SqlParameter("@sem", CInt(sem)), New SqlParameter("@classes", secid), New SqlParameter("@type", type), New SqlParameter("@Dt", CDate(dated.ToString).ToString("MM/dd/yyyy")), New SqlParameter("@prd", CInt(Period)), New SqlParameter("@sub", Subject), New SqlParameter("@t", tearcher), New SqlParameter("@ttype", teachtype), New SqlParameter("@grp", cmd.IsNull(grp, DBNull.Value)), New SqlParameter("@ecode", Elect), New SqlParameter("@ccode", Combine)}

        Dim OldWords() As String = {"&nbsp;", "&amp;", "&quot;", "&lt;", _
"&gt;", "&reg;", "&copy;", "&bull;", "&trade;"}
        Dim NewWords() As String = {" ", "&", """", "<", ">", "Â®", "Â©", "â€¢", "â„¢"}
        '   Dim a As String = cmd.execScaler("[dbo].[Leave_Approved]", "Procedure", s)
        Dim sbHTML As StringBuilder = New StringBuilder(cmd.execScaler("[dbo].[Att_GetstuII]", "Procedure", aa))
        Dim i As Integer = 0
        For i = 0 To OldWords.Length - 1
            Dim b As String = "aa"
            sbHTML.Replace(OldWords(i), NewWords(i))
        Next i

        Return Replace(generate(Replace(sbHTML.ToString, "<th>", "®œ")), "class=""tbl""", "id=""myTable""  class=""tbl""  ")
        ' Return Bind(cmd.getDataTable("dbo.getattstu3", aa, "dt1"))

        ' Dim search() As String = {"<th>"}
        '  Return Replace(generate(Replace(Replace(Replace(cmd.execScaler("dbo.Attgetstu", "Procedure", aa), "&lt;", "<"), "&gt;", ">"), "<th>", "®œ")), "class=""tbl""", "id=""myTable""  class=""tbl""  ")
    End Function

    Public Function GetStudentattcoursewise_insert(ByVal courseid As Integer, ByVal sem As Integer, ByVal secid As Integer, ByVal type As String, ByVal dated As Date, ByVal Period As Integer, ByVal Subject As Integer, ByVal tearcher As Integer, ByVal teachtype As String, ByVal grp As String, ByVal Elect As String, ByVal Combine As String, ByVal dt As DataTable, ByVal room As String) As String
        '  Dim aa As SqlParameter() = {New SqlParameter("@dt", dt), New SqlParameter("@id", CInt(a))}

        Dim aa As SqlParameter() = {New SqlParameter("@course", courseid), New SqlParameter("@sem", CInt(sem)), New SqlParameter("@classes", secid), New SqlParameter("@type", type), New SqlParameter("@Dt", CDate(dated.ToString).ToString("MM/dd/yyyy")), New SqlParameter("@prd", CInt(Period)), New SqlParameter("@sub", Subject), New SqlParameter("@t", tearcher), New SqlParameter("@ttype", teachtype), New SqlParameter("@grp", grp), New SqlParameter("@ecode", Elect), New SqlParameter("@ccode", Combine), New SqlParameter("@tblCustomers", dt), New SqlParameter("@room", room)}
        Return cmd.execStoredProcudureretstr("Att_stuinsII", aa)
        ' Return Bind(cmd.getDataTable("dbo.getattstu3", aa, "dt1"))

        ' Dim search() As String = {"<th>"}
        '  Return Replace(generate(Replace(Replace(Replace(cmd.execScaler("dbo.Attgetstu", "Procedure", aa), "&lt;", "<"), "&gt;", ">"), "<th>", "®œ")), "class=""tbl""", "id=""myTable""  class=""tbl""  ")
    End Function

    Public Function Examoutcomemap(ByVal courseid As Integer, ByVal sem As Integer, ByVal classesid As Integer, ByVal sessionid As Integer, ByVal subjectid As Integer) As String
        Dim aa As SqlParameter() = {New SqlParameter("@Q", "GET"), New SqlParameter("@courseid", courseid), New SqlParameter("@semid", sem), New SqlParameter("@classesid", classesid), New SqlParameter("@subjectid", subjectid), New SqlParameter("@sessionid", sessionid)}
        Return ExportDatatableToHtml(cmd.getDataTable("[dbo].[Exem_ExamOutComeMap]", aa, "dt1"))
        'Dim search() As String = {"<th>"}
        'Return Replace(generate(Replace(Replace(Replace(cmd.execScaler("[dbo].[Exem_ExamOutComeMap]", "Procedure", aa), "&lt;", "<"), "&gt;", ">"), "<th>", "®œ")), "class=""tbl""", "id=""myTable""  class=""tbl""  ")
    End Function
 
    Public Function insert_Examoutcomemap(ByVal courseid As Integer, ByVal sem As Integer, ByVal classesid As Integer, ByVal sessionid As Integer, ByVal subjectid As Integer, ByVal teacherid As Integer, ByVal userid As Integer, ByVal dt As DataTable) As String
        Dim aa As SqlParameter() = {New SqlParameter("@Q", "INSERT"), New SqlParameter("@courseid", courseid), New SqlParameter("@semid", sem), New SqlParameter("@classesid", classesid), New SqlParameter("@subjectid", subjectid), New SqlParameter("@sessionid", sessionid), _
                                    New SqlParameter("@userid", userid), New SqlParameter("@teacherid", teacherid), _
                                     New SqlParameter("@tblCustomers", dt)}

        Return cmd.execStoredProcudureretstr("[dbo].[Exem_ExamOutComeMap]", aa)
    End Function
    '-----question paper..
    'exam name list '


    Public Sub Exam_get_PaperName(ByVal ddl As DropDownList, ByVal courseid As Integer, ByVal sem As Integer, ByVal classesid As Integer, ByVal sessionid As Integer, ByVal subjectid As Integer)
        Dim aa As SqlParameter() = {New SqlParameter("@Q", "Get_Exam_Name"), New SqlParameter("@courseid", courseid), New SqlParameter("@semid", sem), New SqlParameter("@classesid", classesid), New SqlParameter("@subjectid", subjectid), New SqlParameter("@sessionid", sessionid)}
        ' Return cmd.getDataTable("[dbo].[Exem_questionpaper]", aa, "dt1")

        ' Dim s() As SqlParameter = {New SqlParameter("@q", "emp_role_wise_list"), New SqlParameter("@uid", HttpContext.Current.Request.QueryString("u")), New SqlParameter("@roleid", HttpContext.Current.Request.QueryString("r"))}
        cmd.FillDropdown(ddl, "[dbo].[Exem_questionpaper]", aa)
        'Dim search() As String = {"<th>"}
        'Return Replace(generate(Replace(Replace(Replace(cmd.execScaler("[dbo].[Exem_ExamOutComeMap]", "Procedure", aa), "&lt;", "<"), "&gt;", ">"), "<th>", "®œ")), "class=""tbl""", "id=""myTable""  class=""tbl""  ")
    End Sub

    Public Sub Exam_get_PaperName_directmarksenter(ByVal ddl As DropDownList, ByVal courseid As Integer, ByVal sem As Integer, ByVal classesid As Integer, ByVal sessionid As Integer, ByVal subjectid As Integer)
        Dim aa As SqlParameter() = {New SqlParameter("@Q", "Get_Exam_Name_marksenter"), New SqlParameter("@courseid", courseid), New SqlParameter("@semid", sem), New SqlParameter("@classesid", classesid), New SqlParameter("@subjectid", subjectid), New SqlParameter("@sessionid", sessionid)}
        ' Return cmd.getDataTable("[dbo].[Exem_questionpaper]", aa, "dt1")

        ' Dim s() As SqlParameter = {New SqlParameter("@q", "emp_role_wise_list"), New SqlParameter("@uid", HttpContext.Current.Request.QueryString("u")), New SqlParameter("@roleid", HttpContext.Current.Request.QueryString("r"))}
        cmd.FillDropdown(ddl, "[dbo].[Exem_questionpaper]", aa)
        'Dim search() As String = {"<th>"}
        'Return Replace(generate(Replace(Replace(Replace(cmd.execScaler("[dbo].[Exem_ExamOutComeMap]", "Procedure", aa), "&lt;", "<"), "&gt;", ">"), "<th>", "®œ")), "class=""tbl""", "id=""myTable""  class=""tbl""  ")
    End Sub
    'Public Function exam_get_student_marks_direct(ByVal q As String, ByVal uid As String) As String
    '    Dim aa As SqlParameter() = {New SqlParameter("@Q", q), New SqlParameter("@Courseid", uid)}
    '    '   Return ExportDatatableToHtml(cmd.getDataTable("[dbo].[Exem_QuestionPaper]", aa, "dt1"))
    '    Return Replace(generate(Replace(Replace(Replace(cmd.execScaler("[dbo].[Exem_QuestionPaper]", "Procedure", aa), "&lt;", "<"), "&gt;", ">"), "<th>", "®œ")), "class=""tbl""", "id=""myTable""  class=""tbl""  ")
    '    'Dim search() As String = {"<th>"}
    '    'Return Replace(generate(Replace(Replace(Replace(cmd.execScaler("[dbo].[Exem_ExamOutComeMap]", "Procedure", aa), "&lt;", "<"), "&gt;", ">"), "<th>", "®œ")), "class=""tbl""", "id=""myTable""  class=""tbl""  ")
    'End Function

    Public Function exam_get_student_marks_direct(ByVal q As String, ByVal uid As String) As DataTable
        Dim aa As SqlParameter() = {New SqlParameter("@Q", q), New SqlParameter("@Courseid", uid)}
        '   Return ExportDatatableToHtml(cmd.getDataTable("[dbo].[Exem_QuestionPaper]", aa, "dt1"))
        Return cmd.getDataTable("[dbo].[Exem_QuestionPaper]", aa, "dt1")
        'Dim search() As String = {"<th>"}
        'Return Replace(generate(Replace(Replace(Replace(cmd.execScaler("[dbo].[Exem_ExamOutComeMap]", "Procedure", aa), "&lt;", "<"), "&gt;", ">"), "<th>", "®œ")), "class=""tbl""", "id=""myTable""  class=""tbl""  ")
    End Function

    Public Function Exam_get_cos(ByVal q As String, ByVal courseid As Integer, ByVal sem As Integer, ByVal classesid As Integer, ByVal sessionid As Integer, ByVal subjectid As Integer) As DataTable
        Dim aa As SqlParameter() = {New SqlParameter("@Q", q), New SqlParameter("@courseid", courseid), New SqlParameter("@semid", sem), New SqlParameter("@classesid", classesid), New SqlParameter("@subjectid", subjectid), New SqlParameter("@sessionid", sessionid)}
        Return cmd.getDataTable("[dbo].[Exem_questionpaper]", aa, "dt1")
        'Dim search() As String = {"<th>"}
        'Return Replace(generate(Replace(Replace(Replace(cmd.execScaler("[dbo].[Exem_ExamOutComeMap]", "Procedure", aa), "&lt;", "<"), "&gt;", ">"), "<th>", "®œ")), "class=""tbl""", "id=""myTable""  class=""tbl""  ")
    End Function
   
    Public Sub Exam_get_Qpaper_grd(ByVal grd As GridView, ByVal q As String, ByVal sessionid As Integer, ByVal evenodd As Integer, ByVal courseid As Integer, ByVal sem As Integer, ByVal classesid As Integer, ByVal paperid As Integer, ByVal examid As Integer, ByVal subjectid As Integer)
        Dim s() As SqlParameter = {New SqlParameter("@q", "Exam_Qpaper_grd"), New SqlParameter("@sessionid", sessionid), New SqlParameter("@evenodd", evenodd), New SqlParameter("@courseid", courseid), New SqlParameter("@semid", sem), New SqlParameter("@classesid", classesid), New SqlParameter("@paperid", paperid), New SqlParameter("@examid", examid), New SqlParameter("@subjectid", subjectid)}
        cmd.FillGrd(grd, cmd.getDataTable("[dbo].[Exem_questionpaper]", s, "tbl"))
    End Sub

    Public Function insert_Exam_paper(ByVal Threshold As Integer, ByVal Paperid As Integer, ByVal examid As Integer, ByVal Subjectid As Integer, ByVal Courseid As Integer, ByVal sem As Integer, ByVal Classesid As Integer, ByVal Teacherid As Integer, ByVal Sessionid As Integer, ByVal Evenodd As Integer, ByVal userid As Integer, ByVal dt As DataTable) As String
        Dim aa As SqlParameter() = {New SqlParameter("@Q", "INSERT_Qes_paper"), New SqlParameter("@Threshold", Threshold), New SqlParameter("@Paperid", Paperid), New SqlParameter("@examid", examid), New SqlParameter("@Subjectid", Subjectid), New SqlParameter("@courseid", Courseid), _
                                    New SqlParameter("@semid", sem), New SqlParameter("@classesid", Classesid), New SqlParameter("@Teacherid", Teacherid), New SqlParameter("@sessionid", Sessionid), _
                                    New SqlParameter("@userid", userid), New SqlParameter("@evenodd", Evenodd), _
                                     New SqlParameter("@tblCustomers", dt)}
        Return cmd.execStoredProcudureretstr("[dbo].[Exem_questionpaper]", aa)
    End Function

    Public Function insert_Exam_cos_marks(ByVal MaxMarks As Integer, ByVal userid As Integer, ByVal Teacherid As Integer, ByVal dt As DataTable, ByVal examid As Integer, ByVal paperid As Integer, ByVal courseid As Integer, ByVal sem As Integer, ByVal classesid As Integer, ByVal sessionid As Integer, ByVal subjectid As Integer) As String
        Dim aa As SqlParameter() = {New SqlParameter("@Q", "insert_student_marks_cos"), New SqlParameter("@userid", userid), New SqlParameter("@teacherid", Teacherid), _
                                     New SqlParameter("@Courseid", courseid), New SqlParameter("@semid", sem), New SqlParameter("@classesid", classesid), New SqlParameter("@examid", examid), New SqlParameter("@paperid", paperid), New SqlParameter("@subjectid", subjectid), New SqlParameter("@sessionid", sessionid), _
                                     New SqlParameter("@tblCustomers", dt)}
        Return cmd.execStoredProcudureretstr("[dbo].[Exem_questionpaper]", aa)
    End Function
    Public Function insert_Exam_direct_marks(ByVal userid As Integer, ByVal examid As String, ByVal dt As DataTable) As String
        Dim aa As SqlParameter() = {New SqlParameter("@Q", "INSERT_Exam_Marks_Direct"), New SqlParameter("@userid", userid), New SqlParameter("@examid", examid), _
                                                 New SqlParameter("@tblCustomers", dt)}
        Return cmd.execStoredProcudureretstr("[dbo].[Exem_questionpaper]", aa)
    End Function
    Public Function insert_Exam_direct_marks_backpaper(ByVal userid As Integer, ByVal examid As String, ByVal dt As DataTable) As String
        Dim aa As SqlParameter() = {New SqlParameter("@Q", "INSERT_Exam_Marks_Direct_backpaper"), New SqlParameter("@userid", userid), New SqlParameter("@examid", examid), _
                                                 New SqlParameter("@tblCustomers", dt)}
        Return cmd.execStoredProcudureretstr("[dbo].[Exem_questionpaper]", aa)
    End Function
    '------------- exme marks enter cos
    Public Function exam_get_student_marks_cos(ByVal q As String, ByVal courseid As Integer, ByVal sem As Integer, ByVal classesid As Integer, ByVal sessionid As Integer, ByVal subjectid As Integer, ByVal paperid As Integer, ByVal examid As Integer) As String
        Dim aa As SqlParameter() = {New SqlParameter("@Q", q), New SqlParameter("@courseid", courseid), New SqlParameter("@semid", sem), New SqlParameter("@classesid", classesid), New SqlParameter("@subjectid", subjectid), New SqlParameter("@sessionid", sessionid), New SqlParameter("@paperid", paperid), New SqlParameter("@examid", examid)}
        Return ExportDatatableToHtml(cmd.getDataTable("[dbo].[Exem_QuestionPaper]", aa, "dt1"))
        'Dim search() As String = {"<th>"}
        'Return Replace(generate(Replace(Replace(Replace(cmd.execScaler("[dbo].[Exem_ExamOutComeMap]", "Procedure", aa), "&lt;", "<"), "&gt;", ">"), "<th>", "®œ")), "class=""tbl""", "id=""myTable""  class=""tbl""  ")
    End Function
    Public Function exam_get_student_marks_cos_excel_import(ByVal q As String, ByVal courseid As Integer, ByVal sem As Integer, ByVal classesid As Integer, ByVal sessionid As Integer, ByVal subjectid As Integer, ByVal paperid As Integer, ByVal examid As Integer, ByVal dt As DataTable) As String
        Dim aa As SqlParameter() = {New SqlParameter("@Q", q), New SqlParameter("@courseid", courseid), New SqlParameter("@semid", sem), New SqlParameter("@classesid", classesid), New SqlParameter("@subjectid", subjectid), New SqlParameter("@sessionid", sessionid), New SqlParameter("@paperid", paperid), New SqlParameter("@examid", examid), New SqlParameter("@tblCustomers", dt)}
        Return ExportDatatableToHtml(cmd.getDataTable("[dbo].[Exem_QuestionPaper]", aa, "dt1"))
        'Dim search() As String = {"<th>"}
        'Return Replace(generate(Replace(Replace(Replace(cmd.execScaler("[dbo].[Exem_ExamOutComeMap]", "Procedure", aa), "&lt;", "<"), "&gt;", ">"), "<th>", "®œ")), "class=""tbl""", "id=""myTable""  class=""tbl""  ")
    End Function

    Public Function exam_get_student_marks_cos_excelexport(ByVal q As String, ByVal courseid As Integer, ByVal sem As Integer, ByVal classesid As Integer, ByVal sessionid As Integer, ByVal subjectid As Integer, ByVal paperid As Integer, ByVal examid As Integer) As DataTable
        Dim aa As SqlParameter() = {New SqlParameter("@Q", q), New SqlParameter("@courseid", courseid), New SqlParameter("@semid", sem), New SqlParameter("@classesid", classesid), New SqlParameter("@subjectid", subjectid), New SqlParameter("@sessionid", sessionid), New SqlParameter("@paperid", paperid), New SqlParameter("@examid", examid)}
        Return cmd.getDataTable("[dbo].[Exem_QuestionPaper]", aa, "dt1")
        'Dim search() As String = {"<th>"}
        'Return Replace(generate(Replace(Replace(Replace(cmd.execScaler("[dbo].[Exem_ExamOutComeMap]", "Procedure", aa), "&lt;", "<"), "&gt;", ">"), "<th>", "®œ")), "class=""tbl""", "id=""myTable""  class=""tbl""  ")
    End Function

    Public Function exam_get_student_marks_cos_finalrpt(ByVal q As String, ByVal courseid As Integer, ByVal sem As Integer, ByVal classesid As Integer, ByVal sessionid As Integer, ByVal subjectid As Integer, ByVal paperid As Integer, ByVal examid As Integer) As String
        Dim aa As SqlParameter() = {New SqlParameter("@Q", q), New SqlParameter("@courseid", courseid), New SqlParameter("@semid", sem), New SqlParameter("@classesid", classesid), New SqlParameter("@subjectid", subjectid), New SqlParameter("@sessionid", sessionid), New SqlParameter("@paperid", paperid), New SqlParameter("@examid", examid)}
        Return ExportDatatableToHtml(cmd.getDataTable("[dbo].[Exem_outcome_final_rpt]", aa, "dt1"))
        'Dim search() As String = {"<th>"}
        'Return Replace(generate(Replace(Replace(Replace(cmd.execScaler("[dbo].[Exem_ExamOutComeMap]", "Procedure", aa), "&lt;", "<"), "&gt;", ">"), "<th>", "®œ")), "class=""tbl""", "id=""myTable""  class=""tbl""  ")
    End Function
    Public Function exam_get_student_marks_cos_Award_Sheet(ByVal courseid As Integer, ByVal sem As Integer, ByVal classesid As Integer, ByVal sessionid As Integer, ByVal subjectid As Integer) As String
        Dim aa As SqlParameter() = {New SqlParameter("@courseid", courseid), New SqlParameter("@sem", sem), New SqlParameter("@classesid", classesid), New SqlParameter("@subjectid", subjectid), New SqlParameter("@sessionid", sessionid)}
        Return ExportDatatableToHtml(cmd.getDataTable("[dbo].[examheadrpt_subjectwise]", aa, "dt1"))
        'Dim search() As String = {"<th>"}
        'Return Replace(generate(Replace(Replace(Replace(cmd.execScaler("[dbo].[Exem_ExamOutComeMap]", "Procedure", aa), "&lt;", "<"), "&gt;", ">"), "<th>", "®œ")), "class=""tbl""", "id=""myTable""  class=""tbl""  ")
    End Function
    ''----------------------------
    Public Function exam_get_student_marks_cos_view(ByVal courseid As Integer, ByVal sem As Integer, ByVal classesid As Integer, ByVal sessionid As Integer, ByVal subjectid As Integer, ByVal paperid As Integer, ByVal examid As Integer) As String
        Dim aa As SqlParameter() = {New SqlParameter("@Q", "view_student_marks_cos"), New SqlParameter("@courseid", courseid), New SqlParameter("@semid", sem), New SqlParameter("@classesid", classesid), New SqlParameter("@subjectid", subjectid), New SqlParameter("@sessionid", sessionid), New SqlParameter("@paperid", paperid), New SqlParameter("@examid", examid)}
        Return ExportDatatableToHtml(cmd.getDataTable("[dbo].[Exem_questionpaper]", aa, "dt1"))
        'Dim search() As String = {"<th>"}
        'Return Replace(generate(Replace(Replace(Replace(cmd.execScaler("[dbo].[Exem_ExamOutComeMap]", "Procedure", aa), "&lt;", "<"), "&gt;", ">"), "<th>", "®œ")), "class=""tbl""", "id=""myTable""  class=""tbl""  ")
    End Function
    'Public Function exam_get_student_marks_cos_view_studentwise(ByVal courseid As Integer, ByVal sem As Integer, ByVal classesid As Integer, ByVal sessionid As Integer, ByVal subjectid As Integer, ByVal paperid As Integer, ByVal examid As Integer) As String
    '    Dim aa As SqlParameter() = {New SqlParameter("@Q", "view_student_wise_marks_cos"), New SqlParameter("@courseid", courseid), New SqlParameter("@semid", sem), New SqlParameter("@classesid", classesid), New SqlParameter("@subjectid", subjectid), New SqlParameter("@sessionid", sessionid), New SqlParameter("@paperid", paperid), New SqlParameter("@examid", examid)}
    '    Return ExportDatatableToHtml(cmd.getDataTable("[dbo].[Exem_questionpaper]", aa, "dt1"))
    '    'Dim search() As String = {"<th>"}
    '    'Return Replace(generate(Replace(Replace(Replace(cmd.execScaler("[dbo].[Exem_ExamOutComeMap]", "Procedure", aa), "&lt;", "<"), "&gt;", ">"), "<th>", "®œ")), "class=""tbl""", "id=""myTable""  class=""tbl""  ")
    'End Function


    Public Function Exam_POS_map(ByVal courseid As Integer, ByVal sessionid As Integer) As String
        Dim aa As SqlParameter() = {New SqlParameter("@courseid", courseid), New SqlParameter("@sessionid", sessionid)}
        Return ExportDatatableToHtml(cmd.getDataTable("[dbo].[Exem_ExamPOsMap]", aa, "dt1"))
        'Dim search() As String = {"<th>"}
        'Return Replace(generate(Replace(Replace(Replace(cmd.execScaler("[dbo].[Exem_ExamOutComeMap]", "Procedure", aa), "&lt;", "<"), "&gt;", ">"), "<th>", "®œ")), "class=""tbl""", "id=""myTable""  class=""tbl""  ")
    End Function
    Public Function Exem_Outcomes_Sub_list(ByVal courseid As Integer, ByVal sessionid As Integer) As String
        Dim aa As SqlParameter() = {New SqlParameter("@courseid", courseid), New SqlParameter("@sessionid", sessionid)}
        Return ExportDatatableToHtml(cmd.getDataTable("[dbo].[Exem_Outcomes_Sub_list]", aa, "dt1"))
        'Dim search() As String = {"<th>"}
        'Return Replace(generate(Replace(Replace(Replace(cmd.execScaler("[dbo].[Exem_ExamOutComeMap]", "Procedure", aa), "&lt;", "<"), "&gt;", ">"), "<th>", "®œ")), "class=""tbl""", "id=""myTable""  class=""tbl""  ")
    End Function
    Public Function Exem_Outcomes_Sub_list_Poes(ByVal courseid As Integer, ByVal sessionid As Integer) As String
        Dim aa As SqlParameter() = {New SqlParameter("@courseid", courseid), New SqlParameter("@sessionid", sessionid)}
        Return ExportDatatableToHtml(cmd.getDataTable("[dbo].[Exem_Outcomes_Sub_list_Poes]", aa, "dt1"))
        'Dim search() As String = {"<th>"}
        'Return Replace(generate(Replace(Replace(Replace(cmd.execScaler("[dbo].[Exem_ExamOutComeMap]", "Procedure", aa), "&lt;", "<"), "&gt;", ">"), "<th>", "®œ")), "class=""tbl""", "id=""myTable""  class=""tbl""  ")
    End Function
    Public Sub Get_Exem_Name(ByVal ddl As DropDownList)
        's = {New SqlParameter("@q", "leavesetuser"), New SqlParameter("@cid", collegeid), New SqlParameter("@deptid", cmd.IsNull(dept, DBNull.Value))}
        'Return Replace(Replace(cmd.execScaler("[dbo].[Leave_Setlevel]", "Procedure", s), "&lt;", "<"), "&gt;", ">")
        Dim s() As SqlParameter = {New SqlParameter("@q", "get_Exam_Name")}
        cmd.FillDropdown(ddl, "[dbo].[exam_master]", s)
    End Sub
    ' exam marks Setting.. get max marks update
    'Public Sub Get_Exem_maxmarks(ByVal q As String, ByVal grd As GridView, ByVal courseid As String, ByVal sem As String, ByVal classesid As String, ByVal sessionid As String, ByVal paperid As String, ByVal examid As String, ByVal AYID As String)
    '    Dim aa As SqlParameter() = {New SqlParameter("@q", q), New SqlParameter("@courseid", courseid), New SqlParameter("@sem", sem), New SqlParameter("@Classesid", classesid), New SqlParameter("@sessionid", sessionid), New SqlParameter("@examcenterid", paperid), New SqlParameter("@AYID", AYID)}
    '    '  Return cmd.getDataTable("[dbo].[exam_master]", aa, "dd")
    '    cmd.FillGrd(grd, cmd.getDataTable("[dbo].[exam_master]", aa, "tbl"))
    'End Sub

    Public Sub Get_Exem_maxmarks(ByVal q As String, ByVal grd As GridView, ByVal courseid As Integer, ByVal sem As Integer, ByVal classesid As Integer, ByVal sessionid As Integer, ByVal paperid As Integer, ByVal examid As Integer)
        Dim aa As SqlParameter() = {New SqlParameter("@q", q), New SqlParameter("@courseid", courseid), New SqlParameter("@sem", sem), New SqlParameter("@Classesid", classesid), New SqlParameter("@sessionid", sessionid), New SqlParameter("@examcenterid", paperid)}
        '  Return cmd.getDataTable("[dbo].[exam_master]", aa, "dd")
        cmd.FillGrd(grd, cmd.getDataTable("[dbo].[exam_master]", aa, "tbl"))


    End Sub


    Public Function generate(ByVal alg As String) As String
        Dim algSplit As String() = alg.Split("®")
        For digit = 0 To algSplit.Length - 1
            Dim replacement As String = algSplit(digit).Replace("œ", "<th onclick='sortTable(" & digit - 1 & ",""myTable"")'>")
            algSplit(digit) = replacement
        Next
        Return String.Join("", algSplit)
        ' ??? .. you should return something here ????
    End Function
    Public Sub empemailsms(ByVal grd As GridView, ByVal collegeid As Integer, ByVal dept As Integer)
        s = {New SqlParameter("@q", "Emailemp"), New SqlParameter("@cid", collegeid), New SqlParameter("@deptid", cmd.IsNull(dept, DBNull.Value))}
        cmd.FillGrd(grd, cmd.getDataTable("[dbo].[Att_Fill]", s, "tbl"))
    End Sub

    Public Function GetStudentatt2(ByVal dt As Date, ByVal a As Integer, ByVal Sessionid As String, ByVal Courseid As String, ByVal classesid As String, ByVal Sem As String) As DataTable
        Dim aa As SqlParameter() = {New SqlParameter("@dt", dt), New SqlParameter("@id", CInt(a)), New SqlParameter("@Sessionid", Sessionid), New SqlParameter("@COurseid", Courseid), New SqlParameter("@Classesid", classesid), New SqlParameter("@sem", Sem)}
        Return cmd.getDataTable("dbo.Attgetstu", aa, "dd")
    End Function
    ''----------att rpt daily attendance
    Public Sub getdailyattendance(ByVal grd As GridView, ByVal courseid As Integer, ByVal sem As Integer, ByVal classes As Integer, ByVal dtf As Date, ByVal dtto As Date)
        s = {New SqlParameter("@q", "dailyattendance"), New SqlParameter("@cid", cmd.IsNull(courseid, DBNull.Value)), _
             New SqlParameter("@sem", cmd.IsNull(sem, DBNull.Value)), _
             New SqlParameter("@classesid", cmd.IsNull(classes, DBNull.Value)), _
              New SqlParameter("@dtfrom", dtf), _
              New SqlParameter("@dtot", dtto)}
        cmd.FillGrd(grd, cmd.getDataTable("[dbo].[Att_rpt]", s, "tbl"))
    End Sub
    Public Sub getdailyattendanceempwise(ByVal grd As GridView, ByVal uid As Integer, ByVal dtf As Date, ByVal dtto As Date)
        s = {New SqlParameter("@q", "attstuempwise"), _
            New SqlParameter("@uid", uid), _
              New SqlParameter("@dtfrom", dtf), _
              New SqlParameter("@dtot", dtto)}
        cmd.FillGrd(grd, cmd.getDataTable("[dbo].[Att_rpt]", s, "tbl"))
    End Sub
    Public Sub gettimetablelog(ByVal grd As GridView)
        s = {New SqlParameter("@q", "att_timetablelog")}
        cmd.FillGrd(grd, cmd.getDataTable("[dbo].[Att_rpt]", s, "tbl"))
    End Sub
    Public Sub getattendancelog(ByVal grd As GridView)
        s = {New SqlParameter("@q", "att_attendancelog")}
        cmd.FillGrd(grd, cmd.getDataTable("[dbo].[Att_rpt]", s, "tbl"))
    End Sub

    Public Sub getLecture_Planrpt(ByVal grd As GridView)
        s = {New SqlParameter("@q", "Lecture_Plan")}
        cmd.FillGrd(grd, cmd.getDataTable("[dbo].[Att_rpt]", s, "tbl"))
    End Sub

    Public Function ExportDatatableToHtml(ByVal dt As DataTable) As String
        Dim strHTMLBuilder As StringBuilder = New StringBuilder()
        ' strHTMLBuilder.Append("<table width=""100%""  border=""1""  style=""font-size: small"" class=""table table-striped table-responsive"" >")
        strHTMLBuilder.Append("<table width=""100%""  border=""1""  style=""font-size: small"" class=""table table-striped"" >")
        strHTMLBuilder.Append("<thead class=""btn-info"">")
      
        'strHTMLBuilder.Append("<th>")
        'strHTMLBuilder.Append("S.No.")
        'strHTMLBuilder.Append("</th>")
        strHTMLBuilder.Append("<thead>")
        strHTMLBuilder.Append("<tr>")
        strHTMLBuilder.Append("<th  align=""center""  colspan=""" & (dt.Columns.Count) & """>")
        strHTMLBuilder.Append(" <h2> <strong> MAHAKAUSHAL UNIVERSITY </strong></h2>")
        strHTMLBuilder.Append("</th>")
        strHTMLBuilder.Append("</tr>")


        For Each myColumn As DataColumn In dt.Columns
            strHTMLBuilder.Append("<th>")
            strHTMLBuilder.Append(myColumn.ColumnName)
            strHTMLBuilder.Append("</th>")
        Next


        strHTMLBuilder.Append("</thead>")
        Dim x As Integer = 0
        For Each myRow As DataRow In dt.Rows

            strHTMLBuilder.Append("<tr >")
            'strHTMLBuilder.Append("<td >")
            'x = x + 1
            'strHTMLBuilder.Append(x)
            'strHTMLBuilder.Append("</td >")
            For Each myColumn As DataColumn In dt.Columns
                strHTMLBuilder.Append("<td  valign=""top"" >")
                strHTMLBuilder.Append(myRow(myColumn.ColumnName).ToString())
                strHTMLBuilder.Append("</td>")
            Next

            strHTMLBuilder.Append("</tr>")
        Next
        strHTMLBuilder.Append("</table>")
        Dim Htmltext As String = strHTMLBuilder.ToString()
        Return Htmltext
    End Function
    Function RemoveEmptyColumns(ByVal Datatable As DataTable) As DataTable
        Dim mynetable As DataTable = Datatable.Copy
        Dim counter As Integer = mynetable.Rows.Count
        Dim col As DataColumn
        For Each col In mynetable.Columns
            Dim dr() As DataRow = mynetable.Select(col.ColumnName + " is   Null ")
            If dr.Length = counter Then
                Datatable.Columns.Remove(col.ColumnName)
                Datatable.AcceptChanges()
            End If
        Next
        Return Datatable
    End Function


    'Public Function tb_header_col(ByVal hdatatable As DataTable) As DataTable
    '    'Dim sql As String = "Select 'feeheadid' as feeheaid, 'Feehead' as Feehead, courseid AS SEATID,code from view_filter where c='s' order by courseid"
    '    Dim dt As DataTable = hdatatable 'Hari.Utility.db.GetRecodeDT(sql)
    '    Dim name(dt.Columns.Count) As String

    '    Dim i As Integer = 0
    '    For Each column As DataColumn In dt.Columns
    '        name(i) = column.ColumnName
    '        i += 1
    '    Next

    '    Dim col_names As String = Join(name, ",")
    '    Dim col_ids As String = Join(name, ",")
    '    Dim dt1 As New DataTable
    '    Dim dr As DataRow
    '    Dim list1 As String() = col_ids.Split(","c)
    '    Dim list2 As String() = Left(col_names, Len(col_names) - 1).Split(","c)
    '    ' dt.Columns.Add(New DataColumn("col_id"))
    '    dt1.Columns.Add(New DataColumn("col_name"))
    '    For x As Integer = 0 To list2.Length - 1
    '        dr = dt1.NewRow()
    '        '  dr("col_id") = list1(i)
    '        dr("col_name") = list2(x)
    '        dt1.Rows.Add(dr)
    '    Next
    '    Return dt1
    'End Function
    'Public Function tb_row_vals(ByVal dt As DataTable) As DataTable
    '    Return dt
    'End Function

    'Public Function Bind(ByVal dt As DataTable) As String
    '    Dim strb As New StringBuilder()
    '    Dim y As Integer = 3
    '    strb.Append("<table id=""tableID"" class=""table table-striped"">")

    '    strb.Append("<thead class=""btn-info"">")
    '    strb.Append("<tr>")
    '    Dim header As DataTable = tb_header_col(dt)
    '    ' strb.Append("<td></td>")
    '    strb.Append("<th>S No.</th>")

    '    strb.Append("<th>")
    '    strb.Append("<input id=""P"" name=""gender"" onclick=""boxDisable(this.id)""    type=""radio""   />P")
    '    strb.Append("</th>")

    '    strb.Append("<th>")
    '    strb.Append("<input id=""A"" name=""gender"" onclick=""boxDisable(this.id)""    type=""radio""   />A")
    '    strb.Append("</th>")

    '    strb.Append("<th>")
    '    strb.Append("<input id=""E"" name=""gender"" onclick=""boxDisable(this.id)""    type=""radio""   />L")
    '    strb.Append("</th>")

    '    strb.Append("<th>Roll_No</th>")
    '    strb.Append("<th>Student</th>")
    '    strb.Append("<th>AdmissionNo</th>")
    '    strb.Append("<th>Course</th>")
    '    strb.Append("<th>Sem</th>")
    '    strb.Append("<th>Classes</th>")
    '    strb.Append("<th>Grp</th>")

    '    'For i As Integer = 1 To header.Rows.Count - 1
    '    '    strb.Append("<th>" + header.Rows(i)("col_name") & "</th>")
    '    'Next
    '    strb.Append("</tr>")
    '    strb.Append("</thead>")

    '    strb.Append("<tbody>")

    '    Dim row_value As DataTable = dt
    '    For j As Integer = 0 To row_value.Rows.Count - 1
    '        strb.Append("<tr>")

    '        strb.Append("<td>")
    '        strb.Append(j + 1)
    '        strb.Append("</td>")


    '        strb.Append("<td >")
    '        strb.Append("<input id='value_1_" & (j + 1) & "_txb' class=""P""  value=""P"" type=""radio"" name='value_1_" & (j + 1) & "_txb' " & row_value(j)(17) & " />")
    '        strb.Append("</td>")

    '        strb.Append("<td >")
    '        strb.Append("<input id='value_2_" & (j + 1) & "_txb' class=""A""  value=""A""  type=""radio"" name='value_1_" & (j + 1) & "_txb'   " & row_value(j)(18) & "  />")
    '        strb.Append("</td>")

    '        strb.Append("<td >")
    '        strb.Append("<input id='value_3_" & (j + 1) & "_txb' class=""E""   value=""E""  type=""radio"" name='value_1_" & (j + 1) & "_txb'  " & row_value(j)(19) & "  />")
    '        strb.Append("</td>")



    '        strb.Append("<td>")
    '        strb.Append(row_value.Rows(j)("Roll_No"))
    '        strb.Append("</td>")

    '        strb.Append("<td>")
    '        strb.Append(row_value.Rows(j)("Student"))
    '        strb.Append("</td>")

    '        strb.Append("<td>")
    '        strb.Append(row_value.Rows(j)("AdmissionNo"))
    '        strb.Append("</td>")

    '        strb.Append("<td>")
    '        strb.Append(row_value.Rows(j)("Course"))
    '        strb.Append("</td>")

    '        strb.Append("<td>")
    '        strb.Append(row_value.Rows(j)("Sem"))
    '        strb.Append("</td>")

    '        strb.Append("<td>")
    '        strb.Append(row_value.Rows(j)("Classes"))
    '        strb.Append("</td>")

    '        strb.Append("<td>")
    '        strb.Append(row_value.Rows(j)("Grp"))
    '        strb.Append("</td>")

    '        'strb.Append("<td style=""display:none"" >")
    '        'strb.Append(row_value.Rows(j)("feehead"))
    '        'strb.Append("</td>")


    '        strb.Append("</tr>")
    '    Next
    '    strb.Append("</tbody>")
    '    strb.Append("</table>")
    '    ' Literal2.Text = strb.ToString()
    '    '  tableValue = strb.ToString()
    '    Return strb.ToString()
    'End Function

    Public Function insertattendancenew(ByVal ttid As Integer, ByVal dated As Date, ByVal teacherid As Integer, ByVal subjectid As Integer, ByVal prd As Integer, ByVal InBehalfofID As Integer, ByVal dt As DataTable) As String
        Dim aa As SqlParameter() = {New SqlParameter("@ttid", ttid), _
                                    New SqlParameter("@Dated", dated), _
                                    New SqlParameter("@Teacherid", teacherid), _
                                    New SqlParameter("@SubjectID", subjectid), _
                                     New SqlParameter("@prd", prd), _
                                    New SqlParameter("@InBehalfofID", InBehalfofID), _
                                     New SqlParameter("@tblCustomers", dt)}

        Return cmd.execStoredProcudureretstr("Attstuins", aa)

    End Function
    Public Function insertattendance(ByVal dt As DataTable) As String

        Dim aa As SqlParameter() = {New SqlParameter("@Type", "InsertDetails"), _
                                      New SqlParameter("@tblCustomers", dt)}
        Return cmd.execStoredProcudure("spEmpDetails", aa)
    End Function
    Public Sub deleteattendance(ByVal dt As DataTable, ByVal ttid As Integer, ByVal dated As Date)
        Dim sql As String
        cmd.OpenConn()
        For j As Integer = 0 To dt.Rows.Count - 1
            'sql = " delete from studentatt where studentid=" & dt.Rows(j).Item(9) & "  and dated='" & dt.Rows(j).Item(2) & "' and period='" & dt.Rows(j).Item(1) & "' "
            'cmd.execSQL(sql)
            sql = " delete StudentAtt from StudentAtt s  " & _
                    " inner join TimeTable tt on tt.Timetableid='" & ttid & "'" & _
            "where s.Dated='" & dated & "' and s.Period=tt.Prd and s.studentid='" & dt.Rows(j).Item("studentid") & "'"
            cmd.execSQL(sql)

        Next
        cmd.CloseConn()
    End Sub
    Public Function getsubjectbehalfof(ByVal id As Integer) As DataTable
        Dim sql As String = ""
        sql = "select  s.SubjectID, s.Code + N'>>' + s.Subject AS Code  from timetable t " & _
                " inner join subject s on t.courseid=s.courseid and t.sem=s.sem and t.classesid=s.classesid " & _
                " and t.sessionid=s.sessionid and t.evenodd=s.evenodd where t.timetableid=" & id & " order by s.serial"
        Return cmd.getDataTable(sql)

    End Function
    Public Function notice(ByVal value As Integer) As String
        Dim sql As String = ""
        sql = "SELECT [msg] FROM [notice] WHERE ([noticecateory] = " & value & ")"
        Return cmd.execScaler(sql)
    End Function

  

    'Public Function Inserttimetable(ByVal TeacherID As Integer, ByVal FromDt As Date, ByVal ToDt As Date, ByVal CourseID As Integer, ByVal Sem As Integer, ByVal ClassesID As Integer, ByVal ClassRoom As String, ByVal Grp As String, ByVal SubjectID As Integer, ByVal Combineid As String, ByVal Teach_Type As String, ByVal WdNo As List(Of Integer), ByVal Prd As Integer, ByVal elective As String) As Integer

    '    Dim sqlcon As SqlConnection = New SqlConnection(dbnew.ConString)
    '    Dim cmdsql As New SqlCommand
    '    cmdsql.Connection = sqlcon
    '    sqlcon.Open()
    '    Dim SqlTrans As SqlTransaction = Nothing
    '    SqlTrans = sqlcon.BeginTransaction
    '    cmdsql.Transaction = SqlTrans

    '    Dim reader As SqlDataReader
    '    Dim rvalue As Integer
    '    cmdsql.Parameters.AddWithValue("TeacherID", TeacherID)
    '    cmdsql.Parameters.AddWithValue("FromDt", FromDt)
    '    cmdsql.Parameters.AddWithValue("ToDt", ToDt)
    '    cmdsql.Parameters.AddWithValue("Course", CourseID)
    '    cmdsql.Parameters.AddWithValue("ClassRoom", ClassRoom)
    '    cmdsql.Parameters.AddWithValue("Sem", Sem)
    '    cmdsql.Parameters.AddWithValue("Classes", ClassesID)
    '    cmdsql.Parameters.AddWithValue("Grp", Grp)
    '    cmdsql.Parameters.AddWithValue("Subject", SubjectID)
    '    'cmdsql.Parameters.AddWithValue("Combineid", Combineid)
    '    cmdsql.Parameters.AddWithValue("Teach_Type", Teach_Type)
    '    cmdsql.Parameters.AddWithValue("Prd", Prd)
    '    cmdsql.Parameters.AddWithValue("WdNo", DBNull.Value)
    '    cmdsql.Parameters.AddWithValue("Combineid", Combineid)
    '    cmdsql.Parameters.AddWithValue("electiveid", elective)
    '    cmdsql.Parameters.AddWithValue("sessionid", HttpContext.Current.Request.QueryString("s"))
    '    cmdsql.Parameters.AddWithValue("evenodd", HttpContext.Current.Request.QueryString("e"))

    '    cmdsql.Parameters.AddWithValue("userID", HttpContext.Current.Request.QueryString("u"))

    '    cmd.UpdateParameter(cmdsql.Parameters)

    '    Try

    '        'For Each Item As Integer In WdNo
    '        '    cmdsql.Parameters("WdNo").Value = Item
    '        '    'cmdsql.CommandText = "SELECT     TeacherID,sem " & _
    '        '    '                "FROM TimeTable WHERE    (FromDt = @FromDt)" & _
    '        '    '                " AND (ToDt = @ToDt) AND (Prd = @Prd) And (WdNo=@WdNo) and (ClassRoom=@ClassRoom)"

    '        '    cmdsql.CommandText = "SELECT     TeacherID FROM TimeTable " & _
    '        '            "WHERE     (Prd =@prd) And (WdNo=@wdno) and classroom=@classroom and  sessionid=@sessionid and evenodd=@evenodd "

    '        '    '  cmdsql.ExecuteNonQuery()



    '        '    '   Dim dt As Data = 
    '        '    Dim reader As SqlDataReader = cmdsql.ExecuteReader
    '        '    If reader.HasRows Then
    '        '        reader.Read()
    '        '        'Throw New Exception("Already alot to " + GetTeacher(reader(0)) + ", Sem:" + reader("Sem").ToString)
    '        '        Throw New Exception("Already alot to " + GetTeacher(reader(0)) + "")
    '        '    End If
    '        '    reader.Close()
    '        'Next
    '        For Each Item As Integer In WdNo
    '            cmdsql.Parameters("WdNo").Value = Item
    '            cmdsql.CommandText = "SELECT     TeacherID " & _
    '                        "FROM  TimeTable  " & _
    '                        "WHERE(TimeTable.FromDt = @FromDt) AND (TimeTable.ToDt = @ToDt) AND (TimeTable.CourseID = @Course) AND  " & _
    '                        "(TimeTable.Sem = @Sem) AND (TimeTable.ClassesID = @Classes)  AND (TimeTable.Combineid = @Combineid) AND (TimeTable.SubjectID = @Subject) AND  " & _
    '                        "(TimeTable.Teach_Type = @Teach_Type) AND (TimeTable.WdNo = @WdNo) AND (TimeTable.Prd = @Prd) and  sessionid=@sessionid AND Grp=@grp and (TimeTable.evenodd = @evenodd)  "
    '            reader = cmdsql.ExecuteReader
    '            If reader.HasRows Then
    '                reader.Read()
    '                Throw New Exception("Already alot to " + GetTeacher(reader(0)))
    '            End If
    '            reader.Close()

    '            cmdsql.CommandText = "INSERT INTO TimeTable " & _
    '                "(evenodd,sessionid,Combineid,TeacherID, FromDt, ToDt, CourseID, Sem, ClassesID, ClassRoom, Grp, SubjectID, Teach_Type, WdNo, Prd, userid,electiveid) " & _
    '                "VALUES     (@evenodd,@sessionid,@Combineid,@TeacherID,@FromDt,@ToDt,@Course,@Sem,@Classes,@ClassRoom,@Grp,@Subject,@Teach_Type,@WdNo,@Prd,@userID,@electiveid) "

    '            rvalue += cmdsql.ExecuteNonQuery()
    '        Next
    '        Return rvalue
    '    Catch ex As SqlException
    '        reader.Close()
    '        SqlTrans.Rollback()
    '        SqlTrans.Dispose()
    '        Throw New Exception(ex.Message, ex)
    '    Finally

    '        SqlTrans.Commit()
    '        SqlTrans.Dispose()
    '        cmdsql.Dispose()
    '        sqlcon.Dispose()
    '    End Try
    'End Function
    Public Function Inserttimetable(ByVal TeacherID As Integer, ByVal FromDt As Date, ByVal ToDt As Date, ByVal CourseID As Integer, ByVal Sem As Integer, ByVal ClassesID As Integer, ByVal ClassRoom As String, ByVal Grp As String, ByVal SubjectID As Integer, ByVal Teach_Type As String, ByVal WdNo As List(Of Integer), ByVal Prd As Integer) As Integer

        Dim sqlcon As SqlConnection = New SqlConnection(dbnew.ConString)
        Dim cmdsql As New SqlCommand
        cmdsql.Connection = sqlcon
        sqlcon.Open()
        Dim SqlTrans As SqlTransaction = Nothing
        SqlTrans = sqlcon.BeginTransaction
        cmdsql.Transaction = SqlTrans

        Dim reader As SqlDataReader
        Dim rvalue As Integer
        cmdsql.Parameters.AddWithValue("TeacherID", TeacherID)
        cmdsql.Parameters.AddWithValue("FromDt", FromDt)
        cmdsql.Parameters.AddWithValue("ToDt", ToDt)
        cmdsql.Parameters.AddWithValue("Course", CourseID)
        cmdsql.Parameters.AddWithValue("ClassRoom", ClassRoom)
        cmdsql.Parameters.AddWithValue("Sem", Sem)
        cmdsql.Parameters.AddWithValue("Classes", ClassesID)
        cmdsql.Parameters.AddWithValue("Grp", Grp)
        cmdsql.Parameters.AddWithValue("Subject", SubjectID)
        'cmdsql.Parameters.AddWithValue("Combineid", Combineid)
        cmdsql.Parameters.AddWithValue("Teach_Type", Teach_Type)
        cmdsql.Parameters.AddWithValue("Prd", Prd)
        cmdsql.Parameters.AddWithValue("WdNo", DBNull.Value)
     
        cmdsql.Parameters.AddWithValue("sessionid", HttpContext.Current.Request.QueryString("s"))
        cmdsql.Parameters.AddWithValue("evenodd", HttpContext.Current.Request.QueryString("e"))

        cmdsql.Parameters.AddWithValue("userID", HttpContext.Current.Request.QueryString("u"))

        cmd.UpdateParameter(cmdsql.Parameters)



        'For Each Item As Integer In WdNo
        '    cmdsql.Parameters("WdNo").Value = Item
        '    'cmdsql.CommandText = "SELECT     TeacherID,sem " & _
        '    '                "FROM TimeTable WHERE    (FromDt = @FromDt)" & _
        '    '                " AND (ToDt = @ToDt) AND (Prd = @Prd) And (WdNo=@WdNo) and (ClassRoom=@ClassRoom)"

        '    cmdsql.CommandText = "SELECT     TeacherID FROM TimeTable " & _
        '            "WHERE     (Prd =@prd) And (WdNo=@wdno) and classroom=@classroom and  sessionid=@sessionid and evenodd=@evenodd "

        '    '  cmdsql.ExecuteNonQuery()



        '    '   Dim dt As Data = 
        '    Dim reader As SqlDataReader = cmdsql.ExecuteReader
        '    If reader.HasRows Then
        '        reader.Read()
        '        'Throw New Exception("Already alot to " + GetTeacher(reader(0)) + ", Sem:" + reader("Sem").ToString)
        '        Throw New Exception("Already alot to " + GetTeacher(reader(0)) + "")
        '    End If
        '    reader.Close()
        'Next


        For Each Item As Integer In WdNo
            cmdsql.Parameters("WdNo").Value = Item
            'cmdsql.CommandText = "SELECT     TeacherID " & _
            '            "FROM  TimeTable  " & _
            '            "WHERE(TimeTable.FromDt = @FromDt) AND (TimeTable.ToDt = @ToDt) AND (TimeTable.CourseID = @Course) AND  " & _
            '            "(TimeTable.Sem = @Sem) AND (TimeTable.ClassesID = @Classes)  AND (TimeTable.SubjectID = @Subject) AND  " & _
            '            "(TimeTable.Teach_Type = @Teach_Type) AND (TimeTable.WdNo = @WdNo) AND (TimeTable.Prd = @Prd) and  sessionid=@sessionid AND Grp=@grp and (TimeTable.evenodd = @evenodd)  "
            'reader = cmdsql.ExecuteReader
            'If reader.HasRows Then
            '    reader.Read()
            '    Throw New Exception("Already alot to ")
            'End If
            'reader.Close()

            cmdsql.CommandText = "INSERT INTO TimeTable " & _
                "(evenodd,sessionid,TeacherID, FromDt, ToDt, CourseID, Sem, ClassesID, ClassRoom, Grp, SubjectID, Teach_Type, WdNo, Prd, userid) " & _
                "VALUES     (@evenodd,@sessionid,@TeacherID,@FromDt,@ToDt,@Course,@Sem,@Classes,@ClassRoom,@Grp,@Subject,@Teach_Type,@WdNo,@Prd,@userID) "

            rvalue += cmdsql.ExecuteNonQuery()
        Next
        Return rvalue

    End Function
    Public Function GetTeacher(ByVal id As Integer) As String
        Dim sql As String
        sql = "select username from users where userid='" & id & "'"

        Dim str As String = cmd.execScaler(sql)
        Return str
    End Function



    Public Function attnotfeed(ByVal dtfrom As Date, ByVal dtto As Date, ByVal collegeid As Integer, ByVal departmentid As Integer, ByVal evenodd As Integer, ByVal t As String) As String
        Dim aa As SqlParameter() = {New SqlParameter("@t", t), New SqlParameter("@Dt1", dtfrom), New SqlParameter("@Dt2", dtto), New SqlParameter("@cid", cmd.IsNull(collegeid, DBNull.Value)), New SqlParameter("@dptid", cmd.IsNull(departmentid, DBNull.Value))}
        Return Replace(Replace(cmd.execScaler("[dbo].[Att_notFeed]", "Procedure", aa), "&lt;", "<"), "&gt;", ">")
    End Function

    Public Sub Att_getemprolwiselist(ByVal ddl As DropDownList)
        's = {New SqlParameter("@q", "leavesetuser"), New SqlParameter("@cid", collegeid), New SqlParameter("@deptid", cmd.IsNull(dept, DBNull.Value))}
        'Return Replace(Replace(cmd.execScaler("[dbo].[Leave_Setlevel]", "Procedure", s), "&lt;", "<"), "&gt;", ">")
        Dim s() As SqlParameter = {New SqlParameter("@q", "emp_role_wise_list"), New SqlParameter("@uid", HttpContext.Current.Request.QueryString("u")), New SqlParameter("@roleid", HttpContext.Current.Request.QueryString("r"))}
        cmd.FillDropdown(ddl, "[dbo].[Att_Fill]", s)
    End Sub

    Public Sub Att_getemprolwiselist_rolewise(ByVal ddl As DropDownList, ByVal roleid As Integer)
        's = {New SqlParameter("@q", "leavesetuser"), New SqlParameter("@cid", collegeid), New SqlParameter("@deptid", cmd.IsNull(dept, DBNull.Value))}
        'Return Replace(Replace(cmd.execScaler("[dbo].[Leave_Setlevel]", "Procedure", s), "&lt;", "<"), "&gt;", ">")
        Dim s() As SqlParameter = {New SqlParameter("@q", "emp_role_wise_list_role_wise"), New SqlParameter("@uid", HttpContext.Current.Request.QueryString("u")), New SqlParameter("@roleid", roleid)}
        cmd.FillDropdown(ddl, "[dbo].[Att_Fill]", s)
    End Sub

    Public Sub Att_get_subject_teacherwise(ByVal q As String, ByVal ddl As DropDownList, ByVal teacherid As Integer)
        's = {New SqlParameter("@q", "leavesetuser"), New SqlParameter("@cid", collegeid), New SqlParameter("@deptid", cmd.IsNull(dept, DBNull.Value))}
        'Return Replace(Replace(cmd.execScaler("[dbo].[Leave_Setlevel]", "Procedure", s), "&lt;", "<"), "&gt;", ">")
        Dim s() As SqlParameter = {New SqlParameter("@q", q), New SqlParameter("@uid", teacherid), New SqlParameter("@sessionid", HttpContext.Current.Request.QueryString("s")), New SqlParameter("@evenodd", HttpContext.Current.Request.QueryString("e")), New SqlParameter("@ayid", HttpContext.Current.Request.QueryString("ayid"))}
        cmd.FillDropdown(ddl, "[dbo].[Att_Fill]", s)
    End Sub

    Public Sub Att_get_subject_teacherwise_new_subject(ByVal q As String, ByVal ddl As DropDownList, ByVal teacherid As Integer, Optional ByVal examname As String = "0", Optional ByVal cid As String = "0", Optional ByVal deptid As String = "0", Optional ByVal semid As String = "0", Optional ByVal subjectid As String = "0")
        's = {New SqlParameter("@q", "leavesetuser"), New SqlParameter("@cid", collegeid), New SqlParameter("@deptid", cmd.IsNull(dept, DBNull.Value))}
        'Return Replace(Replace(cmd.execScaler("[dbo].[Leave_Setlevel]", "Procedure", s), "&lt;", "<"), "&gt;", ">")
        Dim s() As SqlParameter = {New SqlParameter("@q", q), New SqlParameter("@uid", teacherid), New SqlParameter("@ayid", HttpContext.Current.Request.QueryString("ay")), New SqlParameter("@mastervalue", examname), New SqlParameter("@cid", cid), New SqlParameter("@deptid", deptid), New SqlParameter("@desi", semid), New SqlParameter("@TeachingStaff", subjectid)}
        cmd.FillDropdown(ddl, "[dbo].[Att_Fill]", s)
    End Sub
    Public Sub upload_assignment_subject(ByVal q As String, ByVal grd As GridView, ByVal teacherid As Integer, ByVal ayid As Integer)
        's = {New SqlParameter("@q", "leavesetuser"), New SqlParameter("@cid", collegeid), New SqlParameter("@deptid", cmd.IsNull(dept, DBNull.Value))}
        'Return Replace(Replace(cmd.execScaler("[dbo].[Leave_Setlevel]", "Procedure", s), "&lt;", "<"), "&gt;", ">")
        Dim s() As SqlParameter = {New SqlParameter("@q", q), New SqlParameter("@uid", teacherid), New SqlParameter("@ayid", ayid)}
        cmd.FillGrd(grd, cmd.getDataTablenew("[dbo].[Att_Fill]", s, "tbl"))
    End Sub

    Public Sub Timetable_Adjustment(ByVal ddl As DropDownList, ByVal dated As String, ByVal uid As Integer)
        's = {New SqlParameter("@q", "leavesetuser"), New SqlParameter("@cid", collegeid), New SqlParameter("@deptid", cmd.IsNull(dept, DBNull.Value))}
        'Return Replace(Replace(cmd.execScaler("[dbo].[Leave_Setlevel]", "Procedure", s), "&lt;", "<"), "&gt;", ">")
        Dim s() As SqlParameter = {New SqlParameter("@q", "Tot_Absent_t"), New SqlParameter("@dated", dated), New SqlParameter("@uid", HttpContext.Current.Request.QueryString("u"))}
        cmd.FillDropdown(ddl, "[dbo].[Att_TimeTable_Ar]", s)

    End Sub

    Public Sub Timetable_Adjustment_prdlist(ByVal grd As GridView, ByVal dated As String, ByVal uid As Integer)
        's = {New SqlParameter("@q", "leavesetuser"), New SqlParameter("@cid", collegeid), New SqlParameter("@deptid", cmd.IsNull(dept, DBNull.Value))}
        'Return Replace(Replace(cmd.execScaler("[dbo].[Leave_Setlevel]", "Procedure", s), "&lt;", "<"), "&gt;", ">")
        Dim s() As SqlParameter = {New SqlParameter("@q", "Adjustment_prdlist"), New SqlParameter("@dated", dated), New SqlParameter("@AdjustmentTo", uid)}
        cmd.FillGrd(grd, cmd.getDataTablenew("[dbo].[Att_TimeTable_Ar]", s, "tbl"))
    End Sub

    Public Function Timetable_Adjustment_present(ByVal prd As Integer, ByVal dated As String, ByVal uid As Integer) As DataTable

        Dim s() As SqlParameter = {New SqlParameter("@q", "get_present_teacher_ddl"), New SqlParameter("@dated", dated), New SqlParameter("@uid", HttpContext.Current.Request.QueryString("u"))}
        Return cmd.getDataTablenew("[dbo].[Att_TimeTable_Ar]", s, "tbl")

    End Function

    Public Function Timetable_Adjustment_inset(ByVal timetableid As Integer, ByVal dated As String, ByVal adjfrom As Integer, ByVal adjto As Integer) As String

        Dim s() As SqlParameter = {New SqlParameter("@q", "Adjustment_inst"), New SqlParameter("@dated", dated), New SqlParameter("@Adjustmentfrom", adjfrom), New SqlParameter("@Adjustmentto", adjto), New SqlParameter("@timetableid", timetableid), New SqlParameter("@uid", HttpContext.Current.Request.QueryString("u"))}
        'Return cmd.getDataTablenew("[dbo].[Att_TimeTable_Ar]", s, "tbl")
        Return cmd.execStoredProcudureretstr("[dbo].[Att_TimeTable_Ar]", s)
    End Function

    Public Function timetable_adjustment_rpt(ByVal dated As String, ByVal adjfrom As Integer) As String
        s = {New SqlParameter("@q", "get_adjustment_rpt"), New SqlParameter("@dated", dated), New SqlParameter("@Adjustmentfrom", adjfrom)}
        Dim sbHTML As StringBuilder = New StringBuilder(cmd.execScaler("[dbo].[Att_TimeTable_Ar]", "Procedure", s))
        Dim i As Integer = 0
        Return cmd.Remove_Htmltag(sbHTML)
    End Function
    Public Function Feebback_courewise_rpt(ByVal coureid As Integer, ByVal sem As Integer, ByVal classes As Integer) As String
        s = {New SqlParameter("@q", "Fee_Back_course_wise"), New SqlParameter("@cid", coureid), New SqlParameter("@sem", sem), New SqlParameter("@classesid", classes)}
        Dim sbHTML As StringBuilder = New StringBuilder(cmd.execScaler("[dbo].[Att_rpt]", "Procedure", s))
        Dim i As Integer = 0
        Return cmd.Remove_Htmltag(sbHTML)
    End Function
    '---report/attendance/Attendancerpt
    Public Function assing_sub_rpt(ByVal sessionid As Integer, ByVal evenodd As Integer, ByVal courseid As Integer, ByVal sem As Integer, ByVal classesid As Integer) As String
        Dim s() As SqlParameter = {New SqlParameter("@q", "assign_subject_rpt"), New SqlParameter("@batchid", sessionid), New SqlParameter("@evenodd", evenodd), New SqlParameter("@cid", cmd.IsNull(courseid, DBNull.Value)), New SqlParameter("@sem", cmd.IsNull(sem, DBNull.Value)), New SqlParameter("@cls", cmd.IsNull(classesid, DBNull.Value))}
        Dim sbHTML As StringBuilder = New StringBuilder(cmd.execScaler("[dbo].[Att_assignsub]", "Procedure", s))
        Return cmd.Remove_Htmltag(sbHTML)
    End Function

    'Public Function Struckoff_rpt(ByVal q As String, ByVal Collegeid As Integer, ByVal courseid As Integer, ByVal sem As Integer, ByVal classesid As Integer, ByVal batchid As Integer, Optional ByVal ayid As String = "0") As String
    '    Dim s() As SqlParameter = {New SqlParameter("@q", q), New SqlParameter("@instid", cmd.IsNull(Collegeid, DBNull.Value)), New SqlParameter("@cid", cmd.IsNull(courseid, DBNull.Value)), New SqlParameter("@sem", cmd.IsNull(sem, DBNull.Value)), New SqlParameter("@cls", cmd.IsNull(classesid, DBNull.Value)), New SqlParameter("@ayid", cmd.IsNull(ayid, DBNull.Value))}
    '    Dim sbHTML As StringBuilder = New StringBuilder(cmd.execScaler("[dbo].[Att_assignsub]", "Procedure", s))
    '    Return cmd.Remove_Htmltag(sbHTML)
    'End Function
    Public Function Struckoff_rpt(ByVal q As String, ByVal Collegeid As Integer, ByVal courseid As Integer, ByVal sem As Integer, ByVal classesid As Integer, ByVal batchid As Integer, Optional ByVal ayid As String = "0", Optional ByVal admissionyear As String = "0") As String
        Dim s() As SqlParameter = {New SqlParameter("@q", q), New SqlParameter("@instid", cmd.IsNull(Collegeid, DBNull.Value)), New SqlParameter("@cid", cmd.IsNull(courseid, DBNull.Value)), New SqlParameter("@sem", cmd.IsNull(sem, DBNull.Value)), New SqlParameter("@cls", cmd.IsNull(classesid, DBNull.Value)), New SqlParameter("@ayid", cmd.IsNull(ayid, DBNull.Value)), New SqlParameter("@admissionyear", cmd.IsNull(admissionyear, DBNull.Value))}
        Dim sbHTML As StringBuilder = New StringBuilder(cmd.execScaler("[dbo].[Att_assignsub]", "Procedure", s))
        Return cmd.Remove_Htmltag(sbHTML)
    End Function

    'Public Function Struckoff_rpt_new(ByVal q As String, ByVal Collegeid As Integer, ByVal courseid As Integer, ByVal sem As Integer, ByVal classesid As Integer, ByVal batchid As Integer, ByVal sessionid As Integer, ByVal ayid As String) As String
    '    Dim s() As SqlParameter = {New SqlParameter("@q", q), New SqlParameter("@instid", cmd.IsNull(Collegeid, DBNull.Value)), New SqlParameter("@cid", cmd.IsNull(courseid, DBNull.Value)), New SqlParameter("@sem", cmd.IsNull(sem, DBNull.Value)), New SqlParameter("@cls", cmd.IsNull(classesid, DBNull.Value)), New SqlParameter("@batchid", cmd.IsNull(batchid, DBNull.Value)), New SqlParameter("@evenodd", sessionid), New SqlParameter("@ayid", ayid)}
    '    Dim sbHTML As StringBuilder = New StringBuilder(cmd.execScaler("[dbo].[Att_assignsub]", "Procedure", s))
    '    Return cmd.Remove_Htmltag(sbHTML)
    'End Function
    Public Function Struckoff_rpt_new(ByVal q As String, ByVal Collegeid As Integer, ByVal courseid As Integer, ByVal sem As Integer, ByVal classesid As Integer, ByVal batchid As Integer, ByVal sessionid As Integer, ByVal ayid As String, Optional ByVal admissionyear As String = "0") As String
        Dim s() As SqlParameter = {New SqlParameter("@q", q), New SqlParameter("@instid", cmd.IsNull(Collegeid, DBNull.Value)), New SqlParameter("@cid", cmd.IsNull(courseid, DBNull.Value)), New SqlParameter("@sem", cmd.IsNull(sem, DBNull.Value)), New SqlParameter("@cls", cmd.IsNull(classesid, DBNull.Value)), New SqlParameter("@batchid", cmd.IsNull(batchid, DBNull.Value)), New SqlParameter("@evenodd", cmd.IsNull(sessionid, DBNull.Value)), New SqlParameter("@ayid", ayid), New SqlParameter("@admissionyear", cmd.IsNull(admissionyear, DBNull.Value))}
        Dim sbHTML As StringBuilder = New StringBuilder(cmd.execScaler("[dbo].[Att_assignsub]", "Procedure", s))
        Return cmd.Remove_Htmltag(sbHTML)
    End Function
    Public Function get_student_subject_rpt(ByVal q As String, ByVal Collegeid As Integer, ByVal courseid As Integer, ByVal sem As Integer, ByVal classesid As Integer, ByVal batchid As Integer, ByVal sessionid As Integer, ByVal ayid As String, ByVal subcode As String, ByVal atype As String) As String
        'Dim s() As SqlParameter = {New SqlParameter("@q", q), New SqlParameter("@instid", cmd.IsNull(Collegeid, DBNull.Value)), New SqlParameter("@cid", cmd.IsNull(courseid, DBNull.Value)), New SqlParameter("@sem", cmd.IsNull(sem, DBNull.Value)), New SqlParameter("@cls", cmd.IsNull(classesid, DBNull.Value)), New SqlParameter("@batchid", cmd.IsNull(batchid, DBNull.Value)), New SqlParameter("@evenodd", cmd.IsNull(sessionid, DBNull.Value)), New SqlParameter("@ayid", ayid), New SqlParameter("@admissionyear", cmd.IsNull(admissionyear, DBNull.Value))}
        'Dim sbHTML As StringBuilder = New StringBuilder(cmd.execScaler("[dbo].[Att_assignsub]", "Procedure", s))
        Dim sql As String = ""
        If q = "0" Then
            sql = "select ROW_NUMBER() OVER(ORDER BY AdmissionNo ) AS Sno, V.AdmissionNo AS Regno, v.Student, i.Code AS PROGRAM_Type , c.Code AS Trade,sy.Sem ,cls.Code ,s.Code as Subject_Code,s.Subject  " & _
                        " from StudentSubjectRegistration  ss  left join Subjectnew s on s.SubjectID=ss.Subjectid and ss.Courseid=s.CourseID and ss.Sem=s.Sem and ss.Sessionid=s.Sessionid " & _
                    " left join StudentYear sy on sy.StudentID =ss.Studentid and ss.Sem=sy.SEm and ss.Courseid=sy.Courseid and ss.Sessionid=sy.SessionID and sy.Isstruckoff=0 " & _
                        " left join student v on v.StudentID =sy.StudentID  " & _
                        " left join SubjectType st on st.Subtypeid=s.subtypeid  " & _
                        " left join Course c on c.CourseID=sy.Courseid  " & _
                        " left join Institue i on i.InstitueID=c.Cid " & _
                     " left join Classes cls on cls.ClassesID=sy.Classesid   " & _
                        " where  sy.ayid=" & ayid & " and s.SubjectID='" & subcode & "' and sy.Isstruckoff=0  and sy.courseid=" & courseid.ToString & " and sy.sem=" & sem.ToString & "  order by i.Code , c.Code , sy.SEm,sy.Classesid ,v.AdmissionNo "
        Else
            sql = "select ROW_NUMBER() OVER(ORDER BY i.Code , c.Code , sy.SEm,sy.Classesid ,v.AdmissionNo ) AS Sno,  v.AdmissionNo AS Regno, v.Student, i.Code AS PROGRAM_Type , c.Code AS Trade,sy.Sem ,cls.Code ,s.Code  as Subject_Code ,s.Subject  " & _
                      " from StudentSubjectRegistration  ss  left join Subjectnew s on s.SubjectID=ss.Subjectid and ss.Courseid=s.CourseID and ss.Sem=s.Sem and ss.Sessionid=s.Sessionid " & _
                  " left join StudentYear sy on sy.StudentID =ss.Studentid and ss.Sem=sy.SEm and ss.Courseid=sy.Courseid and ss.Sessionid=sy.SessionID and sy.Isstruckoff=0 " & _
                      " left join student v on v.StudentID =sy.StudentID  " & _
                      " left join SubjectType st on st.Subtypeid=s.subtypeid  " & _
                      " left join Course c on c.CourseID=sy.Courseid  " & _
                      " left join Institue i on i.InstitueID=c.Cid " & _
                   " left join Classes cls on cls.ClassesID=sy.Classesid   " & _
                      " where  sy.ayid=" & ayid & " and s.SubjectID='" & subcode & "'    and sy.Isstruckoff=0  order by i.Code , c.Code , sy.SEm,sy.Classesid ,v.AdmissionNo "

        End If

        'Dim sbHTML As StringBuilder = New StringBuilder(cmd.execScaler(sql))        '
        Return cmd.ExportDatatableToHtml(cmd.getDataTable(sql))

        'Return cmd.Remove_Htmltag(sbHTML)
    End Function

    Public Function Student_List_Session_wise(ByVal q As String, ByVal Collegeid As Integer, ByVal courseid As Integer, ByVal sem As Integer, ByVal classesid As Integer, ByVal batchid As Integer, ByVal sessionid As Integer, ByVal ayid As String) As String
        Dim s() As SqlParameter = {New SqlParameter("@q", q), New SqlParameter("@instid", cmd.IsNull(Collegeid, DBNull.Value)), New SqlParameter("@cid", cmd.IsNull(courseid, DBNull.Value)), New SqlParameter("@sem", cmd.IsNull(sem, DBNull.Value)), New SqlParameter("@cls", cmd.IsNull(classesid, DBNull.Value)), New SqlParameter("@batchid", cmd.IsNull(batchid, DBNull.Value)), New SqlParameter("@evenodd", sessionid), New SqlParameter("@ayid", ayid)}
        Dim sbHTML As StringBuilder = New StringBuilder(cmd.execScaler("[dbo].[Att_assignsub]", "Procedure", s))
        Return cmd.Remove_Htmltag(sbHTML)
    End Function

    Public Sub Student_List_Session_wise_dt(ByVal grd As GridView, ByVal q As String, ByVal Collegeid As Integer, ByVal courseid As Integer, ByVal sem As Integer, ByVal classesid As Integer, ByVal batchid As Integer, ByVal sessionid As Integer, ByVal ayid As String)
        Dim s() As SqlParameter = {New SqlParameter("@q", q), New SqlParameter("@instid", cmd.IsNull(Collegeid, DBNull.Value)), New SqlParameter("@cid", cmd.IsNull(courseid, DBNull.Value)), New SqlParameter("@sem", cmd.IsNull(sem, DBNull.Value)), New SqlParameter("@cls", cmd.IsNull(classesid, DBNull.Value)), New SqlParameter("@batchid", cmd.IsNull(batchid, DBNull.Value)), New SqlParameter("@evenodd", cmd.IsNull(sessionid, DBNull.Value)), New SqlParameter("@ayid", ayid)}
        ' Dim sbHTML As StringBuilder = New StringBuilder(cmd.execScaler("[dbo].[Att_assignsub]", "Procedure", s))
        '  Return cmd.FillGrd(grd, cmd.getDataTablenew("[dbo].[Att_TimeTable_Ar]", s, "tbl"))
        cmd.FillGrd(grd, cmd.getDataTablenew("[dbo].[Att_assignsub]", s, "tbl"))
        '  Return cmd.getDataTablenew("[dbo].[Att_assignsub]", s, "tbl")
    End Sub

    Public Sub Student_List_Session_wise_dt_management(ByVal grd As GridView, ByVal q As String, ByVal uid As Integer, Optional ByVal isstruckoff As Integer = 0)
        Dim s() As SqlParameter = {New SqlParameter("@q", q), New SqlParameter("@uid", cmd.IsNull(uid, DBNull.Value)), New SqlParameter("@isstruckoff", (isstruckoff))}
        cmd.FillGrd(grd, cmd.getDataTablenew("[dbo].[Att_assignsub]", s, "tbl"))

    End Sub

    'Public Function Student_List_Session_wise_struckoff(ByVal q As String, ByVal Collegeid As Integer, ByVal courseid As Integer, ByVal sem As Integer, ByVal classesid As Integer, ByVal batchid As Integer, ByVal sessionid As Integer, ByVal ayid As String, ByVal isstruckoff As String) As DataTable
    '    Dim s() As SqlParameter = {New SqlParameter("@q", q), New SqlParameter("@instid", cmd.IsNull(Collegeid, DBNull.Value)), New SqlParameter("@cid", cmd.IsNull(courseid, DBNull.Value)), New SqlParameter("@sem", cmd.IsNull(sem, DBNull.Value)), New SqlParameter("@cls", cmd.IsNull(classesid, DBNull.Value)), New SqlParameter("@batchid", cmd.IsNull(batchid, DBNull.Value)), New SqlParameter("@evenodd", cmd.IsNull(sessionid, DBNull.Value)), New SqlParameter("@ayid", cmd.IsNull(sessionid, DBNull.Value)), New SqlParameter("@isstruckoff", IIf(isstruckoff = "", DBNull.Value, isstruckoff))}
    '    Return cmd.getDataTable("[dbo].[Att_assignsub]", s, "tbl")
    'End Function

    'Public Function Student_List_Session_wise_struckoff(ByVal q As String, ByVal Collegeid As Integer, ByVal courseid As Integer, ByVal sem As Integer, ByVal classesid As Integer, ByVal batchid As Integer, ByVal sessionid As Integer, ByVal ayid As String, ByVal isstruckoff As String) As DataTable
    '    Dim s() As SqlParameter = {New SqlParameter("@q", q), New SqlParameter("@instid", cmd.IsNull(Collegeid, DBNull.Value)), New SqlParameter("@cid", cmd.IsNull(courseid, DBNull.Value)), New SqlParameter("@sem", cmd.IsNull(sem, DBNull.Value)), New SqlParameter("@cls", cmd.IsNull(classesid, DBNull.Value)), New SqlParameter("@batchid", cmd.IsNull(batchid, DBNull.Value)), New SqlParameter("@evenodd", sessionid), New SqlParameter("@ayid", ayid), New SqlParameter("@isstruckoff", IIf(isstruckoff = "", DBNull.Value, isstruckoff))}
    '    Return cmd.getDataTable("[dbo].[Att_assignsub]", s, "tbl")
    'End Function
    Public Function Student_List_Session_wise_struckoff(ByVal q As String, ByVal Collegeid As Integer, ByVal courseid As Integer, ByVal sem As Integer, ByVal classesid As Integer, ByVal batchid As Integer, ByVal sessionid As Integer, ByVal ayid As String, ByVal isstruckoff As String, Optional ByVal admissionyear As String = "0") As DataTable
        '        Dim s() As SqlParameter = {New SqlParameter("@q", q), New SqlParameter("@instid", cmd.IsNull(Collegeid, DBNull.Value)), New SqlParameter("@cid", cmd.IsNull(courseid, DBNull.Value)), New SqlParameter("@sem", cmd.IsNull(sem, DBNull.Value)), New SqlParameter("@cls", cmd.IsNull(classesid, DBNull.Value)), New SqlParameter("@batchid", cmd.IsNull(batchid, DBNull.Value)), New SqlParameter("@evenodd", sessionid), New SqlParameter("@ayid", ayid), New SqlParameter("@isstruckoff", IIf(isstruckoff = "", DBNull.Value, isstruckoff))}
        Dim s() As SqlParameter = {New SqlParameter("@q", q), New SqlParameter("@instid", cmd.IsNull(Collegeid, DBNull.Value)), New SqlParameter("@cid", cmd.IsNull(courseid, DBNull.Value)), New SqlParameter("@sem", cmd.IsNull(sem, DBNull.Value)), New SqlParameter("@cls", cmd.IsNull(classesid, DBNull.Value)), New SqlParameter("@batchid", cmd.IsNull(batchid, DBNull.Value)), New SqlParameter("@evenodd", cmd.IsNull(sessionid, DBNull.Value)), New SqlParameter("@ayid", ayid), New SqlParameter("@isstruckoff", IIf(isstruckoff = "", DBNull.Value, isstruckoff)), New SqlParameter("@admissionyear", cmd.IsNull(admissionyear, DBNull.Value))}
        Return cmd.getDataTable("[dbo].[Att_assignsub]", s, "tbl")
    End Function
    Public Function Student_List_subject_wise(ByVal q As String, ByVal Collegeid As Integer, ByVal courseid As String, ByVal sem As Integer, ByVal classesid As String, ByVal batchid As String, ByVal sessionid As String, ByVal ayid As String) As String
        Dim s() As SqlParameter = {New SqlParameter("@q", q), New SqlParameter("@instid", cmd.IsNull(Collegeid, DBNull.Value)), New SqlParameter("@cid", cmd.IsNull(courseid, DBNull.Value)), New SqlParameter("@sem", cmd.IsNull(sem, DBNull.Value)), New SqlParameter("@cls", cmd.IsNull(classesid, DBNull.Value)), New SqlParameter("@batchid", cmd.IsNull(batchid, DBNull.Value)), New SqlParameter("@evenodd", sessionid), New SqlParameter("@ayid", ayid)}
        Return ExportDatatableToHtml(cmd.getDataTablenew("[dbo].[Att_assignsub]", s, "Tbl"))
    End Function
    Public Sub Verify_role_wise_subjectold(ByVal grd As GridView, ByVal q As String, ByVal Activityid As String, ByVal ayid As String, ByVal roleid As String, ByVal teacherid As String, ByVal collegeid As String, ByVal courseid As String, ByVal sem As String, ByVal pending As String)
        Dim s() As SqlParameter = {New SqlParameter("@q", q), New SqlParameter("@Activityid", Activityid), New SqlParameter("@ayid", ayid), New SqlParameter("@roleid", roleid), New SqlParameter("@teacherid", teacherid), New SqlParameter("@collegeid", collegeid), New SqlParameter("@courseid", courseid), New SqlParameter("@sem", sem), New SqlParameter("@pending", pending)}
        cmd.FillGrd(grd, cmd.getDataTable("[dbo].[Role_wise_verify]", s, "tbl"))
    End Sub
    Public Sub Verify_role_wise_subject(ByVal grd As GridView, ByVal q As String, ByVal Activityid As String, ByVal ayid As String, ByVal roleid As String, ByVal teacherid As String, ByVal collegeid As String, ByVal courseid As String, ByVal sem As String, ByVal pending As String)
        Dim s() As SqlParameter = {New SqlParameter("@q", q), New SqlParameter("@Activityid", Activityid), New SqlParameter("@ayid", ayid), New SqlParameter("@roleid", roleid), New SqlParameter("@teacherid", teacherid), New SqlParameter("@collegeid", collegeid), New SqlParameter("@courseid", courseid), New SqlParameter("@sem", sem), New SqlParameter("@pending", pending)}
        cmd.FillGrd(grd, cmd.getDataTable("[dbo].[Role_wise_verify_new]", s, "tbl"))
    End Sub
    Public Sub Roll_back_activit_get(ByVal grd As GridView, ByVal q As String, ByVal Activityid As String, ByVal ayid As String, ByVal roleid As String, ByVal teacherid As String, Optional ByVal coursetypeid As String = "0", Optional ByVal collegeid As String = "0", Optional ByVal coursedepartment As String = "0")
        Dim s() As SqlParameter = {New SqlParameter("@q", q), New SqlParameter("@Activityid", Activityid), New SqlParameter("@ayid", ayid), New SqlParameter("@roleid", roleid), New SqlParameter("@teacherid", teacherid), New SqlParameter("@coursetypeid", coursetypeid), New SqlParameter("@collegeid", collegeid), New SqlParameter("@departmentid", coursedepartment)}
        cmd.FillGrd(grd, cmd.getDataTable("[dbo].[Role_wise_College]", s, "tbl"))
    End Sub
    Public Sub Roll_Wise_update_no_Dues(ByVal grd As GridView, ByVal q As String, ByVal Activityid As String, ByVal ayid As String, ByVal roleid As String, ByVal teacherid As String)
        Dim s() As SqlParameter = {New SqlParameter("@q", q), New SqlParameter("@Activityid", Activityid), New SqlParameter("@ayid", ayid), New SqlParameter("@roleid", roleid), New SqlParameter("@teacherid", teacherid)}
        cmd.FillGrd(grd, cmd.getDataTable("[dbo].[Role_wise_College_no_dues]", s, "tbl"))
    End Sub
    Public Sub Roll_back_activit_get_rpt(ByVal grd As GridView, ByVal q As String, ByVal Activityid As String, ByVal ayid As String, ByVal roleid As String, ByVal teacherid As String)
        Dim s() As SqlParameter = {New SqlParameter("@q", q), New SqlParameter("@Activityid", Activityid), New SqlParameter("@ayid", ayid), New SqlParameter("@roleid", roleid), New SqlParameter("@teacherid", teacherid)}
        cmd.FillGrd(grd, cmd.getDataTable("[dbo].[Role_wise_College_rpt]", s, "tbl"))
    End Sub
    Public Sub Roll_back_activit_get_Dashboard_Fee(ByVal grd As GridView, ByVal q As String, ByVal Activityid As String, ByVal ayid As String, ByVal roleid As String, ByVal teacherid As String, Optional ByVal qq As String = "")
        Dim s() As SqlParameter = {New SqlParameter("@q", q), New SqlParameter("@Activityid", Activityid), New SqlParameter("@ayid", ayid), New SqlParameter("@roleid", roleid), New SqlParameter("@teacherid", teacherid), New SqlParameter("@qq", qq)}
        cmd.FillGrd(grd, cmd.getDataTable("[dbo].[Role_wise_College_Fee]", s, "tbl"))
    End Sub
    Public Sub Roll_back_activit_get_Dashboard_category(ByVal grd As GridView, ByVal q As String, ByVal Activityid As String, ByVal ayid As String, ByVal roleid As String, ByVal teacherid As String, Optional ByVal qq As String = "")
        Dim s() As SqlParameter = {New SqlParameter("@q", q), New SqlParameter("@Activityid", Activityid), New SqlParameter("@ayid", ayid), New SqlParameter("@roleid", roleid), New SqlParameter("@teacherid", teacherid), New SqlParameter("@qq", qq)}
        cmd.FillGrd(grd, cmd.getDataTable("[dbo].[Role_wise_College_Category]", s, "tbl"))
    End Sub
    Public Function Roll_back_activit_get_print(ByVal q As String, ByVal Activityid As String, ByVal ayid As String, ByVal roleid As String, ByVal teacherid As String) As DataTable

        Dim s() As SqlParameter = {New SqlParameter("@q", q), New SqlParameter("@Activityid", Activityid), New SqlParameter("@ayid", ayid), New SqlParameter("@roleid", roleid), New SqlParameter("@teacherid", teacherid)}
        '  cmd.FillGrd(grd, cmd.getDataTable("[dbo].[Role_wise_College]", s, "tbl"))
        '  s = {New SqlParameter("@q", "Fee_Back_chartview"), New SqlParameter("@uid", teacherid)}
        Return cmd.getDataTablenew("[dbo].[Role_wise_College_rpt]", s, "tbl")

    End Function

    Public Sub Verify_role_wise_subject_backpaper(ByVal grd As GridView, ByVal q As String, ByVal Activityid As String, ByVal ayid As String, ByVal roleid As String, ByVal teacherid As String, ByVal collegeid As String, ByVal courseid As String, ByVal sem As String, ByVal pending As String, ByVal examname As String)
        Dim s() As SqlParameter = {New SqlParameter("@q", q), New SqlParameter("@Activityid", Activityid), New SqlParameter("@ayid", ayid), New SqlParameter("@roleid", roleid), New SqlParameter("@teacherid", teacherid), New SqlParameter("@collegeid", collegeid), New SqlParameter("@courseid", courseid), New SqlParameter("@sem", sem), New SqlParameter("@pending", pending), New SqlParameter("@examname", examname)}
        cmd.FillGrd(grd, cmd.getDataTable("[dbo].[Role_wise_verify_new_BackPaper]", s, "tbl"))
    End Sub
   
    Public Sub Verify_role_wise_marks_enter_teachername_backpaper(ByVal grd As GridView, ByVal q As String, ByVal Activityid As String, ByVal ayid As String, ByVal roleid As String, ByVal teacherid As String, ByVal collegeid As String, ByVal courseid As String, ByVal sem As String, ByVal pending As String, ByVal examname As String)
        Dim s() As SqlParameter = {New SqlParameter("@q", q), New SqlParameter("@Activityid", Activityid), New SqlParameter("@ayid", ayid), New SqlParameter("@roleid", roleid), New SqlParameter("@teacherid", teacherid), New SqlParameter("@collegeid", collegeid), New SqlParameter("@courseid", courseid), New SqlParameter("@sem", sem), New SqlParameter("@pending", pending), New SqlParameter("@examname", examname)}
        cmd.FillGrd(grd, cmd.getDataTable("[dbo].[Role_wise_verify_new_BackPaper]", s, "tbl"))
    End Sub

    Public Function Verify_role_wise_subject_update(ByVal rid As String, ByVal activityid As String, ByVal roleid As String, ByVal useridnew As String, ByVal lock As Integer, ByVal serial As Integer, ByVal userid As Integer, ByVal ayid As Integer) As String
        s = {New SqlParameter("@rid", rid), New SqlParameter("@Activityid", activityid), New SqlParameter("@roleid", roleid), New SqlParameter("@teacherid", useridnew), New SqlParameter("@lock", lock), New SqlParameter("@serial", serial), New SqlParameter("@userid", userid), New SqlParameter("@ayid", ayid)}
        '            cmd.FillGrd(grd, cmd.getDataTable("[dbo].[exam_master]", s, "tbl"))
        Return cmd.execStoredProcudureretstr("[dbo].[Role_wise_varify_ins]", s)
    End Function

    Public Function Role_wise_varify_ins_stu_sub_update(ByVal rid As String, ByVal activityid As String, ByVal roleid As String, ByVal useridnew As String, ByVal lock As Integer, ByVal serial As Integer, ByVal userid As Integer, ByVal ayid As Integer) As String
        s = {New SqlParameter("@rid", rid), New SqlParameter("@Activityid", activityid), New SqlParameter("@roleid", roleid), New SqlParameter("@teacherid", useridnew), New SqlParameter("@lock", lock), New SqlParameter("@serial", serial), New SqlParameter("@userid", userid), New SqlParameter("@ayid", ayid)}
        '            cmd.FillGrd(grd, cmd.getDataTable("[dbo].[exam_master]", s, "tbl"))
        Return cmd.execStoredProcudureretstr("[dbo].[Role_wise_varify_ins_stu_sub_update]", s)
    End Function
    Public Function Role_wise_varify_ins_stu_sub_updatebackpaper(ByVal rid As String, ByVal activityid As String, ByVal roleid As String, ByVal useridnew As String, ByVal lock As Integer, ByVal serial As Integer, ByVal userid As Integer, ByVal ayid As Integer, ByVal examname As String) As String
        s = {New SqlParameter("@rid", rid), New SqlParameter("@Activityid", activityid), New SqlParameter("@roleid", roleid), New SqlParameter("@teacherid", useridnew), New SqlParameter("@lock", lock), New SqlParameter("@serial", serial), New SqlParameter("@userid", userid), New SqlParameter("@ayid", ayid), New SqlParameter("@examname", examname)}
        '            cmd.FillGrd(grd, cmd.getDataTable("[dbo].[exam_master]", s, "tbl"))
        Return cmd.execStoredProcudureretstr("[dbo].[Role_wise_varify_ins_stu_sub_update_backpaper]", s)
    End Function
    Public Function Role_wise_varify_ins_stu_awardlist_update(ByVal rid As String, ByVal activityid As String, ByVal roleid As String, ByVal useridnew As String, ByVal lock As Integer, ByVal serial As Integer, ByVal userid As Integer, ByVal ayid As Integer) As String
        s = {New SqlParameter("@rid", rid), New SqlParameter("@Activityid", activityid), New SqlParameter("@roleid", roleid), New SqlParameter("@teacherid", useridnew), New SqlParameter("@lock", lock), New SqlParameter("@serial", serial), New SqlParameter("@userid", userid), New SqlParameter("@ayid", ayid)}
        '            cmd.FillGrd(grd, cmd.getDataTable("[dbo].[exam_master]", s, "tbl"))
        Return cmd.execStoredProcudureretstr("[dbo].[Role_wise_varify_ins_stu_awardlist_update]", s)
    End Function

    Public Function Verify_role_wise_examgenrage_update(ByVal rid As String, ByVal activityid As String, ByVal roleid As String, ByVal useridnew As String, ByVal lock As Integer, ByVal serial As Integer, ByVal userid As Integer, ByVal ayid As Integer) As String
        s = {New SqlParameter("@rid", rid), New SqlParameter("@Activityid", activityid), New SqlParameter("@roleid", roleid), New SqlParameter("@teacherid", useridnew), New SqlParameter("@lock", lock), New SqlParameter("@serial", serial), New SqlParameter("@userid", userid), New SqlParameter("@ayid", ayid)}
        '            cmd.FillGrd(grd, cmd.getDataTable("[dbo].[exam_master]", s, "tbl"))
        Return cmd.execStoredProcudureretstr("[dbo].[Role_wise_varify_ins_exam_gen]", s)
    End Function
    Public Function Role_back_activity_admin(ByVal rid As String, ByVal activityid As String, ByVal userid As Integer) As String
        s = {New SqlParameter("@rid", rid), New SqlParameter("@Activityid", activityid), New SqlParameter("@userid", userid)}
        '            cmd.FillGrd(grd, cmd.getDataTable("[dbo].[exam_master]", s, "tbl"))
        Return cmd.execStoredProcudureretstr("[dbo].[Role_back_activity_admin]", s)
    End Function
    Public Function Verify_role_wise_examgenrage_rollback(ByVal rid As String, ByVal activityid As String, ByVal roleid As String, ByVal useridnew As String, ByVal lock As Integer, ByVal serial As Integer, ByVal userid As Integer, ByVal ayid As Integer) As String
        s = {New SqlParameter("@rid", rid), New SqlParameter("@Activityid", activityid), New SqlParameter("@roleid", roleid), New SqlParameter("@teacherid", useridnew), New SqlParameter("@lock", lock), New SqlParameter("@serial", serial), New SqlParameter("@userid", userid), New SqlParameter("@ayid", ayid)}
        '            cmd.FillGrd(grd, cmd.getDataTable("[dbo].[exam_master]", s, "tbl"))
        Return cmd.execStoredProcudureretstr("[dbo].[Role_wise_varify_RollBack]", s)
    End Function
    Public Function Verify_role_wise_subject_update_exam(ByVal rid As String, ByVal lock As Integer, ByVal userid As Integer, ByVal ayid As Integer) As String
        s = {New SqlParameter("@rid", rid), New SqlParameter("@lock", lock), New SqlParameter("@userid", userid), New SqlParameter("@ayid", ayid)}
        '            cmd.FillGrd(grd, cmd.getDataTable("[dbo].[exam_master]", s, "tbl"))
        Return cmd.execStoredProcudureretstr("[dbo].[Role_wise_varify_exam_result_ins]", s)
    End Function
    Public Function Feebback_view_Chart(ByVal teacherid As Integer, ByVal sessionid As Integer) As DataTable
        s = {New SqlParameter("@q", "Fee_Back_chartview"), New SqlParameter("@uid", teacherid)}
        Return cmd.getDataTablenew("[dbo].[Att_rpt]", s, "tbl")
    End Function
    'examveryfyteacher.aspx
    Public Sub get_teacher_subject_marks_verify(ByVal grd As GridView, ByVal q As String, ByVal teacherid As String, ByVal ayid As String)
        Dim s() As SqlParameter = {New SqlParameter("@q", q), New SqlParameter("@uid", teacherid), New SqlParameter("@ayid", ayid)}
        cmd.FillGrd(grd, cmd.getDataTable("[dbo].[Att_assignsub]", s, "tbl"))
    End Sub
    ''sgpa manangement rpt
    Public Function sgpa_management(ByVal q As String, ByVal id As Integer) As DataTable
        '        Dim s() As SqlParameter = {New SqlParameter("@q", q), New SqlParameter("@instid", cmd.IsNull(Collegeid, DBNull.Value)), New SqlParameter("@cid", cmd.IsNull(courseid, DBNull.Value)), New SqlParameter("@sem", cmd.IsNull(sem, DBNull.Value)), New SqlParameter("@cls", cmd.IsNull(classesid, DBNull.Value)), New SqlParameter("@batchid", cmd.IsNull(batchid, DBNull.Value)), New SqlParameter("@evenodd", sessionid), New SqlParameter("@ayid", ayid), New SqlParameter("@isstruckoff", IIf(isstruckoff = "", DBNull.Value, isstruckoff))}
        Dim s() As SqlParameter = {New SqlParameter("@q", q), New SqlParameter("@Courseid", cmd.IsNull(id, DBNull.Value))}
        Return cmd.getDataTable("[dbo].[Exem_sgpa_new_management]", s, "tbl")
    End Function
End Class
