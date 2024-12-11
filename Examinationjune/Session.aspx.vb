Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration

'For Show Popup Message


'Developed by Tarang Sharma

Partial Class Session
    Inherits System.Web.UI.Page
    Dim Constr As String = ConfigurationManager.ConnectionStrings("myconnection").ConnectionString

    'page load
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            pnlSession.Visible = "True"
            ViewState("Userid") = Request.QueryString("u")

            'fill dropdown of session
            Fetchddlsession()

            'fill grid of session
            BindGrid()
        End If
    End Sub

    'Function for Bind grid for session
    Private Sub BindGrid()
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand("select * from Exam_Session order by Academicyear desc")
                Using sda As New SqlDataAdapter()
                    cmd.Connection = con
                    sda.SelectCommand = cmd
                    Dim dt As New DataTable()
                    sda.Fill(dt)
                    GrdViewsession.DataSource = dt
                    GrdViewsession.DataBind()
                End Using
            End Using
        End Using
    End Sub

    'Session Saving on Save Button Click
    Protected Sub btnSessionAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSessionAdd.Click
        Using con As New SqlConnection(Constr)
            Dim query As String = "Sp_InsertSession"
            Using Command As New SqlCommand(query, con)
                Command.CommandType = CommandType.StoredProcedure
                Command.Parameters.AddWithValue("@Academicyear", txtAcademicyear.Text)
                Command.Parameters.AddWithValue("@Session", ddlSession.SelectedItem.Text)
                Command.Parameters.AddWithValue("@USerid", ViewState("Userid"))
                Command.Parameters.AddWithValue("@Academicfromdate ", txtFromdate.Text)
                Command.Parameters.AddWithValue("@Academictodate", txtTodate.Text)
                Command.Parameters.AddWithValue("@FinancialFromdate", txtfromfinance.Text)
                Command.Parameters.AddWithValue("@FinancialTodate", txttofinance.Text)
                con.Open()
                If ddlSession.SelectedItem.Text = "" Or txtFromdate.Text = "" Or txtTodate.Text = "" Or txtfromfinance.Text = "" Or txttofinance.Text = "" Then
                    SaralMsg.Messagebx.Alert(Me, "Filling all the fields is Mandatory!")
                Else
                    ViewState("AcademicYear") = txtAcademicyear.Text
                    SaralMsg.Messagebx.Alert(Me, "Saved Successfully")
                    Command.ExecuteNonQuery()
                    con.Close()
                    Clear()
                End If
            End Using
        End Using
        BindGrid()
    End Sub

    'Clear all fields on Clear Button Click
    Protected Sub btnrefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnrefresh.Click
        Clear()
    End Sub

    'Function for Empty Fields
    Protected Sub Clear()
        ddlSession.Enabled = True
        ddlSession.ClearSelection()
        txtAcademicyear.Text = ""
        txtFromdate.Text = ""
        txtTodate.Text = ""
        txtfromfinance.Text = ""
        txttofinance.Text = ""
    End Sub

    'Fill the Academic year Textbox on Dropdown Session text changed.
    Protected Sub ddlSession_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSession.SelectedIndexChanged
        Dim value As String() = ddlSession.SelectedItem.Text.Split("-")
        txtAcademicyear.Text = value(0)
        fetchdate()
    End Sub

    'For Delete and Edit Session on click of Grid's Button
    Protected Sub GrdViewsession_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GrdViewsession.RowCommand
        If e.CommandName = "Delete" Then
            Dim count As String = 0
            GrdViewsession.SelectedIndex = e.CommandArgument
            Dim rowIndex As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = GrdViewsession.Rows(rowIndex)
            ViewState("id") = GrdViewsession.SelectedDataKey(0)
            ViewState("Academicyear") = row.Cells(4).Text
            Using con As New SqlConnection(Constr)
                Using cmd As New SqlCommand("Select Count(*) as Count from Exam_CourseSession where Academicyear='" & ViewState("Academicyear") & "' ")
                    Using sda As New SqlDataAdapter()
                        cmd.Connection = con
                        sda.SelectCommand = cmd
                        Using dt As New DataTable()
                            sda.Fill(dt)
                            count = dt.Rows(0)("Count").ToString()
                        End Using
                    End Using
                End Using
            End Using
            If count = 0 Then
                Using con As New SqlConnection(Constr)
                    Using cmd As New SqlCommand("Delete from Exam_Session where Sessionid='" & ViewState("id") & "' ")
                        Using sda As New SqlDataAdapter()
                            cmd.Connection = con
                            sda.SelectCommand = cmd
                            Using dt As New DataTable()
                                sda.Fill(dt)
                                Me.BindGrid()
                            End Using
                        End Using
                    End Using
                End Using
                SaralMsg.Messagebx.Alert(Me, "Delete Successfully!")
            Else
                SaralMsg.Messagebx.Alert(Me, "This Session is mapped with Program.")
            End If
        End If
        If e.CommandName = "Edit" Then
            ddlSession.Enabled = False
            GrdViewsession.SelectedIndex = e.CommandArgument
            Dim rowIndex As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = GrdViewsession.Rows(rowIndex)
            ViewState("id") = GrdViewsession.SelectedDataKey(0)
            ViewState("Academicyear") = row.Cells(4).Text
            Using con As New SqlConnection(Constr)
                Using cmd As New SqlCommand("Select * from Exam_Session where Sessionid='" & ViewState("id") & "' ")
                    cmd.Connection = con
                    con.Open()
                    Dim sdr As SqlDataReader = cmd.ExecuteReader
                    While (sdr.Read())
                        txtAcademicyear.Text = sdr("Academicyear").ToString()
                        txtFromdate.Text = Convert.ToDateTime(sdr("Academicfromdate")).ToString("yyyy-MM-dd")
                        txtTodate.Text = Convert.ToDateTime(sdr("Academictodate")).ToString("yyyy-MM-dd")
                        txtfromfinance.Text = Convert.ToDateTime(sdr("FinancialFromdate")).ToString("yyyy-MM-dd")
                        txttofinance.Text = Convert.ToDateTime(sdr("FinancialTodate")).ToString("yyyy-MM-dd")
                    End While
                End Using
            End Using
        End If
    End Sub

    'Function for fetch Previous year Academic date amd Financial date
    Private Sub fetchdate()
        Dim query As String = "select Top 1 * from Exam_Session order by Academicyear desc"
        Dim cmd As New SqlCommand(query)
        Using con As New SqlConnection(Constr)
            Using sda As New SqlDataAdapter()
                cmd.Connection = con
                con.Open()
                Dim dt As New DataTable
                sda.SelectCommand = cmd
                sda.Fill(dt)
                If dt.Rows.Count() Then
                    Dim acfrmon As Date = dt.Rows(0)("Academicfromdate").ToString
                    Dim actomon As Date = dt.Rows(0)("Academictodate").ToString
                    Dim fifrmon As Date = dt.Rows(0)("FinancialFromdate").ToString
                    Dim fitomon As Date = dt.Rows(0)("FinancialTodate").ToString
                    txtFromdate.Text = Convert.ToDateTime("" & acfrmon.Month.ToString() & "-" & acfrmon.Day.ToString() & "-" & txtAcademicyear.Text & "").ToString("yyyy-MM-dd")
                    txtTodate.Text = Convert.ToDateTime("" & actomon.Month.ToString() & "-" & actomon.Day.ToString() & "-" & txtAcademicyear.Text + 1 & "").ToString("yyyy-MM-dd")
                    txtfromfinance.Text = Convert.ToDateTime("" & fifrmon.Month.ToString() & "-" & fifrmon.Day.ToString() & "-" & txtAcademicyear.Text & "").ToString("yyyy-MM-dd")
                    txttofinance.Text = Convert.ToDateTime("" & fitomon.Month.ToString() & "-" & fitomon.Day.ToString() & "-" & txtAcademicyear.Text + 1 & "").ToString("yyyy-MM-dd")
                Else
                    txtFromdate.Text = ""
                    txtTodate.Text = ""
                    txtfromfinance.Text = ""
                    txttofinance.Text = ""
                End If
            End Using
        End Using
    End Sub

    'Function for fill ddl session
    Private Sub Fetchddlsession()
        Dim query As String = "select Academicyear from Exam_Session order by Academicyear desc"
        Dim cmd As New SqlCommand(query)
        Using con As New SqlConnection(Constr)
            Using sda As New SqlDataAdapter()
                cmd.Connection = con
                con.Open()
                Dim dt As New DataTable
                sda.SelectCommand = cmd
                sda.Fill(dt)
                ddlSession.DataSource = dt
                Dim session As String = ""
                If dt.Rows.Count Then
                    session = dt.Rows(0)("Academicyear").ToString()
                    session = session + 1
                    ddlSession.Items.Add(New ListItem(Convert.ToString((session).ToString() + "-" + (session + 1).ToString()), Convert.ToString((session).ToString() + "-" + (session + 1).ToString())))
                Else
                    For i = 0 To 1
                        ddlSession.Items.Add(New ListItem(Convert.ToString((Date.Now.Year + i).ToString() + "-" + (Date.Now.Year + i + 1).ToString()), Convert.ToString((Date.Now.Year + i).ToString() + "-" + (Date.Now.Year + i + 1).ToString())))
                    Next
                End If
                ddlSession.Items.Insert(0, New ListItem("Select", "Select"))
                ddlSession.Items(0).Attributes("disabled") = "disabled"
                con.Close()
            End Using
        End Using
    End Sub

    Protected Sub OnRowEditing(ByVal sender As Object, ByVal f As GridViewEditEventArgs)
    End Sub

    Protected Sub OnRowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
    End Sub

    Protected Sub OnRowDeleting(ByVal sender As Object, ByVal e As GridViewDeleteEventArgs)
        Dim constr As String = ConfigurationManager.ConnectionStrings("myconnection").ConnectionString
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand("DELETE  Exam_Session WHERE Sessionid = ''")
                Using sda As New SqlDataAdapter()
                    cmd.Connection = con
                    sda.SelectCommand = cmd
                    con.Open()
                    Using dt As New DataTable()
                        sda.Fill(dt)
                        Me.BindGrid()
                        con.Close()
                    End Using
                End Using
            End Using
        End Using

    End Sub
End Class