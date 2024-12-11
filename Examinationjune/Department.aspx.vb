Imports System.Data
Imports System.Text
Imports System.Configuration
Imports System.Data.SqlClient
Partial Class Examinationjune_Department
    Inherits System.Web.UI.Page
    Dim constr As String = ConfigurationManager.ConnectionStrings("myconnection").ConnectionString

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            ViewState("Sessionid") = Request.QueryString("s")
            ViewState("ayid") = Request.QueryString("ay")

            Bindgrid()

        End If
    End Sub

    Private Sub Bindgrid()
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand()
                Dim query As String = "Select i.Institue,i.InstitueID,COUNT(b.Branchid) as Total from Institue i left join Branch b on b.Collegeid=i.InstitueID group by i.Institue,i.InstitueID order by i.Institue"
                cmd.CommandText = query
                Using sda As New SqlDataAdapter()
                    cmd.Connection = con
                    sda.SelectCommand = cmd
                    Using dt As New DataTable()
                        sda.Fill(dt)
                        grddepartment.DataSource = dt
                        grddepartment.DataBind()
                        Griddepart.DataSource = dt
                        Griddepart.DataBind()
                    End Using
                End Using
            End Using
        End Using
    End Sub

    Protected Sub btnAddDep_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddDep.Click
        pnldepartments.Visible = False
        Pnladddepart.Visible = True
        Bindgrid()
    End Sub

    Protected Sub Griddepart_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Griddepart.RowCommand
        ViewState("departmentid") = ""
        If e.CommandName = "Edit" Then
            Griddepart.SelectedIndex = e.CommandArgument
            Dim rowIndex As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = Griddepart.Rows(rowIndex)
            ViewState("departmentid") = Griddepart.SelectedDataKey(0)
            btnsubmit.Visible = False
            btnupdate.Visible = True
            Using con As New SqlConnection(constr)
                Using cmd As New SqlCommand()
                    Dim query As String = "Select * from Institue where InstitueID='" & ViewState("departmentid") & "'"
                    cmd.CommandText = query
                    Using sda As New SqlDataAdapter()
                        cmd.Connection = con
                        sda.SelectCommand = cmd
                        Using dt As New DataTable()
                            sda.Fill(dt)
                            If dt.Rows.Count Then
                                txtdepartmentname.Text = dt.Rows(0)("Institue").ToString()
                                End If
                            End Using
                        End Using
                End Using
            End Using

        End If

        If e.CommandName = "Delete" Then
            Griddepart.SelectedIndex = e.CommandArgument
            Dim rowIndex As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = Griddepart.Rows(rowIndex)
            ViewState("departmentid") = Griddepart.SelectedDataKey(0)
           
            Using con As New SqlConnection(constr)
                Using cmd As New SqlCommand()
                    Dim query As String = "Select * from Exam_Course where CID='" & ViewState("departmentid") & "'"
                    cmd.CommandText = query
                    Using sda As New SqlDataAdapter()
                        cmd.Connection = con
                        sda.SelectCommand = cmd
                        Using dt As New DataTable()
                            sda.Fill(dt)
                            If dt.Rows.Count Then
                                SaralMsg.Messagebx.Alert(Me, "This Department is in Processing")
                                Exit Sub
                            Else

                                DeleteDepart(ViewState("departmentid"))
                                SaralMsg.Messagebx.Alert(Me, "Deleted Successfully")
                                Bindgrid()
                            End If
                        End Using
                    End Using
                End Using
            End Using

        End If




    End Sub

    Protected Sub btnupdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnupdate.Click

        Using con As SqlConnection = New SqlConnection(constr)
            Dim query As String = ""

            query = "update Institue set Institue=@Institue where Institueid='" & ViewState("departmentid") & "'"


            Dim cmd As SqlCommand = New SqlCommand(query, con)

            cmd.Parameters.AddWithValue("@Institue", txtdepartmentname.Text)

            con.Open()
            cmd.ExecuteNonQuery()
            con.Close()

            SaralMsg.Messagebx.Alert(Me, "Successfully Update")

            Bindgrid()
            txtdepartmentname.Text = ""
            btnsubmit.Visible = True
            btnupdate.Visible = False

        End Using

    End Sub

    Protected Sub btnsubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsubmit.Click

        Using con As SqlConnection = New SqlConnection(constr)
            Dim query As String = ""

            query = "Select Count(*) as Count from Institue where Institue='" & txtdepartmentname.Text & "'"
            Dim count As Integer = 0
            Dim cmd As SqlCommand = New SqlCommand(query, con)
            cmd.CommandText = query
            Using sda As New SqlDataAdapter()
                cmd.Connection = con
                sda.SelectCommand = cmd
                Using dt As New DataTable()
                    sda.Fill(dt)
                    count = dt.Rows(0)("Count").ToString()
                End Using
            End Using


            If count = 0 Then

                If txtdepartmentname.Text = "" Then
                    SaralMsg.Messagebx.Alert(Me, "Fill Department Name")
                Else
                    Dim query1 As String = "insert into Institue(Institue) values(@Institue)"

                    Dim cmd1 As SqlCommand = New SqlCommand(query1, con)
                    cmd1.Parameters.AddWithValue("@Institue", txtdepartmentname.Text)

                    con.Open()
                    cmd1.ExecuteNonQuery()
                    con.Close()

                    SaralMsg.Messagebx.Alert(Me, "Successfully Save")
                    Bindgrid()
                    txtdepartmentname.Text = ""
                End If

              
            Else
                SaralMsg.Messagebx.Alert(Me, "Department Name Already Exists")
            End If

        End Using

    End Sub

    Private Sub DeleteDepart(ByVal Departid As Object)
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand()
                Dim query As String = "Delete from Institue where InstitueID='" & Departid & "'"
                cmd.CommandText = query
                cmd.Connection = con
                con.Open()
                cmd.ExecuteNonQuery()
                con.Close()
            End Using
        End Using
    End Sub

    Protected Sub btnback_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnback.Click
        pnldepartments.Visible = True
        Pnladddepart.Visible = False
        Bindgrid()
    End Sub

    Protected Sub OnRowEditing(ByVal sender As Object, ByVal f As GridViewEditEventArgs)
    End Sub

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

    Protected Sub grddepartment_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grddepartment.RowCommand
        ViewState("Deparid") = ""
        If e.CommandName = "Branch" Then
            grddepartment.SelectedIndex = e.CommandArgument
            Dim rowIndex As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = grddepartment.Rows(rowIndex)
            Dim departid As String = grddepartment.SelectedDataKey(0)
            ViewState("Deparid") = departid
            pnldepartments.Visible = False
            PnlBranch.Visible = True
            lbldepart.Text = row.Cells(2).Text

            gridbranch(departid)
           

        End If

    End Sub

    Protected Sub btnbakbranch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnbakbranch.Click
        PnlBranch.Visible = False
        pnldepartments.Visible = True
        Bindgrid()
    End Sub

    Private Sub gridbranch(ByVal departid As String)
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand()
                Dim query As String = "Select * from Branch where Collegeid='" & departid & "' order by Code"
                cmd.CommandText = query
                Using sda As New SqlDataAdapter()
                    cmd.Connection = con
                    sda.SelectCommand = cmd
                    Using dt As New DataTable()
                        sda.Fill(dt)

                        Grdbranch.DataSource = dt
                        Grdbranch.DataBind()

                    End Using
                End Using
            End Using
        End Using
    End Sub

    Protected Sub btnaddbranch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnaddbranch.Click
        Using con As SqlConnection = New SqlConnection(constr)
            Dim query As String = ""

            query = "Select Count(*) as Count from Branch where Code='" & txtdepartmentname.Text & "' and Collegeid='" & ViewState("Deparid") & "'"
            Dim count As Integer = 0
            Dim cmd As SqlCommand = New SqlCommand(query, con)
            cmd.CommandText = query
            Using sda As New SqlDataAdapter()
                cmd.Connection = con
                sda.SelectCommand = cmd
                Using dt As New DataTable()
                    sda.Fill(dt)
                    count = dt.Rows(0)("Count").ToString()
                End Using
            End Using


            If count = 0 Then


                If txtbranch.Text = "" Then
                    SaralMsg.Messagebx.Alert(Me, "Fill Branch Name")
                Else

                    Dim query1 As String = "insert into Branch(Code,collegeid) values(@Branch,@collegeid)"

                    Dim cmd1 As SqlCommand = New SqlCommand(query1, con)
                    cmd1.Parameters.AddWithValue("@Branch", txtbranch.Text)
                    cmd1.Parameters.AddWithValue("@collegeid", ViewState("Deparid"))
                    con.Open()
                    cmd1.ExecuteNonQuery()
                    con.Close()

                    SaralMsg.Messagebx.Alert(Me, "Successfully Save")
                    gridbranch(ViewState("Deparid"))
                    txtbranch.Text = ""

                End If

            Else
                SaralMsg.Messagebx.Alert(Me, "Branch Name Already Exists in this Department")
            End If

        End Using
    End Sub


    Protected Sub Grdbranch_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Grdbranch.RowCommand

        ViewState("branchid") = ""
        If e.CommandName = "Edit" Then
            Grdbranch.SelectedIndex = e.CommandArgument
            Dim rowIndex As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = Grdbranch.Rows(rowIndex)
            ViewState("branchid") = Grdbranch.SelectedDataKey(0)
            btnaddbranch.Visible = False
            btnupdatebranch.Visible = True
            Using con As New SqlConnection(constr)
                Using cmd As New SqlCommand()
                    Dim query As String = "Select * from Branch where BranchID='" & ViewState("branchid") & "'"
                    cmd.CommandText = query
                    Using sda As New SqlDataAdapter()
                        cmd.Connection = con
                        sda.SelectCommand = cmd
                        Using dt As New DataTable()
                            sda.Fill(dt)
                            If dt.Rows.Count Then
                                txtbranch.Text = dt.Rows(0)("code").ToString()
                            End If
                        End Using
                    End Using
                End Using
            End Using

        End If

        If e.CommandName = "Delete" Then
            Grdbranch.SelectedIndex = e.CommandArgument
            Dim rowIndex As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = Grdbranch.Rows(rowIndex)
            ViewState("branchid") = Grdbranch.SelectedDataKey(0)

            Using con As New SqlConnection(constr)
                Using cmd As New SqlCommand()
                    Dim query As String = "Select * from Exam_Course where Departmentid='" & ViewState("branchid") & "'"
                    cmd.CommandText = query
                    Using sda As New SqlDataAdapter()
                        cmd.Connection = con
                        sda.SelectCommand = cmd
                        Using dt As New DataTable()
                            sda.Fill(dt)
                            If dt.Rows.Count Then
                                SaralMsg.Messagebx.Alert(Me, "This Branch is in Processing")
                                Exit Sub
                            Else

                                Deletebranch(ViewState("branchid"))
                                SaralMsg.Messagebx.Alert(Me, "Deleted Successfully")
                                gridbranch(ViewState("Deparid"))
                            End If
                        End Using
                    End Using
                End Using
            End Using

        End If


    End Sub


    Protected Sub btnupdatebranch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnupdatebranch.Click

        Using con As SqlConnection = New SqlConnection(constr)
            Dim query As String = ""

            query = "update Branch set Code=@Code where Branchid='" & ViewState("branchid") & "'"


            Dim cmd As SqlCommand = New SqlCommand(query, con)

            cmd.Parameters.AddWithValue("@Code", txtbranch.Text)

            con.Open()
            cmd.ExecuteNonQuery()
            con.Close()

            SaralMsg.Messagebx.Alert(Me, "Successfully Update")

            gridbranch(ViewState("Deparid"))
            txtbranch.Text = ""
            btnaddbranch.Visible = True
            btnupdatebranch.Visible = False

        End Using

    End Sub

    Private Sub Deletebranch(ByVal branchid As Object)
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand()
                Dim query As String = "Delete from Branch where BranchID='" & branchid & "'"
                cmd.CommandText = query
                cmd.Connection = con
                con.Open()
                cmd.ExecuteNonQuery()
                con.Close()
            End Using
        End Using
    End Sub




End Class
