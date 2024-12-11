Imports System.Data.OleDb
Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.IO.Path
Imports System.Web.UI
Imports System.Drawing

Namespace SaralMsg
    Public Class Messagebx
        Public Shared Sub Alert(ByVal cont As Web.UI.Control, ByVal msg As String)
            Dim scrpt As String = String.Format("alert('{0}');", msg)
            ScriptManager.RegisterStartupScript(cont, cont.GetType(), "Myscript", scrpt, True)
        End Sub
    End Class
End Namespace


Partial Class Leads_Importtest
    Inherits System.Web.UI.Page
    Dim constr As String = ConfigurationManager.ConnectionStrings("myconnection").ConnectionString

    Public Sub Check(ByVal sender As Object, ByVal e As System.EventArgs)
        If CType(grvExcelData.HeaderRow.FindControl("chkall"), CheckBox).Checked Then
            For Each rw As GridViewRow In grvExcelData.Rows
                CType(rw.FindControl("chkselect"), CheckBox).Checked = True
            Next
        Else
            For Each rw As GridViewRow In grvExcelData.Rows
                CType(rw.FindControl("chkselect"), CheckBox).Checked = False
            Next
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            upload_file.Attributes("onchange") = "UploadFile(this)"
        End If

        ViewState("x") = 0
    End Sub


    Private Sub Upload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try



            'Upload and save the file
            If ViewState("x") = 0 Then

                ViewState("x") = 1
                If upload_file.HasFile <> True Then
                    Exit Sub
                End If

                Dim excelPath As String = Server.MapPath("~/Hemail/") + Path.GetFileName(upload_file.PostedFile.FileName)
                upload_file.SaveAs(excelPath)

                Dim connString As String = String.Empty
                Dim extension As String = Path.GetExtension(upload_file.PostedFile.FileName)
                Select Case extension
                    Case ".xls"
                        'Excel 97-03
                        connString = ConfigurationManager.ConnectionStrings("Excel03ConString").ConnectionString
                        Exit Select
                    Case ".xlsx"
                        'Excel 07 or higher
                        connString = ConfigurationManager.ConnectionStrings("Excel07+ConString").ConnectionString
                        Exit Select
                End Select
                connString = String.Format(connString, excelPath)
                Using excel_con As New OleDbConnection(connString)
                    excel_con.Open()
                    Dim sheet1 As String = excel_con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, Nothing).Rows(0)("TABLE_NAME").ToString()
                    Dim dtExcelData As New DataTable()


                    Using oda As New OleDbDataAdapter((Convert.ToString("SELECT * FROM [") & sheet1) + "]", excel_con)
                        oda.Fill(dtExcelData)
                    End Using

                    excel_con.Close()
                    ViewState("dt") = dtExcelData
                    grvExcelData.DataSource = dtExcelData
                    grvExcelData.DataBind()

                    Dim _filename As String = ""

                    _filename = Path.GetFileName(excelPath)

                    filepath.Text = _filename.ToString
                End Using
            End If

        Catch ex As Exception
            Dim message As String = "Please Select Coreect File."
            Dim script As String = "window.onload=function(){alert('"
            script &= message
            script &= "');"
            script &= "window.location = '"
            script &= Request.Url.AbsoluteUri
            script &= "'; }"
            ClientScript.RegisterStartupScript(Me.GetType(), "SuccessMessage", script, True)
        End Try

    End Sub

    Sub bind1()
        grvExcelData.DataSource = ViewState("dt")
        grvExcelData.DataBind()
    End Sub

    Protected Sub btnsubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsubmit.Click


        For Each row As GridViewRow In grvExcelData.Rows

            Dim RegNo As String = row.Cells(2).Text
            Dim ROllno As String = row.Cells(3).Text
            Dim Student As String = row.Cells(4).Text
            Dim Course As String = row.Cells(5).Text
            Dim Sem As String = row.Cells(6).Text
            Dim AdmType As String = row.Cells(7).Text
            Dim Gender As String = row.Cells(8).Text
            Dim Batch As String = row.Cells(9).Text
            Dim DOB As String = row.Cells(10).Text
            Dim FatherName As String = row.Cells(11).Text
            Dim FatherOccupation As String = row.Cells(12).Text
            Dim Income As String = row.Cells(14).Text
            Dim FamilyIncome As String = row.Cells(15).Text
            Dim MotherName As String = row.Cells(16).Text
            Dim MotherOccupation As String = row.Cells(17).Text
            Dim Religion As String = row.Cells(18).Text
            Dim CasteCategory As String = row.Cells(19).Text
            Dim Institue As String = row.Cells(20).Text
            Dim BloodGroup As String = row.Cells(21).Text
            Dim CorrAddress1 As String = row.Cells(22).Text
            Dim CorrAddress2 As String = row.Cells(23).Text
            Dim CorrCity As String = row.Cells(24).Text
            Dim CorrDistt As String = row.Cells(25).Text
            Dim CorrState As String = row.Cells(26).Text
            Dim CorrCountry As String = row.Cells(27).Text
            Dim CorrPinCode As String = row.Cells(28).Text
            Dim Mobile As String = row.Cells(29).Text
            Dim Email As String = row.Cells(30).Text
            Dim GuardMobile As String = row.Cells(31).Text
            Dim Nationality As String = row.Cells(32).Text
            Dim AdharNo As String = row.Cells(33).Text
            Dim Enrollmentno As String = row.Cells(34).Text

            Using con As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("myconnection").ConnectionString)
                Dim cmd As New SqlCommand("Select RegNo from Temporaryimr where RegNo=@RegNo", con)

                cmd.Parameters.AddWithValue("@RegNo", RegNo)
                Dim da As New SqlDataAdapter(cmd)
                Dim dt As New DataTable()

                da.Fill(dt)

                If dt.Rows.Count > 0 Then

                    Insertwrongtest(RegNo, Student, Course, Sem, AdmType, Gender, Batch, DOB, FatherName, FatherOccupation, Income, FamilyIncome, MotherName, MotherOccupation, Religion, CasteCategory, Institue, BloodGroup, CorrAddress1, CorrAddress2, CorrCity, CorrDistt, CorrState, CorrCountry, CorrPinCode, Mobile, Email, GuardMobile, Nationality, AdharNo)

                ElseIf RegNo = "" Or Student = "" Or Course = "" Or Sem = "" Or AdmType = "" Or Gender = "" Or Batch = "" Or DOB = "" Or FatherName = "" Or MotherName = "" Or Institue = "" Or Mobile = "" Or Email = "" Or AdharNo = "" Then

                    Insertwrongtest(RegNo, Student, Course, Sem, AdmType, Gender, Batch, DOB, FatherName, FatherOccupation, Income, FamilyIncome, MotherName, MotherOccupation, Religion, CasteCategory, Institue, BloodGroup, CorrAddress1, CorrAddress2, CorrCity, CorrDistt, CorrState, CorrCountry, CorrPinCode, Mobile, Email, GuardMobile, Nationality, AdharNo)

                Else

                    Inserttest(RegNo, ROllno, Student, Course, Sem, AdmType, Gender, Batch, DOB, FatherName, FatherOccupation, Income, FamilyIncome, MotherName, MotherOccupation, Religion, CasteCategory, Institue, BloodGroup, CorrAddress1, CorrAddress2, CorrCity, CorrDistt, CorrState, CorrCountry, CorrPinCode, Mobile, Email, GuardMobile, Nationality, AdharNo, Enrollmentno)
                    Panel1.Height = 0

                End If
                Countwrongleads()

                pnlcorrectIncorrect.Visible = True


            End Using

        Next

        btnsubmit.Visible = False
        grvExcelData.Visible = False
        GridView1.Visible = True
        bindgrid()
        btnUPDATE.Visible = True

    End Sub



    Private Sub Countwrongleads()
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand("select count(RegNo) as leadsWrong  from TemporaryWrongimr", con)
                Dim da As New DataSet
                Dim ds As New SqlDataAdapter(cmd)

                ds.Fill(da)
                Dim i = da.Tables(0).Rows.Count
                If i > 0 Then
                    Lblwrongleads.Text = da.Tables(0).Rows(0)("leadsWrong").ToString

                End If

            End Using
        End Using
    End Sub

    Private Sub fillgridwrongleads()
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand("Select * from [TemporaryWrongimr]", con)
                Dim da As New DataSet
                Dim ds As New SqlDataAdapter(cmd)

                ds.Fill(da)
                grdwrongpanel.DataSource = da
                grdwrongpanel.DataBind()


            End Using
        End Using
    End Sub

    Private Sub Messagepop(ByVal p1 As String)
        Dim message As String = p1
        Dim script As String = "window.onload=function(){alert('"
        script &= message
        script &= "');"


        script &= "; }"
        ClientScript.RegisterStartupScript(Me.GetType(), "SuccessMessage", script, True)
    End Sub


    Private Sub Inserttest(ByVal RegNo As String, ByVal Rollno As String, ByVal Student As String, ByVal Course As String, ByVal Sem As String, ByVal AdmType As String, ByVal Gender As String, ByVal Batch As String, ByVal DOB As String, ByVal FatherName As String, ByVal FatherOccupation As String, ByVal Income As String, ByVal FamilyIncome As String, ByVal MotherName As String, ByVal MotherOccupation As String, ByVal Religion As String, ByVal CasteCategory As String, ByVal Institue As String, ByVal BloodGroup As String, ByVal CorrAddress1 As String, ByVal CorrAddress2 As String, ByVal CorrCity As String, ByVal CorrDistt As String, ByVal CorrState As String, ByVal CorrCountry As String, ByVal CorrPinCode As String, ByVal Mobile As String, ByVal Email As String, ByVal GuardMobile As String, ByVal Nationality As String, ByVal AdharNo As String, ByVal Enrollmentno As String)
        Using con As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("myconnection").ConnectionString)
            Using cmd As SqlCommand = New SqlCommand("Insert into Temporaryimr(RegNo,Rollno,Student,Course,Sem,AdmType,Gender,Batch,DOB,FatherName,FatherOccupation,Income,FamilyIncome,MotherName,MotherOccupation,Religion,CasteCategory,Institue,BloodGroup,CorrAddress1,CorrAddress2,CorrCity,CorrDistt,CorrState,CorrCountry,CorrPinCode,Mobile,Email,GuardMobile,Nationality,AdharNo,Enrollmentno)" & _
    "values(@RegNo,@Rollno,@Student,@Course,@Sem,@AdmType,@Gender,@Batch,@DOB,@FatherName,@FatherOccupation,@Income,@FamilyIncome,@MotherName,@MotherOccupation,@Religion,@CasteCategory,@Institue,@BloodGroup,@CorrAddress1,@CorrAddress2,@CorrCity,@CorrDistt,@CorrState,@CorrCountry,@CorrPinCode,@Mobile,@Email,@GuardMobile,@Nationality,@AdharNo,@Enrollmentno)", con)
                cmd.CommandType = CommandType.Text

                cmd.Parameters.AddWithValue("@RegNo", RegNo)
                cmd.Parameters.AddWithValue("@Rollno", Rollno)
                cmd.Parameters.AddWithValue("@Student", Student)
                cmd.Parameters.AddWithValue("@Course", Course)
                cmd.Parameters.AddWithValue("@Sem", Sem)
                cmd.Parameters.AddWithValue("@AdmType", AdmType)
                cmd.Parameters.AddWithValue("@Gender", Gender)
                cmd.Parameters.AddWithValue("@Batch", Batch)
                cmd.Parameters.AddWithValue("@DOB", DOB)
                cmd.Parameters.AddWithValue("@FatherName", FatherName)
                cmd.Parameters.AddWithValue("@FatherOccupation", FatherOccupation)
                cmd.Parameters.AddWithValue("@Income", Income)
                cmd.Parameters.AddWithValue("@FamilyIncome", FamilyIncome)
                cmd.Parameters.AddWithValue("@MotherName", MotherName)
                cmd.Parameters.AddWithValue("@MotherOccupation", MotherOccupation)
                cmd.Parameters.AddWithValue("@Religion", Religion)
                cmd.Parameters.AddWithValue("@CasteCategory", CasteCategory)
                cmd.Parameters.AddWithValue("@Institue", Institue)
                cmd.Parameters.AddWithValue("@BloodGroup", BloodGroup)
                cmd.Parameters.AddWithValue("@CorrAddress1", CorrAddress1)
                cmd.Parameters.AddWithValue("@CorrAddress2", CorrAddress2)
                cmd.Parameters.AddWithValue("@CorrCity", CorrCity)
                cmd.Parameters.AddWithValue("@CorrDistt", CorrDistt)
                cmd.Parameters.AddWithValue("@CorrState", CorrState)
                cmd.Parameters.AddWithValue("@CorrCountry", CorrCountry)
                cmd.Parameters.AddWithValue("@CorrPinCode", CorrPinCode)
                cmd.Parameters.AddWithValue("@Mobile", Mobile)
                cmd.Parameters.AddWithValue("@Email", Email)
                cmd.Parameters.AddWithValue("@GuardMobile", GuardMobile)
                cmd.Parameters.AddWithValue("@Nationality", Nationality)
                cmd.Parameters.AddWithValue("@AdharNo", AdharNo)
                cmd.Parameters.AddWithValue("@Enrollmentno", Enrollmentno)
                con.Open()
                cmd.ExecuteNonQuery()
                con.Close()

            End Using
        End Using


    End Sub


    Protected Sub btnUPDATE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUPDATE.Click



        Using con As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("myconnection").ConnectionString)
            Using cmd As SqlCommand = New SqlCommand("testimport", con)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@ayid", Request.QueryString("ay"))
                cmd.Parameters.AddWithValue("@userid", Request.QueryString("u"))
                cmd.Parameters.AddWithValue("@sessionid", Request.QueryString("s"))
                cmd.Parameters.AddWithValue("@evenodd", Request.QueryString("e"))
                con.Open()
                cmd.ExecuteNonQuery()
                con.Close()


                SaralMsg.Messagebx.Alert(Me, "Successfully Import Data")
            End Using
        End Using

    End Sub

    Private Sub bindgrid()
        Using con As New SqlConnection(constr)
            Dim cmd As New SqlCommand()
            Dim sql As String = "Select * from [Temporaryimr]"
            cmd.CommandText = sql
            Using sda As New SqlDataAdapter()
                cmd.Connection = con
                sda.SelectCommand = cmd
                Using dt As New DataTable()
                    sda.Fill(dt)
                    GridView1.DataSource = dt
                    GridView1.DataBind()
                End Using
            End Using
        End Using
    End Sub

    Private Sub Insertwrongtest(ByVal RegNo As String, ByVal Student As String, ByVal Course As String, ByVal Sem As String, ByVal AdmType As String, ByVal Gender As String, ByVal Batch As String, ByVal DOB As String, ByVal FatherName As String, ByVal FatherOccupation As String, ByVal Income As String, ByVal FamilyIncome As String, ByVal MotherName As String, ByVal MotherOccupation As String, ByVal Religion As String, ByVal CasteCategory As String, ByVal Institue As String, ByVal BloodGroup As String, ByVal CorrAddress1 As String, ByVal CorrAddress2 As String, ByVal CorrCity As String, ByVal CorrDistt As String, ByVal CorrState As String, ByVal CorrCountry As String, ByVal CorrPinCode As String, ByVal Mobile As String, ByVal Email As String, ByVal GuardMobile As String, ByVal Nationality As String, ByVal AdharNo As String)

        Using con As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("myconnection").ConnectionString)
            Using cmd As SqlCommand = New SqlCommand("Insert into TemporaryWrongimr(RegNo,Student,Course,Sem,AdmType,Gender,Batch,DOB,FatherName,FatherOccupation,Income,FamilyIncome,MotherName,MotherOccupation,Religion,CasteCategory,Institue,BloodGroup,CorrAddress1,CorrAddress2,CorrCity,CorrDistt,CorrState,CorrCountry,CorrPinCode,Mobile,Email,GuardMobile,Nationality,AdharNo)" & _
    "values(@RegNo,@Student,@Course,@Sem,@AdmType,@Gender,@Batch,@DOB,@FatherName,@FatherOccupation,@Income,@FamilyIncome,@MotherName,@MotherOccupation,@Religion,@CasteCategory,@Institue,@BloodGroup,@CorrAddress1,@CorrAddress2,@CorrCity,@CorrDistt,@CorrState,@CorrCountry,@CorrPinCode,@Mobile,@Email,@GuardMobile,@Nationality,@AdharNo)", con)
                cmd.CommandType = CommandType.Text

                cmd.Parameters.AddWithValue("@RegNo", RegNo)
                cmd.Parameters.AddWithValue("@Student", Student)
                cmd.Parameters.AddWithValue("@Course", Course)
                cmd.Parameters.AddWithValue("@Sem", Sem)
                cmd.Parameters.AddWithValue("@AdmType", AdmType)
                cmd.Parameters.AddWithValue("@Gender", Gender)
                cmd.Parameters.AddWithValue("@Batch", Batch)
                cmd.Parameters.AddWithValue("@DOB", DOB)
                cmd.Parameters.AddWithValue("@FatherName", FatherName)
                cmd.Parameters.AddWithValue("@FatherOccupation", FatherOccupation)
                cmd.Parameters.AddWithValue("@Income", Income)
                cmd.Parameters.AddWithValue("@FamilyIncome", FamilyIncome)
                cmd.Parameters.AddWithValue("@MotherName", MotherName)
                cmd.Parameters.AddWithValue("@MotherOccupation", MotherOccupation)
                cmd.Parameters.AddWithValue("@Religion", Religion)
                cmd.Parameters.AddWithValue("@CasteCategory", CasteCategory)
                cmd.Parameters.AddWithValue("@Institue", Institue)
                cmd.Parameters.AddWithValue("@BloodGroup", BloodGroup)
                cmd.Parameters.AddWithValue("@CorrAddress1", CorrAddress1)
                cmd.Parameters.AddWithValue("@CorrAddress2", CorrAddress2)
                cmd.Parameters.AddWithValue("@CorrCity", CorrCity)
                cmd.Parameters.AddWithValue("@CorrDistt", CorrDistt)
                cmd.Parameters.AddWithValue("@CorrState", CorrState)
                cmd.Parameters.AddWithValue("@CorrCountry", CorrCountry)
                cmd.Parameters.AddWithValue("@CorrPinCode", CorrPinCode)
                cmd.Parameters.AddWithValue("@Mobile", Mobile)
                cmd.Parameters.AddWithValue("@Email", Email)
                cmd.Parameters.AddWithValue("@GuardMobile", GuardMobile)
                cmd.Parameters.AddWithValue("@Nationality", Nationality)
                cmd.Parameters.AddWithValue("@AdharNo", AdharNo)

                con.Open()
                cmd.ExecuteNonQuery()
                con.Close()

                fillgridwrongleads()
            End Using
        End Using
    End Sub

    Protected Sub linkwrong_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles linkwrong.Click
        grdwrongpanel.Visible = True
        Exceldownload()



        grdwrongpanel.Visible = False
    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        ' Verifies that the control is rendered
    End Sub

   

    Private Sub Exceldownload()
        Response.Clear()
        Response.Buffer = True
        Response.AddHeader("content-disposition", "attachment;filename=Wrongdata.xls")
        Response.Charset = ""
        Response.ContentType = "application/vnd.ms-excel"
        Using sw As New StringWriter()
            Dim hw As New HtmlTextWriter(sw)

            'To Export all pages
            grdwrongpanel.AllowPaging = False
            Me.bindgrid()

            grdwrongpanel.HeaderRow.BackColor = Color.White
            For Each cell As TableCell In grdwrongpanel.HeaderRow.Cells
                cell.BackColor = grdwrongpanel.HeaderStyle.BackColor
            Next
            For Each row As GridViewRow In grdwrongpanel.Rows
                row.BackColor = Color.White
                For Each cell As TableCell In row.Cells
                    If row.RowIndex Mod 2 = 0 Then
                        cell.BackColor = grdwrongpanel.AlternatingRowStyle.BackColor
                    Else
                        cell.BackColor = grdwrongpanel.RowStyle.BackColor
                    End If
                    cell.CssClass = "textmode"
                Next
            Next

            grdwrongpanel.RenderControl(hw)
            'style to format numbers to string
            Dim style As String = "<style> .textmode { } </style>"
            Response.Write(style)
            Response.Output.Write(sw.ToString())
            Response.Flush()


            Using con As New SqlConnection(constr)
                Dim cmd As New SqlCommand()
                Dim sql As String = "Truncate table TemporaryWrongimr"
                cmd.CommandText = sql
                cmd.Connection = con
                con.Open()
                cmd.ExecuteNonQuery()
                con.Close()

            End Using

            Response.End()



        End Using
    End Sub

End Class
