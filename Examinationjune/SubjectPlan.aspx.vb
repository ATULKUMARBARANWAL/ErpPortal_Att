Imports System.Data
Imports System.Text
Imports System.Configuration
Imports System.Data.SqlClient
Imports System.Collections.Specialized
Partial Class Examinationjune_SubjectPlan
    Inherits System.Web.UI.Page
    Dim constr As String = ConfigurationManager.ConnectionStrings("myconnection").ConnectionString


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            lblAcademicyear.Text = Request.QueryString("acyr")
            lblsubject.Text = Request.QueryString("rid")
            ViewState("cid") = Request.QueryString("cid")
            BindGridsubject()
        End If
    End Sub

    Private Sub BindGridsubject()
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand()
                Dim query As String = "Select * from Exam_SubjectUnit where Coursesubid in( Select Coursesubid from Exam_Coursesubject where Courseid='" & Request.QueryString("cid") & "' and Subjectid='" & Request.QueryString("subjectid") & "' and Academicyear='" & Request.QueryString("acyr") & "')"

                cmd.CommandText = query
                Using sda As New SqlDataAdapter()
                    cmd.Connection = con
                    sda.SelectCommand = cmd
                    Using dt As New DataTable()
                        sda.Fill(dt)
                        gridsubjectplan.DataSource = dt
                        gridsubjectplan.DataBind()
                    End Using
                End Using
            End Using
        End Using
    End Sub

    Protected Sub btnaddunit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnaddunit.Click
        btnaddunit.Visible = False
        Paneladdunit.Visible = True
        SetInitialRow()
        FetchCOursesubid()
        lblAcademicaddunit.Text = Request.QueryString("acyr")
        lblsubjectaddunit.Text = Request.QueryString("rid")
    End Sub

    Private Sub SetInitialRow()
        Dim dt As DataTable = New DataTable()
        Dim dr As DataRow = Nothing
        dt.Columns.Add(New DataColumn("RowNumber", GetType(String)))
        dt.Columns.Add(New DataColumn("Column1", GetType(String)))
        dt.Columns.Add(New DataColumn("Column2", GetType(String)))
        dt.Columns.Add(New DataColumn("Column3", GetType(String)))
        dr = dt.NewRow()
        dr("RowNumber") = 1
        dr("Column1") = String.Empty
        dr("Column2") = String.Empty
        dr("Column3") = String.Empty
        dt.Rows.Add(dr)
        ViewState("CurrentTable") = dt
        Gridview1.DataSource = dt
        Gridview1.DataBind()
    End Sub

    Private Sub AddNewRowToGrid()
        Dim rowIndex As Integer = 0

        If ViewState("CurrentTable") IsNot Nothing Then
            Dim dtCurrentTable As DataTable = CType(ViewState("CurrentTable"), DataTable)
            Dim drCurrentRow As DataRow = Nothing
            If dtCurrentTable.Rows.Count > 0 Then
                For i As Integer = 1 To dtCurrentTable.Rows.Count
                    Dim UnitName As TextBox = CType(Gridview1.Rows(rowIndex).Cells(1).FindControl("UnitName"), TextBox)
                    Dim Description As TextBox = CType(Gridview1.Rows(rowIndex).Cells(2).FindControl("Description"), TextBox)
                    Dim LectureDuration As TextBox = CType(Gridview1.Rows(rowIndex).Cells(3).FindControl("LectureDuration"), TextBox)
                    drCurrentRow = dtCurrentTable.NewRow()
                    drCurrentRow("RowNumber") = i + 1
                    drCurrentRow("Column1") = UnitName.Text
                    drCurrentRow("Column2") = Description.Text
                    drCurrentRow("Column3") = LectureDuration.Text
                    rowIndex += 1
                Next
                dtCurrentTable.Rows.Add(drCurrentRow)
                ViewState("CurrentTable") = dtCurrentTable
                Gridview1.DataSource = dtCurrentTable
                Gridview1.DataBind()
            End If
        Else
            Response.Write("ViewState is null")
        End If

        SetPreviousData()
    End Sub

    Private Sub SetPreviousData()
        Dim rowIndex As Integer = 0

        If ViewState("CurrentTable") IsNot Nothing Then
            Dim dt As DataTable = CType(ViewState("CurrentTable"), DataTable)
            If dt.Rows.Count > 0 Then
                For i As Integer = 1 To dt.Rows.Count - 1
                    Dim UnitName As TextBox = CType(Gridview1.Rows(rowIndex).Cells(1).FindControl("UnitName"), TextBox)
                    Dim Description As TextBox = CType(Gridview1.Rows(rowIndex).Cells(2).FindControl("Description"), TextBox)
                    Dim LectureDuration As TextBox = CType(Gridview1.Rows(rowIndex).Cells(3).FindControl("LectureDuration"), TextBox)
                    UnitName.Text = dt.Rows(i)("Column1").ToString()
                    Description.Text = dt.Rows(i)("Column2").ToString()
                    LectureDuration.Text = dt.Rows(i)("Column3").ToString()
                    rowIndex += 1
                Next
            End If
        End If
    End Sub

    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As EventArgs)
        AddNewRowToGrid()
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As EventArgs)


        Dim rowIndex As Integer = 0
        Dim sc As StringCollection = New StringCollection()


        Dim UnitNamet As String = CType(Gridview1.Rows(rowIndex).Cells(1).FindControl("UnitName"), TextBox).Text
        Dim Descriptiont As String = CType(Gridview1.Rows(rowIndex).Cells(2).FindControl("Description"), TextBox).Text
        Dim LectureDurationt As String = CType(Gridview1.Rows(rowIndex).Cells(3).FindControl("LectureDuration"), TextBox).Text



        If UnitNamet = "" Or Descriptiont = "" Or LectureDurationt = "" Then

            Page.ClientScript.RegisterClientScriptBlock(GetType(Page), "Script", "alert('Please fill all Details!');", True)

        Else


            Using con As SqlConnection = New SqlConnection(constr)
                Using cmd As SqlCommand = New SqlCommand("insert into Exam_SubjectUnit(UnitName,Description,Timeduration,Coursesubid,Academicyear,Dated) VALUES(@UnitName,@Description,@Timeduration,@Coursesubid,@Academicyear,@Dated)")
                    cmd.CommandType = CommandType.Text
                    cmd.Parameters.AddWithValue("@UnitName", UnitNamet)
                    cmd.Parameters.AddWithValue("@Description", Descriptiont)
                    cmd.Parameters.AddWithValue("@Timeduration", LectureDurationt)

                    cmd.Parameters.AddWithValue("@Coursesubid", ViewState("Coursesubid"))
                    cmd.Parameters.AddWithValue("@Academicyear", lblAcademicaddunit.Text)
                    cmd.Parameters.AddWithValue("@Dated", Date.Now())

                    cmd.Connection = con
                    con.Open()
                    cmd.ExecuteNonQuery()
                    con.Close()

                End Using
            End Using


        End If


        BindGridsubject()
        CType(Gridview1.Rows(rowIndex).Cells(1).FindControl("UnitName"), TextBox).Text = ""
        CType(Gridview1.Rows(rowIndex).Cells(1).FindControl("Description"), TextBox).Text = ""
        CType(Gridview1.Rows(rowIndex).Cells(1).FindControl("LectureDuration"), TextBox).Text = ""

    End Sub

  

    Protected Sub Gridview1_RowCreated(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim dt As DataTable = CType(ViewState("CurrentTable"), DataTable)
            Dim lb As ImageButton = CType(e.Row.FindControl("lnkDelete"), ImageButton)
            If lb IsNot Nothing Then
                If dt.Rows.Count > 1 Then
                    If e.Row.RowIndex = dt.Rows.Count - 1 Then
                        lb.Visible = False
                    End If
                Else
                    lb.Visible = False
                End If
            End If
        End If
    End Sub

    Protected Sub lnkDelete_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim lb As LinkButton = CType(sender, LinkButton)
        Dim gvRow As GridViewRow = CType(lb.NamingContainer, GridViewRow)
        Dim rowID As Integer = gvRow.RowIndex + 1
        If ViewState("CurrentTable") IsNot Nothing Then
            Dim dt As DataTable = CType(ViewState("CurrentTable"), DataTable)
            If dt.Rows.Count > 1 Then
                If gvRow.RowIndex < dt.Rows.Count - 1 Then
                    dt.Rows.Remove(dt.Rows(rowID))
                End If
            End If
            ViewState("CurrentTable") = dt
            Gridview1.DataSource = dt
            Gridview1.DataBind()
        End If

        SetPreviousData()
    End Sub

    Private Sub FetchCOursesubid()
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand()
                Dim query As String = "Select Coursesubid from Exam_Coursesubject where Courseid='" & Request.QueryString("cid") & "' and Subjectid='" & Request.QueryString("subjectid") & "' and Academicyear='" & Request.QueryString("acyr") & "'"

                cmd.CommandText = query
                Using sda As New SqlDataAdapter()
                    cmd.Connection = con
                    sda.SelectCommand = cmd
                    Using dt As New DataTable()
                        sda.Fill(dt)
                        ViewState("Coursesubid") = dt.Rows(0)("Coursesubid").ToString()
                    End Using
                End Using
            End Using
        End Using
    End Sub


    Protected Sub lbtnprevious_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbtnprevious.Click
        Paneladdunit.Visible = False
        Panelunits.Visible = True
    End Sub



    Protected Sub backbotton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles backbotton.Click
        BindGridsubject()
        Fetchprogramname()
        Response.Redirect("SubjectToProgram.aspx?acyr=" & lblAcademicyear.Text & "&s=" & Request.QueryString("s") & "&e=" & Request.QueryString("e") & "&u=" & Request.QueryString("u") & "&rid=" & Request.QueryString("cid") & "&ay=" & Request.QueryString("ay"))
    End Sub

    Private Sub Fetchprogramname()
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand()
                Dim query As String = "Select Course from Exam_Course where Courseid='" & Request.QueryString("cid") & "' "

                cmd.CommandText = query
                Using sda As New SqlDataAdapter()
                    cmd.Connection = con
                    sda.SelectCommand = cmd
                    Using dt As New DataTable()
                        sda.Fill(dt)
                        ViewState("Course") = dt.Rows(0)("Course").ToString()
                    End Using
                End Using
            End Using
        End Using
    End Sub

    Protected Sub gridsubjectplan_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gridsubjectplan.RowCommand
        If e.CommandName = "Documents" Then
            gridsubjectplan.SelectedIndex = e.CommandArgument
            Dim rowIndex As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = gridsubjectplan.Rows(rowIndex)
            ViewState("Coursesessionid") = gridsubjectplan.SelectedDataKey(0)
            Session("Unitname") = row.Cells(1).Text
            Response.Redirect("UnitDocuments.aspx?cid=" & Request.QueryString("cid") & "&acyr=" & lblAcademicyear.Text & "&suid=" & gridsubjectplan.SelectedDataKey(0) & "&rid=" & lblsubject.Text & "&uname=" & Session("Unitname") & "&subjectid=" & Request.QueryString("subjectid") & "&s=" & Request.QueryString("s") & "&ay=" & Request.QueryString("ay") & "&u=" & Request.QueryString("u"))
        End If

        If e.CommandName = "Delete" Then

            gridsubjectplan.SelectedIndex = e.CommandArgument
            Dim rowIndex As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = gridsubjectplan.Rows(rowIndex)
            ViewState("Coursesessionid") = gridsubjectplan.SelectedDataKey(0)
            Session("Unitname") = row.Cells(1).Text

            Using con As New SqlConnection(constr)




                Using cmd As New SqlCommand("delete from Exam_SubjectUnit where Subunitid='" & gridsubjectplan.SelectedDataKey(0) & "' ")
                    Using sda As New SqlDataAdapter()
                        cmd.Connection = con
                        sda.SelectCommand = cmd
                        Using dt As New DataTable()
                            sda.Fill(dt)
                            BindGridsubject()

                        End Using
                    End Using
                End Using







            End Using


        End If
    End Sub

    Protected Sub OnRowDeleting(ByVal sender As Object, ByVal e As GridViewDeleteEventArgs)

        Dim constr As String = ConfigurationManager.ConnectionStrings("myconnection").ConnectionString
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand("DELETE  Exam_Course WHERE Courseid = ''")
                Using sda As New SqlDataAdapter()
                    cmd.Connection = con
                    sda.SelectCommand = cmd
                    con.Open()

                    Using dt As New DataTable()
                        sda.Fill(dt)
                        BindGridsubject()
                        con.Close()
                    End Using
                End Using
            End Using
        End Using

    End Sub

    Protected Sub OnRowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        'If e.Row.RowType = DataControlRowType.DataRow AndAlso e.Row.RowIndex <> grdProgram.EditIndex Then
        '    '    TryCast(e.Row.Cells(0).Controls(2), LinkButton).Attributes("onclick") = "return confirm('Do you want to delete this row?');"
        'End If
    End Sub

End Class
