Imports System.Data
Imports System.Text
Imports System.Configuration
Imports System.Data.SqlClient
Imports System.Collections.Specialized
Partial Class StudentAdm_RequiredDoc
    Inherits System.Web.UI.Page
    Dim constr As String = ConfigurationManager.ConnectionStrings("myconnection").ConnectionString

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            ViewState("Sessionid") = Request.QueryString("s")
            ViewState("ayid") = Request.QueryString("ay")
            fetchacademicyear()

            BindGridDoc()
            fetchdocument()
            BindMastDoc()
            BindDocList()

        End If
    End Sub

    Private Sub BindGridDoc()
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand()
                Dim query As String = "Select c.*,r.docid,r.Reqdocid from Exam_CourseSession cs left join Exam_Course c on c.Courseid=cs.Courseid left join Requireddoc r on r.Courseid=c.Courseid where cs.SessionId='" & ViewState("Sessionid") & "' order by c.Course"
                cmd.CommandText = query
                Using sda As New SqlDataAdapter()
                    cmd.Connection = con
                    sda.SelectCommand = cmd
                    Using dt As New DataTable()
                        sda.Fill(dt)
                        grdsearch.DataSource = dt
                        grdsearch.DataBind()
                    End Using
                End Using
            End Using
        End Using
    End Sub

    Private Sub BindMastDoc()
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand()
                Dim query As String = "Select * from EssentialDoc order by EssentialDoc"
                cmd.CommandText = query
                Using sda As New SqlDataAdapter()
                    cmd.Connection = con
                    sda.SelectCommand = cmd
                    Using dt As New DataTable()
                        sda.Fill(dt)
                        GrdDocument.DataSource = dt
                        GrdDocument.DataBind()
                    End Using
                End Using
            End Using
        End Using
    End Sub

    Private Sub BindDocList()
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand()
                Dim query As String = "Select * from EssentialDoc order by EssentialDoc"
                cmd.CommandText = query
                Using sda As New SqlDataAdapter()
                    cmd.Connection = con
                    sda.SelectCommand = cmd
                    Using dt As New DataTable()
                        sda.Fill(dt)
                        GridDocList.DataSource = dt
                        GridDocList.DataBind()
                    End Using
                End Using
            End Using
        End Using
    End Sub

    Private Sub CopyToProg()
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand()
                Dim query As String = "Select c.*,r.docid from Exam_CourseSession cs left join Exam_Course c on c.Courseid=cs.Courseid Collate Database_Default left join Requireddoc r on r.Courseid=c.Courseid where cs.SessionId='" & ViewState("Sessionid") & "' and cs.Courseid Collate Database_Default not in (select Courseid from Requireddoc where Sessionid='" & ViewState("Sessionid") & "') order by c.Course"
                cmd.CommandText = query
                Using sda As New SqlDataAdapter()
                    cmd.Connection = con
                    sda.SelectCommand = cmd
                    Using dt As New DataTable()
                        sda.Fill(dt)
                        Grdcopy.DataSource = dt
                        Grdcopy.DataBind()
                    End Using
                End Using
            End Using
        End Using
    End Sub
   
    Protected Sub grdsearch_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdsearch.RowCommand
        ViewState("Courseid") = ""
        ViewState("docid") = ""
        If e.CommandName = "Documents" Then
            grdsearch.SelectedIndex = e.CommandArgument
            Dim rowIndex As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = grdsearch.Rows(rowIndex)
            ViewState("Courseid") = grdsearch.SelectedDataKey(0)
            lblcourse.Text = row.Cells(2).Text
            ViewState("Reqdocid") = grdsearch.SelectedDataKey(1)

            Using con As New SqlConnection(constr)
                Using cmd As New SqlCommand()
                    Dim query As String = "Select * from Requireddoc where COurseid='" & ViewState("Courseid") & "'"
                    cmd.CommandText = query
                    Using sda As New SqlDataAdapter()
                        cmd.Connection = con
                        sda.SelectCommand = cmd
                        Using dt As New DataTable()
                            sda.Fill(dt)
                            If dt.Rows.Count Then
                                btnAddoc.Text = "Update"
                            Else
                                btnAddoc.Text = "Save"
                            End If
                        End Using
                    End Using
                End Using
            End Using


            paneldocuments.Visible = True
            pnlRequired.Visible = False

            For Each Row1 As GridViewRow In GridDocList.Rows
                TryCast(Row1.FindControl("btnselect"), CheckBox).Checked = False
            Next

            fetchdoccheck()

        End If

        If e.CommandName = "Documetsco" Then
            grdsearch.SelectedIndex = e.CommandArgument
            Dim rowIndex As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = grdsearch.Rows(rowIndex)
            ViewState("docid") = row.Cells(4).Text
            panelcopyprogram.Visible = True
            pnlRequired.Visible = False
            CopyToProg()


            'fetchdoccheck()

        End If

    End Sub

    Protected Sub btnback_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnback.Click
        paneldocuments.Visible = False
        pnlRequired.Visible = True
        BindGridDoc()
        fetchdocument()
    End Sub

    Protected Sub btnAddoc_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddoc.Click

        If btnAddoc.Text = "Save" Then
            Using con As SqlConnection = New SqlConnection(constr)
                Dim query As String = ""
                Dim condition As String = String.Empty
                For Each Row As GridViewRow In GridDocList.Rows
                    If (TryCast(Row.FindControl("btnselect"), CheckBox)).Checked Then
                        condition += String.Format("{0},", Row.Cells(2).Text)
                    End If
                Next

                If Not String.IsNullOrEmpty(condition) Then
                    query = "insert into Requireddoc(Courseid,Docid,Ayid,Sessionid,dated) values(@Courseid,@Docid,@Ayid,@Sessionid,@dated)"
                    condition = String.Format("{0}", condition.Substring(0, condition.Length - 1))

                    Dim cmd As SqlCommand = New SqlCommand(query, con)
                    cmd.Parameters.AddWithValue("@Courseid", ViewState("Courseid"))
                    cmd.Parameters.AddWithValue("@Docid", condition)
                    cmd.Parameters.AddWithValue("@Ayid", ViewState("ayid"))
                    cmd.Parameters.AddWithValue("@Sessionid", ViewState("Sessionid"))
                    cmd.Parameters.AddWithValue("@dated", Date.Now())
                    con.Open()
                    cmd.ExecuteNonQuery()
                    con.Close()

                    SaralMsg.Messagebx.Alert(Me, "Successfully Save")

                    fetchdoccheck()


                End If


            End Using

        Else

            Using con As SqlConnection = New SqlConnection(constr)
                Dim query As String = ""
                Dim condition As String = String.Empty
                For Each Row As GridViewRow In GridDocList.Rows
                    If (TryCast(Row.FindControl("btnselect"), CheckBox)).Checked Then
                        condition += String.Format("{0},", Row.Cells(2).Text)
                    End If
                Next

                If Not String.IsNullOrEmpty(condition) Then
                    query = "update Requireddoc set Docid=@Docid where Reqdocid='" & ViewState("Reqdocid") & "'"
                    condition = String.Format("{0}", condition.Substring(0, condition.Length - 1))

                    Dim cmd As SqlCommand = New SqlCommand(query, con)

                    cmd.Parameters.AddWithValue("@Docid", condition)
                   
                    con.Open()
                    cmd.ExecuteNonQuery()
                    con.Close()

                    SaralMsg.Messagebx.Alert(Me, "Successfully Update")

                    fetchdoccheck()

                Else
                    query = "update Requireddoc set Docid=@Docid where Reqdocid='" & ViewState("Reqdocid") & "'"


                    Dim cmd As SqlCommand = New SqlCommand(query, con)

                    cmd.Parameters.AddWithValue("@Docid", "")

                    con.Open()
                    cmd.ExecuteNonQuery()
                    con.Close()

                    SaralMsg.Messagebx.Alert(Me, "Successfully Update")

                    fetchdoccheck()
                End If


            End Using

        End If
        
    End Sub

    Private Sub fetchdoccheck()
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand()
                Dim query As String = "Select * from Requireddoc where SessionId='" & ViewState("Sessionid") & "' and Courseid='" & ViewState("Courseid") & "'"
                cmd.CommandText = query
                Using sda As New SqlDataAdapter()
                    cmd.Connection = con
                    sda.SelectCommand = cmd
                    Using dt As New DataTable()
                        sda.Fill(dt)
                        If dt.Rows.Count Then

                            Dim doc As String = dt.Rows(0)("Docid").ToString
                            Dim no As String() = doc.Split(New Char() {","c})

                            For Each part As String In no

                                For Each row As GridViewRow In GridDocList.Rows
                                    If row.Cells("2").Text = part Then
                                        TryCast(row.FindControl("btnselect"), CheckBox).Checked = True

                                    End If
                                Next

                            Next


                        End If
                    End Using
                End Using
            End Using
        End Using
    End Sub

    Protected Sub btnsubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsubmit.Click

        Using con As SqlConnection = New SqlConnection(constr)
            Dim query As String = ""

            query = "select COunt(*) as Count from EssentialDoc where EssentialDoc='" & txtdocname.Text & "'"
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

                Dim query1 As String = "insert into EssentialDoc(EssentialDoc) values(@EssentialDoc)"

                Dim cmd1 As SqlCommand = New SqlCommand(query1, con)
                cmd1.Parameters.AddWithValue("@EssentialDoc", txtdocname.Text)

                con.Open()
                cmd1.ExecuteNonQuery()
                con.Close()

                SaralMsg.Messagebx.Alert(Me, "Successfully Save")
                BindMastDoc()
                txtdocname.Text = ""
            Else
                SaralMsg.Messagebx.Alert(Me, "Document Name Already Exists")
            End If

        End Using
    End Sub

    Private Sub fetchdocument()

        For Each row As GridViewRow In grdsearch.Rows
            Using con As New SqlConnection(constr)
                Using cmd As New SqlCommand()
                    Dim query As String = "Select c.*,r.docid from Exam_CourseSession cs left join Exam_Course c on c.Courseid=cs.Courseid left join Requireddoc r on r.Courseid=c.Courseid where cs.SessionId='" & ViewState("Sessionid") & "' and r.Courseid='" & row.Cells("1").Text & "'"
                    cmd.CommandText = query
                    Using sda As New SqlDataAdapter()
                        cmd.Connection = con
                        sda.SelectCommand = cmd
                        Using dt As New DataTable()
                            sda.Fill(dt)

                            If dt.Rows.Count Then

                                Dim docid As String = dt.Rows(0)("docid").ToString
                                Dim condition As String = String.Empty
                                If docid <> "" Then
                                    Dim no As String() = docid.Split(New Char() {","c})
                                    For Each part As String In no
                                        Dim query1 As String = "select * from EssentialDoc where EssentialDocID='" & part & "'"
                                        cmd.CommandText = query1
                                        Using sda1 As New SqlDataAdapter()
                                            cmd.Connection = con
                                            sda1.SelectCommand = cmd
                                            Using dt1 As New DataTable()
                                                sda1.Fill(dt1)
                                                condition += String.Format("{0},", dt1.Rows(0)("EssentialDoc").ToString())

                                            End Using
                                        End Using
                                    Next

                                    condition = String.Format("{0}", condition.Substring(0, condition.Length - 1))
                                End If

                                TryCast(row.FindControl("lbldoc"), Label).Text = condition

                            End If

                        End Using
                    End Using
                End Using
            End Using
        Next


    End Sub

    Protected Sub btncopydoc_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btncopydoc.Click

        For Each row As GridViewRow In Grdcopy.Rows

            If TryCast(row.FindControl("btnselect"), CheckBox).Checked = True Then

                Using con As SqlConnection = New SqlConnection(constr)
                    Dim query As String = ""
                   


                    query = "insert into Requireddoc(Courseid,Docid,Ayid,Sessionid,dated) values(@Courseid,@Docid,@Ayid,@Sessionid,@dated)"


                    Dim cmd As SqlCommand = New SqlCommand(query, con)
                    cmd.Parameters.AddWithValue("@Courseid", row.Cells(2).Text)
                    cmd.Parameters.AddWithValue("@Docid", ViewState("docid"))
                    cmd.Parameters.AddWithValue("@Ayid", ViewState("ayid"))
                    cmd.Parameters.AddWithValue("@Sessionid", ViewState("Sessionid"))
                    cmd.Parameters.AddWithValue("@dated", Date.Now())
                    con.Open()
                    cmd.ExecuteNonQuery()
                    con.Close()

                    SaralMsg.Messagebx.Alert(Me, "Successfully Save")


                    CopyToProg()




                End Using

            End If

        Next

    End Sub

    Protected Sub btnbackcopy_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnbackcopy.Click
        panelcopyprogram.Visible = False
        pnlRequired.Visible = True
        BindGridDoc()
        fetchdocument()
    End Sub

    Protected Sub btnAddDoc_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddDoc.Click
        paneldocuments.Visible = False
        pnlmaster.Visible = True
    End Sub

    Protected Sub btnbackmaster_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnbackmaster.Click
        pnlmaster.Visible = False
        paneldocuments.Visible = True
        BindDocList()
        fetchdoccheck()
    End Sub

    Private Sub fetchacademicyear()
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand()
                Dim query As String = "Select Academicyear from Exam_Session where Sessionid='" & ViewState("Sessionid") & "'"
                cmd.CommandText = query
                Using sda As New SqlDataAdapter()
                    cmd.Connection = con
                    sda.SelectCommand = cmd
                    Using dt As New DataTable()
                        sda.Fill(dt)
                        If dt.Rows.Count Then
                            lblacademicyear.Text = dt.Rows(0)("Academicyear").ToString()
                        End If
                    End Using
                End Using
            End Using
        End Using
    End Sub

    Protected Sub GrdDocument_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GrdDocument.RowCommand
        If e.CommandName = "Edit" Then
            GrdDocument.SelectedIndex = e.CommandArgument
            Dim rowIndex As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = GrdDocument.Rows(rowIndex)
            ViewState("Essentialdocid") = GrdDocument.SelectedDataKey(0)
            btnsubmit.Visible = False
            btnupdate.Visible = True
            Using con As New SqlConnection(constr)
                Using cmd As New SqlCommand()
                    Dim query As String = "Select * from EssentialDoc where EssentialDocid='" & ViewState("Essentialdocid") & "'"
                    cmd.CommandText = query
                    Using sda As New SqlDataAdapter()
                        cmd.Connection = con
                        sda.SelectCommand = cmd
                        Using dt As New DataTable()
                            sda.Fill(dt)
                            If dt.Rows.Count Then
                                txtdocname.Text = dt.Rows(0)("EssentialDoc").ToString()
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
          
            query = "update EssentialDoc set EssentialDoc=@EssentialDoc where EssentialDocid='" & ViewState("Essentialdocid") & "'"


                Dim cmd As SqlCommand = New SqlCommand(query, con)

            cmd.Parameters.AddWithValue("@EssentialDoc", txtdocname.Text)

                con.Open()
                cmd.ExecuteNonQuery()
                con.Close()

                SaralMsg.Messagebx.Alert(Me, "Successfully Update")

            BindMastDoc()
            txtdocname.Text = ""
            btnsubmit.Visible = True
            btnupdate.Visible = False

        End Using
    End Sub


    Protected Sub OnRowEditing(ByVal sender As Object, ByVal f As GridViewEditEventArgs)
    End Sub

    Protected Sub OnRowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
    End Sub
End Class
