Imports System.Data
Imports System.Drawing
Imports System.Configuration
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic

Partial Class Examinationjune_Section
    Inherits System.Web.UI.Page
    Dim constr As String = ConfigurationManager.ConnectionStrings("myconnection").ConnectionString
    Protected Sub DetailsView1_ItemInserted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DetailsViewInsertedEventArgs) Handles DetailsView1.ItemInserted
        If e.Exception Is Nothing Then
            GridView1.DataBind()
            DetailsView1.ChangeMode(DetailsViewMode.Insert)
            SaralMsg.Messagebx.Alert(Me, "Successfully Inserted")
        Else
            e.ExceptionHandled = True
            e.KeepInInsertMode = True
            Dim sqlex As SqlException = TryCast(e.Exception, SqlException)
            If (Not sqlex Is Nothing) AndAlso (sqlex.Number = 2627) Then
                SaralMsg.Messagebx.Alert(Me, "Class Already Exist")
            Else
                SaralMsg.Messagebx.Alert(Me, "Error on saving")
            End If
        End If
    End Sub
    Protected Sub DetailsView1_ItemUpdated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DetailsViewUpdatedEventArgs) Handles DetailsView1.ItemUpdated
        If e.Exception Is Nothing Then
            GridView1.DataBind()
            DetailsView1.ChangeMode(DetailsViewMode.Insert)
            SaralMsg.Messagebx.Alert(Me, "Successfully Updated")
        Else
            e.ExceptionHandled = True
            e.KeepInEditMode = True
            SaralMsg.Messagebx.Alert(Me, "Unable to update")
        End If
    End Sub

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(GridView1, "Select$" & e.Row.RowIndex)
            e.Row.ToolTip = "Click to select this row."
        End If


        If e.Row.RowType = System.Web.UI.WebControls.DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#65a9d7'")
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;")
        End If
    End Sub
    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.SelectedIndexChanged
        For Each row As GridViewRow In GridView1.Rows
            If row.RowIndex = GridView1.SelectedIndex Then
                row.BackColor = ColorTranslator.FromHtml("#A1DCF2")
                row.ToolTip = String.Empty
            Else
                row.BackColor = ColorTranslator.FromHtml("#FFFFFF")
                row.ToolTip = "Click to select this row."
            End If
        Next



        DetailsView1.DataBind()
        DetailsView1.ChangeMode(DetailsViewMode.Edit)
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
       
    End Sub
    Protected Sub sdsclassroom_Updating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceCommandEventArgs) Handles sdsclassroom.Updating
        sdsclassroom.UpdateParameters("ClassesID").DefaultValue = GridView1.SelectedValue
    End Sub
End Class
