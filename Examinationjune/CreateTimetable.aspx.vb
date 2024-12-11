Imports System.Data
Imports System.Text
Imports System.Configuration
Imports System.Data.SqlClient
Imports dbnewcls
Partial Class HOD_CreateTimetable
    Inherits System.Web.UI.Page
    Dim constr As String = ConfigurationManager.ConnectionStrings("myconnection").ConnectionString
    Private cmd As dbnew = New dbnew()
    Private att As Attendance = New Attendance
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            lblAcademicyear.Text = Request.QueryString("acyr")
            lblprogram.Text = Request.QueryString("rid")

            fillddlsem()
            fillddlsubject()
            fillddlclass()
            fillddlclassroom()
            'fillddlgroup()

            ddlsubjects.SelectedIndex = 0
            fetchfaculty()
            Bindgrid(ViewState("Faculty"))
            filldllcombine()
        End If
       

    End Sub

    Private Sub fillddlsem()
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand()
                Dim query As String = "select Coursetype,case when Coursetype='Sem' then Duration*2 when Coursetype='Year' then Duration*1 end as semyear from Exam_Coursesession where Courseid='" & Request.QueryString("cid") & "' and Academicyear='" & Request.QueryString("acyr") & "'"
                cmd.CommandText = query
                Using sda As New SqlDataAdapter()
                    cmd.Connection = con
                    sda.SelectCommand = cmd
                    Using dt As New DataTable()
                        sda.Fill(dt)
                        ddlsemyear.DataSource = dt
                        Dim totalsem As String = dt.Rows(0)("semyear").ToString()
                        Dim Coursetype As String = dt.Rows(0)("Coursetype").ToString()

                        If Coursetype = "Sem" Then
                            lblsemyear.Text = "Semester :"
                        Else
                            lblsemyear.Text = "Year :"
                        End If

                        Dim i As Integer
                        For i = 1 To totalsem
                            ddlsemyear.Items.Add(New ListItem(i.ToString(), i.ToString()))

                        Next


                    End Using
                End Using
            End Using
        End Using
    End Sub

    Private Sub BindDropDownList1(ByVal ddl2 As DropDownList, ByVal query As String, ByVal text As String, ByVal value As String, ByVal defaultText As String)

        Dim cmd As New SqlCommand(query)
        Using con As New SqlConnection(constr)
            Using sda As New SqlDataAdapter()
                cmd.Connection = con
                con.Open()
                ddl2.DataSource = cmd.ExecuteReader()
                ddl2.DataTextField = text
                ddl2.DataValueField = value
                ddl2.DataBind()
                con.Close()
            End Using
        End Using

    End Sub

    Private Sub fillddlsubject()
        Dim query As String = "select s.Subject,s.Subjectid from Exam_Subject s left join Exam_Coursesubject cs on s.Subjectid=cs.Subjectid where cs.Courseid= '" & Request.QueryString("cid") & "'   and cs.Academicyear= '" & Request.QueryString("acyr") & "'   and cs.semyear='1'"
        BindDropDownList1(ddlsubjects, query, "Subject", "Subjectid", "")
      
    End Sub

    Private Sub fillddlclass()
        Dim query As String = "select ClassesID, Code  from Classes "
        BindDropDownList1(ddlclass, query, "Code", "ClassesID", "")
    End Sub

    Private Sub fillddlclassroom()
        Dim query As String = "SELECT classid,ClassRoom   FROM ClassRoom  "
        BindDropDownList1(ddlclassroom, query, "ClassRoom", "classid", "")
    End Sub

    Private Sub fillddlgroup()
        Dim query As String = "select code,code as codetxt from stdGroup where Gtype='g'"
        BindDropDownList1(ddlgroup, query, "codetxt", "code", "")
    End Sub

    Protected Sub btnsave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsave.Click
        Dim wd As New List(Of Integer)

        For Each item As System.Web.UI.WebControls.ListItem In ChkWeek.Items
            If item.Selected Then wd.Add(CInt(item.Value))
        Next


        'Attendance.Inserttimetable(ddlteacher.SelectedValue.ToString, "01/jan/2018", "30/Dec/2018", ddlfilter.getcourseid, ddlsem.SelectedValue.ToString, ddlclasses.SelectedValue.ToString, _
        '                                 cmbClassRoom.SelectedItem.ToString, cmbGroup.SelectedValue.ToString, cmbSubject.SelectedValue.ToString, _
        '                                  ddlcombine.SelectedValue.ToString, rblType.SelectedValue.ToString, wd, txtPeriod.Text, ddlelective.SelectedValue.ToString)



        If ddlcombine.SelectedIndex <> 0 Then


            Dim sql As String = ""
            sql = "select  case when COUNT(*) = (select COUNT(*) from TimeTableCombine t where t.combineid= '" & ddlcombine.SelectedValue.ToString & "' " & _
                " )  then 1 else 0 end as d  from TimeTableCombine t   inner join Exam_Coursesubject s on s.CourseID=t.CourseID and t.Sem=s.Semyear and t.ClassesID =s.Classesid  " & _
                    " and t.SessionID=s.SessionID and  " & ddlsubjects.SelectedItem.Value & " =s.SubjectID where t.combineid=  '" & ddlcombine.SelectedValue.ToString & "' " & _
                    " and s.SubjectID =" & ddlsubjects.SelectedItem.Value & " "
            Dim i As Integer
            i = cmd.execScalerint(sql)
            If i = 0 Then

                SaralMsg.Messagebx.Alert(Me, "Please Assign Subject in every Combined Courses")
                Exit Sub
            End If
        End If


        'Using con As New SqlConnection(constr)
        '    Using cmd As New SqlCommand()
        '        cmd.Connection = con
        '        For Each Item As Integer In wd


        '            Dim query As String = "        SELECT     TeacherID " & _
        '                    "FROM  TimeTable  " & _
        '                    "WHERE(TimeTable.FromDt = '21/jan/2018') AND (TimeTable.ToDt = '30/Dec/2019') AND (TimeTable.CourseID = '" & Request.QueryString("cid") & "') AND  " & _
        '                    "(TimeTable.Sem = '" & ddlsemyear.SelectedItem.Text & "') AND (TimeTable.ClassesID = '" & ddlclass.SelectedValue.ToString & "')  AND (TimeTable.SubjectID = '" & ddlsubjects.SelectedItem.Value & "') AND  " & _
        '                    "(TimeTable.Teach_Type = '" & rblType.SelectedValue.ToString & "') AND (TimeTable.WdNo = '" & Item & "') AND (TimeTable.Prd = '" & txtPeriod.Text & "') and  sessionid='" & Request.QueryString("s") & "' AND Grp='" & ddlgroup.SelectedItem.Value & "' and (TimeTable.evenodd = '" & Request.QueryString("e") & "')  "
        '            cmd.CommandText = query
        '            cmd.CommandType = CommandType.Text
        '            con.Open()
        '            Dim reader As SqlDataReader
        '            reader = cmd.ExecuteReader
        '            If reader.HasRows Then
        '                reader.Read()
        '                Throw New Exception("Already alot to ")
        '                Exit Sub
        '            End If
        '            con.Close()
        '        Next
        '    End Using

        'End Using
        'Dim count As Integer = 0
        'Using con As New SqlConnection(constr)
        '    Using cmd As New SqlCommand()
        '        cmd.Connection = con
        '        For Each Item As Integer In wd
        '            Dim query As String = "SELECT     TeacherID " & _
        '                    "FROM  TimeTable  " & _
        '                    "WHERE(TimeTable.FromDt = '21/jan/2018') AND (TimeTable.ToDt = '30/Dec/2019') AND (TimeTable.CourseID = '" & Request.QueryString("cid") & "') AND  " & _
        '                    "(TimeTable.Sem = '" & ddlsemyear.SelectedItem.Text & "') AND (TimeTable.ClassesID = '" & ddlclass.SelectedValue.ToString & "')  AND (TimeTable.Combineid = '" & ddlcombine.SelectedItem.Value & "') AND (TimeTable.SubjectID = '" & ddlsubjects.SelectedItem.Value & "') AND  " & _
        '                    "(TimeTable.Teach_Type = '" & rblType.SelectedValue.ToString & "') AND (TimeTable.WdNo = '" & Item & "') AND (TimeTable.Prd = '" & txtPeriod.Text & "') and  sessionid='" & Request.QueryString("s") & "' AND Grp='" & ddlgroup.SelectedItem.Value & "' and (TimeTable.evenodd = '" & Request.QueryString("e") & "')  "
        '            cmd.CommandText = query
        '            cmd.CommandType = CommandType.Text
        '            con.Open()
        '            Dim reader As SqlDataReader = cmd.ExecuteReader
        '            If reader.HasRows Then
        '                count = reader(0).ToString
        '            End If
        '            con.Close()
        '        Next
        '    End Using

        'End Using

        'If count <> 0 Then
        '    Throw New Exception("Already alot to " + GetTeacher(count))
        'Else
        Insert(ViewState("Faculty"), wd)

        SaralMsg.Messagebx.Alert(Me, "Successfully Insert")
        ChkWeek.ClearSelection()
        txtPeriod.Text = ""

        ddlgroup.SelectedIndex = 0
        'End If




        'att.Inserttimetable(Faculty, "21/jan/2018", "30/Dec/2019", Request.QueryString("cid"), ddlsemyear.SelectedItem.Text, ddlclass.SelectedValue.ToString, ddlclassroom.SelectedItem.ToString, ddlgroup.SelectedItem.Value, ddlsubjects.SelectedItem.Value, rblType.SelectedValue.ToString, wd, txtPeriod.Text)
        'IIf(ddlclassroom.SelectedValue = "", DBNull.Value.ToString, ), IIf(ddlgroup.SelectedValue = "", DBNull.Value.ToString, ), _






    End Sub

    Public Function GetTeacher(ByVal id As Integer) As String
        Dim sql As String
        sql = "select username from users where userid='" & id & "'"

        Dim str As String = cmd.execScaler(sql)
        Return str
    End Function

    Private Sub Insert(ByVal Faculty As String, ByVal WdNo As List(Of Integer))
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand()
                cmd.Connection = con
                For Each Item As Integer In WdNo
                    Dim query As String = "INSERT INTO TimeTable " & _
                    "(evenodd,sessionid,TeacherID, FromDt, ToDt, CourseID, Sem, ClassesID, ClassRoom, Grp, SubjectID, Teach_Type, WdNo, Prd, userid,Combineid) " & _
                    "VALUES     ('" & Request.QueryString("e") & "','" & Request.QueryString("s") & "','" & Faculty & "','21/jan/2018','30/Dec/2019','" & Request.QueryString("cid") & "','" & ddlsemyear.SelectedItem.Text & "','" & ddlclass.SelectedValue.ToString & "','" & ddlclassroom.SelectedItem.Value & "','','" & ddlsubjects.SelectedItem.Value & "','" & rblType.SelectedValue.ToString & "','" & Item & "','" & txtPeriod.Text & "','" & Request.QueryString("u") & "','" & ddlcombine.SelectedItem.Value & "') "
                    cmd.CommandText = query
                    cmd.CommandType = CommandType.Text 
                    con.Open()
                    cmd.ExecuteNonQuery()
                    con.Close()
                Next
            End Using

        End Using

        Bindgrid(ViewState("Faculty"))

    End Sub

    Private Sub Bindgrid(ByVal Faculty As String)
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand()

                Dim query As String = " select Wd as dayname ,[1]as I,[2] as II,[3] as III,[4] as IV, [5] as V,[6] as VI,[7] as VII,[8] as VIII,[9] as IX, [10] as X, [11] as XI  from wday ww " & _
                   " left join ( " & _
                    "select * from ( " & _
                   " SELECT t.wdno, t.prd ,u.usernamefull + '<br />'+ isnull(c.COurse,'<d>Select Course</d>') + '/' + convert(varchar,isnull(t.sem,'<d>Select Sem</d>')) +'<br />'+ isnull(t.classroom,'<d>select Classroom</d>') +'/' + convert(varchar,isnull(s.Subjectcode,'<d>Change Subject</d>')) + '<br />'+ isnull((select max(combinename) from timetablecombine where combineid=t.combineid),'') +'<br />'+ isnull(cl.Code ,'<d>select Section</d>') +'<br />'+ isnull(t.Grp,'<d>select Grp</d>') +'<br />'+ isnull(t.Teach_Type,'<d>select S_Type</d>') +'<br /> <b>'+ '' + '</b>' as sub      " & _
                   "  FROM TimeTable t  " & _
                   " left join users u on u.userid = t.teacherid " & _
                  "  left join Exam_course c on c.courseid=t.courseid " & _
                   " left join classes cl on cl.classesid=t.classesid " & _
                   " left join Exam_Coursesubject s on s.subjectid=t.subjectid and t.courseid=s.courseid and t.sem=s.Semyear" & _
                   " left join Exam_Subject ss on ss.Subjectid=s.Subjectid" & _
               " WHERE(t.TeacherID = '" & Faculty & "' And t.evenodd = '" & Request.QueryString("e") & "' And t.sessionid = '" & Request.QueryString("s") & "')" & _
                  "  ) as t1 " & _
                  "  pivot  (max(t1.sub) for t1.prd  in ([1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11]))as td  " & _
                     "   ) as  p on ww.wdid =p.wdno"

                cmd.CommandText = query
                Using sda As New SqlDataAdapter()
                    cmd.Connection = con
                    sda.SelectCommand = cmd
                    Using dt As New DataTable()
                        sda.Fill(dt)
                        GridView1.DataSource = dt
                        GridView1.DataBind()
                    End Using
                End Using
            End Using
        End Using

        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand()

                Dim query As String = "SELECT  " & _
         "( select max(combinename) from timetablecombine ttc where ttc.combineid=timetable.combineid ) as combinename,  " & _
