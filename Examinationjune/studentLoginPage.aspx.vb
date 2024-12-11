
'Design By Shivani And Developed By Avaneesh Yadav

Imports System.Data
Imports System.IO
Imports System.Net
Imports System.Data.SqlClient
Imports System.Net.Mail
Partial Class TESTFILES_LoginPage
    Inherits System.Web.UI.Page

    Dim constr As String = ConfigurationManager.ConnectionStrings("myconnection").ConnectionString
    Dim con As New SqlConnection(constr)
    Private _sessionState As String

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

    End Sub
    Protected Sub Button1_Click(sender As Object, e As System.EventArgs) Handles Button1.Click
        Dim UserId As Integer = 0
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand("Validate_User")
                Using sda As New SqlDataAdapter()
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.AddWithValue("@AdmissionNo ", TextBox1.Text.Trim())
                    cmd.Parameters.AddWithValue("@Password", TextBox2.Text.Trim())

                    cmd.Connection = con
                    con.Open()
                    UserId = Convert.ToInt32(cmd.ExecuteScalar())
                    con.Close()
                End Using
            End Using
            Select Case UserId
                Case -1
                    SaralMsg.Messagebx.Alert(Me, "Enter Correct Username Password")
                    Exit Select
                Case -2
                    SaralMsg.Messagebx.Alert(Me, "Accountuhyvdiuhfx Already Created")
                    Exit Select
                Case Else

                    Response.Redirect("../DahboardExam.aspx?rid=" & TextBox1.Text.Trim() & "")
                    Exit Select
            End Select
        End Using

    End Sub
End Class
