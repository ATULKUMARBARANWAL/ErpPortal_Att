
Imports System.Data
Imports System.Text
Imports System.Configuration
Imports System.Data.SqlClient
Imports System.IO
Partial Class Attendance_TimetableAdjustment
    Inherits System.Web.UI.Page

    Dim constr As String = ConfigurationManager.ConnectionStrings("myconnection").ConnectionString

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            BindgridAbsentFaculty()
        End If
    End Sub

    Private Sub BindgridAbsentFaculty()
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand()
                Dim query As String = " Select Pat.*, emp.Employee from P_Attendence Pat join P_Employee emp on Pat.EmpID = emp.EmployeeID where Pat.[P/A] ='A' and Cast(Pat.Dated as date)   =Cast(GETDATE() as Date)"

                cmd.CommandText = query
                Using sda As New SqlDataAdapter()
                    cmd.Connection = con
                    sda.SelectCommand = cmd
                    Using dt As New DataTable()
                        sda.Fill(dt)
                        gridAbsentFaculty.DataSource = dt
                        gridAbsentFaculty.DataBind()
                    End Using
                End Using
            End Using
        End Using
    End Sub

    Protected Sub gridAbsentFaculty_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gridAbsentFaculty.RowCommand
        If e.CommandName = "Viewlecture" Then
            gridAbsentFaculty.SelectedIndex = e.CommandArgument
            Dim rowIndex As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = gridAbsentFaculty.Rows(rowIndex)
            Dim Facultyid As String = row.Cells(3).Text
            ViewState("Adjustfrom") = row.Cells(3).Text
            PnlAbsentfacltylist.Visible = False
            panellecture.Visible = True
            paneladjustment.Visible = False
            backbotton.Visible = False
            backbotton1.Visible = True
            backbotton3.Visible = False
            lblday.Text = Date.Now.ToString("D")
            bindgridtodaylecture(Facultyid)
        End If
    End Sub

    Private Sub bindgridtodaylecture(ByVal Facultyid As String)
        '    If ViewState("Adjustto") = "" Then

        '        grdtodaylecture.Columns(8).Visible = False

        '        Using con As New SqlConnection(constr)
        '            Using cmd As New SqlCommand()
        '                Dim query As String = "Select * from (Select t.Timetableid,t.Prd,case when t.WdNo=2 then 'Monday' when t.WdNo=3 then 'Tuesday' " & _
        '" when t.WdNo=4 then 'Wednesday' when t.WdNo=5 then 'Thursday' when t.WdNo=6 then 'Friday' " & _
        '  "when t.WdNo=7 then 'Saturday' end as LectureDay ,s.Subject,cs.Code," & _
        '  "c.Course,cr.ClassRoom from Timetable t " & _
        '"inner join Exam_Subject s on s.Subjectid=t.SubjectID " & _
        '"inner join Classes cs on cs.ClassesID=t.ClassesID " & _
        '"inner join Exam_Course c on c.Courseid=t.CourseID " & _
        '"inner join ClassRoom cr on cr.classid=t.ClassRoom " & _
        '"where t.TeacherID='" & Facultyid & "' and t.SessionID='" & Request.QueryString("s") & "' and t.EvenOdd='" & Request.QueryString("e") & "'  ) as Lec " & _
        '"where Lec.LectureDay=DAtename(Weekday,GETDATE())"

        '                cmd.CommandText = query
        '                Using sda As New SqlDataAdapter()
        '                    cmd.Connection = con
        '                    sda.SelectCommand = cmd
        '                    Using dt As New DataTable()
        '                        sda.Fill(dt)
        '                        grdtodaylecture.DataSource = dt
        '                        grdtodaylecture.DataBind()
        '                    End Using
        '                End Using
        '            End Using
        '        End Using

        '    Else

        grdtodaylecture.Columns(8).Visible = True

        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand()
                Dim query As String = "Select * from (Select u.usernamefull,t.Timetableid,t.Prd,case when t.WdNo=2 then 'Monday' when t.WdNo=3 then 'Tuesday' " & _
" when t.WdNo=4 then 'Wednesday' when t.WdNo=5 then 'Thursday' when t.WdNo=6 then 'Friday' " & _
  "when t.WdNo=7 then 'Saturday' end as LectureDay ,s.Subject,cs.Code," & _
  "c.Course,cr.ClassRoom from Timetable t " & _
