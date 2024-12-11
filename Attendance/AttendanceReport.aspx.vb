
Imports System
Imports System.Data
Imports System.Text
Imports System.Configuration
Imports System.Data.SqlClient
Imports System.IO
Partial Class Attendance_AttendanceReport
    Inherits System.Web.UI.Page
    Dim constr As String = ConfigurationManager.ConnectionStrings("myconnection").ConnectionString

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then

            ViewState("userid") = Request.QueryString("u")
            ViewState("courseid") = Request.QueryString("Courseid")
            ViewState("sem") = Request.QueryString("Sem")
            ViewState("subjectid") = Request.QueryString("Subjectid")
            ViewState("classid") = Request.QueryString("classesid")
            ViewState("Acyr") = Request.QueryString("acyr")

            lblprogram.Text = Request.QueryString("Course")
            lblsubject.Text = Request.QueryString("Subject")
            Lblsection.Text = Request.QueryString("Section")
            lblSem.Text = Request.QueryString("Sem")
            getseminfo()
            Dim today As String = DateTime.Now.ToString("yyyy-MM-dd")


            Dim previoustoday As String = DateAdd("d", -7, DateTime.Now).ToString("yyyy-MM-dd")

            TxtTodate.text = today
            TxtFromDate.text = previoustoday


            Attendencefetch()

           

        Else
            ViewState("userid") = Request.QueryString("u")
            ViewState("courseid") = Request.QueryString("Courseid")
            ViewState("sem") = Request.QueryString("Sem")
            ViewState("subjectid") = Request.QueryString("Subjectid")
            ViewState("classid") = Request.QueryString("classesid")
            ViewState("Acyr") = Request.QueryString("acyr")

            lblprogram.Text = Request.QueryString("Course")
            lblsubject.Text = Request.QueryString("Subject")
            Lblsection.Text = Request.QueryString("Section")
            getseminfo()
            Attendencefetch()

        End If
    End Sub


    Private Function GetData() As DataTable

        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand("GET_ATTENDANCEREPORT")
                cmd.Parameters.AddWithValue("@STARTDATE", TxtFromDate.text)
                cmd.Parameters.AddWithValue("@ENDDATE", TxtTodate.text)
                cmd.Parameters.AddWithValue("@Teacherid", ViewState("userid"))
                cmd.Parameters.AddWithValue("@courseid", ViewState("courseid"))
                cmd.Parameters.AddWithValue("@sem", ViewState("sem"))
                cmd.Parameters.AddWithValue("@subjectid", ViewState("subjectid"))
                cmd.Parameters.AddWithValue("@Classesid", ViewState("classid"))
                Using sda As New SqlDataAdapter()
                    cmd.Connection = con
                    cmd.CommandType = CommandType.StoredProcedure
                    sda.SelectCommand = cmd
                    Using dt As New DataTable()
                        sda.Fill(dt)
                        Return dt
                    End Using
                End Using
            End Using
        End Using
    End Function

    Private Sub Attendencefetch()




        Dim dt As DataTable = Me.GetData()


        Dim html As New StringBuilder()

        'Table start.
        html.Append("<table class= 'table table-bordered'>")

        'Building the Header row.
        html.Append("<tr>")
        For Each column As DataColumn In dt.Columns
            html.Append("<th>")
            html.Append(column.ColumnName)
            html.Append("</th>")
        Next
        html.Append("</tr>")

        'Building the Data rows.
        For Each row As DataRow In dt.Rows
            html.Append("<tr>")
            For Each column As DataColumn In dt.Columns
                html.Append("<td>")
                html.Append(row(column.ColumnName))
                html.Append("</td>")
            Next
            html.Append("</tr>")
        Next

        'Table end.
        html.Append("</table>")

        'Append the HTML string to Placeholder.
        PlaceHolder1.Controls.Add(New Literal() With { _
           .Text = html.ToString() _
         })
    End Sub
    Private Sub getseminfo()
        Using con As New SqlConnection(constr)
            Dim cmd As New SqlCommand()
            Dim sql As String = "Select CS.Academicyear, CS.Courseid,  Case when Cs.Coursetype like '%Sem%' then 'Semester' " & _
"  when Cs.Coursetype like '%year%' then 'Yearly' end as Course_type from Exam_CourseSession CS where CS.Academicyear='" & ViewState("Acyr") & "' and CS.Courseid='" & ViewState("courseid") & "' "
            cmd.CommandText = sql
            Using sda As New SqlDataAdapter()
                cmd.Connection = con
                sda.SelectCommand = cmd
                Dim dt As New DataTable
                sda.Fill(dt)
                If dt.Rows.Count > 0 Then
                    lblSem.Text = ViewState("sem") + " " + dt.Rows(0)("Course_type").ToString

                End If
            End Using
        End Using

    End Sub

  
    Protected Sub backbotton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles backbotton.Click
        Response.Redirect("../Attendance/FacultyAttendance.aspx?s=" & Request.QueryString("s") & "&e=" & Request.QueryString("e") & "&u=" & Request.QueryString("u"))
    End Sub
    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
    End Sub
    Protected Sub Download_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Download.Click
       
    End Sub

End Class
