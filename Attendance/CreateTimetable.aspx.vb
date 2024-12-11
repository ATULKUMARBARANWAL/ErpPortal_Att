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
            'txttodate.Attributes("min") = DateTime.Now.ToString("yyyy-MM-dd")
            'txtfromdate.Attributes("min") = DateTime.Now.ToString("yyyy-MM-dd")
            lblAcademicyear.Text = Request.QueryString("acyr")
            lblprogram.Text = Request.QueryString("coursename")

            Lblsection.Text = Request.QueryString("Classid")
            Lblsxn.Text = Request.QueryString("Section")
            ViewState("ayid") = Request.QueryString("ay")
            ViewState("cid") = Request.QueryString("cid")
            'fillddlsem()
            fillddlsubject()
            fillddlclass()

            Dim item As ListItem = ddlclass.Items.FindByText(Lblsxn.Text)
            If item IsNot Nothing Then
                item.Selected = True
            Else
                ' Optionally, log or handle the case where the item is not found
                ' lblError.Text = "Class not found in the dropdown list."
            End If


            fillddlclassroom()
            fillddlgroup()

            ddlsubjects.SelectedIndex = 0
            fetchfaculty()
            Bindgrid(ViewState("Faculty"))
            filldllcombine()
        End If


    End Sub

    'Private Sub fillddlsem()
    '    Using con As New SqlConnection(constr)
    '        Using cmd As New SqlCommand()
    '            Dim query As String = "select Coursetype,case when Coursetype='Sem' then Duration*2 when Coursetype='Year' then Duration*1 end as semyear from Exam_Coursesession where Courseid='" & Request.QueryString("cid") & "' and Academicyear='" & Request.QueryString("acyr") & "'"
    '            cmd.CommandText = query
    '            Using sda As New SqlDataAdapter()
    '                cmd.Connection = con
    '                sda.SelectCommand = cmd
    '                Using dt As New DataTable()
    '                    sda.Fill(dt)
    '                    ddlsemyear.DataSource = dt
    '                    Dim totalsem As String = dt.Rows(0)("semyear").ToString()
    '                    Dim Coursetype As String = dt.Rows(0)("Coursetype").ToString()

    '                    If Coursetype = "Sem" Then
    '                        lblsemyear.Text = "Semester :"
    '                    Else
    '                        lblsemyear.Text = "Year :"
    '                    End If

    '                    Dim i As Integer
    '                    For i = 1 To totalsem
    '                        ddlsemyear.Items.Add(New ListItem(i.ToString(), i.ToString()))

    '                    Next


    '                End Using
    '            End Using
    '        End Using
    '    End Using
    'End Sub

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
        Dim query As String = "select s.Subject,s.Subjectid from Exam_Subject s left join Exam_Coursesubject cs on s.Subjectid=cs.Subjectid where cs.Courseid= '" & Request.QueryString("cid") & "'   and cs.Academicyear= '" & Request.QueryString("acyr") & "' "
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
        Dim query As String = "select Groupname,Value as codetxt from Groups "
        BindDropDownList1(ddlgroup, query, "codetxt", "Groupname", "")
    End Sub

    Protected Sub btnsave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsave.Click
        Dim wd As New List(Of Integer)

        For Each item As System.Web.UI.WebControls.ListItem In ChkWeek.Items
            If item.Selected Then wd.Add(CInt(item.Value))
        Next

        If ddlcombine.SelectedIndex <> 0 Then
            Dim sql As String = ""
            sql = "SELECT CASE WHEN COUNT(*) = (SELECT COUNT(*) FROM TimeTableCombine t WHERE t.combineid= '" & ddlcombine.SelectedValue.ToString & "') THEN 1 ELSE 0 END AS d " & _
                  "FROM TimeTableCombine t INNER JOIN Exam_Coursesubject s ON s.CourseID=t.CourseID AND t.Sem=s.Semyear AND t.ClassesID =s.Classesid " & _
                  "AND t.SessionID=s.SessionID AND " & ddlsubjects.SelectedItem.Value & " =s.SubjectID WHERE t.combineid=  '" & ddlcombine.SelectedValue.ToString & "' " & _
                  "AND s.SubjectID =" & ddlsubjects.SelectedItem.Value & " "
            Dim i As Integer
            i = cmd.execScalerint(sql)
            If i = 0 Then
                SaralMsg.Messagebx.Alert(Me, "Please Assign Subject in every Combined Courses")
                Exit Sub
            End If
        End If

        Dim count As Integer = 0
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand()
                cmd.Connection = con
                For Each Item As Integer In wd
                    Dim query As String = "SELECT TeacherID " & _
                                          "FROM TimeTable " & _
                                          "WHERE(TimeTable.FromDt Between '" & txtfromdate.Text & "' AND '" & txttodate.Text & "') AND (TimeTable.CourseID = '" & Request.QueryString("cid") & "') AND " & _
                                          "(TimeTable.ClassesID = '" & ddlclass.SelectedValue.ToString & "') AND (TimeTable.Combineid = '" & ddlcombine.SelectedItem.Value & "') AND (TimeTable.SubjectID = '" & ddlsubjects.SelectedItem.Value & "') AND " & _
                                          "(TimeTable.Teach_Type = '" & rblType.SelectedValue.ToString & "') AND (TimeTable.WdNo = '" & Item & "') AND (TimeTable.Prd = '" & ddlperiod.SelectedItem.Text & "') AND sessionid='" & Request.QueryString("s") & "' AND (TimeTable.evenodd = '" & Request.QueryString("e") & "')"
                    cmd.CommandText = query
                    cmd.CommandType = CommandType.Text
                    con.Open()
                    Using sda As New SqlDataAdapter()
                        sda.SelectCommand = cmd
                        Using dt As New DataTable()
                            sda.Fill(dt)
                            If dt.Rows.Count > 0 Then
                                count = dt.Rows(0)("TeacherID").ToString()
                                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alert", "showCustomAlert('Already  allotted to " & GetTeacher(count) & " from " & txtfromdate.Text & " to " & txttodate.Text & " for the same period.');", True)

                                Exit Sub
                            End If
                            con.Close()
                        End Using
                    End Using
                Next

                Dim count1 As Integer = 0
                Dim query1 As String = "SELECT Count(*) as Counting " & _
                                       "FROM TimeTable " & _
                                       "WHERE('" & txtfromdate.Text & "' between TimeTable.FromDt and TimeTable.ToDt ) AND (TimeTable.CourseID = '" & Request.QueryString("cid") & "') AND " & _
                                       "sessionid='" & Request.QueryString("s") & "' AND (timetable.wdno = '" & ChkWeek.SelectedValue & "') AND (TimeTable.evenodd = '" & Request.QueryString("e") & "') AND (TimeTable.Prd = '" & ddlperiod.SelectedItem.Text & "') AND Grp = '" & ddlgroup.SelectedValue & "'"
                cmd.CommandText = query1
                cmd.CommandType = CommandType.Text
                con.Open()
                Using sda1 As New SqlDataAdapter()
                    sda1.SelectCommand = cmd
                    Using dt1 As New DataTable()
                        sda1.Fill(dt1)
                        If dt1.Rows.Count > 0 Then
                            count1 = dt1.Rows(0)("Counting").ToString()
                            If count1 > 0 Then

                                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alert", "showCustomAlert('Table is Already Exist');", True)
                                Exit Sub
                            Else
                                ' Here you can add the check for the Aadhar number
                                ' Example: If aadharExists Then
                                '   ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alert", "showCustomAlert('Aadhar number already exists. Please use another.');", True)
                                '   Exit Sub
                                ' End If

                                Insert(ViewState("Faculty"), wd)


                                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alert", "showCustomAlert('Successfully Assigned.');", True)
                                ChkWeek.ClearSelection()
                                ddlgroup.SelectedIndex = 0
                            End If
                        End If
                        con.Close()
                    End Using
                End Using
            End Using
        End Using
    End Sub


    Public Function GetTeacher(ByVal id As Integer) As String
        Dim sql As String
        sql = "select usernamefull from users where userid='" & id & "'"

        Dim str As String = cmd.execScaler(sql)
        Return str
    End Function

    Private Sub Insert(ByVal Faculty As String, ByVal WdNo As List(Of Integer))

        If ddlgroup.SelectedItem.Text = "All Group" Then

            Using con As New SqlConnection(constr)
                Using cmd As New SqlCommand()
                    cmd.Connection = con
                    For Each Item As Integer In WdNo
                        Dim query As String = "INSERT INTO TimeTable " & _
                        "(evenodd,sessionid,TeacherID, FromDt, ToDt, CourseID, Sem, ClassesID, ClassRoom, Grp, SubjectID, Teach_Type, WdNo, Prd, userid,Combineid) " & _
                        "VALUES     ('" & Request.QueryString("e") & "','" & Request.QueryString("s") & "','" & Faculty & "','" & txtfromdate.Text & "','" & txttodate.Text & "','" & Request.QueryString("cid") & "','" & Request.QueryString("Sem") & "','" & ddlclass.SelectedValue.ToString & "','" & ddlclassroom.SelectedItem.Value & "','','" & ddlsubjects.SelectedItem.Value & "','" & rblType.SelectedValue.ToString & "','" & Item & "','" & ddlperiod.SelectedItem.Text & "','" & Request.QueryString("u") & "','" & ddlcombine.SelectedItem.Value & "') "
                        cmd.CommandText = query
                        cmd.CommandType = CommandType.Text
                        con.Open()
                        cmd.ExecuteNonQuery()
                        con.Close()
                    Next
                End Using

            End Using

            Bindgrid(ViewState("Faculty"))
        Else


            Using con As New SqlConnection(constr)
                Using cmd As New SqlCommand()
                    cmd.Connection = con
                    For Each Item As Integer In WdNo
                        Dim query As String = "INSERT INTO TimeTable " & _
                        "(evenodd,sessionid,TeacherID, FromDt, ToDt, CourseID,  ClassesID, ClassRoom, Grp, SubjectID, Teach_Type, WdNo, Prd, userid,Combineid) " & _
                        "VALUES     (Null,'" & Request.QueryString("s") & "','" & Faculty & "','" & txtfromdate.Text & "','" & txttodate.Text & "','" & Request.QueryString("cid") & "','" & ddlclass.SelectedValue.ToString & "','" & ddlclassroom.SelectedItem.Value & "','" & ddlgroup.SelectedItem.Text & "','" & ddlsubjects.SelectedItem.Value & "','" & rblType.SelectedValue.ToString & "','" & Item & "','" & ddlperiod.SelectedItem.Text & "','" & Request.QueryString("u") & "','" & ddlcombine.SelectedItem.Value & "') "
                        cmd.CommandText = query
                        cmd.CommandType = CommandType.Text
                        con.Open()
                        cmd.ExecuteNonQuery()
                        con.Close()
                    Next
                End Using

            End Using

            Bindgrid(ViewState("Faculty"))
        End If
    End Sub

    Private Sub Bindgrid(ByVal Faculty As String)
        Dim courseId = Request.QueryString("cid")
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand()

                Dim query As String = " select  Wd as dayname ,[1]as I,[2] as II,[3] as III,[4] as IV, [5] as V,[6] as VI,[7] as VII,[8] as VIII,[9] as IX, [10] as X, [11] as XI  from wday ww " & _
                   " left join ( " & _
                    "select * from ( " & _
                   " SELECT t.wdno, t.prd , '<label Style=""display:none;"">'+ Convert(varchar,t.Timetableid) + '</label>' +'<br />'+  Convert(nvarchar,t.FromDt,106)+ '&nbsp;' + 'to'+ '<br />' + Convert(nvarchar,t.ToDt,106) + '<br />' + u.usernamefull +'<br />' + isnull(cr.classroom,'<d>select Classroom</d>') +'<br />' + convert(varchar,isnull(ss.Subject COLLATE DATABASE_DEFAULT,'<d>Change Subject</d>')) +'<br />'+ isnull(cl.Code ,'<d>select Section</d>') +'<br />'+ isnull(t.Teach_Type,'<d>select S_Type</d>') +'<br />'+ ISNULL(CASE  WHEN t.Grp = '' THEN 'All groups'  ELSE t.Grp End,'<d>select S_Type</d>')   +'<br />'+ isnull((select max(combinename) from timetablecombine where combineid=t.combineid),'') +'<br /> <b>'+ '' + '</b>'  as sub      " & _
                   "  FROM TimeTable t  " & _
                   " left join users u on u.userid = t.teacherid " & _
                  "  left join Exam_course c on c.courseid=t.courseid " & _
                   " left join classes cl on cl.classesid=t.classesid " & _
                   " left join ClassRoom cr on cr.classid=t.ClassRoom" & _
                   " left join Exam_Coursesubject s on s.subjectid=t.subjectid and t.courseid=s.courseid and t.sem=s.Semyear" & _
                   " left join Exam_Subject ss on ss.Subjectid=s.Subjectid" & _
               " WHERE(  t.courseid = '" & Request.QueryString("cid") & "'  and t.classesid='" & ddlclass.SelectedValue & "' And t.sessionid = '" & Request.QueryString("s") & "' and t.ToDt >= '" & Date.Now & "')" & _
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
         "WHERE (TimeTable.TeacherID = '" & Faculty & "'  and TimeTable.sessionid='" & Request.QueryString("s") & "' )order by TimeTable.WdNo ,Prd  "


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
        Try
            Dim Faculty As String = ""

            ' Check if the required query string parameters are present
            Dim courseId = Request.QueryString("cid")
            Dim academicYear = Request.QueryString("acyr")
            Dim subjectId = If(ddlsubjects.SelectedItem IsNot Nothing, ddlsubjects.SelectedItem.Value, String.Empty)

            If String.IsNullOrEmpty(courseId) OrElse String.IsNullOrEmpty(academicYear) OrElse String.IsNullOrEmpty(subjectId) Then
                SaralMsg.Messagebx.Alert(Me, "Required parameters are missing.")
                Return
            End If

            Using con As New SqlConnection(constr)
                Using cmd As New SqlCommand()
                    Dim query As String = "SELECT DISTINCT e.EmployeeID " & _
                                          "FROM P_Employee e " & _
                                          "INNER JOIN Exam_SubjectPlan sp ON sp.Facultyid = e.EmployeeID " & _
                                          "WHERE sp.Courseid = @CourseID " & _
                                          "AND sp.Academicyear = @AcademicYear " & _
                                          "AND sp.Subjectid = @SubjectID"

                    cmd.CommandText = query
                    cmd.Parameters.AddWithValue("@CourseID", courseId)
                    cmd.Parameters.AddWithValue("@AcademicYear", academicYear)
                    cmd.Parameters.AddWithValue("@SubjectID", subjectId)

                    Using sda As New SqlDataAdapter(cmd)
                        cmd.Connection = con
                        Using dt As New DataTable()
                            sda.Fill(dt)

                            ' Check if there are rows returned
                            If dt.Rows.Count > 0 Then
                                Faculty = dt.Rows(0)("EmployeeID").ToString()
                                ViewState("Faculty") = Faculty
                            Else
                                SaralMsg.Messagebx.Alert(Me, "Employee not assigned to this Subject")
                                btnsave.Visible = False
                                ddlsubjects.SelectedIndex = 0
                            End If
                        End Using
                    End Using
                End Using
            End Using

        Catch ex As Exception
            ' Log the exception for debugging purposes if necessary
            SaralMsg.Messagebx.Alert(Me, "First map Subject to this Program")
        End Try
    End Sub


    Protected Sub backbotton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles backbotton.Click
        Response.Redirect("../Attendance/Timetable.aspx?s=" & Request.QueryString("s") & "&cid=" & Request.QueryString("cid") & "&acyr=" & Request.QueryString("acyr") & "&e=" & Request.QueryString("e") & "&u=" & Request.QueryString("u") & "&rid=" & Request.QueryString("rid") & "&ay=" & ViewState("ayid"))
    End Sub

    Private Sub filldllcombine()
        Dim query As String = "SELECT DISTINCT Combineid, CombineName FROM TimeTableCombine  ORDER BY CombineName"
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

    Protected Sub GridView1_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView1.RowCommand
        txtfromdate.ReadOnly = True
        txttodate.ReadOnly = True
        ddlclass.Enabled = False
        ddlcombine.Enabled = False
        ddlperiod.Enabled = False
        ChkWeek.Enabled = False
        ddlclassroom.Enabled = False

        btnsave.Visible = False
        Btnupdate.Visible = True
        btndelete.Visible = True
        Dim Item As String
        Dim ItemName As String
        If e.CommandName = "I" Then

            GridView1.SelectedIndex = e.CommandArgument
            Dim rowIndex As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = GridView1.Rows(rowIndex)
            Item = TryCast(row.FindControl("LinkI"), LinkButton).Text
            ItemName = Item.Substring(29, Item.IndexOf("</label>") - 29)
            fetchtimetable(ItemName)
            ViewState("Timetableid") = ItemName
        End If

        If e.CommandName = "II" Then

            GridView1.SelectedIndex = e.CommandArgument
            Dim rowIndex As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = GridView1.Rows(rowIndex)
            Item = TryCast(row.FindControl("LinkII"), LinkButton).Text
            ItemName = Item.Substring(29, Item.IndexOf("</label>") - 29)

            fetchtimetable(ItemName)
            ViewState("Timetableid") = ItemName
        End If

        If e.CommandName = "III" Then

            GridView1.SelectedIndex = e.CommandArgument
            Dim rowIndex As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = GridView1.Rows(rowIndex)
            Item = TryCast(row.FindControl("LinkIII"), LinkButton).Text
            ItemName = Item.Substring(29, Item.IndexOf("</label>") - 29)

            fetchtimetable(ItemName)
            ViewState("Timetableid") = ItemName
        End If

        If e.CommandName = "IV" Then

            GridView1.SelectedIndex = e.CommandArgument
            Dim rowIndex As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = GridView1.Rows(rowIndex)
            Item = TryCast(row.FindControl("LinkIV"), LinkButton).Text
            ItemName = Item.Substring(29, Item.IndexOf("</label>") - 29)

            fetchtimetable(ItemName)
            ViewState("Timetableid") = ItemName
        End If

        If e.CommandName = "V" Then

            GridView1.SelectedIndex = e.CommandArgument
            Dim rowIndex As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = GridView1.Rows(rowIndex)
            Item = TryCast(row.FindControl("LinkV"), LinkButton).Text
            ItemName = Item.Substring(29, Item.IndexOf("</label>") - 29)

            fetchtimetable(ItemName)
            ViewState("Timetableid") = ItemName
        End If

        If e.CommandName = "VI" Then

            GridView1.SelectedIndex = e.CommandArgument
            Dim rowIndex As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = GridView1.Rows(rowIndex)
            Item = TryCast(row.FindControl("LinkVI"), LinkButton).Text
            ItemName = Item.Substring(29, Item.IndexOf("</label>") - 29)

            fetchtimetable(ItemName)
            ViewState("Timetableid") = ItemName
        End If

        If e.CommandName = "VII" Then

            GridView1.SelectedIndex = e.CommandArgument
            Dim rowIndex As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = GridView1.Rows(rowIndex)
            Item = TryCast(row.FindControl("LinkVII"), LinkButton).Text
            ItemName = Item.Substring(29, Item.IndexOf("</label>") - 29)

            fetchtimetable(ItemName)
            ViewState("Timetableid") = ItemName
        End If

        If e.CommandName = "VIII" Then

            GridView1.SelectedIndex = e.CommandArgument
            Dim rowIndex As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = GridView1.Rows(rowIndex)
            Item = TryCast(row.FindControl("LinkVIII"), LinkButton).Text
            ItemName = Item.Substring(29, Item.IndexOf("</label>") - 29)

            fetchtimetable(ItemName)
            ViewState("Timetableid") = ItemName
        End If

        If e.CommandName = "IX" Then

            GridView1.SelectedIndex = e.CommandArgument
            Dim rowIndex As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = GridView1.Rows(rowIndex)
            Item = TryCast(row.FindControl("LinkIX"), LinkButton).Text
            ItemName = Item.Substring(29, Item.IndexOf("</label>") - 29)

            fetchtimetable(ItemName)
            ViewState("Timetableid") = ItemName
        End If

        If e.CommandName = "X" Then

            GridView1.SelectedIndex = e.CommandArgument
            Dim rowIndex As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = GridView1.Rows(rowIndex)
            Item = TryCast(row.FindControl("LinkX"), LinkButton).Text
            ItemName = Item.Substring(29, Item.IndexOf("</label>") - 29)

            fetchtimetable(ItemName)
            ViewState("Timetableid") = ItemName
        End If


        If e.CommandName = "XI" Then

            GridView1.SelectedIndex = e.CommandArgument
            Dim rowIndex As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = GridView1.Rows(rowIndex)
            Item = TryCast(row.FindControl("LinkXI"), LinkButton).Text
            ItemName = Item.Substring(29, Item.IndexOf("</label>") - 29)

            fetchtimetable(ItemName)
            ViewState("Timetableid") = ItemName
        End If

    End Sub

    'Private Sub fetchtimetable(ByVal p1 As Object)
    '    Throw New NotImplementedException
    'End Sub

    Private Sub fetchtimetable(ByVal ItemName As String)
        Using con As New SqlConnection(constr)
            Dim cmd As New SqlCommand()
            Dim sql As String = " Select * from Timetable where timetableid='" & ItemName & "' "
            cmd.CommandText = sql
            cmd.Connection = con
            con.Open()
            Dim sdr As SqlDataReader = cmd.ExecuteReader
            While (sdr.Read())

                txtfromdate.Text = Convert.ToDateTime(sdr("FromDt")).ToString("yyyy-MM-dd")
                txttodate.Text = Convert.ToDateTime(sdr("ToDt")).ToString("yyyy-MM-dd")
                ddlsubjects.SelectedValue = sdr("SubjectID").ToString()
                ddlclass.SelectedValue = sdr("ClassesID").ToString()
                ddlclassroom.SelectedValue = sdr("ClassRoom").ToString()
                rblType.SelectedValue = sdr("Teach_Type").ToString()
                ChkWeek.SelectedValue = sdr("WdNo").ToString()
                ddlperiod.SelectedValue = sdr("Prd").ToString()
            End While
            con.Close()

        End Using
    End Sub


    Protected Sub Btnupdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Btnupdate.Click
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


    

        Dim count As Integer = 0
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand()
                cmd.Connection = con
                For Each Item As Integer In wd
                    Dim query As String = "SELECT     TeacherID " & _
                            "FROM  TimeTable  " & _
                            "WHERE(TimeTable.FromDt Between '" & txtfromdate.Text & "' AND '" & txttodate.Text & "') AND (TimeTable.CourseID = '" & Request.QueryString("cid") & "') AND  " & _
                            "(TimeTable.Sem = '" & Request.QueryString("Sem") & "') AND (TimeTable.ClassesID = '" & ddlclass.SelectedValue.ToString & "')  AND (TimeTable.Combineid = '" & ddlcombine.SelectedItem.Value & "') AND (TimeTable.SubjectID = '" & ddlsubjects.SelectedItem.Value & "') AND  " & _
                            "(TimeTable.Teach_Type = '" & rblType.SelectedValue.ToString & "') AND (TimeTable.WdNo = '" & Item & "') AND (TimeTable.Prd = '" & ddlperiod.SelectedItem.Text & "') and  sessionid='" & Request.QueryString("s") & "' and (TimeTable.evenodd = '" & Request.QueryString("e") & "')  "


                    cmd.CommandText = query
                    cmd.CommandType = CommandType.Text
                    con.Open()
                    Using sda As New SqlDataAdapter()

                        sda.SelectCommand = cmd
                        Using dt As New DataTable()
                            sda.Fill(dt)
                            If dt.Rows.Count Then
                                count = dt.Rows(0)("TeacherID").ToString()
                                SaralMsg.Messagebx.Alert(Me, "Already alot to " + GetTeacher(count) + " from " + txtfromdate.Text + " to " + txttodate.Text + " for same period.")
                                Exit Sub
                            End If

                            con.Close()
                        End Using
                    End Using
                Next
            End Using

        End Using

        'If count <> 0 Then
        '    SaralMsg.Messagebx.Alert(Me, "Already alot to " + GetTeacher(count) + " for " + txtfromdate.Text)
        'Else
        update(ViewState("Faculty"), wd)


        SaralMsg.Messagebx.Alert(Me, "Successfully Update")
        btnsave.Visible = True
        Btnupdate.Visible = False

        ChkWeek.ClearSelection()
        'txtPeriod.Text = ""

        ddlgroup.SelectedIndex = 0
        'End If




        'att.Inserttimetable(Faculty, "21/jan/2018", "30/Dec/2019", Request.QueryString("cid"), ddlsemyear.SelectedItem.Text, ddlclass.SelectedValue.ToString, ddlclassroom.SelectedItem.ToString, ddlgroup.SelectedItem.Value, ddlsubjects.SelectedItem.Value, rblType.SelectedValue.ToString, wd, txtPeriod.Text)
        'IIf(ddlclassroom.SelectedValue = "", DBNull.Value.ToString, ), IIf(ddlgroup.SelectedValue = "", DBNull.Value.ToString, ), _





    End Sub

    Private Sub update(ByVal Faculty As Object, ByVal wd As List(Of Integer))
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand()
                cmd.Connection = con
                For Each Item As Integer In wd
                    Dim query As String = "update Timetable " & _
                    "set TeacherID= '" & Faculty & "', ClassRoom= '" & ddlclassroom.SelectedItem.Value & "',Grp= '" & ddlgroup.SelectedItem.Text & "', SubjectID= '" & ddlsubjects.SelectedItem.Value & "' where Timetableid='" & ViewState("Timetableid") & "'"
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

    Protected Sub ddlclass_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlclass.SelectedIndexChanged
        fetchfaculty()
        Bindgrid(ViewState("Faculty"))
    End Sub


    Protected Sub btndelete_Click(sender As Object, e As System.EventArgs) Handles btndelete.Click
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand()
                Dim query As String = "Delete from Timetable where Timetableid ='" & ViewState("Timetableid") & "'"
                cmd.CommandText = query
                cmd.Connection = con
                con.Open()
                cmd.ExecuteNonQuery()
                con.Close()
            End Using
        End Using
        txtfromdate.ReadOnly = False
        txttodate.ReadOnly = False
        ddlclass.Enabled = True
        ddlcombine.Enabled = True
        ddlperiod.Enabled = True
        ChkWeek.Enabled = True
        ddlclassroom.Enabled = True

        btnsave.Visible = True
        Btnupdate.Visible = False
        btndelete.Visible = False

        txtfromdate.Text = ""
        txttodate.Text = ""
        For Each item As ListItem In ChkWeek.Items
            item.Selected = False
        Next


        Bindgrid(ViewState("Faculty"))
    End Sub
End Class