"left join Exam_Subject s on s.Subjectid=t.SubjectID " & _
"left join Classes cs on cs.ClassesID=t.ClassesID " & _
"left join Exam_Course c on c.Courseid=t.CourseID " & _
"left join ClassRoom cr on cr.classid=t.ClassRoom " & _
"left join TimeTableAdjustment ta on ta.Timetableid=t.Timetableid " & _
"left join Users u on u.userid=ta.AdjustmentTO " & _
"where t.TeacherID='" & Facultyid & "' and t.SessionID='" & Request.QueryString("s") & "' and t.EvenOdd='" & Request.QueryString("e") & "' and t.ToDt>=CAST (GETDATE() as date)  ) as Lec " & _
"where Lec.LectureDay=DAtename(Weekday,GETDATE())"

                cmd.CommandText = query
                Using sda As New SqlDataAdapter()
                    cmd.Connection = con
                    sda.SelectCommand = cmd
                    Using dt As New DataTable()
                        sda.Fill(dt)
                        grdtodaylecture.DataSource = dt
                        grdtodaylecture.DataBind()
                    End Using
                End Using
            End Using
        End Using

        'End If


    End Sub

    Protected Sub grdtodaylecture_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdtodaylecture.RowCommand
        If e.CommandName = "Adjustment" Then
            grdtodaylecture.SelectedIndex = e.CommandArgument
            Dim rowIndex As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = grdtodaylecture.Rows(rowIndex)
            Dim period As String = row.Cells(1).Text

            ViewState("Timetableid") = row.Cells(2).Text
            ViewState("period") = row.Cells(1).Text

            PnlAbsentfacltylist.Visible = False
            panellecture.Visible = False
            paneladjustment.Visible = True
            backbotton.Visible = False
            backbotton1.Visible = False
            backbotton3.Visible = True
            fillddlsubject()
            Bindavailablefaculty(period)
        End If
    End Sub

    Private Sub fillddlsubject()
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand()
                Dim query As String = " Select Distinct s.* from Exam_SUbject s inner join Exam_Coursesubject cs on cs.Subjectid=s.Subjectid  inner join Exam_SubjectPlan sp on sp.Subjectid=s.Subjectid Where cs.Sessionid='" & Request.QueryString("s") & "' and sp.Facultyid is not null"
                cmd.CommandText = query
                Using sda As New SqlDataAdapter()
                    cmd.Connection = con
                    sda.SelectCommand = cmd
                    Using dt As New DataTable()
                        sda.Fill(dt)
                        Ddlsubject.DataTextField = "Subject"
                        Ddlsubject.DataValueField = "Subjectid"
                        Ddlsubject.DataSource = dt
                        Ddlsubject.DataBind()
                    End Using
                End Using
            End Using
        End Using
    End Sub

    Private Sub Bindavailablefaculty(ByVal period As String)
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand()
                Dim query As String = " select wd as NAme,p.Teacherid, p.usernamefull,p.[1],p.[2],p.[3],p.[4],p.[5],p.[6],p.[7],p.[8],p.[9],p.[10],p.[11] from wday ww " & _
 "left join (select * from (  SELECT t.Teacherid,u.usernamefull,s.Subjectid,t.WdNo,case when t.WdNo=2 then 'Monday' when t.WdNo=3 then 'Tuesday' " & _
 "when t.WdNo=4 then 'Wednesday' when t.WdNo=5 then 'Thursday' when t.WdNo=6 then 'Friday' " & _
  "when t.WdNo=7 then 'Saturday' end as LectureDay, t.prd ,isnull(cl.Code ,'<d>select Section</d>') as sub " & _
   "FROM TimeTable t  " & _
      "left join users u on u.userid = t.teacherid  " & _
      "left join Exam_course c on c.courseid=t.courseid  " & _
      "left join classes cl on cl.classesid=t.classesid  " & _
      "left join ClassRoom cr on cr.classid=t.ClassRoom " & _
      "left join Exam_Coursesubject s on s.subjectid=t.subjectid and t.courseid=s.courseid " & _
       "and t.sem=s.Semyear left join Exam_Subject ss on ss.Subjectid=s.Subjectid " & _
 "WHERE( t.evenodd = '" & Request.QueryString("e") & "' And t.sessionid = '" & Request.QueryString("s") & "' " & _
 "and t.ToDt >= CAST(GETDATE() as date))  ) as t1   pivot  (max(t1.sub) for t1.prd " & _
  "in ([1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11]))as td  where td.[" & period & "] is null and td.Subjectid='" & Ddlsubject.SelectedItem.Value & "') as  p on ww.wdid =p.wdno " & _
  "where p.LectureDay=DAtename(Weekday,GETDATE()) "

                cmd.CommandText = query
                Using sda As New SqlDataAdapter()
                    cmd.Connection = con
                    sda.SelectCommand = cmd
                    Using dt As New DataTable()
                        sda.Fill(dt)
                        grdapply.DataSource = dt
                        grdapply.DataBind()
                    End Using
                End Using
            End Using
        End Using
    End Sub

    Protected Sub grdapply_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdapply.RowCommand
        If e.CommandName = "Apply" Then
            grdapply.SelectedIndex = e.CommandArgument
            Dim rowIndex As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = grdapply.Rows(rowIndex)
            ViewState("Adjustto") = row.Cells(1).Text
            PnlAbsentfacltylist.Visible = False
            panellecture.Visible = True
            paneladjustment.Visible = False

            backbotton.Visible = False
            backbotton1.Visible = True
            backbotton3.Visible = False

            Dim Classroom As String = ""
            Dim Teachtype As String = ""
            Dim Classid As String = ""

            Using con As New SqlConnection(constr)
                Using cmd As New SqlCommand()
                    Dim query As String = "Select * from TimeTable where Timetableid='" & ViewState("Timetableid") & "'"
                    cmd.CommandText = query
                    Using sda As New SqlDataAdapter()
                        cmd.Connection = con
                        sda.SelectCommand = cmd
                        Using dt As New DataTable()
                            sda.Fill(dt)
                            Classroom = dt.Rows(0)("ClassRoom").ToString()
                            Teachtype = dt.Rows(0)("Teach_Type").ToString()
                            Classid = dt.Rows(0)("ClassesID").ToString()
                        End Using
                    End Using
                End Using
            End Using

            InsertAdjustment(Classroom, Teachtype, Classid)
            bindgridtodaylecture(ViewState("Adjustfrom"))

        End If
    End Sub

    Private Sub InsertAdjustment(ByVal Classroom As String, ByVal Teachtype As String, ByVal Classid As String)
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand()
                Dim query As String = "insert into TimeTableAdjustment " & _
 "(AdjustmentFrom,AdjustmentTO,dated,prd,Subjectid,Classroom,Teach_Type,Timetableid,userid,Classesid,cdt) " & _
