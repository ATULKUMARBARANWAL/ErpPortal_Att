Imports System.Data
Imports System.Configuration
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic

Partial Class AddSubject
    Inherits System.Web.UI.Page
    Dim constr As String = ConfigurationManager.ConnectionStrings("myconnection").ConnectionString
    Private cmd As dbnew = New dbnew()

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            BindGrid()
        End If
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As System.EventArgs) Handles btnAdd.Click
        Dim Subjectid As Integer = 0
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand("Addsubject", con)
                cmd.CommandType = CommandType.StoredProcedure
                Using sda As New SqlDataAdapter()

                    cmd.Parameters.AddWithValue("@Subject ", txtsub.Text)
                    cmd.Parameters.AddWithValue("@Subjectcode ", txtcode.Text)
                    cmd.Parameters.AddWithValue("@Subprefix ", txtprefix.Text)
                    cmd.Connection = con
                    con.Open()
                    Subjectid = Convert.ToInt32(cmd.ExecuteScalar())
                    con.Close()
                End Using
            End Using

            Select Case Subjectid
                Case -1
                    SaralMsg.Messagebx.Alert(Me, "This Subject Name Already Exist")

                Case -2
                    SaralMsg.Messagebx.Alert(Me, "This Subject Code Already Exist")


                Case -3
                    SaralMsg.Messagebx.Alert(Me, "This Prefix Already Exist")


                Case Else
                    SaralMsg.Messagebx.Alert(Me, "This Subject Add successful")
                    Exit Select
            End Select
            txtsub.Text = ""
            txtcode.Text = ""
            txtprefix.Text = ""
        End Using

        BindGrid()
    End Sub

    Private Sub BindGrid()
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand("select * from Exam_Subject order by Subject ASC ")
                Using sda As New SqlDataAdapter()
                    cmd.Connection = con
                    sda.SelectCommand = cmd

                    Dim dt As New DataTable()
                    sda.Fill(dt)
                    grdSubject.DataSource = dt
                    grdSubject.DataBind()

                End Using
            End Using
        End Using
    End Sub
    
    Protected Sub OnRowEditing(ByVal sender As Object, ByVal f As GridViewEditEventArgs)


    End Sub

    Protected Sub OnRowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)

    End Sub
    
    Protected Sub OnRowDeleting(sender As Object, e As GridViewDeleteEventArgs)

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

    'Protected Sub OnRowDataBound(sender As Object, e As GridViewRowEventArgs)
    '    If e.Row.RowType = DataControlRowType.DataRow AndAlso e.Row.RowIndex <> grdSubject.EditIndex Then
    '        TryCast(e.Row.Cells(0).Controls(2), LinkButton).Attributes("onclick") = "return confirm('Do you want to delete this row?');"
    '    End If
    'End Sub

    Protected Sub grdSubject_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdSubject.RowCommand
        If e.CommandName = "Delete" Then
            grdSubject.SelectedIndex = e.CommandArgument
            Dim rowIndex As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = grdSubject.Rows(rowIndex)
            ViewState("id") = grdSubject.SelectedDataKey(0)
            Dim Count As Integer = 0
            Using con As New SqlConnection(constr)
                Using cmd As New SqlCommand("Select Count(*) as totalrow from Exam_Coursesubject where Subjectid='" & ViewState("id") & "' ")
                    Using sda As New SqlDataAdapter()
                        cmd.Connection = con
                        sda.SelectCommand = cmd
                        Using dt As New DataTable()
                            sda.Fill(dt)
                            Count = dt.Rows(0)("totalrow").ToString()

                        End Using
                    End Using
                End Using

                If Count = 0 Then

                    Using cmd As New SqlCommand("DELETE from Exam_Subject where not exists(select * from Exam_Coursesubject where Subjectid=Exam_Subject.Subjectid) and Subjectid='" & ViewState("id") & "' ")
                        Using sda As New SqlDataAdapter()
                            cmd.Connection = con
                            sda.SelectCommand = cmd
                            Using dt As New DataTable()
                                sda.Fill(dt)
                                Me.BindGrid()

                            End Using
                        End Using
                    End Using
                    SaralMsg.Messagebx.Alert(Me, "Deleted Successfully")
                Else
                    SaralMsg.Messagebx.Alert(Me, "This subject is mapped in program. You cannot delete this subject from now, First you have to delete it from  program.")
                End If
            End Using
        End If


        If e.CommandName = "Edit" Then
            txtprefix.ReadOnly = True
            txtcode.ReadOnly = True
            btnAdd.Visible = False
            btnupdate.Visible = True
            grdSubject.SelectedIndex = e.CommandArgument
            Dim rowindex As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = grdSubject.Rows(rowindex)
            ViewState("id") = grdSubject.SelectedDataKey(0)
            ViewState("academicyear") = row.Cells(4).Text
            Using con As New SqlConnection(constr)
                Using cmd As New SqlCommand("select * from exam_Subject where Subjectid='" & ViewState("id") & "' ")
                    cmd.Connection = con
                    con.Open()
                    Dim sdr As SqlDataReader = cmd.ExecuteReader
                    While (sdr.Read())
                        txtsub.Text = sdr("Subject").ToString()
                        txtcode.Text = sdr("Subjectcode").ToString()
                        txtprefix.Text = sdr("Subprefix").ToString()
                    End While
                End Using
            End Using
        End If
    End Sub

    Protected Sub btnupdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnupdate.Click
        Using con As New SqlConnection(constr)
            Using Command As New SqlCommand("update exam_Subject set Subjectcode=@Subjectcode,Subject=@Subject,Subprefix=@Subprefix Where Subjectid='" & ViewState("id") & "'", con)

                Command.Parameters.AddWithValue("@Subjectcode ", txtcode.Text)
                Command.Parameters.AddWithValue("@Subprefix", txtprefix.Text)
                Command.Parameters.AddWithValue("@Subject", txtsub.Text)

                con.Open()
                If txtcode.Text = "" Or txtprefix.Text = "" Or txtsub.Text = "" Then
                    Dim msg As String = "Please fill all the fields."
                    Dim script1 As String = "window.onload= function(){alert('"
                    script1 &= msg
                    script1 &= "');"
                    script1 &= ";}"
                    ClientScript.RegisterStartupScript(Me.GetType(), "Fill all Fields", script1, True)
                Else

                    Command.ExecuteNonQuery()
                    con.Close()

                    Dim msg1 As String = "Successfull Update"
                    Dim script As String = "window.onload=function(){alert('"
                    script &= msg1
                    script &= "');"
                    script &= "window.location = '"
                    script &= Request.Url.AbsoluteUri
                    script &= "'; }"
                    ClientScript.RegisterStartupScript(Me.GetType(), "SuccessMessage", script, True)

                End If

            End Using





        End Using
        btnAdd.Visible = True
        btnupdate.Visible = False
        BindGrid()
    End Sub

    Protected Sub btndelete_Click(sender As Object, e As System.EventArgs) Handles btndelete.Click
        Dim subjectsToDelete As New List(Of String)

        ' Loop through GridView rows to find checked subjects
        For Each row As GridViewRow In grdSubject.Rows
            Dim chkSelect As CheckBox = TryCast(row.FindControl("btnselect"), CheckBox)
            If chkSelect IsNot Nothing AndAlso chkSelect.Checked Then
                ' Retrieve the DataKey of the selected row
                Dim subjectId As String = grdSubject.DataKeys(row.RowIndex).Value.ToString()
                subjectsToDelete.Add(subjectId)
            End If
        Next

        ' Perform deletion after collecting all subjects
        If subjectsToDelete.Count > 0 Then
            Using con As New SqlConnection(constr)
                con.Open()

                For Each subjectId In subjectsToDelete
                    ' Check if the subject is used in Exam_Coursesubject
                    Dim count As Integer = 0
                    Using cmd As New SqlCommand("Select Count(*) as totalrow from Exam_Coursesubject where Subjectid=@Subjectid", con)
                        cmd.Parameters.AddWithValue("@Subjectid", subjectId)
                        count = Convert.ToInt32(cmd.ExecuteScalar())
                    End Using

                    If count = 0 Then
                        ' If not used, delete the subject from Exam_Subject
                        Using cmd As New SqlCommand("DELETE from Exam_Subject where not exists(select * from Exam_Coursesubject where Subjectid=Exam_Subject.Subjectid) and Subjectid=@Subjectid", con)
                            cmd.Parameters.AddWithValue("@Subjectid", subjectId)
                            cmd.ExecuteNonQuery()
                        End Using
                    Else
                        ' Show warning message for subjects mapped in a program
                        Dim sql As String
                        'sql = " select  cls.Code as sec, b.batch,CONVERT(varchar,dated,103) as dated,* from student Inner Join Course on Course.CourseID=Student.CourseID where  " & sid
                        sql = "select  Subject from Exam_Subject  where subjectid  = " & subjectId & ""



                        Dim dt As DataTable = cmd.getDataTable(sql)
                        If dt.Rows.Count > 0 Then
                            ViewState("Subject") = dt.Rows(0)("Subject").ToString
                            SaralMsg.Messagebx.Alert(Me, "Subject with Name: " & ViewState("Subject") & " is mapped in a program. You cannot delete it until it is removed from the program.")
                        End If
                    End If
                Next
            End Using

            ' Rebind the grid after all deletions
            Me.BindGrid()
            SaralMsg.Messagebx.Alert(Me, "Selected subjects deleted successfully where possible.")
        Else
            SaralMsg.Messagebx.Alert(Me, "No subjects selected for deletion.")
        End If
    End Sub
End Class
