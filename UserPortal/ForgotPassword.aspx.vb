
Partial Class UserPortal_ForgotPassword
    Inherits System.Web.UI.Page

    Protected Sub btnResetPwd_Click(sender As Object, e As System.EventArgs) Handles btnResetPwd.Click
        PnlForgetPassword.Visible = False
        PnlOTP.Visible = True
    End Sub
End Class
