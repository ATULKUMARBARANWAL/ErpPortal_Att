Imports System.Data
Imports System
Imports System.Configuration
Imports System.Data.SqlClient

Partial Class Payroll_updateatt
    Inherits System.Web.UI.Page
    Dim constr As String = ConfigurationManager.ConnectionStrings("myconnection").ConnectionString
    Private leavemanagement As New leavemanagement()
    Private cmd As New dbnew()
    Dim table As New DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ViewState("Deptid") = Request.QueryString("rid")
            ViewState("cid") = 1
            ViewState("departmentid") = Request.QueryString("deptid")
            TxtDate.Text = Date.Now
            fetchDdlprogram()
            bind()
        End If

    End Sub

    Protected Sub BtnLoad_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnLoad.Click
        bind()
    End Sub
    Sub bind()
        Literal1.Text = leavemanagement.Emp_attendance(ViewState("cid"), Request.QueryString("rid"), TxtDate.Text)
        Panel1.Visible = True
        msgsave.InnerHtml = ""
    End Sub
    Protected Sub Ddlprogram_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles Ddlprogram.SelectedIndexChanged
        bind1()
    End Sub
    Sub bind1()
        Literal1.Text = leavemanagement.Emp_attendance(ViewState("cid"), Ddlprogram.SelectedValue, TxtDate.Text)
        Panel1.Visible = True
        msgsave.InnerHtml = ""
    End Sub
    Private Sub fetchDdlprogram()
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand("Select departmentid, department from department order by Department ")
                Using sda As New SqlDataAdapter()
                    cmd.Connection = con
                    sda.SelectCommand = cmd
                    Using dt As New DataTable()
                        sda.Fill(dt)
                        Ddlprogram.DataSource = dt
                        Ddlprogram.DataTextField = "Department"
                        Ddlprogram.DataValueField = "Departmentid"
                        Ddlprogram.DataBind()
                        Ddlprogram.Items.FindByValue(ViewState("Deptid")).Selected = True

                    End Using
                End Using
            End Using
        End Using

    End Sub
    Protected Sub TxtDate_TextChanged(sender As Object, e As System.EventArgs) Handles TxtDate.TextChanged
        bind()
    End Sub
    
    Protected Sub ddlcollege_SelectedIndexChanged(ByVal usercontrol_collegeddlctr As usercontrol_collegeddlctr, ByVal Empty As System.EventArgs) Handles ddlcollege.SelectedIndexChanged
        ddldepartment.getdepartment(ddlcollege.SelectedValue)
        Panel1.Visible = False
        msgsave.InnerHtml = ""
    End Sub

    Protected Sub btnsave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsave.Click
        Try
            table.Columns.Add("StudentID", GetType(String))
            table.Columns.Add("Status", GetType(String))
            table.Columns.Add("timein", GetType(String))
            table.Columns.Add("timeout", GetType(String))
            table.Columns.Add("remarks", GetType(String))
            For x = 0 To 500
                If Request("idvalue_1_" & (x + 1) & "_txb") <> "" And Request("value_1_" & (x + 1) & "_txb") <> "" Then
                    table.Rows.Add(Request("idvalue_1_" & (x + 1) & "_txb"), Request("value_1_" & (x + 1) & "_txb"), Request("idvalue_in_1_" & (x + 1) & "_txb"), Request("idvalue_out_1_" & (x + 1) & "_txb"), "")
                End If
            Next
           
            If table.Rows.Count > 0 Then
                msgsave.InnerHtml = leavemanagement.insert_emp_attendance(CDate(TxtDate.Text), Request.QueryString("rid"), table)
            End If
            table.Clear()
            Panel1.Visible = False
        Catch ex As Exception
            msgsave.InnerHtml = "" = "Try Again"
        End Try
    End Sub

    Protected Sub txtDt_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDt.TextChanged
        Panel1.Visible = False
        msgsave.InnerHtml = ""
    End Sub

    Protected Sub ddldepartment_SelectedIndexChanged(ByVal usercontrol_departmentddlctr As usercontrol_departmentddlctr, ByVal Empty As System.EventArgs) Handles ddldepartment.SelectedIndexChanged
        Panel1.Visible = False
        msgsave.InnerHtml = ""
    End Sub

    Protected Sub btnheadback_Click(sender As Object, e As System.EventArgs) Handles btnheadback.Click
        Response.Redirect("dashboardemployee.aspx?Empid=" & ViewState("empid") & "&designationId=" & ViewState("leaveType") & "&programid=" & ViewState("programid"))
    End Sub

End Class
