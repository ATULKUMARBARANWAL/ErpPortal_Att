Imports System.Data
Imports System.Text
Imports System.Configuration
Imports System.Data.SqlClient
Imports System.IO

Partial Class Examinationjune_AssignProgram
    Inherits System.Web.UI.Page
    Dim constr As String = ConfigurationManager.ConnectionStrings("myconnection").ConnectionString
    Dim dt1 As New DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Try
                ViewState("Academicyear") = Request.QueryString("acyr")
                ViewState("Sessionid") = Request.QueryString("s")
                ViewState("courseid") = Request.QueryString("rid")
                ViewState("Userid") = Request.QueryString("u")
                lblacedmic.Text = ViewState("Academicyear")
                ViewState("ayid") = Request.QueryString("ay")
                lblProgramName.Text = Session("Coursesessionid")
                'fillddlsemyear()
                getcourseinfo()
                GridAssignment()
            Catch ex As Exception

            End Try

        End If

    End Sub


    Private Sub getcourseinfo()
        Using con As New SqlConnection(constr)
            Dim cmd As New SqlCommand()
            Dim sql As String = "Select * from Exam_Course where Courseid='" & ViewState("courseid") & "' "
            cmd.CommandText = sql
            Using sda As New SqlDataAdapter()
                cmd.Connection = con
                sda.SelectCommand = cmd
                Dim dt As New DataTable
                sda.Fill(dt)
                If dt.Rows.Count > 0 Then
                    '  lbl.Text = dt.Rows(0)("AdmissionNo").ToString
                    lblProgramName.Text = dt.Rows(0)("Course").ToString

                End If
            End Using
        End Using

    End Sub
    Protected Sub backbotton_Click(sender As Object, e As System.EventArgs) Handles backbotton.Click
        ViewState("Courseid") = Session("Courseid")
        'Session("Otherid") = row.Cells(2).Text
        Session("ExamName") = Session("Examname")
        Session("Course") = Session("Course")
        Session("Semyear") = Session("Semyear")
        Response.Redirect("Dashboard.aspx?rid=" & Session("Courseid") & "&acyr=" & ViewState("Academicyear") & "&s=" & Request.QueryString("s") & "&e=" & Request.QueryString("e") & "&u=" & Request.QueryString("u") & "&ay=" & ViewState("ayid"))
    End Sub

 

    Protected Sub btnsavesubject_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsavesubject.Click
        For Each row As GridViewRow In GridView1.Rows
            Dim Assignmentid As String = CType(row.FindControl("ddlAssignment"), DropDownList).SelectedItem.Value
            Dim Academicyear As String = row.Cells(2).Text
            Dim Courseid As String = row.Cells(1).Text
            Dim Semyear As String = row.Cells(3).Text
            Dim TotalAssignment As String = row.Cells(4).Text
            updateAssignemt(Assignmentid, Courseid, Academicyear, Semyear, Assignmentid)
        Next
        SaralMsg.Messagebx.Alert(Me, "Assigned Successfully.")
    End Sub

    Private Sub updateAssignemt(Assignmentid As String, Courseid As String, Academicyear As String, Semyear As String, TotalAssignment As String)
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand("[Sp_AssignAssignment]")
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@sessionid", ViewState("Sessionid"))
                cmd.Parameters.AddWithValue("@Academicyear", Academicyear)
                cmd.Parameters.AddWithValue("@Courseid", Courseid)
                cmd.Parameters.AddWithValue("@Semyear", Semyear)
                cmd.Parameters.AddWithValue("@userid", Request.QueryString("u"))
                cmd.Parameters.AddWithValue("@TotalAssignment", TotalAssignment)

                cmd.Connection = con
                con.Open()
                cmd.ExecuteNonQuery()
                con.Close()
            End Using
        End Using
    End Sub

    Private Sub fetchAssignment()
        For Each row As GridViewRow In GridView1.Rows
            Dim TotalAssignment As DropDownList = TryCast(row.FindControl("ddlAssignment"), DropDownList)
            Dim Sessionid As String = ViewState("Sessionid")
            Dim Courseid As String = row.Cells(1).Text
            Dim Semyear As String = row.Cells(3).Text
            fetchAssignment1(TotalAssignment, Courseid, Sessionid, Semyear)
        Next
    End Sub

    Private Sub fetchAssignment1(ByVal TotalAssignment As DropDownList, ByVal Courseid As String, ByVal Sessionid As String, ByVal Semyear As String)
        Try
            Using con As New SqlConnection(constr)
                Using cmd As New SqlCommand("Select TotalAssignment from Assign_Total  where Sessionid='" + Sessionid + "' and Courseid='" + Courseid + "' and Semyear='" + Semyear + "'")
                    Using sda As New SqlDataAdapter()
                        Dim dt As New DataTable()

                        cmd.Connection = con
                        sda.SelectCommand = cmd
                        sda.Fill(dt)
                        con.Open()
                        If dt.Rows.Count > 0 Then
                            TotalAssignment.SelectedIndex = TotalAssignment.Items.IndexOf(TotalAssignment.Items.FindByValue(dt.Rows(0)("TotalAssignment")))
                        End If

                        con.Close()
                    End Using

                End Using
            End Using
        Catch ex As Exception

        End Try
    End Sub
    Private Sub GridAssignment()
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand()
                Dim query As String = " Select Coursesessionid, Academicyear, Courseid,  Case when CourseType like '%sem%' then 'Semester' when CourseType like '%year%' then 'Year' end as 'CourseType', " & _
