Imports System.Data
Imports System.Configuration
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic
Namespace SaralMsg
    Public Class Messagebox
        Public Shared Sub Alert(ByVal cont As Web.UI.Control, ByVal msg As String)
            Dim scrpt As String = String.Format("alert('{0}');", msg)
            ScriptManager.RegisterStartupScript(cont, cont.GetType(), "Myscript", scrpt, True)
        End Sub
    End Class
End Namespace
Partial Class NEWFiles_HolidaySet
    Inherits System.Web.UI.Page
    Dim constr As String = ConfigurationManager.ConnectionStrings("myconnection").ConnectionString

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            getacademicyear()


            Dim Academicyear As String = ""
            Dim query As String = "select AcademicYear from Exam_Session where Sessionid=" & Request.QueryString("s") & ""
            Dim cmd As New SqlCommand(query)
            Using con As New SqlConnection(constr)
                Using sda As New SqlDataAdapter()
                    Dim dt As New DataTable
                    cmd.Connection = con
                    con.Open()
                    sda.SelectCommand = cmd
                    sda.Fill(dt)
                    If dt.Rows.Count Then
                        Academicyear = dt.Rows(0)("AcademicYear").ToString
                    End If

                    con.Close()
                End Using
            End Using

            '..by default current year select in ddlacademicyear..
            ddlAcademicyear.Items.FindByValue(Academicyear).Selected = True

            lblacademicyear.Text = ddlAcademicyear.SelectedItem.Text

            BindGrid()
        End If
    End Sub

    Private Sub getacademicyear()
        'Dim mylist As New List(Of Integer)
        'mylist.AddRange(Enumerable.Range(2010, 30).Reverse())
        'Ddlacademicyear.DataSource = mylist
        'Ddlacademicyear.DataBind()
        'Ddlacademicyear.Items.FindByValue(System.DateTime.Now.Year.ToString()).Selected = True
        'Dim Year As Integer
        'Year = Convert.ToInt32(Now.ToString("yyyy"))
        'txtacademicyear.Text = Year.ToString
        'Dim i As Integer
        'For i = Year - 10 To Year
        '    Ddlacademicyear.Items.Add(New ListItem(i.ToString(), i.ToString()))
        'Next
        'Ddlacademicyear.Items.FindByValue(System.DateTime.Now.Year.ToString()).Selected = True
        'Ddlsemyear.Items.Insert(0, New ListItem("All", ""))
        Dim query As String = "select AcademicYear from Exam_Session order by Academicyear"
        BindDropDownList1(ddlacademicyear, query, "AcademicYear", "AcademicYear", "")
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

    Public Sub BindGrid()

        Dim y As String = ""

        Dim f As String = ""

        If ddlAcademicyear.SelectedIndex <> 0 Then f = f + " where year(dated) =" + ddlAcademicyear.SelectedValue.ToString

        Dim Sql As String = "Select  Holidayid,min(Dated) as Start,MAX(Dated) as enddate,Reason,Type from Holiday " & f & " group by Reason,Type,Holidayid order by Holidayid desc  "

        GridHolidayList.DataSource = db.getDataReader(Sql)
        GridHolidayList.DataBind()
    End Sub

    'Private Sub BindGrid()
    '    Using con As New SqlConnection(constr)
    '        Using cmd As New SqlCommand("Select     Type,Dated,DATENAME(weekday, [Dated]) As day, Reason FROM Holiday order by dated desc ")
    '            Using sda As New SqlDataAdapter()
    '                cmd.Connection = con
    '                sda.SelectCommand = cmd

    '                Dim dt As New DataTable()
    '                sda.Fill(dt)
    '                GridHolidayList.DataSource = dt
    '                GridHolidayList.DataBind()
    '            End Using
    '        End Using
    '    End Using
    'End Sub


    'Protected Sub Button1_Click(sender As Object, e As System.EventArgs) Handles Button1.Click

    'End Sub
    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button1.Click

        Dim sql As String = ""


        Dim cmd As New SqlCommand With {.Connection = db.Con}
        Dim Rvalue As Integer

        cmd.Parameters.AddWithValue("Dt1", txtDateFrom.Text)
        cmd.Parameters.AddWithValue("Dt2", txtDateTo.Text)
        cmd.Parameters.AddWithValue("Dated", txtDateFrom.Text)
        cmd.Parameters.AddWithValue("Type", ddtype.Text)
        cmd.Parameters.AddWithValue("instid", 0)
        cmd.Parameters.AddWithValue("Reason", IIf(TextBox1.Text = String.Empty, DBNull.Value, TextBox1.Text))
        Dim Trans As SqlTransaction = Nothing

        Dim Dt1 As Date, Dt2 As Date
        Dt1 = txtDateFrom.Text
        Dt2 = txtDateTo.Text

        Try
            If Dt1 <= Dt2 Then




                cmd.CommandText = "SELECT     COUNT(*) FROM Holiday WHERE     (Dated BETWEEN @Dt1 AND @Dt2) and cid=@instid"
                If CInt(cmd.ExecuteScalar) > 0 Then Throw New Exception("Date Already Assigned of holiday")


                Do While Not (Dt1 > Dt2)
                    cmd.CommandText = "INSERT INTO Holiday (Dated,Type,Reason,cid) VALUES (@Dated,@Type,@Reason,@instid)"
                    Rvalue += cmd.ExecuteNonQuery()
                    Dt1 = Dt1.AddDays(1)
                    cmd.Parameters("Dated").Value = Dt1
                    SaralMsg.Messagebx.Alert(Me, "Saved Successfully")
                    ddtype.SelectedIndex = -1
                    txtDateFrom.Text = ""
                    txtDateTo.Text = ""
                    TextBox1.Text = ""
                Loop

            Else
                SaralMsg.Messagebx.Alert(Me, "From date must be greater than To date")
            End If
        Catch ex As Exception
            Throw ex

        End Try


        BindGrid()
    End Sub


    Protected Sub GridHolidayList_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridHolidayList.RowCommand
        If e.CommandName = "Edit" Then

            Button1.Visible = False
            btnupdate.Visible = True
            GridHolidayList.SelectedIndex = e.CommandArgument
            Dim rowindex As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = GridHolidayList.Rows(rowindex)
            ViewState("id") = GridHolidayList.SelectedDataKey(0)
            'ViewState("academicyear") = row.Cells(4).Text
            Using con As New SqlConnection(constr)
                Using cmd As New SqlCommand("Select Convert(varchar, min(Dated),101) as Start,Convert(varchar,MAX(Dated),101) as enddate,Reason,Type from Holiday where HolidayId='" & GridHolidayList.SelectedDataKey(0) & "' group by Reason,Type ")
                    cmd.Connection = con
                    con.Open()
                    Dim sdr As SqlDataReader = cmd.ExecuteReader
                    While (sdr.Read())
                        ddtype.SelectedItem.Text = sdr("Type").ToString()
                        txtDateFrom.Text = Convert.ToDateTime(sdr("Start")).ToString("yyyy-MM-dd")
                        txtDateTo.Text = Convert.ToDateTime(sdr("enddate")).ToString("yyyy-MM-dd")
                        TextBox1.Text = sdr("Reason").ToString()
                    End While
                End Using
            End Using

        End If
        If e.CommandName = "Delete" Then

            GridHolidayList.SelectedIndex = e.CommandArgument
            Dim rowIndex As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = GridHolidayList.Rows(rowIndex)
            ViewState("id") = GridHolidayList.SelectedDataKey(0)
            'ViewState("academicyear") = row.Cells(4).Text
            Using con As New SqlConnection(constr)
                Using cmd As New SqlCommand("Delete from Holiday where  Holidayid='" & ViewState("id") & "'")
                    cmd.Connection = con
                    con.Open()
                    cmd.ExecuteNonQuery()
                    con.Close()
                End Using
            End Using
            BindGrid()

        End If
    End Sub
    Protected Sub btnupdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnupdate.Click
        Dim sql As String = ""
        Dim cmd As New SqlCommand With {.Connection = db.Con}
        Dim Rvalue As Integer

        cmd.Parameters.AddWithValue("Dt1", txtDateFrom.Text)
        cmd.Parameters.AddWithValue("Dt2", txtDateTo.Text)
        cmd.Parameters.AddWithValue("Dated", txtDateFrom.Text)
        cmd.Parameters.AddWithValue("Type", ddtype.Text)
        cmd.Parameters.AddWithValue("instid", 0)
        cmd.Parameters.AddWithValue("Reason", IIf(TextBox1.Text = String.Empty, DBNull.Value, TextBox1.Text))
        Dim Trans As SqlTransaction = Nothing

        Dim Dt1 As Date, Dt2 As Date
        Dt1 = txtDateFrom.Text
        Dt2 = txtDateTo.Text

        Do While Not (Dt1 > Dt2)
            cmd.CommandText = "update Holiday set Dated=@Dated,Type=@Type,Reason=@Reason Where HolidayId='" & GridHolidayList.SelectedDataKey(0) & "'"
            Rvalue += cmd.ExecuteNonQuery()
            Dt1 = Dt1.AddDays(1)
            cmd.Parameters("Dated").Value = Dt1
        Loop
        SaralMsg.Messagebx.Alert(Me, "Updated Successfully!")
        'Clear()
        Button1.Visible = True
        btnupdate.Visible = False

        BindGrid()
    End Sub


    'Private Sub Clear()
    '    txtDateFrom.Text = "" And txtDateTo.Text = "" And TextBox1.Text = "" And ddtype.SelectedItem.Text = ""

    'End Sub

    'Protected Sub GridHolidayList_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GridHolidayList.RowDeleting
    '    Try
    '        Dim subjectID As String = CDate(GridHolidayList.DataKeys(e.RowIndex).Values(0)).ToShortDateString
    '        Dim sql As String = "Delete from Holiday where Dated='" & subjectID & "' "
    '        db.ExecuteNonQuery(sql)
    '        BindGrid()
    '    Catch ex As Exception
    '        ex.Message.ToString()
    '    End Try
    'End Sub

    Protected Sub OnRowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)

    End Sub

    Protected Sub OnRowDeleting(ByVal sender As Object, ByVal e As GridViewDeleteEventArgs)
        Dim constr As String = ConfigurationManager.ConnectionStrings("myconnection").ConnectionString
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand("DELETE  Exam_Subject WHERE Subjectid = ''")
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

    Protected Sub OnRowEditing(ByVal sender As Object, ByVal f As GridViewEditEventArgs)

    End Sub


    Protected Sub btnclear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnclear.Click
        txtDateFrom.Text = ""
        txtDateTo.Text = ""
        TextBox1.Text = ""
        ddtype.SelectedItem.Text = ""
    End Sub
End Class