"values('" & ViewState("Adjustfrom") & "','" & ViewState("Adjustto") & "',GEtdate(),'" & ViewState("period") & "','','" & Classroom & "','" & Teachtype & "','" & ViewState("Timetableid") & "','" & Request.QueryString("u") & "','" & Classid & "',getdate()) "
                cmd.Connection = con
                cmd.CommandText = query

                con.Open()
                cmd.ExecuteNonQuery()
                con.Close()
            End Using
        End Using
    End Sub

    Protected Sub backbotton1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles backbotton1.Click
        PnlAbsentfacltylist.Visible = True
        panellecture.Visible = False
        paneladjustment.Visible = False
        backbotton.Visible = False
        backbotton1.Visible = False
        backbotton3.Visible = False
        BindgridAbsentFaculty()
    End Sub

    Protected Sub backbotton3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles backbotton3.Click
        PnlAbsentfacltylist.Visible = False
        panellecture.Visible = True
        paneladjustment.Visible = False
        backbotton.Visible = False
        backbotton1.Visible = True
        backbotton3.Visible = False
        lblday.Text = Date.Now.ToString("D")
        bindgridtodaylecture(ViewState("Adjustfrom"))
    End Sub

    Protected Sub Ddlsubject_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Ddlsubject.SelectedIndexChanged
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand()
                Dim query As String = " select wd as NAme,p.Teacherid, p.usernamefull,p.[1],p.[2],p.[3],p.[4],p.[5],p.[6],p.[7],p.[8],p.[9],p.[10],p.[11] from wday ww " & _
 "left join (select * from (  SELECT t.Teacherid,u.usernamefull,s.Subjectid,t.WdNo,case when t.WdNo=2 then 'Monday' when t.WdNo=3 then 'Tuesday' " & _
 "when t.WdNo=4 then 'Wednesday' when t.WdNo=5 then 'Thursday' when t.WdNo=6 then 'Friday' " & _
  "when t.WdNo=7 then 'Saturday' end as LectureDay, t.prd ,isnull(cl.Code ,'<d>select Section</d>') as sub " & _
   "FROM TimeTable t  " & _
      "left join users u on u.userid = t.teacherid  " & _
      "left join Exam_course c on c.courseid=t.courseid  " & _
      "left join classes cl on cl.classesid=t.classesid  " & _
      "left join ClassRoom cr on cr.classid=t.ClassRoom " & _
      "left join Exam_Coursesubject s on s.subjectid=t.subjectid and t.courseid=s.courseid " & _
       "and t.sem=s.Semyear left join Exam_Subject ss on ss.Subjectid=s.Subjectid " & _
        "left join Exam_SubjectPlan sp on sp.Subjectid=ss.Subjectid and sp.Courseid=c.Courseid and sp.AcademicYear=YEAR(GETDATE()) " & _
 "WHERE( t.evenodd = '" & Request.QueryString("e") & "' And t.sessionid = '" & Request.QueryString("s") & "' " & _
 "and t.ToDt >= CAST(GETDATE() as date))  ) as t1   pivot  (max(t1.sub) for t1.prd " & _
  "in ([1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11]))as td  where td.[" & ViewState("period") & "] is null  and td.Subjectid='" & Ddlsubject.SelectedItem.Value & "') as  p on ww.wdid =p.wdno " & _
  "where p.LectureDay=DAtename(Weekday,GETDATE()) "

                cmd.CommandText = query
                Using sda As New SqlDataAdapter()
                    cmd.Connection = con
                    sda.SelectCommand = cmd
                    Using dt As New DataTable()
                        sda.Fill(dt)
                        grdapply.DataSource = dt
                        grdapply.DataBind()
                    End Using
                End Using
            End Using
        End Using
    End Sub

    Protected Sub gridAbsentFaculty_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridAbsentFaculty.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            Dim lnkButton As LinkButton = TryCast(e.Row.FindControl("namel"), LinkButton)
            Dim Count As Integer = 0
            Dim teacherid As String = e.Row.Cells(3).Text
            Using con As New SqlConnection(constr)
                Using cmd As New SqlCommand(" Select *  from TimeTableAdjustment where AdjustmentFrom='" & teacherid & "'  and DATED=CAST(GETDATE() as date) and AdjustmentTO is not null")
                    Using sda As New SqlDataAdapter()
                        cmd.Connection = con
                        sda.SelectCommand = cmd
                        Using dt As New DataTable()
                            sda.Fill(dt)
                            If dt.Rows.Count Then
                                Count = 1
                            Else
                                Count = 0
                            End If
                        End Using
                    End Using
                End Using
            End Using
            If Count = 0 Then

                lnkButton.Text = "<span class=""fa fa-times text-danger""></span>"


            Else
                lnkButton.Text = "<span class=""fa fa-check text-success""></span>"

            End If

        End If

    End Sub
    'Protected Sub btnAddHostel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddHostel.Click
    '    Response.Redirect("HostelMaster.aspx?s=" & ViewState("Sessionid") & "&ay=" & ViewState("ayid") & "&u=" & ViewState("userid"))
    'End Sub
    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
    End Sub
    Protected Sub Download_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Download.Click
        Response.ClearContent()
        Response.AddHeader("content-disposition", "attachment;filename=FacultyAbsentList.xls")
        Response.ContentType = "application/vnd.ms-excel"
        Dim sw As StringWriter = New StringWriter()
        Dim htm As HtmlTextWriter = New HtmlTextWriter(sw)
        gridAbsentFaculty.AllowPaging = False

        gridAbsentFaculty.HeaderRow.Cells(1).Visible = False
        gridAbsentFaculty.HeaderRow.Cells(2).Visible = False
        gridAbsentFaculty.HeaderRow.Cells(3).Visible = False
        gridAbsentFaculty.RowStyle.HorizontalAlign = HorizontalAlign.Center
        For Each row As GridViewRow In gridAbsentFaculty.Rows
            row.Cells(1).Visible = False
            row.Cells(2).Visible = False
            row.Cells(3).Visible = False


        Next

        gridAbsentFaculty.RenderControl(htm)
        Response.Write(sw.ToString())
        Response.End()
    End Sub

End Class
