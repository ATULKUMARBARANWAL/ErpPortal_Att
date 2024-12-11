Imports System.Data
Imports System.Configuration
Imports System.Data.SqlClient
Namespace SaralMsg
    Public Class Messagebox
        Public Shared Sub Alert(ByVal cont As Web.UI.Control, ByVal msg As String)
            Dim scrpt As String = String.Format("alert('{0}');", msg)
            ScriptManager.RegisterStartupScript(cont, cont.GetType(), "Myscript", scrpt, True)
        End Sub
    End Class
End Namespace
Partial Class ClassRoom
    Inherits System.Web.UI.Page
    Dim constr As String = ConfigurationManager.ConnectionStrings("myconnection").ConnectionString



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            pnlgrid.Visible = True
            BindGrid()
        End If
    End Sub

    Private Sub BindGrid()
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand("select * from ClassRoom order by classid desc")
                Using sda As New SqlDataAdapter()
                    cmd.Connection = con
                    sda.SelectCommand = cmd

                    Dim dt As New DataTable()
                    sda.Fill(dt)
                    grdClassRoom.DataSource = dt
                    grdClassRoom.DataBind()
                End Using
            End Using
        End Using
    End Sub
    
    Private Sub popupAllFieldsMandatory()
        Dim msg As String = "Filling all the fields are Mandatory."
        Dim script1 As String = "window.onload= function(){alert('"
        script1 &= msg
        script1 &= "');"
        script1 &= ";}"
        ClientScript.RegisterStartupScript(Me.GetType(), "Fill all Fields", script1, True)
    End Sub
    Private Sub popupSaveSuccessful()
        Dim msg As String = "ClassRoom name saved successfully."
        Dim script1 As String = "window.onload= function(){alert('"
        script1 &= msg
        script1 &= "');"
        script1 &= ";}"
        ClientScript.RegisterStartupScript(Me.GetType(), "Fill all Fields", script1, True)
    End Sub
    Private Sub popupUpdatesucessful()
        Dim msg As String = "Updated successfully."
        Dim script1 As String = "window.onload= function(){alert('"
        script1 &= msg
        script1 &= "');"
        script1 &= ";}"
        ClientScript.RegisterStartupScript(Me.GetType(), "Fill all Fields", script1, True)
    End Sub
    Private Sub popupDeleteConfirmation()
        Dim msg As String = "Are you sure you want to delete?"
        Dim script1 As String = "window.onload= function(){alert('"
        script1 &= msg
        script1 &= "');"
        script1 &= ";}"
        ClientScript.RegisterStartupScript(Me.GetType(), "Fill all Fields", script1, True)
    End Sub
    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Dim classid As Integer = 0
        If txtClassRoom.Text = "" And txtDescription.Text = "" Then
            popupAllFieldsMandatory()
        Else
            Using con As New SqlConnection(constr)
                Using cmd As New SqlCommand("[sp_classroom]", con)
                    cmd.CommandType = CommandType.StoredProcedure
                    Using sda As New SqlDataAdapter()

                        cmd.Parameters.AddWithValue("@ClassRoom ", txtClassRoom.Text)
                        cmd.Parameters.AddWithValue("@Description ", txtDescription.Text)

                        cmd.Connection = con
                        con.Open()
                        classid = Convert.ToInt32(cmd.ExecuteScalar())
                        con.Close()
                    End Using
                End Using

                Select Case classid
                    Case -1
                        SaralMsg.Messagebox.Alert(Me, "This ClassRoom already exist")

                    Case -2
                        SaralMsg.Messagebox.Alert(Me, "This ClassRoom Description already exist")

                    Case Else
                        popupSaveSuccessful()
    'SaralMsg.Messagebox.Alert(Me, "ClassRoom Saved successfully")
                        Exit Select
                End Select
                txtClassRoom.Text = ""
                txtDescription.Text = ""
            End Using

            BindGrid()
        End If
    End Sub
    Protected Sub OnPageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs)
        grdClassRoom.PageIndex = e.NewPageIndex
        Me.BindGrid()
    End Sub
    Protected Sub grdClassRoom_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdClassRoom.RowCommand
        If e.CommandName = "Edit" Then
            btnAdd.Visible = False
            btnupdate.Visible = True
            grdClassRoom.SelectedIndex = e.CommandArgument
            Dim rowindex As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = grdClassRoom.Rows(rowindex)
            ViewState("id") = grdClassRoom.SelectedDataKey(0)
            ViewState("classid") = row.Cells(3).Text
            Using con As New SqlConnection(constr)
                Using cmd As New SqlCommand("select * from ClassRoom where classid='" & ViewState("id") & "' ")
                    cmd.Connection = con
                    con.Open()
                    Dim sdr As SqlDataReader = cmd.ExecuteReader
                    While (sdr.Read())
                        txtClassRoom.Text = sdr("ClassRoom").ToString()
                        txtDescription.Text = sdr("Description").ToString()


                    End While
                End Using
            End Using
        End If
        '..........Ddelete code............

        If e.CommandName = "Delete" Then
            grdClassRoom.SelectedIndex = e.CommandArgument
            Dim rowIndex As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = grdClassRoom.Rows(rowIndex)
            ViewState("id") = grdClassRoom.SelectedDataKey(0)
            Dim Count As Integer = 0
            Using con As New SqlConnection(constr)
                Using cmd As New SqlCommand("Select Count(*) as totalrow from ClassRoom where classid='" & ViewState("id") & "' ")
                    Using sda As New SqlDataAdapter()
                        cmd.Connection = con
                        sda.SelectCommand = cmd
                        Using dt As New DataTable()
                            sda.Fill(dt)
                            Count = dt.Rows(0)("totalrow").ToString()

                        End Using
                    End Using
                End Using

                If Count <> 0 Then
                    popupDeleteConfirmation()
                    'SaralMsg.Messagebox.Alert(Me, "Are You Sure you want to Delete?")
                    Using cmd As New SqlCommand("DELETE FROM ClassRoom where classid='" & ViewState("id") & "' ")
                        Using sda As New SqlDataAdapter()
                            cmd.Connection = con
                            sda.SelectCommand = cmd
                            Using dt As New DataTable()
                                sda.Fill(dt)
                                Me.BindGrid()

                            End Using
                        End Using
                    End Using

                End If
            End Using
        End If


    End Sub


    Protected Sub btnupdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnupdate.Click
        Using con As New SqlConnection(constr)
            Using Command As New SqlCommand("update ClassRoom set ClassRoom=@ClassRoom,Description=@Description Where classid='" & ViewState("id") & "'", con)
                Command.Parameters.AddWithValue("@ClassRoom ", txtClassRoom.Text)
                Command.Parameters.AddWithValue("@Description", txtDescription.Text)
                Command.Connection = con
                con.Open()
                Command.ExecuteNonQuery()
                con.Close()

            End Using
        End Using
        popupUpdatesucessful()
        btnAdd.Visible = True
        btnupdate.Visible = False
        Clear()
        BindGrid()
    End Sub

    Protected Sub OnRowDeleting(ByVal sender As Object, ByVal e As GridViewDeleteEventArgs)

    End Sub
    Protected Sub OnRowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)

    End Sub

    Protected Sub OnRowEditing(ByVal sender As Object, ByVal f As GridViewEditEventArgs)


    End Sub

    Private Sub Clear()
        txtClassRoom.Text = ""
        txtDescription.Text = ""
    End Sub

    
End Class