"timetable.timetableid, Exam_Coursesubject.Subjectcode AS Subject,  Classes.Code AS Classes ,  Exam_Course.Course AS Course, Wday.Wd, TimeTable.CourseID, TimeTable.Sem, TimeTable.ClassesID, " & _
        " TimeTable.ClassRoom,TimeTable.TeacherID,convert(varchar,TimeTable.Fromdt,106) as Fromdt, convert(varchar,TimeTable.todt,106) as todt, TimeTable.Grp, TimeTable.SubjectID, TimeTable.Teach_Type, TimeTable.WdNo, TimeTable.Prd ,u.username as TimetableCreator  " & _
        " FROM timetable TimeTable  INNER JOIN " & _
        "Classes on TimeTable.ClassesID=Classes.ClassesID inner join" & _
       " Exam_Course ON TimeTable.CourseID = Exam_Course.CourseID INNER JOIN  " & _
         "Exam_Coursesubject ON TimeTable.SubjectID = Exam_Coursesubject.SubjectID and timetable.sem=Exam_Coursesubject.Semyear and Exam_Coursesubject.courseid=timetable.courseid   INNER JOIN" & _
        " Wday ON TimeTable.WdNo = Wday.WdID inner join Users u on u.userid=TimeTable.userid  " & _
         "WHERE (TimeTable.TeacherID = '" & Faculty & "' and TimeTable.evenodd='" & Request.QueryString("e") & "' and TimeTable.sessionid='" & Request.QueryString("s") & "' )order by TimeTable.WdNo ,Prd  "


                cmd.CommandText = query
                Using sda As New SqlDataAdapter()
                    cmd.Connection = con
                    sda.SelectCommand = cmd
                    Using dt As New DataTable()
                        sda.Fill(dt)
                        GridView3.DataSource = dt
                        GridView3.DataBind()
                    End Using
                End Using
            End Using
        End Using


    End Sub

    Private Sub fetchfaculty()
        Dim Faculty As String = ""
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand()
                Dim query As String = "Select Distinct e.Employeeid from P_Employee e inner join Exam_SubjectPlan sp on sp.Facultyid=e.EmployeeID  where sp.Courseid='" & Request.QueryString("cid") & "' and sp.Academicyear='" & Request.QueryString("acyr") & "' and sp.Subjectid='" & ddlsubjects.SelectedItem.Value & "'"
                cmd.CommandText = query
                Using sda As New SqlDataAdapter()
                    cmd.Connection = con
                    sda.SelectCommand = cmd
                    Using dt As New DataTable()
                        sda.Fill(dt)

                        Faculty = dt.Rows(0)("Employeeid").ToString()
                        ViewState("Faculty") = Faculty
                    End Using
                End Using
            End Using
        End Using
    End Sub

    Protected Sub backbotton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles backbotton.Click
        Response.Redirect("Dashboard.aspx?s=" & Request.QueryString("s") & "&e=" & Request.QueryString("e") & "&u=" & Request.QueryString("u"))
    End Sub

    Private Sub filldllcombine()
        Dim query As String = "SELECT DISTINCT Combineid, CombineName FROM TimeTableCombine WHERE (evenodd = " & Request.QueryString("e") & ") AND (SessionID = " & Request.QueryString("s") & ") ORDER BY CombineName"
        BindDropDownList1(ddlcombine, query, "combinename", "combineid", "")
        Dim list As ListItem = New ListItem()
        list.Text = "Select"
        list.Value = "0"
        ddlcombine.Items.Insert(0, list)
    End Sub

    Protected Sub ddlsubjects_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlsubjects.SelectedIndexChanged

        fetchfaculty()
        Bindgrid(ViewState("Faculty"))
    End Sub

    Protected Sub btnaddcombine_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnaddcombine.Click
        Response.Redirect("../HOD/TTCombine.aspx?s=" & Request.QueryString("s") & "&e=" & Request.QueryString("e") & "&u=" & Request.QueryString("u"))
    End Sub
End Class
