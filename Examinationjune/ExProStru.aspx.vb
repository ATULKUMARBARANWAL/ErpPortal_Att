Imports System.Data
Imports System.Text
Imports System.Configuration
Imports System.Data.SqlClient
Partial Class ExProStru
    Inherits System.Web.UI.Page
    Dim constr As String = ConfigurationManager.ConnectionStrings("myconnection").ConnectionString

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ViewState("Academicyear") = Request.QueryString("acyr")
            ViewState("sessionid") = Request.QueryString("s")
            ViewState("courseid") = Request.QueryString("rid")
            ViewState("userid") = Request.QueryString("u")
            lblacedmic.Text = Request.QueryString("acyr")
            ViewState("ayid") = Request.QueryString("ay")

            lblFetchProgram.Text = Session("Coursesessionid")
            pnlgrdProg.Visible = True
            pnlgrdAllProg.Visible = False
            getcourseinfo()
            progGrdBind()
            allProgGrdBind()


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
                    lblFetchProgram.Text = dt.Rows(0)("Course").ToString

                End If
            End Using
        End Using

    End Sub
    Private Sub progGrdBind()

        Using con As New SqlConnection(constr)
            Dim cmd As New SqlCommand("Select Cs.Coursesessionid,Cs.Courseid, C.Course,C.Coursecode " & _
" from Exam_CourseSession Cs join Exam_Course C on Cs.Courseid Collate Database_Default=C.Courseid  where  Cs.SessionId='" & ViewState("sessionid") & "' and cs.Courseid='" & ViewState("courseid") & "'", con)
            Dim da As New SqlDataAdapter(cmd)
            Dim dt As New DataTable()
            con.Open()
            da.Fill(dt)
            grdProg.DataSource = dt
            grdProg.DataBind()
            con.Close()

        End Using
        fetchexamstructure()
    End Sub



    Private Sub allProgGrdBind()

        Using con As New SqlConnection(constr)
            Dim cmd As New SqlCommand("Select Cs.Coursesessionid,Cs.Courseid, C.Course,C.Coursecode " & _
" from Exam_CourseSession Cs join Exam_Course C on Cs.Courseid Collate Database_Default=C.Courseid  where  Cs.SessionId='" & ViewState("sessionid") & "' ", con)
            Dim da As New SqlDataAdapter(cmd)
            Dim dt As New DataTable()
            con.Open()
            da.Fill(dt)
            grdAllProg.DataSource = dt
            grdAllProg.DataBind()
            con.Close()
        End Using
        fetchallexamstructure()
    End Sub

    Protected Sub chckBoxAllProg_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chckBoxAllProg.CheckedChanged
        If chckBoxAllProg.Checked Then
            lblProgram.Visible = False
            lblFetchProgram.Visible = False
            pnlgrdProg.Visible = False
            pnlgrdAllProg.Visible = True
        Else
            lblProgram.Visible = True
            lblFetchProgram.Visible = True
            pnlgrdProg.Visible = True
            pnlgrdAllProg.Visible = False
        End If
    End Sub

    Protected Sub grdProg_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdProg.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            Dim ddlStructures As DropDownList = CType(e.Row.FindControl("ddlStructure"), DropDownList)
            Using con As New SqlConnection(constr)
                Using cmd As New SqlCommand("  Select Distinct Structurename from Exam_ExamStructure where Structurename is not null order by Structurename")
                    Using sda As New SqlDataAdapter()
                        cmd.Connection = con
                        sda.SelectCommand = cmd
                        Using dt As New DataTable()
                            sda.Fill(dt)
                            ddlStructures.DataSource = dt
                            ddlStructures.DataTextField = "Structurename"

                            ddlStructures.DataBind()
                            ddlStructures.Items.Insert(0, New ListItem("Select", "0"))
                        End Using
                    End Using
                End Using
            End Using
        End If
    End Sub

    Protected Sub grdAllProg_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdAllProg.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            Dim ddlStructures As DropDownList = CType(e.Row.FindControl("ddlStructure"), DropDownList)
            Using con As New SqlConnection(constr)
                Using cmd As New SqlCommand(" Select Distinct Structurename from Exam_ExamStructure where Structurename is not null order by Structurename")
                    Using sda As New SqlDataAdapter()
                        cmd.Connection = con
                        sda.SelectCommand = cmd
                        Using dt As New DataTable()
                            sda.Fill(dt)
                            ddlStructures.DataSource = dt
                            ddlStructures.DataTextField = "Structurename"

                            ddlStructures.DataBind()
                            ddlStructures.Items.Insert(0, New ListItem("Select", "0"))
                        End Using
                    End Using
                End Using
            End Using
        End If
    End Sub

    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        If chckBoxAllProg.Checked = True Then
            For Each row As GridViewRow In grdAllProg.Rows


                Dim Programstructureid As Integer = 0
                Dim courseid As String = row.Cells(4).Text
                Dim coursesessionid As String = row.Cells(5).Text
                Using con As New SqlConnection(constr)
                    If CType(row.FindControl("ddlStructure"), DropDownList).SelectedItem.Text = "Select" Then
                    Else
                        Using cmd As New SqlCommand("sp_AddCourseStru", con)
                            cmd.CommandType = CommandType.StoredProcedure
                            Using sda As New SqlDataAdapter()
                                Dim da As New SqlDataAdapter(cmd)
                                Dim dt As New DataTable()

                                cmd.Parameters.AddWithValue("@Courseid ", courseid)
                                cmd.Parameters.AddWithValue("@Academicyear ", ViewState("Academicyear"))
                                cmd.Parameters.AddWithValue("@Structureid ", CType(row.FindControl("ddlStructure"), DropDownList).SelectedItem.Text)
                                cmd.Parameters.AddWithValue("@userid ", ViewState("userid"))
                                cmd.Parameters.AddWithValue("@courseSessionid ", coursesessionid)
                                cmd.Connection = con
                                con.Open()
                                Programstructureid = cmd.ExecuteNonQuery()
                                con.Close()
                            End Using
                        End Using
                    End If

                End Using
            Next
            SaralMsg.Messagebx.Alert(Me, "Added Successfully")
        Else

            For Each row As GridViewRow In grdProg.Rows


                Dim Programstructureid As Integer = 0
                Dim courseid As String = row.Cells(4).Text
                Dim coursesessionid As String = row.Cells(5).Text
                Using con As New SqlConnection(constr)
                    If CType(row.FindControl("ddlStructure"), DropDownList).SelectedItem.Text = "Select" Then
                    Else
                        Using cmd As New SqlCommand("sp_AddCourseStru", con)
                            cmd.CommandType = CommandType.StoredProcedure
                            Using sda As New SqlDataAdapter()
                                Dim da As New SqlDataAdapter(cmd)
                                Dim dt As New DataTable()
                                cmd.Parameters.AddWithValue("@Courseid ", courseid)
                                cmd.Parameters.AddWithValue("@Academicyear ", ViewState("Academicyear"))
                                cmd.Parameters.AddWithValue("@Structureid ", CType(row.FindControl("ddlStructure"), DropDownList).SelectedItem.Text)
                                cmd.Parameters.AddWithValue("@userid ", ViewState("userid"))
                                cmd.Parameters.AddWithValue("@courseSessionid ", coursesessionid)
                                cmd.Connection = con
                                con.Open()
                                cmd.ExecuteNonQuery()
                                con.Close()
                            End Using
                        End Using
                    End If

                End Using
            Next
            SaralMsg.Messagebx.Alert(Me, "Added Successfully")
        End If


    End Sub

    Protected Sub backbotton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles backbotton.Click
        ViewState("Courseid") = Session("Courseid")
        'Session("Otherid") = row.Cells(2).Text
        Session("ExamName") = Session("Examname")
        Session("Course") = Session("Course")
        Session("Semyear") = Session("Semyear")
        Response.Redirect("Dashboard.aspx?rid=" & Session("Courseid") & "&acyr=" & ViewState("Academicyear") & "&s=" & Request.QueryString("s") & "&e=" & Request.QueryString("e") & "&u=" & Request.QueryString("u") & "&ay=" & ViewState("ayid"))
    End Sub
    Private Sub fetchexamstructure()
        For Each row As GridViewRow In grdProg.Rows
            Dim tfaculty As DropDownList = TryCast(row.FindControl("ddlStructure"), DropDownList)

            Dim Courseid As String = row.Cells(4).Text
            Dim CourseSessioniid As String = row.Cells(5).Text

            fetchexamstruct(tfaculty, Courseid, CourseSessioniid)
        Next
    End Sub

    Private Sub fetchallexamstructure()
        For Each row As GridViewRow In grdAllProg.Rows
            Dim tfaculty As DropDownList = TryCast(row.FindControl("ddlStructure"), DropDownList)

            Dim Courseid As String = row.Cells(4).Text
            Dim CourseSessioniid As String = row.Cells(5).Text
            fetchexamstruct(tfaculty, Courseid, CourseSessioniid)
        Next
    End Sub

    Private Sub fetchexamstruct(ByVal tfaculty As DropDownList, ByVal Courseid As String, ByVal CourseSessioniid As String)

        Try


            Using con As New SqlConnection(constr)
                Using cmd As New SqlCommand("Select * from Exam_ProgramStructure where Academicyear='" & ViewState("Academicyear") & "' and Courseid='" & Courseid & "' and CourseSessionid='" & CourseSessioniid & "' ")
                    Using sda As New SqlDataAdapter()
                        Dim dt As New DataTable()

                        cmd.Connection = con
                        sda.SelectCommand = cmd
                        sda.Fill(dt)
                        con.Open()
                        tfaculty.SelectedIndex = tfaculty.Items.IndexOf(tfaculty.Items.FindByText(dt.Rows(0)("Structureid")))
                        con.Close()
                    End Using

                End Using
            End Using
        Catch ex As Exception

        End Try
    End Sub

End Class
