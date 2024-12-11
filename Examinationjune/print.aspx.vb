'Design And Developed By Avaneesh Yadav
Imports System
Imports System.Data
Imports System.Configuration
Imports System.Collections
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
Partial Class Reports_print
    Inherits System.Web.UI.Page

    Protected Sub Page_Load1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim ctrl As Control = CType(Session("ctrl"), Control)
        ClientScript.RegisterStartupScript(Me.GetType(), "closePage", "window.onunload = CloseWindow();")
        Printweb.PrintWebControl(ctrl)

    End Sub
End Class