"case when Coursetype Like '%sem%' then Duration*2  " & _
"when Coursetype like '%year%' then Duration*1 when Coursetype like '%quart%' then Duration*4 end as  " & _
                "'Totalsem', Duration, Coursetype from Exam_CourseSession where  Sessionid ='" & ViewState("Sessionid") & "' and " & _
 "Courseid = '" & ViewState("courseid") & "'"


                cmd.CommandText = query
                Using sda As New SqlDataAdapter()
                    cmd.Connection = con
                    sda.SelectCommand = cmd

                    Using dt As New DataTable()
                        sda.Fill(dt)
                        ViewState("totalsem") = dt.Rows(0)("Totalsem").ToString()

                        dt1 = dt.Clone()
                        For i As Integer = 1 To ViewState("totalsem")

                            For Each dr As DataRow In dt.Rows
                                dt1.ImportRow(dr)
                            Next
                        Next

                        Dim j As String = 1
                        Dim k As Integer = 0

                        Dim dr1 As DataRow

                        While (j <= ViewState("totalsem"))

                            dr1 = dt1.Rows(k)
                            dr1(4) = j
                            j = j + 1
                            k = k + 1
                        End While

                        GridView1.DataSource = dt1
                        GridView1.DataBind()
                    End Using
                End Using
            End Using
        End Using
        fetchAssignment()
    End Sub

    Protected Sub GridView1_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView1.RowCommand
        Dim btnview As Button = Nothing
        Dim rowIndex As Integer = Convert.ToInt32(e.CommandArgument)
        Dim row As GridViewRow = GridView1.Rows(rowIndex)
        Dim TotalAssignment As DropDownList = TryCast(row.FindControl("ddlAssignment"), DropDownList)
        GridView1.SelectedIndex = e.CommandArgument

        If e.CommandName = "Studentlist" Then
            ' Retrieve the selected value from the TotalAssignment DropDownList
            Dim selectedValue As String = TotalAssignment.SelectedValue

            ' Store the selected value in ViewState
            ViewState("SelectedValueForRow_" & rowIndex) = selectedValue

            ' Your other logic
            lblacedmic.Text = GridView1.Rows(rowIndex).Cells(2).Text
            Session("Semyear") = row.Cells(3).Text
            labeldata()

            Response.Redirect("Assignassignmentindividually.aspx?acyr=" & Request.QueryString("acyr") & "&u=" & ViewState("Userid") & "&s=" & ViewState("Sessionid") & "&rid=" & Request.QueryString("rid") & "&ay=" & ViewState("ayid") & "&Sem=" & row.Cells(3).Text & "&TAI=" & ViewState("TotalAssignID") & "&assignment=" & ViewState("SelectedValueForRow_" & rowIndex))
        End If
        'Dim btnview As Button = Nothing
        'Dim rowIndex As Integer = Convert.ToInt32(e.CommandArgument)
        'Dim row As GridViewRow = GridView1.Rows(rowIndex)
        'Dim TotalAssignment As DropDownList = TryCast(row.FindControl("ddlAssignment"), DropDownList)
        'GridView1.SelectedIndex = e.CommandArgument
        'If e.CommandName = "Studentlist" Then
        '    'ViewState("monthid") = GridView1.SelectedDataKey(0)
        '    lblacedmic.Text = GridView1.Rows(rowIndex).Cells(2).Text
        '    Session("Semyear") = row.Cells(3).Text
        '    labeldata()
        '    ViewState("SelectedValueForRow_" & e.Row.RowIndex) = TotalAssignment
        '    Response.Redirect("Assignassignmentindividually.aspx?acyr=" & Request.QueryString("acyr") & "&u=" & ViewState("Userid") & "&s=" & ViewState("Sessionid") & "&rid=" & Request.QueryString("rid") & "&ay=" & ViewState("ayid") & "&Sem=" & row.Cells(3).Text & "&TAI=" & ViewState("TotalAssignID"))
        'End If
    End Sub
    Private Sub labeldata()
        Dim ds As New DataSet
        Using con As New SqlConnection(constr)
            con.Open()
            Dim cmd1 As New SqlCommand("select TotalAssignmentID From Assign_Total  where  Sessionid ='" & ViewState("Sessionid") & "' and " & _
 "Courseid = '" & ViewState("courseid") & "' and SemYear = '" & Session("Semyear") & "'  ", con)
            Dim da As New SqlDataAdapter(cmd1)
            cmd1.Connection = con
            da.Fill(ds)
            Dim i = ds.Tables(0).Rows.Count()
            If (i > 0) Then
                ViewState("TotalAssignID") = ds.Tables(0).Rows(0)("TotalAssignmentID").ToString()

                
            End If
            con.Close()

        End Using

    End Sub
End Class
